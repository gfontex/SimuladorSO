using System.Collections.Generic;
using SimuladorSO.Arquivos;


namespace SimuladorSO.Processos
{
    public class PCB
    {
        private static int NEXT_PID = 1;
        public int PID { get; }
        public int Prioridade { get; set; }
        public ProcState Estado { get; set; }
        public int ContadorPrograma { get; set; }
        public int[] Registradores { get; } = new int[8];
        public List<SimFileHandle> ArquivosAbertos { get; } = new();
        public int TempoChegada { get; set; }
        public int TempoInicio { get; set; } = -1;
        public int TempoFim { get; set; } = -1;
        public int TempoCpuNecessario { get; set; }
        public int TempoCpuUsado { get; set; } = 0;


        public PCB(int totalCpu, int prioridade)
        {
            PID = NEXT_PID++;
            TempoCpuNecessario = totalCpu;
            Prioridade = prioridade;
            Estado = ProcState.Novo;
        }


        public bool Finalizado => TempoCpuUsado >= TempoCpuNecessario;
    }
}