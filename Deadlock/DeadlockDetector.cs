using System;
using System.Collections.Generic;
using System.Linq;
using SimuladorSO.Processos;

namespace SimuladorSO.Deadlock
{
    // Detector simples de deadlock baseado em grafo de alocação de recursos
    public class DeadlockDetector
    {
        // Map: resource -> process holding it (or null)
        private Dictionary<string, int?> resourceOwner = new Dictionary<string, int?>();
        // Map: process -> list of resources requested
        private Dictionary<int, List<string>> waiting = new Dictionary<int, List<string>>();

        public void SetOwner(string resource, int? processId)
        {
            resourceOwner[resource] = processId;
        }

        public void Request(int processId, string resource)
        {
            if (!waiting.ContainsKey(processId)) waiting[processId] = new List<string>();
            waiting[processId].Add(resource);
        }

        public void Release(int processId, string resource)
        {
            if (waiting.ContainsKey(processId)) waiting[processId].Remove(resource);
            if (resourceOwner.ContainsKey(resource) && resourceOwner[resource] == processId) resourceOwner[resource] = null;
        }

        // Detect cycle in wait-for graph
        public List<int> DetectDeadlock()
        {
            // build wait-for graph: edge P -> Q if P waits for resource held by Q
            var graph = new Dictionary<int, List<int>>();
            foreach (var kv in waiting)
            {
                int pid = kv.Key;
                foreach (var res in kv.Value)
                {
                    if (resourceOwner.TryGetValue(res, out var owner) && owner.HasValue)
                    {
                        if (!graph.ContainsKey(pid)) graph[pid] = new List<int>();
                        graph[pid].Add(owner.Value);
                    }
                }
            }

            // detect cycle using DFS
            var visiting = new HashSet<int>();
            var visited = new HashSet<int>();
            var stack = new List<int>();
            List<int> cycle = null;

            foreach (var node in graph.Keys)
            {
                if (visited.Contains(node)) continue;
                if (Dfs(node)) { cycle = new List<int>(stack); break; }
            }

            bool Dfs(int u)
            {
                if (visiting.Contains(u)) { stack.Add(u); return true; }
                if (visited.Contains(u)) return false;
                visiting.Add(u);
                if (graph.TryGetValue(u, out var outs))
                {
                    foreach (var v in outs)
                    {
                        if (Dfs(v)) return true;
                    }
                }
                visiting.Remove(u);
                visited.Add(u);
                return false;
            }

            return cycle ?? new List<int>();
        }
    }
}
