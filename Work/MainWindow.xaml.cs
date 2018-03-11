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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Mysql mc = new Mysql("cljj", "zhq", "zhq", "localhost");
                DataSet da = mc.CX("select * from hlztp");
                dgTable.AutoGenerateColumns = false;
                dgTable.ItemsSource = da.Tables[0].DefaultView;
            }
            catch
            {
                MessageBox.Show("初始化失败");
            }
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            isLogined = (bool)lw.ShowDialog();
            MessageBox.Show(isLogined.ToString());
            //if(b==true)
        }

        private void dgTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }

        private void mycehis(object sender, DataGridCellEditEndingEventArgs e)
        {
            //MessageBox.Show(e.Column.GetCellContent(0).ToString());
            //DataRowView selectItem = datagrid.items[索引] as DataRowView
            var a = dgTable.SelectedItem;
            var b = a as DataRowView;
            string result = b.Row["xysl"].ToString();
            MessageBox.Show(result);
        }

        private void dgTable_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var a = dgTable.SelectedItem;
            var b = a as DataRowView;
            string result = b.Row[2].ToString();
            MessageBox.Show(result);
        }
    }
    }
