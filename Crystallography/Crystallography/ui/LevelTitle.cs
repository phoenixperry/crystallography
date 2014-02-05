using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class LevelTitle : HudPanel
	{
		public static readonly float X_OFFSET = 10.0f;
		
		SpriteTile Background;
		SpriteTile[] Icons;
        Label LevelNumberText;
		Label TapToDismissText;
		FontMap map;
		List<Label> QualityNames;
		
//		protected GameScene _scene;
		protected bool _initialized;
		
		public bool Entering { get; protected set; }
		public bool Exiting { get; protected set; }
		
		// CONSTRUCTORS -------------------------------------------------------------------------
		
		public LevelTitle (GameScene scene) {
//			_scene = scene;
			
			
			if (_initialized == false) {
				Initialize();
			}
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS -----------------------------------------------------------------------
		
		// OVERRIDES ----------------------------------------------------------------------------
		
		public override void OnExit ()
		{
			Background = null;
			for( int i=0; i<Icons.Length; i++) {
				Icons[i] = null;
			}
			Icons = null;
			LevelNumberText = null;
			Label TapToDismiss = null;
			map = null;
//			Support.RemoveTextureWithFileName("/Application/assets/images/LevelTitleBG.png");
			QualityNames.Clear();
//			_scene = null;
			
			base.OnExit ();
			RemoveAllChildren(true);
		}
		
		// METHODS ------------------------------------------------------------------------------
		
		protected void Initialize() {
			_initialized = true;
			
			Background = Support.SpriteFromFile("/Application/assets/images/LevelTitleBG.png");
			Background.Position = new Vector2(0.0f, 0.0f);
			this.AddChild(Background);
			
			this.Height = Background.CalcSizeInPixels().Y;
			this.Width = Background.CalcSizeInPixels().X;
			
			QualityNames = new List<Label>();
			map = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18, "Regular"));
			LevelNumberText = new Label(){
				Text = "00", 
				FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 102, "Bold")),
				Position = new Vector2( 44.0f, 250.0f),
//				Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f)
			};
			LevelNumberText.RegisterPalette(0);
			
			Background.AddChild(LevelNumberText);
			
			TapToDismissText = new Label() {
				Text = "tap to dismiss", 
				FontMap = map,
				Position = new Vector2( 39.0f, 20.0f)
			};
			TapToDismissText.RegisterPalette(0);
			Background.AddChild(TapToDismissText);
			
			Icons = new SpriteTile[4];
			for( int i=0; i < Icons.Length; i++) {
				Icons[i] = Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2);
				float y = 176.0f - 88.0f * (float)System.Math.Floor(i/2.0f);
				Icons[i].Position = new Vector2( 44.0f + 68.0f*(i%2), y);
				Background.AddChild(Icons[i]);
				Icons[i].Visible = false;
			}
		}
		
		public void SetLevelText( int pNumber ) {
			LevelNumberText.Text = pNumber.ToString("00");
			var x = (220.0f - Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 102, "Bold").GetTextWidth(LevelNumberText.Text))/2.0f;
			LevelNumberText.Position = new Vector2(x, LevelNumberText.Position.Y);
		}
		
		public void SetQualityNames( string[] pNames ) {
			foreach ( Label l in QualityNames) {
				this.RemoveChild(l, true);
			}
			QualityNames.Clear();
			Label n;
			int i = 0;
			foreach(SpriteTile icon in Icons) {
				icon.Visible = false;
			}
			foreach ( string name in pNames ) {
				Icons[i].TileIndex1D = (int)EnumHelper.FromString<Crystallography.Icons>(name);
				Icons[i].Visible = true;
				QualityNames.Add( n = new Label() );
				n.Color = Colors.White;
				n.FontMap = map;
				n.Text = name;
				n.Position = new Vector2( Icons[i].Position.X, Icons[i].Position.Y - 20.0f ); //(QualityNames.Count-1)*80.0f, -25.0f);
				this.AddChild(n);
				i++;
			}
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~LevelTitle() {
			Console.WriteLine("LevelTitle deleted.");
        }
#endif
	}
}

