<Query Kind="Program" />

class Person { public string Name; public int Age; };

void Main()
{
	List<Person> nameList = new List<Person>(10)
			{
				new Person(){Name = "Andy", Age = 20},
				new Person(){Name = "Thomas", Age = 30},
				new Person(){Name = "Jefferson", Age = 40},
				new Person(){Name = "Ben", Age = 50},
				new Person(){Name = "Jack", Age = 60},
				new Person(){Name = "Daniel", Age = 70}
			};
			
	List<Person> nameList2 = new List<Person>(10)
			{
				new Person(){Name = "Andy2", Age = 20},
				new Person(){Name = "Thomas2", Age = 30},
				new Person(){Name = "Jefferso2n", Age = 40},
				new Person(){Name = "Ben2", Age = 50},
				new Person(){Name = "Jack2", Age = 60},
				new Person(){Name = "Daniel2", Age = 70}
			};
	
	List<List<Person>> nameLists = new List<List<Person>>();
	nameLists.Add(nameList);
	nameLists.Add(nameList2);
	
	// Shows 2 lists
	nameLists.Dump("2 lists");
	
	// Combines inner lists to one
	var all = nameLists.SelectMany(l => l);
	all.Dump("2 lists as 1");
	
	int ageLimit = 50;
	
	// Select persons over X
	var selected = nameLists.SelectMany(l => l).Where(x => x.Age > ageLimit);
	selected.Dump("Persons over 50");
	
	// Select persons over X
	var query = from a in nameLists
			from b in a
			where b.Age > ageLimit
			select b.Name;

	query.Dump("Persons over 50");
	
	// Combine Dictionary Value Lists
	var dict = new Dictionary<int, List<double>>();
	dict.Add(1, new List<double>(){ 1.1, 1.2, 1.3 });
	dict.Add(2, new List<double>(){ 2.1, 2.2, 2.3 });
	dict.Add(3, new List<double>(){ 3.1, 3.2, 3.3 });
	
	dict.Dump("Whole dictionary");
	
	dict.Where(x => x.Key != 3).SelectMany(x => x.Value).Dump("Values of 1 and 2");
}