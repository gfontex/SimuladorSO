using System;
using System.Collections.Generic;
using System.Linq;
using SimuladorSO.Processos;

namespace SimuladorSO.Escalonador
{
    public class EscalonadorSJF : IEscalonador
    {
        private List<PCB> fila = new List<PCB>();

        public void AdicionarProcesso(PCB pcb)
        {
            lock (fila)
            {
                fila.Add(pcb);
                // manter ordenado por tempo estimado restante
                fila = fila.OrderBy(p => p.EstimatedRemainingTime).ToList();
            }
        }

        public PCB ObterProximo()
        {
            lock (fila)
            {
                if (fila.Count == 0) return null;
                var pcb = fila[0];
                fila.RemoveAt(0);
                return pcb;
            }
        }
    }
}
