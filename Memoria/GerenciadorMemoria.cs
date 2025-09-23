using System.Collections.Generic;
using System.Linq;


namespace SimuladorSO.Memoria
{
    public class GerenciadorMemoria
    {
        public int TamanhoPagina { get; }
        public int QuantidadeMolduras { get; }
        public Moldura[] Molduras { get; }
        private Queue<int> filaFIFO = new();
        public int FaltasPagina { get; private set; }
        public int Acessos { get; private set; }


        public GerenciadorMemoria(int tamanhoPagina, int qtdMolduras)
        {
            TamanhoPagina = tamanhoPagina;
            QuantidadeMolduras = qtdMolduras;
            Molduras = Enumerable.Range(0, qtdMolduras).Select(i => new Moldura(i)).ToArray();
            foreach (var i in Enumerable.Range(0, qtdMolduras)) filaFIFO.Enqueue(i);
        }


        public int CarregarPagina(Pagina p)
        {
            Acessos++;
            var carregada = System.Array.FindIndex(Molduras, f => f.Pagina != null && f.Pagina.DonoPid == p.DonoPid && f.Pagina.Numero == p.Numero);
            if (carregada >= 0) return carregada;


            FaltasPagina++;
            var livre = System.Array.FindIndex(Molduras, f => f.Pagina == null);
            if (livre >= 0)
            {
                Molduras[livre].Pagina = p;
                filaFIFO.Enqueue(livre);
                return livre;
            }


            int vitima = -1;
            while (filaFIFO.Count > 0)
            {
                var cand = filaFIFO.Dequeue();
                if (Molduras[cand].Pagina != null) { vitima = cand; break; }
            }
            if (vitima == -1) vitima = 0;
            Molduras[vitima].Pagina = p;
            filaFIFO.Enqueue(vitima);
            return vitima;
        }


        public void ResetarEstatisticas() { FaltasPagina = 0; Acessos = 0; }
    }
}