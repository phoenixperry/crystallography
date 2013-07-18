using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.UI; 
using Sce.PlayStation.HighLevel.GameEngine2D; 
namespace Crystallography
{
	public static class RandomExtensions
    {
        public static bool NextBool(this Random self)
        {
            return (self.Next() & 1) == 0;
        }

        public static float NextFloat(this Random self)
        {
            return (float)self.NextDouble();
        }

        public static float NextSignedFloat(this Random self)
        {
            return (float)self.NextDouble() * (float)self.NextSign();
        }

        public static float NextAngle(this Random self)
        {
            return self.NextFloat() * FMath.PI * 2.0f;
        }

        public static float NextSign(this Random self)
        {
            return self.NextDouble() < 0.5 ? -1.0f : 1.0f;
        }

        public static Vector2 NextVector2(this Random self)
        {
            return Vector2.UnitX.Rotate(self.NextFloat() * FMath.PI * 2.0f);
        }

        public static Vector2 NextVector2(this Random self, float magnitude)
        {
            return Vector2.UnitX.Rotate(self.NextFloat() * FMath.PI * 2.0f) * magnitude;
        }

        public static Vector2 NextVector2Variable(this Random self)
        {
            return new Vector2(self.NextFloat(), self.NextFloat());
        }
    }
	
	public class AppMain
	{
		private static readonly bool _ORIENTATION_MATTERS = true;
		
		public static bool ORIENTATION_MATTERS { get { return _ORIENTATION_MATTERS && GameScene.currentLevel != 999; } }
		
		public static bool UI_INPUT_ENABLED = false;
		public static bool GAMEPLAY_INPUT_ENABLED = true;
		
		public static void Main (string[] args)
		{	
			Init ();
			
			while( true ){
				SystemEvents.CheckEvents();
				GamePhysics.Instance.Simulate();
				if ( UI_INPUT_ENABLED ) {
//					UISystem.Update( Touch.GetData(0) );
					Director.Instance.Update();
					Touch.GetData(0).Clear();
					Director.Instance.Render();
//					UISystem.Render();
				} else {
					Director.Instance.Update();
					Touch.GetData(0).Clear();
					Director.Instance.Render();
				}
				
				
//				Director.Instance.Render();
//				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
		}
		
		protected static bool Init() {
			Director.Initialize();
//			UISystem.Initialize(Director.Instance.GL.Context);
			Director.Instance.RunWithScene( new Crystallography.UI.MenuSystemScene("Splash"), true );
//			Director.Instance.RunWithScene( new TitleScene(), true );
			
			if ( false == DataStorage.LoadData() ) {
				DataStorage.Init();
			}
			
#if METRICS
			if ( false == DataStorage.LoadMetrics() ) {
				DataStorage.ClearMetrics();
			}
//			DataStorage.PrintMetrics();
#endif
			
			return true;
		}
	}
}

