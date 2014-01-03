<Query Kind="Program" />

void Main()
{
	var data = "Deer Bear River\nCar Car River\nDeer Car Bear";
	data.Dump("Data");

    var split = data.Split('\n');
	split.Dump("Split");
	
    var map = split.Select(s => s.Split(' ').Select(i => new { Title = i, Value = 1 }));		
	map.Dump("Map");
	
	var shuffle = map.SelectMany(i => i).GroupBy(i => i.Title);
	shuffle.Dump("Shuffle");
	
    var reduce = shuffle.Select(i => i).Select(i => new { Title = i.Key, Count = i.Count() });
	reduce.Dump("Reduce");
}

// Define other methods and classes here
