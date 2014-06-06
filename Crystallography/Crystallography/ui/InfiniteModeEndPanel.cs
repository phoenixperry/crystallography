using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class InfiniteModeEndPanel : HudPanel
	{
		const int MAX_SOLUTIONS_ON_SCREEN = 6;
		const float MESSAGE_SWAP_DELAY = 4.0f;
		
		SpriteTile Background;
		
		Label MessageText;
		Label PossibleSolutionsText;
		
		BetterButton QuitButton;
		BetterButton ReplayButton;
		
		public SolutionIcon[] Solutions;
		public Vector4[] Colors;
		protected int messageIndex;
		protected float messageTimer;
		protected int visibleSolutionIndex;
		
		public string Text {
			get {
				return MessageText.Text;
			}
			set {
				MessageText.Text = value;
				CenterText();
			}
		}
		
		public event EventHandler QuitDetected;
		public event EventHandler RetryDetected;
		
		
		// CONSTRUCTOR --------------------------------------------------------------------------------------------------------------------------------------
		
		public InfiniteModeEndPanel () {
			DismissDelay = 0.0f;
			Width = 248.0f;
			
			Background = Support.UnicolorSprite("white", 255, 255, 255, 255);
			Background.Scale = new Vector2(Width/16.0f, 128.0f/16.0f);
			Background.RegisterPalette(0);
			this.AddChild(Background);
			
//			PossibleSolutionsText = new Label() {
//				Text = "all possible solutions:",
//				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold" ) ),
//				Position = new Vector2(40.0f, 180.0f)
//			};
//			PossibleSolutionsText.RegisterPalette(0);
//			this.AddChild( PossibleSolutionsText );
			
//			MessageText = new Label() {
//				Text = "you clever thing.",
//				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold" ) ),
//			};
//			MessageText.RegisterPalette(0);
//			this.AddChild( MessageText );
			
			ReplayButton = new BetterButton(94.0f + 30.0f, Background.CalcSizeInPixels().Y * Background.Scale.Y) {
				Text = "replay",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				Icon = Support.SpriteFromFile("/Application/assets/images/UI/replay.png"),
				IconAndTextOffset = new Vector2(32.0f, 10.0f),
				TextOffset = new Vector2(-45.0f, -45.0f),
				Position = new Vector2(Width - 124.0f, 0.0f),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			ReplayButton.background.RegisterPalette(0);
			ReplayButton.Icon.Color = LevelManager.Instance.BackgroundColor;
			ReplayButton.TextColor = LevelManager.Instance.BackgroundColor;
			this.AddChild(ReplayButton);
			
			
			QuitButton = new BetterButton(94.0f + 30.0f, Background.CalcSizeInPixels().Y * Background.Scale.Y) {
				Text = "menu",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				Icon = Support.SpriteFromFile("/Application/assets/images/UI/arrow.png"),
				IconAndTextOffset = new Vector2(32.0f, 80.0f),
				TextOffset = new Vector2(-38.0f, -45.0f),
				Position = new Vector2(Width - ReplayButton.Width - 124.0f, 0.0f),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			QuitButton.background.RegisterPalette(0);
			QuitButton.Icon.Color = LevelManager.Instance.BackgroundColor;
			QuitButton.Icon.Rotation = new Vector2(0.0f, -1.0f);
			QuitButton.TextColor = LevelManager.Instance.BackgroundColor;
			this.AddChild(QuitButton);
			
			Height = QuitButton.Height;
			
//			var charHeight = MessageText.FontMap.CharPixelHeight;
//			Height = (charHeight * 5.0f) + QuitButton.Height;
//			MessageText.Position = new Vector2(40.0f, QuitButton.Height + 20 );
//			CenterText();
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS ----------------------------------------------------------------------------------------------------------------------------------------
		
		void HandleOnSlideInComplete (object sender, EventArgs e)
		{
			messageTimer = 0.0f;
			messageIndex = 0;
//			if( Solutions.Length > MAX_SOLUTIONS_ON_SCREEN ) {
//				this.Schedule(SwapMessage, 1);
//			}
		}
		
		void HandleOnSlideInStart (object sender, EventArgs e)
		{
			ReplayButton.On(true);
			QuitButton.On(true);
			ReplayButton.ButtonUpAction += HandleReplayButtonButtonUpAction;
			QuitButton.ButtonUpAction += HandleQuitButtonButtonUpAction;
		}
		
		void HandleOnSlideOutComplete (object sender, EventArgs e)
		{
			CleanUpSolutions();
		}
		
		void HandleOnSlideOutStart (object sender, EventArgs e)
		{
//			this.Unschedule(SwapMessage);
			ReplayButton.ButtonUpAction -= HandleReplayButtonButtonUpAction;
			QuitButton.ButtonUpAction -= HandleQuitButtonButtonUpAction;
		}
		
		void HandleReplayButtonButtonUpAction (object sender, EventArgs e)
		{
			EventHandler handler = RetryDetected;
			if (handler != null ) {
				handler(this, null);
			}
		}

		void HandleQuitButtonButtonUpAction (object sender, EventArgs e)
		{
			EventHandler handler = QuitDetected;
			if ( handler != null ) {
				handler( this, null );
			}
		}
		
		// OVERRIDES --------------------------------------------------------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			OnSlideInComplete += HandleOnSlideInComplete;
			OnSlideOutComplete += HandleOnSlideOutComplete;
			OnSlideInStart += HandleOnSlideInStart;
			OnSlideOutStart += HandleOnSlideOutStart;
		}
		

		public override void OnExit ()
		{
			ReplayButton.ButtonUpAction -= HandleReplayButtonButtonUpAction;
			QuitButton.ButtonUpAction -= HandleQuitButtonButtonUpAction;
			OnSlideInComplete -= HandleOnSlideInComplete;
			OnSlideOutComplete -= HandleOnSlideOutComplete;
			OnSlideOutStart -= HandleOnSlideOutStart;
			OnSlideInStart -= HandleOnSlideInStart;
			
			Background = null;
			MessageText = null;
			PossibleSolutionsText = null;
			QuitButton = null;
			ReplayButton = null;
			
			CleanUpSolutions();
			
			base.OnExit ();
			RemoveAllChildren(true);
		}
		
		// METHODS ----------------------------------------------------------------------------------------------------------------------------------------------
		
		protected void CleanUpSolutions() {
		}
		
		public void Populate( int pCubes, int pScore) {
			PossibleSolutionsText.Text = "";
//			this.Text = "you clever thing.";
//			MessageText.Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f);
		}
		
		
		protected void CenterText() {
			var textWidth = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold").GetTextWidth(Text);
			MessageText.Position = new Vector2(0.5f * (Width - textWidth), MessageText.Position.Y);
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------------------------
#if DEBUG
        ~InfiniteModeEndPanel() {
			Console.WriteLine("InfiniteModeEndPanel deleted.");
        }
#endif
	}
}

