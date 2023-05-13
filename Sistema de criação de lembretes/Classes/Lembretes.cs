using System;

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
}
