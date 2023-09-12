using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Xml.Linq;


namespace 숙제검사
{
    public partial class Form1 : Form
    {       
        System.Windows.Forms.PictureBox picBox;
        Random random = new Random();
        string classNum;
        public static int[] levelArray = new int[9];


        public Form1()
        {

            Start();
            


            /*DataManage dataManage = new DataManage();
            dataManage.input();*/


        }

        public void Start()
        {
            InitializeComponent();
            pictureBox1.Image = Properties.Resources.숙제검사배경2;
            pictureBox1.SendToBack();
            pictureBox2.SendToBack();
            label2.SendToBack();

            string url = @".\LvSetting.xml";

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(url);
                XmlNodeList xnList = xml.SelectNodes("levelCut/levelCut");

                int i = 0;
                foreach (XmlNode xn in xnList)
                {
                    string level = (i + 1).ToString();
                    string cut = xn["cut"].InnerText;
                    //DataManage.tempStuData[i].total = double.Parse(total);
                    //DataManage.tempStuData[i].name = name;
                    levelArray[i] = int.Parse(cut);
                    i++;



                }

                MessageBox.Show("xml 읽기 성공!");

            }
            catch (Exception ex)
            {

                MessageBox.Show("XML 문제 발생\r\n" + ex);
            }

            for (int i = 0; i < 31; i++)
            {
                DataManage.tempStuData.Add(new DataManage.stuData("길동이", 0));
            }


        }



        private void label_Click(object sender, EventArgs e)
        {//이름 클릭시 메시지박스 등장
            MessageBox.Show("생성");
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MakeIcon()
        {

            
        }

        //엑셀이랑 연동시키기

        private void Button2_Click(object sender, EventArgs e)
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
                XmlNodeList xnList = xml.SelectNodes("StudentsData/Student");
                 
                int i= 0;
                foreach (XmlNode xn in xnList)
                {
                    string name = xn["Name"].InnerText;
                    string number = xn["Number"].InnerText;
                    string total = xn["Total"].InnerText;
                    DataManage.tempStuData[i].total = double.Parse(total);
                    DataManage.tempStuData[i].name = name;
                    i++;



                }

                MessageBox.Show("xml 읽기 성공!");

            }
            catch(Exception ex)
            {

                MessageBox.Show("XML 문제 발생\r\n" + ex);
            }


            double scoreTotal = 0;




            //

            for (int i = 0; i < 31; i++)
            {
                System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                label.Location = new System.Drawing.Point(150 + i *45, 535);
                label.Name = "label" + (i + 30).ToString();
                label.Size = new System.Drawing.Size(45, 20);
                label.Font = new System.Drawing.Font(label3.Font.Name, label2.Font.Size);
                label.TabIndex = i;
                label.Text = DataManage.tempStuData[i].name + (i+1).ToString();
                label.ForeColor = Color.Black;
                label.Click += new System.EventHandler(this. label_Click);
                this.Controls.Add(label);

                label.Show();
                label.BringToFront();


                void makePic(int height, System.Drawing.Bitmap bmp)
                {
                    picBox = new System.Windows.Forms.PictureBox();
                    picBox.Name = "pb" + i.ToString();
                    picBox.Image= bmp;
                    picBox.Left = 40;
                    picBox.Top = 40;
                    picBox.Width = 40;
                    picBox.Height = 40;
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    picBox.Location = new System.Drawing.Point(150 + i * 45, height);
                    this.Controls.Add(picBox);


                    picBox.BringToFront();
                }

                void showScore(int height)
                {
                    System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
                    label2.Location = new System.Drawing.Point(155 + i * 45, height);
                    label2.Name = "score" + (i + 30).ToString();
                    label2.Size = new System.Drawing.Size(45, 20);
                    label2.Font = new System.Drawing.Font(label3.Font.Name, label2.Font.Size+2.0f);
                    label2.TabIndex = i;
                    label2.Text = DataManage.tempStuData[i].total.ToString();
                    label2.ForeColor = Color.Black;
                    label2.Click += new System.EventHandler(this.label_Click);
                    this.Controls.Add(label2);

                    label2.Show();
                    label2.BringToFront();
                }

                double level = DataManage.tempStuData[i].total;
                double finalLevel = 0;

                for (int j =0; j<9;j++)
                {
                    if (level >= levelArray[j])
                        finalLevel = j+1;
                }
                


                switch (finalLevel)
                {
                    case -1:
                        showScore(490);
                        break;

                    case 1:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        showScore(460);
                        break;

                    case 2:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        makePic(450, Properties.Resources.레벨2_색칠3);
                        showScore(420);
                        break;

                    case 3:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        makePic(450, Properties.Resources.레벨2_색칠3);
                        makePic(410, Properties.Resources.레벨3);
                        showScore(380);
                        break;

                    case 4:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        makePic(450, Properties.Resources.레벨2_색칠3);
                        makePic(410, Properties.Resources.레벨3);
                        makePic(370, Properties.Resources.레벨4);
                        showScore(340);
                        break;
                    case 5:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        makePic(450, Properties.Resources.레벨2_색칠3);
                        makePic(410, Properties.Resources.레벨3);
                        makePic(370, Properties.Resources.레벨4);
                        makePic(330, Properties.Resources.레벨5);
                        showScore(300);
                        break;
                    case 6:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        makePic(450, Properties.Resources.레벨2_색칠3);
                        makePic(410, Properties.Resources.레벨3);
                        makePic(370, Properties.Resources.레벨4);
                        makePic(330, Properties.Resources.레벨5);
                        makePic(290, Properties.Resources.레벨6);
                        showScore(260);
                        break;
                    case 7:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        makePic(450, Properties.Resources.레벨2_색칠3);
                        makePic(410, Properties.Resources.레벨3);
                        makePic(370, Properties.Resources.레벨4);
                        makePic(330, Properties.Resources.레벨5);
                        makePic(290, Properties.Resources.레벨6);
                        makePic(250, Properties.Resources.레벨7);
                        showScore(220);
                        break;
                    case 8:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        makePic(450, Properties.Resources.레벨2_색칠3);
                        makePic(410, Properties.Resources.레벨3);
                        makePic(370, Properties.Resources.레벨4);
                        makePic(330, Properties.Resources.레벨5);
                        makePic(290, Properties.Resources.레벨6);
                        makePic(250, Properties.Resources.레벨7);
                        makePic(210, Properties.Resources.레벨8);
                        showScore(180);
                        break;

                    case 9:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        makePic(450, Properties.Resources.레벨2_색칠3);
                        makePic(410, Properties.Resources.레벨3);
                        makePic(370, Properties.Resources.레벨4);
                        makePic(330, Properties.Resources.레벨5);
                        makePic(290, Properties.Resources.레벨6);
                        makePic(250, Properties.Resources.레벨7);
                        makePic(210, Properties.Resources.레벨8);
                        makePic(170, Properties.Resources.레벨9);
                        showScore(140);
                        break;

                    default:
                        makePic(490, Properties.Resources.레벨1_색칠3);
                        showScore(460);
                        break;
                }


                scoreTotal += DataManage.tempStuData[i].total;


            }

            label4.Text = scoreTotal.ToString();



            MessageBox.Show("성공, 전체합 : "+scoreTotal);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnClassName_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            scoreUp form2 = new scoreUp();
            form2.Show();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {

            Form3 form3 = new Form3();
            form3.Show();
        }

        private void buttonSit_Click(object sender, EventArgs e)
        {

            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}



