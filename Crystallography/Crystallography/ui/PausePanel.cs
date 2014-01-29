using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class PausePanel : Layer
	{
		SpriteTile Background;
		Label PauseText;
//		FontMap map;
//		ButtonEntity ResetButton;
//		ButtonEntity ResumeButton;
//		ButtonEntity GiveUpButton;
		
		BetterButton ResetButton;
		BetterButton ResumeButton;
		BetterButton GiveUpButton;
		
		protected bool _initialized = false;
		protected GameScene _scene;
		
		public static event EventHandler QuitButtonPressDetected;
		public static event EventHandler ResetButtonPressDetected;
		public static event EventHandler<PauseEventArgs> PauseDetected;
		
		// CONSTRUCTOR --------------------------------------------------------------------------------------------------------
		
		public PausePanel (GameScene scene) {
			_scene = scene;
			if (_initialized == false) {
				Initialize();
			}
		}
		
		// EVENT HANDLERS -----------------------------------------------------------------------------------------------------
		
		void HandleInputManagerInstanceStartJustUpDetected (object sender, EventArgs e) {
			PauseToggle();
		}
		
		void HandleResetButtonButtonUpAction (object sender, EventArgs e) {
			Pause (false);
			EventHandler handler = ResetButtonPressDetected;
			if (handler != null ) {
				handler(this, null);
			}
		}
		
		void HandleGiveUpButtonButtonUpAction (object sender, EventArgs e) {
			EventHandler handler = QuitButtonPressDetected;
			if (handler != null ) {
				handler(this, null);
			}
		}

		void HandleResumeButtonButtonUpAction (object sender, EventArgs e) {
			Pause ( false );
		}
		
		// OVERRIDES ----------------------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			
			InputManager.Instance.StartJustUpDetected += HandleInputManagerInstanceStartJustUpDetected;
		}
		
		public override void OnExit ()
		{
			ResumeButton.ButtonUpAction -= HandleResumeButtonButtonUpAction;
			GiveUpButton.ButtonUpAction -= HandleGiveUpButtonButtonUpAction;
			InputManager.Instance.StartJustUpDetected -= HandleInputManagerInstanceStartJustUpDetected;
			base.OnExit ();
			
			InputManager.Instance.StartJustUpDetected -= HandleInputManagerInstanceStartJustUpDetected;
		}
		
		// METHODS ------------------------------------------------------------------------------------------------------------
		
		protected void Initialize() {
			_initialized = true;
			FontMap map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold") );
			
//			Background = Support.SpriteUVFromFile("/Application/assets/images/PausePanelBG.png");
			Background = Support.UnicolorSprite("med_grey", 153, 153, 153, 255);
			Background.Position = new Vector2(328.0f, 112.0f);
			Background.Scale = new Vector2(20.0f, 18.9375f);
			this.AddChild(Background);
			
			PauseText = new Label("Paused.", map);
			PauseText.Position = new Vector2(418.0f, 365.0f);
			this.AddChild(PauseText);
			
			ResetButton = new BetterButton(289.0f, 71.0f) {
				Text = "restart",
				Position = new Vector2(343.0f, 203.0f),
				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)//,
//				On = false
			};
			ResetButton.On(false);
			this.AddChild(ResetButton);
			
			ResumeButton = new BetterButton(289.0f, 71.0f) {
				Text = "resume",
				Position = new Vector2(343.0f, 284.0f),
				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)//,
//				On = false
			};
			ResumeButton.On (false);
			this.AddChild(ResumeButton);
			
			GiveUpButton = new BetterButton(289.0f, 71.0f) {
				Text = "quit",
				Position = new Vector2(343.0f, 122.0f),
				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)//,
//				On = false
			};
			GiveUpButton.On(false);
			this.AddChild(GiveUpButton);
			
//			ResetButton = new ButtonEntity("      restart", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/blueBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			ResetButton.setPosition(487.0f, 238.0f);
//			ResetButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
//			ResetButton.on = false;
//			this.AddChild(ResetButton.getNode());
//			
//			ResumeButton = new ButtonEntity("     resume", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/blueBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			ResumeButton.setPosition(487.0f, 319.0f);
//			ResumeButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
//			ResumeButton.on = false;
//			this.AddChild(ResumeButton.getNode());
//			
//			GiveUpButton = new ButtonEntity("        quit", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/redBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			GiveUpButton.setPosition(487.0f, 157.0f);
//			GiveUpButton.label.FontMap = ResumeButton.label.FontMap;
//			GiveUpButton.on = false;
//			this.AddChild(GiveUpButton.getNode());
		
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		public void Hide() {
			this.Visible = false;
			ResetButton.On(false);
			ResumeButton.On(false);
			GiveUpButton.On(false);
			ResetButton.ButtonUpAction -= HandleResetButtonButtonUpAction;
			ResumeButton.ButtonUpAction -= HandleResumeButtonButtonUpAction;
			GiveUpButton.ButtonUpAction -= HandleGiveUpButtonButtonUpAction;
		}
		
		public void Show() {
			this.Visible = true;
			ResetButton.On(true);
			ResumeButton.On(true);
			GiveUpButton.On(true);
			ResetButton.ButtonUpAction += HandleResetButtonButtonUpAction;
			ResumeButton.ButtonUpAction += HandleResumeButtonButtonUpAction;
			GiveUpButton.ButtonUpAction += HandleGiveUpButtonButtonUpAction;
		}
		
		public void Pause( bool pOn ) {
			if( GameScene.canPause == false) return;
#if DEBUG
			Console.WriteLine("Pause: " + pOn);
#endif
			if(pOn) {
				Show ();
			} else {
				Hide ();
			}
//			PauseMenu.Visible = pOn;
//			AppMain.UI_INPUT_ENABLED = pOn;
//			AppMain.GAMEPLAY_INPUT_ENABLED = !pOn;
			EventHandler<PauseEventArgs> handler = PauseDetected;
			if (handler != null) {
				handler( this, new PauseEventArgs { isPaused = pOn } );
			}
		}
		
		public void PauseToggle() {
			Pause( !GameScene.paused );
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~PausePanel() {
			Console.WriteLine("PausePanel deleted.");
        }
#endif
	}
}

// HELPER CLASSES ----------------------------------------------------------------------------------------

public class PauseEventArgs : EventArgs {
		public bool isPaused;
	}