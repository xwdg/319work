using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Office.Interop.Excel;
//using Excel;
using System.Data;
using System.Linq;
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
    class ExcelReport
    {
        ApplicationClass app;
        //Workbooks workbooks;
        _Workbook workbook;
       // Sheets sheets;
        _Worksheet worksheet;
        
        
        private Object oMissing = System.Reflection.Missing.Value;  //实例化参数对象
        
        public ExcelReport()
        {
            app = new ApplicationClass();
            app.DisplayAlerts = false;
            if (app == null)
            {
                MessageBox.Show("Excel启动失败", "系统提示", MessageBoxButton.OK);
                return;
            }
        }

        

        #region 打开关闭
        /// <summary>
        /// 打开Excel，并建立默认的Workbooks。
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            //打开并新建立默认的Excel
            //Workbooks.Add([template]) As Workbooks
            workbook = app.Workbooks.Add(oMissing);//根据模板添加excel文件
            worksheet = (_Worksheet)workbook.Worksheets.get_Item(1);
            
            if (worksheet == null)
            {
                MessageBox.Show("Excel操作失败", "系统提示",
                        MessageBoxButton.OK);
                this.Close();
                return;
            }

            if (workbook == null)
            {
                MessageBox.Show("Excel程序出错", "系统提示", MessageBoxButton.OK);
                this.Close();
                return;
            }

        }

        /// <summary>
        /// 根据现有工作薄模板打开，如果指定的模板不存在，则用默认的空模板
        /// </summary>
        /// <param name="p_templateFileName">用作模板的工作薄文件名</param>
        public void Open(string p_templateFileName)
        {
            if (System.IO.File.Exists(p_templateFileName))
            {
                //用模板打开
                //Workbooks.Add Template:="C:\tpt.xlt"
                workbook = app.Workbooks.Add(p_templateFileName);
                worksheet = (_Worksheet)workbook.Worksheets.get_Item(1);
                if (workbook == null)
                {
                    MessageBox.Show("Excel程序出错", "系统提示", MessageBoxButton.OK);
                    this.Close();
                    return;
                }
            }
            else
            {
                Open();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {

            app.Workbooks.Close();
            workbook = null;
            worksheet = null;
            app.Quit();
            app = null;

            oMissing = null;

            //强制垃圾回收，否则每次实例化Excel，则Excell进程多一个。
            System.GC.Collect();
        }


        #endregion

        #region 另存
        /// <summary>
        /// 另存。如果保存成功，则返回true，否则，如果保存不成功或者如果已存在文件但是选择了不替换也返回false
        /// </summary>
        /// <param name="p_fileName">将要保存的文件名</param>
        /// <param name="p_ReplaceExistsFileName">如果文件存在，则替换</param>
        public bool SaveAs(string p_fileName, bool p_ReplaceExistsFileName)
        {
            bool blnReturn = false;
            if (System.IO.File.Exists(p_fileName))
            {
                if (p_ReplaceExistsFileName)
                {
                    try
                    {
                        System.IO.File.Delete(p_fileName);
                        blnReturn = true;
                    }
                    catch (Exception ex)
                    {
                        string strErr = ex.Message;
                    }
                }
            }

            try
            {

                app.ActiveWorkbook.SaveCopyAs(p_fileName);
                blnReturn = true;
            }
            catch
            {
                blnReturn = false;
            }

            return blnReturn;
        }
        #endregion


        #region PrintPreview()、Print()用Excel打印、预览，如果要显示Excel窗口，请设置IsVisibledExcel
        /// <summary>
        /// 显示Excel
        /// </summary>
        public void ShowExcel()
        {
            app.Visible = true;
        }

        /// <summary>
        /// 用Excel打印预览，如果要显示Excel窗口，请设置IsVisibledExcel 
        /// </summary>
        public void PrintPreview()
        {
            try
            {
                //显示excel并且预览
                app.Visible = true;//让excel窗口显示,这里一显示马上就打印预览，打印预览窗口会把excel窗口盖住
                workbook.PrintPreview(true);//false表示打印预览窗口的设置这个按钮不能用
                app.Visible = false;

                //true表示打印预览是窗口上设置按钮可以用
                //workbook.PrintOut(Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch
            {
                MessageBox.Show("预览失败", "系统提示",
                                    MessageBoxButton.OK);
            }

        }

        /// <summary>
        /// 用Excel打印，如果要显示Excel窗口，请设置IsVisibledExcel 
        /// </summary>
        public void Print()
        {
            app.Visible = false;

            Object oMissing = System.Reflection.Missing.Value;  //实例化参数对象
            try
            {
                app.ActiveWorkbook.PrintOut(oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            }
            catch { }
        }
        public void Print(string printername)
        {
            app.Visible = false;

            Object oMissing = System.Reflection.Missing.Value;  //实例化参数对象
            try
            {
                app.ActiveWorkbook.PrintOut(oMissing, oMissing, oMissing, oMissing, printername, oMissing, oMissing, oMissing);
            }
            catch { }
        }
        #endregion

        #region GetRange
        /// <param name="p_startRowIndex">指定单元范围起始行索引，从1开始</param>
        /// <param name="p_startColIndex">指定单元范围起始列数字索引，从1开始</param>
        /// <param name="p_endRowIndex">指定单元范围结束行索引</param>
        /// <param name="p_endColIndex">指定单元范围结束列数字索引</param>
        public Range GetRange(int p_startRowIndex, int p_startColIndex, int p_endRowIndex, int p_endColIndex)
        {
            Range range;
            range = app.get_Range(app.Cells[p_startRowIndex, p_startColIndex], app.Cells[p_endRowIndex, p_endColIndex]);

            return range;
        }

        /// <param name="p_startChars">指定单元范围起始列字母及组合索引</param>
        /// <param name="p_endChars">指定单元范围结束列字母及组合索引</param>
        public Range GetRange(int p_startRowIndex, string p_startColChars, int p_endRowIndex, string p_endColChars)
        {
            //矩形	Range("D8:F11").Select
            Range range;

            range = worksheet.get_Range(p_startColChars + p_startRowIndex.ToString(), p_endColChars + p_endRowIndex.ToString());

            return range;
        }

        /// <summary>
        /// 获取指定单元格或指定范围内的单元格，行索引为从1开始的数字，最大65536，列索引为A~Z、AA~AZ、BA~BZ...HA~HZ、IA~IV的字母及组合，也可以是1-65536数字。
        /// </summary>
        /// <param name="p_rowIndex">单元格行索引，从1开始</param>
        /// <param name="p_colIndex">单元格列索引，从1开始，列索引也可以用字母A到Z或字母组合AA~AZ，最大IV的Excel字母索引</param>
        /// <returns></returns>
        public Range GetRange(int p_rowIndex, int p_colIndex)
        {
            //单个	Range(10,3).Select		//第10行3列
            return GetRange(p_rowIndex, p_colIndex, p_rowIndex, p_colIndex);
        }

        /// <param name="p_colChars">单元格列字母及组合索引，从A开始</param>
        public Range GetRange(int p_rowIndex, string p_colChars)
        {
            //单个	Range("C10").Select		//第10行3列			
            return GetRange(p_rowIndex, p_colChars, p_rowIndex, p_colChars);
        }
        #endregion

        #region MergeCells(Excel.Range p_Range)合并单元格，合并后，默认居中
        /// <summary>
        /// 合并指定范围内单元格，合并后，默认居中
        /// </summary>
        /// <param name="p_Range"></param>
        public void MergeCells(Range p_Range)
        {
            p_Range.HorizontalAlignment = Constants.xlCenter;
            p_Range.VerticalAlignment = Constants.xlCenter;
            p_Range.WrapText = false;
            p_Range.Orientation = 0;
            p_Range.AddIndent = false;
            p_Range.IndentLevel = 0;
            p_Range.ShrinkToFit = false;
            p_Range.MergeCells = false;
            p_Range.Merge(oMissing);
        }

        #endregion

        #region GetCellText(p_Range)
        public string GetCellText(Range p_Range)
        {
            string strReturn = "";
            strReturn = p_Range.Text.ToString();
            return strReturn;
        }

        #endregion


        #region SetCellText(...)，参数对应于Range(...)，可以一个单元格也可以区域内的单元格一起设置同样的文本。用Range或它的指定范围作为参数
        public void SetCellText(int p_rowIndex, int p_colIndex, string p_text)
        {
            //			xlApp.Cells[p_rowIndex,p_colIndex] = p_text;			
            Range range;
            range = GetRange(p_rowIndex, p_colIndex);
            range.Cells.FormulaR1C1 = p_text;
            range = null;
        }

        public void SetCellText(int p_rowIndex, string p_colChars, string p_text)
        {
            Range range;
            range = GetRange(p_rowIndex, p_colChars);
            range.Cells.FormulaR1C1 = p_text;
            range = null;
        }

        public void SetCellText(int p_startRowIndex, int p_startColIndex, int p_endRowIndex, int p_endColIndex, string p_text)
        {
            Range range;
            range = GetRange(p_startRowIndex, p_startColIndex, p_endRowIndex, p_endColIndex);
            range.Cells.FormulaR1C1 = p_text;
            range = null;
        }

        public void SetCellText(int p_startRowIndex, string p_startColChars, int p_endRowIndex, string p_endColChars, string p_text)
        {
            Range range;
            range = GetRange(p_startRowIndex, p_startColChars, p_endRowIndex, p_endColChars);
            range.Cells.FormulaR1C1 = p_text;
            range = null;
        }
        #endregion

        #region 插入一行
        public void InsertRow(int p_rowIndex)
        {
            //    Rows("2:2").Select
            //    Selection.Insert Shift:=xlDown

            Range range;

            range = GetRange(p_rowIndex, "A");
            range.Select();

            //Excel2003支持两参数
            range.EntireRow.Insert(oMissing,oMissing);			

            //Excel2000支持一个参数，经过测试，用Interop.ExcelV1.3(Excel2000)，可以正常运行在Excel2003中
            //range.EntireRow.Insert(oMissing);
        }
        public void InsertRow(int p_rowIndex, int p_templateRowIndex)
        {
            Range range;
            range = (Range)app.Rows[p_templateRowIndex.ToString() + ":" + p_templateRowIndex.ToString(), oMissing];
            range.Select();
            range.Copy(oMissing);

            InsertRow(p_rowIndex);
        }
        #endregion
        #region 删除一整行
        /// <summary>
        /// 删除指定的整行
        /// </summary>
        /// <param name="p_rowIndex">行索引</param>
        public void DeleteRow(int p_rowIndex)
        {
            Range range;

            range = GetRange(p_rowIndex, "A");
            range.Select();
            range.EntireRow.Delete(oMissing);
        }
        #endregion
        
    
    }
}
