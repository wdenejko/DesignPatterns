using System;

namespace PluggableAdapter
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var adaptee = new Adaptee { Name = "FirstObject" };
			var target1 = new Target1 { Name = "SecondObject" };
			var target2 = new Target2 { Name = "ThirdObject" };

			var adapter1 = new Adapter(adaptee);
			var adapter2 = new Adapter(target1);
			var adapter3 = new Adapter(target2);

			Console.WriteLine(adapter1.Request("Adapter 1"));
			Console.WriteLine(adapter2.Request("Adapter 2"));
			Console.WriteLine(adapter3.Request("Adapter 3"));
		}

		public class Adapter : Adaptee
		{
			internal const string Splitter = " #";
			public Func<string,string> Request;

			public Adapter(Adaptee adaptee)
			{
				this.Request =
					delegate (string key) { return String.Concat("Request: ", AdapteeMethod(), key, Splitter, adaptee.Name); };
			}

			public Adapter(Target1 target1)
			{
				this.Request =
					delegate (string key) { return String.Concat("Request: ", target1.Target1Method(key), Splitter, target1.Name); };
			}

			public Adapter(Target2 target2)
			{
				this.Request =
					delegate (string key) { return String.Concat("Request: ", target2.Target2Method(key), Splitter, target2.Name); };
			}
		}

		public class Adaptee
		{
			public Adaptee()
			{
				this.Name = String.Empty;
			}

			public string Name { get; set; }

			public string AdapteeMethod()
			{
				return "Adaptee Method Inside - ";
			}
		}

		public class Target1
		{
			public string Target1Method(string name)
			{
				return string.Concat("Target1 Method Inside - ", name);
			}

			public string Name { get; set; }
		}

		public class Target2
		{
			public string Target2Method(string name)
			{
				return string.Concat("Target2 Method Inside - ", name);
			}

			public string Name { get; set; }
		}
	}
}
