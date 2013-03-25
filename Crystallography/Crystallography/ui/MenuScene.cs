using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI
{
    public partial class MenuScene : Sce.PlayStation.HighLevel.UI.Scene
    {
//		private bool _acceptTouch;
		
        public MenuScene()
        {
//			_acceptTouch = false;
			Touch.GetData(0).Clear();
            InitializeWidget();
			
			NewGameButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			LevelSelectButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			OptionsButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			CreditsButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			
			NewGameButton.TouchEventReceived += HandleNewGameButtonTouchEventReceived;
			LevelSelectButton.TouchEventReceived += (sender, e) => { 
				this.RootWidget.Dispose();
				UISystem.SetScene( new LevelSelectScene() ); 
			};
			OptionsButton.TouchEventReceived += (sender, e) => {
				this.RootWidget.Dispose();
				UISystem.SetScene( new InstructionsScene() ); 
			};
			CreditsButton.TouchEventReceived += (sender, e) => { 
				this.RootWidget.Dispose();
				UISystem.SetScene( new CreditsScene() ); 
			};
        }

        void HandleNewGameButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			this.RootWidget.Dispose();
			UISystem.SetScene( new LoadingScene(0) );
        }
		
//		protected override void OnUpdate (float elapsedTime)
//		{
//			base.OnUpdate (elapsedTime);
//		}
		
		// DESTRUCTOR -------------------------------------------------------------------------
#if DEBUG
		~MenuScene() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
    }
}
