<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft Reactive Extensions SDK\v1.1.11111\Binaries\.NETFramework\v4.0\System.Reactive.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft Reactive Extensions SDK\v1.1.11111\Binaries\.NETFramework\v4.0\System.Reactive.Providers.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft Reactive Extensions SDK\v1.1.11111\Binaries\.NETFramework\v4.0\System.Reactive.Windows.Threading.dll</Reference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Reactive</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Concurrency</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
</Query>

void Main()
{
	var input = new Subject<int>();
	
	input.Subscribe(
		x => Console.WriteLine(x),
		ex => Console.WriteLine(ex.Message),
		() => Console.WriteLine("Done"));
		
	input.OnNext(1);
	input.OnNext(4);
	input.OnCompleted();
	
	Console.WriteLine("---------------");
	
	var source = Observable.Range(1, 5);
	source.Subscribe(value => value.Dump());
	
	Observable.Start(() => 3);
	
	Console.WriteLine("---------------");
	
	var result = AsyncAdd(9,3);
	result.Dump();
	
	Console.WriteLine("---------------");
	
	AsyncCall(64).Subscribe(x => "Here".Dump());
	
	var tt = AsyncCall(16).First();
	tt.Dump();
	

	var results = new[]{1,2,3,4,5,6,7,8,9,10}.ToObservable()
		.SelectMany(x => AsyncCall(x))
		.ToList().First();

	results.Dump();
}

public IObservable<int> AsyncAdd(int a, int b)
{
	return Observable.Start(() => a+b);
}

public IObservable<int> AsyncCall(int a)
{
	return Observable.Start(() => a);
}