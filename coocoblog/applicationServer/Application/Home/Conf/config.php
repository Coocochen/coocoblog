<?php
return array(
	//'配置项'=>'配置值'

    // 开启路由
    'URL_ROUTER_ON' => true ,
    'URL_ROUTE_RULES'=>array(
        ///以下是Get方法 
        array('User/login/:username/:passwords','User/login','',array('method'=>'get')),
        array('Blog/GetBlogList/:username','Blog/GetBlogList','',array('method'=>'get')),
        array('Blog/GetContentOfBlogID/:blogid','Blog/GetContentOfBlogID','',array('method'=>'get')),
        array('Blog/DeleteContentOfBlogID/:blogid','Blog/DeleteContentOfBlogid','',array('method'=>'get')),
        array('User/Newintro/:username/:introduction','User/Newintro','',array('method'=>'get')),
        array('User/Getintro/:username','User/Getintro','',array('method'=>'get')),
        array('User/Deleteintro/:username','User/Deleteintro','',array('method'=>'get')),
        array('Blog/GetTitleAndUsername/:title','Blog/GetTitleAndUsername','',array('method'=>'get')),
        array('Blog/GetBlog/:blogid','Blog/GetBlog','',array('method'=>'get')),
        array('Reply/GetReplyList/:blogid','Reply/GetReplyList','',array('method'=>'get')),
        array('User/isReg/:username','User/isReg','',array('method'=>'get')),
        ///以下是Post方法
        array('User','User/NewUser','',array('method'=>'post')), 
        array('Blog','Blog/Newblog','',array('method'=>'post')),
        array('Reply','Reply/Newreply','',array('method'=>'post')),
      
    ),
);