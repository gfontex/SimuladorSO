using System.Collections.Generic;


namespace SimuladorSO.Dispositivos
{
    public class Dispositivo
    {
        public string Nome { get; }
        public Queue<RequisicaoIO> Fila { get; } = new();
        public int OcupadoAte { get; private set; } = -1;
        public int TempoUtilizacao { get; private set; } = 0;
        public List<(int tempo, RequisicaoIO req)> Pendentes { get; } = new();


        public Dispositivo(string nome) { Nome = nome; }


        public void Enfileirar(RequisicaoIO r) => Fila.Enqueue(r);


        public void Tick(int clock)
        {
            if (OcupadoAte > clock) { TempoUtilizacao++; return; }
            if (Fila.Count == 0) return;
            var req = Fila.Dequeue();
            OcupadoAte = clock + req.Duracao;
            TempoUtilizacao += req.Duracao;
            Pendentes.Add((OcupadoAte, req));
        }
    }
}