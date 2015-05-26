using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI
{
	public class SplashScreen : Layer
	{
//		readonly TimeSpan minProcTime = new TimeSpan(0, 0, 0, 0, 30);
		List<Action> loadProc;
//		Stopwatch stopwatch;
		int _loadIndex;
		
		SpriteTile SplashImage;
		MenuSystemScene MenuSystem;
		float _timer;
		
		public SplashScreen (MenuSystemScene pMenuSystem) {
//			stopwatch = Stopwatch.StartNew();
			_timer = 0.0f;
			MenuSystem = pMenuSystem;
			
			
			
			loadProc = new List<Action>{
				() => {
					;//dummy
				},
				() => {
					SplashImage = Support.SpriteFromFile("/Application/assets/images/UI/eyes.png");
					this.AddChild(SplashImage);
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/UI/header.png");
					temp = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_blue2.png");
					temp = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_red2.png");
					temp = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_bottom.png");
					temp = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_top.png");
					var b = new BetterButton();
					b.Cleanup();
					Support.LoadAtlas("crystallonUI", ".png");
					Support.LoadAtlas("gamePieces", ".png");
				},
				() => {
					Scheduler.Instance.Schedule( this, (dt) => { 
					_timer += dt;
						if (_timer > 3.0f) {
							MenuSystem.SetScreen("Title"); 
							this.UnscheduleAll(); 
						}
					}, 0.0f, false, 0);
				}
			};
			
			this.ScheduleUpdate(0);
		}
		
		// OVERRIDES -------------------------------------------------------------------------------------------------
		
		public override void OnExit ()
		{
			base.OnExit ();
			MenuSystem = null;
			loadProc.Clear();
			loadProc = null;
			this.RemoveAllChildren(true);
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/eyes.png");
		}
		
		public override void Update (float dt)
		{
			base.Update (dt);
			
			if ( _loadIndex < loadProc.Count ) {
				loadProc[_loadIndex++]();
			}
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
#if DEBUG
		~SplashScreen() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

