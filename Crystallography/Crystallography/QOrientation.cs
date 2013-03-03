using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QOrientation : AbstractQuality
	{
		// CONSTRUCTOR ------------------------------------------------------------
		
		public QOrientation () : base()
		{
			_name = "QOrientation";
		}
		
		// OVERRIDES --------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant )
		{
			SpriteTileCrystallonEntity e = pEntity as SpriteTileCrystallonEntity;
			
			switch(pVariant)
			{
			case (0):
				e.setOrientation("Top");
				break;
			case (1):
				e.setOrientation("Left");
				break;
			case (2):
				e.setOrientation("Right");
				break;
			default:
				throw new NotImplementedException("QOrientation.Apply : pVariant must be 0,1,2");
				break;
			}
			var ss = SpriteSingleton.getInstance();
			(e.getNode() as SpriteTile).TileIndex2D = ss.Get( e.getOrientation() + e.getPattern() ).TileIndex2D;
		}
		
		public override bool Match (ICrystallonEntity[] pEntities)
		{
			return base.Match(pEntities);
		}
		
		// METHODS ----------------------------------------------------------------
	}
}

