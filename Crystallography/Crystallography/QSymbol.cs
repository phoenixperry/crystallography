using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class QSymbol : AbstractQuality
	{
		public SpriteTile symbolTiles;
		
		protected static AbstractQuality _instance;
		
		// GET & SET -------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QSymbol Instance {
			get {
				if(_instance == null) {
					_instance = new QSymbol();
					return _instance as QSymbol;
				} else { 
					return _instance as QSymbol; 
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		// CONSTRUCTOR -----------------------------------------------------------
				
		public QSymbol () : base() {
			_name = "QSymbol";
			setPalette();	
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant) {
			CardCrystallonEntity e = pEntity as CardCrystallonEntity;
			
			if ( QualityManager.Instance.scoringQualityList.Contains("QSymbol") ) {
				e.setSymbol( pVariant );
			} else {
				e.setSymbol( null );
			}
		}
		
		/// <summary>
		/// Sets the palette to a default palette.
		/// </summary>
		public void setPalette () {
//			setPalette( LevelManager.Instance.SymbolPath, 3, 3);
		}
		
		/// <summary>
		/// Sets the palette to a specified palette.
		/// </summary>
		public void setPalette( string path, int columns, int rows) {
			LevelManager.Instance.SymbolPath = path;
//			symbolTiles = Support.TiledSpriteFromFile( path, columns, rows );
		}
	}
}

