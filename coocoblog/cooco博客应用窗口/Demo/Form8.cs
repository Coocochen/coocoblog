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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        private void Form8_Load(object sender, EventArgs e)
        {
            string URL = "http://localhost/applicationServer/Home/Reply/GetReplyList/" + Blog.thisblogid;
            string Param = string.Format("");
            string a = HttpGet(URL, Param);
            string jsonContent = a;
            if (a == "null")
            {
                listBox1.Items.Add("还没有评论");
            }
            else
            {
                jsonContent = "{\'Table1\':" + jsonContent + "}";
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonContent);
                DataTable dataTable = dataSet.Tables["Table1"];
                dataTable.Columns.Add("DisplayName", Type.GetType("System.String"));
                foreach (DataRow Row in dataTable.Rows)
                    Row["DisplayName"] = Row["username"].ToString() + " : " + Row["content"].ToString();
                listBox1.DisplayMember = "DisplayName";
                listBox1.ValueMember = "replyID";
                listBox1.DataSource = dataTable.DefaultView;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入评论");
            }
            else
            {
                Reply reply = new Reply(Blog.thisblogid,User.logname,  richTextBox1.Text);
                string jsonResult = JsonConvert.SerializeObject(reply);

                /*List<Actor> newactorList = new List<Actor>();
                for (int i = 0; i < 5; i++)
                {
                    Actor newactor = new Actor(txtFirstName.Text+"_"+i.ToString(), txtLastName.Text);
                    newactorList.Add(newactor);
                }
                string jsonResult = JsonConvert.SerializeObject(newactorList);*/
                string PostURL = "http://localhost/applicationServer/Home/Reply/Newreply";
                string ParamString = "Content=" + jsonResult;
                string result = HttpPost(PostURL, ParamString);
                string URL = "http://localhost/applicationServer/Home/Reply/GetReplyList/" + Blog.thisblogid;
                string Param = string.Format("");
                string a = HttpGet(URL, Param);
                string jsonContent = a;
                if (a == "null")
                {
                    listBox1.Items.Add("还没有评论");
                }
                else
                {
                    jsonContent = "{\'Table1\':" + jsonContent + "}";
                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonContent);
                    DataTable dataTable = dataSet.Tables["Table1"];
                    dataTable.Columns.Add("DisplayName", Type.GetType("System.String"));
                    foreach (DataRow Row in dataTable.Rows)
                        Row["DisplayName"] = Row["username"].ToString() + ":  " + Row["content"].ToString();
                    listBox1.DisplayMember = "DisplayName";
                    listBox1.ValueMember = "replyID";
                    listBox1.DataSource = dataTable.DefaultView;
                 
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Reply.flag == "1")
            {
                Form7 newfrm = new Form7();
                newfrm.Show();
                this.Visible = false;
            }
            else
            {
                Form4 newfrm = new Form4();
                newfrm.Show();
                this.Visible = false;

            }
        }
    }
}
