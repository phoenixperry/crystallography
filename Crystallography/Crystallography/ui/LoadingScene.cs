using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    public partial class LoadingScene : Scene
    {
		protected int _levelNumber;
		protected FadeInEffect fadeInEffect;
		protected float _timer = 0.0f;
		
        public LoadingScene(int pLevelNumber)
        {
			_levelNumber = pLevelNumber;
            InitializeWidget();
			BusyIndicator_1.Visible = false;
			fadeInEffect = new FadeInEffect( BusyIndicator_1, 1.0f, new FadeInEffectInterpolator() );
			fadeInEffect.EffectStopped += HandleFadeInEffectEffectStopped;
			fadeInEffect.Start();
        }

        void HandleFadeInEffectEffectStopped (object sender, EventArgs e)
        {
//        	Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene( new GameScene(_levelNumber) );
        }
		
		protected override void OnUpdate (float elapsedTime) {
			_timer += elapsedTime;
			if (_timer > 1000){
				Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene( new GameScene(_levelNumber) );
			}
		}
    }
}
