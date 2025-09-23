namespace SimuladorSO.Processos
{
    public class TCB
    {
        private static int NEXT_TID = 1;
        public int TID { get; }
        public PCB ProcessoPai { get; }
        public int PonteiroPilha { get; set; }
        public ProcState Estado { get; set; }


        public TCB(PCB parent)
        {
            TID = NEXT_TID++;
            ProcessoPai = parent;
            Estado = ProcState.Novo;
        }
    }
}