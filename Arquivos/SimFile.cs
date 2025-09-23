using System;
using System.Collections.Generic;


namespace SimuladorSO.Arquivos
{
    public class SimFile
    {
        public string Nome { get; }
        public List<byte> Dados { get; } = new();
        public DateTime Criado { get; } = DateTime.UtcNow;
        public DateTime Modificado { get; set; } = DateTime.UtcNow;
        public int DonoPid { get; }
        public SimFile(string nome, int dono) { Nome = nome; DonoPid = dono; }
    }
}