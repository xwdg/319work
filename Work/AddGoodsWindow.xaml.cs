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
    /// AddGoodsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddGoodsWindow : Window
    {
        public AddGoodsWindow()
        {
            InitializeComponent();
            btnQr.IsEnabled = false;
        }

        private void btnQr_Click(object sender, RoutedEventArgs e)
        {
            //Mysql ms = new Mysql("work_319", "zhq", "z", "39.106.61.96");
            //ms.add
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
