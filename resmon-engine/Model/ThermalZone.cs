using System;
using System.Collections.Generic;

namespace resmonengine
{
	public class ThermalZone
	{
		public string Name { get; set;}
		public double Temperature { get; set; }	}

	public class ThermalZoneList : List<ThermalZone> {}
}

