using System;
using System.Data;
using System.Data.SQLite;

namespace BancoDados
{
    public class CRUD : Conexao
    {
        #region Objetos
        SQLiteParameterCollection sQLiteParameterCollection = new SQLiteCommand().Parameters;
        SQLiteCommand sQLiteCommand;
        #endregion

        #region Parametros
        public void LimparParametros()
        {
            sQLiteParameterCollection.Clear();
        }

        public void AdicionarParametros(string nome, object valor)
        {
            sQLiteParameterCollection.Add(new SQLiteParameter(nome, valor));
        }
        #endregion

        #region Manipulação Banco de Dados
        //Insert, Update, Delete, Select
        public object Executar(CommandType commandType, string SQL)
        {
            if (Conectar())
            {
                try
                {
                    sQLiteCommand = sQLiteConnection.CreateCommand();
                    sQLiteCommand.CommandType = commandType;
                    sQLiteCommand.CommandText = SQL;
                    sQLiteCommand.CommandTimeout = 7200;

                    foreach (SQLiteParameter sQLiteParameter in sQLiteParameterCollection)
                    {
                        sQLiteCommand.Parameters.Add(new SQLiteParameter(sQLiteParameter.ParameterName, sQLiteParameter.Value));
                    }
                    return sQLiteCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }
            else
            {
                return null;
            }

        }
        //Conulta para Tabelas
        public DataTable Consultar(CommandType commandType, string SQL)
        {
            if (Conectar())
            {
                try
                {
                    sQLiteCommand = sQLiteConnection.CreateCommand();
                    sQLiteCommand.CommandType = commandType;
                    sQLiteCommand.CommandText = SQL;
                    sQLiteCommand.CommandTimeout = 7200;

                    foreach (SQLiteParameter sQLiteParameter in sQLiteParameterCollection)
                    {
                        sQLiteCommand.Parameters.Add(new SQLiteParameter(sQLiteParameter.ParameterName, sQLiteParameter.Value));
                    }
                    SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(sQLiteCommand);
                    DataTable dataTable = new DataTable();
                    sQLiteDataAdapter.Fill(dataTable);
                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }
            else
            {
                return null;
            }

        }
        #endregion
    }
}
