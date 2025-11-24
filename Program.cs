using System;
using SimuladorSO.Processos;
using SimuladorSO.Escalonador;

namespace SimuladorSO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Simulador de Escalonamento FCFS ===\n");

            // Criando processos (tempo de CPU necessário, prioridade)
            PCB p1 = new PCB(5, 1);
            PCB p2 = new PCB(3, 2);
            PCB p3 = new PCB(4, 1);

            // Criando o escalonador FCFS
            EscalonadorFCFS escalonador = new EscalonadorFCFS();

            // Enfileirando processos
            escalonador.Enfileirar(p1);
            escalonador.Enfileirar(p2);
            escalonador.Enfileirar(p3);

            Console.WriteLine("Fila inicial:");
            Console.WriteLine($" - P{p1.PID} (CPU necessário: {p1.TempoCpuNecessario})");
            Console.WriteLine($" - P{p2.PID} (CPU necessário: {p2.TempoCpuNecessario})");
            Console.WriteLine($" - P{p3.PID} (CPU necessário: {p3.TempoCpuNecessario})\n");

            // Executando até a fila acabar
            PCB? atual;
            while ((atual = escalonador.Proximo()) != null)
            {
                Console.WriteLine($"\nExecutando P{atual.PID}...");

                while (!atual.Finalizado)
                {
                    atual.TempoCpuUsado++;
                    Console.WriteLine($"  P{atual.PID} rodando... usado: {atual.TempoCpuUsado}/{atual.TempoCpuNecessario}");
                }

                atual.Estado = ProcState.Finalizado;
                atual.TempoFim = atual.TempoCpuUsado;
                Console.WriteLine($"*** P{atual.PID} terminou! ***");
            }

            Console.WriteLine($"\nTodos os processos foram concluídos!");
        }
    }
}