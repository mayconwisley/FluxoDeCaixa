using BancoDados;
using Modelo;
using System;
using System.Data;
namespace Controle
{
    public class CategoriaControle
    {
        CRUD crud;
        string SQL;
        public bool Gravar(Categoria categoria)
        {
            crud = new CRUD();
            SQL = "INSERT INTO Categoria (Descricao, Tipo1, Tipo2) " +
                  "VALUES(@Descricao, @Tipo1, @Tipo2)";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Descricao", categoria.Descricao);
                crud.AdicionarParametros("Tipo1", categoria.Tipo1);
                crud.AdicionarParametros("Tipo2", categoria.Tipo2);
                crud.Executar(CommandType.Text, SQL);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Alterar(Categoria categoria)
        {
            crud = new CRUD();
            SQL = "UPDATE Categoria " +
                  "SET Descricao = @Descricao, Tipo1 = @Tipo1, Tipo2 = @Tipo2 " +
                  "WHERE Id = @Id ";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Descricao", categoria.Descricao);
                crud.AdicionarParametros("Tipo1", categoria.Tipo1);
                crud.AdicionarParametros("Tipo2", categoria.Tipo2);
                crud.AdicionarParametros("Id", categoria.Id);
                crud.Executar(CommandType.Text, SQL);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Excluir(Categoria categoria)
        {
            crud = new CRUD();
            SQL = "DELETE FROM Categoria " +
                  "WHERE Id = @Id";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Id", categoria.Id);
                crud.Executar(CommandType.Text, SQL);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int Id(Categoria categoria)
        {
            crud = new CRUD();
            SQL = "SELECT Id " +
                  "FROM Categoria " +
                  "WHERE Descricao = @Descricao ";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Descricao", categoria.Descricao);
                return int.Parse(crud.Executar(CommandType.Text, SQL).ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Entrada ou Saida
        public string Tipo1(Categoria categoria)
        {
            crud = new CRUD();
            SQL = "SELECT Tipo1 " +
                  "FROM Categoria " +
                  "WHERE Descricao = @Descricao ";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Descricao", categoria.Descricao);
                return crud.Executar(CommandType.Text, SQL).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Fixo ou Variavel
        public string Tipo2(Categoria categoria)
        {
            crud = new CRUD();
            SQL = "SELECT Tipo2 " +
                  "FROM Categoria " +
                  "WHERE Descricao = @Descricao ";
            try
            {
                crud.LimparParametros();
                crud.AdicionarParametros("Descricao", categoria.Descricao);
                return crud.Executar(CommandType.Text, SQL).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable Consulta()
        {
            crud = new CRUD();
            SQL = "SELECT Id, Descricao, Tipo1, Tipo2 " +
                  "FROM Categoria " +
                  "ORDER BY Descricao ";
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
