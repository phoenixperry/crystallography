using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI
{
    public partial class LevelSelectScene : Sce.PlayStation.HighLevel.UI.Scene
    {
		private int selectedLevel;
		
        public LevelSelectScene()
        {
			selectedLevel = 0;
			
            InitializeWidget();
			cubeNumber.Font = FontManager.Instance.Get("Bariol", 32); 				
			howToSelect.Font = FontManager.Instance.Get("Bariol", 32); 			
			ScoreText.Font = FontManager.Instance.Get("Bariol", 32); 			
			LevelNumberText.Font = FontManager.Instance.Get("Bariol", 32);
			
			LevelSelectTitleText.Font = FontManager.Instance.Get("Bariol", 60);
			
			LevelTimeText.Font = FontManager.Instance.Get ("Bariol", 25);
			GradeText.Font = FontManager.Instance.Get ("Bariol", 72);
			
			StartButton.TextFont = FontManager.Instance.Get ("Bariol", 25);
			BackButton.TextFont = FontManager.Instance.Get ("Bariol", 25);
			
			StartButton.TouchEventReceived += HandleStartButtonTouchEventReceived;
			BackButton.TouchEventReceived += (sender, e) => { UISystem.SetScene( new MenuScene() ); };
			LevelSelectItem.LevelSelectionDetected += (sender, e) => { 
				selectedLevel = e.LevelID;
				LevelNumberText.Text = e.LevelID.ToString();
			};
        }

        void HandleStartButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			Console.WriteLine( selectedLevel );
			UISystem.SetScene( new LoadingScene( selectedLevel ) );
//			Director.Instance.ReplaceScene( new GameScene( selectedLevel ) );
        }
    }
}
