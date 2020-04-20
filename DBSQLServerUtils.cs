using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace WebApi
{
    public static class DBSQLServerUtils
    {

        public static string ValidateDefaultInstancePostgreSqlServer(string found)
        {
            string expt = "Close";
            try
            {
                using NpgsqlConnection postGresConnection = new NpgsqlConnection
                {
                    ConnectionString = "Server=localhost;Port=5432;Database=AtmBase;User Id=postgres;Password=g7712316;Pooling=true;MinPoolSize=1;MaxPoolSize=100;Command Timeout=600;Timeout=600;"
                };
                using (NpgsqlCommand checkDBCommand = postGresConnection.CreateCommand())
                {
                    postGresConnection.Open();
                    Console.WriteLine(postGresConnection.State);
                    found = postGresConnection.State.ToString();
                    found.ToString();
                }
                postGresConnection.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                expt = ex.ToString();
            }
            if (found.ToString().StartsWith("Open"))
            {
                expt = found.ToString();
            }
            else
            {
                return "IsConnect :" + expt.ToString() + "\n";
            }

            return "IsConnect :" + expt.ToString() + "\n";
        }

        public static string Connection(string host)
        {
            object ip = "";
            DataTable Returndata;
            Console.WriteLine("Connection for select was successful!" + "'" + $"{host}" + "'");
            string q = "SELECT ip FROM inventory where compassname =" + "'" + $"{host}" + "'";
            string connection = "Server=172.16.0.162;Port=5432;Database=AtmBase;User Id=postgres;Password=g7712316;Pooling=true;MinPoolSize=1;MaxPoolSize=100;Command Timeout=600;Timeout=600;";
            NpgsqlConnection conn = new NpgsqlConnection(connection);
            Returndata = DataTablesReturn(q, conn);
            for (int i = 0; i < Returndata.Rows.Count; i++)
            {
                ip = Returndata.Rows[i].ItemArray[0];
                Console.WriteLine(ip.ToString());
            }
            Returndata.Dispose();
            conn.Dispose();
            if (ip.ToString().StartsWith("172")) { return ip.ToString(); }
            //if (ip.ToString().StartsWith("10")) { return ip.ToString(); }
            else
            {
                return "Not Found ATM IP";
            }
            //return ip.ToString();
        }

        public static string Connection2(string host)
        {
            JObject obj2 = new JObject();
            object ip = "";
            object peer = "";
            object compassname = null;
            object peers = null;
            //var input2 = "";
            DataTable Returndata;
            Console.WriteLine("Connection for select was successful!" + "'" + $"{host}" + "'");
            string q = "SELECT ip, peer, compassname FROM inventory where compassname =" + "'" + $"{host}" + "'";
            string connection = "Server=localhost;Port=5432;Database=AtmBase;User Id=postgres;Password=g7712316;Pooling=true;MinPoolSize=1;MaxPoolSize=100;Command Timeout=600;Timeout=600;";
            NpgsqlConnection conn = new NpgsqlConnection(connection);
            Returndata = DataTablesReturn(q, conn);
            for (int i = 0; i < Returndata.Rows.Count; i++)
            {
                ip = Returndata.Rows[i].ItemArray[0];
                peer = Returndata.Rows[i].ItemArray[1];
                compassname = Returndata.Rows[i].ItemArray[2];
                Console.WriteLine(peer);
                obj2[$"{i}"] = $"{i}";
                obj2[$"ip"] = $"{ip}";
                obj2[$"peer"] = $"{peer}";
                obj2[$"compassname"] = $"{compassname}";
            }
            Returndata.Dispose();
            conn.Dispose();
            if (ip != null) {
                //object[,] input2 = { { ip.ToString(), peer.ToString() } };
                //Console.WriteLine(obj2);
                return obj2.ToString();
              }
            //if (ip.ToString().StartsWith("10")) { return ip.ToString(); }
            else
            {
                //return "Not Found ATM IP";
                return obj2.ToString();
            }
            //return ip.ToString();
        }

        public static DataTable DataTablesReturn(string query, NpgsqlConnection conn)
        {
            DataTable dates = new DataTable();
            dates.Clear();
            conn.Open();
            NpgsqlCommand myCommand = new NpgsqlCommand(query, conn);
            if (query.TrimStart().ToUpper().StartsWith("SELECT"))
            {
                NpgsqlDataReader DataRead = myCommand.ExecuteReader();
                dates.Load(DataRead);
                myCommand.Dispose();
            }
            else
            {
                myCommand.ExecuteNonQuery();
            }
            conn.Dispose();
            //Console.WriteLine($"{dates.Rows[1].ItemArray[1]}, {dates.Rows[1].ItemArray[2]}");
            return dates;
        }


        public static int Update_fromConnection(NpgsqlConnection connection, int rows, string password, string ldap, string Result_RSA, string connString)
        {
            NpgsqlConnectionStringBuilder sConnB = new NpgsqlConnectionStringBuilder(connString);
            NpgsqlConnection conn = new NpgsqlConnection(sConnB.ConnectionString);

            if (conn.State == ConnectionState.Closed)
            {
                Console.WriteLine("Connection status Closed: " + conn.State);
                try
                {
                    connection = new NpgsqlConnection(connString);
                    connection.Open();
                    Console.WriteLine(connection.State);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }

            }
            else
            {
                Console.WriteLine($"Connection for update was successful!" + connection.State);
            }

            if (connection.State == ConnectionState.Open)
            {
                string q = "update atm_user set pwd_encrypt = @rsa where ldap like @cnm";
                NpgsqlCommand cmd = new NpgsqlCommand(q, connection);
                cmd.Parameters.AddWithValue("@cnm", ldap);
                //cmd.Parameters.AddWithValue("@ln", p_enc);
                //cmd.Parameters.AddWithValue("@em", pass);
                cmd.Parameters.AddWithValue("@rsa", Result_RSA);
                try
                {
                    rows = cmd.ExecuteNonQuery();
                    rows += rows;
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                cmd.Dispose();
            }
            return rows;
        }
    }
}


