using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class OptionsMenuScreen : Layer
	{
		MenuSystemScene MenuSystem;
		
		Slider stickySlider;
		Slider orbitSlider;
		Slider effectsSlider;
		Slider musicSlider;
		
//		ButtonEntity OKButton;
//		ButtonEntity CancelButton;
//		ButtonEntity ClearButton;
//		ButtonEntity ClearOKButton;
		
		BetterButton OKButton;
		BetterButton CancelButton;
		BetterButton ClearButton;
		BetterButton ClearOKButton;
		
		MessagePanel ClearPanel;
		
		float musicVolume;
		float effectsVolume;
		float orbitDistance;
		float stickiness;
		
		// CONSTRUCTOR ---------------------------------------------------------
		
		public OptionsMenuScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			musicVolume = Support.MusicSystem.Instance.Volume;
			effectsVolume = Support.SoundSystem.Instance.Volume;
			orbitDistance = SelectionGroup.EASE_DISTANCE;
			stickiness = SelectionGroup.MAXIMUM_PICKUP_VELOCITY;
			
			musicSlider = new Slider() {
				Text = "music volume",
				max = 1.0f,
				min = 0.0f,
				Position = new Vector2(320.0f, 400.0f),
				OnChange = (volume) => { 
					Support.MusicSystem.Instance.SetVolume(volume);
				}
			};
			musicSlider.SetSliderValue((float)(DataStorage.options[0]/100.0f));
			this.AddChild(musicSlider);
			
			
			effectsSlider = new Slider() {
				Text = "effects volume",
				max = 1.0f,
				min = 0.0f,
				Position = new Vector2(320.0f, 300.0f),
				OnChange = (volume) => { 
					Support.SoundSystem.Instance.SetVolume(volume);
					Support.SoundSystem.Instance.Play( LevelManager.Instance.SoundPrefix + "high.wav" );
				}
			};
			effectsSlider.SetSliderValue((float)(DataStorage.options[1]/100.0f));
			this.AddChild(effectsSlider);
			
			
			orbitSlider = new Slider() {
				Text = "selection float distance",
				max = 120.0f,
				min = 60.0f,
				Position = new Vector2(320.0f, 200.0f),
				OnChange = (radius) => { 
					SelectionGroup.EASE_DISTANCE = radius;
				}
			};
			orbitSlider.SetSliderValue((float)DataStorage.options[2]);
			this.AddChild(orbitSlider);
			
			
			stickySlider = new Slider() {
				Text = "selection sensitivity",
				max = 1000.0f,
				min = 300.0f,
				Position = new Vector2(320.0f, 100.0f),
				OnChange = (velocity) => { 
					SelectionGroup.MAXIMUM_PICKUP_VELOCITY = velocity;
				}
			};
			stickySlider.SetSliderValue((float)DataStorage.options[3]);
			this.AddChild(stickySlider);
			
			OKButton = new BetterButton(289.0f, 71.0f) {
				Text = "ok",
				Position = new Vector2(671.0f, 0.0f),
				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			this.AddChild(OKButton);
			
			CancelButton = new BetterButton(289.0f, 71.0f) {
				Text = "cancel",
				Position = new Vector2(671.0f, 71.0f),
				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			this.AddChild(CancelButton);
			
			ClearPanel = new MessagePanel(289.0f, 240.0f) {
				TitleText = "really?",
				Text = "destroy your hard-\nwon progress? \nthis cannot be \nundone.",
				Offset = new Vector2(0.0f, OKButton.Height),
				Width = OKButton.Width
			};
			this.AddChild(ClearPanel);
			
			ClearButton = new BetterButton(289.0f, 71.0f) {
				Text = "clear all data",
				Position = new Vector2(0.0f, 0.0f),
				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			this.AddChild(ClearButton);
			
			ClearOKButton = new BetterButton(289.0f, 71.0f) {
				Text = "do it",
				Position = new Vector2(0.0f, 0.0f),
				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			ClearPanel.AddChild(ClearOKButton);
		}
		
		// EVENT HANDLERS ------------------------------------------------------
		
		void HandleOKButtonButtonUpAction (object sender, EventArgs e)
		{
			Exit();
		}
		
		
		void HandleCancelButtonButtonUpAction (object sender, EventArgs e)
		{
			Support.MusicSystem.Instance.SetVolume(musicVolume);
			Support.SoundSystem.Instance.SetVolume(effectsVolume);
			SelectionGroup.EASE_DISTANCE = orbitDistance;
			SelectionGroup.MAXIMUM_PICKUP_VELOCITY = stickiness;
			Exit();
		}
		
		
		void HandleClearButtonButtonUpAction (object sender, EventArgs e)
		{
			ClearPanel.SlideIn();
		}
		
		
		void HandleClearPanelOnSlideInComplete (object sender, EventArgs e)
		{
			ClearOKButton.ButtonUpAction += HandleClearOKButtonButtonUpAction;
		}
		
		
		void HandleClearPanelOnSlideOutStart (object sender, EventArgs e)
		{
			ClearOKButton.ButtonUpAction -= HandleClearOKButtonButtonUpAction;
		}
		
		
		void HandleClearOKButtonButtonUpAction (object sender, EventArgs e)
		{
			DataStorage.ClearData();
			ClearOKButton.ButtonUpAction -= HandleClearOKButtonButtonUpAction;
		}
		
		// OVERRIDES -----------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			OKButton.ButtonUpAction += HandleOKButtonButtonUpAction;
			ClearButton.ButtonUpAction += HandleClearButtonButtonUpAction;
			ClearPanel.OnSlideInComplete += HandleClearPanelOnSlideInComplete;
			ClearPanel.OnSlideOutStart += HandleClearPanelOnSlideOutStart;
			CancelButton.ButtonUpAction += HandleCancelButtonButtonUpAction;
		}

		
		public override void OnExit ()
		{
			OKButton.ButtonUpAction -= HandleOKButtonButtonUpAction;
			ClearButton.ButtonUpAction -= HandleClearButtonButtonUpAction;
			ClearPanel.OnSlideInComplete -= HandleClearPanelOnSlideInComplete;
			ClearOKButton.ButtonUpAction -= HandleClearOKButtonButtonUpAction;
			CancelButton.ButtonUpAction -= HandleCancelButtonButtonUpAction;
			base.OnExit ();
			stickySlider = null;
			orbitSlider = null;
			effectsSlider = null;
			musicSlider = null;
			OKButton = null;
			MenuSystem = null;
		}
		
		
		// METHODS ------------------------------------------------------------
		
		protected void Exit() {
			DataStorage.options[0] = (int)(Support.MusicSystem.Instance.Volume * 100.0);
			DataStorage.options[1] = (int)(Support.SoundSystem.Instance.Volume * 100.0);
			DataStorage.options[2] = (int)SelectionGroup.EASE_DISTANCE;
			DataStorage.options[3] = (int)SelectionGroup.MAXIMUM_PICKUP_VELOCITY;
			DataStorage.SaveData();
			MenuSystem.SetScreen("Menu");
		}
		
		// DESTRUCTOR ---------------------------------------------------------
#if DEBUG
		~OptionsMenuScreen() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}

