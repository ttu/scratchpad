<Query Kind="Statements" />

var csv = "a,b,c\na,b,d\nc,a,d\na,b,c";
var items = csv.Split('\n');
var grouped = items.GroupBy(a => a);
var distinct = grouped.Select(g => new {Name = g.Key, Count = g.Count() });

items.Dump();
grouped.Dump();
distinct.Dump();


