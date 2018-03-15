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

    //struct lala
    //{
    //    public string s { set; get; }
    //    public string t { set; get; }
    //    //public Button la { set; get; }
    //    public lala(string st,string tt)
    //    {
    //        s = st;
    //        t = tt;
    //    }
    //}

    public partial class MainWindow : Window
    {
        private bool isLogined = false;
        private Mysql ms;
        //private string rwmc;
        //private string scmc;
        //private int xysl;
        //private int tpdj;

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
                DataSet da = mc.Select("select * from body");
                //
                dgTable.ItemsSource = da.Tables[0].DefaultView;
                dgTable.LoadingRow += new EventHandler<DataGridRowEventArgs>(dgTable_LoadingRow);
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

            //List<MyData> md = new List<MyData>();
            //foreach (DataRow dr in da.Tables[0].Rows)
            //foreach (DataColumn dc in da.Tables[0].Columns)//遍历所有的列
            //md.Add(new MyData((string)dr.ItemArray[0], (int)dr.ItemArray[1], (string)dr.ItemArray[2], (int)dr.ItemArray[3]));
            // List<lala> data = new List<lala>();
            // lala temp = new lala();
            // temp.s = "aaa";
            // temp.t = "cnm";
            // for (int i = 0; i < 100; i++)
            // {
            //     data.Add(new lala($"{i}",$"{i+1}"));
            //}

            //foreach (DataTable dt in da.Tables)
            //{
            //foreach (DataRow dr in da.Tables[0].Rows)
            //foreach (DataColumn dc in da.Tables[0].Columns)//遍历所有的列
            //MessageBox.Show($"{dr[dc]}");
            //}
            //MessageBox.Show($"{da.Tables[0].Rows[0][0]}");
        }

        //初始化DataGrid行序号
        private void dgTable_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        //登录按钮点击事件
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            isLogined = (bool)lw.ShowDialog();
            MessageBox.Show(isLogined.ToString());
            if (isLogined)
                dgTable.CanUserAddRows = true;
            //if(b==true)
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

        //ToDo：保存要编辑单元格原始内容
        private void dgTable_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var a = dgTable.SelectedItem;
            var b = a as DataRowView;
            //rwmc = b.Row["xysl"].ToString();
            //MessageBox.Show(/*dgTable.SelectedIndex.ToString()+*/ b.Row.Table.Columns.IndexOf("asdhd").ToString());
            //for(int i=0;i<b.Row.Table.Columns.Count)
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
            //if (isLogined == false)
            //    MessageBox.Show("cnm");
            //else
            //dgTable.CanUserAddRows = true;
            AddGoodsWindow add = new AddGoodsWindow();
            add.ShowDialog();
        }

        //“管理商品--删除商品”菜单项点击事件
        private void muDel_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView drv in dgTable.SelectedItems)
                MessageBox.Show(drv.Row[0].ToString());
            //MessageBox.Show(dgTable.SelectedItems.Count.ToString());
        }

        //“管理商品--修改商品”菜单项点击事件
        private void muCha_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataGridColumn temp in dgTable.Columns)
                temp.IsReadOnly = false;
            //dgTable.s
            //dgTable.BeginEdit();
        }

        private void dgTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void myceshi(object sender, DataGridRowEditEndingEventArgs e)
        {
            //MessageBox.Show("asdjjsd");
            //dgTable.
            //DataGridRow 
            MessageBox.Show(e.Row.GetIndex().ToString());
            DataGridRow dr = (DataGridRow)dgTable.ItemContainerGenerator.ContainerFromIndex(e.Row.GetIndex());
            DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(dr);
            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(1); //取第0列每行单元格
            cell.Focus();
            //dgTable.
            //e.Row.IsSelected = true;
            //dgTable.BeginEdit();
            //DataRowView rowView = e.Row.Item as DataRowView;
            //MessageBox.Show(rowView.Row[0].ToString()+ dgTable.Items.Count+e.Row.GetType());
            //e.Cancel = true;
            //var row = dgTable.ItemsContainerGenerator.ContainerFromIndex(0) as FrameworkElement;
            //if (row != null)
            //row.Focus();
            //sender.ToString()
            //tb.Focus();
            //e.Row.//int numVisuals = VisualTreeHelper.GetChildrenCount(e.Row);
            //MessageBox.Show(numVisuals.ToString());
            //DataGridCellsPresenter 
            //e.Row[0][0];
            //cell.Focus();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dgTable.CanUserAddRows = true;
            DataGridRow dt = (DataGridRow)dgTable.ItemContainerGenerator.ContainerFromIndex(80);
            DataGridRow dr = (DataGridRow)dgTable.ItemContainerGenerator.ContainerFromIndex(8);
            dt.IsSelected = true;
            DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(dr);
            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(1); //取第0列每行单元格
            //cell.Focus();
            //dgTable.SelectedIndex = 215;
            dgTable.BeginEdit();
           
        }
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
           //if(check_)
        }

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
                    ms.Execute($"delete from body where id='{drv.Row[0].ToString()}'");
                    ms.Close();
                    MessageBox.Show(drv.Row[0]+"删除成功");
                    dgTable_Lod();
                }

                catch (Exception c)
                {
                    MessageBox.Show(c.Message);
                }
            }
        }

        private void btnAltSelect_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
