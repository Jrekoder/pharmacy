using System;
namespace Pharmacy.Droid
{
	public static partial class Keys
	{
		public static partial class MobileCenter
		{
#if __IOS__
            //public const string AppSecret = @"";
#elif __ANDROID__
			//public const string AppSecret = @"";
#endif
		}

		public static partial class Bot
		{
			public const string DirectLineSecret = @"J7UYMTBu44Q.cwA.pzA.4Td_RQvjo4eoR5Yw_tzyWmFE74tePafQNI849Hs0V1c";
		}
	}
}
