using System;

namespace Crystallography
{
	public class QSound : AbstractQuality
	{
		// CONSTRUCTOR -----------------------------------------------------------
		
		public QSound () : base()
		{
			_name = "QSound";
		}
		
		// OVERRIDES --------------------------------------------------------------
		
		public override void Apply (ICrystallonEntity pEntity, int pVariant)
		{
			AbstractCrystallonEntity e = pEntity as AbstractCrystallonEntity;
			
			switch(pVariant)
			{
			case (0):
				e.setSound("sound1.wav");
				break;
			case (1):
				e.setSound("sound2.wav");
				break;
			case (2):
				e.setSound("sound3.wav");
				break;
			default:
				throw new NotImplementedException("QSound.Apply : pVariant must be 0,1,2");
				break;
			}
		}
		
		public override bool Match (ICrystallonEntity[] pEntities)
		{
			return base.Match(pEntities);
		}
		
		// METHODS ----------------------------------------------------------------
	}
}

