using System;
using System.Collections.Generic;

namespace resmonengine
{
	public class CpuInfo
	{
		public string Name { get; set; }
		public int CurrentFreqency { get; set; }
	}

	public class CpuInfoList : List<CpuInfo> { }
}

