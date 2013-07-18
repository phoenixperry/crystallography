using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class TitleScreen : Layer
	{
		SpriteTile TitleImage;
		Label TouchToStartText;
		MenuSystemScene MenuSystem;
		
		float _timer;
		
		// CONSTRUCTORS --------------------------------------------------------------------------------------------------------------------------------
		
		public TitleScreen (MenuSystemScene pMenuSystem) {
			
			MenuSystem = pMenuSystem;
			FontMap map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold") );
			
			TitleImage = Support.SpriteFromFile("/Application/assets/images/UI/header.png");
			TouchToStartText = new Label("touch to start", map);
			TouchToStartText.Position = new Vector2(229.0f, 73.0f);
			TouchToStartText.Color.A = 0.0f;
			
			Scheduler.Instance.Schedule( TouchToStartText, (dt) => {
				TouchToStartText.Color.A += 0.25f * dt;
				if (TouchToStartText.Color.A >= 1.0f) {
					TouchToStartText.Color.A = 1.0f;
					TouchToStartText.UnscheduleAll();
				}
			}, 0, false, 0);
			
			this.AddChild(TitleImage);
			this.AddChild(TouchToStartText);
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS -----------------------------------------------------------------------------------------------------------------------------
		
		void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e)
		{
			MenuSystem.SetScreen("Menu");
			InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
		}
		
		// OVERRIDES ----------------------------------------------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			_timer = 0.0f;
			base.OnEnter ();
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			TouchToStartText.UnscheduleAll();
			TouchToStartText = null;
			MenuSystem = null;
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/header.png");
		}
		
		
		public override void Update ( float dt )
        {
			if (_timer < 1.0f) {
				_timer += dt;
				if(_timer >= 1.0f) {
					InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
				}
			}
			
//			InputManager.Instance.Update(dt);
            base.Update (dt);
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
#if DEBUG
		~TitleScreen() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

