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
			
			float requiredPages = (float)(Math.Ceiling(((float)GameScene.TOTAL_LEVELS) / 12.0f));
			
			for (int i=0; i < requiredPages; i++ ) {
				PagePanel_1.AddPage(new LevelSelectPanel(i));
			}
			
//			PagePanel_1.AddPage(new LevelSelectPanel(0));
//            PagePanel_1.AddPage(new LevelSelectPanel(1));
//            PagePanel_1.AddPage(new LevelSelectPanel(2));
			
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
			BackButton.TouchEventReceived += (sender, e) => { 
				this.RootWidget.Dispose();
				UISystem.SetScene( new MenuScene() ); 
			};
			LevelSelectItem.LevelSelectionDetected += (sender, e) => { 
				selectedLevel = e.LevelID;
				LevelNumberText.Text = e.LevelID.ToString();
			};
        }

        void HandleStartButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			Console.WriteLine( selectedLevel );
			this.RootWidget.Dispose();
			UISystem.SetScene( new LoadingScene( selectedLevel, false ) );
//			Director.Instance.ReplaceScene( new GameScene( selectedLevel ) );
        }
		
		// DESTRUCTOR --------------------------------------------------------------------------------
#if DEBUG
		~LevelSelectScene() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
    }
}
