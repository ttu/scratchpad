-- Create Category hierarchy
With CategoryHierarchy (Id, ParentId)
AS
(
	SELECT Id, ParentId FROM Category c 
	WHERE Id = 1

	UNION ALL

	SELECT c.Id, c.ParentId FROM Category C
	INNER JOIN CategoryHierarchy ch on c.ParentId = ch.Id
)

SELECT * FROM CategoryHierarchy