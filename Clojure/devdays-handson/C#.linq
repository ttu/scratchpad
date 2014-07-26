<Query Kind="Statements" />

var myList = new List<int>{1,2,3,4,5};
myList.Dump();

myList.First().Dump("First");
myList.Skip(1).First().Dump("Second");

// Copy list
var list2 = myList.ConvertAll(i => i);
list2.Reverse();
list2.Dump("Reversed");
list2.First().Dump("Reversed first");

myList.Last().Dump("Last");

myList.Select(i => i * 2).Dump("Map Doubled");
myList.Sum().Dump("Sum reduced");
myList.Select(i => i).Aggregate((total, current) => total + current).Dump("Aggregate reduced");

myList.Where(i => i % 2 == 0).Dump("Filter");

var myCountFunc = new Func<int,int,int>((total,current) => {return total + current;});
myList.Aggregate(myCountFunc).Dump("Aggregate reduced with Func");

var longString = "37900490610897696126265185408732594047834333441947" +
                 "01850393807417064181700348379116686008018966949867" +
                 "75587222482716536850061657037580780205386629145841" +
                 "06964490601037178417735301109842904952970798120105"+
                 "47016802197685547844962006690576894353336688823830"+
                 "22913337214734911490555218134123051689058329294117"+
                 "83011983450277211542535458190375258738804563705619"+
                 "55277740874464155295278944953199015261800156422805"+
                 "72771774460964310684699893055144451845092626359982"+
                 "79063901081322647763278370447051079759349248247518";

// Convert to list on ints
var ints = longString.ToCharArray().Select(c => (int)Char.GetNumericValue(c)).ToList();

// Linq is missing batch or partition so has to use index
ints.Select((item, index) =>  new { I = item, Idx = index })
	.Dump("New items with index included");

ints.Select((item, index) =>  new { I = item, Idx = index })
	.Select(i => ints.Skip(i.Idx).Take(5))
	.Dump("Batches of 5");

ints.Select((item, index) =>  new { I = item, Idx = index })
	.Select(i => ints.Skip(i.Idx).Take(5))
	.Select(i => i.Aggregate((k,l) => k*l))
	.Dump("Batches multiplied to one value");
	
ints.Select((item, index) =>  new { I = item, Idx = index })
	.Select(i => ints.Skip(i.Idx).Take(5))
	.Select(i => i.Aggregate((k,l) => k*l))
	.Max()
	.Dump("Max");