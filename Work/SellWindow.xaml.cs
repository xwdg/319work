using System;
using System.Collections.Generic;
using System.Data;
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
    /// SellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SellWindow : Window
    {
        struct Spsm
        {
            public string spmc { get; set; }
            public int id { get; set; }
            public double price { get; set; }
        }

        public SellWindow()
        {
            InitializeComponent();
            
            BuydedGoods = new List<Spsm>();
            Mysql mc = new Mysql("gcxm", "gcxm", "gcxmgcxm", "39.106.61.96");
            mc.Open();
            DataSet da = mc.Select("select id from cskcgl");
            mc.Close();
            myCombobox.ItemsSource = da.Tables[0].DefaultView;
            myCombobox.SelectedIndex = 0;
        }

        List<Spsm> BuydedGoods;
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = myCombobox.SelectedItem as DataRowView;

            Mysql mc = new Mysql("gcxm ", "gcxm", "gcxmgcxm", "39.106.61.96");
            mc.Open();
            DataSet da = mc.Select($"select spmc,price from cskcgl where id = '{drv["id"]}'");
            mc.Close();

            if (da.Tables[0].Rows.Count == 0)
                MessageBox.Show("商品不存在");
            else
            {
                tbPrice.Text = (Convert.ToSingle(tbPrice.Text) + (Single)da.Tables[0].Rows[0]["price"]).ToString();

                Spsm temp = new Spsm();

                temp.price = Convert.ToSingle(((float)da.Tables[0].Rows[0]["price"]).ToString("0.00"));
                temp.spmc = da.Tables[0].Rows[0]["spmc"].ToString();
                temp.id = Convert.ToInt32(drv["id"]);
                BuydedGoods.Add(temp);

                crGoods.Text += string.Format("编号:{0,-4}价格:￥{2,-6}商品名称:{1,-9}\n", drv["id"], da.Tables[0].Rows[0]["spmc"], da.Tables[0].Rows[0]["price"]);
            }

            myCombobox.SelectedIndex = 0;
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (BuydedGoods.Count == 0)
                return;

            tbPrice.Text="0.0";
            crGoods.Text = "";

            //id.Clear();
            //id.Focus();

            try
            {
                Mysql ms = new Mysql("gcxm ", "gcxm", "gcxmgcxm", "39.106.61.96");
                ms.Open();
                ms.Begin();

                ExcelReport er = new ExcelReport();
                er.Open();

                string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                string timeSt ="LSH"+Convert.ToInt64(ts.TotalSeconds).ToString();

                int count = 1;

                try
                {
                    foreach (Spsm i in BuydedGoods)
                    {
                    
                        ms.Execute($"update cskcgl set nums = nums-1 where id = {i.id}");
                        ms.Execute($"insert into xsgl values('{dt}','{i.spmc}','{i.price}','{timeSt}')");

                        er.SetCellText(count, 1, dt);
                        er.MergeCells(er.GetRange(count, 1, count, 2));
                        er.SetCellText(count, 3, i.spmc);
                        er.SetCellText(count, 4, i.price.ToString());
                        er.SetCellText(count, 5, timeSt);
                        er.MergeCells(er.GetRange(count, 5, count, 6));
                        count++;
                    }

                    //er.MergeCells(er.GetRange(1, 1, count - 1, 2));
                    //er.MergeCells(er.GetRange(1, 5, count - 1, 6));
                    er.SaveAs($"D:\\temp\\{timeSt}.xlsx", false);
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

            BuydedGoods.Clear();
        }

        private void btnQx_Click(object sender, RoutedEventArgs e)
        {
            tbPrice.Text = "0.0";
            crGoods.Text = "";

            //id.Clear();
            //id.Focus();

            BuydedGoods.Clear();
        }
    }
}
