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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>

    struct lala
    {
        public string s { set; get; }
        public string t { set; get; }
        //public Button la { set; get; }
        public lala(string st,string tt)
        {
            s = st;
            t = tt;
        }
    }

    public partial class MainWindow : Window
    {
        private bool isLogined = false;
        //private string rwmc;
        //private string scmc;
        //private int xysl;
        //private int tpdj;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void dgTable_Lod()
        {
            try
            {
                Mysql mc = new Mysql("work_319", "zhq", "zhqssb", "39.106.61.96");
                DataSet da = mc.CX("select * from hlztp");
                dgTable.AutoGenerateColumns = false;
                dgTable.ItemsSource = da.Tables[0].DefaultView;
                dgTable.LoadingRow += new EventHandler<DataGridRowEventArgs>(dgTable_LoadingRow);
            }
            catch
            {
                MessageBox.Show("初始化失败");
            }
        }

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

        private void dgTable_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            isLogined = (bool)lw.ShowDialog();
            MessageBox.Show(isLogined.ToString());
            if (isLogined)
                dgTable.CanUserAddRows = true;
            //if(b==true)
        }
        
        private void mycehis(object sender, DataGridCellEditEndingEventArgs e)
        {
            //foreach(var a in dgTable.)
            //MessageBox.Show(dgTable.Columns.IndexOf(e.Column).ToString());
            //DataRowView selectItem = datagrid.items[索引] as DataRowView
            MessageBox.Show(e.Column.Header.ToString());
            var a = dgTable.SelectedItem;
            //dgTable.
            var b = a as DataRowView;
            //if(string(b.Row["xysl"])!=rwmc)
            //b.Row["xysl"] = "ah";
            MessageBox.Show(dgTable.CurrentColumn.ToString());
        }

        private void dgTable_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var a = dgTable.SelectedItem;
            var b = a as DataRowView;
            //rwmc = b.Row["xysl"].ToString();
            //MessageBox.Show(/*dgTable.SelectedIndex.ToString()+*/ b.Row.Table.Columns.IndexOf("asdhd").ToString());
            //for(int i=0;i<b.Row.Table.Columns.Count)
        }

        private void dgTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count == 0)
                return;
            var currentCell = e.AddedCells[0];
            
            //if (currentCell.Column == dgTable.Columns[0]|| currentCell.Column == dgTable.Columns[1])   //Columns[]从0开始  我这的ComboBox在第四列  所以为3  
            //{
                //MessageBox.Show(dgTable.Columns.IndexOf(currentCell.Column).ToString());
                dgTable.BeginEdit();    //  进入编辑模式  这样单击一次就可以选择ComboBox里面的值了  

            //}
        }

        private void muAdd_Click(object sender, RoutedEventArgs e)
        {
            //if (isLogined == false)
            //    MessageBox.Show("cnm");
            //else
            dgTable.CanUserAddRows = true;
            dgTable.BeginEdit();
        }

        private void muDel_Click(object sender, RoutedEventArgs e)
        {
            //if (isLogined == false)
                //MessageBox.Show("cnm");
        }

        private void muCha_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataGridColumn temp in dgTable.Columns)
                temp.IsReadOnly = false;
        }
    }
    }
