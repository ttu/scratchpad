<Query Kind="Program" />

class Person { public string Name; public int Location; public int Salary; };

void Main()
{
	var batch1 = new List<Person>(10)
			{
				new Person(){Name = "Andy", Location = 1, Salary = 1 },
				new Person(){Name = "Thomas", Location = 2, Salary = 1 },
				new Person(){Name = "Jefferson", Location = 2, Salary = 2 },
				new Person(){Name = "Ben", Location = 1, Salary = 2 }	
			};
			
	var batch2 = new List<Person>(10)
			{
				new Person(){Name = "Jack", Location = 1, Salary = 2 },
				new Person(){Name = "Daniel", Location = 2, Salary = 2 },
				new Person(){Name = "Sam", Location = 2, Salary = 1 },
				new Person(){Name = "Dick", Location = 2, Salary = 2 }
			};
	
	
	
	// Merge (combine 2 lists)
	var team = batch1.Union(batch2);
	
	// Filter (select only items that match with predicate)
	team.Where(x => x.Location == 2).Dump("Filter");
	
	// Map (execute selection to every item)
	team.Select(x => new {Nm = x.Name, Ss = x.Salary}).Dump("Map");
	team.Select(x => x.Location + ":" + x.Name).Dump("Map");	
	
	// Reduce (list to single item)
	team.Select(x => x.Salary).Sum().Dump("Reduce");
	team.Select(x => x.Salary).Aggregate((a,b) => a + b).Dump("Reduce");
	team.Select(x => x.Name).Aggregate((a,b) => a + "," + b).Dump("Reduce");
	
	// Zip
	var batch1bonus = new int[]{ 5,2,3,4 };	
	batch1.Zip(batch1bonus, (a,b) => new { Name = a.Name, Pay = a.Salary + b }).Dump("Zip");
}