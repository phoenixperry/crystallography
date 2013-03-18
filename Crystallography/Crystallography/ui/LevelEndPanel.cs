using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI
{
    public partial class LevelEndPanel : Panel
    {
        public LevelEndPanel( int pScore, float pTime )
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
			NextLevelButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			LevelSelectButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			QuitButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			
			YourScoreText.Text = pScore.ToString();
			BestScoreText.Text = pScore.ToString();
			
			int minutes = (int)Math.Floor( pTime / 60.0f );
			float seconds = pTime - (60 * minutes);
			
			YourTimeText.Text = minutes + ":" + seconds.ToString("00.0");
			BestTimeText.Text = minutes + ":" + seconds.ToString("00.0");
			
//			NextLevelButton.TouchEventReceived += HandleNextLevelButtonTouchEventReceived;
//			LevelSelectButton.TouchEventReceived += HandleLevelSelectButtonTouchEventReceived;
//			QuitButton.TouchEventReceived += HandleQuitButtonTouchEventReceived;
			
			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
        }

       
		
		// EVENT HANDLERS ------------------------------------------------------------------------
		
		 void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e)
        {
        	if ( e.touchPosition.X > NextLevelButton.X && e.touchPosition.X < NextLevelButton.X + NextLevelButton.Width ) {
				int height = Director.Instance.GL.Context.GetViewport().Height;
				if ( height - e.touchPosition.Y > NextLevelButton.Y && height - e.touchPosition.Y < NextLevelButton.Y + NextLevelButton.Height ) {
					NextLevelButton.IconImage = NextLevelButton.CustomImage.BackgroundNormalImage;
					(Director.Instance.CurrentScene as GameScene).goToNextLevel();
					InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
					InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
					this.Dispose();
					return;
				}
			}
			if ( e.touchPosition.X > LevelSelectButton.X && e.touchPosition.X < LevelSelectButton.X + LevelSelectButton.Width ) {
				int height = Director.Instance.GL.Context.GetViewport().Height;
				if ( height - e.touchPosition.Y > LevelSelectButton.Y && height - e.touchPosition.Y < LevelSelectButton.Y + LevelSelectButton.Height ) {
					LevelSelectButton.IconImage = LevelSelectButton.CustomImage.BackgroundNormalImage;
					GameScene.QuitToLevelSelect();
					InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
					InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
					this.Dispose();
					return;
				}
			}
			if ( e.touchPosition.X > QuitButton.X && e.touchPosition.X < QuitButton.X + QuitButton.Width ) {
				int height = Director.Instance.GL.Context.GetViewport().Height;
				if ( height - e.touchPosition.Y > QuitButton.Y && height - e.touchPosition.Y < QuitButton.Y + QuitButton.Height ) {
					QuitButton.IconImage = QuitButton.CustomImage.BackgroundNormalImage;
					GameScene.QuitToTitle();
					InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
					InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
					this.Dispose();
					return;
				}
			}
        }

        void HandleInputManagerInstanceTouchJustDownDetected (object sender, BaseTouchEventArgs e)
        {
        	if ( e.touchPosition.X > NextLevelButton.X && e.touchPosition.X < NextLevelButton.X + NextLevelButton.Width ) {
				int height = Director.Instance.GL.Context.GetViewport().Height;
				if ( height - e.touchPosition.Y > NextLevelButton.Y && height - e.touchPosition.Y < NextLevelButton.Y + NextLevelButton.Height ) {
					NextLevelButton.IconImage = NextLevelButton.CustomImage.BackgroundPressedImage;
				}
			}
			if ( e.touchPosition.X > LevelSelectButton.X && e.touchPosition.X < LevelSelectButton.X + LevelSelectButton.Width ) {
				int height = Director.Instance.GL.Context.GetViewport().Height;
				if ( height - e.touchPosition.Y > LevelSelectButton.Y && height - e.touchPosition.Y < LevelSelectButton.Y + LevelSelectButton.Height ) {
					LevelSelectButton.IconImage = LevelSelectButton.CustomImage.BackgroundPressedImage;
				}
			}
			if ( e.touchPosition.X > QuitButton.X && e.touchPosition.X < QuitButton.X + QuitButton.Width ) {
				int height = Director.Instance.GL.Context.GetViewport().Height;
				if ( height - e.touchPosition.Y > QuitButton.Y && height - e.touchPosition.Y < QuitButton.Y + QuitButton.Height ) {
					QuitButton.IconImage = QuitButton.CustomImage.BackgroundPressedImage;
				}
			}
        }
		
        void HandleNextLevelButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			TouchEvent v = e.TouchEvents[0];
			if (v.Type == TouchEventType.Up) {
	        	
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
    }
}
