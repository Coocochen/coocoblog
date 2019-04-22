using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    class Blog
    {
        public string blogID;
        public string blogtype;
        public string username;
        public string title;
        public string content;

        public Blog(string Blogtype,string Username, string Title ,string Content)
        {
            blogID = Guid.NewGuid().ToString();
            blogtype = Blogtype;
            username = Username;
            title = Title;
            content = Content;
        }
        public static string thisblogid;
    }
}
