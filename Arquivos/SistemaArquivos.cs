using System;


namespace SimuladorSO.Arquivos
{
    public class SistemaArquivos
    {
        public SimDirectory Raiz { get; } = new("/");


        public SimFile CriarArquivo(string caminho, int dono)
        {
            var segs = caminho.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
            var dir = Raiz;
            for (int i = 0; i < segs.Length - 1; i++)
            {
                if (!dir.Subdirs.ContainsKey(segs[i])) dir.Subdirs[segs[i]] = new(segs[i]);
                dir = dir.Subdirs[segs[i]];
            }
            var fname = segs[^1];
            var f = new SimFile(fname, dono);
            dir.Arquivos[fname] = f;
            return f;
        }


        public SimFile? ObterArquivo(string caminho)
        {
            var segs = caminho.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
            var dir = Raiz;
            for (int i = 0; i < segs.Length - 1; i++)
            {
                if (!dir.Subdirs.TryGetValue(segs[i], out var next)) return null;
                dir = next;
            }
            var fname = segs[^1];
            return dir.Arquivos.TryGetValue(fname, out var f) ? f : null;
        }
    }
}