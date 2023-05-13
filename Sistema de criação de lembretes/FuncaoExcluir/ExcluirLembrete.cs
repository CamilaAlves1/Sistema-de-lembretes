using System;

namespace SistemaLembretes
{
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