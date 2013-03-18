using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class QPattern : AbstractQuality
	{
		
		public SpriteTile patternTiles;
		
		protected static AbstractQuality _instance;
		
		// GET & SET --------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QPattern Instance {
			get {
				if(_instance == null) {
					_instance = new QPattern();
					return _instance as QPattern;
				} else { 
					return _instance as QPattern;
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		// CONSTRUCTOR -----------------------------------------------------------
		
		protected QPattern() : base()
		{
//			Instance = this;
			_name = "QPattern";
			setPalette();
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant)
		{
			SpriteTileCrystallonEntity e = pEntity as SpriteTileCrystallonEntity;
			
			e.setPattern(pVariant);
			
//			switch(pVariant)
//			{
//			case (0):
//				e.setPattern(0);
//				break;
//			case (1):
//				e.setPattern(1);
//				break;
//			case (2):
//				e.setPattern(2);
//				break;
//			default:
//				throw new NotImplementedException("QPattern.Apply : pVariant must be 0,1,2");
//				break;
//			}
//			var ss = SpriteSingleton.getInstance();
			(e.getNode() as SpriteTile).TileIndex2D = new Vector2i( e.getOrientation(), e.getPattern() ); //ss.Get( e.getOrientation() + e.getPattern() ).TileIndex2D;
			
			
			
		}
		
		public void setPalette () {
			setPalette( LevelManager.Instance.PatternPath, 3, 3);
		}
		
		public void setPalette( string path, int columns, int rows) {
			patternTiles = Support.TiledSpriteFromFile( path, columns, rows );
		}
	}
}
