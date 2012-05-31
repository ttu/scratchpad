
DECLARE @table_name nvarchar(100)
DECLARE @column_name nvarchar(100)
DECLARE @new_length nvarchar(100)
DECLARE @null nvarchar(100)
DECLARE @default_value nvarchar(100)


SET @table_name = 'Test'
SET @column_name = 'lastname'
SET @new_length = '30'
SET @null = 'null'
SET @default_value = 'test'

DECLARE @data_type nvarchar(100)
DECLARE @datatype_maximum_length nvarchar(100)

DECLARE @primary_column nvarchar(100)
DECLARE @pk_data_type nvarchar(100)
DECLARE @pk_datatype_maximum_length nvarchar(100)

DECLARE @sql nvarchar(MAX)

-- Get Old Column infos
SET @data_type =(
select data_type from information_schema.columns
where table_name = @table_name AND column_name = @column_name)

SET @datatype_maximum_length =(
select character_maximum_length from information_schema.columns
where table_name = @table_name AND column_name = @column_name)

-- Get primary key column infos
SET @primary_column = (SELECT TOP 1 column_name FROM information_schema.key_column_usage WHERE TABLE_NAME = @table_name)

SET @pk_data_type =(
select data_type from information_schema.columns
where table_name = @table_name AND column_name = @primary_column)

SET @pk_datatype_maximum_length =(
select character_maximum_length from information_schema.columns
where table_name = @table_name AND column_name = @primary_column)

IF @primary_column is null
	RAISERROR('No primary key in table. Script failed',16,1)
	
-- Create temp table with primary key and data column
SET @sql = '
CREATE TABLE #Temp_Data_{0} (
PK {8}'

IF @pk_datatype_maximum_length is not null
 SET @sql += '({9})'
 
SET @sql += ',
Data {2}'

IF @datatype_maximum_length is not null
 SET @sql += '({3})'
 
SET @sql += '
)
'

-- Copy data to temp table
SET @sql += '
INSERT INTO #Temp_Data_{0} (PK,Data) 
(
SELECT {7}, {1} FROM {0}
)
'

-- Drop column and possible constraint
SET @sql += '
ALTER TABLE {0}
DROP COLUMN {1}
'
-- Create new column
SET @sql += '
ALTER TABLE {0}
ADD {1} {2}'

IF @new_length is not null
 SET @sql += '({4})'
 
 SET @sql += ' {5}
 '
 
-- Copy data from temp table to new column
SET @sql += '
UPDATE {0}
SET {1} = 
(
SELECT '
IF @new_length is not null
BEGIN
	IF @new_length < @datatype_maximum_length
		SET @sql += 'LEFT(#Temp_Data_{0}.Data, {4})'
	ELSE
		SET @sql += '#Temp_Data_{0}.Data'
END
ELSE
	SET @sql += '#Temp_Data_{0}.Data'
	
SET @sql +=	' FROM #Temp_Data_{0} WHERE #Temp_Data_{0}.PK = {0}.{7}
)
'

-- Drop temp table
SET @sql += '
DROP TABLE #Temp_Data_{0}
'

SET @sql = REPLACE(@sql, '{0}', @table_name)
SET @sql = REPLACE(@sql, '{1}', @column_name)
SET @sql = REPLACE(@sql, '{2}', @data_type)

IF @datatype_maximum_length is not null
	SET @sql = REPLACE(@sql, '{3}', @datatype_maximum_length)

SET @sql = REPLACE(@sql, '{4}', @new_length)

SET @sql = REPLACE(@sql, '{5}', @null)
SET @sql = REPLACE(@sql, '{6}', @default_value)

SET @sql = REPLACE(@sql, '{7}', @primary_column)
SET @sql = REPLACE(@sql, '{8}', @pk_data_type)

IF @pk_datatype_maximum_length is not null
	SET @sql = REPLACE(@sql, '{9}', @pk_datatype_maximum_length)

PRINT @Sql
--EXEC sp_executesql @sql

