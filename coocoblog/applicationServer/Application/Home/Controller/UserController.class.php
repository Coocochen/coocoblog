<?php
namespace Home\Controller;
use Think\Controller\RestController;
use Home\Model\UserModel;

class UserController extends RestController
{
    protected $allowMethod = array('get','post');
    protected $allowType=array('html','xml','json');
    
    public function NewUser()
    {

        $obj=json_decode($_POST["Content"]);
        $arr=array($obj);
        var_dump($arr);
        $userid=$obj->userID;
        $username=$obj->username;
        $passwords=$obj->passwords;
        $sex=$obj->sex;
        $newUser=new UserModel();
        $newUser->create();
        $data['userID']=$userid;
        $data['username']=$username;
        $data['passwords']=$passwords;
        $data['sex']=$sex;
        $sqlresult=$newUser->add($data);
        $this->response($newUser->getLastSql());

    }

       public function Login($username,$passwords)
       {

           $user=new UserModel();
           $msg =$user->where("username='%s' and passwords='%s'", $username, $passwords)->count()>0? 'success':'fail';
           $this->response($msg);
           
       }
       public function Newintro($username,$introduction)
       {
           $user=new UserModel();
           $data=array('introduction'=>$introduction);
           $msg=$user->data($data)->where("username='%s'",$username)->save();
           $this->response($msg);
       }
       public function Getintro($username)
       {
           $obj=new UserModel();
           $data=$obj->where("username='%s'",$username)->select();
           $this->response($data,'json');
       }
       public function Deleteintro($username)
       {
           $user=new UserModel();
           $data=array('introduction'=>'');
           $msg=$user->data($data)->where("username='%s'",$username)->save();
           $this->response($msg);
       }
       public function isReg($username)
       {
           $user=new UserModel();
           $msg=$user->where("username='%s'",$username)->count()>0?'1':'0';
           $this->response($msg);
       }

}