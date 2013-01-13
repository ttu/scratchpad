<Query Kind="Program" />

class Person { public string Name; public int Age; };

void Main()
{
	var found = new List<Person>(10)
			{
				new Person(){Name = "Andy", Age = 20},
				new Person(){Name = "Thomas", Age = 30},
				new Person(){Name = "Jefferson", Age = 40},
				new Person(){Name = "Ben", Age = 50},
				new Person(){Name = "Jack", Age = 60},
				new Person(){Name = "Daniel", Age = 70}
			};
			
	var old = new List<Person>(10)
			{
				new Person(){Name = "Andy", Age = 20},
				new Person(){Name = "Thomas", Age = 30},
				new Person(){Name = "Jefferson", Age = 40},
				new Person(){Name = "Harry", Age = 40}
			};
			
	var newFound = found.Select(s => s.Name).Except(old.Select(n => n.Name));
	
	var notFound = old.Select(s => s.Name).Except(found.Select(n => n.Name));
	
	var same = old.Select(s => s.Name).Except(notFound);
	
	newFound.Dump();
	
	notFound.Dump();
	
	same.Dump();
}