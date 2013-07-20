using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

using Crystallography;

namespace Crystallography.UI.Deprecated
{
    public partial class LevelEndPanel : Panel
    {
		ButtonEntity NextLevelButton;
		ButtonEntity LevelSelectButton;
		ButtonEntity QuitButton;
		
        public LevelEndPanel()// int pScore, float pTime )
        {
            InitializeWidget();
			
		//	SequenceCompleteText.Font = FontManager.Instance.Get("Bariol", 72);
			//ScoreText.Font = FontManager.Instance.Get("Bariol", 48);
			//TimeText.Font = FontManager.Instance.Get("Bariol", 48);
			//YoursText.Font = FontManager.Instance.Get("Bariol", 48);
			//BestText.Font = FontManager.Instance.Get("Bariol", 48);
			YourScoreText.Font = FontManager.Instance.Get("Bariol", 48);
			BestScoreText.Font = FontManager.Instance.Get("Bariol", 48);
			YourTimeText.Font = FontManager.Instance.Get("Bariol", 48);
			BestTimeText.Font = FontManager.Instance.Get("Bariol", 48);
			
//			this.Visible = false;
//			NextLevelButton.TextFont = FontManager.Instance.Get("Bariol", 25);
//			LevelSelectButton.TextFont = FontManager.Instance.Get("Bariol", 25);
//			QuitButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			
//			YourScoreText.Text = pScore.ToString();
//			BestScoreText.Text = pScore.ToString();
//			
//			int minutes = (int)Math.Floor( pTime / 60.0f );
//			float seconds = pTime - (60 * minutes);
//			
//			YourTimeText.Text = minutes + ":" + seconds.ToString("00.0");
//			BestTimeText.Text = minutes + ":" + seconds.ToString("00.0");
			
//			NextLevelButton.TouchEventReceived += HandleNextLevelButtonTouchEventReceived;

//			LevelSelectButton.TouchEventReceived += HandleLevelSelectButtonTouchEventReceived;
//			QuitButton.TouchEventReceived += HandleQuitButtonTouchEventReceived;
			
//			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
//			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
//			InputManager.Instance.CircleJustUpDetected += HandleInputManagerInstanceCircleJustUpDetected;
//			InputManager.Instance.CrossJustUpDetected += HandleInputManagerInstanceCrossJustUpDetected;
			
//			Disable();
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
        }

        void HandleInputManagerInstanceCrossJustUpDetected (object sender, EventArgs e)
        {
        	QuitButtonReleased();
        }

        void HandleInputManagerInstanceCircleJustUpDetected (object sender, EventArgs e)
        {
        	NextLevelButtonReleased();
        }

       
		
