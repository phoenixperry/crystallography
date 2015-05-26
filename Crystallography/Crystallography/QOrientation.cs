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
			var sprite = e.getSprite();
			e.setOrientation(pVariant);
			
			string orient;
			switch(pVariant) {
			case(0):
			default:
				orient = "T";
				break;
			case(1):
				orient = "L";
				break;
			case(2):
				orient = "R";
				break;
			}
			
//			string setName = LevelManager.Instance.PatternPath.Substring(0,4);
			
			if (sprite != null ) {
				sprite.TextureInfo = Support.TextureInfoFromAtlas("gamePieces", LevelManager.Instance.PatternPath + "_v" + e.getPattern().ToString() + "_" + orient + ".png");
				sprite.Scale = e.getSprite().CalcSizeInPixels();
				sprite.Position = sprite.Scale/-2.0f;
			}
		}
		
//		public override bool Match (ICrystallonEntity[] pEntities)
//		{
//			return base.Match(pEntities);
//		}
		
		// METHODS ----------------------------------------------------------------
	}
}

