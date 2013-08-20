using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class NextLevelPanel : HudPanel
	{
		SpriteTile Background;
		
		Label MessageText;
		Label PercentageText;
		
		ButtonEntity QuitButton;
		ButtonEntity LevelSelectButton;
		ButtonEntity NextLevelButton;
		
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
		
		public float Percentage {
			get {
				return float.Parse(PercentageText.Text);
			}
			set {
				PercentageText.Text = value.ToString("P0");
			}
		}
		
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
			
			MessageText = new Label() {
//				Text = "Lorem ipsum dolor sit amet",
				Text = "you clever thing.",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold" ) ),
				Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f),
//				Position = new Vector2(40.0f, 95.0f)
			};
			this.AddChild( MessageText );
			
			
			QuitButton = new ButtonEntity("", null, null, Support.TiledSpriteFromFile("Application/assets/images/quit_game.png", 1, 3).TextureInfo, new Vector2i(0,0) );
			QuitButton.setPosition(58.5f,26.5f);
			QuitButton.Visible = true;
			this.AddChild(QuitButton.getNode());
			
			
			PercentageText = new Label() {
				Text = "100 %",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold" ) ),
				Position = new Vector2( 320.0f, QuitButton.Height + 90.0f )
			};
			this.AddChild( PercentageText );
			
			
			LevelSelectButton = new ButtonEntity("", null, null, Support.TiledSpriteFromFile("Application/assets/images/levelSelectBtn.png", 1, 3).TextureInfo, new Vector2i(0,0) );
			LevelSelectButton.setPosition(QuitButton.Width + 2.0f + 88.0f,26.5f);
			LevelSelectButton.Visible = true;
			this.AddChild(LevelSelectButton.getNode());
			
			
			
			NextLevelButton = new ButtonEntity("", null, null, Support.TiledSpriteFromFile("Application/assets/images/nextLevel.png", 1, 3).TextureInfo, new Vector2i(0,0) );
			NextLevelButton.setPosition(LevelSelectButton.Width + QuitButton.Width + 4.0f + 74.0f, 26.5f);
			NextLevelButton.Visible = true;
			this.AddChild(NextLevelButton.getNode());
			
			var charHeight = MessageText.FontMap.CharPixelHeight;
			Height = (charHeight * 5.0f) + QuitButton.Height;
			MessageText.Position = new Vector2(40.0f, QuitButton.Height + charHeight );
			CenterText();
		}
		
		// EVENT HANDLERS ----------------------------------------------------------------------------------------------------------------------------------------
		
		void HandleOnSlideInComplete (object sender, EventArgs e)
		{
			messageTimer = -10.0f;
			messageIndex = 0;
			if( Solutions.Length > 4 ) {
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
			Percentage = completion;
			visibleSolutionIndex = 0;
			this.Solutions[0] = new SolutionIcon() {//pCubes.ToString();
				CubeText = pCubes.ToString(),
				ScoreText = pScore.ToString(),
				Color = QColor.palette[1],
				Position = new Vector2(40.0f, QuitButton.Height + 80.0f)
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
							Position = new Vector2(40.0f + 65.0f*(i%4), QuitButton.Height + 80.0f)
						};
						foreach( var ps in previousSolutions) {
							if ( ps[0] == cube && ps[1] == points ) {
								Solutions[i].Color = QColor.palette[0];
								break;
							}
						}
						if ( Solutions[i].Color == Vector4.Zero ) {
							Solutions[i].Color = Sce.PlayStation.HighLevel.GameEngine2D.Base.Colors.Grey50;
						}
						if ( i > 3 ) {
							Solutions[i].Alpha = 0.0f;
						}
						this.AddChild(Solutions[i]);
						i++;
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
				for ( int i = visibleSolutionIndex + 1; i < visibleSolutionIndex + 4; i++ ) {
					if ( i >= Solutions.Length ) break;
					
					Solutions[i].Alpha = Solutions[visibleSolutionIndex].Alpha;
				}
				if( Solutions[visibleSolutionIndex].Alpha <= 0.0f ) {
					Solutions[visibleSolutionIndex].Alpha = 0.0f;
					for ( int i = visibleSolutionIndex + 1; i < visibleSolutionIndex + 4; i++ ) {
						if ( i >= Solutions.Length ) break;
						
						Solutions[i].Alpha = Solutions[visibleSolutionIndex].Alpha;
					}
					
					visibleSolutionIndex += 4;
					if (visibleSolutionIndex >= Solutions.Length) {
						visibleSolutionIndex = 0;
					}
					messageTimer = 0.0f;
				}
//				MessageText.Color.W -= dt / 0.5f;
//				if (MessageText.Color.W <= 0.0f) {
//					MessageText.Color.W = 0.0f;
//					if( messageIndex == Messages.Length ) {
//						messageIndex = 0;
//					}
//					MessageText.Color = this.Colors[messageIndex].Xyz0;
//					Text = Messages[messageIndex++];
//					messageTimer = 0.0f;
//				}
			} else {
				if( Solutions[visibleSolutionIndex].Alpha < 1.0f){
					Solutions[visibleSolutionIndex].Alpha += dt / 0.5f;
					for ( int i = visibleSolutionIndex + 1; i < visibleSolutionIndex + 4; i++ ) {
						if ( i >= Solutions.Length ) break;
						
						Solutions[i].Alpha = Solutions[visibleSolutionIndex].Color.W;
					}
				}
				if( messageTimer > 1.5f) {
					Solutions[visibleSolutionIndex].Alpha = 1.0f;
					for ( int i = visibleSolutionIndex + 1; i < visibleSolutionIndex + 4; i++ ) {
						if ( i >= Solutions.Length ) break;
						
						Solutions[i].Alpha = Solutions[visibleSolutionIndex].Alpha;
					}
					messageTimer = -10.0f;
				}
//				if( MessageText.Color.W < 1.0f ) {
//					MessageText.Color.W += dt / 0.5f;
//				}
//				if ( messageTimer > 1.5f) {
//					MessageText.Color.W = 1.0f;
//					messageTimer = -10.0f;
//				}
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

