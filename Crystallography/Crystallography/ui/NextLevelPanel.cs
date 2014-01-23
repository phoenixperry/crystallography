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
		
		Label MessageText;
//		Label PercentageText;
		Label PossibleSolutionsText;
		
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
		
		public string Text {
			get {
				return MessageText.Text;
			}
			set {
				MessageText.Text = value;
				CenterText();
			}
		}
		
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
//			Height = 176.0f;
			Width = 448.0f;
			
			Background = Support.UnicolorSprite("Grey", 40, 40, 40, 200);
			Background.Scale = new Vector2(28.0f, 15.0f);
			this.AddChild(Background);
			
			PossibleSolutionsText = new Label() {
				Text = "all possible solutions:",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold" ) ),
				Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f),
				Position = new Vector2(40.0f, 180.0f)
			};
			this.AddChild( PossibleSolutionsText );
			
			MessageText = new Label() {
//				Text = "Lorem ipsum dolor sit amet",
				Text = "you clever thing.",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold" ) ),
				Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f),
//				Position = new Vector2(40.0f, 95.0f)
			};
			this.AddChild( MessageText );
			
			
			QuitButton = new BetterButton(117.0f, 53.0f) {
				Text = "quit",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				Position = new Vector2(0.0f, 0.0f),
				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			this.AddChild(QuitButton);
			
//			QuitButton = new ButtonEntity("", null, null, Support.TiledSpriteFromFile("Application/assets/images/quit_game.png", 1, 3).TextureInfo, new Vector2i(0,0) );
//			QuitButton.setPosition(58.5f,26.5f);
//			QuitButton.Visible = true;
//			this.AddChild(QuitButton.getNode());
			
			
//			PercentageText = new Label() {
//				Text = "100 %",
//				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold" ) ),
//				Position = new Vector2( 320.0f, QuitButton.Height + 90.0f )
//			};
//			this.AddChild( PercentageText );
			
			
			LevelSelectButton = new BetterButton(176.0f, 53.0f) {
				Text = "level select",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				Position = new Vector2(QuitButton.Width + 4.0f , 0.0f),
				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			this.AddChild(LevelSelectButton);
			
//			LevelSelectButton = new ButtonEntity("", null, null, Support.TiledSpriteFromFile("Application/assets/images/levelSelectBtn.png", 1, 3).TextureInfo, new Vector2i(0,0) );
//			LevelSelectButton.setPosition(QuitButton.Width + 2.0f + 88.0f,26.5f);
//			LevelSelectButton.Visible = true;
//			this.AddChild(LevelSelectButton.getNode());
			
			
			NextLevelButton = new BetterButton(148.0f, 53.0f) {
				Text = "next level",
				TextFont = FontManager.Instance.GetInGame("Bariol", 25),
				IconOnLeft = false,
				Icon = Support.SpriteFromFile("/Application/assets/images/UI/nextLevelIcon.png"),
				IconAndTextOffset = new Vector2(2.0f, 0.0f),
				TextOffset = new Vector2(-2.0f, 0.0f),
				Position = new Vector2(QuitButton.Width + LevelSelectButton.Width + 8.0f, 0.0f),
				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			this.AddChild(NextLevelButton);
			
			
//			NextLevelButton = new ButtonEntity("", null, null, Support.TiledSpriteFromFile("Application/assets/images/nextLevel.png", 1, 3).TextureInfo, new Vector2i(0,0) );
//			NextLevelButton.setPosition(LevelSelectButton.Width + QuitButton.Width + 4.0f + 74.0f, 26.5f);
//			NextLevelButton.Visible = true;
//			this.AddChild(NextLevelButton.getNode());
			
			var charHeight = MessageText.FontMap.CharPixelHeight;
			Height = (charHeight * 5.0f) + QuitButton.Height;
			MessageText.Position = new Vector2(40.0f, QuitButton.Height + 20 );
			CenterText();
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
			NextLevelButton.ButtonUpAction += HandleNextLevelButtonButtonUpAction;
			LevelSelectButton.ButtonUpAction += HandleLevelSelectButtonButtonUpAction;
			QuitButton.ButtonUpAction += HandleQuitButtonButtonUpAction;
		}
		
		void HandleOnSlideOutComplete (object sender, EventArgs e)
		{
			if (Solutions == null) return;
			for( int i=0; i < Solutions.Length; i++ ) {
				this.RemoveChild(Solutions[i], true);
				Solutions[i] = null;
			}
			Solutions = null;
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
			base.OnExit ();
		}
		
		// METHODS ----------------------------------------------------------------------------------------------------------------------------------------------
		
		public void Populate( int pCubes, int pScore) {
			if ( GameScene.currentLevel == 999 ) {
				PossibleSolutionsText.Text = "";
				this.Solutions = new SolutionIcon[1];
				this.Solutions[0] = new SolutionIcon() {
					CubeText = pCubes.ToString(),
					ScoreText = pScore.ToString(),
					Color = QColor.palette[1],
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
				this.Solutions = new SolutionIcon[LevelManager.Instance.PossibleSolutions];
				this.Colors = new Vector4[LevelManager.Instance.PossibleSolutions + 1];
				var completion = ((float)numFound / (float)LevelManager.Instance.PossibleSolutions);
				PossibleSolutionsText.Text = "possible solutions (found " + numFound.ToString() + " of " + LevelManager.Instance.PossibleSolutions.ToString() + "):";
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
						if ( cube != pCubes || points != pScore ) { //----------------------- already handled the solution player just found
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
			this.Text = "you clever thing.";
			MessageText.Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f);
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
		
		
		protected void CenterText() {
			var textWidth = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold").GetTextWidth(Text);
			MessageText.Position = new Vector2(0.5f * (Width - textWidth), MessageText.Position.Y);
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------------------------
#if DEBUG
        ~NextLevelPanel() {
			Console.WriteLine("MessagePanel deleted.");
        }
#endif
	}
}

