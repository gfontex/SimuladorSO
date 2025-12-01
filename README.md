🖥️ Simulador de Sistema Operacional

Projeto acadêmico em C# — Faculdade FHO
Autor: Gabriel Fontes • RA: 112958

📘 Sobre o Projeto

Este é um simulador didático de Sistema Operacional, desenvolvido para fins acadêmicos, baseado nos conteúdos ministrados na FHO.
Ele implementa de forma simplificada — porém realista — os principais componentes de um SO moderno:

✔ processos
✔ threads
✔ escalonamento
✔ memória virtual
✔ E/S
✔ sistema de arquivos
✔ pipeline de CPU
✔ detecção de deadlock
✔ logs e métricas detalhadas

O projeto evoluiu além da versão mínima da disciplina e hoje funciona como um mini-laboratório de Sistemas Operacionais.



✨ Funcionalidades
🧩 Processos e Threads

- Estrutura PCB (Process Control Block)

- Estrutura TCB (Thread Control Block)

- Estados: Novo, Pronto, Executando, Bloqueado, Finalizado

- Suporte a múltiplas threads por processo

- Contabilização completa de tempos e trocas de contexto

| Algoritmo       | Tipo           | Preempção | Observações                 |
| --------------- | -------------- | --------- | --------------------------- |
| **FCFS**        | Não preemptivo | ❌         | Simples e determinístico    |
| **Round-Robin** | Preemptivo     | ✔         | Quantum configurável        |
| **Prioridade**  | Ambos          | ✔         | Prioridades fixas           |
| **SJF**  | Não preemptivo | ❌         | Usa tempo estimado restante |

🧠 Memória

- Paginação simples

- Molduras de tamanho configurável

- Política de substituição FIFO

- Contagem de page faults

- Mapeamento página→moldura

🖨️ Entrada e Saída

- Dispositivo de bloco (Disco)

- Dispositivo de caractere (TTY)

- Fila interna de requisições

- Simulação de tempos de serviço

- Bloqueio e desbloqueio automático de processos

📁 Sistema de Arquivos
✔ Modelo tradicional (simples)

- Diretórios e arquivos em memória

- Criar → Abrir → Ler → Escrever

✔ Modelo avançado baseado em Inodes 

- Estrutura semelhante ao UNIX

- Diretórios mapeiam nomes → inode IDs

- Arquivos representados por índices numa tabela

- Blocos de dados simulados

⚙️ Pipeline da CPU 
Simulação completa das 5 etapas clássicas:

- IF – Instruction Fetch

- ID – Decode

- EX – Execute

- MEM – Memory Access

- WB – Write Back

Inclui:

- Avanço por ciclo

- Inserção de instruções

- Flush (ex.: branch mispredict)

🕸️ Detecção de Deadlock 

- Implementação de Wait-for Graph

Registro de:

- Quem possui recurso

- Quem está esperando

- Detecção de ciclos via DFS

- Identificação dos processos envolvidos no deadlock

📊 Métricas e Logs

Geração automática de:

- Tempo de retorno médio (turnaround)

- Tempo de espera médio

- Utilização aproximada da CPU

- Context switches

- Page faults

- Utilização de dispositivos

- Log completo em simlog.txt

⚙️ Como Executar
1️⃣ Instale o .NET 7+

https://dotnet.microsoft.com/en-us/download

2️⃣ Crie o projeto:

dotnet new console -n SimuladorSO
cd SimuladorSO

3️⃣ Substitua o Program.cs

Use o arquivo fornecido no projeto completo.

4️⃣ Execute com:
dotnet run -- [opções]

🔧 Parâmetros de Execução
| Parâmetro           | Descrição               |      |      |                                      |
| ------------------- | ----------------------- | ---- | ---- | ------------------------------------ |
| `--alg fcfs         | rr                      | prio | sjf` | Seleciona algoritmo de escalonamento |
| `--quantum N`       | Quantum do Round-Robin  |      |      |                                      |
| `--frames N`        | Molduras de memória     |      |      |                                      |
| `--pagesize N`      | Tamanho da página       |      |      |                                      |
| `--workload sample` | Usa workload de exemplo |      |      |                                      |

Exemplo:
dotnet run -- --alg rr --quantum 3 --frames 4 --pagesize 4096 --workload sample

📤 Exemplo de Saída
Clock final: 42
Avg turnaround: 11.25
Avg waiting: 6.75
CPU utilization (approx): 68.00%
Page faults: 7
Context switches: 5
Device disk utilization time: 9
Device tty utilization time: 0

🧩 Estrutura do Projeto
SimuladorSO/
 ├── Program.cs
 ├── Processos/
 │     ├── PCB.cs
 │     ├── TCB.cs
 ├── Escalonador/
 │     ├── FCFS.cs
 │     ├── RoundRobin.cs
 │     ├── Prioridade.cs
 │     └── SJF.cs
 ├── Memoria/
 │     ├── GerenciadorMemoria.cs
 ├── IO/
 │     ├── Dispositivo.cs
 │     ├── RequisicaoIO.cs
 ├── SistemaArquivos/
 │     ├── FS_Simples.cs
 │     └── InodeFileSystem.cs
 ├── Pipeline/
 │     └── PipelineCPU.cs
 ├── Deadlock/
 │     └── DeadlockDetector.cs
 ├── simlog.txt
 └── SimuladorSO.csproj
