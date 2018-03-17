﻿using System;
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
            name.Focus();
        }

        private void btnQr_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                Mysql mc = new Mysql("gcxm", "gcxm", "gcxmgcxm", "39.106.61.96");
                mc.Open();
                mc.Execute($"insert into body values('{name.Text}','{nums.Text}','{price.Text}','xxxxx')");
                mc.Close();
                if (MessageBox.Show("添加成功,是否继续？", "result", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    name.Clear();
                    name.Focus();
                    nums.Clear();
                    price.Clear();
                }
                else
                    Close();

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
    }
}
