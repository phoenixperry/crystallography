using System;

namespace Crystallography
{
	public class QAnimation : AbstractQuality
	{
		protected static AbstractQuality _instance;
		
		// GET & SET --------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QAnimation Instance {
			get {
				if(_instance == null) {
					_instance = new QAnimation();
					return _instance as QAnimation;
				} else { 
					return _instance as QAnimation;
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		// CONSTRUCTOR -----------------------------------------------------------
		
		
		public QAnimation () : base() {
			_name = "QAnimation";
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant)
		{
			SpriteTileCrystallonEntity e = pEntity as SpriteTileCrystallonEntity;
			
			switch(pVariant)
			{
			case (0):
//				e.setPattern("Solid");
				break;
			case (1):
//				e.setPattern("Stripe");
				break;
			case (2):
//				e.setPattern("Dot");
				break;
			default:
				throw new NotImplementedException("QAnimation.Apply : pVariant must be 0,1,2");
				break;
			}
//			var ss = SpriteSingleton.getInstance();
//			(e.getNode() as SpriteTile).TileIndex2D = ss.Get( e.getOrientation() + e.getPattern() ).TileIndex2D;
			
		}
		
//		public override bool Match (ICrystallonEntity[] pEntities)
//		{
//			return base.Match(pEntities);
//		}
	}
}

