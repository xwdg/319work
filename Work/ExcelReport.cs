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
        
        
        private Object oMissing = System.Reflection.Missing.Value;  //ʵ������������
        
        public ExcelReport()
        {
            app = new ApplicationClass();
            app.DisplayAlerts = false;
            if (app == null)
            {
                MessageBox.Show("Excel����ʧ��", "ϵͳ��ʾ", MessageBoxButton.OK);
                return;
            }
        }

        

        #region �򿪹ر�
        /// <summary>
        /// ��Excel��������Ĭ�ϵ�Workbooks��
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            //�򿪲��½���Ĭ�ϵ�Excel
            //Workbooks.Add([template]) As Workbooks
            workbook = app.Workbooks.Add(oMissing);//����ģ�����excel�ļ�
            worksheet = (_Worksheet)workbook.Worksheets.get_Item(1);
            
            if (worksheet == null)
            {
                MessageBox.Show("Excel����ʧ��", "ϵͳ��ʾ",
                        MessageBoxButton.OK);
                this.Close();
                return;
            }

            if (workbook == null)
            {
                MessageBox.Show("Excel�������", "ϵͳ��ʾ", MessageBoxButton.OK);
                this.Close();
                return;
            }

        }

        /// <summary>
        /// �������й�����ģ��򿪣����ָ����ģ�岻���ڣ�����Ĭ�ϵĿ�ģ��
        /// </summary>
        /// <param name="p_templateFileName">����ģ��Ĺ������ļ���</param>
        public void Open(string p_templateFileName)
        {
            if (System.IO.File.Exists(p_templateFileName))
            {
                //��ģ���
                //Workbooks.Add Template:="C:\tpt.xlt"
                workbook = app.Workbooks.Add(p_templateFileName);
                worksheet = (_Worksheet)workbook.Worksheets.get_Item(1);
                if (workbook == null)
                {
                    MessageBox.Show("Excel�������", "ϵͳ��ʾ", MessageBoxButton.OK);
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
        /// �ر�
        /// </summary>
        public void Close()
        {

            app.Workbooks.Close();
            workbook = null;
            worksheet = null;
            app.Quit();
            app = null;

            oMissing = null;

            //ǿ���������գ�����ÿ��ʵ����Excel����Excell���̶�һ����
            System.GC.Collect();
        }


        #endregion

        #region ���
        /// <summary>
        /// ��档�������ɹ����򷵻�true������������治�ɹ���������Ѵ����ļ�����ѡ���˲��滻Ҳ����false
        /// </summary>
        /// <param name="p_fileName">��Ҫ������ļ���</param>
        /// <param name="p_ReplaceExistsFileName">����ļ����ڣ����滻</param>
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


        #region PrintPreview()��Print()��Excel��ӡ��Ԥ�������Ҫ��ʾExcel���ڣ�������IsVisibledExcel
        /// <summary>
        /// ��ʾExcel
        /// </summary>
        public void ShowExcel()
        {
            app.Visible = true;
        }

        /// <summary>
        /// ��Excel��ӡԤ�������Ҫ��ʾExcel���ڣ�������IsVisibledExcel 
        /// </summary>
        public void PrintPreview()
        {
            try
            {
                //��ʾexcel����Ԥ��
                app.Visible = true;//��excel������ʾ,����һ��ʾ���Ͼʹ�ӡԤ������ӡԤ�����ڻ��excel���ڸ�ס
                workbook.PrintPreview(true);//false��ʾ��ӡԤ�����ڵ����������ť������
                app.Visible = false;

                //true��ʾ��ӡԤ���Ǵ��������ð�ť������
                //workbook.PrintOut(Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch
            {
                MessageBox.Show("Ԥ��ʧ��", "ϵͳ��ʾ",
                                    MessageBoxButton.OK);
            }

        }

        /// <summary>
        /// ��Excel��ӡ�����Ҫ��ʾExcel���ڣ�������IsVisibledExcel 
        /// </summary>
        public void Print()
        {
            app.Visible = false;

            Object oMissing = System.Reflection.Missing.Value;  //ʵ������������
            try
            {
                app.ActiveWorkbook.PrintOut(oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            }
            catch { }
        }
        public void Print(string printername)
        {
            app.Visible = false;

            Object oMissing = System.Reflection.Missing.Value;  //ʵ������������
            try
            {
                app.ActiveWorkbook.PrintOut(oMissing, oMissing, oMissing, oMissing, printername, oMissing, oMissing, oMissing);
            }
            catch { }
        }
        #endregion

        #region GetRange
        /// <param name="p_startRowIndex">ָ����Ԫ��Χ��ʼ����������1��ʼ</param>
        /// <param name="p_startColIndex">ָ����Ԫ��Χ��ʼ��������������1��ʼ</param>
        /// <param name="p_endRowIndex">ָ����Ԫ��Χ����������</param>
        /// <param name="p_endColIndex">ָ����Ԫ��Χ��������������</param>
        public Range GetRange(int p_startRowIndex, int p_startColIndex, int p_endRowIndex, int p_endColIndex)
        {
            Range range;
            range = app.get_Range(app.Cells[p_startRowIndex, p_startColIndex], app.Cells[p_endRowIndex, p_endColIndex]);

            return range;
        }

        /// <param name="p_startChars">ָ����Ԫ��Χ��ʼ����ĸ���������</param>
        /// <param name="p_endChars">ָ����Ԫ��Χ��������ĸ���������</param>
        public Range GetRange(int p_startRowIndex, string p_startColChars, int p_endRowIndex, string p_endColChars)
        {
            //����	Range("D8:F11").Select
            Range range;

            range = worksheet.get_Range(p_startColChars + p_startRowIndex.ToString(), p_endColChars + p_endRowIndex.ToString());

            return range;
        }

        /// <summary>
        /// ��ȡָ����Ԫ���ָ����Χ�ڵĵ�Ԫ��������Ϊ��1��ʼ�����֣����65536��������ΪA~Z��AA~AZ��BA~BZ...HA~HZ��IA~IV����ĸ����ϣ�Ҳ������1-65536���֡�
        /// </summary>
        /// <param name="p_rowIndex">��Ԫ������������1��ʼ</param>
        /// <param name="p_colIndex">��Ԫ������������1��ʼ��������Ҳ��������ĸA��Z����ĸ���AA~AZ�����IV��Excel��ĸ����</param>
        /// <returns></returns>
        public Range GetRange(int p_rowIndex, int p_colIndex)
        {
            //����	Range(10,3).Select		//��10��3��
            return GetRange(p_rowIndex, p_colIndex, p_rowIndex, p_colIndex);
        }

        /// <param name="p_colChars">��Ԫ������ĸ�������������A��ʼ</param>
        public Range GetRange(int p_rowIndex, string p_colChars)
        {
            //����	Range("C10").Select		//��10��3��			
            return GetRange(p_rowIndex, p_colChars, p_rowIndex, p_colChars);
        }
        #endregion

        #region MergeCells(Excel.Range p_Range)�ϲ���Ԫ�񣬺ϲ���Ĭ�Ͼ���
        /// <summary>
        /// �ϲ�ָ����Χ�ڵ�Ԫ�񣬺ϲ���Ĭ�Ͼ���
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


        #region SetCellText(...)��������Ӧ��Range(...)������һ����Ԫ��Ҳ���������ڵĵ�Ԫ��һ������ͬ�����ı�����Range������ָ����Χ��Ϊ����
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

        #region ����һ��
        public void InsertRow(int p_rowIndex)
        {
            //    Rows("2:2").Select
            //    Selection.Insert Shift:=xlDown

            Range range;

            range = GetRange(p_rowIndex, "A");
            range.Select();

            //Excel2003֧��������
            range.EntireRow.Insert(oMissing,oMissing);			

            //Excel2000֧��һ���������������ԣ���Interop.ExcelV1.3(Excel2000)����������������Excel2003��
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
        #region ɾ��һ����
        /// <summary>
        /// ɾ��ָ��������
        /// </summary>
        /// <param name="p_rowIndex">������</param>
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
