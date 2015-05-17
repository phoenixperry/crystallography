using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class Slider : Node
	{
		protected static readonly float TICK_WIDTH = 8.0f/16.0f;
		
		Label Title;
		SpriteTile Track;
		SpriteTile Knob;
		SpriteTile[] Ticks;
		Bounds2 bounds;
		float val;
		float length;
		bool active;
		
		public List<float> discreteOptions;
		
		public Action<float> OnChange;
		
		public float min;
		public float max;
		
		// GET & SET --------------------------------------------------------------------------
		
		public int NumOptions { get; protected set; }
		public int SelectedOption { get; protected set;}
		
		public float Value {
			get { return val; }
		}
		
		public float Length {
			get { return length; }
		}
		
		public string Text {
			get {
				return Title.Text;
			}
			set {
				Title.Text = value;
			}
		}
		
		// CONSTRUCTOR -----------------------------------------------------------------------
		
		public Slider (int trackLength=320) {
			active = false;
			
			Title = new Label() {
				FontMap = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 32, "Bold") ),
				Position = new Vector2(-TICK_WIDTH/2.0f, 29.0f),
				Color = Colors.Black
			};
			this.AddChild(Title);
			
			min = 0.0f;
			max = 100.0f;
			val = min;
			Track = Support.UnicolorSprite("white", 255, 255, 255, 255);
			Track.Scale = new Vector2((float)trackLength/16.0f, 0.625f);
			this.AddChild(Track);
			length = (float)trackLength;
			
//			Knob = Support.UnicolorSprite("white", 255, 255, 255, 255);
//			Knob = Support.SpriteFromFile("/Application/assets/images/UI/sliderLozenge.png");
			Knob = Support.SpriteFromAtlas("crystallonUI", "sliderLozenge.png");
			Knob.CenterSprite(new Vector2(0.5f, 0.36f));
//			Knob.Position = new Vector2((pValue/(max-min+min))*length, 0.0f);
			this.AddChild(Knob,100);
			
			bounds = new Bounds2( new Vector2(-20.0f, -20.0f), new Vector2(length+20.0f, 36.0f) );
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS --------------------------------------------------------------------
		
		void HandleInputManagerInstanceTouchDownDetected (object sender, SustainedTouchEventArgs e) {
			if(active) {
				Knob.Position = new Vector2(this.WorldToLocal(e.touchPosition).X, Knob.Position.Y);
				if ( Knob.Position.X < 0.0f) {
					Knob.Position = new Vector2( 0.0f, Knob.Position.Y);
				} else if (Knob.Position.X > length) {
					Knob.Position = new Vector2(length, Knob.Position.Y);
				}
			}
		}

		void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e) {
			if(active) {
				var percentage = Knob.Position.X / length;
				val = percentage * (max-min) + min;
				
				if ( discreteOptions != null && discreteOptions.Count > 0 ) {
					float diff = float.MaxValue;
					float finalVal = val;
					foreach (float opt in discreteOptions) {
						if (diff > Sce.PlayStation.Core.FMath.Abs(val - opt) ) {
							finalVal = opt;
							diff = val - opt;
						} else {
							break;
						}
					}
					val = finalVal;
				}
				SetSliderValue(val);
				active = false;
			}
		}

		void HandleInputManagerInstanceTouchJustDownDetected (object sender, BaseTouchEventArgs e) {
			if ( bounds.IsInside(this.WorldToLocal(e.touchPosition) ) ) {
				active = true;
				Knob.Position = new Vector2(this.WorldToLocal(e.touchPosition).X, Knob.Position.Y);
				if ( Knob.Position.X < 0.0f) {
					Knob.Position = new Vector2( 0.0f, Knob.Position.Y);
				} else if (Knob.Position.X > length) {
					Knob.Position = new Vector2(length, Knob.Position.Y);
				}
			}
		}
		
		// OVERRIDES -------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
			InputManager.Instance.TouchDownDetected += HandleInputManagerInstanceTouchDownDetected;
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
			InputManager.Instance.TouchDownDetected -= HandleInputManagerInstanceTouchDownDetected;
			
			Knob = null;
			Track = null;
			Title = null;
			Ticks = null;
		}
		
		// METHODS --------------------------------------------------------------------------
		
		public void SetSliderValue(float pValue, bool pSilent=false){
			val = Sce.PlayStation.Core.FMath.Min(pValue, max);
			val = Sce.PlayStation.Core.FMath.Max(val, min);
			Knob.Position = new Vector2( length * ((pValue-min)/(max-min)), 0.0f);
			if(discreteOptions != null && discreteOptions.Count > 0) {
				SelectedOption = discreteOptions.IndexOf(pValue);
			}
			if ( false == pSilent && OnChange != null) {
				OnChange(val);
			}
		}
		
		public void AddTickmarks() {
			if (discreteOptions != null && discreteOptions.Count > 0) {
				NumOptions = discreteOptions.Count;
				if (Ticks != null) {
					foreach (SpriteTile s in Ticks) {
						this.RemoveChild(s, true);
					}
					Ticks = null;
				}
				Ticks = new SpriteTile[discreteOptions.Count];
				var optionsArray = discreteOptions.ToArray();
				float offsetX = -( 1 + Sce.PlayStation.Core.FMath.Floor(TICK_WIDTH * 8.0f) );
				for (int i=0; i < Ticks.Length; i ++) {
					Ticks[i] = Support.UnicolorSprite("white", 255, 255, 255, 255);
					Ticks[i].Scale = new Vector2( TICK_WIDTH, 1.125f);
					Ticks[i].Position = new Vector2( length * ((optionsArray[i]-min) / (max-min)) + offsetX, -8.0f );
					Ticks[i].Color = Track.Color;
					this.AddChild(Ticks[i]);
				}
			}
		}
		
		public void RegisterPalette(int pIndex) {
//			Title.RegisterPalette(pIndex);
			Knob.RegisterPalette(((pIndex+1)%3+1)%3);
			Track.RegisterPalette(pIndex);
			if(Ticks != null ) {
				foreach (var tick in Ticks) {
					tick.RegisterPalette(pIndex);
				}
			}
		}
		
		public void UnregisterPalette() {
//			Title.UnregisterPalette();
			Knob.UnregisterPalette();
			Track.UnregisterPalette();
			if(Ticks != null ) {
				foreach (var tick in Ticks) {
					tick.UnregisterPalette();
				}
			}
		}
		
		// DESTRUCTOR ---------------------------------------------------------
#if DEBUG
		~Slider() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}

