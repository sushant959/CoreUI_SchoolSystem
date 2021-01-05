/*
SQLyog Enterprise - MySQL GUI v8.14 
MySQL - 5.7.21-log : Database - test
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`test` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `test`;

/*Table structure for table `tbladvancedpaid` */

DROP TABLE IF EXISTS `tbladvancedpaid`;

CREATE TABLE `tbladvancedpaid` (
  `AdvancedPaidId` int(11) NOT NULL AUTO_INCREMENT,
  `StudentId` varchar(50) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  `ReceiptId` int(11) DEFAULT NULL,
  PRIMARY KEY (`AdvancedPaidId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `tbladvancedpaid` */

insert  into `tbladvancedpaid`(`AdvancedPaidId`,`StudentId`,`Amount`,`Status`,`CompanyId`,`ReceiptId`) values (1,'2021-3',250,'Used',1,5),(2,'2021-3',35,'Not-Used',1,8),(3,'20201-1',400,'Used',1,3);

/*Table structure for table `tblbatchclass` */

DROP TABLE IF EXISTS `tblbatchclass`;

CREATE TABLE `tblbatchclass` (
  `BatchClassId` bigint(20) NOT NULL AUTO_INCREMENT,
  `Batch` varchar(50) NOT NULL,
  `Class` varchar(50) NOT NULL,
  `Faculty` varchar(50) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`Batch`,`Class`,`Faculty`,`CompanyId`),
  KEY `BatchClassId` (`BatchClassId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `tblbatchclass` */

insert  into `tblbatchclass`(`BatchClassId`,`Batch`,`Class`,`Faculty`,`CompanyId`) values (2,'2020-2021','UKG','SEC002',1),(3,'2020-2021','LKG','SEC002',1);

/*Table structure for table `tblbatchclassfee` */

DROP TABLE IF EXISTS `tblbatchclassfee`;

CREATE TABLE `tblbatchclassfee` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `FeeId` int(11) NOT NULL,
  `FeeStructureName` varchar(50) DEFAULT NULL,
  `FeeName` varchar(50) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Refundable` varchar(50) DEFAULT NULL,
  `BatchClassId` int(11) NOT NULL,
  `StudentType` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

/*Data for the table `tblbatchclassfee` */

insert  into `tblbatchclassfee`(`ID`,`FeeId`,`FeeStructureName`,`FeeName`,`Amount`,`Refundable`,`BatchClassId`,`StudentType`,`CompanyId`) values (9,2,'Yearly','Fee1',3000,' No',2,'General',1),(10,6,'Yearly','fee3',5246,' Yes',2,'Day-Boarder',1),(11,4,'Initial','Fee1',2500,' Yes',2,'Day-Boarder',1),(12,7,'Monthly','Fee1',3000,' Yes',2,'General',1),(13,9,'Monthly','Fee2',3685,' Yes',2,'Day-Boarder',1),(19,2,'Yearly','Fee1',3000,' No',3,'General',1),(20,6,'Yearly','fee3',5246,' Yes',3,'Day-Boarder',1),(21,4,'Initial','Fee1',2500,' Yes',3,'Day-Boarder',1),(22,10,'Monthly','coronafee',1500,' No',3,'Boarder',1),(23,7,'Monthly','Fee1',3000,' Yes',3,'General',1),(24,9,'Monthly','Fee2',3685,' Yes',3,'Day-Boarder',1);

/*Table structure for table `tblbatchdetails` */

DROP TABLE IF EXISTS `tblbatchdetails`;

CREATE TABLE `tblbatchdetails` (
  `BatchId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FromYear` int(11) DEFAULT NULL,
  `ToYear` int(11) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`BatchId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

/*Data for the table `tblbatchdetails` */

insert  into `tblbatchdetails`(`BatchId`,`FromYear`,`ToYear`,`Status`,`IsDeleted`,`CompanyId`) values (10,2020,2021,'Active',0,1);

/*Table structure for table `tblbilling` */

DROP TABLE IF EXISTS `tblbilling`;

CREATE TABLE `tblbilling` (
  `BillingId` int(11) NOT NULL AUTO_INCREMENT,
  `StudentId` varchar(50) NOT NULL,
  `Date` varchar(50) DEFAULT NULL,
  `Month` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `IsCreated` tinyint(1) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`BillingId`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

/*Data for the table `tblbilling` */

insert  into `tblbilling`(`BillingId`,`StudentId`,`Date`,`Month`,`Status`,`IsCreated`,`Batch`,`CreatedBy`,`CompanyId`) values (14,'20201-1','07/03/2020','01','Paid',1,'2020-2021',NULL,1),(15,'20201-1','07/03/2020','03','Paid',1,'2020-2021',NULL,1),(16,'20201-1','07/03/2020','04','Partial Paid',1,'2020-2021',NULL,1),(17,'20201-1','07/03/2020','02','Paid',1,'2020-2021',NULL,1),(18,'20201-0','07/06/2020','01','UnPaid',0,'2020-2021',NULL,1),(19,'20201-2','07/07/2020','01','Partial Paid',1,'2020-2021',NULL,1),(22,'20201-4','09/17/2020','06','UnPaid',0,'2020-2021',NULL,1),(23,'20201-4','09/17/2020','07','UnPaid',0,'2020-2021',NULL,1),(24,'20201-1','09/18/2020','05','UnPaid',0,'2020-2021',NULL,1),(25,'20201-1','09/18/2020','06','UnPaid',0,'2020-2021',NULL,1),(26,'20201-1','09/18/2020','07','UnPaid',0,'2020-2021',NULL,1),(27,'20201-4','09/18/2020','05','UnPaid',0,'2020-2021',NULL,1),(28,'20201-1','09/18/2020','08','UnPaid',0,'2020-2021',NULL,1),(29,'20201-1','09/18/2020','09','UnPaid',0,'2020-2021',NULL,1),(30,'20201-4','09/18/2020','08','UnPaid',0,'2020-2021',NULL,1),(31,'20201-4','09/18/2020','09','UnPaid',0,'2020-2021',NULL,1);

/*Table structure for table `tblbillingdetails` */

DROP TABLE IF EXISTS `tblbillingdetails`;

CREATE TABLE `tblbillingdetails` (
  `BillingFeeId` int(11) NOT NULL AUTO_INCREMENT,
  `FeeStructureName` varchar(50) DEFAULT NULL,
  `FeeName` varchar(50) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `BillingId` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`BillingFeeId`),
  KEY `FK_tblbillingdetails_tblbilling` (`BillingId`),
  CONSTRAINT `FK_tblbillingdetails_tblbilling` FOREIGN KEY (`BillingId`) REFERENCES `tblbilling` (`BillingId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8;

/*Data for the table `tblbillingdetails` */

insert  into `tblbillingdetails`(`BillingFeeId`,`FeeStructureName`,`FeeName`,`Amount`,`BillingId`,`CompanyId`) values (20,'Yearly','Fee1',3000,14,1),(21,'Monthly','Fee1',2800,14,1),(22,'Monthly','Fee1',2800,15,1),(23,'Monthly','Fee1',2800,16,1),(24,'Monthly','Fee1',2800,17,1),(25,'Monthly','Fee1',3000,18,1),(26,'Yearly','Fee1',1000,19,1),(27,'Monthly','coronafee',1500,19,1),(28,'Monthly','Fee1',3000,19,1),(31,'Yearly','Fee1',3000,22,1),(32,'Monthly','Fee1',3000,22,1),(33,'Monthly','Fee1',3000,23,1),(34,'Monthly','Fee1',3000,24,1),(35,'Monthly','Fee1',3000,25,1),(36,'Monthly','Fee1',3000,26,1),(37,'Monthly','Fee1',3000,27,1),(38,'Monthly','Fee1',3000,28,1),(39,'Monthly','Fee1',3000,29,1),(40,'Monthly','Fee1',3000,30,1),(41,'Monthly','Fee1',3000,31,1);

/*Table structure for table `tblclassdetails` */

DROP TABLE IF EXISTS `tblclassdetails`;

CREATE TABLE `tblclassdetails` (
  `ClassId` bigint(20) NOT NULL AUTO_INCREMENT,
  `ClassName` varchar(50) DEFAULT NULL,
  `ClassCode` varchar(50) DEFAULT NULL,
  `ClassType` varchar(50) DEFAULT NULL,
  `StudentLimit` int(11) DEFAULT NULL,
  `FacultyId` bigint(20) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ClassId`),
  KEY `FK_tblclassdetails_tblfacultydetails` (`FacultyId`),
  CONSTRAINT `FK_tblclassdetails_tblfacultydetails` FOREIGN KEY (`FacultyId`) REFERENCES `tblfacultydetails` (`FacultyId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblclassdetails` */

insert  into `tblclassdetails`(`ClassId`,`ClassName`,`ClassCode`,`ClassType`,`StudentLimit`,`FacultyId`,`CompanyId`) values (2,'UKG','ukg010','Yearly',80,3,1),(3,'LKG','lkg09','Yearly',985,2,1),(5,'ZXas','sadas','Trimester',23,2,1);

/*Table structure for table `tblcompanydetails` */

DROP TABLE IF EXISTS `tblcompanydetails`;

CREATE TABLE `tblcompanydetails` (
  `CompanyId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) DEFAULT NULL,
  `PhoneNo` varchar(50) DEFAULT NULL,
  `RegistrationDate` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `PAN` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`CompanyId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `tblcompanydetails` */

insert  into `tblcompanydetails`(`CompanyId`,`Name`,`PhoneNo`,`RegistrationDate`,`Status`,`Address`,`PAN`) values (1,'ebpearls','9632587412','Feb 12 2020  2:40PM','Approved','kathmandu','ddd343');

/*Table structure for table `tblcountrydetails` */

DROP TABLE IF EXISTS `tblcountrydetails`;

CREATE TABLE `tblcountrydetails` (
  `CountryId` int(11) NOT NULL AUTO_INCREMENT,
  `CountryName` varchar(50) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`CountryName`,`CompanyId`),
  KEY `CountryId` (`CountryId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `tblcountrydetails` */

insert  into `tblcountrydetails`(`CountryId`,`CountryName`,`CompanyId`) values (2,'Nepal',1),(4,'India',1);

/*Table structure for table `tblcurrentaddress` */

DROP TABLE IF EXISTS `tblcurrentaddress`;

CREATE TABLE `tblcurrentaddress` (
  `AddressId` int(11) NOT NULL AUTO_INCREMENT,
  `Qno` varchar(50) DEFAULT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `Municipality` varchar(50) DEFAULT NULL,
  `State` varchar(50) DEFAULT NULL,
  `Country` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) NOT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`AddressId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblcurrentaddress` */

insert  into `tblcurrentaddress`(`AddressId`,`Qno`,`Street`,`Municipality`,`State`,`Country`,`StudentId`,`IsDeleted`,`CompanyId`) values (1,'fsfsf','werfwerwe11','wefwe1','Kathmandu','Nepal','2021-0001',1,1),(2,'dfgdfgd','dfdgdg','dgerger','Kathmandu','Nepal','2021-2',1,1),(3,'dfd','sdd','rtyr','Kathmandu','Nepal','2021-3',1,1),(4,'sdsdifs','sjdisj','asjdia','Kathmandu','Nepal','2021-4',1,1),(5,'jasdjkas','asjdjas','aksjdasj','Kathmandu','Nepal','2021-5',0,1);

/*Table structure for table `tblcurrenteducation` */

DROP TABLE IF EXISTS `tblcurrenteducation`;

CREATE TABLE `tblcurrenteducation` (
  `CurrentEducationId` int(11) NOT NULL AUTO_INCREMENT,
  `Faculty` varchar(50) DEFAULT NULL,
  `Class` varchar(50) DEFAULT NULL,
  `Section` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) NOT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `Batch` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`CurrentEducationId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblcurrenteducation` */

insert  into `tblcurrenteducation`(`CurrentEducationId`,`Faculty`,`Class`,`Section`,`StudentId`,`IsDeleted`,`Batch`,`CompanyId`) values (1,'Primary','UKG','A','2021-0001',1,'2020-2021',1),(2,'Primary','UKG','A','2021-2',1,'2020-2021',1),(3,'Secondary','UKG','A','2021-3',1,'2020-2021',1),(4,'Primary','UKG','A','2021-4',1,'2020-2021',1),(5,'Secondary','UKG','A','2021-5',0,'2020-2021',1);

/*Table structure for table `tbldayoff` */

DROP TABLE IF EXISTS `tbldayoff`;

CREATE TABLE `tbldayoff` (
  `DayOffId` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `DateFrom` varchar(50) DEFAULT NULL,
  `DateTo` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`DayOffId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `tbldayoff` */

insert  into `tbldayoff`(`DayOffId`,`Title`,`Batch`,`DateFrom`,`DateTo`,`CompanyId`) values (1,'lockdown','2020-2021','2077-03-24','2077-03-31',1),(2,'lockdown','2020-2021','2077-03-24','2077-03-31',1);

/*Table structure for table `tbldayoffdetails` */

DROP TABLE IF EXISTS `tbldayoffdetails`;

CREATE TABLE `tbldayoffdetails` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Department` varchar(50) DEFAULT NULL,
  `Faculty` varchar(50) DEFAULT NULL,
  `Class` varchar(50) DEFAULT NULL,
  `DayOffId` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `DayOffId` (`DayOffId`),
  CONSTRAINT `tbldayoffdetails_ibfk_1` FOREIGN KEY (`DayOffId`) REFERENCES `tbldayoff` (`DayOffId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `tbldayoffdetails` */

insert  into `tbldayoffdetails`(`Id`,`Department`,`Faculty`,`Class`,`DayOffId`,`CompanyId`) values (1,'Student','SEC002','UKG',2,1),(2,'Student','SEC002','LKG',2,1),(3,'Teacher','SEC002',NULL,2,1);

/*Table structure for table `tbldeletedbilling` */

DROP TABLE IF EXISTS `tbldeletedbilling`;

CREATE TABLE `tbldeletedbilling` (
  `BillingId` int(11) NOT NULL,
  `StudentId` varchar(50) DEFAULT NULL,
  `Date` varchar(50) DEFAULT NULL,
  `Month` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `IsCreated` varchar(50) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `DeletedDate` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`BillingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tbldeletedbilling` */

/*Table structure for table `tbldeletedbillingdetails` */

DROP TABLE IF EXISTS `tbldeletedbillingdetails`;

CREATE TABLE `tbldeletedbillingdetails` (
  `BillingFeeId` int(11) NOT NULL,
  `FeeStructureName` varchar(50) DEFAULT NULL,
  `FeeName` varchar(50) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `BillingId` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  `DeletedDate` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`BillingFeeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tbldeletedbillingdetails` */

/*Table structure for table `tbldeletedreceipt` */

DROP TABLE IF EXISTS `tbldeletedreceipt`;

CREATE TABLE `tbldeletedreceipt` (
  `ReceiptId` int(11) NOT NULL,
  `StudentId` varchar(50) DEFAULT NULL,
  `Date` varchar(50) DEFAULT NULL,
  `Month` varchar(50) DEFAULT NULL,
  `TotalAmount` int(11) DEFAULT NULL,
  `PaidAmount` int(11) DEFAULT NULL,
  `DueAmount` int(11) DEFAULT NULL,
  `PaymentMethod` varchar(50) DEFAULT NULL,
  `BankName` varchar(50) DEFAULT NULL,
  `ChequeNo` varchar(50) DEFAULT NULL,
  `BillingId` int(11) DEFAULT NULL,
  `Fine` int(11) DEFAULT NULL,
  `Discount` int(11) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  `DeletedDate` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ReceiptId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tbldeletedreceipt` */

/*Table structure for table `tbldepartment` */

DROP TABLE IF EXISTS `tbldepartment`;

CREATE TABLE `tbldepartment` (
  `DepartmentId` int(11) NOT NULL AUTO_INCREMENT,
  `Department` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`DepartmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `tbldepartment` */

insert  into `tbldepartment`(`DepartmentId`,`Department`,`CompanyId`) values (1,'Student',1),(2,'Accountant',1),(3,'Teacher',1);

/*Table structure for table `tbldesignation` */

DROP TABLE IF EXISTS `tbldesignation`;

CREATE TABLE `tbldesignation` (
  `DesignationId` int(11) NOT NULL AUTO_INCREMENT,
  `Designation` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`DesignationId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `tbldesignation` */

insert  into `tbldesignation`(`DesignationId`,`Designation`,`CompanyId`) values (1,'Teacher',1),(2,'Accountant',1);

/*Table structure for table `tbleducation` */

DROP TABLE IF EXISTS `tbleducation`;

CREATE TABLE `tbleducation` (
  `EducationId` int(11) NOT NULL AUTO_INCREMENT,
  `Degree` varchar(50) DEFAULT NULL,
  `Institution` varchar(50) DEFAULT NULL,
  `TotalMarks` int(11) DEFAULT NULL,
  `Division` varchar(50) DEFAULT NULL,
  `TeacherId` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`EducationId`),
  KEY `FK_tbleducation_tblteacherinfo` (`TeacherId`),
  CONSTRAINT `FK_tbleducation_tblteacherinfo` FOREIGN KEY (`TeacherId`) REFERENCES `tblteacherinfo` (`TeacherId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Data for the table `tbleducation` */

insert  into `tbleducation`(`EducationId`,`Degree`,`Institution`,`TotalMarks`,`Division`,`TeacherId`,`IsDeleted`,`CompanyId`) values (1,'sdfsdf','sdfsdfs',215,'first',1,1,1),(5,'rewefew','tgfds',234,'ferfer',2,1,1),(7,'ereer','erfrd',343,'qwwq',3,1,1),(8,'asdnjs','jdfd',345,'sdhfsdj',4,0,1);

/*Table structure for table `tblemergencycontact` */

DROP TABLE IF EXISTS `tblemergencycontact`;

CREATE TABLE `tblemergencycontact` (
  `EmergencyContactId` int(11) NOT NULL AUTO_INCREMENT,
  `ParentName` varchar(50) DEFAULT NULL,
  `ContactNumber` varchar(50) DEFAULT NULL,
  `Location` varchar(100) DEFAULT NULL,
  `StudentId` varchar(50) NOT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`EmergencyContactId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblemergencycontact` */

insert  into `tblemergencycontact`(`EmergencyContactId`,`ParentName`,`ContactNumber`,`Location`,`StudentId`,`IsDeleted`,`CompanyId`) values (1,'adasdss','2345678907','sdfsdfsdvs','2021-0001',1,1),(2,'efergerg','8745963258','shsjs','2021-2',1,1),(3,'sdfdfd','8596325896','sdfdsfs','2021-3',1,1),(4,'sdie wiej','7412589632','asjdasdjias','2021-4',1,1),(5,'sjdjsjf','9878767878','ksjdkfjsd','2021-5',0,1);

/*Table structure for table `tblexperience` */

DROP TABLE IF EXISTS `tblexperience`;

CREATE TABLE `tblexperience` (
  `ExperienceId` int(11) NOT NULL AUTO_INCREMENT,
  `Organisation` varchar(50) DEFAULT NULL,
  `Post` varchar(50) DEFAULT NULL,
  `DateFrom` varchar(50) DEFAULT NULL,
  `DateTo` varchar(50) DEFAULT NULL,
  `TeacherId` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ExperienceId`),
  KEY `FK_tblexperience_tblteacherinfo` (`TeacherId`),
  CONSTRAINT `FK_tblexperience_tblteacherinfo` FOREIGN KEY (`TeacherId`) REFERENCES `tblteacherinfo` (`TeacherId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Data for the table `tblexperience` */

insert  into `tblexperience`(`ExperienceId`,`Organisation`,`Post`,`DateFrom`,`DateTo`,`TeacherId`,`IsDeleted`,`CompanyId`) values (1,'dsfsdsd','tyhygt','2076-12-05','2076-12-15',1,1,1),(5,'rtfgrr','trfg','2076-12-05','2076-12-07',2,1,1),(7,'retdede','redf','2077-01-03','2077-01-11',3,1,1),(8,'jsdhfsd','jshdjsd','2077-03-02','2077-03-01',4,0,1);

/*Table structure for table `tblfacultydetails` */

DROP TABLE IF EXISTS `tblfacultydetails`;

CREATE TABLE `tblfacultydetails` (
  `FacultyId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FacultyCode` varchar(50) DEFAULT NULL,
  `FacultyName` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`FacultyId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `tblfacultydetails` */

insert  into `tblfacultydetails`(`FacultyId`,`FacultyCode`,`FacultyName`,`CompanyId`) values (2,'PRI001','Primary',1),(3,'89','Secondary',1),(4,'RRT','SDASD',1);

/*Table structure for table `tblfeature` */

DROP TABLE IF EXISTS `tblfeature`;

CREATE TABLE `tblfeature` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `Company_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;

/*Data for the table `tblfeature` */

insert  into `tblfeature`(`ID`,`Name`,`Company_ID`) values (5,'Dashboard',1),(6,'GeneralSettings',1),(10,'StudentManagement',1),(11,'Scholarship',1),(12,'BillingManagement',1),(13,'ReceiptManagement',1),(14,'DueReport',1),(15,'StudentAttendance',1),(16,'StaffManagement',1),(17,'StaffAttendance',1),(18,'FineManagement',1),(19,'TransportManagement',1),(20,'TransportMapping',1),(21,'ExtraFeeMapping',1),(22,'Group',1),(23,'Feature',1),(24,'Action',1),(25,'UserAccess',1);

/*Table structure for table `tblfeatureaction` */

DROP TABLE IF EXISTS `tblfeatureaction`;

CREATE TABLE `tblfeatureaction` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `Company_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `tblfeatureaction` */

insert  into `tblfeatureaction`(`ID`,`Name`,`Company_ID`) values (2,'add',1),(3,'edit',1),(4,'delete',1),(5,'view',1),(6,'report',1);

/*Table structure for table `tblfeedetails` */

DROP TABLE IF EXISTS `tblfeedetails`;

CREATE TABLE `tblfeedetails` (
  `FeeId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FeeStructureName` varchar(50) NOT NULL,
  `FeeName` varchar(50) NOT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Refundable` varchar(50) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `StudentType` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`FeeStructureName`,`FeeName`,`CompanyId`),
  KEY `FeeId` (`FeeId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

/*Data for the table `tblfeedetails` */

insert  into `tblfeedetails`(`FeeId`,`FeeStructureName`,`FeeName`,`Amount`,`Refundable`,`IsDeleted`,`StudentType`,`CompanyId`) values (3,'Extra','Fee1',5000,'No',0,'Day-Boarder',1),(4,'Initial','Fee1',2500,'Yes',0,'Day-Boarder',1),(10,'Monthly','coronafee',1500,'No',0,'Boarder',1),(7,'Monthly','Fee1',3000,'Yes',0,'General',1),(9,'Monthly','Fee2',3685,'Yes',0,'Day-Boarder',1),(2,'Yearly','Fee1',3000,'No',0,'General',1),(6,'Yearly','fee3',5246,'Yes',0,'Day-Boarder',1);

/*Table structure for table `tblfeestructuredetails` */

DROP TABLE IF EXISTS `tblfeestructuredetails`;

CREATE TABLE `tblfeestructuredetails` (
  `FeeStructureId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FeeStructureName` varchar(50) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`FeeStructureName`,`CompanyId`),
  KEY `FeeStructureId` (`FeeStructureId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblfeestructuredetails` */

insert  into `tblfeestructuredetails`(`FeeStructureId`,`FeeStructureName`,`CompanyId`) values (1,'Yearly',1),(3,'Initial',1),(4,'Extra',1),(5,'Monthly',1);

/*Table structure for table `tblfine` */

DROP TABLE IF EXISTS `tblfine`;

CREATE TABLE `tblfine` (
  `FineId` int(11) NOT NULL AUTO_INCREMENT,
  `DayFrom` int(11) DEFAULT NULL,
  `DayTo` int(11) DEFAULT NULL,
  `FineType` varchar(50) DEFAULT NULL,
  `FineAmount` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`FineId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Data for the table `tblfine` */

insert  into `tblfine`(`FineId`,`DayFrom`,`DayTo`,`FineType`,`FineAmount`,`CompanyId`) values (5,21,30,'flat',200,1),(6,31,40,'flat',300,1),(7,41,50,'percentage',1200,1),(8,0,5,'flat',200,1);

/*Table structure for table `tblfollowup` */

DROP TABLE IF EXISTS `tblfollowup`;

CREATE TABLE `tblfollowup` (
  `FollowUpId` int(11) NOT NULL AUTO_INCREMENT,
  `StudentId` varchar(50) NOT NULL,
  `Response` varchar(200) DEFAULT NULL,
  `FollowUpDate` varchar(50) DEFAULT NULL,
  `ExpectedDate` varchar(50) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`FollowUpId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `tblfollowup` */

insert  into `tblfollowup`(`FollowUpId`,`StudentId`,`Response`,`FollowUpDate`,`ExpectedDate`,`Batch`,`CompanyId`) values (1,'2021-3','Remind me later','2020-04-02','2020-04-06','2020-2021',1),(2,'2021-3','Remind me later','2020-04-07','2020-04-10','2020-2021',1),(3,'20201-1','Remind me later','2020-07-08','2020-07-20','2020-2021',1);

/*Table structure for table `tblgroup` */

DROP TABLE IF EXISTS `tblgroup`;

CREATE TABLE `tblgroup` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserRole` varchar(50) DEFAULT NULL,
  `Company_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `tblgroup` */

insert  into `tblgroup`(`ID`,`UserRole`,`Company_ID`) values (3,'admin',1),(4,'developer',1);

/*Table structure for table `tblpasteducation` */

DROP TABLE IF EXISTS `tblpasteducation`;

CREATE TABLE `tblpasteducation` (
  `PastEducationId` int(11) NOT NULL AUTO_INCREMENT,
  `Degree` varchar(50) DEFAULT NULL,
  `School` varchar(50) DEFAULT NULL,
  `TotalMarks` int(11) DEFAULT NULL,
  `Division` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) NOT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`PastEducationId`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;

/*Data for the table `tblpasteducation` */

insert  into `tblpasteducation`(`PastEducationId`,`Degree`,`School`,`TotalMarks`,`Division`,`StudentId`,`IsDeleted`,`CompanyId`) values (3,'10th','vvrs2',230,'first','2021-0001',1,1),(4,'sjdjsd','uyduds',856,'first','2021-2',1,1),(12,'sdhushduah','wefwiejfwi',52,'uuh','2021-4',1,1),(20,'10th','dav',230,'first','2021-3',1,1),(22,'jskdjsdf','sjsd kjasidj',768,'uasdia','2021-5',0,1);

/*Table structure for table `tblpermanentaddress` */

DROP TABLE IF EXISTS `tblpermanentaddress`;

CREATE TABLE `tblpermanentaddress` (
  `AddressId` int(11) NOT NULL AUTO_INCREMENT,
  `Qno` varchar(50) DEFAULT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `Municipality` varchar(50) DEFAULT NULL,
  `State` varchar(50) DEFAULT NULL,
  `Country` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) NOT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`AddressId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblpermanentaddress` */

insert  into `tblpermanentaddress`(`AddressId`,`Qno`,`Street`,`Municipality`,`State`,`Country`,`StudentId`,`IsDeleted`,`CompanyId`) values (1,'fsfsf','werfwerwe','wefwe','Kathmandu','Nepal','2021-0001',1,1),(2,'dfgdfgd','dfdgdg','dgerger','Kathmandu','Nepal','2021-2',1,1),(3,'dfd','sdd','rtyr','Kathmandu','Nepal','2021-3',1,1),(4,'sdsdifs','sjdisj','asjdia','Kathmandu','Nepal','2021-4',1,1),(5,'jasdjkas','asjdjas','aksjdasj','Kathmandu','Nepal','2021-5',0,1);

/*Table structure for table `tblreceipt` */

DROP TABLE IF EXISTS `tblreceipt`;

CREATE TABLE `tblreceipt` (
  `ReceiptId` int(11) NOT NULL AUTO_INCREMENT,
  `StudentId` varchar(50) NOT NULL,
  `Date` varchar(50) DEFAULT NULL,
  `Month` varchar(50) DEFAULT NULL,
  `TotalAmount` int(11) DEFAULT NULL,
  `PaidAmount` int(11) DEFAULT NULL,
  `DueAmount` int(11) DEFAULT NULL,
  `PaymentMethod` varchar(50) DEFAULT NULL,
  `BankName` varchar(50) DEFAULT NULL,
  `ChequeNo` varchar(100) DEFAULT NULL,
  `BillingId` int(11) DEFAULT NULL,
  `Fine` int(11) DEFAULT NULL,
  `Discount` int(11) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ReceiptId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `tblreceipt` */

insert  into `tblreceipt`(`ReceiptId`,`StudentId`,`Date`,`Month`,`TotalAmount`,`PaidAmount`,`DueAmount`,`PaymentMethod`,`BankName`,`ChequeNo`,`BillingId`,`Fine`,`Discount`,`Batch`,`CreatedBy`,`CompanyId`) values (1,'20201-1','2020-07-06','01',5600,4500,1100,'cash',NULL,NULL,14,0,200,'2020-2021',NULL,1),(2,'20201-1','2020-07-06','01',1100,1100,0,'cash',NULL,NULL,14,0,0,'2020-2021',NULL,1),(3,'20201-1','2020-07-06','02',2800,2800,0,'cash',NULL,NULL,17,0,0,'2020-2021',NULL,1),(4,'20201-1','2020-07-06','03',2800,2800,0,'cash',NULL,NULL,15,0,0,'2020-2021',NULL,1),(5,'20201-2','2020-07-07','01',5700,5000,700,'cash',NULL,NULL,19,200,0,'2020-2021',NULL,1),(6,'20201-1','2020-09-17','04',2500,1700,800,'cash',NULL,NULL,16,0,300,'2020-2021',NULL,1);

/*Table structure for table `tblresponsetext` */

DROP TABLE IF EXISTS `tblresponsetext`;

CREATE TABLE `tblresponsetext` (
  `ResponseTextId` int(11) NOT NULL AUTO_INCREMENT,
  `ResponseText` varchar(200) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ResponseTextId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `tblresponsetext` */

insert  into `tblresponsetext`(`ResponseTextId`,`ResponseText`,`CompanyId`) values (2,'Remind me later',1),(3,'asasc',1),(4,'sabdj',1);

/*Table structure for table `tblscholarship` */

DROP TABLE IF EXISTS `tblscholarship`;

CREATE TABLE `tblscholarship` (
  `StudentScholarshipId` int(11) NOT NULL AUTO_INCREMENT,
  `ScholarshipName` varchar(50) DEFAULT NULL,
  `Description` varchar(50) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `StudentId` varchar(50) NOT NULL,
  `DateOfAdmission` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`StudentScholarshipId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblscholarship` */

insert  into `tblscholarship`(`StudentScholarshipId`,`ScholarshipName`,`Description`,`IsDeleted`,`StudentId`,`DateOfAdmission`,`CompanyId`) values (1,'Normal','wwewefw wefwe',1,'2021-0001','2076-12-05',1),(2,'Normal','hhfshdf sdfsd',1,'2021-2','2076-12-05',1),(3,'Normal','sdsss',1,'2021-3','2076-12-05',1),(4,'Normal','hsdsdhsu uasd',1,'2021-4','2076-12-05',1),(5,'Normal','ijsdijsijdis',0,'2021-5','2077-02-01',1);

/*Table structure for table `tblscholarshipdetails` */

DROP TABLE IF EXISTS `tblscholarshipdetails`;

CREATE TABLE `tblscholarshipdetails` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ScholarshipName` varchar(50) NOT NULL,
  `DiscountType` varchar(50) DEFAULT NULL,
  `Discount` int(11) DEFAULT NULL,
  `FeeId` int(11) DEFAULT NULL,
  `FeeStructureName` varchar(50) DEFAULT NULL,
  `Batch` varchar(50) NOT NULL,
  `Class` varchar(50) NOT NULL,
  `Faculty` varchar(50) NOT NULL,
  `Amount` int(11) DEFAULT NULL,
  `StudentType` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;

/*Data for the table `tblscholarshipdetails` */

insert  into `tblscholarshipdetails`(`Id`,`ScholarshipName`,`DiscountType`,`Discount`,`FeeId`,`FeeStructureName`,`Batch`,`Class`,`Faculty`,`Amount`,`StudentType`,`CompanyId`) values (16,'General','flat',500,10,'Monthly','2020-2021','LKG','SEC002',1500,'Boarder',1);

/*Table structure for table `tblscholarshipname` */

DROP TABLE IF EXISTS `tblscholarshipname`;

CREATE TABLE `tblscholarshipname` (
  `ScholarshipId` bigint(20) NOT NULL AUTO_INCREMENT,
  `ScholarshipName` varchar(50) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`ScholarshipName`,`CompanyId`),
  KEY `ScholarshipId` (`ScholarshipId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblscholarshipname` */

insert  into `tblscholarshipname`(`ScholarshipId`,`ScholarshipName`,`CompanyId`) values (4,'Normal',1),(5,'jasda',1);

/*Table structure for table `tblsectiondetails` */

DROP TABLE IF EXISTS `tblsectiondetails`;

CREATE TABLE `tblsectiondetails` (
  `SectionId` bigint(20) NOT NULL AUTO_INCREMENT,
  `SectionName` varchar(50) DEFAULT NULL,
  `StudentLimit` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`SectionId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblsectiondetails` */

insert  into `tblsectiondetails`(`SectionId`,`SectionName`,`StudentLimit`,`CompanyId`) values (2,'A',40,1),(5,'B',48,1);

/*Table structure for table `tblspecialscholarship` */

DROP TABLE IF EXISTS `tblspecialscholarship`;

CREATE TABLE `tblspecialscholarship` (
  `SpecialScholarshipId` int(11) NOT NULL AUTO_INCREMENT,
  `Batch` varchar(50) DEFAULT NULL,
  `Class` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) NOT NULL,
  `FeeId` int(11) DEFAULT NULL,
  `FeeStructureName` varchar(50) DEFAULT NULL,
  `Discount` int(11) DEFAULT NULL,
  `Faculty` varchar(50) DEFAULT NULL,
  `DiscountType` varchar(50) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `StudentType` varchar(50) DEFAULT NULL,
  `BatchClassId` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`SpecialScholarshipId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `tblspecialscholarship` */

insert  into `tblspecialscholarship`(`SpecialScholarshipId`,`Batch`,`Class`,`StudentId`,`FeeId`,`FeeStructureName`,`Discount`,`Faculty`,`DiscountType`,`Amount`,`StudentType`,`BatchClassId`,`CompanyId`) values (1,'2020-2021','LKG','20201-2',2,'Yearly',2000,'SEC002','flat',3000,'General',3,1);

/*Table structure for table `tblstaffattendance` */

DROP TABLE IF EXISTS `tblstaffattendance`;

CREATE TABLE `tblstaffattendance` (
  `StaffId` int(11) NOT NULL AUTO_INCREMENT,
  `Batch` varchar(50) DEFAULT NULL,
  `TeacherId` int(11) DEFAULT NULL,
  `Date` varchar(50) DEFAULT NULL,
  `Attendance` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`StaffId`),
  KEY `FK_tblstaffattendance_tblteacherinfo` (`TeacherId`),
  CONSTRAINT `FK_tblstaffattendance_tblteacherinfo` FOREIGN KEY (`TeacherId`) REFERENCES `tblteacherinfo` (`TeacherId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `tblstaffattendance` */

insert  into `tblstaffattendance`(`StaffId`,`Batch`,`TeacherId`,`Date`,`Attendance`,`CompanyId`) values (4,'2020-2021',4,'2077-03-04','P',1);

/*Table structure for table `tblstartdate` */

DROP TABLE IF EXISTS `tblstartdate`;

CREATE TABLE `tblstartdate` (
  `StartDateId` int(11) NOT NULL AUTO_INCREMENT,
  `Date` varchar(50) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`StartDateId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `tblstartdate` */

insert  into `tblstartdate`(`StartDateId`,`Date`,`Batch`,`CompanyId`) values (6,'2077-01-18','2020-2021',1);

/*Table structure for table `tblstartpoint` */

DROP TABLE IF EXISTS `tblstartpoint`;

CREATE TABLE `tblstartpoint` (
  `PlaceId` int(11) NOT NULL AUTO_INCREMENT,
  `Place` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`PlaceId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `tblstartpoint` */

insert  into `tblstartpoint`(`PlaceId`,`Place`,`CompanyId`) values (2,'Hattiban1',1);

/*Table structure for table `tblstatedetails` */

DROP TABLE IF EXISTS `tblstatedetails`;

CREATE TABLE `tblstatedetails` (
  `StateId` int(11) NOT NULL AUTO_INCREMENT,
  `StateName` varchar(50) NOT NULL,
  `CountryName` varchar(50) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`StateName`,`CountryName`,`CompanyId`),
  KEY `StateId` (`StateId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `tblstatedetails` */

insert  into `tblstatedetails`(`StateId`,`StateName`,`CountryName`,`CompanyId`) values (2,'Kathmandu','Nepal',1);

/*Table structure for table `tblstudentattendance` */

DROP TABLE IF EXISTS `tblstudentattendance`;

CREATE TABLE `tblstudentattendance` (
  `StudentAttendanceId` int(11) NOT NULL AUTO_INCREMENT,
  `Batch` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) DEFAULT NULL,
  `Date` varchar(50) DEFAULT NULL,
  `Attendance` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`StudentAttendanceId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Data for the table `tblstudentattendance` */

insert  into `tblstudentattendance`(`StudentAttendanceId`,`Batch`,`StudentId`,`Date`,`Attendance`,`CompanyId`) values (1,'2020-2021','2021-3','2076-12-18','A',1),(3,'2020-2021','2021-3','2076-12-20','P',1),(4,'2020-2021','2021-3','2076-12-21','P',1),(5,'2020-2021','2021-3','2077-01-04','P',1),(6,'2020-2021','2021-3','2077-01-11','A',1),(7,'2020-2021','2021-3','2077-01-13','P',1),(8,'2020-2021','20201-1','2077-03-09','A',1),(9,'2020-2021','20201-1','2077-03-10','P',1);

/*Table structure for table `tblstudentextrafee` */

DROP TABLE IF EXISTS `tblstudentextrafee`;

CREATE TABLE `tblstudentextrafee` (
  `StudentExtraFeeId` int(11) NOT NULL AUTO_INCREMENT,
  `Batch` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) DEFAULT NULL,
  `Month` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`StudentExtraFeeId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Data for the table `tblstudentextrafee` */

insert  into `tblstudentextrafee`(`StudentExtraFeeId`,`Batch`,`StudentId`,`Month`,`CompanyId`) values (11,'2020-2021','2021-3','02',1),(12,'2020-2021','20201-1','01',1);

/*Table structure for table `tblstudentextrafeedetails` */

DROP TABLE IF EXISTS `tblstudentextrafeedetails`;

CREATE TABLE `tblstudentextrafeedetails` (
  `StudentExtraId` int(11) NOT NULL AUTO_INCREMENT,
  `FeeName` varchar(50) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `StudentExtraFeeId` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`StudentExtraId`),
  KEY `FK_tblstudentextrafeedetails_tblstudentextrafee` (`StudentExtraFeeId`),
  CONSTRAINT `FK_tblstudentextrafeedetails_tblstudentextrafee` FOREIGN KEY (`StudentExtraFeeId`) REFERENCES `tblstudentextrafee` (`StudentExtraFeeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Data for the table `tblstudentextrafeedetails` */

insert  into `tblstudentextrafeedetails`(`StudentExtraId`,`FeeName`,`Amount`,`StudentExtraFeeId`,`CompanyId`) values (11,'Fee1',5009,11,1),(12,'Fee1',2000,12,1);

/*Table structure for table `tblstudentinfo` */

DROP TABLE IF EXISTS `tblstudentinfo`;

CREATE TABLE `tblstudentinfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `DateOfBirth_BS` varchar(50) DEFAULT NULL,
  `DateOfBirth_AD` varchar(50) DEFAULT NULL,
  `Gender` varchar(50) DEFAULT NULL,
  `EmailId` varchar(50) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `MobileNo` varchar(50) DEFAULT NULL,
  `FatherName` varchar(50) DEFAULT NULL,
  `FatherNumber` varchar(50) DEFAULT NULL,
  `MotherName` varchar(50) DEFAULT NULL,
  `MotherNumber` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `StudentType` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`StudentId`,`CompanyId`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblstudentinfo` */

insert  into `tblstudentinfo`(`ID`,`FirstName`,`LastName`,`DateOfBirth_BS`,`DateOfBirth_AD`,`Gender`,`EmailId`,`Batch`,`MobileNo`,`FatherName`,`FatherNumber`,`MotherName`,`MotherNumber`,`Status`,`IsDeleted`,`StudentType`,`StudentId`,`CompanyId`) values (1,'Sushant1','Kumar','2076-12-03','2020-03-02','Male','sushant@gmail.com','2020-2021','9876567890','hksingh1','9856321456','7412589932','7412589932','Active',1,'Boarder','2021-0001',1),(2,'roshant','roy','2076-12-12','2020-03-11','Male','roshan@gmail.com','2020-2021','0987687675','udfusdj','9856325412','7415896325','7415896325','Active',1,'Boarder','2021-2',1),(3,'sneha','kumari','2076-12-05','2020-03-04','Female','sneha@gmail.com','2020-2021','9874563255','sdjfksd ijsf','9856325896','7412589632','7412589632','Active',1,'General','2021-3',1),(4,'uuuyrwenew','sudfsiud','2077-01-01','2020-04-06','Male','sush@gmail.com','2020-2021','7896325741','sudusds','4125639874','1452369856','1452369856','Active',1,'Boarder','2021-4',1),(5,'test','one','2077-02-01','2020-05-05','Male','sush@gmail.com','2020-2021','6787898989','asjdijsa jajsda','5676677766','8786777877','8786777877','Active',0,'Boarder','2021-5',1);

/*Table structure for table `tblstudenttransport` */

DROP TABLE IF EXISTS `tblstudenttransport`;

CREATE TABLE `tblstudenttransport` (
  `StudentTransportId` int(11) NOT NULL AUTO_INCREMENT,
  `Batch` varchar(50) DEFAULT NULL,
  `StudentId` varchar(50) DEFAULT NULL,
  `PlaceTo` varchar(50) DEFAULT NULL,
  `Type` varchar(50) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`StudentTransportId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `tblstudenttransport` */

insert  into `tblstudenttransport`(`StudentTransportId`,`Batch`,`StudentId`,`PlaceTo`,`Type`,`Amount`,`CompanyId`) values (1,'2020-2021','20201-2','lagankhel','TwoWay',400,1);

/*Table structure for table `tblstudenttype` */

DROP TABLE IF EXISTS `tblstudenttype`;

CREATE TABLE `tblstudenttype` (
  `StudentTypeId` bigint(20) NOT NULL AUTO_INCREMENT,
  `StudentTypeName` varchar(50) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`StudentTypeName`,`CompanyId`),
  KEY `StudentTypeId` (`StudentTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `tblstudenttype` */

insert  into `tblstudenttype`(`StudentTypeId`,`StudentTypeName`,`CompanyId`) values (1,'Boarder',1),(4,'Day-Boarder',1),(5,'General',1);

/*Table structure for table `tblteachercurrentaddress` */

DROP TABLE IF EXISTS `tblteachercurrentaddress`;

CREATE TABLE `tblteachercurrentaddress` (
  `AddressId` int(11) NOT NULL AUTO_INCREMENT,
  `HouseNo` varchar(50) DEFAULT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `Municipality` varchar(50) DEFAULT NULL,
  `Country` varchar(50) DEFAULT NULL,
  `State` varchar(50) DEFAULT NULL,
  `MobileNo` varchar(50) DEFAULT NULL,
  `PhoneNo` varchar(50) DEFAULT NULL,
  `TeacherId` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`AddressId`),
  KEY `FK_tblteachercurrentaddress_tblteacherinfo` (`TeacherId`),
  CONSTRAINT `FK_tblteachercurrentaddress_tblteacherinfo` FOREIGN KEY (`TeacherId`) REFERENCES `tblteacherinfo` (`TeacherId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `tblteachercurrentaddress` */

insert  into `tblteachercurrentaddress`(`AddressId`,`HouseNo`,`Street`,`Municipality`,`Country`,`State`,`MobileNo`,`PhoneNo`,`TeacherId`,`IsDeleted`,`CompanyId`) values (1,'sds','ytgh','trgf','Nepal','Kathmandu','7412589632','7458963258',1,1,1),(2,'sddscdds','redfg','ujhyg','Nepal','Kathmandu','8745896325','8745896332',2,1,1),(3,'ewrwerwe','ewrwerwe','sdsfsdf','Nepal','Kathmandu','2345678906','9856325412',3,1,1),(4,'hasd','jsdjsahd','js','Nepal','Province 2','8976789878','9856787677',4,0,1);

/*Table structure for table `tblteacherinfo` */

DROP TABLE IF EXISTS `tblteacherinfo`;

CREATE TABLE `tblteacherinfo` (
  `TeacherId` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Designation` varchar(50) DEFAULT NULL,
  `Faculty` varchar(50) DEFAULT NULL,
  `Department` varchar(50) DEFAULT NULL,
  `Gender` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `DateOfBirth` varchar(50) DEFAULT NULL,
  `Batch` varchar(50) DEFAULT NULL,
  `Citizenship` varchar(50) DEFAULT NULL,
  `JoiningDate` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`TeacherId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `tblteacherinfo` */

insert  into `tblteacherinfo`(`TeacherId`,`FirstName`,`LastName`,`Designation`,`Faculty`,`Department`,`Gender`,`Email`,`DateOfBirth`,`Batch`,`Citizenship`,`JoiningDate`,`Status`,`IsDeleted`,`CompanyId`) values (1,'Sonali','roy','Teacher','Primary','Teacher','Female','sonali@gmail.com','2076-12-04','2020-2021','Nepal','2076-12-06','Active',1,1),(2,'roshan','roy','Teacher','Primary','Teacher','Male','roshan@gmail.com','2076-12-04','2020-2021','Nepal','2076-12-06','Active',1,1),(3,'sushmita','roy','Teacher','Primary','Teacher','Female','sushmita@gmail.com','2077-01-02','2020-2021','234567898','2077-01-03','Active',1,1),(4,'testteaher','check','Teacher','SEC002','Teacher','Male','techer@gmail.com','2077-03-03','2020-2021','shdjsh12365','2077-03-02','Active',0,1);

/*Table structure for table `tblteacherpermanentaddress` */

DROP TABLE IF EXISTS `tblteacherpermanentaddress`;

CREATE TABLE `tblteacherpermanentaddress` (
  `AddressId` int(11) NOT NULL AUTO_INCREMENT,
  `HouseNo` varchar(50) DEFAULT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `Municipality` varchar(50) DEFAULT NULL,
  `Country` varchar(50) DEFAULT NULL,
  `State` varchar(50) DEFAULT NULL,
  `MobileNo` varchar(50) DEFAULT NULL,
  `PhoneNo` varchar(50) DEFAULT NULL,
  `TeacherId` int(11) DEFAULT NULL,
  `IsDeleted` tinyint(3) unsigned DEFAULT '0',
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`AddressId`),
  KEY `FK_tblteacherpermanentaddress_tblteacherinfo` (`TeacherId`),
  CONSTRAINT `FK_tblteacherpermanentaddress_tblteacherinfo` FOREIGN KEY (`TeacherId`) REFERENCES `tblteacherinfo` (`TeacherId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `tblteacherpermanentaddress` */

insert  into `tblteacherpermanentaddress`(`AddressId`,`HouseNo`,`Street`,`Municipality`,`Country`,`State`,`MobileNo`,`PhoneNo`,`TeacherId`,`IsDeleted`,`CompanyId`) values (1,'sddscdds','redfg','ujhyg','Nepal','Kathmandu','8745896325','8745896332',2,1,1),(2,'ewrwerwe','ewrwerwe','sdsfsdf','Nepal','Kathmandu','2345678906','9856325412',3,1,1),(3,'hasd','jsdjsahd','js','Nepal','Province','8976789878','9856787677',4,0,1);

/*Table structure for table `tbltransportation` */

DROP TABLE IF EXISTS `tbltransportation`;

CREATE TABLE `tbltransportation` (
  `TransportationId` int(11) NOT NULL AUTO_INCREMENT,
  `PlaceTo` varchar(50) DEFAULT NULL,
  `DistanceFrom` int(11) DEFAULT NULL,
  `DistanceTo` int(11) DEFAULT NULL,
  `OneWayAmount` int(11) DEFAULT NULL,
  `TwoWayAmount` int(11) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  PRIMARY KEY (`TransportationId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `tbltransportation` */

insert  into `tbltransportation`(`TransportationId`,`PlaceTo`,`DistanceFrom`,`DistanceTo`,`OneWayAmount`,`TwoWayAmount`,`CompanyId`) values (2,'hattiban',0,5,300,500,1),(3,'lagankhel',0,10,200,400,1);

/*Table structure for table `tblusercontrol` */

DROP TABLE IF EXISTS `tblusercontrol`;

CREATE TABLE `tblusercontrol` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Company_ID` int(11) DEFAULT NULL,
  `Group_ID` int(11) DEFAULT NULL,
  `Feature_ID` int(11) DEFAULT NULL,
  `Action_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_usercontrol_Group` (`Group_ID`),
  KEY `FK_usercontrol_Feature` (`Feature_ID`),
  KEY `FK_usercontrol_Action` (`Action_ID`),
  CONSTRAINT `FK_usercontrol_Action` FOREIGN KEY (`Action_ID`) REFERENCES `tblfeatureaction` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_usercontrol_Feature` FOREIGN KEY (`Feature_ID`) REFERENCES `tblfeature` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_usercontrol_Group` FOREIGN KEY (`Group_ID`) REFERENCES `tblgroup` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=736 DEFAULT CHARSET=utf8;

/*Data for the table `tblusercontrol` */

insert  into `tblusercontrol`(`ID`,`Company_ID`,`Group_ID`,`Feature_ID`,`Action_ID`) values (646,1,3,5,2),(647,1,3,5,3),(648,1,3,5,4),(649,1,3,5,5),(650,1,3,5,6),(651,1,3,6,2),(652,1,3,6,3),(653,1,3,6,4),(654,1,3,6,5),(655,1,3,6,6),(656,1,3,10,2),(657,1,3,10,3),(658,1,3,10,4),(659,1,3,10,5),(660,1,3,10,6),(661,1,3,11,2),(662,1,3,11,3),(663,1,3,11,4),(664,1,3,11,5),(665,1,3,11,6),(666,1,3,12,2),(667,1,3,12,3),(668,1,3,12,4),(669,1,3,12,5),(670,1,3,12,6),(671,1,3,13,2),(672,1,3,13,3),(673,1,3,13,4),(674,1,3,13,5),(675,1,3,13,6),(676,1,3,14,2),(677,1,3,14,3),(678,1,3,14,4),(679,1,3,14,5),(680,1,3,14,6),(681,1,3,15,2),(682,1,3,15,3),(683,1,3,15,4),(684,1,3,15,5),(685,1,3,15,6),(686,1,3,16,2),(687,1,3,16,3),(688,1,3,16,4),(689,1,3,16,5),(690,1,3,16,6),(691,1,3,17,2),(692,1,3,17,3),(693,1,3,17,4),(694,1,3,17,5),(695,1,3,17,6),(696,1,3,18,2),(697,1,3,18,3),(698,1,3,18,4),(699,1,3,18,5),(700,1,3,18,6),(701,1,3,19,2),(702,1,3,19,3),(703,1,3,19,4),(704,1,3,19,5),(705,1,3,19,6),(706,1,3,20,2),(707,1,3,20,3),(708,1,3,20,4),(709,1,3,20,5),(710,1,3,20,6),(711,1,3,21,2),(712,1,3,21,3),(713,1,3,21,4),(714,1,3,21,5),(715,1,3,21,6),(716,1,3,22,2),(717,1,3,22,3),(718,1,3,22,4),(719,1,3,22,5),(720,1,3,22,6),(721,1,3,23,2),(722,1,3,23,3),(723,1,3,23,4),(724,1,3,23,5),(725,1,3,23,6),(726,1,3,24,2),(727,1,3,24,3),(728,1,3,24,4),(729,1,3,24,5),(730,1,3,24,6),(731,1,3,25,2),(732,1,3,25,3),(733,1,3,25,4),(734,1,3,25,5),(735,1,3,25,6);

/*Table structure for table `tbluserdetail` */

DROP TABLE IF EXISTS `tbluserdetail`;

CREATE TABLE `tbluserdetail` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `Password` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `CompanyId` int(11) DEFAULT NULL,
  `Role` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `tbluserdetail` */

insert  into `tbluserdetail`(`UserID`,`Password`,`Email`,`CompanyId`,`Role`) values (1,'YWRtaW4=','superadmin',0,'SuperAdmin'),(2,'MTIzNDU=','ebpearl@gmail.com',1,'Admin');

/*Table structure for table `tblusertype` */

DROP TABLE IF EXISTS `tblusertype`;

CREATE TABLE `tblusertype` (
  `UserTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `UserType` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`UserTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tblusertype` */

/* Procedure structure for procedure `checkcompanystatus` */

/*!50003 DROP PROCEDURE IF EXISTS  `checkcompanystatus` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `checkcompanystatus`(
    IN _Email varchar(50)
    )
BEGIN
	select tblcompanydetails.`Status` from tblcompanydetails
	inner join tbluserdetail on tbluserdetail.`CompanyId`=tblcompanydetails.`CompanyId`
	where tbluserdetail.`Email`=_Email;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `edit_batch` */

/*!50003 DROP PROCEDURE IF EXISTS  `edit_batch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `edit_batch`(
    IN _BatchId bigint,
    IN _FromYear Int,
    IN _ToYear int,
    IN _CompanyId int
    
    )
BEGIN
	Update tblbatchdetails
	set
	FromYear=_FromYear,
	ToYear	=_ToYear
	where BatchId=_BatchId and CompanyId=_CompanyId;

	END */$$
DELIMITER ;

/* Procedure structure for procedure `edit_studentTransport` */

/*!50003 DROP PROCEDURE IF EXISTS  `edit_studentTransport` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `edit_studentTransport`(
    IN _Id int,
    IN _Batch varchar(50),
    in _PlaceTo varchar(50),
    in _Type varchar(50),
    in _Amount int,
    in _CompanyId int
    )
BEGIN
	update tblstudenttransport
	set
	Batch=_Batch,
	PlaceTo=_PlaceTo,
	Type=_Type,
	Amount=_Amount
	where StudentTransportId=_Id and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getabsentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getabsentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getabsentattendance`(
    IN _Batch varchar(50),
    in _Class varchar(50),
    in _Section varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    )
BEGIN
	SELECT tblstudentattendance.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.`Batch`,ebmasterdb.classes.`ClassName`,ebmasterdb.sections.`SectionName`,COUNT(tblstudentattendance.`Attendance`) AS Absent
	FROM tblstudentattendance
	INNER JOIN ebmasterdb.studentinfoes ON tblstudentattendance.`StudentId`= ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`= tblstudentattendance.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE (tblstudentattendance.`Batch` LIKE CONCAT('%',_Batch,'%') OR tblstudentattendance.`Batch`='' ) 
	AND (ebmasterdb.classes.`ClassName` LIKE CONCAT('%',_Class,'%') OR ebmasterdb.classes.`ClassName`='' ) 
	AND (ebmasterdb.sections.`SectionName` LIKE CONCAT('%',_Section,'%') OR ebmasterdb.sections.`SectionName`='' ) 
	AND (tblstudentattendance.Date >= _DateFrom AND tblstudentattendance.Date <= _DateTo)
	AND tblstudentattendance.Attendance='A'
	AND tblstudentattendance.`CompanyId`=_CompanyId
	GROUP BY tblstudentattendance.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.`Batch`,ebmasterdb.classes.ClassName,ebmasterdb.sections.`SectionName`
	ORDER BY ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallabsentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallabsentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getallabsentattendance`(
    in _Batch varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    
    )
BEGIN
	SELECT tblstudentattendance.StudentId,ebmasterdb.studentinfoes.FirstName,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.Batch,ebmasterdb.classes.ClassName,ebmasterdb.sections.`SectionName`,COUNT(tblstudentattendance.`Attendance`) AS Absent
	FROM tblstudentattendance
	INNER JOIN ebmasterdb.studentinfoes ON tblstudentattendance.`StudentId`= ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`= tblstudentattendance.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE tblstudentattendance.`Batch`=_Batch
	AND (tblstudentattendance.Date >=_DateFrom AND tblstudentattendance.Date <= _DateTo)
	AND tblstudentattendance.`Attendance`='A'
	AND tblstudentattendance.`CompanyId`=_CompanyId
	GROUP BY tblstudentattendance.`Attendance`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.`Batch`,ebmasterdb.classes.ClassName,ebmasterdb.sections.`SectionName`
	ORDER BY ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallbatchclassfee` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallbatchclassfee` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getallbatchclassfee`(In _CompanyId int)
BEGIN
	Select tblbatchclass.`BatchClassId`,tblbatchclass.`Batch`,tblbatchclass.Class,tblbatchclass.`Faculty`,
	tblbatchclassfee.`FeeStructureName`,tblbatchclassfee.`StudentType`,tblbatchclassfee.`FeeName`,
	tblbatchclassfee.`Amount`,tblbatchclassfee.`Refundable`
	from tblbatchclass
	inner join tblbatchclassfee on tblbatchclass.`BatchClassId`=tblbatchclassfee.`BatchClassId`
	where tblbatchclass.`CompanyId`=_CompanyId;
	
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallcompany` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallcompany` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getallcompany`()
BEGIN
	select * from tblcompanydetails
	inner join tbluserdetail on tblcompanydetails.`CompanyId`=tbluserdetail.`CompanyId`
	where tbluserdetail.`Role`='Admin';
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getalldemobillingdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `getalldemobillingdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getalldemobillingdetails`(
	in _Batch varchar(50),
	in _Class int,
	in _Month Varchar(50),
	in _CompanyId int
    )
BEGIN
	select tblbilling.`BillingId`,tblbilling.`Batch`,tblbilling.`Month`,tblbilling.`StudentId`,tblbilling.`CreatedBy`,
	tblbilling.`Date`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblbilling.`Status`,
	tblbilling.`IsCreated`,sum(tblbillingdetails.`Amount`) as total
	from tblbillingdetails
	inner join tblbilling on tblbillingdetails.`BillingId`=tblbilling.`BillingId`
	inner join ebmasterdb.studentinfoes on tblbilling.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join ebmasterdb.currenteducations on ebmasterdb.studentinfoes.`StudentId`=ebmasterdb.currenteducations.`CurrentEduId`
	
	where tblbilling.`Batch`=_Batch and ebmasterdb.currenteducations.`ClassId`=_Class and tblbilling.`Month`=_Month and tblbilling.`CompanyId`=_CompanyId and tblbillingdetails.`CompanyId`=_CompanyId
		GROUP BY
	tblbilling.`BillingId`,
	tblbilling.`Batch`,
	tblbilling.`Month`,
	tblbilling.`StudentId`,
	tblbilling.`Date`,
	ebmasterdb.studentinfoes.`FirstName`,
	ebmasterdb.studentinfoes.`LastName`,
	tblbilling.`Status`,
	tblbilling.`IsCreated`,
	tblbilling.`CreatedBy`,
	ebmasterdb.currenteducations.`ClassId`,
	tblbilling.`CompanyId`,
	tblbillingdetails.`CompanyId`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallfollowup` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallfollowup` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getallfollowup`(IN _CompanyId int)
BEGIN
	select ebmasterdb.studentinfoes.FirstName,ebmasterdb.studentinfoes.LastName,tblfollowup.*
	from tblfollowup
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.StudentId= tblfollowup.StudentId
	where tblfollowup.CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallpresentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallpresentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getallpresentattendance`(
    IN _Batch VARCHAR(50),
    IN _DateFrom VARCHAR(50),
    IN _DateTo VARCHAR(50),
    IN _CompanyId INT
    )
BEGIN
	SELECT tblstudentattendance.StudentId,ebmasterdb.studentinfoes.FirstName,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.Batch,ebmasterdb.classes.ClassName,ebmasterdb.sections.`SectionName`,COUNT(tblstudentattendance.`Attendance`) AS Present
	FROM tblstudentattendance
	INNER JOIN ebmasterdb.studentinfoes ON tblstudentattendance.`StudentId`= ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`= tblstudentattendance.`StudentId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	inner join ebmasterdb.sections on ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE tblstudentattendance.`Batch`=_Batch
	AND (tblstudentattendance.Date >=_DateFrom AND tblstudentattendance.Date <= _DateTo)
	AND tblstudentattendance.`Attendance`='P'
	AND tblstudentattendance.`CompanyId`=_CompanyId
	GROUP BY tblstudentattendance.`Attendance`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.`Batch`,ebmasterdb.classes.ClassName,ebmasterdb.sections.`SectionName`
	ORDER BY ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallreceiptdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallreceiptdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getallreceiptdetails`(
    IN _Batch varchar(50),
    in _Class varchar(50),
    in _Month varchar(50),
    IN _CompanyId int
    
    )
BEGIN
	select ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.LastName,tblreceipt.*
	from tblreceipt
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblreceipt.`StudentId`
	inner join ebmasterdb.currenteducations on ebmasterdb.studentinfoes.`StudentId`=ebmasterdb.currenteducations.`CurrentEduId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	where tblreceipt.Batch=_Batch and tblreceipt.Month=_Month and ebmasterdb.classes.ClassName=_Class and tblreceipt.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallscholarshipdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallscholarshipdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getallscholarshipdetails`(in _CompanyId int)
BEGIN
	select Distinct ScholarshipName,Batch,Class,Faculty
	from tblscholarshipdetails
	where CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallsearchedextrafee` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallsearchedextrafee` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ebpearls`@`%` PROCEDURE `getallsearchedextrafee`(
    IN _Batch VARCHAR(50), 
    IN _Class VARCHAR(50),
    IN _Faculty VARCHAR(50),
    IN _Section VARCHAR(50),
    IN _StudentId VARCHAR(50),
    IN _Month VARCHAR(50),
    IN _CompanyId INT
    )
BEGIN
	SELECT tblstudentextrafee.`StudentExtraFeeId`,tblstudentextrafee.`Batch`,tblstudentextrafee.`Month`,tblstudentextrafee.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,
	ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	FROM tblstudentextrafee
	INNER JOIN ebmasterdb.studentinfoes ON ebmasterdb.studentinfoes.`StudentId`= tblstudentextrafee.`StudentId`
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	inner join ebmasterdb.faculties on ebmasterdb.faculties.FacultyId = ebmasterdb.currenteducations.FacultyId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE (tblstudentextrafee.`Batch` LIKE  CONCAT('%',_Batch,'%') OR tblstudentextrafee.`Batch`='')
	AND (ebmasterdb.classes.`ClassName` LIKE CONCAT('%',_Class,'%') OR ebmasterdb.classes.`ClassName`='')
	AND (ebmasterdb.faculties.`FacultyName` LIKE CONCAT('%',_Faculty,'%') OR ebmasterdb.faculties.`FacultyName`='')
	AND (ebmasterdb.sections.`SectionName` LIKE CONCAT('%',_Section,'%') OR ebmasterdb.sections.`SectionName`='')
	AND (tblstudentextrafee.`Month` LIKE CONCAT('%',_Month,'%') OR tblstudentextrafee.`Month`='')
	AND (tblstudentextrafee.`StudentId` LIKE CONCAT('%',_StudentId,'%') or tblstudentextrafee.`StudentId`='')
	AND tblstudentextrafee.`CompanyId`=_CompanyId;
	
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallsearchedtransport` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallsearchedtransport` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallsearchedtransport`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Faculty varchar(50),
    in _Section varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	SELECT tblstudenttransport.*,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	FROM tblstudenttransport
	INNER JOIN ebmasterdb.studentinfoes ON tblstudenttransport.`StudentId`= ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.faculties ON ebmasterdb.faculties.FacultyId = ebmasterdb.currenteducations.FacultyId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE (tblstudenttransport.`Batch` LIKE CONCAT('%',_Batch,'%') OR tblstudenttransport.`Batch`='')
	AND (ebmasterdb.classes.`ClassName` LIKE CONCAT('%',_Class,'%') OR ebmasterdb.classes.`ClassName`='')
	AND (ebmasterdb.faculties.`FacultyName` LIKE CONCAT('%',_Faculty,'%') OR ebmasterdb.faculties.`FacultyName`='')
	AND (ebmasterdb.sections.`SectionName` LIKE CONCAT('%',_Section,'%') OR ebmasterdb.sections.`SectionName`='')
	AND (tblstudenttransport.`StudentId` LIKE CONCAT('%',_StudentId,'%') or tblstudenttransport.`StudentId`='')
	AND tblstudenttransport.`CompanyId`=_CompanyId;
	
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallspecialscholarship` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallspecialscholarship` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallspecialscholarship`(in _CompanyId int)
BEGIN
	select distinct tblspecialscholarship.`StudentId`,tblspecialscholarship.Faculty,tblspecialscholarship.Class,tblspecialscholarship.Batch,ebmasterdb.studentinfoes.`FirstName`,
	ebmasterdb.studentinfoes.`LastName`
	from tblspecialscholarship
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblspecialscholarship.`StudentId`
	where tblspecialscholarship.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstaffabsentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstaffabsentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstaffabsentattendance`(
    in _Batch varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    )
BEGIN
	select tblteacherinfo.`TeacherId`,tblteacherinfo.`Batch`,tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblteacherinfo.`Department`,tblteacherinfo.`Faculty`,tblteacherinfo.`Designation`,count(tblstaffattendance.`Attendance`) as Absent
	from tblteacherinfo
	inner join tblstaffattendance on tblstaffattendance.`TeacherId`=tblteacherinfo.`TeacherId`
	where tblteacherinfo.`Batch` =_Batch
	And (Date >= _DateFrom and Date <= _DateTo)
	And tblstaffattendance.`Attendance`='A'
	and tblstaffattendance.`CompanyId`=_CompanyId
	group by tblteacherinfo.`TeacherId`,tblteacherinfo.`Batch`,tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblteacherinfo.`Department`,tblteacherinfo.`Designation`,tblteacherinfo.`Faculty`
	order by tblteacherinfo.`Department`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstaffpresentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstaffpresentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstaffpresentattendance`(
    IN _Batch VARCHAR(50),
    IN _DateFrom VARCHAR(50),
    IN _DateTo VARCHAR(50),
    IN _CompanyId INT
    )
BEGIN
	SELECT tblteacherinfo.`TeacherId`,tblteacherinfo.`Batch`,tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblteacherinfo.`Department`,tblteacherinfo.`Faculty`,tblteacherinfo.`Designation`,COUNT(tblstaffattendance.`Attendance`) AS Present
	FROM tblteacherinfo
	INNER JOIN tblstaffattendance ON tblstaffattendance.`TeacherId`=tblteacherinfo.`TeacherId`
	WHERE tblteacherinfo.`Batch` =_Batch
	AND (DATE >= _DateFrom AND DATE <= _DateTo)
	AND tblstaffattendance.`Attendance`='P'
	AND tblstaffattendance.`CompanyId`=_CompanyId
	GROUP BY tblteacherinfo.`TeacherId`,tblteacherinfo.`Batch`,tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblteacherinfo.`Department`,tblteacherinfo.`Designation`,tblteacherinfo.`Faculty`
	ORDER BY tblteacherinfo.`Department`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudent` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudent` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudent`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _CompanyId int
    )
BEGIN
	Select tblstudentinfo.*,tblcurrenteducation.`Class`,tblcurrenteducation.`Faculty`,tblcurrenteducation.`Section`
	From tblstudentinfo
	inner join tblcurrenteducation on tblstudentinfo.`StudentId`=tblcurrenteducation.`StudentId`
	where tblcurrenteducation.`Batch`=_Batch and tblcurrenteducation.`Class`=_Class and tblstudentinfo.`IsDeleted`=0 and tblcurrenteducation.`IsDeleted`=0 
	and tblstudentinfo.`CompanyId`=_CompanyId and tblcurrenteducation.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentextrafee` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentextrafee` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentextrafee`(IN _CompanyId int)
BEGIN
	select tblstudentextrafee.`StudentExtraFeeId`,tblstudentextrafee.`Batch`,tblstudentextrafee.`Month`,tblstudentextrafee.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	from tblstudentextrafee
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`=tblstudentextrafee.`StudentId`
	inner join ebmasterdb.currenteducations on ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	where tblstudentextrafee.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentid` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentid`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.currenteducations.`CurrentEduId`,ebmasterdb.studentinfoes.`StudentId`
	 from ebmasterdb.currenteducations
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`=ebmasterdb.CurrentEducations.`CurrentEduId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	inner join ebmasterdb.sessions on ebmasterdb.sessions.SessionId = ebmasterdb.currenteducations.SessionId
	where ebmasterdb.sessions.SessionFrom=_Batch and ebmasterdb.classes.ClassName=_Class and ebmasterdb.studentinfoes.`Status`='Active'
	and ebmasterdb.studentinfoes.`CompanyId`=_CompanyId and ebmasterdb.currenteducations.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlist` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlist` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlist`(IN _CompanyId int)
BEGIN
	SELECT ebmasterdb.studentinfoes.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	FROM ebmasterdb.studentinfoes
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	WHERE ebmasterdb.studentinfoes.`Status`='Active' AND ebmasterdb.studentinfoes.`CompanyId`=_CompanyId
	ORDER BY ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistbyclass` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistbyclass` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistbyclass`(
    in _Faculty varchar(50),
    in _Class varchar(50),
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.studentinfoes.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	FROM ebmasterdb.studentinfoes
	inner join ebmasterdb.currenteducations on ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.faculties ON ebmasterdb.faculties.FacultyId = ebmasterdb.currenteducations.FacultyId 
	where ebmasterdb.studentinfoes.`Status`='Active' and ebmasterdb.faculties.`FacultyName`=_Faculty and ebmasterdb.classes.`ClassName`=_Class and ebmasterdb.studentinfoes.`CompanyId`=_CompanyId
	order by ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistbyfaculty` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistbyfaculty` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistbyfaculty`(
    in _Faculty varchar(50),
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.studentinfoes.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	FROM ebmasterdb.studentinfoes
	inner join ebmasterdb.currenteducations on ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	inner join ebmasterdb.faculties on ebmasterdb.faculties.FacultyId = ebmasterdb.currenteducations.FacultyId
	where ebmasterdb.studentinfoes.`Status`='Active' and ebmasterdb.faculties.`FacultyName`=_Faculty and ebmasterdb.studentinfoes.`CompanyId`=_CompanyId
	order by ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistbyname` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistbyname` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistbyname`(
    in _Batch varchar(50),
    in _Faculty varchar(50),
    in _Class varchar(50),
    in _Section varchar(50),
    in _Name varchar(50),
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.studentinfoes.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	FROM ebmasterdb.studentinfoes
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	inner join ebmasterdb.sessions on ebmasterdb.sessions.SessionId = ebmasterdb.currenteducations.SessionId
	where ebmasterdb.studentinfoes.`Status`='Active'
	And (ebmasterdb.sessions.`SessionFrom` like  concat('%',_Batch,'%') or ebmasterdb.sessions.`SessionFrom`='' ) 
	And (ebmasterdb.classes.`ClassName` like concat('%',_Class,'%') or ebmasterdb.classes.`ClassName`='' ) 
	And (ebmasterdb.sections.`SectionName` like concat('%',_Section,'%') or ebmasterdb.sections.`SectionName`='' ) 
	And (ebmasterdb.studentinfoes.FirstName like concat(_Name,'%') or ebmasterdb.studentinfoes.`FirstName`='' ) 
	and ebmasterdb.studentinfoes.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistbysection` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistbysection` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistbysection`(
    in _Faculty varchar(50),
    in _Class Varchar(50),
    in _Section varchar(50),
    in _CompanyId int
    )
BEGIN
	SELECT ebmasterdb.studentinfoes.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	FROM ebmasterdb.studentinfoes
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.faculties ON ebmasterdb.faculties.FacultyId = ebmasterdb.currenteducations.FacultyId 
	inner join ebmasterdb.sections on ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE ebmasterdb.studentinfoes.`Status`='Active' AND ebmasterdb.faculties.`FacultyName`=_Faculty AND ebmasterdb.classes.`ClassName`=_Class and ebmasterdb.sections.SectionName=_Section AND ebmasterdb.studentinfoes.`CompanyId`=_CompanyId
	ORDER BY ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistdue` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistdue` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistdue`(in _CompanyId int)
BEGIN
	select FirstName,LastName,StudentId,Batch,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,CompanyId
	having  _totaldue.CompanyId=_CompanyId
	order by Class;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistduebybatch` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistduebybatch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistduebybatch`(
    in _Batch varchar(50),
    in _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,CompanyId
	having _totaldue.`Batch`=_Batch and _totaldue.`CompanyId`=_CompanyId
	order by Class;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistduebybatchclass` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistduebybatchclass` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistduebybatchclass`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,CompanyId
	having _totaldue.`Batch`=_Batch and _totaldue.`Class`=_Class and _totaldue.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistduebyclass` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistduebyclass` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistduebyclass`(
    in _Class varchar(50),
    in _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,CompanyId
	having _totaldue.`Class`=_Class and _totaldue.CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudentlistduebystudentid` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudentlistduebystudentid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudentlistduebystudentid`(
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,CompanyId
	having StudentId=_StudentId and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallstudenttransport` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallstudenttransport` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallstudenttransport`(in _CompanyId int)
BEGIN
	select tblstudenttransport.*,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`
	from tblstudenttransport
	inner join ebmasterdb.studentinfoes on tblstudenttransport.`StudentId`= ebmasterdb.studentinfoes.`StudentId`
	inner join ebmasterdb.currenteducations on ebmasterdb.currenteducations.`CurrentEduId`= ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId 
	where tblstudenttransport.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getallteacher` */

/*!50003 DROP PROCEDURE IF EXISTS  `getallteacher` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getallteacher`(in _CompanyId int)
BEGIN
	select TeacherId,FirstName,LastName,Designation,Department,Faculty,Gender,JoiningDate,Status
	from tblteacherinfo 
	where IsDeleted=0 and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getalltransportbyid` */

/*!50003 DROP PROCEDURE IF EXISTS  `getalltransportbyid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getalltransportbyid`(
     in _Batch varchar(50),
     in _Month varchar(50),
     in _StudentId varchar(50),
     in _CompanyId int
    )
BEGIN
	select tblbillingdetails.`FeeName`, tblbillingdetails.`FeeStructureName`
	from tblbillingdetails
	inner join tblbilling on tblbillingdetails.`BillingId`=tblbilling.`BillingId`
	where tblbilling.`Batch`=_Batch and tblbilling.`Month`=_Month and tblbilling.`StudentId`=_StudentId and tblbillingdetails.`FeeName`='Transport Fee'
	and tblbillingdetails.`CompanyId`=_CompanyId and tblbilling.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getattendancedetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `getattendancedetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getattendancedetails`(
    in _Batch varchar(50),
    in _StudentId varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.* from tblstudentattendance
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblstudentattendance.`StudentId`
	where tblstudentattendance.`Batch`=_Batch
	and tblstudentattendance.`StudentId`=_StudentId
	and tblstudentattendance.Date >=_DateFrom and tblstudentattendance.Date <= _DateTo
	and tblstudentattendance.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getcolumnchartdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `getcolumnchartdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getcolumnchartdetails`(
    IN _Batch varchar(50),
    in _CompanyId int
    )
BEGIN
	select * from
	(select tblbilling.Month, Sum(tblbillingdetails.Amount) as Total 
	from tblbilling
	inner join tblbillingdetails on tblbilling.`BillingId` = tblbillingdetails.`BillingId`
	inner join ebmasterdb.studentinfoes on tblbilling.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	group by tblbilling.Month,tblbilling.`Batch`,tblbilling.`CompanyId`
	having tblbilling.`Batch`=_Batch and tblbilling.`CompanyId`=_CompanyId) as t1
	left join
	(select tblreceipt.`Month`, Sum(tblreceipt.PaidAmount) as Paid from tblreceipt
	INNER JOIN ebmasterdb.studentinfoes ON tblreceipt.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join tblbilling ON tblbilling.`BillingId`= tblreceipt.`BillingId`
	group by tblreceipt.`Month`,tblbilling.`Batch`,tblreceipt.`CompanyId`
	having tblbilling.`Batch`=_Batch and tblreceipt.`CompanyId`=_CompanyId) as t2
	on t1.Month = t2.Month
	left join
	(select tblreceipt.`Month`, Sum(tblreceipt.Discount) as Discount from tblreceipt
	inner join ebmasterdb.studentinfoes on tblreceipt.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join tblbilling ON tblbilling.`BillingId`= tblreceipt.`BillingId`
	group by tblreceipt.`Month`,tblbilling.`Batch`,tblreceipt.`CompanyId`
	having tblbilling.`Batch`=_Batch and tblreceipt.`CompanyId`=_CompanyId) as t3
	on t1.Month = t3.Month
	left join
	(select tblreceipt.`Month`, Sum(tblreceipt.Fine) as Fine from tblreceipt
	inner join ebmasterdb.studentinfoes on tblreceipt.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join tblbilling ON tblbilling.`BillingId`=tblreceipt.`BillingId`
	group by tblreceipt.`Month`,tblbilling.`Batch`,tblreceipt.`CompanyId`
	having tblbilling.`Batch`=_Batch and tblreceipt.`CompanyId`=_CompanyId) as t4
	on t1.Month = t4.Month
	order by t1.Month;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getcolumnchartdetailsbybatch` */

/*!50003 DROP PROCEDURE IF EXISTS  `getcolumnchartdetailsbybatch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getcolumnchartdetailsbybatch`(in _CompanyId int)
BEGIN
	select * from
	(select tblbilling.`Batch`, Sum(tblbillingdetails.Amount) as Total from tblbilling
	inner join tblbillingdetails on tblbilling.`BillingId` = tblbillingdetails.`BillingId`
	inner join ebmasterdb.studentinfoes on tblbilling.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	where tblbilling.`CompanyId`=_CompanyId
	group by tblbilling.`Batch`) as t1
	left join
	(select tblbilling.`Batch`, Sum(tblreceipt.PaidAmount) as Paid from tblreceipt
	inner join ebmasterdb.studentinfoes on tblreceipt.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join tblbilling ON tblbilling.`BillingId`= tblreceipt.`BillingId`
	where tblreceipt.`CompanyId`=_CompanyId
	group by tblbilling.`Batch`) as t2
	on t1.Batch = t2.Batch
	left join
	(select tblbilling.`Batch`, Sum(tblreceipt.Discount) as Discount from tblreceipt
	inner join ebmasterdb.studentinfoes on tblreceipt.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join tblbilling ON tblbilling.`BillingId`= tblreceipt.`BillingId`
	where tblreceipt.`CompanyId`=_CompanyId
	group by tblbilling.`Batch`) as t3
	on t1.Batch = t3.Batch
	left join
	(select tblbilling.`Batch`, Sum(tblreceipt.Fine) as Fine from tblreceipt
	inner join ebmasterdb.studentinfoes on tblreceipt.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join tblbilling ON tblbilling.`BillingId`= tblreceipt.`BillingId`
	where tblreceipt.`CompanyId`=_CompanyId
	group by tblbilling.`Batch`) as t4
	on t1.Batch = t4.Batch;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getcompanydetailsbyid` */

/*!50003 DROP PROCEDURE IF EXISTS  `getcompanydetailsbyid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getcompanydetailsbyid`(IN _CompanyId int)
BEGIN
	select * from tblcompanydetails
	inner join tbluserdetail on tblcompanydetails.`CompanyId`=tbluserdetail.`CompanyId`
	where tblcompanydetails.`CompanyId`=_CompanyId and tbluserdetail.`Role`='Admin';
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getduedetailsbystudent` */

/*!50003 DROP PROCEDURE IF EXISTS  `getduedetailsbystudent` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getduedetailsbystudent`(
    in _Batch varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,ebmasterdb.classes.`ClassName`,sum(tblbillingdetails.`Amount`) as Total 
	from tblbilling
	inner join tblbillingdetails on tblbilling.`BillingId`= tblbillingdetails.`BillingId`
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblbilling.`StudentId`
	inner join ebmasterdb.currenteducations on ebmasterdb.studentinfoes.`StudentId`=ebmasterdb.currenteducations.`CurrentEduId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	group by ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`StudentId`,tblbilling.`IsCreated`,tblbilling.`BillingId`,tblbilling.`Status`,ebmasterdb.classes.`ClassName`,tblbilling.`CompanyId`
	having tblbilling.`StudentId`=_StudentId and tblbilling.`Batch`=_Batch and tblbilling.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getduelist` */

/*!50003 DROP PROCEDURE IF EXISTS  `getduelist` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getduelist`(
    in _Batch varchar(50),
    IN _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,CompanyId,Batch,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,CompanyId,Batch
	having _totaldue.`Batch`=_Batch and _totaldue.`CompanyId`=_CompanyId
	order by Due desc;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getextrafeebyid` */

/*!50003 DROP PROCEDURE IF EXISTS  `getextrafeebyid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getextrafeebyid`(
    in _Batch varchar(50),
    in _Month varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select tblbillingdetails.`FeeName`
	from tblbillingdetails
	inner join tblbilling on tblbillingdetails.`BillingId`=tblbilling.`BillingId`
	where tblbilling.`Batch`=_Batch and tblbilling.`Month`=_Month and tblbilling.`StudentId`=_StudentId and tblbillingdetails.`FeeStructureName`='Extra'
	and tblbilling.`CompanyId`=_CompanyId and tblbillingdetails.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getmonthlyfeebyid` */

/*!50003 DROP PROCEDURE IF EXISTS  `getmonthlyfeebyid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getmonthlyfeebyid`(
    in _Batch varchar(50),
    in _Month varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select tblbillingdetails.FeeStructureName from tblbillingdetails
	inner join tblbilling on tblbillingdetails.`BillingId` = tblbilling.`BillingId`
	where tblbilling.`StudentId` =_StudentId and tblbilling.`Batch`=_Batch and tblbilling.`Month`=_Month 
	and tblbillingdetails.`FeeStructureName` != 'Initial' and tblbillingdetails.`FeeStructureName` != 'Yearly'
	and tblbilling.`CompanyId`=_CompanyId and tblbillingdetails.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getpartialduedetailsbystudent` */

/*!50003 DROP PROCEDURE IF EXISTS  `getpartialduedetailsbystudent` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getpartialduedetailsbystudent`(
    IN _Batch varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,ebmasterdb.classes.`ClassName`,sum(tblbillingdetails.`Amount`) as Total,0 as due  
from tblbilling
	inner join tblbillingdetails on tblbilling.`BillingId`= tblbillingdetails.`BillingId`
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblbilling.`StudentId`
	inner join ebmasterdb.currenteducations on ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	group by ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Status`,ebmasterdb.classes.`ClassName`,tblbilling.`CompanyId`
	having tblbilling.`Status`='Partial Paid' and tblbilling.`StudentId`=_StudentId and tblbilling.`Batch`=_Batch and tblbilling.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getpresentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getpresentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getpresentattendance`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Section varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    )
BEGIN
	Select tblstudentattendance.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.`Batch`,ebmasterdb.classes.`ClassName`,ebmasterdb.sections.`SectionName`,count(tblstudentattendance.`Attendance`) as Present
	FROM tblstudentattendance
	INNER JOIN ebmasterdb.studentinfoes ON tblstudentattendance.`StudentId`= ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`= tblstudentattendance.`StudentId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	where (tblstudentattendance.`Batch` like concat('%',_Batch,'%') or tblstudentattendance.`Batch`='' ) 
	And (ebmasterdb.classes.`ClassName` like concat('%',_Class,'%') or ebmasterdb.classes.`ClassName`='' ) 
	And (ebmasterdb.sections.`SectionName` like concat('%',_Section,'%') or ebmasterdb.sections.`SectionName`='' ) 
	And (tblstudentattendance.Date >= _DateFrom and tblstudentattendance.Date <= _DateTo)
	And tblstudentattendance.Attendance='P'
	and tblstudentattendance.`CompanyId`=_CompanyId
	GROUP BY tblstudentattendance.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblstudentattendance.`Batch`,ebmasterdb.classes.ClassName,ebmasterdb.sections.`SectionName`
	ORDER BY ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getpreviousdue` */

/*!50003 DROP PROCEDURE IF EXISTS  `getpreviousdue` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getpreviousdue`(
    in _StudentId varchar(50),
    in _Month int
    )
BEGIN
	select ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.studentinfoes.`FatherName`,ebmasterdb.studentinfoes.`FatherNumber`,ebmasterdb.studentinfoes.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`,sum(tblbillingdetails.`Amount`) as Total ,min(tblreceipt.`DueAmount`) as due 
	from tblbilling
	inner join tblbillingdetails on tblbilling.`BillingId`= tblbillingdetails.`BillingId`
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblbilling.`StudentId`
	inner join tblreceipt on tblbilling.`BillingId`= tblreceipt.`BillingId`
	group by ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.studentinfoes.`FatherName`,ebmasterdb.studentinfoes.`FatherNumber`,ebmasterdb.studentinfoes.`MotherNumber`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`StudentId`,tblbilling.`Status`,tblbilling.`BillingId`
	having tblbilling.`Status`='Unpaid' and tblbilling.`StudentId`=_StudentId and tblbilling.`Month` < _Month
	union
	select ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.studentinfoes.`FatherName`,ebmasterdb.studentinfoes.`FatherNumber`,ebmasterdb.studentinfoes.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`,sum(tblbillingdetails.`Amount`) as Total,sum(tblbillingdetails.`Amount`) as due  
	from tblbilling
	inner join tblbillingdetails on tblbilling.`BillingId`= tblbillingdetails.`BillingId`
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblbilling.`StudentId`
	group by ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.studentinfoes.`FatherName`,ebmasterdb.studentinfoes.`FatherNumber`,ebmasterdb.studentinfoes.`MotherNumber`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`StudentId`,tblbilling.`Status`,tblbilling.`IsCreated`,tblbilling.`BillingId`
	having tblbilling.`Status`='Unpaid' and tblbilling.`IsCreated`='0' and tblbilling.`StudentId`=_StudentId and tblbilling.`Month` < _Month;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getprintbillingdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `getprintbillingdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getprintbillingdetails`(
    IN _BillingId int,
    in _CompanyId int    
    )
BEGIN
	SELECT ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.`ClassName`,ebmasterdb.sections.`SectionName`,tblbilling.*
	from tblbilling
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`=tblbilling.`StudentId`
	inner join ebmasterdb.currenteducations on ebmasterdb.currenteducations.`CurrentEduId`=tblbilling.`StudentId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	inner join ebmasterdb.sections on ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	where tblbilling.`BillingId`=_BillingId and tblbilling.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getprintbillinginfo` */

/*!50003 DROP PROCEDURE IF EXISTS  `getprintbillinginfo` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getprintbillinginfo`(
    in _BillingId int,
    in _CompanyId int
    )
BEGIN
	select * from tblbillingdetails
	where BillingId=_BillingId and Amount !=0 and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getprintreceiptinfo` */

/*!50003 DROP PROCEDURE IF EXISTS  `getprintreceiptinfo` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getprintreceiptinfo`(
    in _ReceiptId int,
    in _CompanyId int    
    )
BEGIN
	Select * from tblreceipt
	where ReceiptId=_ReceiptId and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getreceiptdetail` */

/*!50003 DROP PROCEDURE IF EXISTS  `getreceiptdetail` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getreceiptdetail`(
    in _Id varchar(50),
    in _CompanyId int
    )
BEGIN
	select tblbilling.`Batch`,ebmasterdb.classes.`ClassName`,ebmasterdb.faculties.`FacultyName`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblbilling.*,sum(tblbillingdetails.`Amount`) as Total
	from ebmasterdb.studentinfoes
	inner join tblbilling on tblbilling.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join tblbillingdetails on tblbilling.`BillingId`=tblbillingdetails.`BillingId`
	inner join ebmasterdb.currenteducations on ebmasterdb.currenteducations.CurrentEduId = ebmasterdb.studentinfoes.StudentId
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	inner join ebmasterdb.faculties on ebmasterdb.faculties.FacultyId = ebmasterdb.currenteducations.FacultyId
	where tblbilling.`BillingId`=_Id and tblbilling.`CompanyId`=_CompanyId and tblbillingdetails.`CompanyId`=_CompanyId
	group by
	tblbilling.`Batch`,
	ebmasterdb.classes.`ClassName`,
	ebmasterdb.faculties.`FacultyName`,
	ebmasterdb.studentinfoes.`FirstName`,
	ebmasterdb.studentinfoes.`LastName`,
	tblbilling.`BillingId`,
	tblbilling.`StudentId`,
	tblbilling.`IsCreated`,
	tblbilling.`Date`,
	tblbilling.`Month`,
	tblbilling.`CreatedBy`,
	tblbilling.`Status`,
	tblbilling.`CompanyId`,
	tblbillingdetails.`CompanyId`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedattendance`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Section varchar(50),
    in _Date varchar(50)    
    )
BEGIN
	Select tblstudentattendance.`Date`,tblstudentattendance.`StudentId`,tblstudentattendance.`Batch`,ebmasterdb.classes.`ClassName`,ebmasterdb.sections.`SectionName`,tblstudentattendance.`Attendance`
	from tblstudentattendance
	inner join ebmasterdb.currenteducations on ebmasterdb.currenteducations.`CurrentEduId`= tblstudentattendance.`StudentId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	inner join ebmasterdb.sections on ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	where (tblstudentattendance.`Batch` like concat('%',_Batch,'%') or tblstudentattendance.`Batch`='' ) 
	And (ebmasterdb.classes.`ClassName` like concat('%',_Class,'%') or ebmasterdb.classes.`ClassName`='' ) 
	And (ebmasterdb.sections.`SectionName` like concat('%',_Section,'%') or ebmasterdb.sections.`SectionName`='' ) 
	And (tblstudentattendance.`Date` like concat('%',_Date,'%'))
	order by ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedbilling` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedbilling` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedbilling`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Month varchar(50),
    in _Status Varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int    
    )
BEGIN
	
	
	SELECT tblbilling.`BillingId`,tblbilling.`Batch`,tblbilling.`Month`,tblbilling.`StudentId`,tblbilling.`CreatedBy`,tblbilling.`Date`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblbilling.`Status`,tblbilling.`IsCreated`, SUM(tblbillingdetails.`Amount`) AS total
	FROM tblbillingdetails
	INNER JOIN tblbilling ON tblbillingdetails.`BillingId`= tblbilling.`BillingId`
	INNER JOIN ebmasterdb.studentinfoes ON tblbilling.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.currenteducations ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	WHERE (tblbilling.`Batch` LIKE  CONCAT('%',_Batch,'%') OR tblBilling.`Batch`='' ) 
	AND (ebmasterdb.classes.`ClassName` LIKE CONCAT('%',_Class,'%') OR ebmasterdb.classes.`ClassName`='' ) 
	AND (tblbilling.`Month` LIKE CONCAT('%',_Month,'%') OR tblbilling.`Month`='' ) 
	AND (tblbilling.`Status` LIKE CONCAT('',_Status,'%') OR tblbilling.`Status`='' ) 
	AND (tblbilling.`StudentId` LIKE CONCAT('%',_StudentId,'%') or tblbilling.`StudentId`='' )
	AND tblbilling.`CompanyId`=_CompanyId AND tblbillingdetails.`CompanyId`=_CompanyId
	GROUP BY 
	tblbilling.`BillingId`,
	tblbilling.`Batch`,
	tblbilling.`Month`,
	tblbilling.`StudentId`,
	tblbilling.`Date`,
	ebmasterdb.studentinfoes.`FirstName`,
	ebmasterdb.studentinfoes.`LastName`,
	tblbilling.`Status`,
	tblbilling.`CreatedBy`,
	tblbilling.`IsCreated`;
	
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedclassfee` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedclassfee` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedclassfee`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Faculty varchar(50),
    in _CompanyId int    
    )
BEGIN
	SELECT tblbatchclass.`BatchClassId`,tblbatchclass.`Batch`,tblbatchclass.`Class`,tblbatchclass.`Faculty`,tblbatchclassfee.`StudentType`,
	tblbatchclassfee.`FeeStructureName`,tblbatchclassfee.`FeeName`,tblbatchclassfee.`Amount`,tblbatchclassfee.`Refundable` from tblbatchclass
	inner join tblbatchclassfee on tblbatchclass.`BatchClassId`=tblbatchclassfee.`BatchClassId`
	where (tblbatchclass.`Batch` like concat('%',_Batch,'%') or tblbatchclass.`Batch`='' ) 
	And (tblbatchclass.`Class` like concat('%',_Class,'%') or tblbatchclass.`Class`='' ) 
	and (tblbatchclass.`Faculty` like concat('%',_Faculty,'%') or tblbatchclass.`Faculty`='') 
	and tblbatchclass.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearcheddayoff` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearcheddayoff` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearcheddayoff`(
    in _Batch varchar(50),
    in _Department varchar(50),
    in _Faculty varchar(50),
    in _Class varchar(50),
    in _CompanyId int
    )
BEGIN
	select * from tbldayoffdetails
	inner join tbldayoff on tbldayoff.`DayOffId` = tbldayoffdetails.`DayOffId`
	where (tbldayoff.`Batch` like  concat('%',_Batch,'%') or tbldayoff.`Batch`='' ) 
	And (tbldayoffdetails.`Department` like CONCAT('%',_Department,'%') or tbldayoffdetails.`Department`='' )
	AND (tbldayoffdetails.`Faculty` like CONCAT('%',_Faculty,'%') or tbldayoffdetails.`Faculty`='' or tbldayoffdetails.`Faculty` Is null)
	AND (tbldayoffdetails.`Class` like Concat('%',_Class,'%') or tbldayoffdetails.`Class`='' or tbldayoffdetails.`Class` Is null)
	AND tbldayoff.`CompanyId`=_CompanyId
	And tbldayoffdetails.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduebybatch` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduebybatch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduebybatch`(
    IN _Batch varchar(50),
    in _CompanyId int    
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,CompanyId
	having Batch=_Batch and CompanyId=_CompanyId
	order by Due DESC;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduebybatchclass` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduebybatchclass` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduebybatchclass`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,CompanyId
	having Batch=_Batch and Class=_Class and CompanyId=_CompanyId
	order by Due desc;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduebybatchclassmonth` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduebybatchclassmonth` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduebybatchclassmonth`(
    in _Class varchar(50),
    in _Month varchar(50),
    in _Batch varchar(50),
    in _CompanyId int    
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Month,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,Month,CompanyId
	having Batch=_Batch and Class=_Class and Month=_Month and CompanyId=_CompanyId
	order by Due desc;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduebybatchmonth` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduebybatchmonth` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduebybatchmonth`(
    in _Batch varchar(50),
    in _Month varchar(50),
    in _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Month,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,Month,CompanyId
	having Batch=_Batch and Month=_Month and CompanyId=_CompanyId
	order by Due desc;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduebyclass` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduebyclass` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduebyclass`(
    in _Class varchar(50),
    in _CompanyId int    
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,CompanyId
	having Class=_Class and CompanyId=_CompanyId
	order by Due desc;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduebyclassmonth` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduebyclassmonth` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduebyclassmonth`(
    in _Class varchar(50),
    in _Month varchar(50),
    in _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Month,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,Month,CompanyId
	having Class=@Class and Month=_Month and CompanyId=_CompanyId
	order by Due DESC;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduebymonth` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduebymonth` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduebymonth`(
    in _Month varchar(50),
    in _CompanyId int    
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,Month,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Month,CompanyId
	having Month=_Month and CompanyId=_CompanyId
	order by Due DESC;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduebystudentid` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduebystudentid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduebystudentid`(
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select FirstName,LastName,StudentId,Batch,CompanyId,Class,sum(due) as Due from _totaldue
	group by FirstName,LastName,StudentId,Batch,Class,CompanyId
	having StudentId=_StudentId and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedduereport` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedduereport` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedduereport`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Month varchar(50),
    in _StudentId varchar(50)
    
    )
BEGIN
	if _StudentId ='' then
	set _StudentId=null;
	select tblstudentinfo.`FirstName`,tblstudentinfo.`LastName`,tblstudentinfo.`FatherName`,tblstudentinfo.`FatherNumber`,tblstudentinfo.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`,sum(tblbillingdetails.`Amount`) as Total ,min(tblreceipt.`DueAmount`) as due from tblbilling
	inner join tblbillingdetails on tblbilling.`BillingId`=tblbillingdetails.`BillingId`
	inner join tblstudentinfo on tblstudentinfo.`StudentId`=tblbilling.`StudentId`
	inner join tblreceipt on tblbilling.`BillingId`=tblreceipt.`BillingId`
	inner join tblcurrenteducation on tblcurrenteducation.`StudentId`=tblstudentinfo.`StudentId`
	where (tblbilling.`Batch` like concat('%',_Batch,'%') or tblBilling.`Batch`='' ) 
	And (tblcurrenteducation.`Class` like concat('%',_Class,'%') or tblcurrenteducation.`Class`='' ) 
	And (tblbilling.`Month` like concat('%',_Month,'%') or tblbilling.`Month`='' ) 
	And (tblbilling.`StudentId` is null or tblbilling.`StudentId` like concat('%',_StudentId,'%'))
	group by tblstudentinfo.`FirstName`,tblstudentinfo.`LastName`,tblstudentinfo.`FatherName`,tblstudentinfo.`FatherNumber`,tblstudentinfo.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`
	union
	SELECT tblstudentinfo.`FirstName`,tblstudentinfo.`LastName`,tblstudentinfo.`FatherName`,tblstudentinfo.`FatherNumber`,tblstudentinfo.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`,SUM(tblbillingdetails.`Amount`) AS Total ,Sum(tblbillingdetails.`DueAmount`) AS due FROM tblbilling
	INNER JOIN tblbillingdetails ON tblbilling.`BillingId`=tblbillingdetails.`BillingId`
	INNER JOIN tblstudentinfo ON tblstudentinfo.`StudentId`=tblbilling.`StudentId`
	INNER JOIN tblreceipt ON tblbilling.`BillingId`=tblreceipt.`BillingId`
	INNER JOIN tblcurrenteducation ON tblcurrenteducation.`StudentId`=tblstudentinfo.`StudentId`
	WHERE (tblbilling.`Batch` LIKE CONCAT('%',_Batch,'%') OR tblBilling.`Batch`='' ) 
	AND (tblcurrenteducation.`Class` LIKE CONCAT('%',_Class,'%') OR tblcurrenteducation.`Class`='' ) 
	AND (tblbilling.`Month` LIKE CONCAT('%',_Month,'%') OR tblbilling.`Month`='' ) 
	AND (tblbilling.`StudentId` IS NULL OR tblbilling.`StudentId` LIKE CONCAT('%',_StudentId,'%'))
	and tblbilling.`IsCreated`=0 
	GROUP BY tblstudentinfo.`FirstName`,tblstudentinfo.`LastName`,tblstudentinfo.`FatherName`,tblstudentinfo.`FatherNumber`,tblstudentinfo.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`;
	else
	SELECT tblstudentinfo.`FirstName`,tblstudentinfo.`LastName`,tblstudentinfo.`FatherName`,tblstudentinfo.`FatherNumber`,tblstudentinfo.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`,SUM(tblbillingdetails.`Amount`) AS Total ,MIN(tblreceipt.`DueAmount`) AS due FROM tblbilling
	INNER JOIN tblbillingdetails ON tblbilling.`BillingId`=tblbillingdetails.`BillingId`
	INNER JOIN tblstudentinfo ON tblstudentinfo.`StudentId`=tblbilling.`StudentId`
	INNER JOIN tblreceipt ON tblbilling.`BillingId`=tblreceipt.`BillingId`
	INNER JOIN tblcurrenteducation ON tblcurrenteducation.`StudentId`=tblstudentinfo.`StudentId`
	WHERE (tblbilling.`Batch` LIKE CONCAT('%',_Batch,'%') OR tblBilling.`Batch`='' ) 
	AND (tblcurrenteducation.`Class` LIKE CONCAT('%',_Class,'%') OR tblcurrenteducation.`Class`='' ) 
	AND (tblbilling.`Month` LIKE CONCAT('%',_Month,'%') OR tblbilling.`Month`='' ) 
	AND (tblbilling.`StudentId` LIKE CONCAT('%',_StudentId,'%'))
	GROUP BY tblstudentinfo.`FirstName`,tblstudentinfo.`LastName`,tblstudentinfo.`FatherName`,tblstudentinfo.`FatherNumber`,tblstudentinfo.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`
	UNION
	SELECT tblstudentinfo.`FirstName`,tblstudentinfo.`LastName`,tblstudentinfo.`FatherName`,tblstudentinfo.`FatherNumber`,tblstudentinfo.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`,SUM(tblbillingdetails.`Amount`) AS Total ,SUM(tblbillingdetails.`DueAmount`) AS due FROM tblbilling
	INNER JOIN tblbillingdetails ON tblbilling.`BillingId`=tblbillingdetails.`BillingId`
	INNER JOIN tblstudentinfo ON tblstudentinfo.`StudentId`=tblbilling.`StudentId`
	INNER JOIN tblreceipt ON tblbilling.`BillingId`=tblreceipt.`BillingId`
	INNER JOIN tblcurrenteducation ON tblcurrenteducation.`StudentId`=tblstudentinfo.`StudentId`
	WHERE (tblbilling.`Batch` LIKE CONCAT('%',_Batch,'%') OR tblBilling.`Batch`='' ) 
	AND (tblcurrenteducation.`Class` LIKE CONCAT('%',_Class,'%') OR tblcurrenteducation.`Class`='' ) 
	AND (tblbilling.`Month` LIKE CONCAT('%',_Month,'%') OR tblbilling.`Month`='' ) 
	AND (tblbilling.`StudentId` LIKE CONCAT('%',_StudentId,'%'))
	AND tblbilling.`IsCreated`=0 
	GROUP BY tblstudentinfo.`FirstName`,tblstudentinfo.`LastName`,tblstudentinfo.`FatherName`,tblstudentinfo.`FatherNumber`,tblstudentinfo.`MotherNumber`,tblbilling.`StudentId`,tblbilling.`BillingId`,tblbilling.`Month`,tblbilling.`Batch`,tblbilling.`Status`;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedexpectedpayment` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedexpectedpayment` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedexpectedpayment`(
    in _FromDate varchar(50),
    in _ToDate varchar(50),
    in _CompanyId int
    )
BEGIN
	Select tblfollowup.*,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`
	from tblfollowup
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblfollowup.`StudentId`
	where tblfollowup.`ExpectedDate` between _FromDate and _ToDate
	and tblfollowup.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedfee` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedfee` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedfee`(
    in _Name varchar(50),
    in _Type varchar(50),
    in _CompanyId int
    
    )
BEGIN
	select * from tblfeedetails
	where (FeeStructureName like  concat('%',_Name,'%') or FeeStructureName='' ) 
	And (StudentType like concat(_Type,'%') or StudentType='' ) 
	and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedfollowup` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedfollowup` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedfollowup`(
    in _FromDate varchar(50),
    in _ToDate varchar(50),
    in _CompanyId int    
    )
BEGIN
	Select tblfollowup.*,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`
	from tblfollowup
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblfollowup.`StudentId`
	where tblfollowup.`FollowUpDate` between _FromDate and _ToDate
	and tblfollowup.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedreceipt` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedreceipt` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedreceipt`(
    IN _Batch varchar(50),
    in _Class varchar(50),
    in _Month varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,tblreceipt.*
	from tblreceipt
	INNER JOIN ebmasterdb.studentinfoes ON tblreceipt.`StudentId`=ebmasterdb.studentinfoes.`StudentId`
	inner join ebmasterdb.currenteducations on tblreceipt.`StudentId`=ebmasterdb.currenteducations.`CurrentEduId`
	inner join ebmasterdb.classes on ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	where (tblreceipt.`Batch` like  concat('%',_Batch,'%') or tblreceipt.`Batch`='' ) 
	And (ebmasterdb.classes.`ClassName` like concat('%',_Class,'%') or ebmasterdb.classes.`ClassName`='' ) 
	And (tblreceipt.`Month` like concat ('%',_Month,'%') or tblreceipt.`Month`='' ) 
	And (tblreceipt.`StudentId` like concat('%',_StudentId,'%') or tblreceipt.`StudentId`='' ) 
	and tblreceipt.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedscholarship` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedscholarship` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedscholarship`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select distinct tblspecialscholarship.`StudentId`,tblspecialscholarship.Faculty,tblspecialscholarship.Class,tblspecialscholarship.`Batch`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`
	from tblspecialscholarship
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= tblspecialscholarship.`StudentId`
	where (tblspecialscholarship.`Batch` like  concat('%',_Batch,'%') or tblspecialscholarship.`Batch`='' ) 
	And (tblspecialscholarship.`Class` like Concat('%',_Class,'%') or tblspecialscholarship.`Class`='' ) 
	and (tblspecialscholarship.`StudentId` like concat('%',_StudentId,'%') or tblspecialscholarship.`StudentId`='')
	and tblspecialscholarship.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getsearchedstudent` */

/*!50003 DROP PROCEDURE IF EXISTS  `getsearchedstudent` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getsearchedstudent`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Section varchar(50),
    in _FirstName varchar(50),
    in _Type varchar(50),
    in _CompanyId int
    )
BEGIN
	Select tblstudentinfo.*,tblcurrenteducation.`Class`,tblcurrenteducation.`Faculty`,tblcurrenteducation.`Section`
	From tblstudentinfo
	inner join tblcurrenteducation on tblstudentinfo.`StudentId`=tblcurrenteducation.`StudentId`
	where (tblcurrenteducation.`Batch` like  concat('%',_Batch,'%') or tblcurrenteducation.`Batch`='' ) 
	And (tblcurrenteducation.`Class` like concat('%',_Class,'%') or tblcurrenteducation.`Class`='' ) 
	And (tblcurrenteducation.`Section` like concat('%',_Section,'%') or tblcurrenteducation.`Section`='' ) 
	And (tblstudentinfo.`FirstName` like concat(_FirstName,'%') or tblstudentinfo.`FirstName`='' ) 
	And (tblstudentinfo.`StudentType` like concat('%',_Type,'%') or tblstudentinfo.`StudentType`='' ) 
	And(tblstudentinfo.`IsDeleted`=0 and tblcurrenteducation.`IsDeleted`=0)
	and tblstudentinfo.`CompanyId`=_CompanyId AND tblcurrenteducation.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstaffabsentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstaffabsentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstaffabsentattendance`(
    in _Batch varchar(50),
    in _Department varchar(50),
    in _Designation varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    )
BEGIN
	select tblteacherinfo.`TeacherId`,tblteacherinfo.`Batch`,tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblteacherinfo.`Department`,tblteacherinfo.`Faculty`,tblteacherinfo.`Designation`,count(tblstaffattendance.`Attendance`) as Absent
	from tblteacherinfo
	inner join tblstaffattendance on tblstaffattendance.`TeacherId`=tblteacherinfo.`TeacherId`
	where (tblteacherinfo.`Batch` like  concat('%',_Batch,'%') or tblteacherinfo.`Batch`='' ) 
	And (tblteacherinfo.`Department` like concat('%',_Department,'%') or tblteacherinfo.`Department`='' ) 
	And (tblteacherinfo.`Designation` like concat('%',_Designation,'%') or tblteacherinfo.`Designation`='' ) 
	And (Date >= _DateFrom and Date <= _DateTo)
	And tblstaffattendance.`Attendance`='A'
	and tblstaffattendance.`CompanyId`=_CompanyId
	group by tblteacherinfo.`TeacherId`,tblteacherinfo.`Batch`,tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblteacherinfo.`Department`,tblteacherinfo.`Designation`,tblteacherinfo.`Faculty`
	order by tblteacherinfo.`Department`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstaffattendancedetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstaffattendancedetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstaffattendancedetails`(
    in _Batch varchar(50),
    in _TeacherId varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int    
    )
BEGIN
	select tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblstaffattendance.* from tblstaffattendance
	inner join tblteacherinfo on tblteacherinfo.`TeacherId`=tblstaffattendance.`TeacherId`
	where tblstaffattendance.`Batch`=_Batch
	and tblstaffattendance.`TeacherId`=_TeacherId
	and Date >=_DateFrom and Date <= _DateTo
	and tblstaffattendance.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstaffattendancelistbybatch` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstaffattendancelistbybatch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstaffattendancelistbybatch`(
    in _Batch varchar(50),
    in _Date varchar(50),
    in _CompanyId int    
    )
BEGIN
	select * from tblstaffattendance
	where Batch=_Batch and Date=_Date and CompanyId=_CompanyId;

	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstafflistbybatch` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstafflistbybatch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstafflistbybatch`(
    in _Batch varchar(50),
    in _CompanyId int    
    )
BEGIN
	select TeacherId,FirstName,LastName,Department,Designation
	from tblteacherinfo
	where Batch=_Batch and Status='Active' and CompanyId=_CompanyId and IsDeleted ='0'
	order by Department;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstafflistbydepartment` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstafflistbydepartment` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstafflistbydepartment`(
    in _Batch varchar(50),
    in _Department varchar(50),
    in _CompanyId int    
    )
BEGIN
	select TeacherId,FirstName,LastName,Department,Designation
	from tblteacherinfo
	where Batch=_Batch and Department=_Department and Status='Active' and CompanyId=_CompanyId AND IsDeleted ='0'
	order by Department;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstafflistbydesignation` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstafflistbydesignation` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstafflistbydesignation`(
    in _Batch varchar(50),
    in _Department varchar(50),
    in _Designation varchar(50),
    in _CompanyId int    
    )
BEGIN
	select TeacherId,FirstName,LastName,Department,Designation
	from tblteacherinfo
	where Batch=_Batch and Department=_Department and Designation=_Designation and Status='Active' and CompanyId=_CompanyId AND IsDeleted ='0'
	order by Department;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstaffpresentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstaffpresentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstaffpresentattendance`(
    in _Batch varchar(50),
    in _Department varchar(50),
    in _Designation varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    
    )
BEGIN
	select tblteacherinfo.`TeacherId`,tblteacherinfo.`Batch`,tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblteacherinfo.`Department`,tblteacherinfo.`Faculty`,tblteacherinfo.`Designation`,count(tblstaffattendance.`Attendance`) as Present
	from tblteacherinfo
	inner join tblstaffattendance on tblstaffattendance.`TeacherId`=tblteacherinfo.`TeacherId`
	where (tblteacherinfo.`Batch` like  concat('%',_Batch,'%') or tblteacherinfo.`Batch`='' ) 
	And (tblteacherinfo.`Department` like concat('%',_Department,'%') or tblteacherinfo.`Department`='' ) 
	And (tblteacherinfo.`Designation` like concat('%',_Designation,'%') or tblteacherinfo.`Designation`='' ) 
	And (Date >= _DateFrom and Date <= _DateTo)
	And tblstaffattendance.`Attendance`='P'
	and tblstaffattendance.`CompanyId`=_CompanyId
	group by tblteacherinfo.`TeacherId`,tblteacherinfo.`Batch`,tblteacherinfo.`FirstName`,tblteacherinfo.`LastName`,tblteacherinfo.`Department`,tblteacherinfo.`Faculty`,tblteacherinfo.`Designation`
	order by tblteacherinfo.`Department`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstudentattendancelistbybatch` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstudentattendancelistbybatch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstudentattendancelistbybatch`(
    in _Batch varchar(50),
    in _Date varchar(50),
    in _CompanyId int    
    )
BEGIN
	select * from tblstudentattendance
	where Batch=_Batch and Date=_Date and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstudentclass` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstudentclass` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstudentclass`(
    in _StudentId varchar(50),
    in _CompanyId int    
    )
BEGIN
	select ebmasterdb.currenteducations.*,ebmasterdb.studentinfoes.*,ebmasterdb.studentscholarships.* from ebmasterdb.currenteducations
	inner join ebmasterdb.studentinfoes on ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	inner join ebmasterdb.studentscholarships on ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentscholarships.`ScholarshipId`
	where ebmasterdb.studentInfoes.`StudentId`=_StudentId and ebmasterdb.studentinfoes.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstudentclassinfo` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstudentclassinfo` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstudentclassinfo`(
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	SELECT ebmasterdb.studentinfoes.`StudentId`, ebmasterdb.studentinfoes.StudentTypeId, ebmasterdb.studentscholarships.ScholarshipNameId ,
	ebmasterdb.studenttypes.Name,ebmasterdb.classes.ClassName,ebmasterdb.faculties.FacultyName,ebmasterdb.sections.SectionName,ebmasterdb.sessions.SessionId,
	ebmasterdb.classes.ClassId,ebmasterdb.faculties.FacultyId,ebmasterdb.sections.SectionId,ebmasterdb.scholarshipnames.Name as ScholarshipName
	FROM ebmasterdb.currenteducations
	INNER JOIN ebmasterdb.studentinfoes ON ebmasterdb.studentinfoes.`StudentId`=ebmasterdb.currenteducations.`CurrentEduId`
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.faculties ON ebmasterdb.faculties.FacultyId = ebmasterdb.currenteducations.FacultyId
	INNER JOIN ebmasterdb.sessions ON ebmasterdb.sessions.SessionId = ebmasterdb.currenteducations.SessionId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	INNER JOIN ebmasterdb.studenttypes ON ebmasterdb.studentinfoes.StudentTypeId = ebmasterdb.studenttypes.StudentTypeId
	inner join ebmasterdb.studentscholarships on ebmasterdb.studentscholarships.ScholarshipId = ebmasterdb.studentinfoes.StudentId
	inner join ebmasterdb.scholarshipnames on ebmasterdb.scholarshipnames.ScholarshipId = ebmasterdb.studentscholarships.ScholarshipNameId
	WHERE ebmasterdb.studentinfoes.`StudentId`=_StudentId AND ebmasterdb.studentinfoes.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstudentlist` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstudentlist` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstudentlist`(
    in _Batch int,
    in _Class int,
    in _Section int,
    in _CompanyId int
    )
BEGIN
	select ebmasterdb.currenteducations.`CurrentEduId`,ebmasterdb.studentinfoes.`StudentId` from ebmasterdb.currenteducations
	inner join ebmasterdb.studentinfoes on ebmasterdb.studentinfoes.`StudentId`= ebmasterdb.currenteducations.`CurrentEduId`
	where ebmasterdb.currenteducations.`SessionId` =_Batch and ebmasterdb.currenteducations.`ClassId`=_Class and ebmasterdb.currenteducations.`SectionId`=_Section AND ebmasterdb.studentinfoes.`Status`='Active'
	and ebmasterdb.studentinfoes.`CompanyId`=_CompanyId and ebmasterdb.studentinfoes.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstudentlistbybatch` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstudentlistbybatch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstudentlistbybatch`(
    in _Date varchar(50),
    in _CompanyId int    
    )
BEGIN
	SELECT ebmasterdb.studentinfoes.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.ClassName,ebmasterdb.sections.SectionName
	FROM ebmasterdb.currenteducations
	INNER JOIN ebmasterdb.studentinfoes ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.studentscholarships ON ebmasterdb.studentinfoes.`StudentId`= ebmasterdb.studentscholarships.ScholarshipId
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE  ebmasterdb.studentinfoes.`Status`='Active' 
	AND ( ebmasterdb.studentscholarships.DateOfAdmission <= _Date OR ebmasterdb.studentscholarships.`DateOfAdmission` IS NULL)
	AND ebmasterdb.studentinfoes.`CompanyId`=_CompanyId
	order by ebmasterdb.classes.`ClassName`;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstudentlistbyclass` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstudentlistbyclass` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstudentlistbyclass`(
    in _Class varchar(50),
    in _Date varchar(50),
    in _CompanyId int    
    )
BEGIN
	SELECT ebmasterdb.studentinfoes.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.ClassName,ebmasterdb.sections.SectionName
	FROM ebmasterdb.currenteducations
	INNER JOIN ebmasterdb.studentinfoes ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.studentscholarships ON ebmasterdb.studentinfoes.`StudentId`= ebmasterdb.studentscholarships.ScholarshipId
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE ebmasterdb.classes.ClassName=_Class AND ebmasterdb.studentinfoes.`Status`='Active' 
	AND ( ebmasterdb.studentscholarships.DateOfAdmission <= _Date OR ebmasterdb.studentscholarships.`DateOfAdmission` IS NULL)
	AND ebmasterdb.studentinfoes.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getstudentlistbysection` */

/*!50003 DROP PROCEDURE IF EXISTS  `getstudentlistbysection` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getstudentlistbysection`(
    in _Class varchar(50),
    in _Section varchar(50),
    in _Date varchar(50),
    in _CompanyId int    
    )
BEGIN
	SELECT ebmasterdb.studentinfoes.`StudentId`,ebmasterdb.studentinfoes.`FirstName`,ebmasterdb.studentinfoes.`LastName`,ebmasterdb.classes.ClassName,ebmasterdb.sections.SectionName
	FROM ebmasterdb.currenteducations
	INNER JOIN ebmasterdb.studentinfoes ON ebmasterdb.currenteducations.`CurrentEduId`=ebmasterdb.studentinfoes.`StudentId`
	INNER JOIN ebmasterdb.studentscholarships ON ebmasterdb.studentinfoes.`StudentId`= ebmasterdb.studentscholarships.ScholarshipId
	INNER JOIN ebmasterdb.classes ON ebmasterdb.classes.ClassId = ebmasterdb.currenteducations.ClassId
	INNER JOIN ebmasterdb.sections ON ebmasterdb.sections.SectionId = ebmasterdb.currenteducations.SectionId
	WHERE ebmasterdb.classes.ClassName=_Class AND ebmasterdb.sections.SectionName=_Section AND ebmasterdb.studentinfoes.`Status`='Active' 
	and ( ebmasterdb.studentscholarships.DateOfAdmission <= _Date or ebmasterdb.studentscholarships.`DateOfAdmission` IS NULL)
	and ebmasterdb.studentinfoes.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `gettotaldepartmentholidays` */

/*!50003 DROP PROCEDURE IF EXISTS  `gettotaldepartmentholidays` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `gettotaldepartmentholidays`(
    in _Batch varchar(50),
    in _Department varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    )
BEGIN
	select Sum(DATEDIFF(DateFrom,DateTo) + 1) as Holiday from tbldayoff
	inner join tbldayoffdetails on tbldayoff.`DayOffId`=tbldayoffdetails.`DayOffId`
	where tbldayoffdetails.`Department`=_Department AND tbldayoff.`Batch`=_Batch and
	DateFrom >=_DateFrom and DateTo <= _DateTo
	and tbldayoff.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `gettotalholidays` */

/*!50003 DROP PROCEDURE IF EXISTS  `gettotalholidays` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `gettotalholidays`(
    in _Batch varchar(50),
    in _Class varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int    
    )
BEGIN
	select Sum(DATEDIFF(DateFrom,DateTo) + 1) as Holiday from tbldayoff
	inner join tbldayoffdetails on tbldayoff.`DayOffId`=tbldayoffdetails.`DayOffId`
	where tbldayoffdetails.`Class`=_Class AND tbldayoff.`Batch`=_Batch and
	DateFrom >=_DateFrom and DateTo <= _DateTo
	and tbldayoff.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `gettotalteacherholidays` */

/*!50003 DROP PROCEDURE IF EXISTS  `gettotalteacherholidays` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `gettotalteacherholidays`( 
    in _Batch varchar(50),
    in _Department varchar(50),
    in _Faculty varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int
    )
BEGIN
	select Sum(DATEDIFF(DateFrom,DateTo) + 1) as Holiday from tbldayoff
	inner join tbldayoffdetails on tbldayoff.`DayOffId`=tbldayoffdetails.`DayOffId`
	where tbldayoffdetails.`Department`=_Department AND tbldayoffdetails.`Faculty`=_Faculty AND tbldayoff.`Batch`=_Batch and
	DateFrom >=_DateFrom and DateTo <= _DateTo
	and tbldayoff.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `getyearlyfeebyid` */

/*!50003 DROP PROCEDURE IF EXISTS  `getyearlyfeebyid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `getyearlyfeebyid`(
    IN _Batch varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	select tblbillingdetails.`FeeStructureName` from tblbillingdetails
	inner join tblbilling on tblbillingdetails.`BillingId` = tblbilling.`BillingId`
	where tblbilling.`StudentId` =_StudentId and tblbilling.`Batch`=_Batch
	and (tblbillingdetails.`FeeStructureName` = 'Initial' or tblbillingdetails.`FeeStructureName` = 'Yearly')
	and tblbilling.`CompanyId`=_CompanyId and tblbillingdetails.`CompanyId`=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `login` */

/*!50003 DROP PROCEDURE IF EXISTS  `login` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `login`(
    in _id varchar(50),
    out _pass varchar(50)
    )
BEGIN
	select _pass=Password from tbluserdetail where Email=_id;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `savestudentattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `savestudentattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `savestudentattendance`(
    in _Batch varchar(50),
    in _Date varchar(50),
    in _StudentId varchar(50),
    in _Attendance varchar(50),
    in _CompanyId int    
    )
BEGIN
	INSERT INTO tblstudentattendance(Batch,tblstudentattendance.`Date`,StudentId,Attendance,CompanyId)
					Values(_Batch,_Date,_StudentId,_Attendance,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_advancedpaid` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_advancedpaid` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_advancedpaid`(
    in _StudentId varchar(50),
    in _Amount int,
    in _Status varchar(50),
    in _CompanyId int,
    in _ReceiptId int    
    )
BEGIN
	insert into tbladvancedpaid(StudentId,Amount,Status,CompanyId,ReceiptId)
			Values(_StudentId,_Amount,_Status,_CompanyId,_ReceiptId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_batch` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_batch` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_batch`(
    in _FromYear int,
    in _ToYear int,
    in _CompanyId int,
    in _Status varchar(50)    
    )
BEGIN
	INSERT INTO tblbatchdetails(FromYear,ToYear,CompanyId,Status)
				values(_FromYear,_ToYear,_CompanyId,_Status);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_batchclass` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_batchclass` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_batchclass`(
    in _BatchClassId int,
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Faculty varchar(50),
    in _CompanyId int,
    out _id int    
    )
BEGIN
	if _BatchClassId =0 then
	INSERT INTO tblbatchclass(Batch,Class,Faculty,CompanyId)
				VALUES(_Batch,_Class,_Faculty,_CompanyId);
set _id = last_insert_id();
select _id;
else
UPDATE tblbatchclass
		SET
	Batch=_Batch,
	Class=_Class,
	Faculty=_Faculty
	where BatchClassId=_BatchClassId and CompanyId=_CompanyId;
	set _id=_BatchClassId;
	select _id;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_batchclassfee` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_batchclassfee` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_batchclassfee`(
    in _ID BIGINT,
    IN _FeeId int,
    in _FeeStructureName varchar(50),
    in _FeeName varchar(50),
    in _Amount int,
    in _Refundable varchar(50),
    in _BatchClassId int,
    in _StudentType varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _ID = 0 then
	INSERT INTO tblbatchclassfee(FeeStructureName,FeeName,Amount,Refundable,BatchClassId,FeeId,StudentType,CompanyId)
					Values(_FeeStructureName,_FeeName,_Amount,_Refundable,_BatchClassId,_FeeId,_StudentType,_CompanyId);

else
update tblbatchclassfee
set
FeeStructureName=_FeeStructureName,
FeeName=_FeeName,
Amount=_Amount,
Refundable=_Refundable,
StudentType=_StudentType
where BatchClassId=_BatchClassId and FeeId=_FeeId and CompanyId=_CompanyId;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_billing` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_billing` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_billing`(
    in _StudentId varchar(50),
    in _Date varchar(50),
    in _Month varchar(50),
    in _Batch varchar(50),
    in _Status varchar(20),
    in _IsCreated bit,
    in _CreatedBy varchar(50),
    in _CompanyId int,
    out _id int
    )
BEGIN
	INSERT INTO tblbilling(StudentId,Date,Month,Status,IsCreated,Batch,CreatedBy,CompanyId)
			VALUES(_StudentId,_Date,_Month,_Status,_IsCreated,_Batch,_CreatedBy,_CompanyId);
			SEt _id =LAST_INSERT_ID();
			select _id;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_billingdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_billingdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_billingdetails`(
    in _BillingId varchar(50),
    in _Amount int,
    in _FeeStructureName varchar(50),
    in _FeeName varchar(50),
    in _CompanyId int    
    )
BEGIN
	INSERT INTO tblbillingdetails(BillingId,Amount,FeeStructureName,FeeName,CompanyId)
				VALUES(_BillingId,_Amount,_FeeStructureName,_FeeName,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_class` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_class` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_class`(
    in _ClassId bigint,
    in _ClassName varchar(50),
    in _ClassCode varchar(50),
    in _ClassType varchar(50),
    in _StudentLimit int,
    in _FacultyId int,
    in _CompanyId int    
    )
BEGIN
	if _ClassId = 0 then
	Insert into tblclassdetails(ClassName,ClassCode,ClassType,StudentLimit,FacultyId,CompanyId)
					values(_ClassName,_ClassCode,_ClassType,_StudentLimit,_FacultyId,_CompanyId);

else
Update tblclassdetails
		set
	ClassName=_ClassName,
	ClassCode=_ClassCode,
	ClassType=_ClassType,
	StudentLimit=_StudentLimit,
	FacultyId=_FacultyId
	where ClassId=_ClassId and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_companydetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_companydetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_companydetails`(
    in _CompanyId int,
    in _Name varchar(50),
    in _Address varchar(50),
    in _PhoneNo varchar(50),
    in _PAN VARCHAR(50),
    IN _RegistrationDate varchar(50),
    in _Status varchar(50),
    out _id int    
    )
BEGIN
	if _CompanyId = 0 then
	INSERT INTO tblcompanydetails(Name,Address,PAN,PhoneNo,RegistrationDate,Status)
					Values(_Name,_Address,_PAN,_PhoneNo,CURDATE(),_Status);
				SET _id =LAST_INSERT_ID();
				SELECT _id;

else
Update tblcompanydetails
set
Name = _Name,
PhoneNo=_PhoneNo,
Address=_Address,
PAN=_PAN,
Status=_Status
WHERE CompanyId=_CompanyId;
SEt _id =_CompanyId;
select _id;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_country` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_country` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_country`(
    in _CountryId int,
    in _CountryName varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _CountryId = 0 then

	INSERT INTO tblcountrydetails(CountryName,CompanyId)
					VAlues(_CountryName,_CompanyId);

ELSE
UPDATE tblcountrydetails
		set
		CountryName=_CountryName
		where CountryId=_CountryId and CompanyId=_CompanyId;
		end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_currentaddress` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_currentaddress` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_currentaddress`(
    in _AddressId int,
    in _Qno varchar(50),
    in _Street varchar(50),
    in _Municipality varchar(50),
    in _State varchar(50),
    in _Country varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	IF _AddressId = 0 then
INSERT INTO tblcurrentaddress(Qno,Street,Municipality,State,Country,StudentId,CompanyId)
					VALUES(_Qno,_Street,_Municipality,_State,_Country,_StudentId,_CompanyId);

ELSE
UPDATE tblcurrentaddress
	SET
	Qno = _Qno,
	Street = _Street,
	Municipality = _Municipality,
	State = _State,
	Country = _Country,
	StudentId = _StudentId
	WHERE AddressId = _AddressId and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_currenteducation` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_currenteducation` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_currenteducation`(
    in _CurrentEducationId int,
    in _Faculty varchar(50),
    in _Class varchar(50),
    in _Section varchar(50),
    in _StudentId varchar(50),
    in _Batch varchar(50),
    in _CompanyId int    
    )
BEGIN
	IF _CurrentEducationId = 0 then
INSERT INTO tblcurrenteducation(Faculty,Class,Section,StudentId,Batch,CompanyId)
						VALUES(_Faculty,_Class,_Section,_StudentId,_Batch,_CompanyId);

ELSE
UPDATE tblcurrenteducation
		SET
		Faculty=_Faculty,
		Class=_Class,
		Section=_Section,
		StudentId=_StudentId,
		Batch=_Batch
		where CurrentEducationId=_CurrentEducationId and CompanyId=_CompanyId;
		end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_dayoff` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_dayoff` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_dayoff`(
    in _DayOffId int,
    in _Batch varchar(50),
    in _Title varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _CompanyId int,
    out _id int
    )
BEGIN
	if _DayOffId = 0 then
INSERT INTO tbldayoff(Batch,Title,DateFrom,DateTo,CompanyId)
			VALUES(_Batch,_Title,_DateFrom,_DateTo,_CompanyId);
	set _id=last_Insert_ID();
	SELECT _id;
else

update tbldayoff
set
Batch=_Batch,
Title=_Title,
DateFrom=_DateFrom,
DateTo=_DateTo
where DayOffId=_DayOffId and CompanyId=_CompanyId;
set _id=_DayOffId;
select _id=_DayOffId;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_dayoffdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_dayoffdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_dayoffdetails`(
    in _DayOffId int,
    in _Department varchar(50),
    in _Faculty varchar(50),
    in _Class varchar(50),
    in _CompanyId int
    )
BEGIN
	insert into tbldayoffdetails(DayOffId,Department,Faculty,Class,CompanyId)
				Values(_DayOffId,_Department,_Faculty,_Class,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_discountdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_discountdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_discountdetails`(
    in _Id int,
    in _ScholarshipName varchar(50),
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Faculty varchar(50),
    in _DisCountType varchar(50),
    in _Discount int,
    in _FeeStructureName varchar(50),
    in _FeeId int,
    in _Amount int,
    in _StudentType varchar(50),
    in _CompanyId int    
    )
BEGIN
	INSERT INTO tblscholarshipdetails(ScholarshipName,Batch,Class,Faculty,DiscountType,Discount,FeeId,FeeStructureName,Amount,StudentType,CompanyId)
							Values(_ScholarshipName,_Batch,_Class,_Faculty,_DisCountType,_Discount,_FeeId,_FeeStructureName,_Amount,_StudentType,_CompanyId);

	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_editstudent` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_editstudent` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_editstudent`(
    in _StudentId varchar(50),
    in _FirstName varchar(50),
    in _LastName varchar(50),
    in _DateOfBirth_BS varchar(50),
    in _DateOfBirth_AD varchar(50),
    in _Gender varchar(50),
    in _EmailId varchar(50),
    in _Batch varchar(50),
    in _MobileNo varchar(50),
    in _FatherName varchar(50),
    in _FatherNumber varchar(50),
    in _MotherName varchar(50),
    in _MotherNumber varchar(50),
    in _Status Varchar(50),
    in _StudentType varchar(50),
    in _CompanyId int
    )
BEGIN
	Update tblstudentinfo
	SET
	 FirstName=_FirstName,
	 LastName=_LastName,
	 DateOfBirth_BS=_DateOfBirth_BS,
	 DateOfBirth_AD=_DateOfBirth_AD,
	 Gender=_Gender,
	 EmailId=_EmailId,
	 MobileNo=_MobileNo,
	 Batch=_Batch,
	 FatherName=_FatherName,
	 FatherNumber=_FatherNumber,
	 MotherName=_MotherName,	
	 MotherNumber=_MotherNumber,
	 Status=_Status,
	 StudentType=_StudentType
	 Where StudentId=_StudentId and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_editteacher` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_editteacher` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_editteacher`(
    in _TeacherId int,
    in _FirstName varchar(50),
    in _LastName varchar(50),
    in _Designation Varchar(50),
    in _Department varchar(50),
    in _Faculty varchar(50),
    in _Gender varchar(50),
    in _Email varchar(50),
    in _DateOfBirth varchar(50),
    in _Batch varchar(50),
    in _Citizenship varchar(50),
    in _JoiningDate varchar(50),
    in _Status varchar(50),
    in _CompanyId int  
    )
BEGIN
	UPDATE tblteacherinfo
 set
 FirstName=_FirstName,
 LastName=_LastName,
 Designation=_Designation,
 Department=_Department,
 Faculty=_Faculty,
 Gender=_Gender,
 Email=_Email,
 DateOfBirth=_DateOfBirth,
 Batch=_Batch,
 Citizenship=_Citizenship,
 JoiningDate=_JoiningDate,
 tblteacherinfo.`Status`=_Status
 where TeacherId=_TeacherId and CompanyId=_CompanyId;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_education` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_education` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_education`(
    in _Degree varchar(50),
    in _Institution varchar(50),
    in _TotalMarks int,
    in _Division varchar(50),
    in _TeacherId int,
    in _CompanyId int
    )
BEGIN
	INSERT INTO tbleducation(Degree,Institution,TotalMarks,Division,TeacherId,CompanyId)
				VALUES(_Degree,_Institution,_TotalMarks,_Division,_TeacherId,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_emergencycontact` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_emergencycontact` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_emergencycontact`(
    in _EmergencyContactId int,
    in _ParentName varchar(50),
    in _ContactNumber varchar(50),
    in _Location varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int    
    )
BEGIN
	IF _EmergencyContactId = 0 then
INSERT INTO tblemergencycontact(ParentName,ContactNumber,Location,StudentId,CompanyId)
					VALUES(_ParentName,_ContactNumber,_Location,_StudentId,_CompanyId);

ELSE

UPDATE tblemergencycontact
		SET
	ParentName=_ParentName,
	ContactNumber=_ContactNumber,
	Location=_Location,
	StudentId=_StudentId
	WHERE EmergencyContactId=_EmergencyContactId and CompanyId=_CompanyId;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_experience` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_experience` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_experience`(
    in _Organisation varchar(50),
    in _Post varchar(50),
    in _DateFrom varchar(50),
    in _DateTo varchar(50),
    in _TeacherId int,
    in _CompanyId int
    )
BEGIN
	insert INTO tblexperience(Organisation,Post,DateFrom,DateTo,TeacherId,CompanyId)
				VALUES(_Organisation,_Post,_DateFrom,_DateTo,_TeacherId,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_faculty` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_faculty` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_faculty`(
    in _FacultyId bigint,
    in _FacultyName varchar(50),
    in _FacultyCode varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _FacultyId = 0 then
Insert into tblfacultydetails(FacultyName,FacultyCode,CompanyId)
					values(_FacultyName,_FacultyCode,_Companyid);

else

Update tblfacultydetails
		set
	FacultyName=_FacultyName,
	FacultyCode=_FacultyCode
	where FacultyId=_FacultyId and CompanyId=_Companyid;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_feature` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_feature` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_feature`(
   	IN _ID INT,
	IN _Name VARCHAR(50),
	IN _Company_ID INT
    )
BEGIN
	IF _ID = 0 THEN
	INSERT INTO tblfeature(Name,Company_ID)
			VALUES(_Name,_Company_ID);
	ELSE
	UPDATE tblfeature
	SET 
	Name=_Name,
	Company_ID=_Company_ID
	WHERE ID =_ID;
END IF;
    END */$$
DELIMITER ;

/* Procedure structure for procedure `save_featureaction` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_featureaction` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_featureaction`(
    	IN _ID INT,
	IN _Name VARCHAR(50),
	IN _Company_ID INT
    )
BEGIN
	IF _ID = 0 THEN
	INSERT INTO tblfeatureaction(NAME,Company_ID)
			VALUES(_Name,_Company_ID);
	ELSE
	UPDATE tblfeatureaction
	SET 
	NAME=_Name,
	Company_ID=_Company_ID
	WHERE ID =_ID;
END IF;
    END */$$
DELIMITER ;

/* Procedure structure for procedure `save_feedetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_feedetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_feedetails`(
    in _FeeId bigint,
    in _FeeStructureName varchar(50),
    in _FeeName varchar(50),
    in _Amount int,
    in _Refundable varchar(50),
    in _StudentType varchar(50),
    in _CompanyId int
    )
BEGIN
	if _FeeId = 0 then
Insert into tblfeedetails(FeeStructureName,FeeName,Amount,Refundable,StudentType,CompanyId)
					values(_FeeStructureName,_FeeName,_Amount,_Refundable,_StudentType,_CompanyId);

else

Update tblfeedetails
		set
	FeeStructureName=_FeeStructureName,
	FeeName=_FeeName,
	Amount=_Amount,
	Refundable=_Refundable,
	StudentType=_StudentType
	where FeeId=_FeeId AND CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_feestructure` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_feestructure` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_feestructure`(
    in _FeeStructureId bigint,
    in _FeeStructureName varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _FeeStructureId = 0 then
Insert into tblfeestructuredetails(FeeStructureName,CompanyId)
					values(_FeeStructureName,_CompanyId);

else
Update tblfeestructuredetails
		set
	FeeStructureName=_FeeStructureName
	where FeeStructureId=_FeeStructureId and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_finedetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_finedetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_finedetails`(
    in _FineId int,
    in _DayFrom int,
    in _DayTo int,
    in _FineType varchar(50),
    in _FineAmount int,
    in _CompanyId int
    )
BEGIN
	if _FineId = 0 then
insert into tblfine(DayFrom,DayTo,FineType,FineAmount,CompanyId)
			VALUES(_DayFrom,_DayTo,_FineType,_FineAmount,_CompanyId);
else
update tblfine
set
DayFrom=_DayFrom,
DayTo=_DayTo,
FineType=_FineType,
FineAmount=_FineAmount
where FineId=_FineId and CompanyId=_CompanyId;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_followupdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_followupdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_followupdetails`(
    in _StudentId varchar(50),
    in _Batch varchar(50),
    in _Response varchar(200),
    in _FollowUpDate varchar(50),
    in _ExpectedDate varchar(50),
    in _CompanyId int    
    )
BEGIN
	Insert into tblfollowup(StudentId,Response,FollowUpDate,ExpectedDate,Batch,CompanyId)
				Values(_StudentId,_Response,_FollowUpDate,_ExpectedDate,_Batch,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_group` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_group` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_group`(
	in _ID INT,
	IN _UserRole varchar(50),
	in _Company_ID INT
    )
BEGIN
	IF _ID = 0 THEN
	INSERT INTO tblgroup(UserRole,Company_ID)
			Values(_UserRole,_Company_ID);
	ELSE
	UPDATE tblgroup
	set 
	UserRole=_UserRole,
	Company_ID=_Company_ID
	WHERE ID =_ID;
END IF;
    END */$$
DELIMITER ;

/* Procedure structure for procedure `save_pasteducation` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_pasteducation` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_pasteducation`(
    in _PastEducationId int,
    in _Degree varchar(50),
    in _School varchar(50),
    in _TotalMarks int,
    in _Division varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	IF _PastEducationId = 0 then
INSERT INTO tblpasteducation(Degree,School,TotalMarks,Division,StudentId,CompanyId)
		VALUES(_Degree,_School,_TotalMarks,_Division,_StudentId,_CompanyId);
ELSE
INSERT INTO tblpasteducation(Degree,School,TotalMarks,Division,StudentId,CompanyId)
		VALUES(_Degree,_School,_TotalMarks,_Division,_StudentId,_CompanyId);
		end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_permanentaddress` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_permanentaddress` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_permanentaddress`(
    in _AddressId int,
    in _Qno varchar(50),
    in _Street varchar(50),
    in _Municipality varchar(50),
    in _State varchar(50),
    in _Country varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int
    )
BEGIN
	IF _AddressId = 0 then
INSERT INTO tblpermanentaddress(Qno,Street,Municipality,State,Country,StudentId,CompanyId)
					VALUES(_Qno,_Street,_Municipality,_State,_Country,_StudentId,_CompanyId);

ELSE
UPDATE tblpermanentaddress
		SET
	Qno = _Qno,
	Street = _Street,
	Municipality =_Municipality,
	State = _State,
	Country = _Country,
	StudentId = _StudentId
	WHERE AddressId = _AddressId and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_receiptdetail` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_receiptdetail` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_receiptdetail`(
    in _ReceiptId int,
    in _StudentId varchar(50),
    in _TotalAmount int,
    in _Month varchar(50),
    in _Batch varchar(50),
    in _Date varchar(50),
    in _PaidAmount int,
    in _DueAmount int,
    in _PaymentMethod varchar(50),
    in _BankName varchar(50),
    in _ChequeNo varchar(50),
    in _BillingId int,
    in _Discount int,
    in _Fine int,
    in _CreatedBy varchar(50),
    in _CompanyId int,
    out _id int    
    )
BEGIN
	
		INSERT into tblreceipt(StudentId,tblreceipt.`Date`,tblreceipt.`Month`,TotalAmount,PaidAmount,DueAmount,PaymentMethod,BankName,ChequeNo,BillingId,Discount,Fine,Batch,CreatedBy,CompanyId)
				VALUES(_StudentId,_Date,_Month,_TotalAmount,_PaidAmount,_DueAmount,_PaymentMethod,_BankName,_ChequeNo,_BillingId,_Discount,_Fine,_Batch,_CreatedBy,_CompanyId);
				SEt _id = last_insert_id();
				select _id;
				
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_responsetext` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_responsetext` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_responsetext`(
    in _ResponseTextId int,
    in _ResponseText varchar(200),
    in _CompanyId int    
    )
BEGIN
	if _ResponseTextId = 0 then
INSERT INTO tblresponsetext(ResponseText,CompanyId)
					VAlues(_ResponseText,_CompanyId);
ELSE
UPDATE tblresponsetext
		set
		ResponseText=_ResponseText
		where ResponseTextId=_ResponseTextId and CompanyId=_CompanyId;
		end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_scholarship` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_scholarship` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_scholarship`(
    in _StudentScholarshipId int,
    in _ScholarshipName varchar(50),
    in _Description varchar(50),
    in _DateOfAdmission varchar(50),
    in _StudentId varchar(50),
    in _CompanyId int    
    )
BEGIN
	IF _StudentScholarshipId = 0 then

INSERT INTO tblscholarship(ScholarshipName,Description,DateOfAdmission,StudentId,CompanyId)
				VALUES(_ScholarshipName,_Description,_DateOfAdmission,_StudentId,_CompanyId);
ELSE
UPDATE tblscholarship
		SET
	ScholarshipName=_ScholarshipName,
	Description=_Description,
	DateOfAdmission=_DateOfAdmission,
	StudentId=_StudentId
	WHERE StudentScholarshipId = _StudentScholarshipId and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_scholarshipname` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_scholarshipname` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_scholarshipname`(
    IN _ScholarshipId int,
    in _ScholarshipName varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _ScholarshipId = 0 then
Insert into tblscholarshipname(ScholarshipName,CompanyId)
					values(_ScholarshipName,_CompanyId);

else
Update tblscholarshipname
		set
	ScholarshipName=_ScholarshipName
	where ScholarshipId=_ScholarshipId and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_section` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_section` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_section`(
    in _SectionId bigint,
    in _SectionName varchar(50),
    in _StudentLimit int,
    in _CompanyId int    
    )
BEGIN
	if _SectionId = 0 then
Insert into tblsectiondetails(SectionName,StudentLimit,CompanyId)
					values(_SectionName,_StudentLimit,_CompanyId);
else
Update tblsectiondetails
		set
	SectionName=_SectionName,
	StudentLimit=_StudentLimit
	where SectionId=_SectionId and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_specialscholarship` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_specialscholarship` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_specialscholarship`(
    in _SpecialScholarshipId int,
    in _Batch varchar(50),
    in _Class varchar(50),
    in _Faculty varchar(50),
    in _StudentId varchar(50),
    IN _BatchClassId int,
    in _FeeStructureName varchar(50),
    in _FeeId int,
    in _Discount int ,
    in _DiscountType varchar(50),
    in _Amount int,
    in _StudentType varchar(50),
    in _CompanyId int    
    )
BEGIN
INSERT INTO tblspecialscholarship(Batch,Class,Faculty,StudentId,FeeStructureName,FeeId,Discount,DiscountType,Amount,StudentType,BatchClassId,CompanyId)
						VALUES(_Batch,_Class,_Faculty,_StudentId,_FeeStructureName,_FeeId,_Discount,_DiscountType,_Amount,_StudentType,_BatchClassId,_CompanyId);

	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_staffattendance` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_staffattendance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_staffattendance`(
    in _Batch varchar(50),
    in _Date varchar(50),
    in _TeacherId varchar(50),
    in _Attendance varchar(50),
    in _CompanyId int    
    )
BEGIN
	INSERT INTO tblstaffattendance(Batch,Date,TeacherId,Attendance,CompanyId)
					Values(_Batch,_Date,_TeacherId,_Attendance,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_startdate` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_startdate` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_startdate`(
    in _StartDateId int,
    in _Date varchar(50),
    in _Batch varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _StartDateId = 0 then

insert into tblstartdate(Date,Batch,CompanyId)
				Values(_Date,_Batch,_CompanyId);
else
update tblstartdate
set
Date=_Date,
Batch=_Batch
where StartDateId=_StartDateId;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_startpoint` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_startpoint` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_startpoint`(
    in _Id int,
    in _Place varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _Id = 0 then

Insert into tblstartpoint(Place,CompanyId)
		Values(_Place,_CompanyId);

else
Update tblstartpoint
set
Place=_Place
where PlaceId=_Id and CompanyId=_CompanyId;
End if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_state` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_state` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_state`(
    in _StateId int,
    in _StateName varchar(50),
    in _CountryName varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _StateId = 0 then
INSERT INTO tblstatedetails(StateName,CountryName,CompanyId)
				Values(_StateName,_CountryName,_CompanyId);

ELSE
UPDATE tblstatedetails
		SET
	StateName=_StateName,
	CountryName=_CountryName
	where StateId=_StateId and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_student` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_student` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_student`(
    IN _StudentId VARCHAR(50),
    IN _FirstName VARCHAR(50),
    IN _LastName VARCHAR(50),
    IN _DateOfBirth_BS VARCHAR(50),
    IN _DateOfBirth_AD VARCHAR(50),
    IN _Gender VARCHAR(50),
    IN _EmailId VARCHAR(50),
    IN _Batch VARCHAR(50),
    IN _MobileNo VARCHAR(50),
    IN _FatherName VARCHAR(50),
    IN _FatherNumber VARCHAR(50),
    IN _MotherName VARCHAR(50),
    IN _MotherNumber VARCHAR(50),
    IN _Status VARCHAR(50),
    IN _StudentType VARCHAR(50),
    IN _CompanyId INT,
    out _id int
    )
BEGIN
	INSERT INTO tblstudentinfo(FirstName,LastName,DateOfBirth_BS,DateOfBirth_AD,Gender,EmailId,Batch,MobileNo,FatherName,FatherNumber,MotherName,MotherNumber,Status,StudentType,StudentId,CompanyId)
	               VALUES(_FirstName,_LastName,_DateOfBirth_BS,_DateOfBirth_AD,_Gender,_EmailId,_Batch,_MobileNo,_FatherName,_FatherNumber,_MotherName,_MotherNumber,_Status,_StudentType,_StudentId,_CompanyId);
	SEt _id =last_insert_id();
	select _id;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_studentextrafee` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_studentextrafee` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_studentextrafee`(
    in _batch varchar(50),
    in _month varchar(50),
    in _studentid varchar(50),
    in _CompanyId int,
    out _id int    
    )
BEGIN
	insert into tblstudentextrafee(Batch,Month,StudentId,CompanyId)
					values(_batch,_month,_studentid,_CompanyId);
	set _id=last_insert_id();
	select _id;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_studentextrafeedetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_studentextrafeedetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_studentextrafeedetails`(
    in _studentextrafeeid int,
    in _feename varchar(50),
    in _amount int,
    in _CompanyId int    
    )
BEGIN
	insert into tblstudentextrafeedetails(StudentExtraFeeId,FeeName,Amount,CompanyId)
								Values(_studentextrafeeid,_feename,_amount,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_studentTransport` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_studentTransport` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_studentTransport`(
    in _Batch varchar(50),
    in _StudentId varchar(50),
    in _PlaceTo varchar(50),
    in _Type varchar(50),
    in _Amount varchar(50),
    in _CompanyId int    
    )
BEGIN
	Insert into tblstudenttransport(Batch,StudentId,PlaceTo,tblstudenttransport.Type,Amount,CompanyId)
						Values(_Batch,_StudentId,_PlaceTo,_Type,_Amount,_CompanyId);
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_studenttype` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_studenttype` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_studenttype`(
    in _StudentTypeId bigint,
    in _StudentTypeName varchar(50),
    in _CompanyId int
    )
BEGIN
	if _StudentTypeId = 0 then
INSERT INTO tblstudenttype(StudentTypeName,CompanyId)
		Values(_StudentTypeName,_CompanyId);

ELSE
UPDATE tblstudenttype
SET
StudentTypeName=_StudentTypeName
WHERE StudentTypeId=_StudentTypeId and CompanyId=_CompanyId;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_teacher` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_teacher` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_teacher`(
    in _FirstName varchar(50),
    in _LastName varchar(50),
    in _Designation varchar(50),
    in _Department varchar(50),
    in _Faculty varchar(50),
    in _Gender varchar(50),
    in _Email varchar(50),
    in _DateOfBirth varchar(50),
    in _Batch varchar(50),
    in _Citizenship varchar(50),
    in _JoiningDate varchar(50),
    in _Status varchar(50),
    in _CompanyId int,
    out _id int
    )
BEGIN
	Insert into tblteacherinfo(FirstName,LastName,Designation,Department,Faculty,Gender,Email,DateOfBirth,Batch,Citizenship,JoiningDate,Status,CompanyId)
					Values(_FirstName,_LastName,_Designation,_Department,_Faculty,_Gender,_Email,_DateOfBirth,_Batch,_Citizenship,_JoiningDate,_Status,_CompanyId);
	SEt _id =last_insert_id();
	select _id;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_teachercurrentaddress` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_teachercurrentaddress` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_teachercurrentaddress`(
    in _AddressId int,
    in _HouseNo varchar(50),
    in _Street varchaR(50),
    in _Municipality varchar(50),
    in _Country varchar(50),
    in _State varchar(50),
    in _MobileNo varchar(50),
    in _PhoneNo varchar(50),
    in _TeacherId int,
    in _CompanyId int
    )
BEGIN
	IF _AddressId = 0 then

insert into tblteachercurrentaddress(HouseNo,Street,Municipality,Country,State,MobileNo,PhoneNo,TeacherId,CompanyId)
						VALUES(_HouseNo,_Street,_Municipality,_Country,_State,_MobileNo,_PhoneNo,_TeacherId,_CompanyId);
ELSE
update tblteachercurrentaddress
	SET
HouseNo=_HouseNo,
Street=_Street,
Municipality=_Municipality,
Country=_Country,
State=_State,
MobileNo=_MobileNo,
PhoneNo=_PhoneNo
where AddressId=_AddressId and CompanyId=_CompanyId;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_teacherpermanentaddress` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_teacherpermanentaddress` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_teacherpermanentaddress`(
    IN _AddressId INT,
    IN _HouseNo VARCHAR(50),
    IN _Street VARCHAR(50),
    IN _Municipality VARCHAR(50),
    IN _Country VARCHAR(50),
    IN _State VARCHAR(50),
    IN _MobileNo VARCHAR(50),
    IN _PhoneNo VARCHAR(50),
    IN _TeacherId INT,
    IN _CompanyId INT
    )
BEGIN
	IF _AddressId = 0 THEN

INSERT INTO tblteacherpermanentaddress(HouseNo,Street,Municipality,Country,State,MobileNo,PhoneNo,TeacherId,CompanyId)
						VALUES(_HouseNo,_Street,_Municipality,_Country,_State,_MobileNo,_PhoneNo,_TeacherId,_CompanyId);
ELSE
UPDATE tblteacherpermanentaddress
	SET
HouseNo=_HouseNo,
Street=_Street,
Municipality=_Municipality,
Country=_Country,
State=_State,
MobileNo=_MobileNo,
PhoneNo=_PhoneNo
WHERE AddressId=_AddressId AND CompanyId=_CompanyId;
END IF;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_transportdetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_transportdetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_transportdetails`(
    in _TransportationId int,
    in _PlaceTo varchar(50),
    in _DistanceFrom int,
    in _DistanceTo int,
    in _OneWayAmount int,
    in _TwoWayAmount int,
    in _CompanyId int    
    )
BEGIN
	if _TransportationId = 0 then

Insert into tbltransportation(PlaceTo,DistanceFrom,DistanceTo,OneWayAmount,TwoWayAmount,CompanyId)
						VALUES(_PlaceTo,_DistanceFrom,_DistanceTo,_OneWayAmount,_TwoWayAmount,_CompanyId);

else
Update tbltransportation
set 
PlaceTo = _PlaceTo,
DistanceFrom =_DistanceFrom,
DistanceTo = _DistanceTo,
OneWayAmount=_OneWayAmount,
TwoWayAmount=_TwoWayAmount
where TransPortationId=_TransportationId and CompanyId=_CompanyId;

end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `save_usercontrol` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_usercontrol` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_usercontrol`(
    in _Company_ID int,
    IN _Group_ID INT,
    IN _Feature_ID INT,
    IN _Action_ID INT
    )
BEGIN
	INSERT into tblusercontrol(Company_ID,Group_ID,Feature_ID,Action_ID)
			Values(_Company_ID,_Group_ID,_Feature_ID,_Action_ID);
    END */$$
DELIMITER ;

/* Procedure structure for procedure `save_userinfo` */

/*!50003 DROP PROCEDURE IF EXISTS  `save_userinfo` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `save_userinfo`(
    in _UserID INT,
    IN _Password varchar(50),
    in _Email varchar(50),
    in _Role varchar(50),
    in _CompanyId int    
    )
BEGIN
	if _UserID = 0 then

INSERT INTO tbluserdetail(Email,tbluserdetail.`Password`,Role,CompanyId)
				values(_Email,_Password,_Role,_CompanyId);

elseif _Password = NULL then 
update tbluserdetail
set
	Email=_Email,
	Role=_Role
	where UserID=_UserID and CompanyId=_CompanyId;

else

update tbluserdetail
set
	Email=_Email,
	Role=_Role,
	tbluserdetails.Password=_Password
	where UserID=_UserID and CompanyId=_CompanyId;
	end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `SearchCompanyDetails` */

/*!50003 DROP PROCEDURE IF EXISTS  `SearchCompanyDetails` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `SearchCompanyDetails`(
    in _Name varchar(50),
    in _Email varchar(50),
    in _Status varchar(50)    
    )
BEGIN
	select * from tblcompanydetails
inner join tbluserdetail on tblcompanydetails.`CompanyId`=tbluserdetail.`CompanyId`
WHERE 
(tbluserdetail.Email=_Email or Email='')
and(tblcompanydetails.Name=_Name or tblcompanydetails.`Name`='')
and(tblcompanydetails.Status=_Status or tblcompanydetails.`Status`='')
and tbluserdetail.Role='Admin';
	END */$$
DELIMITER ;

/* Procedure structure for procedure `testviewalter` */

/*!50003 DROP PROCEDURE IF EXISTS  `testviewalter` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `testviewalter`()
BEGIN
SELECT
  `tblstudentinfo`.`FirstName`  AS `FirstName`,
  `tblstudentinfo`.`LastName`   AS `LastName`,
  `tblstudentinfo`.`CompanyId`  AS `CompanyId`,
  `tblbilling`.`StudentId`      AS `StudentId`,
  `tblbilling`.`BillingId`      AS `BillingId`,
  `tblbilling`.`Batch`          AS `Batch`,
  `tblbilling`.`Month`          AS `Month`,
  `tblcurrenteducation`.`Class` AS `Class`,
  SUM(`tblbillingdetails`.`Amount`) AS `Total`,
  SUM(`tblbillingdetails`.`Amount`) AS `due`
FROM (((`tblbilling`
     JOIN `tblbillingdetails`
       ON ((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`)))
    JOIN `tblstudentinfo`
      ON ((`tblstudentinfo`.`StudentId` = `tblbilling`.`StudentId`)))
   JOIN `tblcurrenteducation`
     ON ((`tblstudentinfo`.`StudentId` = `tblcurrenteducation`.`StudentId`)))
GROUP BY `tblstudentinfo`.`FirstName`,`tblstudentinfo`.`LastName`,`tblstudentinfo`.`CompanyId`,`tblbilling`.`Batch`,`tblbilling`.`StudentId`,`tblbilling`.`Month`,`tblbilling`.`IsCreated`,`tblbilling`.`BillingId`,`tblbilling`.`Status`,`tblcurrenteducation`.`Class`,`tblbilling`.`Month`
HAVING (`tblbilling`.`IsCreated` = '0')UNION SELECT
                                               `tblstudentinfo`.`FirstName`   AS `FirstName`,
                                               `tblstudentinfo`.`LastName`    AS `LastName`,
                                               `tblstudentinfo`.`CompanyId`   AS `CompanyId`,
                                               `tblbilling`.`StudentId`       AS `StudentId`,
                                               `tblbilling`.`BillingId`       AS `BillingId`,
                                               `tblbilling`.`Batch`           AS `Batch`,
                                               `tblbilling`.`Month`           AS `Month`,
                                               `tblcurrenteducation`.`Class`  AS `Class`,
                                               SUM(`tblbillingdetails`.`Amount`) AS `Total`,
                                               0                              AS `due`
                                             FROM (((`tblbilling`
                                                  JOIN `tblbillingdetails`
                                                    ON ((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`)))
                                                 JOIN `tblstudentinfo`
                                                   ON ((`tblstudentinfo`.`StudentId` = `tblbilling`.`StudentId`)))
                                                JOIN `tblcurrenteducation`
                                                  ON ((`tblstudentinfo`.`StudentId` = `tblcurrenteducation`.`StudentId`)))
                                             GROUP BY `tblstudentinfo`.`FirstName`,`tblstudentinfo`.`LastName`,`tblstudentinfo`.`CompanyId`,`tblbilling`.`Batch`,`tblbilling`.`StudentId`,`tblbilling`.`Month`,`tblbilling`.`IsCreated`,`tblbilling`.`BillingId`,`tblbilling`.`Status`,`tblcurrenteducation`.`Class`
                                             HAVING ((`tblbilling`.`IsCreated` = '1')
                                                     AND (`tblbilling`.`Status` = 'Partial Paid'));
    END */$$
DELIMITER ;

/* Procedure structure for procedure `updatesuperadmin` */

/*!50003 DROP PROCEDURE IF EXISTS  `updatesuperadmin` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `updatesuperadmin`(
    IN _UserID INT,
    IN _Password varchar(50),
    in _Email varchar(50)    
    )
BEGIN
	if _Password = null then

Update tbluserdetail
set
Email=_Email
where UserID=_UserID and Email=_Email;
else
Update tbluserdetail
set
tbluserdetail.`Password`=_Password
where UserID=_UserID and Email=_Email;
end if;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `_test` */

/*!50003 DROP PROCEDURE IF EXISTS  `_test` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `_test`()
BEGIN
SELECT
  `ebmasterdb.studentinfoes`.`FirstName`  AS `FirstName`,
  `ebmasterdb.studentinfoes`.`LastName`   AS `LastName`,
  `ebmasterdb.studentinfoes`.`CompanyId`  AS `CompanyId`,
  `tblbilling`.`StudentId`      AS `StudentId`,
  `tblbilling`.`BillingId`      AS `BillingId`,
  `tblbilling`.`Batch`          AS `Batch`,
  `tblbilling`.`Month`          AS `Month`,
  `ebmasterdb.classes`.`ClassName` AS `Class`,
  SUM(`tblbillingdetails`.`Amount`) AS `Total`,
  SUM(`tblbillingdetails`.`Amount`) AS `due`
FROM `tblbilling`
     INNER JOIN `tblbillingdetails` ON `tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`
   INNER  JOIN `ebmasterdb.studentinfoes` ON `ebmasterdb.studentinfoes`.`StudentId` = `tblbilling`.`StudentId`
  INNER JOIN `ebmasterdb.currenteducations` ON `ebmasterdb.currenteducations`.`CurrentEduId` = `ebmasterdb.studentinfoes`.`StudentId`
  INNER JOIN `ebmasterdb.classes` ON `ebmasterdb.classes`.`ClassId` = `ebmasterdb.currenteducations`.`Classid`
GROUP BY 
`ebmasterdb.studentinfoes`.`FirstName`,
`ebmasterdb.studentinfoes`.`LastName`,
`ebmasterdb.studentinfoes`.`CompanyId`,
`tblbilling`.`Batch`,
`tblbilling`.`StudentId`,
`tblbilling`.`Month`,
`tblbilling`.`IsCreated`,
`tblbilling`.`BillingId`,
`tblbilling`.`Status`,
`ebmasterdb.classes`.`ClassName`,
`tblbilling`.`Month`
HAVING `tblbilling`.`IsCreated` = '0'
union 
select
  `ebmasterdb.studentinfoes`.`FirstName`  AS `FirstName`,
  `ebmasterdb.studentinfoes`.`LastName`   AS `LastName`,
  `ebmasterdb.studentinfoes`.`CompanyId`  AS `CompanyId`,
  `tblbilling`.`StudentId`      AS `StudentId`,
  `tblbilling`.`BillingId`      AS `BillingId`,
  `tblbilling`.`Batch`          AS `Batch`,
  `tblbilling`.`Month`          AS `Month`,
  `ebmasterdb.classes`.`ClassName` AS `Class`,
  SUM(`tblbillingdetails`.`Amount`) AS `Total`,
	0 						AS `due`
FROM `tblbilling`
     INNER JOIN `tblbillingdetails` ON `tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`
   INNER  JOIN `ebmasterdb.studentinfoes` ON `ebmasterdb.studentinfoes`.`StudentId` = `tblbilling`.`StudentId`
  INNER JOIN `ebmasterdb.currenteducations` ON `ebmasterdb.currenteducations`.`CurrentEduId` = `ebmasterdb.studentinfoes`.`StudentId`
  INNER JOIN `ebmasterdb.classes` ON `ebmasterdb.classes`.`ClassId` = `ebmasterdb.currenteducations`.`Classid`
GROUP BY 
`ebmasterdb.studentinfoes`.`FirstName`,
`ebmasterdb.studentinfoes`.`LastName`,
`ebmasterdb.studentinfoes`.`CompanyId`,
`tblbilling`.`Batch`,
`tblbilling`.`StudentId`,
`tblbilling`.`Month`,
`tblbilling`.`IsCreated`,
`tblbilling`.`BillingId`,
`tblbilling`.`Status`,
`ebmasterdb.classes`.`ClassName`,
`tblbilling`.`Month`
HAVING `tblbilling`.`IsCreated` = '0' AND `tblbilling`.`Status` = 'Partial Paid';
 END */$$
DELIMITER ;

/* Procedure structure for procedure `_totaldue` */

/*!50003 DROP PROCEDURE IF EXISTS  `_totaldue` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `_totaldue`()
BEGIN
	SELECT
		tblbilling.StudentId 	  	as 	StudentId,
		tblbilling.BillingId 	  	as 	BillingId,
		tblbilling.Batch		  	AS 	Batch,
		tblbilling.Month		  	AS 	Month,
		SUM(tblbillingdetails.Amount)	as 	Total,
		SUM(tblbillingdetails.Amount) 	as 	due,
		ebmasterdb.studentinfoes.FirstName 	as 	FirstName,
		ebmasterdb.studentinfoes.LastName 	as 	LastName,
		ebmasterdb.studentinfoes.CompanyId 	as 	CompanyId,
		ebmasterdb.classes.ClassName 		as 	Class
	From tblbilling
	Inner join tblbillingdetails on tblbilling.BillingId = tblbillingdetails.BillingId
	inner join ebmasterdb.studentinfoes on tblbilling.StudentId = ebmasterdb.studentinfoes.StudentId
	inner join ebmasterdb.currenteducations on tblbilling.StudentId = ebmasterdb.currenteducations.CurrentEduId
	inner join ebmasterdb.classes on ebmasterdb.currenteducations.ClassId = ebmasterdb.classes.ClassId
	GROUP BY
	tblbilling.Batch,
	tblbilling.StudentId,
	tblbilling.Month,
	tblbilling.IsCreated,
	tblbilling.BillingId,
	tblbilling.Status,
	ebmasterdb.studentinfoes.FirstName,
	ebmasterdb.studentinfoes.LastName,
	ebmasterdb.studentinfoes.CompanyId,
	ebmasterdb.classes.ClassName
	HAVING tblbilling.IsCreated = 0
	union
	SELECT
		tblbilling.StudentId 	  	AS 	StudentId,
		tblbilling.BillingId 	  	AS 	BillingId,
		tblbilling.Batch		  	AS 	Batch,
		tblbilling.Month		  	AS 	MONTH,
		SUM(tblbillingdetails.Amount)	AS 	Total,
		0					AS 	due,
		ebmasterdb.studentinfoes.FirstName 	AS 	FirstName,
		ebmasterdb.studentinfoes.LastName 	AS 	LastName,
		ebmasterdb.studentinfoes.CompanyId 	AS 	CompanyId,
		ebmasterdb.classes.ClassName 		AS 	Class
	FROM tblbilling
	INNER JOIN tblbillingdetails ON tblbilling.BillingId = tblbillingdetails.BillingId
	INNER JOIN ebmasterdb.studentinfoes ON tblbilling.StudentId = ebmasterdb.studentinfoes.StudentId
	INNER JOIN ebmasterdb.currenteducations ON tblbilling.StudentId = ebmasterdb.currenteducations.CurrentEduId
	INNER JOIN ebmasterdb.classes ON ebmasterdb.currenteducations.ClassId = ebmasterdb.classes.ClassId
	GROUP BY
	tblbilling.Batch,
	tblbilling.StudentId,
	tblbilling.Month,
	tblbilling.IsCreated,
	tblbilling.BillingId,
	tblbilling.Status,
	ebmasterdb.studentinfoes.FirstName,
	ebmasterdb.studentinfoes.LastName,
	ebmasterdb.studentinfoes.CompanyId,
	ebmasterdb.classes.ClassName
	HAVING tblbilling.IsCreated = 0 and tblbilling.Status = 'Partial Paid';
	
END */$$
DELIMITER ;

/*Table structure for table `_billing` */

DROP TABLE IF EXISTS `_billing`;

/*!50001 DROP VIEW IF EXISTS `_billing` */;
/*!50001 DROP TABLE IF EXISTS `_billing` */;

/*!50001 CREATE TABLE  `_billing`(
 `FirstName` varchar(50) NULL ,
 `StudentId` varchar(50) NOT NULL ,
 `Due` decimal(32,0) NULL 
)*/;

/*Table structure for table `_billing1` */

DROP TABLE IF EXISTS `_billing1`;

/*!50001 DROP VIEW IF EXISTS `_billing1` */;
/*!50001 DROP TABLE IF EXISTS `_billing1` */;

/*!50001 CREATE TABLE  `_billing1`(
 `StudentId` varchar(50) NOT NULL ,
 `Due` int(11) NULL 
)*/;

/*Table structure for table `_billing2` */

DROP TABLE IF EXISTS `_billing2`;

/*!50001 DROP VIEW IF EXISTS `_billing2` */;
/*!50001 DROP TABLE IF EXISTS `_billing2` */;

/*!50001 CREATE TABLE  `_billing2`(
 `FirstName` varchar(50) NULL ,
 `StudentId` varchar(50) NOT NULL  default '' ,
 `Due` decimal(32,0) NULL 
)*/;

/*Table structure for table `_check` */

DROP TABLE IF EXISTS `_check`;

/*!50001 DROP VIEW IF EXISTS `_check` */;
/*!50001 DROP TABLE IF EXISTS `_check` */;

/*!50001 CREATE TABLE  `_check`(
 `FirstName` varchar(50) NULL ,
 `LastName` varchar(50) NULL ,
 `FatherName` varchar(50) NULL ,
 `FatherNumber` varchar(50) NULL ,
 `MotherNumber` varchar(50) NULL ,
 `StudentId` varchar(50) NOT NULL  default '' ,
 `BillingId` int(11) NOT NULL  default '0' ,
 `Month` varchar(50) NULL ,
 `Batch` varchar(50) NULL ,
 `Status` varchar(50) NULL ,
 `Total` decimal(32,0) NULL ,
 `due` decimal(32,0) NULL 
)*/;

/*Table structure for table `_checkmonth` */

DROP TABLE IF EXISTS `_checkmonth`;

/*!50001 DROP VIEW IF EXISTS `_checkmonth` */;
/*!50001 DROP TABLE IF EXISTS `_checkmonth` */;

/*!50001 CREATE TABLE  `_checkmonth`(
 `StudentId` varchar(50) NOT NULL ,
 `Due` int(11) NULL 
)*/;

/*Table structure for table `_getlist` */

DROP TABLE IF EXISTS `_getlist`;

/*!50001 DROP VIEW IF EXISTS `_getlist` */;
/*!50001 DROP TABLE IF EXISTS `_getlist` */;

/*!50001 CREATE TABLE  `_getlist`(
 `FirstName` varchar(50) NULL ,
 `LastName` varchar(50) NULL ,
 `FatherName` varchar(50) NULL ,
 `FatherNumber` varchar(50) NULL ,
 `MotherNumber` varchar(50) NULL ,
 `StudentId` varchar(50) NOT NULL  default '' ,
 `Class` varchar(50) NULL ,
 `BillingId` int(11) NOT NULL  default '0' ,
 `Month` varchar(50) NULL ,
 `Batch` varchar(50) NULL ,
 `Status` varchar(50) NULL ,
 `Total` decimal(32,0) NULL ,
 `due` decimal(32,0) NULL ,
 `Discount` decimal(32,0) NULL ,
 `Fine` decimal(32,0) NULL 
)*/;

/*Table structure for table `_golist` */

DROP TABLE IF EXISTS `_golist`;

/*!50001 DROP VIEW IF EXISTS `_golist` */;
/*!50001 DROP TABLE IF EXISTS `_golist` */;

/*!50001 CREATE TABLE  `_golist`(
 `FirstName` varchar(50) NULL ,
 `LastName` varchar(50) NULL ,
 `StudentId` varchar(50) NOT NULL  default '' ,
 `Batch` varchar(50) NULL ,
 `FatherName` varchar(50) NULL ,
 `FatherNumber` varchar(50) NULL ,
 `Month` varchar(50) NULL ,
 `MotherNumber` varchar(50) NULL ,
 `Due` decimal(54,0) NULL 
)*/;

/*Table structure for table `_listno` */

DROP TABLE IF EXISTS `_listno`;

/*!50001 DROP VIEW IF EXISTS `_listno` */;
/*!50001 DROP TABLE IF EXISTS `_listno` */;

/*!50001 CREATE TABLE  `_listno`(
 `StudentId` varchar(50) NOT NULL  default '' ,
 `total` decimal(32,0) NULL 
)*/;

/*Table structure for table `_totaldue` */

DROP TABLE IF EXISTS `_totaldue`;

/*!50001 DROP VIEW IF EXISTS `_totaldue` */;
/*!50001 DROP TABLE IF EXISTS `_totaldue` */;

/*!50001 CREATE TABLE  `_totaldue`(
 `FirstName` varchar(50) NULL ,
 `LastName` varchar(50) NULL ,
 `CompanyId` int(11) NOT NULL  default '0' ,
 `StudentId` varchar(50) NOT NULL  default '' ,
 `BillingId` int(11) NOT NULL  default '0' ,
 `Batch` varchar(50) NULL ,
 `Month` varchar(50) NULL ,
 `Class` varchar(50) NULL ,
 `Total` decimal(32,0) NULL ,
 `due` decimal(32,0) NULL 
)*/;

/*View structure for view _billing */

/*!50001 DROP TABLE IF EXISTS `_billing` */;
/*!50001 DROP VIEW IF EXISTS `_billing` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_billing` AS select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblbilling`.`StudentId` AS `StudentId`,sum(`tblbillingdetails`.`Amount`) AS `Due` from ((`tblbilling` join `tblstudentinfo` on((`tblbilling`.`StudentId` = `tblstudentinfo`.`StudentId`))) join `tblbillingdetails` on((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`))) group by `tblbilling`.`StudentId`,`tblbilling`.`IsCreated`,`tblbilling`.`Month`,`tblstudentinfo`.`FirstName`,`tblbillingdetails`.`Amount` having ((`tblbilling`.`Month` = '1') and (`tblbilling`.`IsCreated` = 0) and (`tblbillingdetails`.`Amount` <> 0)) */;

/*View structure for view _billing1 */

/*!50001 DROP TABLE IF EXISTS `_billing1` */;
/*!50001 DROP VIEW IF EXISTS `_billing1` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_billing1` AS (select `tblreceipt`.`StudentId` AS `StudentId`,min(`tblreceipt`.`DueAmount`) AS `Due` from `tblreceipt` where (`tblreceipt`.`Month` = '1') group by `tblreceipt`.`StudentId`) */;

/*View structure for view _billing2 */

/*!50001 DROP TABLE IF EXISTS `_billing2` */;
/*!50001 DROP VIEW IF EXISTS `_billing2` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_billing2` AS select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblbilling`.`StudentId` AS `StudentId`,sum(`tblbillingdetails`.`Amount`) AS `Due` from ((`tblbilling` join `tblstudentinfo` on((`tblbilling`.`StudentId` = `tblstudentinfo`.`StudentId`))) join `tblbillingdetails` on((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`))) where ((`tblbilling`.`Month` = '1') and (`tblbilling`.`IsCreated` = 0)) group by `tblbilling`.`StudentId`,`tblstudentinfo`.`FirstName` union select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblreceipt`.`StudentId` AS `StudentId`,min(`tblreceipt`.`DueAmount`) AS `Due` from (`tblreceipt` join `tblstudentinfo` on((`tblreceipt`.`StudentId` = `tblstudentinfo`.`StudentId`))) where (`tblreceipt`.`Month` = '1') group by `tblreceipt`.`StudentId`,`tblstudentinfo`.`FirstName` */;

/*View structure for view _check */

/*!50001 DROP TABLE IF EXISTS `_check` */;
/*!50001 DROP VIEW IF EXISTS `_check` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_check` AS select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblstudentinfo`.`LastName` AS `LastName`,`tblstudentinfo`.`FatherName` AS `FatherName`,`tblstudentinfo`.`FatherNumber` AS `FatherNumber`,`tblstudentinfo`.`MotherNumber` AS `MotherNumber`,`tblbilling`.`StudentId` AS `StudentId`,`tblbilling`.`BillingId` AS `BillingId`,`tblbilling`.`Month` AS `Month`,`tblbilling`.`Batch` AS `Batch`,`tblbilling`.`Status` AS `Status`,sum(`tblbillingdetails`.`Amount`) AS `Total`,min(`tblreceipt`.`DueAmount`) AS `due` from ((((`tblbilling` join `tblbillingdetails` on((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`))) join `tblstudentinfo` on((`tblstudentinfo`.`StudentId` = `tblbilling`.`StudentId`))) join `tblreceipt` on((`tblbilling`.`BillingId` = `tblreceipt`.`BillingId`))) join `tblcurrenteducation` on((`tblcurrenteducation`.`StudentId` = `tblstudentinfo`.`StudentId`))) where (`tblreceipt`.`Month` = '1') group by `tblstudentinfo`.`FirstName`,`tblstudentinfo`.`LastName`,`tblstudentinfo`.`FatherName`,`tblstudentinfo`.`FatherNumber`,`tblstudentinfo`.`MotherNumber`,`tblbilling`.`Month`,`tblbilling`.`Batch`,`tblbilling`.`StudentId`,`tblbilling`.`Status`,`tblbilling`.`BillingId`,`tblreceipt`.`DueAmount` having ((`tblbilling`.`Status` = 'Unpaid') and (`tblreceipt`.`DueAmount` <> 0)) union select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblstudentinfo`.`LastName` AS `LastName`,`tblstudentinfo`.`FatherName` AS `FatherName`,`tblstudentinfo`.`FatherNumber` AS `FatherNumber`,`tblstudentinfo`.`MotherNumber` AS `MotherNumber`,`tblbilling`.`StudentId` AS `StudentId`,`tblbilling`.`BillingId` AS `BillingId`,`tblbilling`.`Month` AS `Month`,`tblbilling`.`Batch` AS `Batch`,`tblbilling`.`Status` AS `Status`,sum(`tblbillingdetails`.`Amount`) AS `Total`,sum(`tblbillingdetails`.`Amount`) AS `due` from (((`tblbilling` join `tblbillingdetails` on((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`))) join `tblstudentinfo` on((`tblstudentinfo`.`StudentId` = `tblbilling`.`StudentId`))) join `tblcurrenteducation` on((`tblcurrenteducation`.`StudentId` = `tblstudentinfo`.`StudentId`))) where (`tblbilling`.`Month` = '1') group by `tblstudentinfo`.`FirstName`,`tblstudentinfo`.`LastName`,`tblstudentinfo`.`FatherName`,`tblstudentinfo`.`FatherNumber`,`tblstudentinfo`.`MotherNumber`,`tblbilling`.`Month`,`tblbilling`.`Batch`,`tblbilling`.`StudentId`,`tblbilling`.`Status`,`tblbilling`.`IsCreated`,`tblbilling`.`BillingId`,`tblbillingdetails`.`Amount` having ((`tblbilling`.`Status` = 'Unpaid') and (`tblbilling`.`IsCreated` = '0') and (`tblbillingdetails`.`Amount` <> 0)) */;

/*View structure for view _checkmonth */

/*!50001 DROP TABLE IF EXISTS `_checkmonth` */;
/*!50001 DROP VIEW IF EXISTS `_checkmonth` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_checkmonth` AS select `dbo`.`tblreceipt`.`StudentId` AS `StudentId`,min(`dbo`.`tblreceipt`.`DueAmount`) AS `Due` from `dbo`.`tblreceipt` where (`dbo`.`tblreceipt`.`Month` = '1') group by `dbo`.`tblreceipt`.`StudentId` */;

/*View structure for view _getlist */

/*!50001 DROP TABLE IF EXISTS `_getlist` */;
/*!50001 DROP VIEW IF EXISTS `_getlist` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_getlist` AS select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblstudentinfo`.`LastName` AS `LastName`,`tblstudentinfo`.`FatherName` AS `FatherName`,`tblstudentinfo`.`FatherNumber` AS `FatherNumber`,`tblstudentinfo`.`MotherNumber` AS `MotherNumber`,`tblbilling`.`StudentId` AS `StudentId`,`tblcurrenteducation`.`Class` AS `Class`,`tblbilling`.`BillingId` AS `BillingId`,`tblbilling`.`Month` AS `Month`,`tblbilling`.`Batch` AS `Batch`,`tblbilling`.`Status` AS `Status`,sum(`tblbillingdetails`.`Amount`) AS `Total`,min(`tblreceipt`.`DueAmount`) AS `due`,sum(`tblreceipt`.`Discount`) AS `Discount`,sum(`tblreceipt`.`Fine`) AS `Fine` from ((((`tblbilling` join `tblbillingdetails` on((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`))) join `tblstudentinfo` on((`tblstudentinfo`.`StudentId` = `tblbilling`.`StudentId`))) join `tblcurrenteducation` on((`tblstudentinfo`.`StudentId` = `tblcurrenteducation`.`StudentId`))) join `tblreceipt` on((`tblbilling`.`BillingId` = `tblreceipt`.`BillingId`))) group by `tblstudentinfo`.`FirstName`,`tblstudentinfo`.`LastName`,`tblstudentinfo`.`FatherName`,`tblstudentinfo`.`FatherNumber`,`tblstudentinfo`.`MotherNumber`,`tblbilling`.`Month`,`tblbilling`.`Batch`,`tblbilling`.`StudentId`,`tblbilling`.`Status`,`tblcurrenteducation`.`Class`,`tblbilling`.`BillingId`,`tblbilling`.`Status` having (`tblbilling`.`Status` = 'Partial Paid') union select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblstudentinfo`.`LastName` AS `LastName`,`tblstudentinfo`.`FatherName` AS `FatherName`,`tblstudentinfo`.`FatherNumber` AS `FatherNumber`,`tblstudentinfo`.`MotherNumber` AS `MotherNumber`,`tblbilling`.`StudentId` AS `StudentId`,`tblcurrenteducation`.`Class` AS `Class`,`tblbilling`.`BillingId` AS `BillingId`,`tblbilling`.`Month` AS `Month`,`tblbilling`.`Batch` AS `Batch`,`tblbilling`.`Status` AS `Status`,sum(`tblbillingdetails`.`Amount`) AS `Total`,sum(`tblbillingdetails`.`Amount`) AS `due`,0 AS `Discount`,0 AS `Fine` from (((`tblbilling` join `tblbillingdetails` on((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`))) join `tblstudentinfo` on((`tblstudentinfo`.`StudentId` = `tblbilling`.`StudentId`))) join `tblcurrenteducation` on((`tblstudentinfo`.`StudentId` = `tblcurrenteducation`.`StudentId`))) group by `tblstudentinfo`.`FirstName`,`tblstudentinfo`.`LastName`,`tblstudentinfo`.`FatherName`,`tblstudentinfo`.`FatherNumber`,`tblstudentinfo`.`MotherNumber`,`tblbilling`.`Month`,`tblbilling`.`Batch`,`tblbilling`.`StudentId`,`tblbilling`.`Status`,`tblcurrenteducation`.`Class`,`tblbilling`.`IsCreated`,`tblbilling`.`BillingId` having (`tblbilling`.`IsCreated` = '0') */;

/*View structure for view _golist */

/*!50001 DROP TABLE IF EXISTS `_golist` */;
/*!50001 DROP VIEW IF EXISTS `_golist` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_golist` AS select `_getlist`.`FirstName` AS `FirstName`,`_getlist`.`LastName` AS `LastName`,`_getlist`.`StudentId` AS `StudentId`,`_getlist`.`Batch` AS `Batch`,`_getlist`.`FatherName` AS `FatherName`,`_getlist`.`FatherNumber` AS `FatherNumber`,`_getlist`.`Month` AS `Month`,`_getlist`.`MotherNumber` AS `MotherNumber`,sum(`_getlist`.`due`) AS `Due` from `_getlist` group by `_getlist`.`FirstName`,`_getlist`.`LastName`,`_getlist`.`StudentId`,`_getlist`.`Batch`,`_getlist`.`FatherName`,`_getlist`.`FatherNumber`,`_getlist`.`MotherNumber`,`_getlist`.`Month` having ((`_getlist`.`Month` = '1') and (`_getlist`.`Batch` = '2076-2077')) order by `Due` desc limit 100 */;

/*View structure for view _listno */

/*!50001 DROP TABLE IF EXISTS `_listno` */;
/*!50001 DROP VIEW IF EXISTS `_listno` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_listno` AS select `tblbilling`.`StudentId` AS `StudentId`,sum(`tblbillingdetails`.`Amount`) AS `total` from (`tblbillingdetails` join `tblbilling` on((`tblbillingdetails`.`BillingId` = `tblbilling`.`BillingId`))) where ((`tblbilling`.`Month` = '1') and (`tblbilling`.`IsCreated` = 0) and (`tblbillingdetails`.`Amount` <> 0)) group by `tblbilling`.`StudentId` union select `_checkmonth`.`StudentId` AS `StudentId`,sum(`_checkmonth`.`Due`) AS `total` from `_checkmonth` where (`_checkmonth`.`Due` <> 0) group by `_checkmonth`.`StudentId` */;

/*View structure for view _totaldue */

/*!50001 DROP TABLE IF EXISTS `_totaldue` */;
/*!50001 DROP VIEW IF EXISTS `_totaldue` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `_totaldue` AS select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblstudentinfo`.`LastName` AS `LastName`,`tblstudentinfo`.`CompanyId` AS `CompanyId`,`tblbilling`.`StudentId` AS `StudentId`,`tblbilling`.`BillingId` AS `BillingId`,`tblbilling`.`Batch` AS `Batch`,`tblbilling`.`Month` AS `Month`,`tblcurrenteducation`.`Class` AS `Class`,sum(`tblbillingdetails`.`Amount`) AS `Total`,sum(`tblbillingdetails`.`Amount`) AS `due` from (((`tblbilling` join `tblbillingdetails` on((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`))) join `tblstudentinfo` on((`tblstudentinfo`.`StudentId` = `tblbilling`.`StudentId`))) join `tblcurrenteducation` on((`tblstudentinfo`.`StudentId` = `tblcurrenteducation`.`StudentId`))) group by `tblstudentinfo`.`FirstName`,`tblstudentinfo`.`LastName`,`tblstudentinfo`.`CompanyId`,`tblbilling`.`Batch`,`tblbilling`.`StudentId`,`tblbilling`.`Month`,`tblbilling`.`IsCreated`,`tblbilling`.`BillingId`,`tblbilling`.`Status`,`tblcurrenteducation`.`Class`,`tblbilling`.`Month` having (`tblbilling`.`IsCreated` = '0') union select `tblstudentinfo`.`FirstName` AS `FirstName`,`tblstudentinfo`.`LastName` AS `LastName`,`tblstudentinfo`.`CompanyId` AS `CompanyId`,`tblbilling`.`StudentId` AS `StudentId`,`tblbilling`.`BillingId` AS `BillingId`,`tblbilling`.`Batch` AS `Batch`,`tblbilling`.`Month` AS `Month`,`tblcurrenteducation`.`Class` AS `Class`,sum(`tblbillingdetails`.`Amount`) AS `Total`,0 AS `due` from (((`tblbilling` join `tblbillingdetails` on((`tblbilling`.`BillingId` = `tblbillingdetails`.`BillingId`))) join `tblstudentinfo` on((`tblstudentinfo`.`StudentId` = `tblbilling`.`StudentId`))) join `tblcurrenteducation` on((`tblstudentinfo`.`StudentId` = `tblcurrenteducation`.`StudentId`))) group by `tblstudentinfo`.`FirstName`,`tblstudentinfo`.`LastName`,`tblstudentinfo`.`CompanyId`,`tblbilling`.`Batch`,`tblbilling`.`StudentId`,`tblbilling`.`Month`,`tblbilling`.`IsCreated`,`tblbilling`.`BillingId`,`tblbilling`.`Status`,`tblcurrenteducation`.`Class` having ((`tblbilling`.`IsCreated` = '1') and (`tblbilling`.`Status` = 'Partial Paid')) */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
