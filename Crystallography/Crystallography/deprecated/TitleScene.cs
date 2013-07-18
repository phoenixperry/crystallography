using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


namespace Crystallography.UI.Deprecated
{
    public partial class TitleScene : Sce.PlayStation.HighLevel.UI.Scene
    {
		private bool _acceptTouch;
		private float _timer;
		
        public TitleScene()
        {
			_acceptTouch = false;
			_timer = 0.0f;
			
			Touch.GetData(0).Clear();
			
			InitializeWidget();
			TouchToStartText.Font = FontManager.Instance.Get("Bariol", 25);
			FadeInEffect effect = new FadeInEffect(TitleImage, 300, new FadeInEffectInterpolator() );
			effect.EffectStopped += (sender, e) => { _acceptTouch = true; };
			effect.Start();
        }
		
		protected override void OnUpdate (float elapsedTime)
		{
			if (_acceptTouch) {
				_timer += elapsedTime;
				if (_timer > 3000){
					TouchToStartText.Visible = true;
				}
				
				if ( Input2.Touch00.Down ) {
//					Director.Instance.ReplaceScene( new MenuScene() );
					UISystem.SetScene( new MenuScene() );
					this.RootWidget.Dispose();
				}
			}
			base.OnUpdate (elapsedTime);
		}
		
		// DESTRUCTOR -----------------------------------------------------------------------------
#if DEBUG
		~TitleScene() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
    }
}
