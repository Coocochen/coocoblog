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
    public partial class Form4 : Form
    {
        public Form4()
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
        private void Form4_Load(object sender, EventArgs e)
        {
            label2.Text = User.logname;
            string URL = "http://localhost/applicationServer/Home/Blog/GetBlogList/" + label2.Text;
                string Param = string.Format("");
                string a = HttpGet(URL, Param);
            if (a.Length == 0)
            {

            }
            else
                SetJsonToBlogList(a);
        }
        private void SetJsonToBlogList(string jsonContent)
        {
            jsonContent = "{\'Table1\':" + jsonContent + "}";
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonContent);
            DataTable dataTable = dataSet.Tables["Table1"];
            dataTable.Columns.Add("DisplayName", Type.GetType("System.String"));
            foreach (DataRow Row in dataTable.Rows)
                Row["DisplayName"] = Row["title"].ToString();
            comboBox1.DisplayMember = "DisplayName";
            comboBox1.ValueMember = "blogID";
            comboBox1.DataSource = dataTable.DefaultView;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string URL = "http://localhost/applicationServer/Home/Blog/GetContentOfBlogID/" + comboBox1.SelectedValue.ToString();
            string Param = string.Format("");
            string a = HttpGet(URL, Param);
            string jsonContent = a;
            jsonContent = "{\'Table1\':" + jsonContent + "}";
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonContent);
            DataTable dataTable = dataSet.Tables["Table1"];
            dataTable.Columns.Add("DisplayName", Type.GetType("System.String"));
            foreach (DataRow Row in dataTable.Rows)
            {
                label3.Text = Row["content"].ToString();
                Blog.thisblogid = Row["blogID"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form3 newfrm = new Form3();
            newfrm.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string URL = "http://localhost/applicationServer/Home/Blog/DeleteContentOfBlogID/" + comboBox1.SelectedValue.ToString();
            string Param = string.Format("");
            string a = HttpGet(URL, Param);
            if (a.Length == 0)
            {
                MessageBox.Show("删除成功!");
            }
            Form3 newfrm = new Form3();
            newfrm.Show();
            this.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reply.flag = "0";
            Form8 newfrm = new Form8();
            newfrm.Show();
            this.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
