using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class LevelTitle : Layer
	{
		static readonly float X_OFFSET = 10.0f;
		
		SpriteUV Background;
//		SpriteUV[] Icons;
		SpriteTile[] Icons;
//		Label NextLevelText;
        Label LevelNumberText;
		Label TapToDismissText;
		FontMap map;
		List<Label> QualityNames;
		
		protected GameScene _scene;
		protected bool _initialized;
		protected bool _entering;
		protected bool _exiting;
		
		// CONSTRUCTORS -------------------------------------------------------------------------
		
		public LevelTitle (GameScene scene) {
			_scene = scene;
			
			
			if (_initialized == false) {
				Initialize();
			}
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS -----------------------------------------------------------------------
		
		void HandleInputManagerInstanceTapDetected (object sender, BaseTouchEventArgs e)
		{
			if(this.Position.Y < 272.0f){
				_entering = false;
				_exiting = true;
			}
		}
		
		void HandleInputManagerInstanceDragDetected (object sender, SustainedTouchEventArgs e)
		{
			if(this.Position.Y < 272.0f){
				_entering = false;
				_exiting = true;
			}
		}
		
		// OVERRIDES ----------------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			base.Update (dt);
			
			if(_entering) {
				var y = this.Position.Y;
				y -= dt * 1000.0f;
				if (y < 41.0f) {
					y = 41.0f;
					_entering = false;
					Sequence sequence = new Sequence();
					sequence.Add( new DelayTime( 4.0f ) );
					sequence.Add( new CallFunc( () => ExitAnim() ) );
					this.RunAction(sequence);
				}
				this.Position = new Vector2(X_OFFSET, y);
			}
			
			else if( _exiting ) {
//				this.StopAllActions();
				var y = this.Position.Y;
				y += dt * 1000.0f;
				if (y > 545.0f) {
					y = 545.0f;
					_exiting = false;
					Hide();
				}
				this.Position = new Vector2(X_OFFSET, y);
			}
			
//			if(_entering) {
//				var x = this.Position.X;
//				x -= dt * 1000.0f;
//				if (x < 100.0f) {
//					x = 100.0f;
//					_entering = false;
//					Sequence sequence = new Sequence();
//					sequence.Add( new DelayTime( 2.0f ) );
//					sequence.Add( new CallFunc( () => ExitAnim() ) );
//					this.RunAction(sequence);
//				}
//				this.Position = new Vector2(x, 272.0f);
//			}
//			
//			if( _exiting ) {
//				var x = this.Position.X;
//				x -= dt * 1000.0f;
//				if (x < -100.0f) {
//					x = -100.0f;
//					_exiting = false;
//					Hide();
//				}
//				this.Position = new Vector2(x, 272.0f);
//			}
		}
		
		// METHODS ------------------------------------------------------------------------------
		
		protected void Initialize() {
			_initialized = true;
//			this.Position = new Vector2( 970.0f, 272.0f );
			this.Position = new Vector2( X_OFFSET, 545.0f );
			
			Background = Support.SpriteUVFromFile("/Application/assets/images/LevelTitleBG.png");
			Background.Position = new Vector2(0.0f, 0.0f);
			this.AddChild(Background);
			
			QualityNames = new List<Label>();
			map = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18, "Regular"));
			LevelNumberText = new Label( "0", Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 102, "Bold")) );
			LevelNumberText.Position = new Vector2( 44.0f, 250.0f);
			LevelNumberText.Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f);
			
			Background.AddChild(LevelNumberText);
//			Console.WriteLine( "-------------------WIDTH: " + Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 72, "Bold").GetTextWidth("level"));
//			NextLevelText = new Label("level", map);

//			NextLevelText.Position = new Vector2( 44.0f, 350.0f);
//			Background.AddChild(NextLevelText);
			
			TapToDismissText = new Label("tap to dismiss", Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Regular") ) );
			TapToDismissText.Position = new Vector2( 39.0f, 20.0f);
			Background.AddChild(TapToDismissText);
			
			Icons = new SpriteTile[4];
			for( int i=0; i < Icons.Length; i++) {
//				Icons[i] = new SpriteUV(Support.SpriteUVFromFile("/Application/assets/images/icons/animation.png").TextureInfo);
//				Icons[i] = Support.SpriteUVFromFile("/Application/assets/images/icons/animation.png");
				Icons[i] = Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2);
//				var y = ( i > 1 ) ? 108.0f : 176.0f;
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
		
		public void Hide() {
			this.Visible = false;
			_entering = false;
			_exiting = false;
		}
		
		public void Show() {
			this.Visible = true;
		}
		
		public void EnterAnim() {
			this.StopAllActions();
			_entering = true;
			_exiting = false;
			this.Position = new Vector2(X_OFFSET, 545.0f);
			Show();
			InputManager.Instance.TapDetected += HandleInputManagerInstanceTapDetected;
			InputManager.Instance.DragDetected += HandleInputManagerInstanceDragDetected;
//			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
		}

		
		
		public void ExitAnim() {
			this.StopAllActions();
			InputManager.Instance.TapDetected -= HandleInputManagerInstanceTapDetected;
			InputManager.Instance.DragDetected -= HandleInputManagerInstanceDragDetected;
//			InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
			_exiting = true;
			_entering = false;
			Show();
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~LevelTitle() {
			Console.WriteLine("LevelTitle deleted.");
        }
#endif
	}
}

