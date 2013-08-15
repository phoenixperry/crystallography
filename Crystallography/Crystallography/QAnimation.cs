using System;
//using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QAnimation : AbstractQuality
	{
		public static int[,] palette = new int[3,2];
		public static SpriteTile animTiles;
//		public SpriteTile maskTiles;
		
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
			setPalette();
//			maskTiles = Support.TiledSpriteFromFile("Application/assets/animation/masks.png", 3, 1);
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant)
		{
			CardCrystallonEntity e = pEntity as CardCrystallonEntity;
			e.setAnim(animTiles, palette[pVariant, 0], palette[pVariant, 1]);			
		}
		
		/// <summary>
		/// Match the specified pEntities.
		/// </summary>
		/// <param name='pEntities'>
		/// P entities.
		/// </param>
		public override int Match (ICrystallonEntity[] pEntities, bool pForScore)
		{
			//HACK Temporary override while we wait for Animations to be fully integrated!!!
//			return allSameScore;
			return base.Match(pEntities, pForScore );
		}
		
		/// <summary>
		/// Sets the palette to a default palette.
		/// </summary>
		public void setPalette () {
//			setPalette( "Application/assets/animation/leftFall/leftFall.png", 6, 5, 16, 16, 0, 15, 16, 25);
			//explain please why this is not 0-13 and 14-15. Thanks! :) 
			setPalette( "Application/assets/animation/animation2.png", 4, 4, 0,14,14,16,0,0);
//			setPalette( "Application/assets/animation/char_anims.png", 3, 2, 0, 0, 0, 3, 3, 6);
//			setPalette( "Application/assets/animation/ball_anim.png", 5, 1, 0, 0, 0, 5, 0, 5);
		}
		
		/// <summary>
		/// Sets the palette to a specified palette.
		/// </summary>
		public void setPalette( string path, int columns, int rows, int start1, int end1, int start2, int end2, int start3, int end3 ) {
			animTiles = Support.TiledSpriteFromFile( path, columns, rows );
			palette[0,0] = start1;
			palette[0,1] = end1;
			palette[1,0] = start2;
			palette[1,1] = end2;
			palette[2,0] = start3;
			palette[2,1] = end3;
		}
	}
}

