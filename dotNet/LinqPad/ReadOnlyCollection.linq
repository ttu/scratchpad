<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Collections.dll</Reference>
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

void Main()
{
	CloningExample(true);
}

void CloningExample(bool clone = false)
{
	var nameList = CreateList();
	
	nameList.Dump("Original");
	
	var enumNameList = nameList as IEnumerable<Customer>;
	
	// Not possible
	//enumNameList.Add(new Customer(){ Name = "Third" });
	
	foreach(var item in enumNameList)
	{
		item.Name = item.Name + "_edit";
	}
	
	enumNameList.Dump("After IEnumerable edit");
	
	ReadOnlyCollection<Customer> readOnlyNameList;
	
	if (clone)
		readOnlyNameList = new ReadOnlyCollection<Customer>(nameList.ConvertAll(i => new Customer() {Name = i.Name}));
	else
		readOnlyNameList = new ReadOnlyCollection<Customer>(nameList);
	
	// Not possible
	//readOnlyNameList.Add(new Customer(){ Name = "Third" });
	
	foreach(var item in readOnlyNameList)
	{
		item.Name = item.Name + "_edit";
	}
	
	readOnlyNameList.Dump("After ReadOnly edit");
	
	nameList.Dump("Original in the end");
}

List<Customer> CreateList()
{
	var nameList = new List<Customer>();
	nameList.Add(new Customer(){ Name = "First" });
	nameList.Add(new Customer(){ Name = "Second" });
	return nameList;
}

class Customer
	{
		public string Name {get;set;}
	}
