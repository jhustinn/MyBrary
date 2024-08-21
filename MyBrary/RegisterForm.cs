using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MyBrary
{
    public partial class RegisterForm : Form
    {

        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=desktop_library_db;User ID=root;");


        public RegisterForm()
        {


            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog();
            this.Hide();
        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=desktop_library_db;User ID=root;"))
            {
                conn.Open();

                string name = nameBox.Text.Trim();
                string username = usnBox.Text.Trim();
                string password = pwBox.Text.Trim();
                string confirmPw = pwBox2.Text.Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPw))
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                if (password != confirmPw)
                {
                    MessageBox.Show("Passwords do not match!");
                    return;
                }

                // Check if the username already exists
                string query = "SELECT * FROM user_tb WHERE username = @username";
                using (MySqlCommand cmd2 = new MySqlCommand(query, conn))
                {
                    cmd2.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader rdr = cmd2.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            MessageBox.Show("Username already used! Please use another.");
                            return; // Exit if username already exists
                        }
                    }
                }

                // Insert the new user if the username is not taken
                string insertQuery = "INSERT INTO user_tb (name, username, password, role) VALUES (@name, @username, @password, 'peminjam')";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("User created successfully! Please log in.");

                        this.Hide();
                        LoginForm login = new LoginForm();
                        login.ShowDialog();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("SQL Error: " + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }


        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
