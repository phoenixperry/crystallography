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
        }
		
		void HandleNewGameButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene( new GameScene(0) );
        }
    }
}
