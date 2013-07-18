using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI.Deprecated
{
    public partial class CreditsScene : Scene
    {
        public CreditsScene()
        {
            InitializeWidget();
			
			CreditsTitleText.Font = FontManager.Instance.Get("Bariol", 72);
			BackButton.TextFont = FontManager.Instance.Get ("Bariol",25);
			
			BackButton.TouchEventReceived += (sender, e) => { 
				this.RootWidget.Dispose();
				UISystem.SetScene( new MenuScene() ); 
			};
        }
		
#if DEBUG
		~CreditsScene() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
    }
}
