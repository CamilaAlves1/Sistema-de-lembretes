using System;
using System.Collections.Generic;
using System.IO;

     namespace SistemaLembretes
    {
        class Lembrete
            {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public Lembrete(int id, string nome, DateTime data)
        {
            Id = id;
            Nome = nome;
            Data = data;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Lembrete> listaLembretes = new List<Lembrete>();
            int proximoId = 1;

            while (true)
            {
                
                Console.Clear();
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Novo lembrete");
                Console.WriteLine("2 - Excluir lembrete");
                string opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Novo lembrete");
                    Console.WriteLine("Campo \"Nome\": ");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Campo \"Data\" (dd/mm/yyyy): ");
                    DateTime data;

                    // Verifica se a data inserida é válida
                    while (!DateTime.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Data inválida. Insira uma data no futuro (dd/mm/yyyy): ");
                    }

                    // Verifica se o nome do lembrete foi inserido
                    if (string.IsNullOrEmpty(nome))
                    {
                        Console.WriteLine("Nome do lembrete inválido.");
                        Console.ReadKey();
                        continue;
                    }

                    // Verifica se a data inserida é no futuro
                    if (data < DateTime.Today)
                    {
                        Console.WriteLine("Data inválida. Insira uma data no futuro (dd/mm/yyyy): ");
                        Console.ReadKey();
                        continue;
                    }

                    // Verifica se já existe um lembrete para a data inserida
                    bool existeLembrete = false;
                        foreach (Lembrete l in listaLembretes)
                    {
                        if (l.Data.Date == data.Date)
                        {
                            Console.WriteLine("Já existe um lembrete para esta data. O novo lembrete será adicionado abaixo do anterior.");
                            listaLembretes.Insert(listaLembretes.IndexOf(l) + 1, new Lembrete(proximoId, nome, data));
                            proximoId++;
                            existeLembrete = true;
                            break;
                        }
                    }

                    if (!existeLembrete)
                    {
                        listaLembretes.Add(new Lembrete(proximoId, nome, data));
                        proximoId++;
                    }
                        using (StreamWriter listaEscrita = new StreamWriter("lista.txt", true))
                        {
                            listaEscrita.WriteLine(proximoId - 1 + " - " + data.ToString("dd/MM/yyyy") + " - " + nome);
                        }
                    
                    // Ordena a lista de lembretes em ordem cronológica
                        listaLembretes.Sort((l1, l2) => l1.Data.CompareTo(l2.Data));
                                        
                    // Exibe a lista de lembretes em ordem cronológica
                        Console.WriteLine("\nLista de lembretes:");
                        foreach (Lembrete l in listaLembretes)
                        {
                            Console.WriteLine(l.Id + " - " + l.Data.ToString("dd/MM/yyyy") + " - " + l.Nome);
                        }


                    Console.ReadKey();        
                }
                else if (opcao == "2")
                {
                    ExcluirLembrete(listaLembretes);
                    Console.Clear();
                    Console.WriteLine("Excluir lembrete");
                    Console.WriteLine("Digite o ID do lembrete que deseja excluir:");

                    // Exibe a lista de lembretes em ordem cronológica
                    Console.WriteLine("\nLista de lembretes:");
                    StreamReader listaLeitura = new StreamReader("lista.txt");
                    while (!listaLeitura.EndOfStream)
                    {
                        Console.WriteLine(listaLeitura.ReadLine());
                    }
                    listaLeitura.Close();

                    Console.ReadKey();
                    
                }
            }
        }
                private static void ExcluirLembrete(List<Lembrete> listaLembretes)
                {
                    Console.Clear();
                    Console.WriteLine("Excluir lembrete");
                    Console.WriteLine("Digite o ID do lembrete que deseja excluir:");

                // Exibe a lista de lembretes em ordem cronológica
                    Console.WriteLine("\nLista de lembretes:");
                    StreamReader listaLeitura = new StreamReader("lista.txt");
                    while (!listaLeitura.EndOfStream)
                    {
                        Console.WriteLine(listaLeitura.ReadLine());
                    }
                    listaLeitura.Close();

                // Lê o ID do lembrete a ser excluído
                    int idExcluir;
                    while (!int.TryParse(Console.ReadLine(), out idExcluir))
                    {
                        Console.WriteLine("ID inválido. Digite novamente:");
                    }

                // Remove o lembrete da lista
                    bool lembreteEncontrado = false;
                    foreach (Lembrete l in listaLembretes)
                    {
                    if (l.Id == idExcluir)
                    {
                        listaLembretes.Remove(l);
                        lembreteEncontrado = true;
                        Console.WriteLine("Lembrete excluído com sucesso!");
                        break;
                    }
                }

                    if (!lembreteEncontrado)
                    {
                        Console.WriteLine("ID não encontrado.");
                    }

                    // Reescreve o arquivo "lista.txt" sem o lembrete excluído
                    StreamWriter listaEscrita = new StreamWriter("lista.txt");
                    foreach (Lembrete l in listaLembretes)
                    {
                        listaEscrita.WriteLine(l.Id + " - " + l.Data.ToString("dd/MM/yyyy") + " - " + l.Nome);
                    }
                    listaEscrita.Close();
        }

    }
}
