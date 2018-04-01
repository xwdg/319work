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
    /// PageAlterGoods.xaml 的交互逻辑
    /// </summary>
    public partial class PageAlterGoods : Page
    {
        public MainWindow parentWindow;

        public MainWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }

        public PageAlterGoods()
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgTable.SelectedItems.Count == 0)
            {
                MessageBox.Show($"{TryFindResource("Errmessage1")}", "ERROR");
                return;
            }
            else
            {
                DataRowView drv = (DataRowView)dgTable.SelectedItem;
                Mysql ms = new Mysql();

                ms.Open();
                ms.Begin();

                try
                {
                    ms.Execute($"delete from cskcgl where id='{drv.Row[0].ToString()}'");
                    ms.Execute($"insert into record values(null,'{parentWindow.UserName}','delete',now(),{drv.Row[0]},{drv.Row[1]},null,{drv.Row[2]},null)");

                    MessageBox.Show(drv.Row[0] + $"{TryFindResource("Message1")}");
                    dgTable_Lod();
                    ms.Commit();
                }

                catch (Exception c)
                {
                    MessageBox.Show(c.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ms.Rollback();
                }

                ms.Close();
                dgTable_Lod();
                id.Text = price.Text = nums.Text = name.Text = "";
            }
            
        }

        private void btnAlter_Click(object sender, RoutedEventArgs e)
        {
            if (dgTable.SelectedItems.Count == 0)
            {
                MessageBox.Show($"{TryFindResource("Errmessage1")}", "ERROR");
                return;
            }

            DataRowView drv = (DataRowView)dgTable.SelectedItem;

            if (id.Text == drv[0].ToString()&& name.Text == drv[1].ToString() && price.Text == drv[2].ToString() && nums.Text == drv[3].ToString())
            {
                MessageBox.Show("no change");
                return;
            }

            try
            {
                Mysql mc = new Mysql();
                mc.Open();
                mc.Execute($"update cskcgl set nums='{nums.Text}',price='{price.Text}',spmc='{name.Text}' where id = '{drv.Row[0].ToString()}'");
                mc.Execute($"insert into record values(null,'{parentWindow.UserName}','update',now(),{drv.Row[0]},'{drv.Row[1]}','{name.Text}',{drv.Row[2]},{price.Text})");
                mc.Close();

                MessageBox.Show($"{TryFindResource("Message3")}", "drv.Row[0]");
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            id.Text = price.Text = nums.Text = name.Text = "";
            dgTable_Lod();
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

        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow item = (DataGridRow)sender;
                List<TextBox> a = new List<TextBox>();

                a.Add(id);
                a.Add(name);
                a.Add(price);
                a.Add(nums);

                FrameworkElement fe = null;
                TextBlock tb = null;

                for (int i = 0; i < dgTable.Columns.Count; i++)
                {
                    fe = dgTable.Columns[i].GetCellContent(item);
                    tb = (TextBlock)fe;
                    a[i].Text = tb.Text;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
