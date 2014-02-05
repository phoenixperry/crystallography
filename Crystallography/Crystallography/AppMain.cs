using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
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
	
	public static class LabelExtensions {
		public static void ShiftLabelColor (this Label self, Vector4 pColor, float pDuration = 0.0f) {
			self.StopActionByTag(3);
			var shift = new TintTo(pColor, pDuration) {
				Tag = 3,
				Get = () => self.Color,
				Set = (value) => {
					self.Color.R = value.R;
					self.Color.G = value.G;
					self.Color.B = value.B;
				},
				Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut(t,2)
			};
			self.RunAction(shift);
		}
		
		public static void ShiftLabelAlpha( this SpriteBase self, Vector4 pColor, float pDuration=0.0f) {
			self.StopActionByTag(2);
			var shift = new TintTo(pColor, pDuration) {
				Tag = 2,
				Get = () => self.Color,
				Set = (value) => { self.Color.A = value.A; },
				Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut(t,2)
			};
			self.RunAction(shift);
		}
	}
	
	public static class SpriteBaseExtensions {
		public static void ShiftSpriteColor (this SpriteBase self, Vector4 pColor, float pDuration = 0.0f) {
			self.StopActionByTag(3);
			var shift = new TintTo(pColor, pDuration) {
				Tag = 3,
				Get = () => self.Color,
				Set = (value) => {
					self.Color.R = value.R;
					self.Color.G = value.G;
					self.Color.B = value.B;
				},
				Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut(t,2)
			};
			self.RunAction(shift);
		}
		
		public static void ShiftSpriteAlpha( this SpriteBase self, Vector4 pColor, float pDuration=0.0f) {
			self.StopActionByTag(2);
			var shift = new TintTo(pColor, pDuration) {
				Tag = 2,
				Get = () => self.Color,
				Set = (value) => { self.Color.A = value.A; },
				Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut(t,2)
			};
			self.RunAction(shift);
		}
	}
	
	public static class NodeExtensions {		
		public static void RegisterPalette( this Node self, int pIndex ) {
			var reg = QColor.registry[pIndex];
			if (QColor.registry[pIndex] == null) {
				QColor.registry[pIndex] = new List<Node>();
			}
			if ( false == QColor.registry[pIndex].Contains(self) ) {
				QColor.registry[pIndex].Add(self);
			}
			QColor.Instance.ApplyUI(self, pIndex);
		}
		
		public static void UnregisterPalette( this Node self ) {
			for (int i = 0; i < QColor.registry.Length; i++) {
				var reg = QColor.registry[i];
				if (reg != null) {
					reg.Remove(self);
				}
			}
		}
	}
	
	public class AppMain
	{
		private static bool _ORIENTATION_MATTERS = true;
		
		public static bool ORIENTATION_MATTERS {
			get { 
				return _ORIENTATION_MATTERS && GameScene.currentLevel != 999; 
			}
			set { 
				_ORIENTATION_MATTERS = value;
			}
		}
		
		public static bool UI_INPUT_ENABLED = false;
		public static bool GAMEPLAY_INPUT_ENABLED = true;
		
		public static void Main (string[] args)
		{	
			Init ();
			
			while( true ){
				SystemEvents.CheckEvents();
				if( false == GameScene.paused) {
					GamePhysics.Instance.Simulate();
				}
				Director.Instance.Update();
				Touch.GetData(0).Clear();
				Director.Instance.Render();
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
		}
		
		protected static bool Init() {
			Director.Initialize();
			Director.Instance.RunWithScene( new Crystallography.UI.MenuSystemScene("Splash"), true );
			if ( false == DataStorage.LoadData() ) {
				DataStorage.Init();
			}
			
			Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.GL.Context.SetClearColor(Support.ExtractColor("E5E3D1"));
			
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

