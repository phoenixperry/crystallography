using System;
using Sce.PlayStation.Core;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QColor : AbstractQuality
	{
		protected static AbstractQuality _instance;
		
		// GET & SET --------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QColor Instance {
			get {
				if(_instance == null) {
					_instance = new QColor();
					return _instance as QColor;
				} else { 
					return _instance as QColor; 
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		public static Vector4[] palette = new Vector4[3];
		
		// CONSTRUCTOR ------------------------------------------------------------------
		
		protected QColor() : base()
		{
//			Instance = this;
			_name = "QColor";
//			Instance = this;
			setPalette();
			           
			           //new Vector4(0.96f,0.88f,0.88f,1.0f), // pink
//			           new Vector4(0.90f,0.075f,0.075f,1.0f), // red
//			           new Vector4(0.16f,0.88f,0.88f,1.0f) ); // teal
		}
		
		// OVERRIDES --------------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant ) {
			if ( pEntity is SpriteTileCrystallonEntity ) {
				(pEntity.getNode() as SpriteBase).Color = palette[pVariant];
				
//				var entityTintTo = new TintTo(palette[pVariant], 0.0f) {
//					Get = () => ( pEntity.getNode() as SpriteBase).Color,
//					Set = value => { (pEntity.getNode() as SpriteBase).Color = value; },
//					Tween = (x) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut(x,2)
//				};
				( pEntity as SpriteTileCrystallonEntity ).setColor(pVariant);
//				Director.Instance.CurrentScene.RunAction(entityTintTo);
			}
		}
		
//		public override bool Match ( ICrystallonEntity[] pEntities ) {
//			return base.Match(pEntities);
//		}
		
		// METHODS ----------------------------------------------------------------------
		
		public void setPalette () {
			setPalette( LevelManager.Instance.Palette[0], LevelManager.Instance.Palette[1], LevelManager.Instance.Palette[2] );
		}
		
		public void setPalette( Vector4 pColor1, Vector4 pColor2, Vector4 pColor3 ) {
			palette[0] = pColor1;
			palette[1] = pColor2;
			palette[2] = pColor3;
		}
		
		public void rotatePalette() {
			Vector4 temp = palette[0];
			for (var i = 0; i < palette.Length-1; i++) {
				palette[i] = palette[i+1];
				Console.WriteLine(palette[i].ToString());
			}
			palette[palette.Length-1] = temp;
		}
	}
}
