using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Xml.Linq;

namespace 숙제검사
{
    public class DataManage
    {

        public class stuData
        {
            public string name;
            public double total;

            public stuData(string name, double total)
            {
                this.name = name;
                this.total = total;
            }

        }

        public static List<stuData> tempStuData = new List<stuData>();

        /*public MySqlConnection connection = new MySqlConnection("Server=localhost;Database=studata_db;Uid=root;Pwd=s5t9a2r0960724;");

        public void input()
        {

            connection.Open();
            string insertQuery = "insert into students51 values ('홍길동', 1, 0)";

            for (int i = 0; i<24; i++)
            {

                MySqlCommand command = new MySqlCommand(insertQuery, connection);

                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("정상적!");
                    }
                    else
                    {

                        MessageBox.Show("비정상!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }












            }
            connection.Close();
        }*/

    }

}
