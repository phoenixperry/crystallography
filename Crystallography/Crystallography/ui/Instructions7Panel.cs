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
			Button_2.TouchEventReceived += (sender, e) => { 
				UISystem.CurrentScene.RootWidget.Dispose();
				Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene( new MenuSystemScene("Menu") ); 
				
			};
        }
		
		void HandleNewGameButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			UISystem.CurrentScene.RootWidget.Dispose();
			UISystem.SetScene( new LoadingScene( 0 ) );
        }
    }
}
