/*
Navicat MySQL Data Transfer

Source Server         : xiao_ming
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : book_manage

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2019-05-21 21:41:22
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for book
-- ----------------------------
DROP TABLE IF EXISTS `book`;
CREATE TABLE `book` (
  `book_id` varchar(255) NOT NULL,
  `book_name` varchar(255) NOT NULL,
  `book_author` varchar(255) NOT NULL,
  `book_status` varchar(255) NOT NULL,
  PRIMARY KEY (`book_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of book
-- ----------------------------
INSERT INTO `book` VALUES ('9787111187776', '算法导论 原书第二版', 'Thomas H. Cormen', 'ok');
INSERT INTO `book` VALUES ('9787111377870', '组合数学原书第五版', '威斯康星大学麦迪逊分校', '1608170235');
INSERT INTO `book` VALUES ('9787302078005', '算法艺术与信息学竞赛', '刘汝佳', '1608170231');
INSERT INTO `book` VALUES ('9787302356288', '算法竞赛入门经典第二版', '刘汝佳', '1608170231');
INSERT INTO `book` VALUES ('9787302465102', 'Visual C#.NET程序设计第二版', '刘秋香', 'ok');
INSERT INTO `book` VALUES ('9787512401136', 'Android程序设计', '柯元旦', '1608170235');

-- ----------------------------
-- Table structure for usr
-- ----------------------------
DROP TABLE IF EXISTS `usr`;
CREATE TABLE `usr` (
  `user_id` varchar(255) NOT NULL,
  `user_pwd` varchar(255) NOT NULL,
  `user_name` varchar(255) NOT NULL,
  `user_type` varchar(255) NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of usr
-- ----------------------------
INSERT INTO `usr` VALUES ('1608170231', '1608170231', '陈起', 'usr');
INSERT INTO `usr` VALUES ('1608170235', '1608170235', '潘铭辉', 'usr');
INSERT INTO `usr` VALUES ('admin', 'admin', 'admin', 'rot');
