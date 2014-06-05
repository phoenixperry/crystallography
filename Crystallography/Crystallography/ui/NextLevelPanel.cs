using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class NextLevelPanel : HudPanel
	{
		const int MAX_SOLUTIONS_ON_SCREEN = 6;
		const float MESSAGE_SWAP_DELAY = 4.0f;
		
		SpriteTile Background;
		SpriteTile DiagonalLine;
		
//		Label MessageText;
//		Label PercentageText;
		Label FoundSolutionsText;
		Label PossibleSolutionsText;
		Label OutOfText;
		Label TotalSolutionsText;
		
//		ButtonEntity QuitButton;
//		ButtonEntity LevelSelectButton;
//		ButtonEntity NextLevelButton;
		
		BetterButton QuitButton;
		BetterButton LevelSelectButton;
		BetterButton NextLevelButton;
		
		public SolutionIcon[] Solutions;
		public Vector4[] Colors;
		protected int messageIndex;
		protected float messageTimer;
		protected int visibleSolutionIndex;
		
//		public string Text {
//			get {
//				return MessageText.Text;
//			}
//			set {
//				MessageText.Text = value;
//				CenterText();
//			}
//		}
		
//		public float Percentage {
//			get {
//				return float.Parse(PercentageText.Text);
//			}
//			set {
//				PercentageText.Text = value.ToString("P0");
//			}
//		}
		
		public event EventHandler QuitDetected;
		public event EventHandler LevelSelectDetected;
		public event EventHandler NextLevelDetected;
		
		
		// CONSTRUCTOR --------------------------------------------------------------------------------------------------------------------------------------
		
		public NextLevelPanel () {
			DismissDelay = 0.0f;
			Width = 458.0f;
			
			Background = Support.UnicolorSprite("white", 255, 255, 255, 255);
			Background.Scale = new Vector2(448.0f/16.0f, 128.0f/16.0f);
			Background.RegisterPalette(0);
			this.AddChild(Background);
			
			
			
//			MessageText = new Label() {
//				Text = "you clever thing.",
//				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold" ) ),
//			};
//			MessageText.RegisterPalette(0);
//			this.AddChild( MessageText );
			
			
			NextLevelButton = new BetterButton(78.0f + 30.0f, Background.CalcSizeInPixels().Y * Background.Scale.Y) {
				Text = "next",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
//				IconOnLeft = false,
				Icon = Support.SpriteFromFile("/Application/assets/images/UI/arrow.png"),
				IconAndTextOffset = new Vector2(22.0f, 10.0f),
				TextOffset = new Vector2(-40.0f, -45.0f),
				Position = new Vector2(Width - 108.0f, 0.0f),
//				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			NextLevelButton.background.RegisterPalette(0);
			NextLevelButton.Icon.Color = LevelManager.Instance.BackgroundColor;
			NextLevelButton.TextColor = LevelManager.Instance.BackgroundColor;
			this.AddChild(NextLevelButton);
			
			LevelSelectButton = new BetterButton(80.0f + 30.0f, Background.CalcSizeInPixels().Y * Background.Scale.Y) {
				Text = "select",
				Icon = Support.SpriteFromFile("/Application/assets/images/UI/levels.png"),
				IconAndTextOffset = new Vector2(30.0f, 10.0f),
				TextOffset = new Vector2(-35.0f, -45.0f),
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				Position = new Vector2(Width - NextLevelButton.Width - 110.0f, 0.0f),
			};
			LevelSelectButton.background.RegisterPalette(0);
			LevelSelectButton.Icon.Color = LevelManager.Instance.BackgroundColor;
			LevelSelectButton.TextColor = LevelManager.Instance.BackgroundColor;
			this.AddChild(LevelSelectButton);
			
			QuitButton = new BetterButton(94.0f + 30.0f, Background.CalcSizeInPixels().Y * Background.Scale.Y) {
				Text = "quit",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				Icon = Support.SpriteFromFile("/Application/assets/images/UI/replay.png"),
				IconAndTextOffset = new Vector2(22.0f, 10.0f),
				TextOffset = new Vector2(-45.0f, -45.0f),
				Position = new Vector2(Width - NextLevelButton.Width - LevelSelectButton.Width - 124.0f, 0.0f),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			QuitButton.background.RegisterPalette(0);
			QuitButton.Icon.Color = LevelManager.Instance.BackgroundColor;
			QuitButton.TextColor = LevelManager.Instance.BackgroundColor;
			this.AddChild(QuitButton);
			
			DiagonalLine = Support.SpriteFromFile("/Application/assets/images/UI/diagonalLine.png");
			DiagonalLine.Position = new Vector2( QuitButton.Position.X - DiagonalLine.CalcSizeInPixels().X - 5.0f , 15.0f);
			DiagonalLine.Color = LevelManager.Instance.BackgroundColor;
			this.AddChild(DiagonalLine);
			
			PossibleSolutionsText = new Label() {
				Text = "solutions\n  found",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 14, "Bold" ) ),
				Position = new Vector2(15.0f, 80.0f)
			};
//			PossibleSolutionsText.RegisterPalette(0);
			PossibleSolutionsText.Color = LevelManager.Instance.BackgroundColor;
			this.AddChild( PossibleSolutionsText );
			
			OutOfText = new Label() {
				Text = "out of",
				FontMap = PossibleSolutionsText.FontMap,
				Position = new Vector2(60.0f, 40.0f)
			};
			OutOfText.Color = LevelManager.Instance.BackgroundColor;
			this.AddChild( OutOfText );
			
			FoundSolutionsText = new Label() {
				Text = "00",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold" ) ),
				Position = new Vector2(24.0f, 90.0f)
			};
			FoundSolutionsText.Color = LevelManager.Instance.BackgroundColor;
			this.AddChild(FoundSolutionsText);
			
			TotalSolutionsText = new Label{
				Text = "00",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold" ) ),
				Position = new Vector2(62.0f, 15.0f)
			};
			TotalSolutionsText.Color = LevelManager.Instance.BackgroundColor;
			this.AddChild(TotalSolutionsText);
			
//			var charHeight = MessageText.FontMap.CharPixelHeight;
//			Height = (charHeight * 5.0f) + QuitButton.Height;
			Height = QuitButton.Height;
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
			if( Solutions.Length > MAX_SOLUTIONS_ON_SCREEN ) {
				this.Schedule(SwapMessage, 1);
			}
		}
		
		void HandleOnSlideInStart (object sender, EventArgs e)
		{
			LevelSelectButton.On(false);
			NextLevelButton.On(false);
			if (   GameScene.currentLevel != 0
			    && GameScene.currentLevel != 999 ) {
				
				LevelSelectButton.On(true);
				LevelSelectButton.ButtonUpAction += HandleLevelSelectButtonButtonUpAction;
				if ( GameScene.currentLevel != GameScene.TOTAL_LEVELS-1 ) {
					NextLevelButton.On(true);
					NextLevelButton.ButtonUpAction += HandleNextLevelButtonButtonUpAction;
				}
			}
			QuitButton.ButtonUpAction += HandleQuitButtonButtonUpAction;
		}
		
		void HandleOnSlideOutComplete (object sender, EventArgs e)
		{
			CleanUpSolutions();
		}
		
		void HandleOnSlideOutStart (object sender, EventArgs e)
		{
			this.Unschedule(SwapMessage);
			NextLevelButton.ButtonUpAction -= HandleNextLevelButtonButtonUpAction;
			LevelSelectButton.ButtonUpAction -= HandleLevelSelectButtonButtonUpAction;
			QuitButton.ButtonUpAction -= HandleQuitButtonButtonUpAction;
		}
		
		void HandleNextLevelButtonButtonUpAction (object sender, EventArgs e)
		{
			EventHandler handler = NextLevelDetected;
			if ( handler != null ) {
				handler( this, null );
			}
		}

		void HandleLevelSelectButtonButtonUpAction (object sender, EventArgs e)
		{
			EventHandler handler = LevelSelectDetected;
			if ( handler != null ) {
				handler( this, null );
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
			NextLevelButton.ButtonUpAction -= HandleNextLevelButtonButtonUpAction;
			LevelSelectButton.ButtonUpAction -= HandleLevelSelectButtonButtonUpAction;
			QuitButton.ButtonUpAction -= HandleQuitButtonButtonUpAction;
			OnSlideInComplete -= HandleOnSlideInComplete;
			OnSlideOutStart -= HandleOnSlideOutStart;
			OnSlideInStart -= HandleOnSlideInStart;
			
			Background = null;
//			MessageText = null;
			PossibleSolutionsText = null;
			QuitButton = null;
			LevelSelectButton = null;
			NextLevelButton = null;
			
			CleanUpSolutions();
			
			base.OnExit ();
			RemoveAllChildren(true);
		}
		
		// METHODS ----------------------------------------------------------------------------------------------------------------------------------------------
		
		protected void CleanUpSolutions() {
			if (Solutions != null) {
				for( int i=0; i < Solutions.Length; i++ ) {
					this.RemoveChild(Solutions[i], true);
					Solutions[i] = null;
				}
				Solutions = null;
			}
			Colors = null;
		}
		
		public void Populate( int pCubes, int pScore) {
			if ( GameScene.currentLevel == 999 ) {
				PossibleSolutionsText.Text = "";
				this.Solutions = new SolutionIcon[1];
				this.Solutions[0] = new SolutionIcon() {
					CubeText = pCubes.ToString(),
					ScoreText = pScore.ToString(),
					Color = QColor.palette[0],
					Position = new Vector2(40.0f, QuitButton.Height + 60.0f)
				};
				this.AddChild(Solutions[0]);
			} else {
				var previousSolutions = DataStorage.puzzleSolutionsFound[GameScene.currentLevel];
				var numFound = previousSolutions.Count;
				bool okToAdd = true;
				foreach( var ps in previousSolutions ) { // ---- Check if solution was found previously
					if ( ps[0] == pCubes ) {
						if ( ps[1] == pScore ) {
							okToAdd = false;
							break;
						}
					}
				}
				if (okToAdd) {
					numFound++;
				}
				
				FoundSolutionsText.Text = DataStorage.puzzleSolutionsFound[GameScene.currentLevel].Count.ToString();
				TotalSolutionsText.Text = LevelManager.Instance.PossibleSolutions.ToString();
				
				this.Solutions = new SolutionIcon[LevelManager.Instance.PossibleSolutions];
				this.Colors = new Vector4[LevelManager.Instance.PossibleSolutions + 1];
				var completion = ((float)numFound / (float)LevelManager.Instance.PossibleSolutions);
//				PossibleSolutionsText.Text = "possible solutions (found " + numFound.ToString() + " of " + LevelManager.Instance.PossibleSolutions.ToString() + "):";
//				Percentage = completion;
				visibleSolutionIndex = 0;
				this.Solutions[0] = new SolutionIcon() {
					CubeText = pCubes.ToString(),
					ScoreText = pScore.ToString(),
					Color = QColor.palette[1],
					Position = new Vector2(40.0f, QuitButton.Height + 60.0f)
				};
				this.AddChild(Solutions[0]);
				int i = 1;
				foreach( int cube in LevelManager.Instance.goalDict.Keys ) {
					foreach ( int points in LevelManager.Instance.goalDict[cube] ) {
						if ( (  cube != pCubes || points != pScore ) && i < Solutions.Length ) { //----------------------- already handled the solution player just found
							Solutions[i] = new SolutionIcon() {
								CubeText = cube.ToString(),
								ScoreText = points.ToString(),
								Color = Vector4.Zero,
								Position = new Vector2(40.0f + 65.0f*(i%MAX_SOLUTIONS_ON_SCREEN), QuitButton.Height + 60.0f)
							};
							foreach( var ps in previousSolutions) {
								if ( ps[0] == cube && ps[1] == points ) {
//									Solutions[i].Color = QColor.palette[0];
									Solutions[i].Color = Sce.PlayStation.HighLevel.GameEngine2D.Base.Colors.White;
									break;
								}
							}
							if ( Solutions[i].Color == Vector4.Zero ) {
								Solutions[i].Color = Sce.PlayStation.HighLevel.GameEngine2D.Base.Colors.Grey50;
							}
							if ( i > MAX_SOLUTIONS_ON_SCREEN-1 ) {
								Solutions[i].Alpha = 0.0f;
							}
							this.AddChild(Solutions[i]);
							i++;
						}
					}
				}
			}
//			this.Text = "you clever thing.";
//			MessageText.Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f);
		}
		
		
		protected void SwapMessage( float dt ) {
			messageTimer += dt;
			if (messageTimer < 0.0f) {
				Solutions[visibleSolutionIndex].Alpha -= dt / 0.5f;
				for ( int i = visibleSolutionIndex + 1; i < visibleSolutionIndex + MAX_SOLUTIONS_ON_SCREEN; i++ ) {
					if ( i >= Solutions.Length ) break;
					Solutions[i].Alpha = Solutions[visibleSolutionIndex].Alpha;
				}
				if( Solutions[visibleSolutionIndex].Alpha <= 0.0f ) {
					Solutions[visibleSolutionIndex].Alpha = 0.0f;
					for ( int i = visibleSolutionIndex + 1; i < visibleSolutionIndex + MAX_SOLUTIONS_ON_SCREEN; i++ ) {
						if ( i >= Solutions.Length ) break;
						
						Solutions[i].Alpha = Solutions[visibleSolutionIndex].Alpha;
					}
					
					visibleSolutionIndex += MAX_SOLUTIONS_ON_SCREEN;
					if (visibleSolutionIndex >= Solutions.Length) {
						visibleSolutionIndex = 0;
					}
					messageTimer = 0.0f;
				}
			} else {
				if( Solutions[visibleSolutionIndex].Alpha < 1.0f){
					Solutions[visibleSolutionIndex].Alpha += dt / 0.5f;
					for ( int i = visibleSolutionIndex + 1; i < visibleSolutionIndex + MAX_SOLUTIONS_ON_SCREEN; i++ ) {
						if ( i >= Solutions.Length ) break;
						
						Solutions[i].Alpha = Solutions[visibleSolutionIndex].Color.W;
					}
				}
				if( messageTimer > MESSAGE_SWAP_DELAY) {
					Solutions[visibleSolutionIndex].Alpha = 1.0f;
					for ( int i = visibleSolutionIndex + 1; i < visibleSolutionIndex + MAX_SOLUTIONS_ON_SCREEN; i++ ) {
						if ( i >= Solutions.Length ) break;
						
						Solutions[i].Alpha = Solutions[visibleSolutionIndex].Alpha;
					}
					messageTimer = -10.0f;
				}
			}
		}
		
		
//		protected void CenterText() {
//			var textWidth = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold").GetTextWidth(Text);
//			MessageText.Position = new Vector2(0.5f * (Width - textWidth), MessageText.Position.Y);
//		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------------------------
#if DEBUG
        ~NextLevelPanel() {
			Console.WriteLine("MessagePanel deleted.");
        }
#endif
	}
}

