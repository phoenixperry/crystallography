using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.BG
{
	public class CrystallonBackground : Layer
	{
		protected int _screenWidth;
		protected int _screenHeight;
		
		protected List<Vector2> BASES;
		protected List<Vector2> RANGES;
		
		protected List<CrystallonBackgroundObject> BackgroundObjects;
		
		private List<SpriteBase> Color0Objects;
		private List<SpriteBase> Color1Objects;
		private List<SpriteBase> Color2Objects;
		
		// CONSTRUCTOR -------------------------------------------------------------------------------
		
		public CrystallonBackground () : base () {
			BASES = new List<Vector2>();
			RANGES = new List<Vector2>();
			Color0Objects = new List<SpriteBase>();
			Color1Objects = new List<SpriteBase>();
			Color2Objects = new List<SpriteBase>();
			
			_screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			
			BackgroundObjects = new List<CrystallonBackgroundObject>();
			
			PickBackground();
			
			GameScene.LevelChangeDetected += (sender, e) => { SetPalette(); };
			
			SetPalette();
		}
		
		// OVERRIDES ---------------------------------------------------------------------------
		
		
		// METHODS -----------------------------------------------------------------------------
		
		
		
		public void BG1() {
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.32f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.13f*_screenHeight) );
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.45f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.1f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[0], RANGES[0]) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[1], RANGES[1]) );
			
			var s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/leftSolidWhite.png"),
				Pivot = new Vector2(1.0f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color1Objects.Add(s);
			BackgroundObjects[0].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/rightSolidWhite.png"),
				Pivot = new Vector2(0.0f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color1Objects.Add(s);
			BackgroundObjects[0].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/topSolidChevron.png"),
				Pivot = new Vector2(0.5f, 0.04f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color1Objects.Add(s);
			BackgroundObjects[0].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/leftHollowWhite.png"),
				Pivot = new Vector2(1.0f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[1].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/rightHollowWhite.png"),
				Pivot = new Vector2(0.0f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[1].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/topSolidChevron.png"),
				Pivot = new Vector2(0.5f, 0.04f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color0Objects.Add(s);
			BackgroundObjects[1].AddChild(s);	
		}
		
		public void BG2() {
			
			// CENTER SHAPE -------------------------------------------------------------------------------------
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[0], RANGES[0]) );
			
			var s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color1Objects.Add(s);
			BackgroundObjects[0].AddChild(s);
			
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.1f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[1], RANGES[1]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[1].AddChild(s);
			
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.1f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[2], RANGES[2]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.4f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[2].AddChild(s);
			
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.05f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[3], RANGES[3]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.3f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[3].AddChild(s);
			
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[4], RANGES[4]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color2Objects.Add(s);
			BackgroundObjects[4].AddChild(s);
			
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[5], RANGES[5]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.4f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color2Objects.Add(s);
			BackgroundObjects[5].AddChild(s);
			
			BASES.Add ( new Vector2(0.5f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[6], RANGES[6]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.3f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color2Objects.Add(s);
			BackgroundObjects[6].AddChild(s);
			
			// LEFT SHAPE ------------------------------------------------------------------------------------
			BASES.Add ( new Vector2(0.17f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[7], RANGES[7]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[7].AddChild(s);
			
			BASES.Add ( new Vector2(0.17f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.1f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[8], RANGES[8]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[8].AddChild(s);
			
			BASES.Add ( new Vector2(0.17f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.1f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[9], RANGES[9]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.4f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[9].AddChild(s);
			
			BASES.Add ( new Vector2(0.17f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.05f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[10], RANGES[10]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.3f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color0Objects.Add(s);
			BackgroundObjects[10].AddChild(s);
			
			BASES.Add ( new Vector2(0.17f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[11], RANGES[11]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color0Objects.Add(s);
			BackgroundObjects[11].AddChild(s);
			
			BASES.Add ( new Vector2(0.17f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[12], RANGES[12]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.4f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color2Objects.Add(s);
			BackgroundObjects[12].AddChild(s);
			
			BASES.Add ( new Vector2(0.17f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[13], RANGES[13]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.3f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color2Objects.Add(s);
			BackgroundObjects[13].AddChild(s);
			
			// RIGHT SHAPE ------------------------------------------------------------------------------------
			BASES.Add ( new Vector2(0.83f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[14], RANGES[14]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[14].AddChild(s);
			
			BASES.Add ( new Vector2(0.83f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.1f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[15], RANGES[15]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color0Objects.Add(s);
			BackgroundObjects[15].AddChild(s);
			
			BASES.Add ( new Vector2(0.83f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.1f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[16], RANGES[16]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.4f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color0Objects.Add(s);
			BackgroundObjects[16].AddChild(s);
			
			BASES.Add ( new Vector2(0.83f*_screenWidth, 0.55f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.05f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[17], RANGES[17]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.3f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[17].AddChild(s);
			
			BASES.Add ( new Vector2(0.83f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[18], RANGES[18]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color2Objects.Add(s);
			BackgroundObjects[18].AddChild(s);
			
			BASES.Add ( new Vector2(0.83f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[19], RANGES[19]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.4f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color0Objects.Add(s);
			BackgroundObjects[19].AddChild(s);
			
			BASES.Add ( new Vector2(0.83f*_screenWidth, 0.39f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.10f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[20], RANGES[20]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/2/whiteHollowChevron.png"),
				Pivot = new Vector2(0.5f, 0.3f)
			};
			s.Scale = s.CalcSizeInPixels() * new Vector2(1f,-1f);
			Color2Objects.Add(s);
			BackgroundObjects[20].AddChild(s);
		}
		
		public void BG3() {
			
			BASES.Add ( new Vector2(0.65f*_screenWidth, 0.32f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.13f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[0], RANGES[0]) );
			
			var s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/3/zigTotal.png"),
				Pivot = new Vector2(0.69f, -0.2f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color1Objects.Add(s);
			BackgroundObjects[0].AddChild(s);
			
			BASES.Add ( new Vector2(0.65f*_screenWidth, 0.32f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.13f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[1], RANGES[1]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/leftHollowWhite.png"),
				Pivot = new Vector2(1.0f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[1].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/rightHollowWhite.png"),
				Pivot = new Vector2(0.0f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[1].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/leftHollowWhite.png"),
				Pivot = new Vector2(0.0f, 0.0f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[1].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/rightHollowWhite.png"),
				Pivot = new Vector2(1.0f, 0.0f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[1].AddChild(s);
			
			BASES.Add ( new Vector2(0.312f*_screenWidth, 0.32f*_screenHeight) );
			RANGES.Add ( new Vector2(0.0f, 0.13f*_screenHeight) );
			
			BackgroundObjects.Add( new CrystallonBackgroundObject(BASES[2], RANGES[2]) );
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/leftHollowWhite.png"),
				Pivot = new Vector2(1.0f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[2].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/rightHollowWhite.png"),
				Pivot = new Vector2(0.0f, 0.5f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[2].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/leftHollowWhite.png"),
				Pivot = new Vector2(0.0f, 0.0f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[2].AddChild(s);
			
			s = new SpriteUV() {
				TextureInfo = new TextureInfo("/Application/assets/images/bg/1/rightHollowWhite.png"),
				Pivot = new Vector2(1.0f, 0.0f)
			};
			s.Scale = s.CalcSizeInPixels();
			Color2Objects.Add(s);
			BackgroundObjects[2].AddChild(s);
		}
		
		public void PickBackground() {
			Reset();
			
			switch( (int)System.Math.Floor( GameScene.Random.NextFloat()*3.0f ) ) {
			case(0):
				BG1();
				break;
			case(1):
				BG2();
				break;
			case(2):
				BG3();
				break;
			default:
				BG1();
				break;
			}
			
			foreach ( CrystallonBackgroundObject o in BackgroundObjects ) {
				AddChild(o);
				o.Start();
			}
		}
		
		public void Reset() {
			foreach ( CrystallonBackgroundObject o in BackgroundObjects ) {
				o.RemoveAllChildren( true );
				RemoveChild(o, true);
			}
			BASES.Clear();
			RANGES.Clear();
			Color0Objects.Clear();
			Color1Objects.Clear();
			Color2Objects.Clear();
			BackgroundObjects.Clear();
		}
		
		public void SetPalette() {
			foreach ( SpriteBase s in Color0Objects ) {
				s.RunAction( new TintTo( LevelManager.Instance.Palette[0], 1.0f ) );
			}
			foreach ( SpriteBase s in Color1Objects ) {
				s.RunAction( new TintTo( LevelManager.Instance.Palette[1], 1.0f ) );
			}
			foreach ( SpriteBase s in Color2Objects ) {
				s.RunAction( new TintTo( LevelManager.Instance.Palette[2], 1.0f ) );
			}
		}
	}
}

