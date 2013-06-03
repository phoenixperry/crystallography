using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class GameSceneHud : Layer
	{
		SpriteUV GameHudBar;
		SpriteUV ScoreBar;
		SpriteUV TimerBar;
		SpriteUV BlueBox;
		SpriteUV RedBox;
		ButtonEntity NextLevelButton;
		ButtonEntity HitMeButton;
		ButtonEntity RestartButton;
		
		Label ScoreTitleText;
		Label GoalTitleText;
		Label ScoreText;
		Label GoalText;
		Label TimerSeparatorText;
		Label TimerSecondsText;
		Label TimerMinutesText;
		
		LevelTitle levelTitle;
		
		public LevelEndPanel levelEndPanel;
		public PausePanel pausePanel;
		
		private const float SCORE_UPDATE_DELAY = 0.200f;
		private const float INITIAL_SCORE_UPDATE_DELAY = 0.800f;
		private int _score;
		private int _displayScore;
		private float _updateTimer;
		
		private float _displayTimer;
		
		protected GameScene _scene;
		protected bool _initialized = false;
		protected bool _buttonSlideIn;
		protected bool _pauseTimer;
		
		// CONSTRUCTOR -------------------------------------------------------------------------------------------
		
		public GameSceneHud( GameScene scene ) {
			_scene = scene;
			_buttonSlideIn = false;
			
			if (_initialized == false) {
				Initialize();
			}
			Reset ();
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS -----------------------------------------------------------------------------------------
		
		void HandleCardManagerInstanceNoMatchesPossibleDetected (object sender, EventArgs e) {
			Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "levelcomplete.wav");
			NextLevelButton.setPosition(845.0f, 587.0f); //Director.Instance.GL.Context.Screen.Height + NextLevelButton.Height);
			NextLevelButton.Visible = true;
			RestartButton.on = false;
			_buttonSlideIn = true;
			_pauseTimer = true;
		}
		
		void HandleGameSceneLevelChangeDetected (object sender, EventArgs e) {
			Reset ();
			levelTitle.SetLevelText(GameScene.currentLevel);
			List<string> variables = new List<string>();
			foreach (string key in QualityManager.Instance.qualityDict.Keys) {
				if ( key == "QOrientation") continue;
				var variations = QualityManager.Instance.qualityDict[key];
				if( variations[0] != null && variations[1] != null && variations[2] != null ) {
					variables.Add(key.Substring(1));
				}
			}
			levelTitle.SetQualityNames( variables.ToArray() );
			levelTitle.EnterAnim();
			RestartButton.on = true;
		}
		
		void HandleHitMeButtonButtonUpAction (object sender, EventArgs e) {
			CardManager.Instance.Populate( true );
		}
		
		void HandleNextLevelButtonButtonUpAction (object sender, EventArgs e) {
			NextLevelButton.Visible = false;
			CardManager.Instance.Reset( Director.Instance.CurrentScene );
			GroupManager.Instance.Reset( Director.Instance.CurrentScene );
//			levelEndPanel.UpdateAndShow(_score, _displayTimer);
			NextLevelButton.ButtonUpAction -= HandleNextLevelButtonButtonUpAction;
			InputManager.Instance.CircleJustUpDetected -= HandleNextLevelButtonButtonUpAction;
			_scene.goToNextLevel();
		}
		
		void HandleCardManagerInstanceCardSpawned (object sender, EventArgs e) {
			HitMeButton.on = ( CardManager.Instance.TotalCardsInDeck > 0 && CardManager.availableCards.Count < CardManager.MAX_CARD_POPULATION );
		}
		
		void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e) {
			ScheduleScoreModifier( e.Points );
			new Crystallography.UI.ScorePopup( e.Entity, e.Points );
		}
		
		// OVERRIDES -----------------------------------------------------------------------------------------------
		
		public override void Update (float dt) {
			if (_initialized == false) {
				return;
			}
			
			base.Update (dt);
			
			if (GameScene.paused == false && _pauseTimer == false ) {
				calculateTimer( dt );
			}
			
			if ( _score != _displayScore ) {
				_updateTimer += dt;
				if ( _updateTimer > SCORE_UPDATE_DELAY ) {
					int mod;
					int difference = _score - _displayScore;
					int sign = difference > 0 ? 1 : -1;
					if ( System.Math.Abs(difference) > 9 ) {
						mod = sign * 10;
					} else {
						mod = sign;
					}
					_displayScore += mod;
					ScoreText.Text = _displayScore.ToString();
					_updateTimer = 0.0f;
				}
			}
			
			if ( _buttonSlideIn ) {
				var y = NextLevelButton.getPosition().Y;
				y -= dt * 100;
				if (y < 451.0f) {
					y = 451.0f;
					_buttonSlideIn = false;
					NextLevelButton.ButtonUpAction += HandleNextLevelButtonButtonUpAction;
					InputManager.Instance.CircleJustUpDetected += HandleNextLevelButtonButtonUpAction;
				}
				NextLevelButton.setPosition(845.0f, y);
			}
		}
		
		public override void OnEnter () {
			base.OnEnter();
			QualityManager.MatchScoreDetected += HandleQualityManagerMatchScoreDetected;
			CardManager.Instance.NoMatchesPossibleDetected += HandleCardManagerInstanceNoMatchesPossibleDetected;
			GameScene.LevelChangeDetected += HandleGameSceneLevelChangeDetected;
		}
		
		public override void OnExit () {
			base.OnExit();
			QualityManager.MatchScoreDetected -= HandleQualityManagerMatchScoreDetected;
			CardManager.Instance.NoMatchesPossibleDetected -= HandleCardManagerInstanceNoMatchesPossibleDetected;
			GameScene.LevelChangeDetected -= HandleGameSceneLevelChangeDetected;
		}
		
		
		// METHODS ------------------------------------------------------------------------------------------------
		
		private void calculateTimer(float elapsedTime) {
			_displayTimer += elapsedTime;
			var oldMinutes = TimerMinutesText.Text;
			var minutes = System.Math.Floor(_displayTimer/60.0f);
			var seconds = _displayTimer - (60.0f * minutes);
			TimerMinutesText.Text = minutes.ToString();
			if (TimerMinutesText.Text != oldMinutes) {
				var minutesOffset = 329.0f - 3.0f - Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18, "Bold").GetTextWidth(TimerMinutesText.Text);
				TimerMinutesText.Position = new Vector2(minutesOffset, 7.0f);
			}
			TimerSecondsText.Text = seconds.ToString("00.0");
		}
		
		private void Initialize() {
			_initialized = true;
			
			levelTitle = new LevelTitle(_scene);
//			_scene.DialogLayer.AddChild(levelTitle);
			this.AddChild(levelTitle);
			levelTitle.Hide();
			
			pausePanel = new PausePanel(_scene);
			_scene.DialogLayer.AddChild(pausePanel);
			pausePanel.Hide();
			
			levelEndPanel = new LevelEndPanel(_scene);
			_scene.DialogLayer.AddChild(levelEndPanel);
			levelEndPanel.Hide();
			
			NextLevelButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/next_level_sm.png", 1, 3).TextureInfo, new Vector2i(0,0) );
			NextLevelButton.setPosition(845.0f, 587.0f);
			this.AddChild(NextLevelButton.getNode());
			NextLevelButton.Visible = false;
			
			FontMap map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold") );
			FontMap bigMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold") );
			GameHudBar = Support.SpriteUVFromFile("/Application/assets/images/GameHudBar.png");
			GameHudBar.Position = new Vector2(0.0f, 473.0f);
			this.AddChild(GameHudBar);
			
//			ScoreBar = Support.SpriteUVFromFile("/Application/assets/images/UI/score_now.png");
			ScoreBar = Support.SpriteUVFromFile("/Application/assets/images/handIcon.png");
			ScoreBar.Position = new Vector2(20.0f, 16.0f);
			GameHudBar.AddChild(ScoreBar);
			
			ScoreTitleText = new Label("score", map);
			ScoreTitleText.Position = new Vector2(54.0f, 25.0f);
			ScoreTitleText.Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f);
			GameHudBar.AddChild(ScoreTitleText);
			
			BlueBox = Support.SpriteUVFromFile("/Application/assets/images/blueBox.png");
			BlueBox.Position = new Vector2(120.0f, 0.0f);
			GameHudBar.AddChild(BlueBox);
			
			ScoreText = new Label("0123456789", bigMap);
			ScoreText.Position = new Vector2(5.0f, 12.0f);
			BlueBox.AddChild(ScoreText);
			
