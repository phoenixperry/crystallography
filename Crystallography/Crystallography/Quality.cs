using System;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class Quality
	{
		public enum VALUE { ONE=0x1, TWO=0x2, THREE=0x4 };
		
		public Quality()
		{
		}
		
		public bool Compare( Quality q )
		{
			bool match = q.GetType() == this.GetType();
//			if (match) { 
//				match = this.qualityValue == q.qualityValue;
//			}
			return match;
		}
		
		public void Update()
		{
		}
		
		public virtual void ApplyQuality( Node host, uint val )
		{
		}
	}
}	
