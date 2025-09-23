namespace SimuladorSO.Arquivos
{
    public class SimFileHandle
    {
        public SimFile Arquivo { get; }
        public int Cursor { get; set; }
        public SimFileHandle(SimFile f) { Arquivo = f; Cursor = 0; }
    }
}