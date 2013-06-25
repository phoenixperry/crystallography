using System;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D;
//using Sce.PlayStation.HighLevel.UI;

namespace Crystallography
{
	public class MenuSystemScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{
		Layer Screen;
		Layer OldScreen;
		
		public MenuSystemScene ( string pDestinationScreen ) {
			SetScreen( pDestinationScreen );
			this.Camera.SetViewFromViewport();
			Scheduler.Instance.ScheduleUpdateForTarget(this, 0, false );
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// OVERRIDES -----------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			Support.MusicSystem.Instance.Play("intromusic.mp3");
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			Support.MusicSystem.Instance.StopAll();
			System.GC.Collect();
			AppMain.UI_INPUT_ENABLED = false;
		}
		
		public override void Update (float dt)
		{
			InputManager.Instance.Update(dt);
//			UISystem.Update( Touch.GetData(0) );
			base.Update (dt);
		}
		
//		public override void Draw ()
//		{
//			base.Draw ();
//			UISystem.Render();
//		}
		
		// METHODS ---------------------------------------------------------------------------------
		
		public void SetScreen( string pDestinationScreen) {
			OldScreen = Screen;
			switch (pDestinationScreen) {
			case ("Splash"):
				Screen = new SplashScreen(this);
				break;
			case ("Title"):
				Screen = new TitleScreen(this);
//				UISystem.SetScene( new Crystallography.UI.SplashScene() );
				break;
			case ("Menu"):
				Screen = new MainMenuScreen(this);
//				UISystem.SetScene ( new Crystallography.UI.MenuScene() );
				break;
			case ("LevelSelect"):
//				UISystem.SetScene ( new Crystallography.UI.LevelSelectScene() );
				break;
			case ("Credits"):
				Screen = new CreditsScreen(this);
				break;
			case ("Instructions"):
				break;
			default:
//				UISystem.SetScene ( new Crystallography.UI.SplashScene() );
				break;
			}
			this.AddChild(Screen);
			this.RemoveChild(OldScreen, true);
			OldScreen = null;
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
#if DEBUG
		~MenuSystemScene() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

