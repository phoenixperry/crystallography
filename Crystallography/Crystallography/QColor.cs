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
			pEntity.getNode().RunAction( new TintTo( palette[pVariant], 0.0f ) );
			if ( pEntity is SpriteTileCrystallonEntity ) {
				( pEntity as SpriteTileCrystallonEntity ).setColor(pVariant);
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
	}
}
