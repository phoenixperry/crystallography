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
		protected float _timer;
		protected float _angle;
		protected bool _timed;
		
        public LoadingScene(int pLevelNumber, bool pTimed=false)
        {
			_angle = 0.0f;
			_timer = 0.0f;
			_levelNumber = pLevelNumber;
			_timed = pTimed;
            
			InitializeWidget();
			
			Label_1.Font = FontManager.Instance.Get("Bariol", 25, "Bold");
//			BusyIndicator_1.Visible = false;
			ImageBox_1.Visible = false;
			ImageBox_1.PivotType = PivotType.MiddleCenter;
			fadeInEffect = new FadeInEffect( ImageBox_1, 500.0f, new FadeInEffectInterpolator() );
//			fadeInEffect.EffectStopped += HandleFadeInEffectEffectStopped;
			fadeInEffect.Start();
			
        }

//        void HandleFadeInEffectEffectStopped (object sender, EventArgs e)
//        {
//        	Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene( new GameScene(_levelNumber) );
//        }
		
		protected override void OnUpdate (float elapsedTime) {
			_angle += 0.2f*(elapsedTime/30.0f);
			ImageBox_1.Transform3D = Matrix4.RotationXyz(0.0f, 0.0f, _angle);
			ImageBox_1.SetPosition(500.0f, 261.0f);
			_timer += elapsedTime;
			if (_timer > 1000){
				Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene( new GameScene(_levelNumber, _timed) );
			}
		}
		
		// DESTRUCTOR --------------------------------------------------------------------------------
#if DEBUG
		~LoadingScene() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
    }
}
