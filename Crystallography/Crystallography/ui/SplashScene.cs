using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D; 


namespace Crystallography.UI
{
    public partial class SplashScene : Sce.PlayStation.HighLevel.UI.Scene
    {
		private float _timer;
		private FadeOutEffect fadeOutEffect;
		
        public SplashScene()
        {
            InitializeWidget();
			_timer = -100.0f;
			SplashText.Font = FontManager.Instance.Get( "Bariol", 18 );
			
			FadeInEffect fadeInEffect = new FadeInEffect( SplashText, 300, new FadeInEffectInterpolator() );
			fadeOutEffect = new FadeOutEffect( SplashText, 300, new FadeOutEffectInterpolator() );
			
			fadeInEffect.EffectStopped += (sender, e) => { _timer = 1.0f; };
			fadeOutEffect.EffectStopped += (sender, e) => { fadeOutEffect = null; UISystem.SetScene( new TitleScene() ); };
			fadeInEffect.Start();
        }
		
		protected override void OnUpdate (float elapsedTime)
		{
			if (_timer > 0) {
				_timer += elapsedTime;
				if (_timer > 3000) {
//					Director.Instance.ReplaceScene( new TitleScene() );
					fadeOutEffect.Start();
				}
			}
			
			base.OnUpdate (elapsedTime);
		}
    }
}
