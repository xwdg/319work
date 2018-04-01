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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            dgTable_Lod();
        }

        public void dgTable_Lod(string sql="")
        {
            try
            {
                Mysql mc = new Mysql();
                mc.Open();
                DataSet da = mc.Select($"select * from cskcgl {sql}");
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

        private void nums_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[0-9]");
            e.Handled = !re.IsMatch(e.Text);
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            if (minPrice.Text == "")
            {
                minPrice.Focus();
                return;
            }
            else if (maxPrice.Text == "")
            {
                maxPrice.Focus();
                return;
            }
            else
            {
                dgTable_Lod($"where price between {Convert.ToInt32(minPrice.Text)} and {Convert.ToInt32(maxPrice.Text)}");
            }
        }

        private void btnQuery2_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text == "")
            {
                id.Focus();
                return;
            }

            else
            {
                dgTable_Lod($"where id = {Convert.ToInt16(id.Text)}");
            }
        }

        private void btnCz_Click(object sender, RoutedEventArgs e)
        {
            minPrice.Text = "";
            maxPrice.Text = "";
            id.Text = "";

            dgTable_Lod();
        }
    }
}
