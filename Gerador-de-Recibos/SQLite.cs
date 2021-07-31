using Finisar.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_de_Recibos
{
    class SQLite
    {
        SQLiteConnection sqlite_con;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;

        int count = 0;

        public void CreateData()
        {
            try
            {

                if (File.Exists("data.db"))
                {
                    //pass
                }
                else
                {
                    //create a new database connection:
                    sqlite_con = new SQLiteConnection("Data Source=data.db;Version=3;New=True;Compress=True");

                    //open the connection:
                    sqlite_con.Open();

                    //create a new SQL command:
                    sqlite_cmd = sqlite_con.CreateCommand();

                    //Let the SQLiteCommand object know our SQL-Query:
                    sqlite_cmd.CommandText = "CREATE TABLE recibo (" +
                                                "id integer primary key," +
                                                "cliente varchar(100)," +
                                                "cpf_cnpj varchar(100)," +
                                                "valor varchar(100)," +
                                                "descricao varchar(100)," +
                                                "tipo boolean);";

                    //Now lets execute the SQL ;D
                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_con.Close();
                }            
            }
            catch(Exception error){
                throw error;
            }
        }

        public void persistData(int id,string cliente,string cpf_cnpj,string valor,string descricao,int tipo)
        {
            try
            {
                sqlite_con = new SQLiteConnection("Data Source=data.db;Version=3");
                sqlite_con.Open();
                sqlite_cmd = sqlite_con.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO recibo (id,cliente,cpf_cnpj,valor,descricao,tipo) values("+id+",'"+cliente+"','"+cpf_cnpj+"','"+valor+"','"+descricao+"',"+tipo+")";
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                sqlite_con.Close();
            }
        }

        public int automaticId()
        {
            try
            {
                count = Convert.ToInt32(File.ReadLines(@".counter").Skip(0).Take(1).First());
                StreamWriter sw = new StreamWriter(@".counter");
                sw.WriteLine(count + 1);
                sw.Close();

            }
            catch (Exception error)
            {
                throw error;
            }

            return count+1;
        }

        public DataTable list()
        {
            sqlite_con = new SQLiteConnection("Data Source=data.db;Version=3");
            sqlite_con.Open();
            sqlite_cmd = sqlite_con.CreateCommand();
            sqlite_cmd.CommandText = "select * from recibo";
            SQLiteDataAdapter da = new SQLiteDataAdapter();
            da.SelectCommand = sqlite_cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable busca(string content)
        {
            sqlite_con = new SQLiteConnection("Data Source=data.db;Version=3");
            sqlite_con.Open();
            sqlite_cmd = sqlite_con.CreateCommand();

            int value;

            if(int.TryParse(content,out value))
                sqlite_cmd.CommandText = "select * from recibo where id = "+value;
            else
                sqlite_cmd.CommandText = "select * from recibo where cliente like '%" + content + "%'";


            SQLiteDataAdapter da = new SQLiteDataAdapter();
            da.SelectCommand = sqlite_cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public void delete(int recibo)
        {
            try
            {
                sqlite_con = new SQLiteConnection("Data Source=data.db;Version=3");
                sqlite_con.Open();
                sqlite_cmd = sqlite_con.CreateCommand();
                sqlite_cmd.CommandText = "delete from recibo where id = " +recibo;
                sqlite_cmd.ExecuteNonQuery();

            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                sqlite_con.Close();
            }
        }

    }
}
