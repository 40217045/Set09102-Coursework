using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace _40217045_CW1
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string User = "New User";
        List<User> User_List = new List<User>();

        public Login()
        {
            InitializeComponent();
            LoadUsers();
            
        }

        private void LoadUsers()
        {
            try
            {
                string FileLocSms = @"Resources\Users.json";
                string json = File.ReadAllText(FileLocSms);
                User_List = JsonConvert.DeserializeObject<List<User>>(json);
            }
            catch { }

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User = txtUser.Text;
            MainWindow NewWindow = new MainWindow(User);
            NewWindow.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            cvsNewUser.Visibility = Visibility.Visible;
        }

       

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtTwitter.Clear();

            txtUsername.Clear();
            
            cvsNewUser.Visibility = Visibility.Hidden;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            User u = new User();
            double phone=0;
            try
            {
                phone = double.Parse(txtPhone.Text);
            }
            catch { }
            try
            {
                u.Username = txtUsername.Text;
                u.Name = txtName.Text;
                u.EmailAdd = txtEmail.Text;
                u.Twitter = txtTwitter.Text;
                u.Phonenumber = phone;
                User_List.Add(u);
                SaveUser();
                MessageBox.Show("User " + txtUser.Text + " added");

                txtEmail.Clear();
                txtName.Clear();
                txtPhone.Clear();
                txtTwitter.Clear();
                txtUsername.Clear();
                cvsNewUser.Visibility = Visibility.Hidden;
            }
            catch(Exception UserError)
            {
                MessageBox.Show(UserError.ToString());
            }
           

        }

        private void SaveUser()
        {
            string FileLoc = @"Resources\Users.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(User_List));
            Console.WriteLine("All data saved to " + FileLoc);
        }
    }
}
