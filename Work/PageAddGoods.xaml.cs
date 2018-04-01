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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Work
{
    /// <summary>
    /// PageAddGoods.xaml 的交互逻辑
    /// </summary>
    public partial class PageAddGoods : Page
    {
        struct productInfo
        {
            public int number { get; set; }
            public string spmc { get; set; }
            public int nums { get; set; }
            public string price { get; set; }
            public bool flag { get; set; }

            public productInfo(int _number, string _spmc, int _nums, string _price, bool _flag)
            {
                number = _number;
                spmc = _spmc;
                nums = _nums;
                price = _price;
                flag = _flag;
            }
        }

        private List<productInfo> pif;

        public PageAddGoods()
        {
            InitializeComponent();
            dgTable_Lod();
            pif = new List<productInfo>();
        }

        
        public void dgTable_Lod(string sql = "")
        {
            try
            {
                Mysql mc = new Mysql();
                mc.Open();
                DataSet da = mc.Select($"select * from cskcgl {sql}");
                mc.Close();

                dgTable2.ItemsSource = da.Tables[0].DefaultView;
                dgTable2.LoadingRow += new EventHandler<DataGridRowEventArgs>(dgTable_LoadingRow);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void dgTable_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void btnAddNewGoods_Click(object sender, RoutedEventArgs e)
        {
            if (tbNums1.Text == "")
            {
                tbNums1.Focus();
                return;
            }
            else if (tbPrice.Text == "")
            {
                tbPrice.Focus();
                return;
            }
            else if (tbPname.Text == "")
            {
                tbPname.Focus();
                return;
            }
            else
            {
                try
                {
                    pif.Add(new productInfo(pif.Count, tbPname.Text, Convert.ToInt32(tbNums1.Text), tbPrice.Text,false));
                    tbNums1.Text = tbPrice.Text = tbPname.Text = "";
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

                dgTable.ItemsSource = null;
                dgTable.ItemsSource = pif;
            }
        }

        private void btnAddOldGoods_Click(object sender, RoutedEventArgs e)
        {
            if (tbNums2.Text == "")
            {
                tbNums2.Focus();
            }
            else if (tbId.Text == "")
            {
                tbId.Focus();
            }

            else
            { 
                Mysql mc = new Mysql();

                mc.Open();
                DataSet da = mc.Select($"select * from cskcgl where id = '{tbId.Text}'");
                mc.Close();

                if (da.Tables[0].Rows.Count == 0)
                    tbId.Focus();
                else
                {
                    try
                    {
                        pif.Add(new productInfo(pif.Count, da.Tables[0].Rows[0]["spmc"].ToString(), Convert.ToInt32(tbNums2.Text), Convert.ToString(da.Tables[0].Rows[0]["price"]), true));
                        tbId.Text = tbNums2.Text = "";
                    }

                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }

                    dgTable.ItemsSource = null;
                    dgTable.ItemsSource = pif;
                }
            }
        }

        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
            if (pif.Count == 0)
                return;

            Mysql mc = new Mysql();
            mc.Open();
            mc.Begin();
            try
            {
                foreach (productInfo temp in pif)
                {
                    if (temp.flag == true)
                        mc.Execute($"update cskcgl set nums =nums+{temp.nums} where spmc='{temp.spmc}'");
                    else
                        mc.Execute($"insert into cskcgl values(null,'{temp.spmc}','{temp.price}','{temp.nums}')");
                }
                mc.Commit();
                pif.Clear();
                dgTable.ItemsSource = null;
            }

            catch (Exception err)
            {
                mc.Rollback();
                MessageBox.Show(err.Message);
            }
            mc.Close();
            dgTable_Lod();
        }
       
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tbNumber.Text == "")
            {
                return;
            }
            pif.RemoveAt(Convert.ToInt32(tbNumber.Text));
            for (int i = Convert.ToInt32(tbNumber.Text); i < pif.Count; i++)
                pif[i] = new productInfo(pif[i].number-1, pif[i].spmc, pif[i].nums, pif[i].price, pif[i].flag);

            dgTable.ItemsSource = null;
            tbNumber.Text = "";
            dgTable.ItemsSource = pif;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            pif.Clear();
            dgTable.ItemsSource = null;
        }

        private void nums_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[0-9]");
            e.Handled = !re.IsMatch(e.Text);
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("^[0-9]+(\\u002e[0-9]{0,2})?$");
            e.Handled = !re.IsMatch(tbPrice.Text + e.Text);
        }

        private void dgTable_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow item = (DataGridRow)sender;
                
                FrameworkElement fe = dgTable.Columns[0].GetCellContent(item);
                TextBlock tb = (TextBlock)fe;
                tbNumber.Text = tb.Text;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void dgTable2_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow item = (DataGridRow)sender;

                FrameworkElement fe = dgTable.Columns[0].GetCellContent(item);
                TextBlock tb = (TextBlock)fe;
                tbId.Text = tb.Text;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
