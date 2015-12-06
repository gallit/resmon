using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace resmonengine
{
	public class ThermalZoneBll
	{
		private const string virtualThermalZonePath = "/sys/devices/virtual/thermal";
		public ThermalZoneList LoadAll()
		{
			var result = new ThermalZoneList ();
			var directories = new List<string> ();
			directories.AddRange(System.IO.Directory.GetDirectories (virtualThermalZonePath, "thermal_zone*"));
			//Parallel.ForEach(directories, (p => 
			foreach(var p in directories)
			{
				if (Directory.Exists(p))
				{
					try
					{
						var	item = new ThermalZone()
						{
							Name = File.ReadAllLines(string.Format("{0}/{1}", p, "type"))[0],
							Temperature = double.Parse(File.ReadAllText(string.Format("{0}/{1}", p, "temp")))
						};
						result.Add(item);					
					}
					catch
					{
						Tracer.Trace("Warning : ThermalZone read failure : {0}", p);
					}
				}
			};
			return result;
		}
	}
}