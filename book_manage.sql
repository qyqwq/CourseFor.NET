/*
Navicat MySQL Data Transfer

Source Server         : mysql
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : book_manage

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2019-06-19 16:26:04
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for book
-- ----------------------------
DROP TABLE IF EXISTS `book`;
CREATE TABLE `book` (
  `book_id` varchar(255) NOT NULL COMMENT '书id',
  `book_name` varchar(255) NOT NULL COMMENT '书名',
  `book_author` varchar(255) NOT NULL COMMENT '作者',
  `book_status` varchar(255) NOT NULL COMMENT '书状态',
  `lend_time` int(20) DEFAULT NULL COMMENT '出借时间（当书被出借时）',
  `back_time` int(20) DEFAULT NULL COMMENT '应还书时间',
  PRIMARY KEY (`book_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of book
-- ----------------------------
INSERT INTO `book` VALUES ('9787111187776', '算法导论 原书第二版', 'Thomas H. Cormen', '114514', '1560929804', '1566113804');
INSERT INTO `book` VALUES ('9787111377870', '组合数学原书第五版', '威斯康星大学麦迪逊分校', '114514', '1560929805', '1566113805');
INSERT INTO `book` VALUES ('9787302078005', '算法艺术与信息学竞赛', '刘汝佳', '114514', '1560929805', '1566113805');
INSERT INTO `book` VALUES ('9787302356288', '算法竞赛入门经典第二版', '刘汝佳', 'ok', '1560929016', '1566113016');
INSERT INTO `book` VALUES ('9787302465102', 'Visual C#.NET程序设计第二版', '刘秋香', 'ok', '0', '0');
INSERT INTO `book` VALUES ('9787512401136', 'Android程序设计', '柯元旦', 'ok', '0', '0');

-- ----------------------------
-- Table structure for usr
-- ----------------------------
DROP TABLE IF EXISTS `usr`;
CREATE TABLE `usr` (
  `user_id` varchar(255) NOT NULL COMMENT '账号',
  `user_pwd` varchar(255) NOT NULL COMMENT '密码',
  `user_name` varchar(255) NOT NULL COMMENT '昵称',
  `user_type` varchar(255) NOT NULL COMMENT '账号类型',
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of usr
-- ----------------------------
INSERT INTO `usr` VALUES ('114514', '114514', '吾乐', 'usr');
INSERT INTO `usr` VALUES ('1608170231', '1608170231', '陈起', 'usr');
INSERT INTO `usr` VALUES ('1608170235', '1608170235', '潘铭辉', 'usr');
INSERT INTO `usr` VALUES ('admin', 'admin', 'admin', 'rot');
