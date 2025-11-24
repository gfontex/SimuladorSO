using System.Collections.Generic;
using SimuladorSO.Processos;


namespace SimuladorSO.Escalonador
{
    public class EscalonadorFCFS : IEscalonador
    {
        private Queue<PCB> fila = new();
        public int TrocasContexto { get; private set; } = 0;


        public void Enfileirar(PCB p)
        {
            p.Estado = ProcState.Pronto;
            fila.Enqueue(p);
        }


        public PCB? Proximo()
        {
            if (fila.Count == 0) return null;
            TrocasContexto++;
            var p = fila.Dequeue();
            p.Estado = ProcState.Executando;
            return p;
        }


        public void Tick(int clock) { /* FCFS não faz nada a cada tick por si só */ }
    }
}