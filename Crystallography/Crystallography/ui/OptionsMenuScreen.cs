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
		
		ButtonEntity OKButton;
		
		// CONSTRUCTOR ---------------------------------------------------------
		
		public OptionsMenuScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			musicSlider = new Slider() {
				Text = "music volume",
				max = 1.0f,
				min = 0.0f,
				Position = new Vector2(100.0f, 400.0f),
				OnChange = (volume) => { 
					Support.MusicSystem.Instance.SetVolume(volume);
				}
			};
			musicSlider.SetSliderValue(Support.MusicSystem.Instance.Volume);
			this.AddChild(musicSlider);
			
			
			effectsSlider = new Slider() {
				Text = "effects volume",
				max = 1.0f,
				min = 0.0f,
				Position = new Vector2(100.0f, 300.0f),
				OnChange = (volume) => { 
					Support.SoundSystem.Instance.SetVolume(volume);
					Support.SoundSystem.Instance.Play( LevelManager.Instance.SoundPrefix + "high.wav" );
				}
			};
			effectsSlider.SetSliderValue(Support.SoundSystem.Instance.Volume);
			this.AddChild(effectsSlider);
			
			
			orbitSlider = new Slider() {
				Text = "selection float distance",
				max = 120.0f,
				min = 60.0f,
				Position = new Vector2(100.0f, 200.0f),
				OnChange = (radius) => { 
					SelectionGroup.EASE_DISTANCE = radius;
				}
			};
			orbitSlider.SetSliderValue(SelectionGroup.EASE_DISTANCE);
			this.AddChild(orbitSlider);
			
			
			stickySlider = new Slider() {
				Text = "selection sensitivity",
				max = 1000.0f,
				min = 300.0f,
				Position = new Vector2(100.0f, 100.0f),
				OnChange = (velocity) => { 
					SelectionGroup.MAXIMUM_PICKUP_VELOCITY = velocity;
				}
			};
			stickySlider.SetSliderValue(SelectionGroup.MAXIMUM_PICKUP_VELOCITY);
			this.AddChild(stickySlider);
			
			
			OKButton = new ButtonEntity("         ok", MenuSystem, null, Support.TiledSpriteFromFile("Application/assets/images/blueBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			OKButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			OKButton.setPosition(816.0f, 35.0f);
			this.AddChild(OKButton.getNode());
		}
		
		// EVENT HANDLERS ------------------------------------------------------
		
		void HandleOKButtonButtonUpAction (object sender, EventArgs e)
		{
			MenuSystem.SetScreen("Menu");
		}
		
		// OVERRIDES -----------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			OKButton.ButtonUpAction += HandleOKButtonButtonUpAction;
		}
		
		public override void OnExit ()
		{
			OKButton.ButtonUpAction -= HandleOKButtonButtonUpAction;
			base.OnExit ();
			stickySlider = null;
			orbitSlider = null;
			effectsSlider = null;
			musicSlider = null;
			OKButton = null;
			MenuSystem = null;
		}
		
	}
}

