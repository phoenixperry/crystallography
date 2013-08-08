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
		
		ButtonEntity QuitButton;
		ButtonEntity LevelSelectButton;
		ButtonEntity NextLevelButton;
		
		public string[] Messages;
		public Vector4[] Colors;
		protected int messageIndex;
		protected float messageTimer;
		
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
		public event EventHandler LevelSelectDetected;
		public event EventHandler NextLevelDetected;
		
		
		// CONSTRUCTOR --------------------------------------------------------------------------------------------------------------------------------------
		
		public NextLevelPanel () {
			DismissDelay = 0.0f;
//			Height = 176.0f;
			Width = 448.0f;
			
			Background = Support.UnicolorSprite("Grey", 40, 40, 40, 200);
			Background.Scale = new Vector2(28.0f, 11.0f);
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
			
			
			
			LevelSelectButton = new ButtonEntity("", null, null, Support.TiledSpriteFromFile("Application/assets/images/levelSelectBtn.png", 1, 3).TextureInfo, new Vector2i(0,0) );
			LevelSelectButton.setPosition(QuitButton.Width + 2.0f + 88.0f,26.5f);
			LevelSelectButton.Visible = true;
			this.AddChild(LevelSelectButton.getNode());
			
			
			
			NextLevelButton = new ButtonEntity("", null, null, Support.TiledSpriteFromFile("Application/assets/images/nextLevel.png", 1, 3).TextureInfo, new Vector2i(0,0) );
			NextLevelButton.setPosition(LevelSelectButton.Width + QuitButton.Width + 4.0f + 74.0f, 26.5f);
			NextLevelButton.Visible = true;
			this.AddChild(NextLevelButton.getNode());
			
			var charHeight = MessageText.FontMap.CharPixelHeight;
			Height = (charHeight * 3.0f) + QuitButton.Height;
			MessageText.Position = new Vector2(40.0f, QuitButton.Height + charHeight );
			CenterText();
		}
		
		// EVENT HANDLERS ----------------------------------------------------------------------------------------------------------------------------------------
		
		void HandleOnSlideInComplete (object sender, EventArgs e)
		{
			messageTimer = -10.0f;
			messageIndex = 0;
			this.Schedule(SwapMessage, 1);
		}
		
		void HandleOnSlideInStart (object sender, EventArgs e)
		{
			NextLevelButton.ButtonUpAction += HandleNextLevelButtonButtonUpAction;
			LevelSelectButton.ButtonUpAction += HandleLevelSelectButtonButtonUpAction;
			QuitButton.ButtonUpAction += HandleQuitButtonButtonUpAction;
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
			var score = previousSolutions.Count;
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
				score++;
			}
			this.Messages = new string[LevelManager.Instance.PossibleSolutions + 1];
			this.Colors = new Vector4[LevelManager.Instance.PossibleSolutions + 1];
			var completion = ((float)score / (float)LevelManager.Instance.PossibleSolutions);
			this.Messages[0] = completion.ToString("P0");
			this.Colors[0] = Sce.PlayStation.HighLevel.GameEngine2D.Base.Colors.Yellow;
			this.Messages[1] = pCubes.ToString();
			if (pCubes == 1 ) {
				this.Messages[1] += " cube, ";
			} else {
				this.Messages[1] += " cubes, ";
			}  
			this.Messages[1] += pScore.ToString();
			if (pScore == 1) {
				this.Messages[1] += " point";
			} else {
				this.Messages[1] += " points";
			}
			this.Colors[1] = Sce.PlayStation.HighLevel.GameEngine2D.Base.Colors.Yellow;
			int i = 2;
			foreach( int cube in LevelManager.Instance.goalDict.Keys ) {
				foreach ( int points in LevelManager.Instance.goalDict[cube] ) {
					if ( cube != pCubes || points != pScore ) {
						foreach( var ps in previousSolutions) {
							if ( ps[0] == cube && ps[1] == points ) {
								this.Colors[i] = Sce.PlayStation.HighLevel.GameEngine2D.Base.Colors.White;
								break;
							}
						}
						if ( this.Colors[i] == Vector4.Zero ) {
							this.Colors[i] = Sce.PlayStation.HighLevel.GameEngine2D.Base.Colors.Grey50;
						}
						this.Messages[i] = cube.ToString();
						if (cube == 1 ) {
							this.Messages[i] += " cube, ";
						} else {
							this.Messages[i] += " cubes, ";
						}  
						this.Messages[i] += points.ToString();
						if (points == 1) {
							this.Messages[i] += " point";
						} else {
							this.Messages[i] += " points";
						}
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
				MessageText.Color.W -= dt / 0.5f;
				if (MessageText.Color.W <= 0.0f) {
					MessageText.Color.W = 0.0f;
					if( messageIndex == Messages.Length ) {
						messageIndex = 0;
					}
					MessageText.Color = this.Colors[messageIndex].Xyz0;
					Text = Messages[messageIndex++];
					messageTimer = 0.0f;
				}
			} else {
				if( MessageText.Color.W < 1.0f ) {
					MessageText.Color.W += dt / 0.5f;
				}
				if ( messageTimer > 1.5f) {
					MessageText.Color.W = 1.0f;
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

