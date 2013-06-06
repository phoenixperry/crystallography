using System;

namespace Crystallography
{
	public static class EnumHelper
	{
//		public EnumHelper ()
//		{
//		}
		
//		public static T FromInt<T>(int value) { 
//			return (T)value;
//		}
		
		public static T FromString<T>(string value) { 
			return (T)Enum.Parse(typeof(T), value, true); 
		}
	}
}

