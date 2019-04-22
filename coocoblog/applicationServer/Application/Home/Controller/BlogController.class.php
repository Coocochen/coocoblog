<?php
namespace Home\Controller;
use Think\Controller\RestController;
use Home\Model\BlogModel;

class blogController extends RestController
{
    protected $allowMethod = array('get','post');
    protected $allowType=array('html','xml','json');
    public function GetBlogList($username)
    {
        $blogobj=new BlogModel();
        $data=$blogobj->where("username='%s'",$username)->order('title')->select();
        $this->response($data,'json');
    }
    
    public function Newblog()
    {

        $obj=json_decode($_POST["Content"]);
        $arr=array($obj);
        var_dump($arr);
        $blogid=$obj->blogID;
        $blogtype=$obj->blogtype;
        $userid=$obj->userid;
        $username=$obj->username;
        $title=$obj->title;
        $content=$obj->content;
        $newblog=new BlogModel();
        $newblog->create();
        $data['blogID']=$blogid;
        $data['blogtype']=$blogtype;
        $data['userID']=$userid;
        $data['username']=$username;
        $data['title']=$title;
        $data['content']=$content;
        $sqlresult=$newblog->add($data);
        $this->response($newblog->getLastSql());

    }
    public function GetContentOfBlogID($blogid)
    {
        $blogobj=new BlogModel();
        $data=$blogobj->where("blogID='%s'",$blogid)->order('content')->select();
        $this->response($data,'json');
    }
    public function DeleteContentOfBlogID($blogid)
    {
        $blogobj=new BlogModel();
        $data=$blogobj->where("blogID='%s'",$blogid)->limit('1')->delete();
        $this->response($data);
    }
    public function GetTitleAndUsername($title)
    {
        $blogobj=new BlogModel();
        $data=$blogobj->field('title,username')->where("title='%s'",$title)->count();
        if($data>0)
        { 
          $msg=$blogobj->field('blogID,title,username')->where("title='%s'",$title)->limit('3')->select();
          $this->response($msg,'json');
        }
        else
        $this->response('Null');
    }
    public function GetBlog($blogid)
    {
        $blogobj=new BlogModel();
        $data=$blogobj->where("blogID='%s'",$blogid)->select();
        $this->response($data,'json');
    }

}