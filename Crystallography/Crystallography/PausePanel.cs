using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class PausePanel : Layer
	{
		SpriteUV Background;
		Label PauseText;
//		FontMap map;
		ButtonEntity ResumeButton;
		ButtonEntity GiveUpButton;
		
		protected bool _initialized = false;
		protected GameScene _scene;
		
		public static event EventHandler QuitButtonPressDetected;
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
			base.OnExit ();
			
			InputManager.Instance.StartJustUpDetected -= HandleInputManagerInstanceStartJustUpDetected;
		}
		
		// METHODS ------------------------------------------------------------------------------------------------------------
		
		protected void Initialize() {
			_initialized = true;
			FontMap map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold") );
			
			Background = Support.SpriteUVFromFile("/Application/assets/images/PausePanelBG.png");
			Background.Position = new Vector2(55.0f, 227.0f);
			this.AddChild(Background);
			
			PauseText = new Label("Paused.", map);
			PauseText.Position = new Vector2(26.0f, 28.0f);
			Background.AddChild(PauseText);
			
			ResumeButton = new ButtonEntity("    resume", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/blueBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			ResumeButton.setPosition(413.0f, 277.0f);
			ResumeButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			this.AddChild(ResumeButton.getNode());
			
			GiveUpButton = new ButtonEntity("      give up", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/redBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			GiveUpButton.setPosition(694.0f, 277.0f);
			GiveUpButton.label.FontMap = ResumeButton.label.FontMap;
			this.AddChild(GiveUpButton.getNode());
		}
		
		public void Hide() {
			this.Visible = false;
			ResumeButton.ButtonUpAction -= HandleResumeButtonButtonUpAction;
			GiveUpButton.ButtonUpAction -= HandleGiveUpButtonButtonUpAction;
		}
		
		public void Show() {
			this.Visible = true;
			ResumeButton.ButtonUpAction += HandleResumeButtonButtonUpAction;
			GiveUpButton.ButtonUpAction += HandleGiveUpButtonButtonUpAction;
		}
		
		public void Pause( bool pOn ) {
			if( GameScene.canPause == false) return;
			
			Console.WriteLine("Pause: " + pOn);
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