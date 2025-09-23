using System.Collections.Generic;


namespace SimuladorSO.Arquivos
{
    public class SimDirectory
    {
        public string Nome { get; }
        public Dictionary<string, SimFile> Arquivos { get; } = new();
        public Dictionary<string, SimDirectory> Subdirs { get; } = new();
        public SimDirectory(string nome) { Nome = nome; }
    }
}