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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Work
{
    /// <summary>
    /// PageQuerySell.xaml 的交互逻辑
    /// </summary>
    public partial class PageQuerySell : Page
    {
        public PageQuerySell()
        {
            InitializeComponent();
            dgTable_Lod();
        }

        public void dgTable_Lod(string sql = "")
        {
            try
            {
                Mysql mc = new Mysql();
                mc.Open();
                DataSet da = mc.Select($"select * from jyjl {sql}");
                mc.Close();

                dgTable.ItemsSource = da.Tables[0].DefaultView;
                dgTable.LoadingRow += new EventHandler<DataGridRowEventArgs>(dgTable_LoadingRow);
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

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            if (stDate.Text == "")
            {
                stDate.Focus();
            }

            else if (endDate.Text == "")
            {
                endDate.Focus();
            }
            else
                dgTable_Lod($"where DATE_FORMAT(date,'%Y-%m-%d') between '{Convert.ToDateTime(stDate.Text).ToString("yyyy-MM-dd")}' and '{Convert.ToDateTime(endDate.Text).ToString("yyyy-MM-dd")}'");
        }

        private void btnQuery2_Click(object sender, RoutedEventArgs e)
        {
            if (lsh.Text == "")
            {
                lsh.Focus();
                return;
            }

            else
            {
                dgTable_Lod($"where lsh = '{lsh.Text}'");
            }
        }

        private void btnCz_Click(object sender, RoutedEventArgs e)
        {
            lsh.Text = "";
            stDate.Text = "";
            endDate.Text = "";

            dgTable_Lod();
        }
    }
}
