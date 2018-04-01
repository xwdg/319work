using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// AddGoodsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddGoodsWindow : Window
    {
        private string UserName { get; set; }

        public AddGoodsWindow(string _pname)
        {
            InitializeComponent();

            btnQr.IsEnabled = false;
            name.Focus();
            dgTable_Lod();

            UserName = _pname;
        }

        public void dgTable_Lod()
        {
            try
            {
                Mysql mc = new Mysql();
                mc.Open();
                DataSet da = mc.Select($"select * from cskcgl");
                mc.Close();

                dgTable.ItemsSource = da.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnQr_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                Mysql mc = new Mysql();

                mc.Open();
                mc.Begin();
                try
                {
                    mc.Execute($"insert into cskcgl values(null,'{name.Text}','{price.Text}','{nums.Text}')");
                    mc.Execute($"insert into record values(null,'{UserName}','insert',now())");
                    mc.Commit();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    mc.Rollback();
                }

                mc.Close();
                dgTable_Lod();
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message, "error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btnQx_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (name.Text != "" && nums.Text != "" && price.Text != "")
                btnQr.IsEnabled = true;
            else
                btnQr.IsEnabled = false;
        }

        private void nums_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[0-9]");
            e.Handled = !re.IsMatch(e.Text);
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("^[0-9]+(\\u002e[0-9]{0,2})?$");
            e.Handled = !re.IsMatch(price.Text + e.Text);
        }
    }
}
