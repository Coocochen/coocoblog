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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 newfrm = new Form3();
            newfrm.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form3 newfrm = new Form3();
            newfrm.Show();
            this.Visible = false;
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
            string URL = "http://localhost/applicationServer/Home/Blog/GetTitleAndUsername/" + textBox1.Text;
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入标题");
            }
            else
            {
                string Param = string.Format("");
                string a = HttpGet(URL, Param);
                string jsonContent = a;
                if (jsonContent == "Null")
                {
                    MessageBox.Show("没有相关内容");
                }
                else
                {
                    jsonContent = "{\'Table1\':" + jsonContent + "}";
                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonContent);
                    DataTable dataTable = dataSet.Tables["Table1"];
                    dataTable.Columns.Add("DisplayName", Type.GetType("System.String"));
                    int flag = 0;
                    foreach (DataRow Row in dataTable.Rows)
                    {
                        if (flag == 0)
                        {
                            linkLabel1.Text = Row["username"].ToString() + " : " + Row["title"].ToString();
                            linkLabel1.Tag = Row["blogID"].ToString();
                        }

                        if (flag == 1)
                        {
                            linkLabel2.Text = Row["username"].ToString() + " : " + Row["title"].ToString();
                            linkLabel2.Tag = Row["blogID"].ToString();
                        }
                        if (flag == 2)
                        {
                            linkLabel2.Text = Row["username"].ToString() + " : " + Row["title"].ToString();
                            linkLabel2.Tag = Row["blogID"].ToString();
                        }
                        flag++;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Blog.thisblogid = linkLabel1.Tag.ToString();
            Form7 newfrm = new Form7();
            newfrm.Show();
            this.Visible = false;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
         
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Blog.thisblogid = linkLabel3.Tag.ToString();
            Form7 newfrm = new Form7();
            newfrm.Show();
            this.Visible = false;
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Blog.thisblogid = linkLabel2.Tag.ToString();
            Form7 newfrm = new Form7();
            newfrm.Show();
            this.Visible = false;
        }
    }
}
