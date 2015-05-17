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
			_name = "QPattern";
			setPalette();
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant)
		{
			SpriteTileCrystallonEntity e = pEntity as SpriteTileCrystallonEntity;
			
			e.setPattern(pVariant);
			
			(e.getNode() as SpriteTile).TileIndex2D = new Vector2i( e.getOrientation(), e.getPattern() );
		}
		
		public void setPalette () {
			setPalette( LevelManager.Instance.PatternPath, 3, 3);
		}
		
		public void setPalette( string path, int columns, int rows) {
//			patternTiles = Support.TiledSpriteFromFile( path, columns, rows );
			var atlas = path.Substring(0, path.LastIndexOf('.'));
			patternTiles = Support.TiledSpriteFromAtlas(atlas, "gamePieces.png", columns, rows);
		}
	}
}
