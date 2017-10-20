using System;

using Newtonsoft.Json;

namespace Pharmacy.Domain
{
	public class Content //: Entity
	{
		public string Name { get; set; }

		public string DisplayName { get; set; }

		public string ProducerId { get; set; }

		public string Description { get; set; }

		public string RemoteAssetUri { get; set; }

		//public UserRoles PublishedTo { get; set; } = UserRoles.Producer;

		public DateTimeOffset? PublishedAt { get; set; }
	}
}
