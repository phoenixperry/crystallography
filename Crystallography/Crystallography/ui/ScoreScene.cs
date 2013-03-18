using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI
{
    public partial class ScoreScene : Sce.PlayStation.HighLevel.UI.Scene
    {
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
            InitializeWidget();
			
			// Set fonts to non-system font
			//ScoreLabelText.Font = FontManager.Instance.Get("Bariol", 25);
			ScoreText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
			TimerSecondsText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
			TimerMinutesText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
			TimerSeparatorText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
			PauseMenuText.Font = FontManager.Instance.Get ("Bariol", 44);
			ResumeButton.TextFont = FontManager.Instance.Get ("Bariol", 25);
			GiveUpButton.TextFont = FontManager.Instance.Get ("Bariol", 25);
			NextLevelButton.TextFont = FontManager.Instance.Get ("Bariol", 25);
			NextLevelButton.Visible = false;
			
			// Assign Event Handlers
			InputManager.Instance.StartJustUpDetected += (sender, e) => { PauseToggle(); };
			ResumeButton.TouchEventReceived += HandleResumeButtonTouchEventReceived;
			GiveUpButton.TouchEventReceived += HandleGiveUpButtonTouchEventReceived;
			NextLevelButton.TouchEventReceived += HandleNextLevelButtonTouchEventReceived;
			QualityManager.MatchScoreDetected += HandleQualityManagerMatchScoreDetected;
			CardManager.Instance.NoMatchesPossibleDetected += HandleCardManagerInstanceNoMatchesPossibleDetected; //+= (sender, e) => {
			
			Reset();
		}

        // EVENT HANDLERS -------------------------------------------------------------------
		
		void HandleCardManagerInstanceNoMatchesPossibleDetected (object sender, EventArgs e)
        {
        	switch (_currentLayoutOrientation){
          		case LayoutOrientation.Vertical:
            		new SlideInEffect()
                	    {
                	        Widget = NextLevelButton,
                	        MoveDirection = FourWayDirection.Up,
                	    }.Start();
                    	break;

                default:
                   	new SlideInEffect()
                   	{
                   	    Widget = NextLevelButton,
                   	    MoveDirection = FourWayDirection.Up,
                   	}.Start();
                   	break;
            }
				Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "levelcomplete.wav");
				NextLevelButton.Visible = true;
//			}
        }
	
		void HandleNextLevelButtonButtonAction (object sender, TouchEventArgs e)
        {
			foreach (TouchEvent v in e.TouchEvents){
				Console.WriteLine(v.Type.ToString());
				if (v.Type == TouchEventType.None) {
					(Director.Instance.CurrentScene as GameScene).goToNextLevel();
				}
			}
        }
		
		void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e) {
			ScheduleScoreModifier( e.Points );
//			ScorePanel panel = new ScorePanel( e.Entity, e.Points );
			new ScorePopup( e.Entity, e.Points );
//			this.RootWidget.AddChildLast(panel);
        }
		
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
				NextLevelButton.Visible = false;
				this.RootWidget.AddChildLast( new LevelEndPanel( _score, _displayTimer ) );
			}
        }
		
		void HandleResumeButtonTouchEventReceived (object sender, TouchEventArgs e)
        {
			//TODO: Buttons should activate on up, but the SDK is funky w/r/t "button up" events. Redo later.
			Pause ( false );
        }
		
		// OVERRIDES ------------------------------------------------------------------------
		
		protected override void OnUpdate (float elapsedTime)
		{
//			float dt = elapsedTime - _lastFrame;
//			_lastFrame = elapsedTime;
			if (GameScene.paused == false ) {
				calculateTimer( elapsedTime );
			}
			base.OnUpdate (elapsedTime);
			
			if ( _score != _displayScore ) {
				_updateTimer += elapsedTime;
				if ( _updateTimer > SCORE_UPDATE_DELAY ) {
					int mod;
					int difference = _score - _displayScore;
					int sign = difference > 0 ? 1 : -1;
					if ( Math.Abs(difference) > 9 ) {
						mod = sign * 10;
					} else {
						mod = sign;
					}
//					if (sign > 0) {
//						Support.SoundSystem.Instance.Play("score_up.wav");
//					} else {
//						Support.SoundSystem.Instance.Play("score_down.wav");
//					}
					_displayScore += mod;
					ScoreText.Text = _displayScore.ToString();
					_updateTimer = 0.0f;
				}
			}
		}
		
		// METHODS --------------------------------------------------------------------------
		
		private void calculateTimer(float elapsedTime) {
			_displayTimer += elapsedTime/1000.0f;
			var minutes = Math.Floor(_displayTimer/60.0f);
			var seconds = _displayTimer - (60.0f * minutes);
			TimerMinutesText.Text = minutes.ToString();
			TimerSecondsText.Text = seconds.ToString("00.0");
		}
		
		public void Pause( bool pOn ) {
//			paused = pOn;
			PauseMenu.Visible = pOn;
			EventHandler<PauseEventArgs> handler = PauseDetected;
			if (handler != null) {
				handler( this, new PauseEventArgs { isPaused = pOn } );
			}
		}
		
		public void PauseToggle() {
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
    }
	
	// HELPER CLASSES --------------------------------------------------------------------------------------
	
	public class PauseEventArgs : EventArgs {
		public bool isPaused;
	}
}
