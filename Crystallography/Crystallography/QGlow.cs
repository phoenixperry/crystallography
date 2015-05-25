using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QGlow : AbstractQuality
	{
		protected static AbstractQuality _instance;
		public static Vector4[] palette = new Vector4[3];
		
		public SpriteTile GlowTiles;
		
		// GET & SET -------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QGlow Instance {
			get {
				if(_instance == null) {
					_instance = new QGlow();
					return _instance as QGlow;
				} else { 
					return _instance as QGlow; 
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		// CONSTRUCTORS -----------------------------------------------------------
		
		public QGlow () {
			_name = "QGlow";
			GlowTiles = Support.TiledSpriteFromFile("Application/assets/images/glow.png", 3, 1);
			setPalette();
		}
		
		// METHODS ----------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant ) {
			if (pEntity is CardCrystallonEntity == false) return;
			CardCrystallonEntity e = pEntity as CardCrystallonEntity;
			e.setGlow( pVariant );
			if (pVariant == -1) {
				return;
			}
			e.GlowSprite.RunAction( new TintTo(palette[pVariant], 0.0f) );
//			pEntity.getNode().RunAction( new TintTo( palette[pVariant], 0.0f ) );
		}
		
		public void setPalette() {
			setPalette( new Vector4(0.89411765f, 0.61176471f, 1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.9254902f, 1.0f), new Vector4(1.0f, 1.0f, 0.1176471f, 1.0f));
		}
		
		public void setPalette( Vector4 pColor1, Vector4 pColor2, Vector4 pColor3 ) {
			palette[0] = pColor1;
			palette[1] = pColor2;
			palette[2] = pColor3;
		}
	}
}

