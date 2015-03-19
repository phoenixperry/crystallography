using System;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D;
//using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
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
#if DEBUG
			Console.WriteLine("########### ENTER MenuSystemScene ###############");
#endif
			base.OnEnter ();
			Support.MusicSystem.Instance.Play("europa.mp3");
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			Support.MusicSystem.Instance.StopAll();
			System.GC.Collect();
			AppMain.UI_INPUT_ENABLED = false;
			
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/instructions1.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/instructions2.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/instructions3.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/instructions4.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/instructions5.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/instructions8.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/instructions7.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/icons/tie.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/icons/phones.png");
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/icons/glasses.png");
#if DEBUG
			Console.WriteLine("########### EXIT MenuSystemScene ###############");
#endif
		}
		
		public override void Update (float dt)
		{
			base.Update (dt);
			InputManager.Instance.Update(dt);
		}
		
		// METHODS ---------------------------------------------------------------------------------
		
		public void SetScreen( string pDestinationScreen) {
			OldScreen = Screen;
			switch (pDestinationScreen) {
			case ("Splash"):
				Screen = new SplashScreen(this);
				break;
			case ("Title"):
				Screen = new TitleScreen(this);
				break;
			case ("Menu"):
				Screen = new MainMenuScreen(this);
				break;
			case ("Level Select"):
				Screen = new LevelSelectScreen(this);
				break;
			case ("Credits"):
				Screen = new CreditsScreen(this);
				break;
			case ("Instructions"):
				Screen = new InstructionsScreen(this);
				break;
			case("Options"):
				Screen = new OptionsMenuScreen(this);
				break;
			case("Infinite Mode"):
				Screen = new InfiniteModeScreen(this);
				break;
			default:
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

