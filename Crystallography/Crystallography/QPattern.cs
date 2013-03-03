using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QPattern : AbstractQuality
	{
		public QPattern() : base()
		{
			_name = "QPattern";
		}
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant)
		{
			SpriteTileCrystallonEntity e = pEntity as SpriteTileCrystallonEntity;
			
			switch(pVariant)
			{
			case (0):
				e.setPattern("Solid");
				break;
			case (1):
				e.setPattern("Stripe");
				break;
			case (2):
				e.setPattern("Dot");
				break;
			default:
				throw new NotImplementedException("QPattern.Apply : pVariant must be 0,1,2");
				break;
			}
			var ss = SpriteSingleton.getInstance();
			(e.getNode() as SpriteTile).TileIndex2D = ss.Get( e.getOrientation() + e.getPattern() ).TileIndex2D;
			
		}
		
		public override bool Match (ICrystallonEntity[] pEntities)
		{
			return base.Match(pEntities);
		}
	}
}
