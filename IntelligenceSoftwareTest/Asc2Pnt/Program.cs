using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Asc2Pnt.Model;

namespace Asc2Pnt
{
	class Program
	{
		[STAThread]
		static int Main(string[] args)
		{
			try
			{
				var arguments = GetArguments(args, defaultValues: new Dictionary<string, string>{ { "mode", "console" }});
				var storage = new FileStorage<DiscretePoint>(new DiscretePointSerializer());

				var figure = storage.LoadFromStorage(arguments("in"));
				var turnsDetector = new TurnsDetector();
				Action work = () =>
					{
						var turnPoints = turnsDetector.FindTurnPointsIn(figure);
						storage.SaveToStorage(arguments("out"), turnPoints);
						Console.WriteLine("Результаты записаны в {0}", arguments("out"));
				};

				if (arguments("mode") == "winforms")
				{
					var form = new DemoForm();
					turnsDetector.PointHandled += (o,e) => form.ShowPoint(e.Point);
					turnsDetector.TurnDetected += (o,e) => form.ShowTurn(e.Point);
					turnsDetector.DemoMode = true;
					var handle = new ManualResetEvent(false);
					ThreadPool.QueueUserWorkItem(_ => { work(); handle.Set(); });
					Application.Run(form);
					
					// если форму закроют раньше, чем отработает рабочий процесс, дадим ему доделать свою работу
					if (!handle.WaitOne(0))
					{
						Console.WriteLine("Подождите, идет завершение процесса вычисления...");
						handle.WaitOne();
					}

				}
				else
				{
					work();
					WaitForInput();
				}

				return 0;
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine("Не указан параметр: {0}", ex.Message);
				WaitForInput();
				return 1;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ошибка: {0}", ex.Message);
				WaitForInput();
				return 2;
			}
		}

		private static void WaitForInput()
		{
			Console.WriteLine("Нажмите Enter для завершения...");
			Console.ReadLine();
		}

		private static Func<string, string> GetArguments(IEnumerable<string> args, Dictionary<string, string> defaultValues)
		{
			var paramRegex = new Regex(@"/(?<name>[\w\d]+)\=(?<value>.+)");
			var dictionary = (from arg in args
			                  let match = paramRegex.Match(arg)
			                  where match.Success
			                  select new {Key = match.Groups["name"].Value, match.Groups["value"].Value})
				.ToDictionary(_ => _.Key, _ => _.Value);

			return key =>
				{
					if (dictionary.ContainsKey(key))
						return dictionary[key];
					else if (defaultValues.ContainsKey(key))
						return defaultValues[key];
					else
						throw new ArgumentException(key);
				};
		}
	}
}
