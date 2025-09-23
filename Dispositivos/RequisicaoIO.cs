using System;


namespace SimuladorSO.Dispositivos
{
    public class RequisicaoIO
    {
        public int Pid { get; }
        public int Duracao { get; }
        public int EnfileiradoEm { get; }
        public Action? Callback { get; }


        public RequisicaoIO(int pid, int duracao, int enfileiradoEm, Action? cb)
        {
            Pid = pid;
            Duracao = duracao;
            EnfileiradoEm = enfileiradoEm;
            Callback = cb;
        }
    }
}