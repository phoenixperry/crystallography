using System;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class SplashScreen : Layer
	{
		SpriteTile SplashImage;
		MenuSystemScene MenuSystem;
		
		public SplashScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			SplashImage = Support.SpriteFromFile("/Application/assets/images/UI/eyes.png");
			this.AddChild(SplashImage);
			
			Scheduler.Instance.Schedule( this, (dt) => { 
				MenuSystem.SetScreen("Title"); 
				this.UnscheduleAll(); 
			}, 3.0f, false, 0);
		}
		
		// OVERRIDES -------------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			Support.SpriteUVFromFile("/Application/assets/images/UI/header.png");
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			MenuSystem = null;
			this.RemoveAllChildren(true);
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/eyes.png");
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
#if DEBUG
		~SplashScreen() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

