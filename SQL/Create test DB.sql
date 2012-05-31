USE master
GO

IF EXISTS (SELECT * FROM master.dbo.sysdatabases  WHERE name = 'TestDB')
	DROP DATABASE TestDB
END IF
GO

CREATE DATABASE TestDB
GO

USE [TestDB]

IF EXISTS (SELECT * FROM sysobjects where name = 'Item') 
	DROP TABLE Item
GO

CREATE TABLE Item(
	[Id] INT NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	PRIMARY KEY ([Id])
)

INSERT INTO Item VALUES (1, NEWID())
INSERT INTO Item VALUES (2, NEWID())
INSERT INTO Item VALUES (3, NEWID())
INSERT INTO Item VALUES (5, NEWID())
INSERT INTO Item VALUES (8, NEWID())
INSERT INTO Item VALUES (10, NEWID())
INSERT INTO Item VALUES (11, NEWID())

IF EXISTS (SELECT * FROM sysobjects where name = 'Category') 
	DROP TABLE Category
GO


CREATE TABLE Category(
	[Id] INT NOT NULL,
	[ParentId] INT NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	PRIMARY KEY ([Id])
)

INSERT INTO Category VALUES (1, NULL, NEWID())
INSERT INTO Category VALUES (2, NULL, NEWID())
INSERT INTO Category VALUES (3, 1, NEWID())
INSERT INTO Category VALUES (5, 1, NEWID())
INSERT INTO Category VALUES (8, 2, NEWID())
INSERT INTO Category VALUES (10, 3, NEWID())
INSERT INTO Category VALUES (11, 10, NEWID())
GO