//			TimerBar = Support.SpriteUVFromFile("/Application/assets/images/UI/time_now.png");
			TimerBar = Support.SpriteUVFromFile("/Application/assets/images/stopIcon.png");
			TimerBar.Position = new Vector2(244.0f, 16.0f);
//			GameHudBar.AddChild(TimerBar);
			
			GoalTitleText = new Label("goal", map);
			GoalTitleText.Position = new Vector2(299.0f, 25.0f);
			GoalTitleText.Color = new Vector4( 0.89803922f, 0.0745098f, 0.0745098f, 1.0f);
//			GameHudBar.AddChild(GoalTitleText);
			
			RedBox = Support.SpriteUVFromFile("/Application/assets/images/redbox.png");
			RedBox.Position = new Vector2(354.0f, 0.0f);
//			GameHudBar.AddChild(RedBox);
			
			GoalText = new Label("--", bigMap);
			GoalText.Position = new Vector2(5.0f, 12.0f);
//			RedBox.AddChild(GoalText);
			
			TimerSeparatorText = new Label(":", map);
			TimerSeparatorText.Position = new Vector2(329.0f, 9.0f);
//			RedBox.AddChild(TimerSeparatorText);
			
			TimerSecondsText = new Label("00.0", map);
			TimerSecondsText.Position = new Vector2(334.0f, 7.0f);
