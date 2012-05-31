USE master
GO
-- KILL ALL Current connections to MyDB
DECLARE @SPId int
DECLARE @DatabaseName nvarchar(50)
SET @DatabaseName = N'MyDB'
DECLARE my_cursor CURSOR FAST_FORWARD FOR
SELECT SPId FROM MASTER..SysProcesses WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId
OPEN my_cursor
	FETCH NEXT FROM my_cursor INTO @SPId
	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC('KILL '+@SPId)
		FETCH NEXT FROM my_cursor INTO @SPId
	END
CLOSE my_cursor
DEALLOCATE my_cursor 
GO
ALTER DATABASE MyDB SET single_user with no_wait
--rollback immediate
GO
RESTORE DATABASE [MyDB] FROM  DISK = N'C:\Bak_folder\MyDB.bak' WITH  FILE = 1,
  MOVE N'TPCentralDB_Data' TO N'C:\DB_folder\MyDB.MDF',
  MOVE N'TPCentralDB_Log' TO N'C:\DB_folder\MyDB.LDF',
  NOUNLOAD,  REPLACE,  STATS = 10
GO
ALTER DATABASE MyDB SET multi_user with no_wait

GO

