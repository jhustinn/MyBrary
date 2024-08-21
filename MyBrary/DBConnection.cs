using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace MyBrary
{
    public class DBConnection
    {

        private string connectionString;
        private MySqlConnection connection;

        public DBConnection()
        {
            connectionString = "server=localhost;user=root;database=desktop_library_db;user=root;password=";
            connection = new MySqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {

                if(connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

            } catch (MySqlException ex) {

                MessageBox.Show("MySql Open Connection Error : " + ex.Message);

            }
        }

        public void CloseConnection()
        {
            try
            {

                if(connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            } catch (MySqlException ex)
            {
                MessageBox.Show("MySql Close Connection Error : " + ex.Message);
            }
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }


    }
}