		// EVENT HANDLERS ------------------------------------------------------------------------
		
//		 void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e)
//        {
//			if(this.Visible == false) return;
//			
//        	if ( e.touchPosition.X > NextLevelButton.X && e.touchPosition.X < NextLevelButton.X + NextLevelButton.Width ) {
//				int height = Director.Instance.GL.Context.GetViewport().Height;
//				if ( height - e.touchPosition.Y > NextLevelButton.Y && height - e.touchPosition.Y < NextLevelButton.Y + NextLevelButton.Height ) {
//					NextLevelButtonReleased();
////					NextLevelButton.IconImage = NextLevelButton.CustomImage.BackgroundNormalImage;
////					(Director.Instance.CurrentScene as GameScene).goToNextLevel();
////					InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
////					InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
////					this.Dispose();
//				}
//			}
//			if ( e.touchPosition.X > LevelSelectButton.X && e.touchPosition.X < LevelSelectButton.X + LevelSelectButton.Width ) {
//				int height = Director.Instance.GL.Context.GetViewport().Height;
//				if ( height - e.touchPosition.Y > LevelSelectButton.Y && height - e.touchPosition.Y < LevelSelectButton.Y + LevelSelectButton.Height ) {
//					LevelSelectButton.IconImage = LevelSelectButton.CustomImage.BackgroundNormalImage;
//					GameScene.QuitToLevelSelect();
//					InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
//					InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
//					InputManager.Instance.CircleJustUpDetected -= HandleInputManagerInstanceCircleJustUpDetected;
//					InputManager.Instance.CrossJustUpDetected -= HandleInputManagerInstanceCrossJustUpDetected;
//					this.Dispose();
//					return;
//				}
//			}
//			if ( e.touchPosition.X > QuitButton.X && e.touchPosition.X < QuitButton.X + QuitButton.Width ) {
//				int height = Director.Instance.GL.Context.GetViewport().Height;
//				if ( height - e.touchPosition.Y > QuitButton.Y && height - e.touchPosition.Y < QuitButton.Y + QuitButton.Height ) {
//					QuitButtonReleased();
////					QuitButton.IconImage = QuitButton.CustomImage.BackgroundNormalImage;
////					GameScene.QuitToTitle();
////					InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
////					InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
////					this.Dispose();
//				}
//			}
//        }

//        void HandleInputManagerInstanceTouchJustDownDetected (object sender, BaseTouchEventArgs e)
//        {
//			if (this.Visible == false) return;
//			
//        	if ( e.touchPosition.X > NextLevelButton.X && e.touchPosition.X < NextLevelButton.X + NextLevelButton.Width ) {
//				int height = Director.Instance.GL.Context.GetViewport().Height;
//				if ( height - e.touchPosition.Y > NextLevelButton.Y && height - e.touchPosition.Y < NextLevelButton.Y + NextLevelButton.Height ) {
//					NextLevelButton.IconImage = NextLevelButton.CustomImage.BackgroundPressedImage;
//					return;
//				}
//			}
//			if ( e.touchPosition.X > LevelSelectButton.X && e.touchPosition.X < LevelSelectButton.X + LevelSelectButton.Width ) {
//				int height = Director.Instance.GL.Context.GetViewport().Height;
//				if ( height - e.touchPosition.Y > LevelSelectButton.Y && height - e.touchPosition.Y < LevelSelectButton.Y + LevelSelectButton.Height ) {
//					LevelSelectButton.IconImage = LevelSelectButton.CustomImage.BackgroundPressedImage;
//					return;
//				}
//			}
//			if ( e.touchPosition.X > QuitButton.X && e.touchPosition.X < QuitButton.X + QuitButton.Width ) {
//				int height = Director.Instance.GL.Context.GetViewport().Height;
//				if ( height - e.touchPosition.Y > QuitButton.Y && height - e.touchPosition.Y < QuitButton.Y + QuitButton.Height ) {
//					QuitButton.IconImage = QuitButton.CustomImage.BackgroundPressedImage;
//					return;
//				}
//			}
//        }
		
        void HandleNextLevelButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			TouchEvent v = e.TouchEvents[0];
			if (v.Type == TouchEventType.Up) {
	        	NextLevelButtonReleased();
			}
        }
		
