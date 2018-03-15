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
        public LoginWindow()
        {
            InitializeComponent();
            user.Focus();
            btnLog.IsEnabled = false;
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            Mysql mc = new Mysql("work_319", "zhq", "zhqssb", "39.106.61.96");
            mc.Open();
            DataSet da = mc.Select($"select pwd from user where name = '{user.Text}'");
            mc.Close();
            if (da.Tables[0].Rows.Count == 0)
                MessageBox.Show("账户不存在");
            else 
            
            if (da.Tables[0].Rows[0][0].ToString() != pwd.Password)
                MessageBox.Show("密码错误");
            else
                DialogResult = true;
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
