using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI.Deprecated
{
    public partial class ScoreScene : Sce.PlayStation.HighLevel.UI.Scene
    {
		LevelTitle LevelTitle_1;
//		LevelEndPanel LevelEndPanel_1;
		
		private const float SCORE_UPDATE_DELAY = 200.0f;
		private const float INITIAL_SCORE_UPDATE_DELAY = 800.0f;
		private int _score;
		private int _displayScore;
		private float _updateTimer;
		
		private float _displayTimer;
		
		public static event EventHandler<PauseEventArgs> PauseDetected;
		public static event EventHandler QuitButtonPressDetected;
		
		// CONSTRUCTOR ---------------------------------------------------------------------
		
        public ScoreScene() {
//            InitializeWidget();
//			
//			LevelTitle_1 = new LevelTitle();
//			LevelTitle_1.Y = 200;
//			this.RootWidget.AddChildLast(LevelTitle_1);
//			LevelTitle_1.Visible = false;
////			LevelEndPanel_1 = new LevelEndPanel();// _score, _displayTimer );
////			this.RootWidget.AddChildLast( LevelEndPanel_1 );
////			LevelEndPanel_1.Hide();
//			
//			
//			// Set fonts to non-system font
//			//ScoreLabelText.Font = FontManager.Instance.Get("Bariol", 25);
//			ScoreText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
//			TimerSecondsText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
//			TimerMinutesText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
//			TimerSeparatorText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
//			PauseMenuText.Font = FontManager.Instance.Get ("Bariol", 44);
//			ResumeButton.TextFont = FontManager.Instance.Get ("Bariol", 25);
//			GiveUpButton.TextFont = FontManager.Instance.Get ("Bariol", 25);
//			NextLevelButton.TextFont = FontManager.Instance.Get ("Bariol", 25);
//			NextLevelButton.Visible = false;
//			
//			Reset();
//
//#if DEBUG
//			Console.WriteLine(GetType().ToString() + " created" );
//#endif
		}

        // EVENT HANDLERS -------------------------------------------------------------------
		
		void HandleCardManagerInstanceNoMatchesPossibleDetected (object sender, EventArgs e)
        {
        	switch (_currentLayoutOrientation){
          		case LayoutOrientation.Vertical:
            		new SlideInEffect()
                	    {
                	        Widget = NextLevelButton,
                	        MoveDirection = FourWayDirection.Down,
                	    }.Start();
                    	break;

                default:
                   	new SlideInEffect()
                   	{
                   	    Widget = NextLevelButton,
                   	    MoveDirection = FourWayDirection.Down,
                   	}.Start();
                   	break;
            }
			Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "levelcomplete.wav");
			NextLevelButton.Visible = true;
			
//			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
//			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
			InputManager.Instance.CircleJustUpDetected += HandleInputManagerInstanceCircleJustUpDetected;
			NextLevelButton.TouchEventReceived += HandleNextLevelButtonTouchEventReceived;
        }
		
		void HandleGameSceneLevelChangeDetected (object sender, EventArgs e)
		{
			Reset();
			ShowLevelTitle();
		}

		void HandleInputManagerInstanceCircleJustUpDetected (object sender, EventArgs e)
		{
			if (NextLevelButton.Visible) {
				NextLevelButtonReleased();
			}
		}
		
		void HandleInputManagerInstanceStartJustUpDetected (object sender, EventArgs e)
		{
			PauseToggle();
		}
		
//		void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e)
//		{
//			if (NextLevelButton.Visible) {
//				if ( e.touchPosition.X > NextLevelButton.X && e.touchPosition.X < NextLevelButton.X + NextLevelButton.Width ) {
//					int height = Director.Instance.GL.Context.GetViewport().Height;
//					if ( height - e.touchPosition.Y > NextLevelButton.Y && height - e.touchPosition.Y < NextLevelButton.Y + NextLevelButton.Height ) {
//						NextLevelButtonReleased();
//					}
//				}
//			}
//		}

//		void HandleInputManagerInstanceTouchJustDownDetected (object sender, BaseTouchEventArgs e)
//		{
//			if (NextLevelButton.Visible) {
//				if ( e.touchPosition.X > NextLevelButton.X && e.touchPosition.X < NextLevelButton.X + NextLevelButton.Width ) {
//					int height = Director.Instance.GL.Context.GetViewport().Height;
//					if ( height - e.touchPosition.Y > NextLevelButton.Y && height - e.touchPosition.Y < NextLevelButton.Y + NextLevelButton.Height ) {
//						NextLevelButton.IconImage = NextLevelButton.CustomImage.BackgroundPressedImage;
//					}
//				}
//			}
//		}
	
		void HandleNextLevelButtonButtonAction (object sender, TouchEventArgs e)
        {
			foreach (TouchEvent v in e.TouchEvents){
				Console.WriteLine(v.Type.ToString());
				if (v.Type == TouchEventType.None) {
//					(Director.Instance.CurrentScene as GameScene).TempDisableInput();
					(Director.Instance.CurrentScene as GameScene).goToNextLevel();
//					InputManager.Instance.TouchDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
//					InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
				}
			}
        }
		
//		void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e) {
//			ScheduleScoreModifier( e.Points );
//			new ScorePopup( e.Entity, e.Points );
//        }
		
		void HandleGiveUpButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			EventHandler handler = QuitButtonPressDetected;
			if (handler != null ) {
				handler(this, null);
			}
        }
		
		void HandleNextLevelButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			TouchEvent v = e.TouchEvents[0];
			if (v.Type == TouchEventType.Up) {
//				(Director.Instance.CurrentScene as GameScene).goToNextLevel();
//				NextLevelButton.Visible = false;
//				this.RootWidget.AddChildLast( new LevelEndPanel( _score, _displayTimer ) );
//				LevelEndPanel_1.Show (_score, _displayTimer);
				NextLevelButtonReleased();
			}
        }
		
		void HandleResumeButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			//TODO: Buttons should activate on up, but the SDK is funky w/r/t "button up" events. Redo later.
			Pause ( false );
        }
		
		// OVERRIDES ------------------------------------------------------------------------
		
		protected override void OnHiding () {
			base.OnHiding ();
			
			// Assign Event Handlers
//			InputManager.Instance.StartJustUpDetected -= HandleInputManagerInstanceStartJustUpDetected;
//			GameScene.LevelChangeDetected -= HandleGameSceneLevelChangeDetected;
//			ResumeButton.TouchEventReceived -= HandleResumeButtonTouchEventReceived;
//			GiveUpButton.TouchEventReceived -= HandleGiveUpButtonTouchEventReceived;
//			QualityManager.MatchScoreDetected -= HandleQualityManagerMatchScoreDetected;
//			CardManager.Instance.NoMatchesPossibleDetected -= HandleCardManagerInstanceNoMatchesPossibleDetected;
		}
		
		protected override void OnShown () {
			base.OnShown ();
			
			// Assign Event Handlers
//			InputManager.Instance.StartJustUpDetected += HandleInputManagerInstanceStartJustUpDetected;
//			GameScene.LevelChangeDetected += HandleGameSceneLevelChangeDetected;
//			ResumeButton.TouchEventReceived += HandleResumeButtonTouchEventReceived;
//			GiveUpButton.TouchEventReceived += HandleGiveUpButtonTouchEventReceived;
//			QualityManager.MatchScoreDetected += HandleQualityManagerMatchScoreDetected;
//			CardManager.Instance.NoMatchesPossibleDetected += HandleCardManagerInstanceNoMatchesPossibleDetected;
		}
		
		protected override void OnUpdate (float elapsedTime)
		{
////			float dt = elapsedTime - _lastFrame;
////			_lastFrame = elapsedTime;
//			if (GameScene.paused == false ) {
//				calculateTimer( elapsedTime );
//			}
//			base.OnUpdate (elapsedTime);
//			
//			if ( _score != _displayScore ) {
//				_updateTimer += elapsedTime;
//				if ( _updateTimer > SCORE_UPDATE_DELAY ) {
//					int mod;
//					int difference = _score - _displayScore;
//					int sign = difference > 0 ? 1 : -1;
//					if ( Math.Abs(difference) > 9 ) {
//						mod = sign * 10;
//					} else {
//						mod = sign;
//					}
////					if (sign > 0) {
////						Support.SoundSystem.Instance.Play("score_up.wav");
////					} else {
////						Support.SoundSystem.Instance.Play("score_down.wav");
////					}
//					_displayScore += mod;
//					ScoreText.Text = _displayScore.ToString();
//					_updateTimer = 0.0f;
//				}
//			}
		}
		
		// METHODS --------------------------------------------------------------------------
		
		private void calculateTimer(float elapsedTime) {
			_displayTimer += elapsedTime/1000.0f;
			var minutes = Math.Floor(_displayTimer/60.0f);
			var seconds = _displayTimer - (60.0f * minutes);
			TimerMinutesText.Text = minutes.ToString();
			TimerSecondsText.Text = seconds.ToString("00.0");
		}
		
		public void NextLevelButtonReleased() {
//			NextLevelButton.IconImage = NextLevelButton.CustomImage.BackgroundNormalImage;
			NextLevelButton.Visible = false;
			CardManager.Instance.Reset( Director.Instance.CurrentScene );
			GroupManager.Instance.Reset( Director.Instance.CurrentScene );
//			this.RootWidget.AddChildLast( new LevelEndPanel( _score, _displayTimer ) );
//			LevelEndPanel_1.Show (_score, _displayTimer);
//			InputManager.Instance.TouchDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
//			InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
			InputManager.Instance.CircleJustUpDetected -= HandleInputManagerInstanceCircleJustUpDetected;
		}
		
		public void Pause( bool pOn ) {
			PauseMenu.Visible = pOn;
			AppMain.UI_INPUT_ENABLED = pOn;
			AppMain.GAMEPLAY_INPUT_ENABLED = !pOn;
			EventHandler<PauseEventArgs> handler = PauseDetected;
			if (handler != null) {
				handler( this, new PauseEventArgs { isPaused = pOn } );
			}
		}
		
		public void PauseToggle() {
			Console.WriteLine( (!GameScene.paused).ToString() );
			Pause( !GameScene.paused );
		}
		
		public void Reset () {
			_score = 0;
			_displayScore = 0;
			_displayTimer = 0.0f;
			_updateTimer = 0.0f;
			ScoreText.Text = _displayScore.ToString();
			NextLevelButton.Visible = false;
		}
		
		public void ScheduleScoreModifier( int pHowMuch ) {
			if (_score == _displayScore) { // -------------- Throw a little delay on score display update if not currently updating.
				_updateTimer = -INITIAL_SCORE_UPDATE_DELAY;
			}
			_score += pHowMuch;
		}
		
		void ShowLevelTitle() {
			LevelTitle_1.SetLevelText(GameScene.currentLevel);
//			string[] variables = new string[] {"Color", "Pattern"};
			List<string> variables = new List<string>();
			foreach (string key in QualityManager.Instance.qualityDict.Keys) {
				if ( key == "QOrientation") continue;
				var variations = QualityManager.Instance.qualityDict[key];
				if( variations[0] != null && variations[1] != null && variations[2] != null ) {
					variables.Add(key.Substring(1));
				}
			}
//			.CopyTo(variables,0);
			LevelTitle_1.SetVariableNames( variables.ToArray() );
			
			switch (_currentLayoutOrientation){
          		case LayoutOrientation.Vertical:
            		new SlideInEffect()
                	    {
                	        Widget = LevelTitle_1,
                	        MoveDirection = FourWayDirection.Left,
                	    }.Start();
                    	break;

                default:
                   	new SlideInEffect()
                   		{
                   	    	Widget = LevelTitle_1,
                   	    	MoveDirection = FourWayDirection.Left,
                   		}.Start();
                   		break;
            }
			
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime(1.5f) );
			sequence.Add( new CallFunc( () => new SlideOutEffect{ Widget = LevelTitle_1, MoveDirection = FourWayDirection.Left }.Start() ));
//			sequence.Add (new CallFunc( () => LevelTitle_1.SetVisible(false) ) );
			Director.Instance.CurrentScene.RunAction( sequence );
			AppMain.UI_INPUT_ENABLED = false;
			AppMain.GAMEPLAY_INPUT_ENABLED = true;
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~ScoreScene() {
			Console.WriteLine("ScoreScene deleted.");
        }
#endif
    }
	
	// HELPER CLASSES --------------------------------------------------------------------------------------
	
	public class PauseEventArgs : EventArgs {
		public bool isPaused;
	}
}
