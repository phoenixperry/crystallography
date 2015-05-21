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
			
			Background = null;
			PauseText = null;
			ResetButton = null;
			ResumeButton = null;
			GiveUpButton = null;
			
			RemoveAllChildren(true);
			
			InputManager.Instance.StartJustUpDetected -= HandleInputManagerInstanceStartJustUpDetected;
		}
		
		// METHODS ------------------------------------------------------------------------------------------------------------
		
		protected void Initialize() {
			_initialized = true;
			FontMap map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold") );
			
			Background = Support.UnicolorSprite("black", 0, 0, 0, 255);
			Background.Position = new Vector2(298.0f, 97.0f);
			Background.Scale = new Vector2(22.125f, 22.125f);
			
			
			PauseText = new Label("paused", map) {
				Position = new Vector2(415.0f, 342.0f),
				Color = LevelManager.Instance.BackgroundColor
			};
			
			
			GiveUpButton = new BetterButton(354.0f, 61.0f) {
				Text = "quit",
				Position = Background.Position
			};
			GiveUpButton.background.RegisterPalette(2);
			GiveUpButton.On(false);
			
			
			ResetButton = new BetterButton(354.0f, 61.0f) {
				Text = "restart",
				Position = new Vector2(GiveUpButton.Position.X, GiveUpButton.Position.Y + GiveUpButton.Height),
			};
			ResetButton.background.RegisterPalette(1);
			ResetButton.On(false);
			
			
			ResumeButton = new BetterButton(354.0f, 61.0f) {
				Text = "resume",
				Position = new Vector2(ResetButton.Position.X, ResetButton.Position.Y + ResetButton.Height)
			};
			ResumeButton.background.RegisterPalette(0);
			ResumeButton.On (false);
			
			this.AddChild(Background);
			this.AddChild(PauseText);
			this.AddChild(ResetButton);
			this.AddChild(ResumeButton);
			this.AddChild(GiveUpButton);
			
		
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