//			RedBox.AddChild(TimerSecondsText);
			
			TimerMinutesText = new Label("000", map);
			TimerMinutesText.Position = new Vector2(291.0f, 7.0f);
//			RedBox.AddChild(TimerMinutesText);
			
			RestartButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/restartBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			RestartButton.setPosition( 748.0f, 509.0f );
			this.AddChild(RestartButton.getNode());
			RestartButton.ButtonUpAction += HandleRestartButtonButtonUpAction;
			
			HitMeButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/hitMe.png", 1, 3).TextureInfo, new Vector2i(0,0));
			HitMeButton.setPosition(883.0f, 509.0f);
			this.AddChild(HitMeButton.getNode());
			HitMeButton.ButtonUpAction += HandleHitMeButtonButtonUpAction;
			
			CardManager.Instance.CardSpawned += HandleCardManagerInstanceCardSpawned;
		}

		void HandleRestartButtonButtonUpAction (object sender, EventArgs e)
		{
			_scene.resetToLevel();
		}
		
		public void Reset () {
			_score = 0;
			_displayScore = 0;
			_displayTimer = 0.0f;
			_updateTimer = 0.0f;
			_buttonSlideIn = false;
			_pauseTimer = false;
			ScoreText.Text = _displayScore.ToString();
		}
		
		public void ScheduleScoreModifier( int pHowMuch ) {
			if (_score == _displayScore) { // -------------- Throw a little delay on score display update if not currently updating.
				_updateTimer = -INITIAL_SCORE_UPDATE_DELAY;
			}
			_score += pHowMuch;
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~GameSceneHud() {
			Console.WriteLine("GameSceneHud deleted.");
        }
#endif
	}
}

