using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    public partial class Instructions7Panel : Panel
    {
        public Instructions7Panel()
        {
            InitializeWidget();
			
			Button_1.TouchEventReceived += HandleNewGameButtonTouchEventReceived;
			Button_2.TouchEventReceived += (sender, e) => { Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene( new MenuSystemScene("Menu") ); };
        }
		
		void HandleNewGameButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			UISystem.SetScene( new LoadingScene( 0 ) );
//			Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene( new GameScene(0) );
        }
    }
}
