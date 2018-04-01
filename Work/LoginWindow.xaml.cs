using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Work
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string UserName { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
            user.Focus();
            btnLog.IsEnabled = false;
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            Mysql mc = new Mysql();

            mc.Open();
            DataSet da = mc.Select($"select * from user where name = '{user.Text}' and pwd =MD5('{pwd.Password}')");
            mc.Close();

            if (da.Tables[0].Rows.Count == 0)
                MessageBox.Show($"{TryFindResource("Message2")}");
            else
            {
                UserName = user.Text;
                DialogResult = true;
            }
        }
        
        private void user_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (user.Text != "" && pwd.Password != "")
                btnLog.IsEnabled = true;
        }

        private void pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (user.Text != "" && pwd.Password != "")
                btnLog.IsEnabled = true;
        }

        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
