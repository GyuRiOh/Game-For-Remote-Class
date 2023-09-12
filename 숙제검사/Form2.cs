using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

using System.Runtime.InteropServices;
using System.Data.OleDb;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace 숙제검사
{
    public partial class scoreUp : Form
    {
        //static Excel.Application excelApp = null;
        //static Excel.Workbook workBook = null;
        //static Excel.Worksheet workSheet = null;


        int[] score = new int[32];
        public static string[] stuName = new string[32];
        string classNum;
        public static int[] total = new int[32];
        int counter;


        public scoreUp()
        {
            InitializeComponent();

            InitializeTimer();



            for (int i = 0; i < 31; i++)
            {
                var btn = this.Controls.Find("button" + (i + 1).ToString(), true).FirstOrDefault();
                if (btn != null)
                {
                    this.Controls.Find("button" + (i + 1).ToString(), true).FirstOrDefault().Text = (i + 1).ToString();
                    this.Controls.Find("button" + (i + 1).ToString(), true).FirstOrDefault().Click += ButtonUp_event;
                }

                var btn2 = this.Controls.Find("button" + (62 - i).ToString(), true).FirstOrDefault();
                if (btn2 != null)
                {
                    this.Controls.Find("button" + (62 - i).ToString(), true).FirstOrDefault().Text = (i + 1).ToString();
                    this.Controls.Find("button" + (62 - i).ToString(), true).FirstOrDefault().Click += ButtonDown_event;
                }

            }


        }
        private void InitializeTimer()
        {
            // Run this procedure in an appropriate event.  
            counter = 300;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            // Hook up timer's tick event handler.  
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (counter == 0)
            {
                // Exit loop code.
                XmlWriteOnly();
                this.timer1.Tick += new System.EventHandler(this.ButtonRead_Click);
                for (int i = 0; i < 32; i++)
                {
                    score[i] = 0;
                }
                counter = 300;
            }
            else
            {
                // Run your procedure here.  
                // Increment counter.  

                this.timer1.Tick -= new System.EventHandler(this.ButtonRead_Click);
                timerTick.Text = (counter--).ToString() + "초 후";
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void ButtonUp_event(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Button)
            {
                System.Windows.Forms.Button btnEx = sender as System.Windows.Forms.Button;
                string name = btnEx.Name.ToString();
                int number = int.Parse(name.Substring(6));

                ButtonUpClick(number);
                Triangle(number - 1);
            }


        }

        private void ButtonDown_event(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Button)
            {
                System.Windows.Forms.Button btnEx = sender as System.Windows.Forms.Button;
                string name = btnEx.Name.ToString();
                int number = int.Parse(name.Substring(6));

                ButtonDownClick(63 - number);
                Triangle(62 - number);
            }
        }

        private int ButtonUpClick(int num)
        {
            return score[num - 1]++;
        }

        private int ButtonDownClick(int num)
        {
            return score[num - 1]--;
        }

        private void Triangle(int num)
        {

            if (score[num] > 0)
            {
                listBox1.Items[num] = listBox1.Items[num] = (num + 1) + "." + stuName[num] + " : " + total[num] + " +" + score[num] + " ▲";

            }
            else if (score[num] < 0)
            {

                listBox1.Items[num] = listBox1.Items[num] = (num + 1) + "." + stuName[num] + " : " + total[num] + " " + score[num] + " ▼";
            }
            else
            {


                listBox1.Items[num] = listBox1.Items[num] = (num + 1) + "." + stuName[num] + " : " + total[num] + " +" + score[num];
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButtonWrite_Click(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    classNum = "51";
                    break;
                case 1:
                    classNum = "52";
                    break;
                case 2:
                    classNum = "53";
                    break;
                case 3:
                    classNum = "54";
                    break;

                case 4:
                    classNum = "61";
                    break;
                case 5:
                    classNum = "62";
                    break;
                case 6:
                    classNum = "63";
                    break;

                default:
                    break;
            }

            string url = @".\stu" + classNum + ".xml";
            //string urlLvSetting = @".\LvSetting.xml";
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            //XDocument xdocLS = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            XElement xroot = new XElement("StudentsData");
            //XElement xLv = new XElement("levelCut");
            
            xdoc.Add(xroot);
            //xdocLS.Add(xLv);

            XElement[] xArray = new XElement[31];
           // XElement[] lvArray = new XElement[9];


            for (int i = 0; i < 31; i++)
            {


                xArray[i] = (new XElement("Student",
                     new XAttribute("id", (i + 1).ToString()),
                     new XElement("Number", (i + 1).ToString()),
                    new XElement("Name", scoreUp.stuName[i]),
                    new XElement("Total", (scoreUp.total[i] + score[i]).ToString())
                ));

                xroot.Add(xArray[i]);



            }

            /*for (int i = 0; i<9; i++)
            {
                lvArray[i] = (new XElement("levelCut",
                     new XAttribute("level", (i + 1).ToString()),
                     new XElement("cut", (30 * i).ToString())
                )); ;

                xLv.Add(lvArray[i]);
            }*/


            xdoc.Save(url);
            //xdocLS.Save(urlLvSetting);

            if (checkBox1.Checked == true)
            {

                try
                {

                    string date = DateTime.Now.ToString("MM_dd_HH_mm");
                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Columns.AddRange(new DataColumn[2] { new DataColumn("이름", typeof(string)), new DataColumn(date, typeof(int)) });



                    string startPath = Path.Combine(System.Windows.Forms.Application.StartupPath, @".\엑셀파일모음\" + classNum + "excel");
                    if (!Directory.Exists(startPath))
                    {
                        Directory.CreateDirectory(startPath);
                    }
                    string path = Path.Combine(startPath, "stu" + classNum + "_" + date + ".xlsx");
                  
                   //excelApp = new Excel.Application();
                   //workBook = excelApp.Workbooks.Add();
                   //workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;
                   //workSheet.Cells[1, 1] = "이름";
                   //workSheet.Cells[1, 2] = date;

                    List<forChart> forCharts = new List<forChart>();
                    for (int i = 0; i < 31; i++)
                    {
                        forCharts.Add(new forChart(scoreUp.stuName[i], scoreUp.total[i]+ score[i]));
                        forChart forchart1 = forCharts[i];
                        dt.Rows.Add(forchart1.name, forchart1.score);
                        //workSheet.Cells[2 + i, 1] = forchart1.name;
                        //workSheet.Cells[2 + i, 2] = forchart1.score;

                    }

                    using (XLWorkbook xLWorkbook = new XLWorkbook())
                    {
                        xLWorkbook.Worksheets.Add(dt, "Data" + date);
                        xLWorkbook.SaveAs(path);
                    }
                   // workSheet.Columns.AutoFit(); // 열 너비 자동 맞춤 
                    //workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault);

                    //workBook.SaveCopyAs(path, Excel.XlFileFormat.xlWorkbookDefault);
                    //workBook.Close(true);
                    //excelApp.Quit();

                    MessageBox.Show("엑셀 저장 성공");




                }
                catch (Exception ex)
                {
                    MessageBox.Show("실패:" + ex);
                }

                /*finally
                {
                    ReleaseObject(workSheet);
                    ReleaseObject(xLWorkBook);
                    ReleaseObject(excelApp);


                }*/
            }

            for(int i =0; i<32; i++)
            {
                score[i] = 0;
            }


        }
        static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj); // 액셀 객체 해제 
                    obj = null; }
            } catch(Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect(); // 가비지 수집
            }
        }


        private void XmlWriteOnly()
        {


            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    classNum = "51";
                    break;
                case 1:
                    classNum = "52";
                    break;
                case 2:
                    classNum = "53";
                    break;
                case 3:
                    classNum = "54";
                    break;

                case 4:
                    classNum = "61";
                    break;
                case 5:
                    classNum = "62";
                    break;
                case 6:
                    classNum = "63";
                    break;

                default:
                    break;
            }

            string url = @".\stu" + classNum + ".xml";
            //string urlLvSetting = @".\LvSetting.xml";
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            //XDocument xdocLS = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            XElement xroot = new XElement("StudentsData");
            //XElement xLv = new XElement("levelCut");

            xdoc.Add(xroot);
            //xdocLS.Add(xLv);

            XElement[] xArray = new XElement[31];
            // XElement[] lvArray = new XElement[9];


            for (int i = 0; i < 31; i++)
            {


                xArray[i] = (new XElement("Student",
                     new XAttribute("id", (i + 1).ToString()),
                     new XElement("Number", (i + 1).ToString()),
                    new XElement("Name", scoreUp.stuName[i]),
                    new XElement("Total", (scoreUp.total[i] + score[i]).ToString())
                ));

                xroot.Add(xArray[i]);



            }

            /*for (int i = 0; i<9; i++)
            {
                lvArray[i] = (new XElement("levelCut",
                     new XAttribute("level", (i + 1).ToString()),
                     new XElement("cut", (30 * i).ToString())
                )); ;

                xLv.Add(lvArray[i]);
            }*/


            xdoc.Save(url);
            //xdocLS.Save(urlLvSetting);
        }


        private void ButtonRead_Click(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    classNum = "51";
                    break;
                case 1:
                    classNum = "52";
                    break;
                case 2:
                    classNum = "53";
                    break;
                case 3:
                    classNum = "54";
                    break;

                case 4:
                    classNum = "61";
                    break;
                case 5:
                    classNum = "62";
                    break;
                case 6:
                    classNum = "63";
                    break;

                default:
                    break;
            }
            string url = @".\stu" + classNum + ".xml";

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(url);
                XmlNodeList xnList = xml.SelectNodes("StudentsData/Student"); //접근할 노드

                listBox1.Items.Clear();
                int i = 0;
                foreach (XmlNode xn in xnList)
                {
                    string name = xn["Name"].InnerText;
                    string number = xn["Number"].InnerText;
                    string total = xn["Total"].InnerText;
                    scoreUp.total[i] = int.Parse(total);
                    scoreUp.stuName[i] = name;
                    i++;

                    listBox1.Items.Add(number + "." + name + " : " + total);


                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("XML 문제 발생\r\n" + ex);
            }


        }

        class forChart
        {
            public string name;
            public double score;

            public forChart(string name, double score)
            {
                this.name = name;
                this.score = score;
            }

        }



        private void ButtonCallSave_Click(object sender, EventArgs e)
        {

        }


        private void ButtonCall_Click(object sender, EventArgs e)
        {


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void ExcelTest()
        {

            
        }

        private void buttonSortDown_Click(object sender, EventArgs e)
        {


            if (listBox1.Items.Count > 0)
            {



                int[] sortedList = new int[32];
                int[] scoreTempList = new int[32];
                int[] sortedNumber = new int[32];
                int[] numberTempList = new int[32];
                string[] listBoxTemp = new string[32];
                string[] sortedListBox = new string[32];


                for (int num = 0; num < 32; num++)
                {

                    scoreTempList[num] = scoreUp.total[num];
                    listBoxTemp[num] = scoreUp.stuName[num];
                    numberTempList[num] = num + 1;
                }


                void merge(int[] list, int left, int mid, int right)
                {

                    int i, j, k, l;
                    i = left;
                    j = mid + 1;
                    k = left;

                    while (i <= mid && j <= right)
                    {
                        if (list[i] <= list[j])
                        {
                            sortedList[k++] = list[i++];
                            sortedListBox[k++] = listBoxTemp[i++];
                            //sortedNumber[k++] = numberTempList[i++];
                        }


                        else
                        {
                            sortedList[k++] = list[j++];
                            sortedListBox[k++] = listBoxTemp[j++];
                            //sortedNumber[k++] = numberTempList[j++];
                        }
                    }

                    if (i > mid)
                    {
                        for (l = j; l <= right; l++)

                        {
                            sortedList[k++] = list[l];
                            sortedListBox[k++] = listBoxTemp[l];
                            //sortedNumber[k++] = numberTempList[l];
                        }
                    }
                    else
                    {
                        for (l = i; l <= mid; l++)
                        {
                            sortedList[k++] = list[l];
                            sortedListBox[k++] = listBoxTemp[l];
                            //sortedNumber[k++] = numberTempList[l];
                        }
                    }

                    for (l = left; l <= right; l++)
                    {
                        list[l] = sortedList[l];
                        listBoxTemp[l] = sortedListBox[l];
                        numberTempList[l] = sortedNumber[l];
                    }
                }

                void merge_sort(int[] list, int left, int right)
                {
                    int mid;
                    if (left < right)
                    {
                        mid = ((left + right) / 2);
                        merge_sort(list, left, mid);
                        merge_sort(list, mid + 1, right);
                        merge(list, left, mid, right);

                    }
                }

                merge_sort(scoreTempList, 0, 31);
            
                
            }

            }

        

    }
}

