--https://sqlserverperformance.wordpress.com/tag/dmv-queries/

-- Currently running queries
SELECT sqltext.TEXT,
req.session_id,
req.status,
req.command,
req.cpu_time,
req.total_elapsed_time
FROM sys.dm_exec_requests req
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS sqltext 

--KILL [session_id]

-- Most common commands
SELECT
txt.text, total_elapsed_time
FROM
sys.dm_exec_query_stats stat
CROSS APPLY sys.dm_exec_sql_text (stat.sql_handle) txt
ORDER BY
total_elapsed_time desc 


-- Most time consuming commands
select
    source_code,stats.total_elapsed_time/1000000 as seconds,last_execution_time
from sys.dm_exec_query_stats as stats
    cross apply(SELECT text as source_code FROM sys.dm_exec_sql_text(sql_handle))AS query_text
order by total_elapsed_time desc


--Top 10 codes that takes maximum time
select top 10 source_code,stats.total_elapsed_time/1000000 as seconds,
last_execution_time from sys.dm_exec_query_stats as stats
cross apply
(SELECT text as source_code FROM sys.dm_exec_sql_text(sql_handle))
AS query_text
order by total_elapsed_time desc


--Top 10 codes that takes maximum physical_reads
select top 10 source_code,stats.total_elapsed_time/1000000 as seconds,
last_execution_time from sys.dm_exec_query_stats as stats
cross apply
(SELECT text as source_code FROM sys.dm_exec_sql_text(sql_handle))
AS query_text
order by total_physical_reads desc


-- Count All Rows and Size of Table
SELECT
TableName = obj.name,
TotalRows = prt.rows,
[SpaceUsed(KB)] = SUM(alloc.used_pages)*8
FROM sys.objects obj
JOIN sys.indexes idx on obj.object_id = idx.object_id
JOIN sys.partitions prt on obj.object_id = prt.object_id
JOIN sys.allocation_units alloc on alloc.container_id = prt.partition_id
WHERE
obj.type = 'U' AND idx.index_id IN (0, 1)
GROUP BY obj.name, prt.rows
ORDER BY TableName


-- Total size of tables
SELECT SUM(Size) AS [TotalSize(KB)] FROM
(
SELECT
Size = SUM(alloc.used_pages)*8
FROM sys.objects obj
JOIN sys.indexes idx on obj.object_id = idx.object_id
JOIN sys.partitions prt on obj.object_id = prt.object_id
JOIN sys.allocation_units alloc on alloc.container_id = prt.partition_id
WHERE
obj.type = 'U' AND idx.index_id IN (0, 1)
GROUP BY obj.name, prt.rows
) AS AllSizes


-- Has clustered index, but not nonclustered index
SELECT obj.name as TableName
FROM sys.objects obj
INNER JOIN sys.indexes idx ON idx.OBJECT_ID = obj.OBJECT_ID
WHERE
 (obj.type='U'
 AND obj.OBJECT_ID NOT IN (
  SELECT OBJECT_ID
  FROM sys.indexes
  WHERE index_id > 1)
 AND idx.Index_ID = 1)
 
-- Primary Key List
SELECT o.name As TableName,
 Max(s.name) As TableSchema,
 Max(i.name) As PrimaryKey,
 Max(s.name) As PrimaryKeySchema,
 Count(*) As NumberOfColumns 
FROM sys.objects o
INNER JOIN sys.schemas s On o.schema_id = s.schema_id
INNER JOIN sys.indexes i On o.object_id = i.object_id And i.is_primary_key = 1
INNER JOIN sys.index_columns ic On i.object_id = ic.object_id And i.index_id = ic.index_id
GROUP BY o.name


-- Missing IDs (in here from Item table)
;WITH Missing (missnum, maxid)
AS
(
 SELECT 1 AS missnum, (select max(Id) from Item)
 UNION ALL
 SELECT missnum + 1, maxid FROM Missing
 WHERE missnum < maxid
)
SELECT missnum
FROM Missing
LEFT OUTER JOIN Item on Item.Id = Missing.missnum
WHERE Item.Id is NULL
OPTION (MAXRECURSION 0);


-- Temp DB Size
SELECT
SUM(unallocated_extent_page_count
+ user_object_reserved_page_count
+ internal_object_reserved_page_count
+ mixed_extent_page_count
+ version_store_reserved_page_count) * (8.0/1024.0)
AS [TotalTempDBSizeInMB]
, SUM(unallocated_extent_page_count * (8.0/1024.0))
AS [FreeTempDBSpaceInMB]
FROM sys.dm_db_file_space_usage


-- Tables with Identity Fields
SELECT object_name(o.object_id) AS TableName, c.name as IdentityColumn
FROM sys.columns c JOIN sys.objects  o
ON c.object_id = o.object_id
WHERE is_identity = 1
AND o.type IN ('U')


-- View object dependencies
SELECT
OBJECT_NAME (referencing_id) as referencing_entity_name,
obj.type_desc AS referencing_desciption,
referenced_schema_name,
referenced_entity_name,
referenced_server_name,
referenced_database_name
FROM sys.sql_expression_dependencies AS sqled
INNER JOIN sys.objects AS obj ON sqled.referencing_id = obj.object_id

-- Database Sizes
exec sp_spaceused