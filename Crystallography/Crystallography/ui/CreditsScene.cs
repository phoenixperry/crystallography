using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    public partial class CreditsScene : Scene
    {
        public CreditsScene()
        {
            InitializeWidget();
			
			CreditsTitleText.Font = FontManager.Instance.Get("Bariol", 72);
			BackButton.TextFont = FontManager.Instance.Get ("Bariol",25);
			
			BackButton.TouchEventReceived += (sender, e) => { UISystem.SetScene( new MenuScene() ); };
        }
    }
}
