<?php
namespace Home\Model;
use Think\Model;

class ReplyModel extends Model
{
    protected $trueTableName = 'reply';
    public function __construct()
    {
        parent::__construct();
    }
}