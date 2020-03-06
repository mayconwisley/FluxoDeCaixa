using BancoDados;
using Modelo;
using System;
using System.Data;

namespace Controle
{
    public class MovimentacaoControle
    {
        CRUD crud;
        string SQL;

        public bool Gravar(Movimentacao movimentacao)
        {
            crud = new CRUD();
            SQL = "INSERT INTO Movimento (Descricao, Data_Movimentacao, Valor, Categoria_Id) " +
                  "VALUES(@Descricao, @Data_Movimentacao, @Valor, @Categoria_Id)";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Descricao", movimentacao.Descricao);
                crud.AdicionarParametros("Data_Movimentacao", movimentacao.Data_Movimentacao);
                crud.AdicionarParametros("Valor", movimentacao.Valor);
                crud.AdicionarParametros("Categoria_Id", movimentacao.Categoria.Id);
                crud.Executar(CommandType.Text, SQL);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Alterar(Movimentacao movimentacao)
        {
            crud = new CRUD();
            SQL = "UPDATE Movimento " +
                  "SET Descricao = @Descricao, Data_Movimentacao = @Data_Movimentacao, Valor = @Valor, Categoria_Id = Categoria_Id " +
                  "WHERE Id = @Id ";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Descricao", movimentacao.Descricao);
                crud.AdicionarParametros("Data_Movimentacao", movimentacao.Data_Movimentacao);
                crud.AdicionarParametros("Valor", movimentacao.Valor);
                crud.AdicionarParametros("Categoria_Id", movimentacao.Categoria.Id);
                crud.AdicionarParametros("Id", movimentacao.Id);
                crud.Executar(CommandType.Text, SQL);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Excluir(Movimentacao movimentacao)
        {
            crud = new CRUD();
            SQL = "DELETE FROM Movimento " +
                  "WHERE Id = @Id";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Id", movimentacao.Id);
                crud.Executar(CommandType.Text, SQL);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal TotalEntrada()
        {
            crud = new CRUD();
            SQL = "SELECT SUM(Valor) " +
                  "FROM Movimento " +
                  "INNER JOIN Categoria ON Movimento.Categoria_Id = Categoria.Id " +
                  "WHERE Categoria.Tipo1 = 'Entrada'";
            try
            {
                return decimal.Parse(crud.Executar(CommandType.Text, SQL).ToString());
            }
            catch
            {
                return 0;
            }

        }
        public decimal TotalSaida()
        {
            crud = new CRUD();
            SQL = "SELECT SUM(Valor) " +
                  "FROM Movimento " +
                  "INNER JOIN Categoria ON Movimento.Categoria_Id = Categoria.Id " +
                  "WHERE Categoria.Tipo1 = 'Saida'";
            try
            {
                return decimal.Parse(crud.Executar(CommandType.Text, SQL).ToString());
            }
            catch
            {
                return 0;
            }

        }


        public DataTable Consulta()
        {
            crud = new CRUD();
            SQL = "SELECT Movimento.Id, Movimento.Descricao AS Movimento, Movimento.Data_Movimentacao, Movimento.Valor, Movimento.Categoria_Id, Categoria.Descricao As Categoria, Categoria.Tipo1, Categoria.Tipo2 " +
                  "FROM Movimento " +
                  "INNER JOIN Categoria ON Movimento.Categoria_Id = Categoria.Id";
            try
            {
                DataTable dataTable = crud.Consultar(CommandType.Text, SQL);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
