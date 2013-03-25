using System;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography
{
	public class MenuSystemScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{
		public MenuSystemScene ( string pDestinationScene ) {
			
			switch (pDestinationScene) {
			case ("Splash"):
				UISystem.SetScene( new Crystallography.UI.SplashScene() );
				break;
			case ("Menu"):
				UISystem.SetScene ( new Crystallography.UI.MenuScene() );
				break;
			case ("LevelSelect"):
				UISystem.SetScene ( new Crystallography.UI.LevelSelectScene() );
				break;
			default:
				UISystem.SetScene ( new Crystallography.UI.SplashScene() );
				break;
			}	
			Scheduler.Instance.ScheduleUpdateForTarget(this, 0, false );
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
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
		}
		
		public override void Update (float dt)
		{
			UISystem.Update( Touch.GetData(0) );
			base.Update (dt);
		}
		
		public override void Draw ()
		{
			base.Draw ();
			UISystem.Render();
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------
#if DEBUG
		~MenuSystemScene() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

