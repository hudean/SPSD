/*
 Navicat Premium Data Transfer

 Source Server         : MaterialDataBase
 Source Server Type    : SQLite
 Source Server Version : 3035005
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3035005
 File Encoding         : 65001

 Date: 27/05/2025 10:28:23
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for TEquationOfStatePara
-- ----------------------------
DROP TABLE IF EXISTS "TEquationOfStatePara";
CREATE TABLE "TEquationOfStatePara" (
  "EosParaId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "EosTypeId" integer NOT NULL,
  "ParameterValueStr" varchar(255) NOT NULL
);

-- ----------------------------
-- Records of TEquationOfStatePara
-- ----------------------------

-- ----------------------------
-- Table structure for TEquationOfStateType
-- ----------------------------
DROP TABLE IF EXISTS "TEquationOfStateType";
CREATE TABLE "TEquationOfStateType" (
  "EosTypeId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "EosName" varchar(255) NOT NULL,
  "ParameterStr" varchar(255) NOT NULL,
  "ParameterDescriptionStr" varchar(255) NOT NULL
);

-- ----------------------------
-- Records of TEquationOfStateType
-- ----------------------------
INSERT INTO "TEquationOfStateType" VALUES (1, 'GRUNEISEN', 'C,S1,S2,S3,GAMAO,A,E0,V0', '无');
INSERT INTO "TEquationOfStateType" VALUES (2, 'JWL', 'A,B,R1,R2,OMEG,E0,VO', '无');
INSERT INTO "TEquationOfStateType" VALUES (3, 'LINEAR_POLYNOMIAL', 'C0,C1,C2,C3,C4,C5,C6,E0,V0', '无');
INSERT INTO "TEquationOfStateType" VALUES (4, '空', '   ', '  ');

-- ----------------------------
-- Table structure for TFragmentArrange
-- ----------------------------
DROP TABLE IF EXISTS "TFragmentArrange";
CREATE TABLE "TFragmentArrange" (
  "FragmentArrangeId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "FragmentArrangeName" varchar(255) NOT NULL
);

-- ----------------------------
-- Records of TFragmentArrange
-- ----------------------------
INSERT INTO "TFragmentArrange" VALUES (1, 'xxx');
INSERT INTO "TFragmentArrange" VALUES (2, '12');
INSERT INTO "TFragmentArrange" VALUES (3, '测试方案');

-- ----------------------------
-- Table structure for TFragments
-- ----------------------------
DROP TABLE IF EXISTS "TFragments";
CREATE TABLE "TFragments" (
  "FragmentID" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "Index" integer NOT NULL,
  "Length" real NOT NULL,
  "Width" real NOT NULL,
  "Height" real NOT NULL,
  "MateriaId" integer NOT NULL,
  "SegmentedListId" integer NOT NULL
);

-- ----------------------------
-- Records of TFragments
-- ----------------------------
INSERT INTO "TFragments" VALUES (1, 1, 1.0, 1.0, 1.0, 0, 2);
INSERT INTO "TFragments" VALUES (3, 1, 1.0, 1.0, 1.0, 0, 4);
INSERT INTO "TFragments" VALUES (9, 1, 0.5, 0.5, 0.5, 11, 7);
INSERT INTO "TFragments" VALUES (10, 1, 1.0, 1.0, 1.0, 11, 6);
INSERT INTO "TFragments" VALUES (17, 1, 5.0, 5.0, 5.0, 0, 5);

-- ----------------------------
-- Table structure for TImportPoints
-- ----------------------------
DROP TABLE IF EXISTS "TImportPoints";
CREATE TABLE "TImportPoints" (
  "ImportPointsId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "index" integer NOT NULL,
  "pointX" real NOT NULL,
  "pointY" real NOT NULL,
  "SegmentedListId" integer NOT NULL
);

-- ----------------------------
-- Records of TImportPoints
-- ----------------------------
INSERT INTO "TImportPoints" VALUES (169, 1, 45.0, 0.0, 5);
INSERT INTO "TImportPoints" VALUES (170, 2, 43.0, 0.5, 5);
INSERT INTO "TImportPoints" VALUES (171, 3, 42.0, 1.0, 5);
INSERT INTO "TImportPoints" VALUES (172, 4, 41.0, 1.5, 5);
INSERT INTO "TImportPoints" VALUES (173, 5, 41.0, 2.0, 5);
INSERT INTO "TImportPoints" VALUES (174, 6, 42.0, 2.5, 5);
INSERT INTO "TImportPoints" VALUES (175, 7, 42.0, 3.0, 5);
INSERT INTO "TImportPoints" VALUES (176, 8, 43.0, 3.5, 5);
INSERT INTO "TImportPoints" VALUES (177, 9, 4.5257, 4.0, 5);
INSERT INTO "TImportPoints" VALUES (178, 10, 4.7343, 4.5, 5);
INSERT INTO "TImportPoints" VALUES (179, 11, 5.0, 5.0, 5);
INSERT INTO "TImportPoints" VALUES (180, 12, 5.2508, 5.5, 5);
INSERT INTO "TImportPoints" VALUES (181, 13, 5.4552, 6.0, 5);
INSERT INTO "TImportPoints" VALUES (182, 14, 5.6158, 6.5, 5);
INSERT INTO "TImportPoints" VALUES (183, 15, 5.7338, 7.0, 5);
INSERT INTO "TImportPoints" VALUES (184, 16, 5.7097, 7.5, 5);
INSERT INTO "TImportPoints" VALUES (185, 17, 5.6797, 8.0, 5);
INSERT INTO "TImportPoints" VALUES (186, 18, 5.572, 8.5, 5);
INSERT INTO "TImportPoints" VALUES (187, 19, 5.4744, 9.0, 5);
INSERT INTO "TImportPoints" VALUES (188, 20, 5.3657, 9.5, 5);
INSERT INTO "TImportPoints" VALUES (189, 21, 5.25, 10.0, 5);

-- ----------------------------
-- Table structure for TMarterial
-- ----------------------------
DROP TABLE IF EXISTS "TMarterial";
CREATE TABLE "TMarterial" (
  "MaterialId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "MaterialName" varchar(255) NOT NULL,
  "StrengthTypeId" integer NOT NULL,
  "StrengthParaId" integer NOT NULL,
  "EosTypeId" integer NOT NULL,
  "EosParaId" integer NOT NULL
);

-- ----------------------------
-- Records of TMarterial
-- ----------------------------

-- ----------------------------
-- Table structure for TSegmentedList
-- ----------------------------
DROP TABLE IF EXISTS "TSegmentedList";
CREATE TABLE "TSegmentedList" (
  "SegmentedListId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "SegmentedName" varchar(255) NOT NULL,
  "OutlineType" integer NOT NULL,
  "OutlineStartPointX" real NOT NULL,
  "OutlineStartPointY" real NOT NULL,
  "OutlineCenterPointX" real NOT NULL,
  "OutlineCenterPointY" real NOT NULL,
  "OutlineEndPointX" real NOT NULL,
  "OutlineEndPointY" real NOT NULL,
  "EquationString" varchar(255) NOT NULL,
  "StartX" real NOT NULL,
  "EndX" real NOT NULL,
  "FragmentArrangeId" integer NOT NULL
);

-- ----------------------------
-- Records of TSegmentedList
-- ----------------------------
INSERT INTO "TSegmentedList" VALUES (6, 'xxx', 0, 0.0, 5.0, 7.0, 5.5, 15.0, 5.0, '', 0.0, 0.0, 1);
INSERT INTO "TSegmentedList" VALUES (7, '双曲线', 1, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 'y=0.000285585x^4-0.01903x^3+0.22924x^2-0.6222x^1+4.59473', 0.0, 10.0, 3);

-- ----------------------------
-- Table structure for TSimulationCondition
-- ----------------------------
DROP TABLE IF EXISTS "TSimulationCondition";
CREATE TABLE "TSimulationCondition" (
  "ConditionId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "ConditionName" varchar(255) NOT NULL,
  "Description" varchar(255) NOT NULL,
  "WorkPath" varchar(255) NOT NULL,
  "WarheadModelId" integer NOT NULL,
  "SimulinkParameterId" integer NOT NULL,
  "StateDescription" varchar(255) NOT NULL
);

-- ----------------------------
-- Records of TSimulationCondition
-- ----------------------------
INSERT INTO "TSimulationCondition" VALUES (5, '工况2', 'xxx', 'C:\Users\lenovo\Desktop\2015', 4, 5, 'DataGridViewTextBoxCell { ColumnIndex=7, RowIndex=0 }');
INSERT INTO "TSimulationCondition" VALUES (6, '工况3', 'xxxx', 'C:\Users\lenovo\Desktop\test', 8, 5, 'DataGridViewTextBoxCell { ColumnIndex=7, RowIndex=1 }');

-- ----------------------------
-- Table structure for TSimulationTask
-- ----------------------------
DROP TABLE IF EXISTS "TSimulationTask";
CREATE TABLE "TSimulationTask" (
  "TaskId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "ConditionName" varchar(255) NOT NULL,
  "Description" varchar(255) NOT NULL,
  "WorkPath" varchar(255) NOT NULL,
  "WarheadModelId" integer NOT NULL,
  "SimulinkParameterId" integer NOT NULL,
  "StateDescription" varchar(255) NOT NULL
);

-- ----------------------------
-- Records of TSimulationTask
-- ----------------------------

-- ----------------------------
-- Table structure for TSimulinkParaMeter
-- ----------------------------
DROP TABLE IF EXISTS "TSimulinkParaMeter";
CREATE TABLE "TSimulinkParaMeter" (
  "SimulinkParameterId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "SimulinkParameterName" varchar(255) NOT NULL,
  "EndTime" real NOT NULL,
  "OutputInterval" real NOT NULL,
  "Slsfac" real NOT NULL,
  "Tssfac" real NOT NULL,
  "Pfac" real,
  "Nquad" integer
);

-- ----------------------------
-- Records of TSimulinkParaMeter
-- ----------------------------
INSERT INTO "TSimulinkParaMeter" VALUES (4, '1', 100.0, 10.0, 0.1, 0.67, 0.1, 3);
INSERT INTO "TSimulinkParaMeter" VALUES (5, '3', 1000.0, 10.0, 5.0, 0.67, 5.0, 5);

-- ----------------------------
-- Table structure for TStrengthModelPara
-- ----------------------------
DROP TABLE IF EXISTS "TStrengthModelPara";
CREATE TABLE "TStrengthModelPara" (
  "StrengthParaId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "StrengthTypeId" integer NOT NULL,
  "ParameterValueStr" varchar(255) NOT NULL
);

-- ----------------------------
-- Records of TStrengthModelPara
-- ----------------------------

-- ----------------------------
-- Table structure for TStrengthModelType
-- ----------------------------
DROP TABLE IF EXISTS "TStrengthModelType";
CREATE TABLE "TStrengthModelType" (
  "StrengthTypeId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "StrengthModelName" varchar(255) NOT NULL,
  "ParameterStr" varchar(255) NOT NULL,
  "ParameterDescription" varchar(255) NOT NULL
);

-- ----------------------------
-- Records of TStrengthModelType
-- ----------------------------
INSERT INTO "TStrengthModelType" VALUES (1, 'PLASTIC_KINEMATIC', 'RO,E,PR,SIGY,ETAN,BETA,SRC,SRP,FS,VP', '  ');
INSERT INTO "TStrengthModelType" VALUES (2, 'JOHNSON_COOK', 'RO,G,E,PR,DTF,VP,A,B,N,C,M,TM,TR,EPSO,CP,PC,SPALL,IT,D1,D2,D3,D4,D5', '  ');
INSERT INTO "TStrengthModelType" VALUES (3, 'HIGH_EXPLOSIVE_BURN', 'RO,D,PCJ,BETA,K,G,SIGY', ' ');
INSERT INTO "TStrengthModelType" VALUES (4, 'ELASTIC_PLASTIC_HYDRO', 'RO,G,SIGO,EH,PC,FS,CHARL,A1,A2,SPALL,EPS1,EPS2,EPS3,EPS4,EPS5,EPS6,EPS7,EPS8,EPS9,EPS10,EPS11,EPS12,EPS13,EPS14,EPS15,EPS16,ES2,ES3,ES4,ES5,ES6,ES7,ES8,ES9,ES10,ES11,ES12,ES13,ES14,ES15,ES16', ' ');
INSERT INTO "TStrengthModelType" VALUES (5, 'NULL', 'RO,PC,MU,TEROD,CEROD,YM,PR', ' ');
INSERT INTO "TStrengthModelType" VALUES (6, 'ELASTIC ', 'RO,E,PR,DA,DB,K,VC,CP', '  ');

-- ----------------------------
-- Table structure for TTmpMaterial
-- ----------------------------
DROP TABLE IF EXISTS "TTmpMaterial";
CREATE TABLE "TTmpMaterial" (
  "FragmentID" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "MaterialName" varchar(255) NOT NULL,
  "StrengthModelName" varchar(255) NOT NULL,
  "StrengthModelParameter" varchar(255) NOT NULL,
  "StrengthModelValue" varchar(255) NOT NULL,
  "EOSName" varchar(255) NOT NULL,
  "EOSParameter" varchar(255) NOT NULL,
  "EOSValue" varchar(255) NOT NULL,
  "ReferenceCount" integer NOT NULL
);

-- ----------------------------
-- Records of TTmpMaterial
-- ----------------------------
INSERT INTO "TTmpMaterial" VALUES (9, 'TNT炸药', 'HIGH_EXPLOSIVE_BURN', '{R0,D,PCJ,BETA,K,G,SIGY}', '1.63,0.693,0.21,0,0,0,0', 'JWL', '{A,B,R1,R2,OMEG,E0,V0}', '3.713,0.0323,4.15,0.95,0.3,0.043,1.0', 0);
INSERT INTO "TTmpMaterial" VALUES (10, '钨合金', 'JOHNSON_COOK', '{RO,G,E,PR,DTF,VP,A,B,N,C,M,TM,TR,EPSO,CP,PC,SPAL,IT,D1,D2,D3,D4,D5}', '17.6,1.36,3.5,0.284,,,0.151E-01,0.177E-02,0.12,0.80E-02,1.0,0.1450E+04,294,0.100E-05,0.134E-05,-9.00E+00,3.00,0.0,1.5,0.00,0.00,0.00,0.00', 'GRUNEISEN', '{C,S1,S2,S3,GAMAO,A,E0,V0}', '0.399,1.24,0,0,1.54,0,0,0', 0);
INSERT INTO "TTmpMaterial" VALUES (11, '钨合金1', 'PLASTIC_KINEMATIC', '{RO,E,PR,SIGY,ETAN,BETA,SRC,SRP,FS,VP}', '18.62,1.17,0.22,1.79E-02,,1,,,0.8,0', '空', ' ', '  ', 0);
INSERT INTO "TTmpMaterial" VALUES (12, '45#钢', 'PLASTIC_KINEMATIC', '{RO,E,PR,SIGY,ETAN,BETA,SRC,SRP,FS,VP}', '7.896,2.1,0.284,1.00E-02,,1,,,0.8,0', '空', '  ', '  ', 0);

-- ----------------------------
-- Table structure for TWarheadInfo
-- ----------------------------
DROP TABLE IF EXISTS "TWarheadInfo";
CREATE TABLE "TWarheadInfo" (
  "WarheadId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "WarheadName" varchar(255) NOT NULL,
  "WarheadType" integer NOT NULL,
  "diameter_FrontCover" real NOT NULL,
  "Length_FrontCover" real NOT NULL,
  "diameter_RearCover" real NOT NULL,
  "Length_RearCover" real NOT NULL,
  "diameter_CenterTube" real NOT NULL,
  "Length_CenterTube" real NOT NULL,
  "thickness_Separator" real NOT NULL,
  "fragmentSpacing" real NOT NULL,
  "CoverMaterialId" integer NOT NULL,
  "SeparatorMaterialId" integer NOT NULL,
  "DrugMaterialId" integer NOT NULL,
  "FragmentArrangeId" integer NOT NULL,
  "warheadMeshSize" real
);

-- ----------------------------
-- Records of TWarheadInfo
-- ----------------------------
INSERT INTO "TWarheadInfo" VALUES (4, '测试战斗部', 0, 11.0, 1.0, 11.0, 0.8, 2.0, 2.0, 0.1, 0.005, 12, 10, 9, 1, 0.1);
INSERT INTO "TWarheadInfo" VALUES (5, 'ceshi ', 0, 11.0, 1.0, 11.0, 0.8, 2.0, 2.0, 0.1, 0.005, 6, 6, 6, 2, 0.1);
INSERT INTO "TWarheadInfo" VALUES (6, 'ceshi', 0, 11.0, 1.0, 11.0, 0.8, 2.0, 2.0, 0.1, 0.005, 8, 12, 10, 3, 0.1);
INSERT INTO "TWarheadInfo" VALUES (7, 'ceshi2', 0, 11.0, 1.0, 11.0, 0.8, 2.0, 2.0, 0.1, 0.005, 8, 12, 10, 3, 0.1);
INSERT INTO "TWarheadInfo" VALUES (8, '实验工况1', 0, 11.0, 1.0, 11.0, 0.8, 2.0, 2.0, 0.1, 0.005, 10, 12, 9, 3, 0.1);
INSERT INTO "TWarheadInfo" VALUES (9, '测试战斗部', 0, 11.0, 1.0, 11.0, 0.8, 2.0, 2.0, 0.1, 0.005, 12, 10, 9, 1, 0.1);

-- ----------------------------
-- Table structure for _TSimulinkParaMeter_old_20250225
-- ----------------------------
DROP TABLE IF EXISTS "_TSimulinkParaMeter_old_20250225";
CREATE TABLE "_TSimulinkParaMeter_old_20250225" (
  "SimulinkParameterId" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  "SimulinkParameterName" varchar(255) NOT NULL,
  "EndTime" real NOT NULL,
  "OutPutInterval" real NOT NULL,
  "FluidStructureInteractionStrength" real NOT NULL,
  "Slsfac" real NOT NULL,
  "Tssfac" real NOT NULL,
  "Pfac" real,
  "Nquad" integer
);

-- ----------------------------
-- Records of _TSimulinkParaMeter_old_20250225
-- ----------------------------

-- ----------------------------
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "sqlite_sequence";
CREATE TABLE "sqlite_sequence" (
  "name",
  "seq"
);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "sqlite_sequence" VALUES ('TFragmentArrange', 3);
INSERT INTO "sqlite_sequence" VALUES ('TSegmentedList', 8);
INSERT INTO "sqlite_sequence" VALUES ('TFragments', 17);
INSERT INTO "sqlite_sequence" VALUES ('TTmpMaterial', 12);
INSERT INTO "sqlite_sequence" VALUES ('TWarheadInfo', 9);
INSERT INTO "sqlite_sequence" VALUES ('TEquationOfStateType', 4);
INSERT INTO "sqlite_sequence" VALUES ('TStrengthModelType', 6);
INSERT INTO "sqlite_sequence" VALUES ('TSimulinkParaMeter', 5);
INSERT INTO "sqlite_sequence" VALUES ('TSimulationCondition', 6);
INSERT INTO "sqlite_sequence" VALUES ('TImportPoints', 189);

-- ----------------------------
-- Auto increment value for TEquationOfStateType
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 4 WHERE name = 'TEquationOfStateType';

-- ----------------------------
-- Auto increment value for TFragmentArrange
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 3 WHERE name = 'TFragmentArrange';

-- ----------------------------
-- Auto increment value for TFragments
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 17 WHERE name = 'TFragments';

-- ----------------------------
-- Auto increment value for TImportPoints
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 189 WHERE name = 'TImportPoints';

-- ----------------------------
-- Auto increment value for TSegmentedList
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 8 WHERE name = 'TSegmentedList';

-- ----------------------------
-- Auto increment value for TSimulationCondition
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 6 WHERE name = 'TSimulationCondition';

-- ----------------------------
-- Auto increment value for TSimulinkParaMeter
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 5 WHERE name = 'TSimulinkParaMeter';

-- ----------------------------
-- Auto increment value for TStrengthModelType
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 6 WHERE name = 'TStrengthModelType';

-- ----------------------------
-- Auto increment value for TTmpMaterial
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 12 WHERE name = 'TTmpMaterial';

-- ----------------------------
-- Auto increment value for TWarheadInfo
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 9 WHERE name = 'TWarheadInfo';

PRAGMA foreign_keys = true;
