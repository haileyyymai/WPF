using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Entity.Core.Common;
using System.Windows.Markup;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public void ClearAllClick(object sender, RoutedEventArgs e)
        {
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            emailTextBox.Text = "";
            passwordTextBox.Clear();
        }

        public void LoginClick(object sender, RoutedEventArgs e)
        {



            try
            {
                using (var conn = new SQLiteConnection("Data Source = mydatabase.db"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT firstname, lastname, email, password FROM USER_TABLE WHERE firstname = '" + firstNameTextBox.Text + "' AND lastname = '" + lastNameTextBox.Text + "' AND email = '" + emailTextBox.Text + "' AND password = '" + passwordTextBox.Password.ToString() + "'", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var count = 0;
                            while (reader.Read())
                            {
                                count = count + 1;
                            }
                            if (count == 1)
                            {
                                GrantAccess();
                            }
                            else if (count == 0)
                            {
                                MessageBox.Show("User Not Found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("User Not Found.");
            }

        }

        public void RegisterClick(object sender, RoutedEventArgs e)
        {
            
            SQLiteConnection sqliteConn = new SQLiteConnection("Data Source = mydatabase.db");

            

            try
            {
                sqliteConn.Open();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);

            }

            string sql = "create table if not exists USER_TABLE (firstname varchar(20), lastname varchar(20), email varchar(50), password varchar(20))";
            SQLiteCommand command = new SQLiteCommand(sql, sqliteConn);
            command.ExecuteNonQuery();

            string insertSQL = "INSERT INTO USER_TABLE (firstname, lastname, email, password) VALUES ('" + firstNameTextBox.Text + "', '" + lastNameTextBox.Text + "', '" + emailTextBox.Text + "', '" + passwordTextBox.Password.ToString() + "');";
            SQLiteCommand insertComm = new SQLiteCommand(insertSQL, sqliteConn);
            insertComm.ExecuteNonQuery();
            sqliteConn.Close();


            GrantAccessRegister();


        }


        public void GrantAccess()
        {
            Window1 main = new Window1();
            main.Show();


        }

        public void GrantAccessRegister()
        {
            Window2 main = new Window2();
            main.Show();


        }
    }


    
    
}
