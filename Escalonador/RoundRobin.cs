using System;
using System.Collections.Generic;
using SimuladorSO.Processos;

namespace SimuladorSO.Escalonador
{
    public class EscalonadorRoundRobin : IEscalonador
    {
        public int Quantum { get; private set; }

        public EscalonadorRoundRobin(int quantum = 4)
        {
            Quantum = quantum;
        }

        public void AdicionarProcesso(PCB pcb)
        {
            lock (Fila) { Fila.Enqueue(pcb); }
        }

        private Queue<PCB> Fila = new Queue<PCB>();

        public PCB ObterProximo()
        {
            lock (Fila)
            {
                if (Fila.Count == 0) return null;
                var pcb = Fila.Dequeue();
                return pcb;
            }
        }

        // After running for up to Quantum, requeue if still runnable
        public void RequeueIfNeeded(PCB pcb)
        {
            if (pcb == null) return;
            if (!pcb.IsFinished && !pcb.IsBlocked)
            {
                AdicionarProcesso(pcb);
            }
        }
    }
}
