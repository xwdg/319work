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

namespace Work
{
    /// <summary>
    /// AlterGoodWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class AlterGoodWindow : Window
    {
        private string ggid { get; set; }
        private string gname { get; set; }
        private double gprice { get; set; }
        private int gid { get; set; }

        public AlterGoodWindow(string _id = "", string _name = "",double _price = 0.0,int _nums=0)
        {
            InitializeComponent();

            name.Text = _name;
            price.Text = _price.ToString();
            nums.Text = _nums.ToString();
            ggid = _id;

            name.Focus();
        }

        private void btnQr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mysql mc = new Mysql("gcxm", "gcxm", "gcxmgcxm", "39.106.61.96");
                mc.Open();
                mc.Execute($"update body set height='{nums.Text}',weight='{price.Text}',cm='{name.Text}' where id = '{ggid}'");
                mc.Close();
                MessageBox.Show("修改成功", "result");
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
    }
}
