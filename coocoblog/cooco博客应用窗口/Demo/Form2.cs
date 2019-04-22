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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();

        }
        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
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
         private void button1_Click(object sender, EventArgs e)
         {
             if (textBox1.Text.Length == 0)
             {
                 MessageBox.Show("请输入用户名");
             }
             else
             {
                 string URL = "http://localhost/applicationServer/Home/User/isReg/" + textBox1.Text;
                 string Param = string.Format("");
                 string a = HttpGet(URL, Param);
                 //MessageBox.Show(a);
                 if (a == "1")
                 {
                     MessageBox.Show("用户名已注册");
                 }
                 else
                 {
                     User newuser = new User(textBox1.Text, textBox2.Text, comboBox1.Text);
                     string jsonResult = JsonConvert.SerializeObject(newuser);
                     string PostURL = "http://localhost/applicationServer/Home/User/NewUser";
                     string ParamString = "Content=" + jsonResult;
                     string result = HttpPost(PostURL, ParamString);
                     MessageBox.Show("注册成功！");
                 }
             }
         }

        private void button2_Click(object sender, EventArgs e)
        {
            login newfrm = new login();
            newfrm.Show();
            this.Visible = false;
        }
    }
}
