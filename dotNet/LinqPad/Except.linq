<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference Relative="..\..\..\..\Documents\Microsoft Visual Studio Async CTP\Samples\AsyncCtpLibrary.dll">&lt;MyDocuments&gt;\Microsoft Visual Studio Async CTP\Samples\AsyncCtpLibrary.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\ReachFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\System.Printing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationProvider.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationTypes.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\WindowsBase.dll</Reference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

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