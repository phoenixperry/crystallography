using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class QOrientation : AbstractQuality
	{
		protected static AbstractQuality _instance;
		
		// GET & SET --------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QOrientation Instance {
			get {
				if(_instance == null) {
					_instance = new QOrientation();
					return _instance as QOrientation;
				} else { 
					return _instance as QOrientation;
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		// CONSTRUCTOR ------------------------------------------------------------
		
		protected QOrientation () : base()
		{
//			Instance = this;
			_name = "QOrientation";
			if( AppMain.ORIENTATION_MATTERS ) {
				allSameScore = 0; // ------ All-Same for orientation reads as failure
			}
		}
		
		// OVERRIDES --------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant )
		{
			SpriteTileCrystallonEntity e = pEntity as SpriteTileCrystallonEntity;
			e.setOrientation(pVariant);
			
			if (e.getSprite() != null ) {
				(e.getSprite() as SpriteTile).TileIndex2D = new Vector2i( e.getOrientation(), e.getPattern() ); //= ss.Get( e.getOrientation() + e.getPattern() ).TileIndex2D;
			}
		}
		
//		public override bool Match (ICrystallonEntity[] pEntities)
//		{
//			return base.Match(pEntities);
//		}
		
		// METHODS ----------------------------------------------------------------
	}
}

