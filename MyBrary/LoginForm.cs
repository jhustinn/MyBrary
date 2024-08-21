using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MyBrary
{
    public partial class LoginForm : Form
    {

        private DBConnection dBConnection;

        public LoginForm()
        {
            InitializeComponent();

            dBConnection = new DBConnection();

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void signInBtn_Click(object sender, EventArgs e)
        {

            string username = usnBox.Text;
            string password= pwBox.Text;


            try
            {

                dBConnection.OpenConnection();
                MySqlConnection conn = dBConnection.GetConnection();

                string query = "SELECT * FROM user_tb WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                if(rdr.HasRows)
                {

                    if (rdr["password"].ToString() == password)
                    {

                        if (rdr["role"].ToString() == "admin" || rdr["role"].ToString() == "petugas")
                        {
                            AdminDashboard adminDashboard = new AdminDashboard(rdr["role"].ToString(), Convert.ToInt16(rdr["id"]));
                            adminDashboard.Show();
                            this.Hide();
                        }
                        else if (rdr["role"].ToString() == "peminjam")
                        {
                            Dashboard dashboard = new Dashboard(true, rdr["role"].ToString(), Convert.ToUInt16(rdr["id"]));
                            dashboard.Show();
                            this.Hide();
                        }

                        
                    } else
                    {
                        MessageBox.Show("Wrong Password!");

                    }

                } else
                {
                    MessageBox.Show("User Not Found!");
                }

                rdr.Close();


            } catch (Exception ex) {
                MessageBox.Show("MySql Open Connection Error : " + ex.Message);
            } finally
            {
                dBConnection.CloseConnection();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.ShowDialog();
            this.Hide();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard(false, "", 0);
            dashboard.Show();
            this.Hide();
        }
    }
}
