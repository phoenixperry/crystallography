using System;

namespace Crystallography
{
	public class QParticle : AbstractQuality
	{
		protected static AbstractQuality _instance;
		
		// GET & SET --------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QParticle Instance {
			get {
				if(_instance == null) {
					_instance = new QParticle();
					return _instance as QParticle;
				} else { 
					return _instance as QParticle;
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		// CONSTRUCTOR ----------------------------------------------------------
		
		public QParticle () : base() {
			_name = "QParticle";
		}
		
		// OVERRIDES ------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant)
		{
			SpriteTileCrystallonEntity e = pEntity as SpriteTileCrystallonEntity;
			
			e.setParticle(pVariant);
			
//			(e.getNode() as SpriteTile).TileIndex2D = new Vector2i( e.getOrientation(), e.getPattern() );
		}
		
		// METHODS ------------------------------------------------------------
		
		public void setPalette () {
//			setPalette( LevelManager.Instance.PatternPath, 3, 3);
		}
		
		public void setPalette( string path, int columns, int rows) {
//			patternTiles = Support.TiledSpriteFromFile( path, columns, rows );
		}
	}
}

