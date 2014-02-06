using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class InfiniteModeScreen : Layer
	{
		protected MenuSystemScene MenuSystem;
		protected BetterButton playButton;
		protected BetterButton cancelButton;
		
		protected Label _timeLimitText;
		
		Slider timeLimitSlider;
		
		// CONSTRUCTOR ------------------------------------------
		
		public InfiniteModeScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			_timeLimitText = new Label() {
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") )
			};
			_timeLimitText.RegisterPalette(0);
			this.AddChild(_timeLimitText);
			
			timeLimitSlider = new Slider() {
				Text = "time limit",
				Position = new Vector2(320.0f, 400.0f),
				max = 60.0f,
				min = 5.0f,
				discreteOptions = new List<float>() { 5.0f, 10.0f, 20.0f, 35.0f, 60.0f },
				OnChange = (unused) => {
					if ( timeLimitSlider.Value != timeLimitSlider.max ) {
						_timeLimitText.Text = timeLimitSlider.Value.ToString() + " min";
					} else {
						_timeLimitText.Text = "infinite";
					}
				}
			};
			timeLimitSlider.AddTickmarks();
			timeLimitSlider.RegisterPalette(0);
			timeLimitSlider.SetSliderValue( (float)DataStorage.options[4] );
			this.AddChild(timeLimitSlider);
			
			_timeLimitText.Position = new Vector2(timeLimitSlider.Position.X + timeLimitSlider.Length + 20.0f, timeLimitSlider.Position.Y);
			
			cancelButton = new BetterButton(289.0f, 71.0f) {
				Text = "main menu",
				Position = new Vector2(671.0f, 71.0f),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			cancelButton.background.RegisterPalette(2);
			this.AddChild(cancelButton);
			
			playButton = new BetterButton(289.0f, 71.0f) {
				Text = "play",
				Position = new Vector2(671.0f, 0.0f),
//				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			playButton.background.RegisterPalette(0);
			this.AddChild(playButton);
		}
		
		// EVENT HANDLERS ----------------------------------------
		
		void HandleCancelButtonButtonUpAction (object sender, EventArgs e)
		{
			Exit();
			MenuSystem.SetScreen("Menu");
		}
		
		void HandleplayButtonButtonUpAction (object sender, EventArgs e)
		{
			Exit();
			Director.Instance.ReplaceScene(new LoadingScene(999, false) );
		}
		
		// OVERRIDES ---------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			playButton.ButtonUpAction += HandleplayButtonButtonUpAction;
			cancelButton.ButtonUpAction += HandleCancelButtonButtonUpAction;
//			timeLimitSlider.
		}
		
		public override void OnExit ()
		{
			playButton.ButtonUpAction -= HandleplayButtonButtonUpAction;
			base.OnExit ();
			timeLimitSlider = null;
			playButton = null;
		}
		
		// METHODS -----------------------------------------------
		
		protected void Exit() {
			// TODO WRITE SETTINGS TO DATA FOR PERSISTENCE
			DataStorage.options[4] = (int)timeLimitSlider.Value;
		}
		
		// DESTRUCTOR --------------------------------------------
#if DEBUG
		~InfiniteModeScreen() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}

