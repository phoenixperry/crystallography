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
		BetterButton RetryButton;
		
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
			Width = 448.0f;
			
			Background = Support.UnicolorSprite("Grey", 40, 40, 40, 200);
			Background.Scale = new Vector2(28.0f, 15.0f);
			this.AddChild(Background);
			
			PossibleSolutionsText = new Label() {
				Text = "all possible solutions:",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold" ) ),
				Position = new Vector2(40.0f, 180.0f)
			};
			PossibleSolutionsText.RegisterPalette(0);
			this.AddChild( PossibleSolutionsText );
			
			MessageText = new Label() {
				Text = "you clever thing.",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold" ) ),
			};
			MessageText.RegisterPalette(0);
			this.AddChild( MessageText );
			
			
			QuitButton = new BetterButton(117.0f, 53.0f) {
				Text = "quit",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				Position = new Vector2(0.0f, 0.0f),
				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			QuitButton.background.RegisterPalette(2);
			this.AddChild(QuitButton);
			
			RetryButton = new BetterButton(176.0f, 53.0f) {
				Text = "try again",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				Position = new Vector2(QuitButton.Width + 4.0f , 0.0f),
			};
			RetryButton.background.RegisterPalette(2);
			this.AddChild(RetryButton);
			
			var charHeight = MessageText.FontMap.CharPixelHeight;
			Height = (charHeight * 5.0f) + QuitButton.Height;
			MessageText.Position = new Vector2(40.0f, QuitButton.Height + 20 );
			CenterText();
			
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
			RetryButton.On(true);
			QuitButton.On(true);
			RetryButton.ButtonUpAction += HandleRetryButtonButtonUpAction;
			QuitButton.ButtonUpAction += HandleQuitButtonButtonUpAction;
		}
		
		void HandleOnSlideOutComplete (object sender, EventArgs e)
		{
			CleanUpSolutions();
		}
		
		void HandleOnSlideOutStart (object sender, EventArgs e)
		{
//			this.Unschedule(SwapMessage);
			RetryButton.ButtonUpAction -= HandleRetryButtonButtonUpAction;
			QuitButton.ButtonUpAction -= HandleQuitButtonButtonUpAction;
		}
		
		void HandleRetryButtonButtonUpAction (object sender, EventArgs e)
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
			RetryButton.ButtonUpAction -= HandleRetryButtonButtonUpAction;
			QuitButton.ButtonUpAction -= HandleQuitButtonButtonUpAction;
			OnSlideInComplete -= HandleOnSlideInComplete;
			OnSlideOutComplete -= HandleOnSlideOutComplete;
			OnSlideOutStart -= HandleOnSlideOutStart;
			OnSlideInStart -= HandleOnSlideInStart;
			
			Background = null;
			MessageText = null;
			PossibleSolutionsText = null;
			QuitButton = null;
			RetryButton = null;
			
			CleanUpSolutions();
			
			base.OnExit ();
			RemoveAllChildren(true);
		}
		
		// METHODS ----------------------------------------------------------------------------------------------------------------------------------------------
		
		protected void CleanUpSolutions() {
		}
		
		public void Populate( int pCubes, int pScore) {
			PossibleSolutionsText.Text = "";
			this.Text = "you clever thing.";
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

