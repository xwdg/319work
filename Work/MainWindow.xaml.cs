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
        private bool IsLogined = false;
        public string UserName { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            frmMain.Navigate(new Uri("PageQuery.xaml", UriKind.Relative));
        }
        //登录按钮点击事件
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            MenuItem btn = (MenuItem)sender;
            if (IsLogined == true)
            {
                IsLogined = false;
                UserName = "";
            }

            else
            {
                LoginWindow lw = new LoginWindow();

                IsLogined = (bool)lw.ShowDialog();

                if (IsLogined)
                {
                    UserName = lw.UserName;
                }
            }
        }
        
        ////数据格失去焦点后同步数据
        //private void mycehis(object sender, DataGridCellEditEndingEventArgs e)
        //{
        //    //foreach(var a in dgTable.)
        //    //MessageBox.Show(dgTable.Columns.IndexOf(e.Column).ToString());
        //    //DataRowView selectItem = datagrid.items[索引] as DataRowView
        //    //DataGridTextColumn dgt = e.Column as DataGridTextColumn;
        //    //Binding binding = dgt.Binding as Binding;
        //    //MessageBox.Show(binding.Path.Path);
        //    //var a = dgTable.SelectedItem;
        //    //dgTable.
        //    //if(string(b.Row["xysl"])!=rwmc)
        //    //b.Row["xysl"] = "ah";
        //    //MessageBox.Show(dgTable.CurrentColumn.ToString());
        //}
        
        ////。。。
        //private void dgTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    if (e.AddedCells.Count == 0)
        //        return;
        //    //var currentCell = e.AddedCells[0];

        //    //if (currentCell.Column == dgTable.Columns[0]|| currentCell.Column == dgTable.Columns[1])   //Columns[]从0开始  我这的ComboBox在第四列  所以为3  
        //    //{
        //    //MessageBox.Show(dgTable.Columns.IndexOf(currentCell.Column).ToString());
        //    dgTable.BeginEdit();    //  进入编辑模式  这样单击一次就可以选择ComboBox里面的值了

        //    //}
        //}

        //“管理商品--添加商品”菜单项点击事件
        private void muAdd_Click(object sender, RoutedEventArgs e)
        {
            //AddGoodsWindow add = new AddGoodsWindow(UserName);
            //add.ShowDialog();

            //dgTable_Lod(Da[0].dg, Da[0].ta);
            frmMain.Navigate(new Uri("PageAddGoods.xaml", UriKind.Relative));

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
            //if (dgTable.SelectedItems.Count == 0)
            //{
            //    MessageBox.Show($"{TryFindResource("Errmessage1")}", "ERROR");
            //    return;
            //}
            //else
            //{
            //    DataRowView drv = (DataRowView)dgTable.SelectedItem;
            //    Mysql ms = new Mysql();

            //    ms.Open();
            //    ms.Begin();

            //    try
            //    {
            //        ms.Execute($"delete from cskcgl where id='{drv.Row[0].ToString()}'");
            //        ms.Execute($"insert into record values(null,'{UserName}','delete',now(),{drv.Row[0]},{drv.Row[1]},{drv.Row[2]},null,null)");
                    
            //        MessageBox.Show(drv.Row[0]+$"{TryFindResource("Message1")}");
            //        dgTable_Lod(Da[0].dg, Da[0].ta);
            //        ms.Commit();
            //    }

            //    catch (Exception c)
            //    {
            //        MessageBox.Show(c.Message,"error",MessageBoxButton.OK,MessageBoxImage.Error);
            //        ms.Rollback();
            //    }

            //    ms.Close();
            //}
        }
        //修改商品时的代码
        private void btnAltSelect_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void muXsgl_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new Uri("PageQuerySellDetail.xaml", UriKind.Relative));
        }

        private void muSellGoods_Click(object sender, RoutedEventArgs e)
        {
            SellWindow sw = new SellWindow();
            sw.ShowDialog();
        }

        private void bthSwitch_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            ResourceDictionary dict = new ResourceDictionary();

            dict.Source = new Uri($@"Resources\Language\{mi.Name}.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries[0] = dict;
        }

        
        private void muSel_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new Uri("PageQuery.xaml", UriKind.Relative));
        }

        private void tviMangeRecord_Selected(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new Uri("PageQuerySell.xaml", UriKind.Relative));
        }

        private void tvidel_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void tvispbg_Selected(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new Uri("PageQueryGoodsAlter.xaml", UriKind.Relative));
        }

        private void tviUp_Selected(object sender, RoutedEventArgs e)
        {
            //frmMain.Navigate(new Uri("PageAlterGoods.xaml", UriKind.Relative));
            PageAlterGoods page1 = new PageAlterGoods();
            this.frmMain.Content = page1;
            page1.parentWindow = this;
        }

        private void tviShowDatebase_Selected(object sender, RoutedEventArgs e)
        {
            GetAppCon gac = new GetAppCon();

            string DBName = gac.ReadSetting("DBName");
            string DBURL = gac.ReadSetting("DBURL");
            string DBUser = gac.ReadSetting("DBUser");
            string DBPassword = gac.ReadSetting("DBPwd");
            string DBPort = gac.ReadSetting("DBPort");

            MessageBox.Show($"{TryFindResource("dbName")}:{DBName}\n{TryFindResource("ipAddress")}:{DBURL}\n{TryFindResource("Username")}:{DBUser}\n{TryFindResource("Password")}:{DBPassword}\n{TryFindResource("port")}:{DBPort}\n");
        }
    }
}
