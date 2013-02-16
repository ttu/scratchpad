<Query Kind="Program" />

class Person { public string Name; public int Location; public int Group; };

void Main()
{
		var team = new List<Person>(10)
			{
				new Person(){Name = "Andy", Location = 1, Group = 1 },
				new Person(){Name = "Thomas", Location = 2, Group = 1 },
				new Person(){Name = "Jefferson", Location = 2, Group = 2 },
				new Person(){Name = "Ben", Location = 1, Group = 2 },
				new Person(){Name = "Jack", Location = 1, Group = 2 },
				new Person(){Name = "Daniel", Location = 2, Group = 2 },
				new Person(){Name = "Sam", Location = 2, Group = 1 },
				new Person(){Name = "Dick", Location = 2, Group = 2 }
			};

			team.Dump("Whole team");
			
			var grouped_a = team.GroupBy(t => t.Location);
			grouped_a.Dump("Grouped by location");
			
			var grouped_b = team.GroupBy(t => new { t.Location, t.Group });
			grouped_b.Dump("Grouped by location & group");
			
			IEnumerable<List<Person>> grouping;
			
			var selection = 0;
			
			if (selection == 1)
			{
				grouping = grouped_a.Select(x => x.ToList());
			}
			else 
			{
				grouping = grouped_b.Select(x => x.ToList());
			}
			
			foreach(var g in grouping)
			{
				g.Dump();
			}
}