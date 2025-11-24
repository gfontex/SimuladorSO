using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimuladorSO.Processos
{
    // Representação simples de "threads" dentro de um PCB.
    public class SimThread
    {
        public int Id { get; private set; }
        public Action Entry { get; private set; }
        public bool IsFinished { get; private set; } = false;

        private static int nextId = 1;

        public SimThread(Action entry)
        {
            Id = nextId++;
            Entry = entry;
        }

        public void Run()
        {
            try
            {
                Entry?.Invoke();
            }
            finally
            {
                IsFinished = true;
            }
        }
    }
}
