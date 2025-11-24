using System;
using System.Collections.Generic;
using SimuladorSO.Processos;

namespace SimuladorSO.Processador
{
    // Very small conceptual 5-stage pipeline simulation structure.
    public enum PipelineStage { IF, ID, EX, MEM, WB }

    public class PipelineRegister
    {
        public string Instruction { get; set; }
        public PCB Owner { get; set; }
        public bool Empty => string.IsNullOrEmpty(Instruction);
    }

    public class Pipeline
    {
        public PipelineRegister IF_ID = new PipelineRegister();
        public PipelineRegister ID_EX = new PipelineRegister();
        public PipelineRegister EX_MEM = new PipelineRegister();
        public PipelineRegister MEM_WB = new PipelineRegister();

        // Advance pipeline by one clock cycle
        public void Tick()
        {
            MEM_WB = EX_MEM;
            EX_MEM = ID_EX;
            ID_EX = IF_ID;
            IF_ID = new PipelineRegister(); // new fetch slot
        }

        // Inject instruction into IF stage (fetch)
        public bool Fetch(string instr, PCB owner)
        {
            if (!IF_ID.Empty) return false; // fetch stall
            IF_ID.Instruction = instr;
            IF_ID.Owner = owner;
            return true;
        }

        public void Flush()
        {
            IF_ID = new PipelineRegister();
            ID_EX = new PipelineRegister();
            EX_MEM = new PipelineRegister();
            MEM_WB = new PipelineRegister();
        }
    }
}
