using System;

namespace resmonengine
{
	public static class Tracer
	{
		public static void Trace(string format, params object[] args)
		{
			Trace (null, format, args);
		}

		public static void Trace(Exception e, string format, params object[] args)
		{
			var traceInfo = (args != null && args.Length > 0 ? 
				string.Format (format, args) :
				format);
			if (e != null) 
			{
				traceInfo += string.Format (@"
--> e.Message = {0}
---
--> e.StackTrace = {1}", e.Message, e.StackTrace);
			}

			Console.WriteLine (traceInfo);
		}
	}
}

