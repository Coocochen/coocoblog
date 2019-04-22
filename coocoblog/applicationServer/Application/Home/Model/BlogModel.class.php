<?php
namespace Home\Model;
use Think\Model;

class BlogModel extends Model
{
    protected $trueTableName = 'blog';
    public function __construct()
    {
        parent::__construct();
    }
}