		void HandleLevelSelectButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			TouchEvent v = e.TouchEvents[0];
			if (v.Type == TouchEventType.Up) {
    	    	GameScene.QuitToLevelSelect();
				this.Dispose();
			}
        }
		
		void HandleQuitButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			TouchEvent v = e.TouchEvents[0];
			if (v.Type == TouchEventType.Up) {
	        	GameScene.QuitToTitle();
				this.Dispose();
			}
        }
		
		// METHODS ------------------------------------------------------------------------------------
		
		public void Disable() {
//			NextLevelButton.Width = 0;
//			LevelSelectButton.Width = 0;
//			QuitButton.Width = 0;
//			NextLevelButton.Enabled = false;
//			LevelSelectButton.Enabled = false;
//			QuitButton.Enabled = false;
		}
		
		public void Enable() {
//			NextLevelButton.Enabled = true;
//			LevelSelectButton.Enabled = true;
//			QuitButton.Enabled = true;
		}
		
		public void Hide(){
			this.Visible = false;
			if (NextLevelButton == null) return;
			NextLevelButton.Visible = false;
			LevelSelectButton.Visible = false;
			QuitButton.Visible = false;
//			Sequence sequence = new Sequence();
//			sequence.Add( new DelayTime( 0.1f ) );
//			sequence.Add( new CallFunc( () => Disable() ) );
//			(Director.Instance.CurrentScene).RunAction( sequence );
		}
		
		public void LevelSelectButtonReleased() {
			GameScene.QuitToLevelSelect();
			InputManager.Instance.CircleJustUpDetected -= HandleInputManagerInstanceCircleJustUpDetected;
			InputManager.Instance.CrossJustUpDetected -= HandleInputManagerInstanceCrossJustUpDetected;
			Hide();
//			this.Dispose();
		}
		
		public void NextLevelButtonReleased() {
			Console.WriteLine("-----NextLevelButtonReleased--------");
//			NextLevelButton.IconImage = NextLevelButton.CustomImage.BackgroundNormalImage;
//			(Director.Instance.CurrentScene as GameScene).TempDisableInput();
			(Director.Instance.CurrentScene as GameScene).GoToNextLevel();
//			InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
//			InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
//			InputManager.Instance.CircleJustUpDetected -= HandleInputManagerInstanceCircleJustUpDetected;
//			InputManager.Instance.CrossJustUpDetected -= HandleInputManagerInstanceCrossJustUpDetected;
			
			Hide();
//			this.Dispose();
		}
		
		public void QuitButtonReleased() {
//			QuitButton.IconImage = QuitButton.CustomImage.BackgroundNormalImage;
			GameScene.QuitToTitle();
//			InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
//			InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
			InputManager.Instance.CircleJustUpDetected -= HandleInputManagerInstanceCircleJustUpDetected;
			InputManager.Instance.CrossJustUpDetected -= HandleInputManagerInstanceCrossJustUpDetected;
			Hide();
//			this.Dispose();
		}
		
		public void Show( int pScore, float pTime ) {
			NextLevelButton = new ButtonEntity("Next Level", Director.Instance.CurrentScene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/button_85x56.png", 1, 3).TextureInfo, new Vector2i(0,0));
			NextLevelButton.setPosition(491, 35);
//			NextLevelButton.addToScene(2);
			NextLevelButton.ButtonUpAction += (sender, e) => {
				NextLevelButtonReleased();
			};
			NextLevelButton.addToScene(2);
			LevelSelectButton = new ButtonEntity("LevelSelect", Director.Instance.CurrentScene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/button_85x56.png", 1, 3).TextureInfo, new Vector2i(0,0));
			LevelSelectButton.setPosition(260, 35);
			LevelSelectButton.ButtonUpAction += (sender, e) => {
				LevelSelectButtonReleased();
			};
			LevelSelectButton.addToScene(2);
			QuitButton = new ButtonEntity("Quit", Director.Instance.CurrentScene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/button_85x56.png", 1, 3).TextureInfo, new Vector2i(0,0));
			QuitButton.setPosition(107, 35);
			QuitButton.ButtonUpAction += (sender, e) => {
				QuitButtonReleased();
			};
			QuitButton.addToScene(2);
			this.Visible = true;
			YourScoreText.Text = pScore.ToString();
			BestScoreText.Text = pScore.ToString();
			
			int minutes = (int)System.Math.Floor( pTime / 60.0f );
			float seconds = pTime - (60 * minutes);
			YourTimeText.Text = minutes + ":" + seconds.ToString("00.0");
			BestTimeText.Text = minutes + ":" + seconds.ToString("00.0");
//			AppMain.UI_INPUT_ENABLED = true;
//			AppMain.GAMEPLAY_INPUT_ENABLED = false;
			
//			Sequence sequence = new Sequence();
//			sequence.Add( new DelayTime( 0.1f ) );
//			sequence.Add( new CallFunc( () => Enable() ) );
//			(Director.Instance.CurrentScene).RunAction( sequence );
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~LevelEndPanel() {
			Console.WriteLine("LevelEndPanel deleted.");
        }
#endif
		
    }
}
