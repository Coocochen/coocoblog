<?php
namespace Home\Controller;
use Think\Controller\RestController;
use Home\Model\ReplyModel;

class ReplyController extends RestController
{
    protected $allowMethod = array('get','post');
    protected $allowType=array('html','xml','json');
    public function GetReplyList($blogid)
    {
        $replyobj=new ReplyModel();
        $msg=$replyobj->where("blogID='%s'",$blogid)->count();
        if($msg>0)
        {
        $data=$replyobj->field('username,content')->where("blogID='%s'",$blogid)->select();
        $this->response($data,'json');
        }
        else
           $this->response('null');
    }
    public function Newreply()
    {
    
        $obj=json_decode($_POST["Content"]);
        $arr=array($obj);
        var_dump($arr);
        $replyid=$obj->replyID;
        $blogid=$obj->blogID;
        $userid=$obj->userID;
        $username=$obj->username;
        $content=$obj->content;
        $newreply=new ReplyModel();
        $newreply->create();
        $data['replyID']=$replyid;
        $data['blogID']=$blogid;
        $data['userID']=$userid;
        $data['username']=$username;
        $data['content']=$content;
        $sqlresult=$newreply->add($data);
        $this->response($newreply->getLastSql());
    
    }
}