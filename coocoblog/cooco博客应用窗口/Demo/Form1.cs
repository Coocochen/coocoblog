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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 发送GEt命令并获取返回字符串结果
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 发送Post命令并获取返回字符串结果
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>

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
        private void Form1_Load(object sender, EventArgs e)
        {

            //MessageBox.Show(a);
        }
        private void SetJsonToActorList(string jsonContent)
        {      
            jsonContent = "{\'Table1\':" + jsonContent + "}";
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonContent);
            DataTable dataTable = dataSet.Tables["Table1"];
            dataTable.Columns.Add("DisplayName", Type.GetType("System.String"));
            foreach (DataRow Row in dataTable.Rows)
                Row["DisplayName"] = Row["last_name"].ToString() + " " + Row["first_name"].ToString();
            cmbboxBlog.DisplayMember = "DisplayName";
            cmbboxBlog.ValueMember = "actor_id";
            cmbboxBlog.DataSource = dataTable.DefaultView;
        }

        private void cmbboxActor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SetJsonToFileList(string jsonContent)
        {
            jsonContent = "{\'Table1\':" + jsonContent + "}";
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonContent);
            DataTable dataTable = dataSet.Tables["Table1"];
            dataTable.Columns.Add("DisplayName", Type.GetType("System.String"));
            dgvFileList.DataSource = dataTable.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbboxBlog.Text.Length == 0)
            {
                MessageBox.Show("请选择种类");
            }
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入标题");
            }
            if (richTextBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入内容");
            }
            else
            {
                Blog newblog = new Blog(cmbboxBlog.Text, User.logname, textBox1.Text, richTextBox1.Text);
                string jsonResult = JsonConvert.SerializeObject(newblog);

                /*List<Actor> newactorList = new List<Actor>();
                for (int i = 0; i < 5; i++)
                {
                    Actor newactor = new Actor(txtFirstName.Text+"_"+i.ToString(), txtLastName.Text);
                    newactorList.Add(newactor);
                }
                string jsonResult = JsonConvert.SerializeObject(newactorList);*/
                string PostURL = "http://localhost/applicationServer/Home/Blog/Newblog";
                string ParamString = "Content=" + jsonResult;
                string result = HttpPost(PostURL, ParamString);
                MessageBox.Show("发表成功!");
                Form3 newfrm = new Form3();
                newfrm.Show();
                this.Visible = false;
            }
        }
        private void dgvFileList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 newfrm = new Form3();
            newfrm.Show();
            this.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}