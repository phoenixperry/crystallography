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
			
			SequenceCompleteText.Font = FontManager.Instance.Get("Bariol", 72);
			ScoreText.Font = FontManager.Instance.Get("Bariol", 48);
			TimeText.Font = FontManager.Instance.Get("Bariol", 48);
			YoursText.Font = FontManager.Instance.Get("Bariol", 48);
			BestText.Font = FontManager.Instance.Get("Bariol", 48);
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
			
			NextLevelButton.TouchEventReceived += HandleNextLevelButtonTouchEventReceived;
			LevelSelectButton.TouchEventReceived += HandleLevelSelectButtonTouchEventReceived;
			QuitButton.TouchEventReceived += HandleQuitButtonTouchEventReceived;
        }
		
		// EVENT HANDLERS ------------------------------------------------------------------------
		
        void HandleNextLevelButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			TouchEvent v = e.TouchEvents[0];
			if (v.Type == TouchEventType.Up) {
	        	(Director.Instance.CurrentScene as GameScene).goToNextLevel();
				this.Dispose();
			}
        }
		
		void HandleLevelSelectButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			TouchEvent v = e.TouchEvents[0];
			if (v.Type == TouchEventType.Up) {
    	    	GameScene.QuitToTitle();
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
