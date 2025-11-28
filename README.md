Simulador de Sistema
Este projeto é um simulador de Sistema Operacional implementado em C# baseado no conteúdo visto na faculdade FHO. O objetivo é oferecer uma base simplificada para estudar conceitos fundamentais de SO: processos, escalonamento, gerenciamento de memória, E/S e sistema de arquivos.

Nome: Gabriel Fontes    RA:112958

Funcionalidades Implementadas
•	Processos e Threads
o	Estruturas PCB e TCB.
o	Estados: Novo, Pronto, Executando, Bloqueado, Finalizado.
•	Escalonador de CPU
o	Algoritmos: FCFS, Round-Robin (quantum configurável) e Prioridade.
o	Contabilização de trocas de contexto.
•	Gerenciamento de Memória
o	Paginação simples com molduras.
o	Política FIFO para substituição.
o	Contagem de faltas de página.
•	Entrada e Saída (E/S)
o	Dispositivo genérico de bloco (ex.: disco) e de caractere (ex.: terminal).
o	Fila de pedidos de E/S e tempos de serviço.
•	Sistema de Arquivos
o	Estrutura hierárquica simples em memória.
o	Operações básicas: criar, abrir, escrever, ler.
•	Métricas e Log
o	Tempo de retorno médio.
o	Tempo de espera médio.
o	Utilização aproximada da CPU.
o	Número de trocas de contexto.
o	Faltas de página.
o	Log textual salvo em simlog.txt.

Como Executar
1.	Instale o .NET SDK 7+: Download aqui
2.	Crie um novo projeto console:
 	dotnet new console -n SimuladorSO
cd SimuladorSO
3.	Substitua o conteúdo de Program.cs pelo arquivo SimuladorSO_Basico.cs.
4.	Execute com:
 	dotnet run -- [opções]

 Parâmetros Disponíveis
•	--alg fcfs|rr|prio → Define o algoritmo de escalonamento.
•	--quantum N → Quantum usado no Round-Robin (padrão: 2).
•	--frames N → Número de molduras de memória.
•	--pagesize N → Tamanho de página (simulado).
•	--workload sample → Usa a carga de trabalho de exemplo.
Exemplo:
dotnet run -- --alg rr --quantum 3 --frames 4 --pagesize 4096 --workload sample

Saída do Programa
Durante a execução, o simulador: - Mostra no terminal os eventos (admissão, execução, bloqueio, fim de processos). - Gera o arquivo simlog.txt com todo o histórico de execução. - Ao final, imprime as métricas calculadas.
Exemplo de métricas:
Clock final: 42
Avg turnaround: 11.25
Avg waiting: 6.75
CPU utilization (approx): 68.00%
Page faults: 7
Context switches: 5
Device disk utilization time: 9
Device tty utilization time: 0
________________________________________
 Estrutura do Projeto
•	SimuladorSO_Basico.cs → Código principal do simulador.
•	README.md → Este arquivo com explicações.
•	simlog.txt → Log gerado em cada execução.

