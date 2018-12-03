using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterStrikeDB
{
    class DBConnection
    {
        private string server = "asyourtiger.com";
        private string user = "aytgrcom_counterstrike";
        private string password = "counterstrike1$";
        private string port = "3306";
        private string sslM = "none";

        private DBConnection()
        {

        }
        private string databaseName = "test";
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                Console.WriteLine("Connecting");
                if (String.IsNullOrEmpty(databaseName))
                    return false;

                string connstring = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5};", server, port, user, password, databaseName, sslM);
                connection = new MySqlConnection(connstring);
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Connection Failure:" + e);
                    return false;
                }

            }
           
            return true;
        }

        public void Close()
        {
            connection.Close();
        }

    }
}


