using System;

namespace Modelo
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_Movimentacao { get; set; }
        public decimal Valor { get; set; }
        public Categoria Categoria { get; set; }
    }
}
