using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 숙제검사
{
    public partial class Form4 : Form
    { 

        string classNum;

        public Form4()
        {
            InitializeComponent();

            
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnCommit_Click(object sender, EventArgs e)
        {



        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
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


        }
    }
}
