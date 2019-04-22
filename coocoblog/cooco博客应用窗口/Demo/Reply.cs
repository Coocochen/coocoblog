using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    class Reply
    {
        public string replyID;
        public string blogID;
        public string username;
        public string content;

        public Reply( string Blogid,string Username, string Content)
        {
            replyID = Guid.NewGuid().ToString();
            username = Username;
            blogID = Blogid;
            content = Content;
        }
        public static string flag = "0";
    }
}
