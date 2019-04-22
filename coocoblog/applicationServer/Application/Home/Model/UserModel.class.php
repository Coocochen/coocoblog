<?php
namespace Home\Model;
use Think\Model;

class UserModel extends Model
{
    protected $trueTableName = 'users';
    public function __construct()
    {
        parent::__construct();
    }
}