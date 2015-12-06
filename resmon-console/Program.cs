using System;
using System.Linq;
using resmonengine;
using System.Threading.Tasks;
using System.Threading;

namespace resmonconsole
{
	class Program
	{
		public static void Main (string[] args)
		{
			try
			{
				Console.WriteLine ("App.Start");
				var running = true;
				var t = new Thread(new ThreadStart(() =>
				{
					Tracer.Trace("Thread.Start");
					while(running)
					{
						ShowCpuInfo();
						ShowThermalZone();
						Thread.Sleep(1000);
					}					
					Tracer.Trace("Thread.Stop");
				}));
				t.Start();
				Console.ReadLine();
				running = false;
				t.Join();
				Console.WriteLine ("App.End");
			}
			catch(Exception e) 
			{
				Tracer.Trace ("Error\r\n{0}\r\n{1}", e.Message, e.StackTrace);
			}
		}

		private static void ShowCpuInfo()
		{
			Tracer.Trace ("CpuInfos");
			var bll = new CpuInfoBll ();
			var data = bll.LoadAll ();
			foreach (var p in data.OrderBy(x => x.Name)) 
			{
				Tracer.Trace ("{0} : {1}Mhz", 
					p.Name, 
					(p.CurrentFreqency / 1000).ToString().PadLeft(20 - p.Name.Length));
			}
		}

		private static void ShowThermalZone()
		{
			Tracer.Trace ("ThermalZones");
			var bll = new ThermalZoneBll ();
			var data = bll.LoadAll ();
			foreach (var p in data.OrderBy(x => x.Name)) 
			{
				Tracer.Trace ("{0} : {1}°C", 
					p.Name, 
					(p.Temperature / 1000).ToString("#.00#").PadLeft(20 - p.Name.Length));
			}
		}
	}
}

