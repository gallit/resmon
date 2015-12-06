using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace resmonengine
{
	public class CpuInfoBll
	{
		private const string cpuInfoPath = "/sys/devices/system/cpu";

		public CpuInfoList LoadAll()
		{		
			var result = new CpuInfoList ();
			var data = new List<int> ();
			data.AddRange (GetAvalaibleCpuList ());
			//Parallel.ForEach (data, (x => 
			foreach(var x in data)
			{
				var directory = string.Format("{0}/cpu{1}", cpuInfoPath, x);
				var filePath = string.Format("{0}/{1}", directory, "cpufreq/scaling_cur_freq");
				try
				{
					if (File.Exists(filePath))
					{
						var cpuInfo = new CpuInfo()
						{
							Name = string.Format("Cpu{0}", x),
							CurrentFreqency = int.Parse(File.ReadAllText(filePath))
						};
						result.Add(cpuInfo);										
					}	
				}
				catch
				{
					Tracer.Trace("Warning, CpuInfo read failure : {0}", directory);
				}
			};		
			return result;
		}

		public int[] GetAvalaibleCpuList()
		{
			var result = new List<int> ();
			var values = File.ReadAllLines (string.Format ("{0}/possible", cpuInfoPath)) [0].Split ('-');
			var lowerCore = int.Parse (values[0]);
			var higherCore = int.Parse (values[1]);
			var i = lowerCore;
			while (i >= lowerCore && i <= higherCore) 
			{
				result.Add (i);
				i++;
			}
			return result.ToArray ();
		}
	}
}

