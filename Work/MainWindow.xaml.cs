using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>

    public partial class MainWindow : Window
    {
        private bool isLogined = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        //初始化或刷新DataGrid数据
        public void dgTable_Lod()
        {
            try
            {
                Mysql mc = new Mysql("gcxm", "gcxm", "gcxmgcxm", "39.106.61.96");
                mc.Open();
                DataSet da = mc.Select("select * from cskcgl");
                mc.Close();
                dgTable.ItemsSource = da.Tables[0].DefaultView;
                dgTable.LoadingRow += new EventHandler<DataGridRowEventArgs>(dgTable_LoadingRow);
            }
            catch
            {
                MessageBox.Show("初始化失败");
            }
        }

        public void dgTable2_Lod()
        {
            try
            {
                Mysql mc = new Mysql("gcxm", "gcxm", "gcxmgcxm", "39.106.61.96");
                mc.Open();
                DataSet da = mc.Select("select * from xsgl");
                mc.Close();
                dgTable2.ItemsSource = da.Tables[0].DefaultView;
            }
            catch
            {
                MessageBox.Show("初始化失败");
            }
        }

        //初始化DataGrid
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dgTable_Lod();
        }

        //初始化DataGrid行序号
        private void dgTable_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        //登录按钮点击事件
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            MenuItem btn = (MenuItem)sender;
            if (isLogined == true)
            {
                btn.Header = "登录";
                isLogined = false;
                muMange.Visibility = Visibility.Hidden;
                muXsgl.Visibility = Visibility.Hidden;
                muSellGoods.Visibility = Visibility.Hidden;
                dgTable.Visibility = Visibility.Visible;
                dgTable2.Visibility= Visibility.Hidden;
            }

            else
            {
                LoginWindow lw = new LoginWindow();
                isLogined = (bool)lw.ShowDialog();

                if (isLogined)
                {
                    MessageBox.Show("登录成功");
                    btn.Header = "注销";
                    muMange.Visibility = Visibility.Visible;
                    muXsgl.Visibility = Visibility.Visible;
                    muSellGoods.Visibility = Visibility.Visible;
                }
            }
        }
        
        //数据格失去焦点后同步数据
        private void mycehis(object sender, DataGridCellEditEndingEventArgs e)
        {
            //foreach(var a in dgTable.)
            //MessageBox.Show(dgTable.Columns.IndexOf(e.Column).ToString());
            //DataRowView selectItem = datagrid.items[索引] as DataRowView
            //DataGridTextColumn dgt = e.Column as DataGridTextColumn;
            //Binding binding = dgt.Binding as Binding;
            //MessageBox.Show(binding.Path.Path);
            //var a = dgTable.SelectedItem;
            //dgTable.
            //if(string(b.Row["xysl"])!=rwmc)
            //b.Row["xysl"] = "ah";
            //MessageBox.Show(dgTable.CurrentColumn.ToString());
        }
        
        //。。。
        private void dgTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count == 0)
                return;
            //var currentCell = e.AddedCells[0];

            //if (currentCell.Column == dgTable.Columns[0]|| currentCell.Column == dgTable.Columns[1])   //Columns[]从0开始  我这的ComboBox在第四列  所以为3  
            //{
            //MessageBox.Show(dgTable.Columns.IndexOf(currentCell.Column).ToString());
            dgTable.BeginEdit();    //  进入编辑模式  这样单击一次就可以选择ComboBox里面的值了

            //}
        }

        //“管理商品--添加商品”菜单项点击事件
        private void muAdd_Click(object sender, RoutedEventArgs e)
        {
            AddGoodsWindow add = new AddGoodsWindow();
            add.ShowDialog();
            dgTable_Lod();
        }
        
        //“管理商品--修改商品”菜单项点击事件

        private void dgTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void myceshi(object sender, DataGridRowEditEndingEventArgs e)
        //{
        //    //MessageBox.Show("asdjjsd");
        //    //dgTable.
        //    //DataGridRow 
        //    MessageBox.Show(e.Row.GetIndex().ToString());
        //    DataGridRow dr = (DataGridRow)dgTable.ItemContainerGenerator.ContainerFromIndex(e.Row.GetIndex());
        //    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(dr);
        //    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(1); //取第0列每行单元格
        //    cell.Focus();
        //    //dgTable.
        //    //e.Row.IsSelected = true;
        //    //dgTable.BeginEdit();
        //    //DataRowView rowView = e.Row.Item as DataRowView;
        //    //MessageBox.Show(rowView.Row[0].ToString()+ dgTable.Items.Count+e.Row.GetType());
        //    //e.Cancel = true;
        //    //var row = dgTable.ItemsContainerGenerator.ContainerFromIndex(0) as FrameworkElement;
        //    //if (row != null)
        //    //row.Focus();
        //    //sender.ToString()
        //    //tb.Focus();
        //    //e.Row.//int numVisuals = VisualTreeHelper.GetChildrenCount(e.Row);
        //    //MessageBox.Show(numVisuals.ToString());
        //    //DataGridCellsPresenter 
        //    //e.Row[0][0];
        //    //cell.Focus();
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dgTable_Lod();
        }

        //public static T GetVisualChild<T>(Visual parent) where T : Visual
        //{
        //    T child = default(T);
        //    int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
        //    for (int i = 0; i < numVisuals; i++)
        //    {
        //        Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
        //        child = v as T;
        //        if (child == null)
        //        {
        //            child = GetVisualChild<T>(v);
        //        }
        //        if (child != null)
        //        {
        //            break;
        //        }
        //    }
        //    return child;
        //}
        
        private void btnDltSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgTable.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中商品", "ERROR");
                return;
            }
            else
            {
                try
                {
                    DataRowView drv = (DataRowView)dgTable.SelectedItem;
                    Mysql ms = new Mysql("gcxm", "gcxm", "gcxmgcxm", "39.106.61.96");

                    ms.Open();
                    ms.Execute($"delete from cskcgl where id='{drv.Row[0].ToString()}'");
                    ms.Close();
                    MessageBox.Show(drv.Row[0]+"删除成功");
                    dgTable_Lod();
                }

                catch (Exception c)
                {
                    MessageBox.Show(c.Message,"error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
        //修改商品时的代码
        private void btnAltSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgTable.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中商品", "ERROR");
                return;
            }
            else
            {
                try
                {
                    DataRowView drv = (DataRowView)dgTable.SelectedItem;
                    AlterGoodWindow agw = new AlterGoodWindow((UInt16)drv.Row["id"], (string)drv.Row["spmc"],(float)drv.Row["price"],(int)drv.Row["nums"]);
                    agw.ShowDialog();
                    dgTable_Lod();
                }

                catch (Exception c)
                {
                    MessageBox.Show(c.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void muXsgl_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            if ("销售记录" == mi.Header.ToString())
            {
                mi.Header = "库存管理";
                dgTable.Visibility = Visibility.Hidden;
                dgTable2.Visibility = Visibility.Visible;
                dgTable2_Lod();
                muMange.Visibility = Visibility.Hidden;
            }

            else if ("库存管理" == mi.Header.ToString())
            {
                mi.Header = "销售记录";
                dgTable.Visibility = Visibility.Visible;
                dgTable2.Visibility = Visibility.Hidden;
                dgTable_Lod();
                muMange.Visibility = Visibility.Visible;
            }
        }

        private void muSellGoods_Click(object sender, RoutedEventArgs e)
        {
            SellWindow sw = new SellWindow();
            sw.ShowDialog();
        }
    }
}
