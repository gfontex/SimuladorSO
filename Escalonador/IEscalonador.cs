using SimuladorSO.Processos;


namespace SimuladorSO.Escalonador
{
    public interface IEscalonador
    {
        void Enfileirar(PCB p);
        PCB? Proximo();
        void Tick(int clock);
        int TrocasContexto { get; }
    }
}