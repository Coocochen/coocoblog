using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Demo
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = User.logname;
            string URL = "http://localhost/applicationServer/Home/User/Getintro/" + User.logname;
            string Param = string.Format("");
            string a = HttpGet(URL, Param);
            if (a.Length == 0)
            {

            }
            else
            {
                string jsonContent = a;
                jsonContent = "{\'Table1\':" + jsonContent + "}";
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonContent);
                DataTable dataTable = dataSet.Tables["Table1"];
                dataTable.Columns.Add("DisplayName", Type.GetType("System.String"));
                foreach (DataRow Row in dataTable.Rows)
                    label4.Text = Row["introduction"].ToString();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 newfrm = new Form1();
            newfrm.Show();
            this.Visible = false;
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            Form4 newfrm = new Form4();
            newfrm.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            login newfrm = new login();
            newfrm.Show();
            this.Visible = false;
        }
        public static string HttpPost(string url, string postDataStr)
        {
            string strReturn;
            //在转换字节时指定编码格式
            byte[] byteData = Encoding.UTF8.GetBytes(postDataStr);

            //配置Http协议头
            HttpWebRequest resquest = (HttpWebRequest)WebRequest.Create(url);
            resquest.Method = "POST";
            resquest.ContentType = "application/x-www-form-urlencoded";
            resquest.ContentLength = byteData.Length;

            //发送数据
            using (Stream resquestStream = resquest.GetRequestStream())
            {
                resquestStream.Write(byteData, 0, byteData.Length);
            }

            //接受并解析信息
            using (WebResponse response = resquest.GetResponse())
            {
                //解决乱码：utf-8 + streamreader.readToEnd
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                strReturn = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
            }

            return strReturn;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 newfrm = new Form5();
            newfrm.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 newfrm = new Form6();
            newfrm.Show();
            this.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
