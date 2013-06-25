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
		private float _holdtimer;
		
        public MenuScene()
        {
//			_acceptTouch = false;
			Touch.GetData(0).Clear();
            InitializeWidget();
			
			NewGameButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			LevelSelectButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			OptionsButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			CreditsButton.TextFont = FontManager.Instance.Get("Bariol", 25);
			
			NewGameButton.ButtonAction += HandleNewGameButtonTouchEventReceived;
			InfiniteModeButton.ButtonAction += HandleInfiniteModeButtonTouchEventReceived;
			TimedModeButton.ButtonAction += HandleTimedModeButtonTouchEventReceived;
#if METRICS
			PrintAnalyticsButton.ButtonAction += (sender, e) => {
				DataStorage.PrintMetrics();
			};
			ClearAnalyticsButton.ButtonAction += (sender, e) => {
				DataStorage.ClearMetrics();
			};
#endif
			
			LevelSelectButton.ButtonAction += (sender, e) => { 
				this.RootWidget.Dispose();
				UISystem.SetScene( new LevelSelectScene() ); 
			};
			OptionsButton.ButtonAction += (sender, e) => {
				this.RootWidget.Dispose();
				UISystem.SetScene( new InstructionsScene() ); 
			};
			CreditsButton.ButtonAction += (sender, e) => { 
				this.RootWidget.Dispose();
				UISystem.SetScene( new CreditsScene() ); 
			};
			
			InfiniteModeButton.Visible = false;
			TimedModeButton.Visible = false;
			PrintAnalyticsButton.Visible = false;
			ClearAnalyticsButton.Visible = false;
        }

        void HandleTimedModeButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
        	this.RootWidget.Dispose();
			UISystem.SetScene( new LoadingScene(999, true) );
        }

        void HandleInfiniteModeButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
        	this.RootWidget.Dispose();
			UISystem.SetScene( new LoadingScene(999, false) );
        }

        void HandleNewGameButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			this.RootWidget.Dispose();
			UISystem.SetScene( new LoadingScene(0, false ));
        }
		
#if METRICS
		protected override void OnUpdate (float elapsedTime)
		{
			if ( ( ( GamePad.GetData(0).Buttons & GamePadButtons.L) != 0 ) && 
			     ( ( GamePad.GetData(0).Buttons & GamePadButtons.R ) != 0 ) ) {
				_holdtimer += elapsedTime;
				if (_holdtimer > 2000.0f) {
					_holdtimer = 0.0f;
					PrintAnalyticsButton.Visible = !PrintAnalyticsButton.Visible;
					ClearAnalyticsButton.Visible = !ClearAnalyticsButton.Visible;
				}
			} else {
				_holdtimer = 0.0f;
			}
			base.OnUpdate (elapsedTime);
		}
#endif
		
		// DESTRUCTOR -------------------------------------------------------------------------
#if DEBUG
		~MenuScene() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
    }
}
