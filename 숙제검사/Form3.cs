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

namespace 숙제검사
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            for (int i = 0; i < 9; i++)
            {
                var tb = this.Controls.Find("textBox" + (i + 1).ToString(), true).FirstOrDefault();
                if (tb != null)
                {
                    this.Controls.Find("textBox" + (i + 1).ToString(), true).FirstOrDefault().Text = Form1.levelArray[i].ToString();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCommit_Click(object sender, EventArgs e)
        {

            string urlLvSetting = @".\LvSetting.xml";
            XDocument xdocLS = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            XElement xLv = new XElement("levelCut");


            xdocLS.Add(xLv);

            void parse(int num)
            {


                XElement[] lvArray = new XElement[9];

                lvArray[num] = (new XElement("levelCut",
                         new XAttribute("level", (num + 1).ToString()),
                         new XElement("cut", this.Controls.Find("textBox" + (num + 1).ToString(), true).FirstOrDefault().Text)
                    )); ;

                xLv.Add(lvArray[num]);
                Form1.levelArray[num] = int.Parse(this.Controls.Find("textBox" + (num + 1).ToString(), true).FirstOrDefault().Text);

            }

            for (int i=0; i<9; i++)
            {
                var tb = this.Controls.Find("textBox" + (i + 1).ToString(), true).FirstOrDefault();
                if(tb != null)
                {
                    int tmpNum = int.Parse(this.Controls.Find("textBox" + (i + 1).ToString(), true).FirstOrDefault().Text);
                    

                    if (i != 0 )
                    {
                        int compNum = int.Parse(this.Controls.Find("textBox" + (i).ToString(), true).FirstOrDefault().Text);
                        if (compNum >= tmpNum)
                        {
                            MessageBox.Show("잘못된 입력입니다.");
                            return;
                        }
                        else
                        {
                            parse(i);
                        }
                    }
                    else
                    {

                        parse(i);
                    }
                }
            }


            xdocLS.Save(urlLvSetting);

            MessageBox.Show("레벨 저장 성공!");

        }

      
    }
}
