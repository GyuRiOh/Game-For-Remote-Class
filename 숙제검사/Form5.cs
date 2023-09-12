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
    public partial class Form5 : Form
    {
        
        static List<int> numList = new List<int>();
        static List<int> randomList = new List<int>();
        public Form5()
        {
            InitializeComponent();
            numList.Clear();
        }

        private int randomFunction(int a, int b)
        {
            Random random = new Random();            
            return random.Next(a, b);
        }

        private void recursive()
        {


            Random random = new Random();
            if (numList.Count == 0)
                return;
            

            int index = random.Next(0, numList.Count);
            randomList.Add(numList[index]);
            numList.RemoveAt(index);
            recursive();
            


        }

        private void buttonMix_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 31; i++)
            {
                numList.Add(i + 1);
            }
            randomList.Clear();

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
            listBox9.Items.Clear();
            listBox10.Items.Clear();
            listBox11.Items.Clear();
            int listIndex = 1;

            recursive();


            int j = 1;
            
            for(int i = 0; i<31; i++)
            {

                switch(listIndex)
                {
                    case 1:

                        listBox1.Items.Add(randomList[i]);

                        break;

                    case 2:

                        listBox2.Items.Add(randomList[i]);
                        break;
                    case 3:


                        listBox3.Items.Add(randomList[i]);
                        break;
                    case 4:

                        listBox4.Items.Add(randomList[i]);
                        break;
                    case 5:

                        listBox5.Items.Add(randomList[i]);
                        break;
                    case 6:

                        listBox6.Items.Add(randomList[i]);
                        break;
                    case 7:

                        listBox7.Items.Add(randomList[i]);
                        break;
                    case 8:

                        listBox8.Items.Add(randomList[i]);
                        break;
                    case 9:

                        listBox9.Items.Add(randomList[i]);
                        break;

                    case 10:

                        listBox10.Items.Add(randomList[i]);
                        break;

                    case 11:

                        listBox11.Items.Add(randomList[i]);
                        break;

                    default:
                        break;
                }


                if (j==3)
                {
                    listIndex++;
                    j = 0;
                }


                j++;

            }



        }
    }
}
