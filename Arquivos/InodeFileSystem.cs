using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimuladorSO.Arquivos
{
    // Simples implementação de um sistema de arquivos baseado em inodes (apenas conceitual)
    public class Inode
    {
        public int Id { get; set; }
        public long Size { get; set; }
        public List<int> Blocks { get; set; } = new List<int>();
        public bool IsDirectory { get; set; } = false;
        public Dictionary<string, int> DirectoryEntries { get; set; } // name -> inode
    }

    public class InodeFileSystem
    {
        private Dictionary<int, Inode> inodes = new Dictionary<int, Inode>();
        private byte[][] blocks;
        private const int BlockSize = 4096;
        private int nextInodeId = 1;

        public InodeFileSystem(int totalBlocks = 1024)
        {
            blocks = new byte[totalBlocks][];
            for (int i = 0; i < totalBlocks; i++) blocks[i] = new byte[BlockSize];
            // root
            var root = new Inode { Id = nextInodeId++, IsDirectory = true, DirectoryEntries = new Dictionary<string,int>() };
            inodes[root.Id] = root;
            RootInodeId = root.Id;
        }

        public int RootInodeId { get; private set; }

        public int CreateFile(int parentInodeId, string name)
        {
            if (!inodes.ContainsKey(parentInodeId)) throw new DirectoryNotFoundException();
            var parent = inodes[parentInodeId];
            if (!parent.IsDirectory) throw new IOException("Parent is not directory");
            var inode = new Inode { Id = nextInodeId++ , IsDirectory = false };
            inodes[inode.Id] = inode;
            parent.DirectoryEntries[name] = inode.Id;
            return inode.Id;
        }

        public int CreateDirectory(int parentInodeId, string name)
        {
            if (!inodes.ContainsKey(parentInodeId)) throw new DirectoryNotFoundException();
            var parent = inodes[parentInodeId];
            if (!parent.IsDirectory) throw new IOException("Parent is not directory");
            var inode = new Inode { Id = nextInodeId++, IsDirectory = true, DirectoryEntries = new Dictionary<string,int>() };
            inodes[inode.Id] = inode;
            parent.DirectoryEntries[name] = inode.Id;
            return inode.Id;
        }

        public void WriteToInode(int inodeId, byte[] data)
        {
            if (!inodes.ContainsKey(inodeId)) throw new FileNotFoundException();
            var inode = inodes[inodeId];
            // naive allocation: use successive free blocks
            int needed = (data.Length + BlockSize - 1) / BlockSize;
            inode.Blocks.Clear();
            int allocated = 0;
            for (int i = 0; i < blocks.Length && allocated < needed; i++)
            {
                if (blocks[i] == null) { blocks[i] = new byte[BlockSize]; }
                inode.Blocks.Add(i);
                Array.Copy(data, allocated*BlockSize, blocks[i], 0, Math.Min(BlockSize, data.Length - allocated*BlockSize));
                allocated++;
            }
            inode.Size = data.Length;
        }

        public byte[] ReadInode(int inodeId)
        {
            if (!inodes.ContainsKey(inodeId)) throw new FileNotFoundException();
            var inode = inodes[inodeId];
            byte[] result = new byte[inode.Size];
            int copied = 0;
            foreach (var b in inode.Blocks)
            {
                int toCopy = (int)Math.Min(BlockSize, inode.Size - copied);
                Array.Copy(blocks[b], 0, result, copied, toCopy);
                copied += toCopy;
                if (copied >= inode.Size) break;
            }
            return result;
        }
    }
}
