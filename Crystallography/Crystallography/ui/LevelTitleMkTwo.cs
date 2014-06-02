using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


namespace Crystallography.UI
{
	public class LevelTitleMkTwo : HudPanel 
	{
		SpriteTile[] Icons;
		HudPanel[] IconSliders;
		Label TapToDismissLabel;
        Label LevelTitleLabel;
		HudPanel LevelTitleSlider;
		List<Label> QualityNames;
		
		static readonly float ICON_MOVE_DURATION = 0.5f;
		
		protected float iconWidth;
		
		protected bool _initialized = false;
		
		// GET & SET -------------------------------------------------
		
		public string Title { 
			get {
				if(LevelTitleLabel != null) {
					return LevelTitleLabel.Text;
				}
				return null;
			}
			set {
				if(LevelTitleLabel != null) {
					LevelTitleLabel.Text = value;
				}
			}
		}
		
		// CONSTRUCTOR ------------------------------------------------
		
		public LevelTitleMkTwo () : base() {
			if (_initialized == false) {
				Initialize();
			}
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS ---------------------------------------------
		
		/// <summary>
		/// Triggers when icons reach their destination after sliding into view (on screen).
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Event arguments
		/// </param>
		void HandleOnSlideInComplete (object sender, EventArgs e) {
			Sequence sequence = new Sequence();
			
			for ( int i=0; i < QualityNames.Count; i++ ) {
				var slider = IconSliders[i];
				float x = ( (float)QualityNames.Count - (float)i ) * 960.0f/( 1.0f + (float)QualityNames.Count ) - Icons[i].CalcSizeInPixels().X/2.0f;
				
//				Icons[i].Visible = true;
				slider.Position = slider.Offset = new Vector2(x, 0.0f);
				sequence.Add( new CallFunc( () => {
					slider.Visible = true;
					slider.SlideIn();
				}));
				sequence.Add( new DelayTime(0.5f * ICON_MOVE_DURATION) );
			}
			sequence.Add( new CallFunc( () => { 
				TapToDismissLabel.Visible = true;
			}));
			Director.Instance.CurrentScene.RunAction(sequence);
		}
		
		// OVERRIDES --------------------------------------------------
		
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			OnSlideInComplete += HandleOnSlideInComplete;
		}
		
		
		public override void OnExit ()
		{
			OnSlideInComplete -= HandleOnSlideInComplete;
			
			base.OnExit ();
			RemoveAllChildren(true);
			
			for( int i=0; i<Icons.Length; i++) {
				Icons[i] = null;
			}
			Icons = null;
			LevelTitleLabel = null;
			QualityNames.Clear();
		}
		
		/// <summary>
		/// Slide the icons out of view (off screen).
		/// </summary>
		public override void SlideOut ()
		{
			Sequence sequence = new Sequence();
			sequence.Add ( new CallFunc ( () => {
				TapToDismissLabel.Visible = false;
			}));
			sequence.Add( new DelayTime( 0.5f * ICON_MOVE_DURATION ) );
			sequence.Add ( new CallFunc ( () => {
				base.SlideOut ();
			}));
			Director.Instance.CurrentScene.RunAction(sequence);
		}
		
		// METHODS ----------------------------------------------------
		
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize() {
			SlideInDirection = SlideDirection.RIGHT;
			SlideOutDirection = SlideDirection.LEFT;
			Width = Director.Instance.GL.Context.Screen.Width;
			Height = Director.Instance.GL.Context.Screen.Height;
			
			LevelTitleSlider = new HudPanel(){
				SlideInDirection = this.SlideInDirection,
				SlideOutDirection = this.SlideOutDirection
			};
			this.AddChild(LevelTitleSlider);
			
			LevelTitleLabel = new Label() {
				Text = "00",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold")),
				Position = new Vector2(30.0f, 0.75f * 544.0f)
			};
			this.AddChild(LevelTitleLabel);
			LevelTitleLabel.RegisterPalette(0);
			
			TapToDismissLabel = new Label() {
				Text = "tap to dismiss", 
				FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold")),
				Position = new Vector2( 20.0f, 20.0f)
			};
			TapToDismissLabel.RegisterPalette(0);
			this.AddChild(TapToDismissLabel);
			TapToDismissLabel.Visible = false;
			
			
			QualityNames = new List<Label>();
			
			IconSliders = new HudPanel[4];
			for( int i=0; i < IconSliders.Length; i++ ) {
				IconSliders[i] = new HudPanel() {
					SlideInDirection = this.SlideInDirection,
					SlideOutDirection = this.SlideOutDirection,
					Width = Director.Instance.GL.Context.Screen.Width,
					Height = Director.Instance.GL.Context.Screen.Height,
					MoveDuration = ICON_MOVE_DURATION
				};
				float x = ( (float)IconSliders.Length - (float)i ) * 960.0f/( 1.0f + (float)IconSliders.Length );
				IconSliders[i].Position = new Vector2( x, 0.0f );
				IconSliders[i].Offset = new Vector2(x, 0.0f);
				IconSliders[i].Visible = false;
				this.AddChild(IconSliders[i]);
			}
			
			Icons = new SpriteTile[4];
			for( int i=0; i < Icons.Length; i++) {
				Icons[i] = Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2);
				float y = 544.0f/2.0f - Icons[i].CalcSizeInPixels().Y/2.0f;
				Icons[i].RegisterPalette(i%3);
				Icons[i].Position = new Vector2(0.0f, y);
				IconSliders[i].AddChild(Icons[i]);
//				Icons[i].Visible = false;
			}
			iconWidth = Icons[0].CalcSizeInPixels().X;
		}
		
		/// <summary>
		/// Sets the contents of the labels that appear under the icons at level start
		/// </summary>
		/// <param name='pNames'>
		/// P names.
		/// </param>
		public void SetQualityNames( string[] pNames ) {
			foreach ( Label l in QualityNames) {
				l.Parent.RemoveChild(l, true);
			}
			QualityNames.Clear();
			Label n;
			int i = 0;
			foreach(HudPanel icon in IconSliders) {
				icon.Visible = false;
			}
			foreach ( string name in pNames ) {
				var ix = (int?)EnumHelper.FromString<Crystallography.Icons>(name) ?? 0;
				Icons[i].TileIndex1D = ix;
				QualityNames.Add( n = new Label() {
					FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18, "Regular")),
					Text = name,
//					Position = new Vector2( 0.0f, -20.0f )
				});
				n.Position = new Vector2( 0.5f * (iconWidth - n.GetlContentLocalBounds().Size.X), -20.0f ); // CENTER TEXT UNDER ICON
				n.RegisterPalette(i%3);
				Icons[i].AddChild(n);
				i++;
			}
		}
		
		// DESTRUCTOR -------------------------------------------------
#if DEBUG
        ~LevelTitleMkTwo() {
			Console.WriteLine("LevelTitle deleted.");
        }
#endif
	}
}

