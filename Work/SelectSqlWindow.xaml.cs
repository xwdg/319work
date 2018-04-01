using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

namespace Work
{
    /// <summary>
    /// SelectSqlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectSqlWindow : Window
    {
        public SelectSqlWindow()
        {
            InitializeComponent();

            GetAppCon gac = new GetAppCon();
            DBName.Text = gac.ReadSetting("DBName");
            DBUser.Text = gac.ReadSetting("DBUser");
            DBUrl.Text = gac.ReadSetting("DBUrl");
            DBPwd.Text = gac.ReadSetting("DBPwd");
            DBPort.Text = gac.ReadSetting("DBPort");
        }
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mysql ms = new Mysql(DBUrl.Text, DBName.Text, DBUser.Text, DBPwd.Text, DBPort.Text);
                ms.Open();
                MessageBox.Show("success");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btnEnsure_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;

            try
            {
                Mysql ms = new Mysql(DBUrl.Text, DBName.Text, DBUser.Text, DBPwd.Text, DBPort.Text);
                ms.Open();
                ms.Close();
                flag = true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            if (flag == true)
            {
                GetAppCon gac = new GetAppCon();

                gac.AddUpdateAppSettings("DBName", DBName.Text);
                gac.AddUpdateAppSettings("DBUser", DBUser.Text);
                gac.AddUpdateAppSettings("DBUrl", DBUrl.Text);
                gac.AddUpdateAppSettings("DBPwd", DBPwd.Text);
                gac.AddUpdateAppSettings("DBPort", DBPort.Text);
                try
                {
                    FileInfo file = new FileInfo(@"./sql/gcxm.sql");  //filename是sql脚本文件路径。
                    string sql = file.OpenText().ReadToEnd();

                    Mysql ms = new Mysql();
                    ms.Open();
                    ms.Begin();

                    try
                    {
                        ms.ExecScript(sql);
                    }

                    catch(Exception error3)
                    {
                        ms.Rollback();
                        MessageBox.Show(error3.Message);
                    }

                    ms.Commit();
                    ms.Close();
                    
                    MainWindow mw = new MainWindow();
                    Close();
                    mw.ShowDialog();
                }
                catch (Exception error2)
                {
                    MessageBox.Show(error2.Message);
                }
            }
        }
    }
}

