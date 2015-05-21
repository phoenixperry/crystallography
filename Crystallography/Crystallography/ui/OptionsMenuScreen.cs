using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class OptionsMenuScreen : Layer
	{
		static readonly float SLIDER_H_GAP = 114.0f;
		static readonly float SLIDER_V_GAP = 154.0f;
		static readonly int SLIDER_TRACK_LENGTH = 331;
		
		static readonly float BUTTON_WIDTH = 351.0f;
		static readonly float BUTTON_HEIGHT = 61.0f;
		
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
			
			// SLIDERS -----------------------
			
			// MUSIC SLIDER
			musicVolume = Support.MusicSystem.Instance.Volume;
			effectsVolume = Support.SoundSystem.Instance.Volume;
			orbitDistance = SelectionGroup.EASE_DISTANCE;
			stickiness = SelectionGroup.MAXIMUM_PICKUP_VELOCITY;
			
			float anchorX = (960.0f - (2.0f * (float)SLIDER_TRACK_LENGTH + SLIDER_H_GAP)) / 2.0f;
			float anchorY = 397.0f;
			
			musicSlider = new Slider(SLIDER_TRACK_LENGTH) {
				Text = "music volume",
				max = 1.0f,
				min = 0.0f,
				Position = new Vector2(anchorX, anchorY),
				OnChange = (volume) => { 
					Support.MusicSystem.Instance.SetVolume(volume);
				}
			};
			musicSlider.SetSliderValue((float)(DataStorage.options[0]/100.0f));
			musicSlider.RegisterPalette(2);
			
			
			// SOUND EFFECTS SLIDER
			effectsSlider = new Slider(SLIDER_TRACK_LENGTH) {
				Text = "sound effects volume",
				max = 1.0f,
				min = 0.0f,
				Position = new Vector2(anchorX, anchorY - SLIDER_V_GAP),
				OnChange = (volume) => { 
					Support.SoundSystem.Instance.SetVolume(volume);
					Support.SoundSystem.Instance.Play( LevelManager.Instance.SoundPrefix + "high.wav" );
				}
			};
			effectsSlider.SetSliderValue((float)(DataStorage.options[1]/100.0f));
			effectsSlider.RegisterPalette(1);
			
			
			// SELECTION SENSITIVITY SLIDER
			stickySlider = new Slider(SLIDER_TRACK_LENGTH) {
				Text = "selection sensitivity",
				max = 1000.0f,
				min = 300.0f,
				Position = new Vector2(anchorX + musicSlider.Length + SLIDER_H_GAP, anchorY),
				OnChange = (velocity) => { 
					SelectionGroup.MAXIMUM_PICKUP_VELOCITY = velocity;
				}
			};
			stickySlider.SetSliderValue((float)DataStorage.options[3]);
			stickySlider.RegisterPalette(1);
			
			
			// ORBIT DISTANCE SLIDER
			orbitSlider = new Slider(SLIDER_TRACK_LENGTH) {
				Text = "selection float distance",
				max = 120.0f,
				min = 60.0f,
				Position = new Vector2(stickySlider.Position.X, anchorY - SLIDER_V_GAP),
				OnChange = (radius) => { 
					SelectionGroup.EASE_DISTANCE = radius;
				}
			};
			orbitSlider.SetSliderValue((float)DataStorage.options[2]);
			orbitSlider.RegisterPalette(0);
			
			// BUTTONS -------------------------------
			
			// OK BUTTON
			OKButton = new BetterButton(BUTTON_WIDTH, BUTTON_HEIGHT) {
				Text = "ok",
				Position = new Vector2(960.0f-BUTTON_WIDTH, 0.0f),
//				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			OKButton.background.RegisterPalette(1);
			
			
			// CANCEL BUTTON
			CancelButton = new BetterButton(BUTTON_WIDTH, BUTTON_HEIGHT) {
				Text = "cancel",
				Position = new Vector2(OKButton.Position.X, BUTTON_HEIGHT),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			CancelButton.background.RegisterPalette(2);
			
			
			// CLEAR DATA BUTTON
			ClearButton = new BetterButton(BUTTON_WIDTH, BUTTON_HEIGHT) {
				Text = "clear all data",
				Position = new Vector2(0.0f, 0.0f),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			ClearButton.background.RegisterPalette(2);
			
			
			// CLEAR DATA CONFIRMATION PANEL -------------------------
			
			// CLEAR MESSAGE PANEL
			ClearPanel = new MessagePanel(BUTTON_WIDTH, 165.0f) {
				TitleText = "really?",
				Text = "erase all your progress?\nthis cannot be undone.",
				Offset = new Vector2(0.0f, 0.0f),//OKButton.Height),
				Width = OKButton.Width
			};
			ClearPanel.BackgroundAlpha = 1.0f;
			
			
			// CLEAR DATA CONFIRMATION BUTTON
			ClearOKButton = new BetterButton(BUTTON_WIDTH, BUTTON_HEIGHT) {
				Text = "make it so",
				Position = new Vector2(0.0f, 0.0f),
//				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			ClearOKButton.background.RegisterPalette(1);
			
			
			
			this.AddChild(musicSlider);
			this.AddChild(effectsSlider);
			this.AddChild(stickySlider);
			this.AddChild(orbitSlider);
			this.AddChild(OKButton);
			this.AddChild(CancelButton);
			this.AddChild(ClearButton);
			this.AddChild(ClearPanel);
			
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
		
		
		void HandleClearPanelOnSlideInStart (object sender, EventArgs e)
		{
			ClearButton.ButtonUpAction -= HandleClearButtonButtonUpAction;
		}
		
		
		void HandleClearPanelOnSlideInComplete (object sender, EventArgs e)
		{
			ClearOKButton.ButtonUpAction += HandleClearOKButtonButtonUpAction;
		}
		
		
		void HandleClearPanelOnSlideOutStart (object sender, EventArgs e)
		{
			ClearOKButton.ButtonUpAction -= HandleClearOKButtonButtonUpAction;
		}
		
		
		void HandleClearPanelOnSlideOutComplete (object sender, EventArgs e)
		{
			ClearButton.ButtonUpAction += HandleClearButtonButtonUpAction;
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
			ClearPanel.OnSlideInStart += HandleClearPanelOnSlideInStart;
			ClearPanel.OnSlideInComplete += HandleClearPanelOnSlideInComplete;
			ClearPanel.OnSlideOutStart += HandleClearPanelOnSlideOutStart;
			ClearPanel.OnSlideOutComplete += HandleClearPanelOnSlideOutComplete;
			CancelButton.ButtonUpAction += HandleCancelButtonButtonUpAction;
		}

		
		public override void OnExit ()
		{
			OKButton.UnregisterPalette();
			ClearButton.UnregisterPalette();
			ClearOKButton.UnregisterPalette();
			CancelButton.UnregisterPalette();
			
			OKButton.ButtonUpAction -= HandleOKButtonButtonUpAction;
			ClearButton.ButtonUpAction -= HandleClearButtonButtonUpAction;
			ClearPanel.OnSlideInStart -= HandleClearPanelOnSlideInStart;;
			ClearPanel.OnSlideInComplete -= HandleClearPanelOnSlideInComplete;
			ClearPanel.OnSlideOutStart -= HandleClearPanelOnSlideOutStart;
			ClearPanel.OnSlideOutComplete -= HandleClearPanelOnSlideOutComplete;
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

