/*==============================================================*/
/* Database name:  Database_1                                   */
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018/10/22 20:05:44                          */
/*==============================================================*/


drop database if exists Database_1;

/*==============================================================*/
/* Database: Database_1                                         */
/*==============================================================*/
create database Database_1;

use Database_1;

/*==============================================================*/
/* Table: blog                                                  */
/*==============================================================*/
create table blog
(
   blogID               varchar(50) not null,
   blogtype           nvarchar(50),
   userID               varchar(50),
   username             varchar(30),
   title               nvarchar(50),
   content              nvarchar(500),
   primary key (blogID)
);
INSERT INTO `blog` VALUES ('2','心情','','lala','嘻嘻嘻','哈哈哈哈哈哈哈');
/*==============================================================*/

/*==============================================================*/
/* Table: reply                                                 */
/*==============================================================*/
create table reply
(
   replyID             varchar(50) not null,
   blogID               varchar(50),
   userID               varchar(50),
   username             varchar(30),
   content              nvarchar(100),
   primary key (replyID)
);
/*INSERT INTO `reply` VALUES (1,1,1,'chenhuan','lalalallalal');
/*==============================================================*/
/* Table: users                                                 */
/*==============================================================*/
create table users
(
   userID               varchar(50) not null,
   username             varchar(30)  not null,
   passwords            varchar(30),
   sex                  varchar(10),
   introduction        nvarchar(50),
   primary key (userID,username)
);
INSERT INTO `users` VALUES ('1','ccc','ccc12d8113','woman','每一天都是温柔的日子~'),('2','lala','hehehehe','woman','每一天都是温柔的日子~');


alter table blog add constraint FK_fk_user_blog foreign key (userID, username)
      references users (userID, username) on delete restrict on update restrict;

alter table reply add constraint FK_fk_blog_coment foreign key (blogID)
      references blog (blogID) on delete restrict on update restrict;

alter table reply add constraint FK_fk_user_coment foreign key (userID, username)
      references users (userID, username) on delete restrict on update restrict;

