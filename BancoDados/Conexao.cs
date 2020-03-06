using System.Data;
using System.Data.SQLite;

namespace BancoDados
{
    public class Conexao
    {
        private static string caminho = @"Data Source = |DataDirectory|\Banco Dados\Movimento.db; Version=3; Password=123456";
        protected SQLiteConnection sQLiteConnection;

        protected bool Conectar()
        {
            sQLiteConnection = new SQLiteConnection(caminho);
            try
            {
                sQLiteConnection.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                throw new SQLiteException(ex.Message);
            }
        }
        protected bool Desconectar()
        {
            try
            {
                if (sQLiteConnection.State != ConnectionState.Closed)
                {
                    sQLiteConnection.Close();
                    sQLiteConnection.Dispose();
                    return true;
                }
                else
                {
                    sQLiteConnection.Dispose();
                    return false;
                }
            }
            catch (SQLiteException ex)
            {
                throw new SQLiteException(ex.Message);
            }
        }
    }
}
