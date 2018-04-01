using System;
using System.Collections.Generic;
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
    /// AlterGoodWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class AlterGoodWindow : Window
    {
        private int Ggid { get; set; }
        private string UserName { get; set; }

        public AlterGoodWindow(int _id = 0, string _name = "",double _price = 0.0,int _nums=0,string _pname="")
        {
            InitializeComponent();

            name.Text = _name;
            price.Text = _price.ToString("0.00");
            nums.Text = _nums.ToString();
            Ggid = _id;
            UserName = _pname;

            name.Focus();
        }

        private void btnQr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mysql mc = new Mysql();
                mc.Open();
                mc.Execute($"update cskcgl set nums='{nums.Text}',price='{price.Text}',spmc='{name.Text}' where id = '{Ggid}'");
                mc.Execute($"insert into record values(null,'{UserName}','update',now())");
                mc.Close();

                MessageBox.Show($"{TryFindResource("Message3")}", "result");
                Close();
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
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
