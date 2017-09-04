#if __MOBILE__
using System;
using Newtonsoft.Json;
#endif

namespace Pharmacy.Domain
{
	public class AvContent : Content
	{
		public double Duration { get; set; }

		public AvContentTypes ContentType { get; set; }
	}
}
