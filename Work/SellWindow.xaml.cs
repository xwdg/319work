using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// SellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SellWindow : Window
    {
        struct Spsm
        {
            public string spmc { get; set; }
            public int id { get; set; }
            public int nums { get; set; }
            public string sprice { get; set; }
            public string wprice { get; set; }
        }
        
        List<Spsm> BuydedGoods;  //保存当前选中商品的列表
        private static string path = ConfigurationManager.AppSettings["excelPath"];

        public SellWindow()
        {
            InitializeComponent();
            BuydedGoods = new List<Spsm>();

            Mysql mc = new Mysql();
            mc.Open();
            DataSet da = mc.Select("select id from cskcgl");
            mc.Close();

            myCombobox.ItemsSource = da.Tables[0].DefaultView;         //将combobox的值设置为商品id
            myCombobox.SelectedIndex = 0;
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = myCombobox.SelectedItem as DataRowView;

            Mysql mc = new Mysql();
            mc.Open();
            DataSet da = mc.Select($"select spmc,price from cskcgl where id = '{drv["id"]}'");
            mc.Close();

            if (da.Tables[0].Rows.Count == 0)
                MessageBox.Show("商品不存在");

            else
            {
                
                Spsm temp = new Spsm();

                temp.sprice = da.Tables[0].Rows[0]["price"].ToString();
                temp.spmc = da.Tables[0].Rows[0]["spmc"].ToString();
                temp.nums = Convert.ToInt32(nums.Text);
                temp.id = Convert.ToInt32(drv["id"]);
                temp.wprice = (Convert.ToSingle(temp.sprice) * temp.nums).ToString();

                tbPrice.Text = (Convert.ToSingle(tbPrice.Text) + Convert.ToSingle(temp.wprice)).ToString();

                BuydedGoods.Add(temp);
                dgTable.ItemsSource = null;
                dgTable.ItemsSource = BuydedGoods;
                //crGoods.Text += string.Format("编号:{0,-4}价格:￥{2,-6}商品名称:{1,-9}\n", drv["id"], da.Tables[0].Rows[0]["spmc"], da.Tables[0].Rows[0]["price"]);
            }

            nums.Text = "1";
            myCombobox.SelectedIndex = 0;
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (BuydedGoods.Count == 0)
                return;

            if (prices.Text == "")
            { 
                prices.Focus();
                return;
            }

            try
            {
                Mysql ms = new Mysql();
                ms.Open();
                ms.Begin();

                ExcelReport er = new ExcelReport();
                er.Open();

                string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                string timeSt ="LSH"+Convert.ToInt64(ts.TotalSeconds).ToString();

                int  count = 1;

                try
                {
                    foreach (Spsm i in BuydedGoods)
                    {
                        ms.Execute($"update cskcgl set nums = nums-{i.nums} where id = {i.id}");
                        ms.Execute($"insert into xsgl values('{dt}','{i.spmc}','{i.wprice}','{i.sprice}','{timeSt}',{i.nums})");

                        
                        //er.SetCellText(count, 1, i.spmc);
                        //er.SetCellText(count, 2, i.sprice);
                        //er.SetCellText(count, 3, i.nums.ToString());
                        //er.SetCellText(count, 4, i.wprice);
                        //count++;
                    }
                    ms.Execute($"insert into jyjl values(null,'{timeSt}','{dt}','{tbPrice.Text}','{prices.Text}',{tbCharge.Text})");
                    //er.SetCellText(count, 1, TryFindResource("date").ToString());
                    //er.MergeCells(er.GetRange(count, 1, count, 2));
                    //er.SetCellText(count, 3, dt);
                    //er.MergeCells(er.GetRange(count, 3, count, 4));

                    //er.SetCellText(++count, 1, TryFindResource("serialNumber").ToString());
                    //er.MergeCells(er.GetRange(count, 1, count, 2));
                    //er.SetCellText(count, 3, timeSt);
                    //er.MergeCells(er.GetRange(count, 3, count, 4));

                    //er.SetCellText(++count, 1, TryFindResource("Price").ToString());
                    //er.SetCellText(count, 2, TryFindResource("Money").ToString());
                    //er.SetCellText(count, 3, tbPrice.Text);
                    //er.MergeCells(er.GetRange(count, 3, count, 4));

                    //er.SetCellText(++count, 1, TryFindResource("Pay").ToString());
                    //er.SetCellText(count, 2, TryFindResource("Money").ToString());
                    //er.SetCellText(count, 3, prices.Text);
                    //er.MergeCells(er.GetRange(count, 3, count, 4));

                    //er.SetCellText(++count, 1, TryFindResource("Charge").ToString());
                    //er.SetCellText(count, 2, TryFindResource("Money").ToString());
                    //er.SetCellText(count, 3, tbCharge.Text);
                    //er.MergeCells(er.GetRange(count, 3, count, 4));
                    ////er.MergeCells(er.GetRange(1, 1, count - 1, 2));
                    ////er.MergeCells(er.GetRange(1, 5, count - 1, 6));
                    //er.SaveAs($"{path}{timeSt}.xlsx", false);
                    ms.Commit();
                }

                catch (Exception er1)
                {
                    MessageBox.Show(er1.Message);
                    ms.Rollback();
                }

                er.Close();
                ms.Close();
            }
            catch (Exception er2)
            {
                MessageBox.Show(er2.Message);
            }

            tbPrice.Text = "0.0";
            nums.Text = "1";
            prices.Text = "";
            //crGoods.Text = "";
            dgTable.ItemsSource = null;
            BuydedGoods.Clear();
        }

        private void btnQx_Click(object sender, RoutedEventArgs e)
        {
            tbPrice.Text = "0.0";
            //crGoods.Text = "";

            //id.Clear();
            //id.Focus();

            dgTable.ItemsSource = null;
            prices.Text = "";
            nums.Text = "1";
            BuydedGoods.Clear();
        }

        private void prices_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (prices.Text == "")
            {
                tbCharge.Text = "0.0";
                return;
            }

            if (Convert.ToSingle(prices.Text) > Convert.ToSingle(tbPrice.Text))
                tbCharge.Text = (Convert.ToSingle(prices.Text) - Convert.ToSingle((Convert.ToSingle(tbPrice.Text)).ToString("0.00"))).ToString();
            else
                tbCharge.Text = "0.0";
        }

        private void prices_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           
                Regex re = new Regex("^[0-9]+(\\u002e[0-9]{0,2})?$");
                e.Handled = !re.IsMatch(prices.Text + e.Text);
        }

        private void nums_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nums.Text == "")
                btnAdd.IsEnabled = false;
            else
                btnAdd.IsEnabled = true;
        }

        private void nums_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[0-9]");
            e.Handled = !re.IsMatch(e.Text);
        }
    }
}
