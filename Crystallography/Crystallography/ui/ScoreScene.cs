using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
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
		
        public ScoreScene() {
            InitializeWidget();
			ScoreLabelText.Font = FontManager.Instance.Get("Bariol", 25);
			ScoreText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
			Reset();
			AbstractQuality.MatchScoreDetected += HandleAbstractQualityMatchScoreDetected;
        }
		
		// EVENT HANDLERS -------------------------------------------------------------------
		
		void HandleAbstractQualityMatchScoreDetected (object sender, MatchScoreEventArgs e) {
			ScheduleScoreModifier( e.Points );
        }
		
		// OVERRIDES ------------------------------------------------------------------------
		
		protected override void OnUpdate (float elapsedTime)
		{
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
					_displayScore += mod;
					ScoreText.Text = _displayScore.ToString();
					_updateTimer = 0.0f;
				}
			}
		}
		
		// METHODS --------------------------------------------------------------------------
		
		public void ScheduleScoreModifier( int pHowMuch ) {
			if (_score == _displayScore) { // -------------- Throw a little delay on score display update if not currently updating.
				_updateTimer = -INITIAL_SCORE_UPDATE_DELAY;
			}
			_score += pHowMuch;
		}

		public void Reset () {
			_score = 0;
			_displayScore = 0;
			_updateTimer = 0.0f;
			ScoreText.Text = _displayScore.ToString();
		}
    }
}
