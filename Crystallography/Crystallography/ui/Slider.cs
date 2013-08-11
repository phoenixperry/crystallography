using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class Slider : Node
	{
		Label Title;
		SpriteTile Track;
		SpriteTile Knob;
		Bounds2 bounds;
		float val;
		float length;
		bool active;
		
		public Action<float> OnChange;
		
		public float min;
		public float max;
		
		public string Text {
			get {
				return Title.Text;
			}
			set {
				Title.Text = value;
			}
		}
		
		// CONSTRUCTOR -----------------------------------------------------------------------
		
		public Slider () {
			active = false;
			
			Title = new Label() {
				FontMap = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
				Position = new Vector2(0.0f, 36.0f)
			};
			this.AddChild(Title);
			
			min = 0.0f;
			max = 100.0f;
			Track = Support.UnicolorSprite("white", 255, 255, 255, 255);
			Track.Scale = new Vector2(20.0f, 0.5f);
			this.AddChild(Track);
			length = 320;
			
			Knob = Support.UnicolorSprite("white", 255, 255, 255, 255);
			Knob.CenterSprite(new Vector2(0.5f, 0.0f));
//			Knob.Position = new Vector2((pValue/(max-min+min))*length, 0.0f);
			this.AddChild(Knob);
			
			bounds = new Bounds2( new Vector2(-20.0f, -20.0f), new Vector2(length+20.0f, 36.0f) );
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
				active = false;
				OnChange(val);
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
		}
		
		// METHODS --------------------------------------------------------------------------
		
		public void SetSliderValue(float pValue){
			val = pValue;
			Knob.Position = new Vector2((pValue/(max-min+min))*length, 0.0f);
		}
		
	}
}

