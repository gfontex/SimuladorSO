namespace SimuladorSO.Memoria
{
    public class Moldura
    {
        public int Numero { get; }
        public Pagina? Pagina { get; set; }
        public Moldura(int n) => Numero = n;
    }
}