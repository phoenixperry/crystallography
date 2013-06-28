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
		SpriteUV ScoreIcon;
		SpriteUV GoalIcon;
		SpriteUV BlueBox;
		SpriteUV RedBox;
		ButtonEntity NextLevelButton;
		ButtonEntity HitMeButton;
		ButtonEntity RestartButton;
		
		Label ScoreTitleText;
		Label GoalTitleText;
		Label ScoreText;
		Label GoalText;
		
		SpriteUV TimeBox;
		Label TimerSeparatorText;
		Label TimerSecondsText;
		Label TimerMinutesText;
		
//		Label MessageTitleText;
//		Label MessageText;
		
		LevelTitle levelTitle;
		MessagePanel _messagePanel;
//		HudPanel _nextLevelButtonPanel;
		NextLevelPanel _nextLevelPanel;
		
		public LevelEndPanel levelEndPanel;
		public PausePanel pausePanel;
		
		private const float SCORE_UPDATE_DELAY = 0.100f;
		private const float INITIAL_SCORE_UPDATE_DELAY = 0.400f;
		
//		private int Goal;
		private int _displayScore;
		private float _updateTimer;
		private bool _metGoal;
		
		protected GameScene _scene;
		protected bool _initialized = false;
//		protected bool _buttonSlideIn;
		protected bool _pauseTimer;
		
		
		
		// GET & SET ---------------------------------------------------------------------------------------------
		
		public LevelExitCode ExitCode {get; private set;}
		public int BreaksDetected {get; private set;}
		public int HitMesDetected {get; private set;}
		public float DisplayTimer {get; private set;}
		public float MetGoalTime {get; private set;}
		public float NoMatchesPossibleTime {get; private set;}
		public int Goal {get; private set;}
		public int Score {get; private set;}
		
		// CONSTRUCTOR -------------------------------------------------------------------------------------------
		
		public GameSceneHud( GameScene scene ) {
			_scene = scene;
//			_buttonSlideIn = false;
			
			if (_initialized == false) {
				Initialize();
			}
			Reset ();
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS -----------------------------------------------------------------------------------------
		
		/// <summary>
		/// On Group broken
		/// </summary>
		void HandleGroupCrystallonEntityBreakDetected (object sender, EventArgs e)
		{
			BreaksDetected++;
		}
		
		/// <summary>
		/// Handles the card manager instance no matches possible detected.
		/// </summary>
		void HandleCardManagerInstanceNoMatchesPossibleDetected (object sender, EventArgs e) {
			NoMatchesPossibleTime = DisplayTimer;
			if (Goal <= Score) {
				RestartButton.on = false;
			}
//			_buttonSlideIn = true;
			_pauseTimer = true;
		}
		
		/// <summary>
		/// On Level Change
		/// </summary>
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
//			levelTitle.EnterAnim();
			levelTitle.SlideIn();
			if(GameScene.currentLevel != 999) {
				RestartButton.on = true;
			}
		}
		
		/// <summary>
		/// On Hit Me Button Up
		/// </summary>
		void HandleHitMeButtonButtonUpAction (object sender, EventArgs e) {
			HitMesDetected++;
			CardManager.Instance.Populate( true );
		}
		
		/// <summary>
		/// On Next Level Button Up
		/// </summary>
		void HandleNextLevelButtonButtonUpAction (object sender, EventArgs e) {
			ExitCode = LevelExitCode.NEXT_LEVEL;
#if METRICS
			DataStorage.CollectMetrics();
#endif
//			NextLevelButton.Visible = false;
			_nextLevelPanel.SlideOut();
			if( GameScene.currentLevel != 999 ) {
				DataStorage.SavePuzzleScore( GameScene.currentLevel, Score );
			}
			CardManager.Instance.Reset( Director.Instance.CurrentScene );
			GroupManager.Instance.Reset( Director.Instance.CurrentScene );
//			NextLevelButton.ButtonUpAction -= HandleNextLevelButtonButtonUpAction;
			InputManager.Instance.CircleJustUpDetected -= HandleNextLevelButtonButtonUpAction;
			_scene.goToNextLevel();
		}
		
		/// <summary>
		/// On Card Spawn
		/// </summary>
		void HandleCardManagerInstanceCardSpawned (object sender, EventArgs e) {
			HitMeButton.on = ( CardManager.Instance.TotalCardsInDeck > 0 && CardManager.availableCards.Count < CardManager.MAX_CARD_POPULATION );
		}
		
		/// <summary>
		/// On Match Score Detected
		/// </summary>
		void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e) {
			ScheduleScoreModifier( e.Points );
			new Crystallography.UI.ScorePopup( e.Entity, e.Points );
			Crystallography.UI.IconPopupManager.Instance.ScoreIcons( e.Entity, e.ScoreQualities );
		}
		
		/// <summary>
		/// On Failed Match Detected
		/// </summary>
		void HandleQualityManagerFailedMatchDetected (object sender, FailedMatchEventArgs e)
		{
			Crystallography.UI.IconPopupManager.Instance.FailedIcons( e.Entity, e.Names);
		}
		
		/// <summary>
		/// On Restart Button Up
		/// </summary>
		void HandleRestartButtonButtonUpAction (object sender, EventArgs e)
		{
			ExitCode = LevelExitCode.RESET;
#if METRICS
			DataStorage.CollectMetrics();
#endif
			_scene.resetToLevel();
		}
		
		/// <summary>
		/// On LevelSelectButton Up
		/// </summary>
		void Handle_nextLevelPanelLevelSelectDetected (object sender, EventArgs e)
		{
			ExitCode = LevelExitCode.QUIT_LEVEL_SELECT;
#if METRICS
			DataStorage.CollectMetrics();
#endif
			GameScene.QuitToLevelSelect();
		}
		
		/// <summary>
		/// On Quit From Pause Menu
		/// </summary>
		void HandlePausePanelQuitButtonPressDetected (object sender, EventArgs e)
		{
			ExitCode = LevelExitCode.QUIT_MENU;
#if METRICS
			DataStorage.CollectMetrics();
#endif
			GameScene.QuitToTitle();
		}
		
		// OVERRIDES -----------------------------------------------------------------------------------------------
		
		public override void Update (float dt) {
			if (_initialized == false) {
				return;
			}
			
			base.Update (dt);
			if (GameScene.paused == false && _pauseTimer == false ) {
				DisplayTimer += dt;
			}
			
			if ( Score != _displayScore ) {
				_updateTimer += dt;
				if ( _updateTimer > SCORE_UPDATE_DELAY ) {
					int mod;
					int difference = Score - _displayScore;
					int sign = difference > 0 ? 1 : -1;
					if ( System.Math.Abs(difference) > 9 ) {
						mod = sign * 10;
					} else {
						mod = sign;
					}
					_displayScore += mod;
					if(Goal <= _displayScore && _metGoal == false) {
						MetGoal();
					}
					ScoreText.Text = _displayScore.ToString();
					float x = 0.5f * BlueBox.CalcSizeInPixels().X - 0.5f * Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(ScoreText.Text);
					ScoreText.Position = new Vector2(x, ScoreText.Position.Y);
					_updateTimer = 0.0f;
				}
				
			}
		}
		
		public override void OnEnter () {
			base.OnEnter();
			_nextLevelPanel.NextLevelDetected += HandleNextLevelButtonButtonUpAction;
			_nextLevelPanel.QuitDetected += HandlePausePanelQuitButtonPressDetected;
			_nextLevelPanel.LevelSelectDetected += Handle_nextLevelPanelLevelSelectDetected;
			QualityManager.MatchScoreDetected += HandleQualityManagerMatchScoreDetected;
			QualityManager.FailedMatchDetected += HandleQualityManagerFailedMatchDetected;
			CardManager.Instance.NoMatchesPossibleDetected += HandleCardManagerInstanceNoMatchesPossibleDetected;
			GameScene.LevelChangeDetected += HandleGameSceneLevelChangeDetected;
			GroupCrystallonEntity.BreakDetected += HandleGroupCrystallonEntityBreakDetected;
			PausePanel.QuitButtonPressDetected += HandlePausePanelQuitButtonPressDetected;
			if(GameScene.currentLevel == 999) {
				this.Schedule(calculateTimer,1);
			}
#if METRICS
			DataStorage.AddMetric( "Goal", () => Goal, MetricSort.LAST );
			DataStorage.AddMetric( "Score", () => Score, MetricSort.LAST );
			DataStorage.AddMetric( "Time", () => DisplayTimer, MetricSort.LAST );
			DataStorage.AddMetric( "No-Match Time", () => NoMatchesPossibleTime, MetricSort.LAST );
			DataStorage.AddMetric( "Met-Goal Time", () => MetGoalTime, MetricSort.LAST);
			DataStorage.AddMetric( "Breaks", () => BreaksDetected, MetricSort.LAST );
			DataStorage.AddMetric( "Hit Me", () => HitMesDetected, MetricSort.LAST );
			DataStorage.AddMetric( "Exit Code", () => ExitCode, MetricSort.LAST );
#endif
		}
		
		public override void OnExit () {
			base.OnExit();
			_nextLevelPanel.NextLevelDetected -= HandleNextLevelButtonButtonUpAction;
			_nextLevelPanel.QuitDetected -= HandlePausePanelQuitButtonPressDetected;
			_nextLevelPanel.LevelSelectDetected -= Handle_nextLevelPanelLevelSelectDetected;
			QualityManager.MatchScoreDetected -= HandleQualityManagerMatchScoreDetected;
			QualityManager.FailedMatchDetected -= HandleQualityManagerFailedMatchDetected;
			CardManager.Instance.NoMatchesPossibleDetected -= HandleCardManagerInstanceNoMatchesPossibleDetected;
			GameScene.LevelChangeDetected -= HandleGameSceneLevelChangeDetected;
			GroupCrystallonEntity.BreakDetected -= HandleGroupCrystallonEntityBreakDetected;
			PausePanel.QuitButtonPressDetected -= HandlePausePanelQuitButtonPressDetected;
			if(GameScene.currentLevel == 999) {
				this.Unschedule(calculateTimer);
			}
#if METRICS
			if(ExitCode == LevelExitCode.NULL){
				DataStorage.CollectMetrics();
			}
			DataStorage.RemoveMetric("Goal");
			DataStorage.RemoveMetric("Score");
			DataStorage.RemoveMetric("Time");
			DataStorage.RemoveMetric("No-Match Time");
			DataStorage.RemoveMetric("Met-Goal Time");
			DataStorage.RemoveMetric("Breaks");
			DataStorage.RemoveMetric("Hit Me");
			DataStorage.RemoveMetric("Exit Code");
#endif
		}
		
		
		// METHODS ------------------------------------------------------------------------------------------------
		
		private void calculateTimer(float dt) {
			var oldMinutes = TimerMinutesText.Text;
			var minutes = System.Math.Floor(DisplayTimer/60.0f);
			var seconds = DisplayTimer - (60.0f * minutes);
			TimerMinutesText.Text = minutes.ToString("00");
			if (TimerMinutesText.Text != oldMinutes) {
				var minutesOffset = 97.0f - 3.0f - Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(TimerMinutesText.Text);
				TimerMinutesText.Position = new Vector2(minutesOffset, -4.0f);
			}
			TimerSecondsText.Text = seconds.ToString("00.0");
		}
		
		private void Initialize() {
			_initialized = true;
			
			FontMap map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold") );
			FontMap bigMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold") );
			
			pausePanel = new PausePanel(_scene);
			_scene.DialogLayer.AddChild(pausePanel);
			pausePanel.Hide();
			
			levelEndPanel = new LevelEndPanel(_scene);
			_scene.DialogLayer.AddChild(levelEndPanel);
			levelEndPanel.Hide();
			
			GameHudBar = Support.SpriteUVFromFile("/Application/assets/images/GameHudBar.png");
			GameHudBar.Position = new Vector2(0.0f, 473.0f);
			this.AddChild(GameHudBar);
			
			levelTitle = new LevelTitle(_scene){
				Offset = new Vector2(LevelTitle.X_OFFSET, 0.0f),
				Lifetime = 4.0f
			};
			GameHudBar.AddChild(levelTitle, -1);
			
			_nextLevelPanel = new NextLevelPanel(){
				Offset = new Vector2(510.0f, 0.0f),
				
			};
			GameHudBar.AddChild(_nextLevelPanel, -1);
			
//			NextLevelButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/next_level_sm.png", 1, 3).TextureInfo, new Vector2i(0,0) );
////			NextLevelButton.setPosition(845.0f, 587.0f);
//			NextLevelButton.setPosition(115.0f,0.0f);
////			this.AddChild(NextLevelButton.getNode());
//			NextLevelButton.Visible = true;
//			
//			_nextLevelButtonPanel = new HudPanel(){
//				Height = NextLevelButton.Height*1.5f,
//				Width = NextLevelButton.Width,
//				Offset = new Vector2(729.0f, 0.0f),
//				DismissDelay = 0.0f
//			};
//			GameHudBar.AddChild(_nextLevelButtonPanel, -1);
//			_nextLevelButtonPanel.AddChild(NextLevelButton.getNode());
			
			_messagePanel = new MessagePanel(){
				Offset = new Vector2(480.0f, 0.0f),
				Position = new Vector2(-400.0f, 0.0f),
			};
			
//			MessageTitleText = new Label() {
//				Text = "Lorem ipsum dolor sit amet, consectetur",
//				FontMap = map,
//				Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f),
//				Position = new Vector2(40.0f, 135.0f)
//			};
//			
//			MessageText = new Label() {
//				Text = "Lorem ipsum dolor sit amet, consectetur adipisicing \n" +
//					"elit, sed do eiusmod tempor incididunt ut labore et \n" +
//					"dolore magna aliqua. Ut enim ad minim veniam, quis \n"+
//					"nostrud exercitation ullamco laboris nisi ut aliquip \n" +
//					"ex ea commodo consequat. Duis aute irure dolor in \n", //+
////					"reprehenderit in voluptate velit esse cillum dolore eu \n" +
////					"fugiat nulla pariatur. Excepteur sint occaecat cupidatat \n" +
////					"non proident, sunt in culpa qui officia deserunt mollit \n" +
////					"anim id est laborum.",
//				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18) ),
//				Position = new Vector2(40.0f, 107.0f)
//			};
//			var messageBG = Support.UnicolorSprite("Grey", 40, 40, 40, 200);
//			messageBG.Scale = new Vector2(30.0f, 11.0f);
//			var messageBar = Support.UnicolorSprite("LightBlue", 41, 226, 226, 255);
//			messageBar.Scale = new Vector2(30.0f, 0.25f);
//			messageBar.Position = new Vector2(0.0f, 172.0f);
//			_messagePanel.AddChild( messageBG );
//			_messagePanel.AddChild( messageBar);
//			_messagePanel.AddChild( MessageTitleText);
//			_messagePanel.AddChild( MessageText);
			this.AddChild(_messagePanel);
			
			Sequence sequence = new Sequence();
			sequence.Add( new CallFunc( () => {_messagePanel.SlideIn();} ) );
			sequence.Add( new DelayTime( 5.0f) );
			sequence.Add( new CallFunc( () => {_messagePanel.SlideOut();} ) );
			this.RunAction(sequence);
			
//			ScoreIcon = Support.SpriteUVFromFile("/Application/assets/images/UI/score_now.png");
			ScoreIcon = Support.SpriteUVFromFile("/Application/assets/images/handIcon.png");
			ScoreIcon.Position = new Vector2(20.0f, 16.0f);
			GameHudBar.AddChild(ScoreIcon);
			
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
			
//			GoalIcon = Support.SpriteUVFromFile("/Application/assets/images/UI/time_now.png");
			
			if(GameScene.currentLevel != 999) {
				GoalIcon = Support.SpriteUVFromFile("/Application/assets/images/stopIcon.png");
				GoalIcon.Position = new Vector2(244.0f, 16.0f);
				GameHudBar.AddChild(GoalIcon);
			
				GoalTitleText = new Label("goal", map);
				GoalTitleText.Position = new Vector2(299.0f, 25.0f);
				GoalTitleText.Color = new Vector4( 0.89803922f, 0.0745098f, 0.0745098f, 1.0f);
				GameHudBar.AddChild(GoalTitleText);
			
				RedBox = Support.SpriteUVFromFile("/Application/assets/images/redbox.png");
				RedBox.Position = new Vector2(354.0f, 0.0f);
				GameHudBar.AddChild(RedBox);
			
				GoalText = new Label("--", bigMap);
				GoalText.Position = new Vector2(5.0f, 12.0f);
				RedBox.AddChild(GoalText);
				
				RestartButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/restartBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
				RestartButton.setPosition( 748.0f, 509.0f );
				this.AddChild(RestartButton.getNode());
				RestartButton.ButtonUpAction += HandleRestartButtonButtonUpAction;
			} else {
				TimeBox = Support.SpriteUVFromFile("/Application/assets/images/timerIcon.png");
//				TimeBox.Position = new Vector2(454.0f, 16.0f);
				TimeBox.Position = new Vector2(244.0f, 16.0f);
				GameHudBar.AddChild(TimeBox);
				
				TimerSeparatorText = new Label(":", bigMap);
				TimerSeparatorText.Position = new Vector2(97.0f, -2.0f);
				TimeBox.AddChild(TimerSeparatorText);
			
				TimerSecondsText = new Label("00.0", bigMap);
				TimerSecondsText.Position = new Vector2(107.0f, -4.0f);
				TimeBox.AddChild(TimerSecondsText);
			
				TimerMinutesText = new Label("00", bigMap);
				TimerMinutesText.Position = new Vector2(45.0f, -4.0f);
				TimeBox.AddChild(TimerMinutesText);
				
//				this.Schedule(calculateTimer,1);
			}
			
			HitMeButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/hitMe.png", 1, 3).TextureInfo, new Vector2i(0,0));
			HitMeButton.setPosition(883.0f, 509.0f);
			this.AddChild(HitMeButton.getNode());
			HitMeButton.ButtonUpAction += HandleHitMeButtonButtonUpAction;
			
			CardManager.Instance.CardSpawned += HandleCardManagerInstanceCardSpawned;
		}
		
		public void Reset () {
//			_nextLevelButtonPanel.Reset();
//			_nextLevelPanel.Reset();
			Score = 0;
			_displayScore = 0;
			DisplayTimer = 0.0f;
			MetGoalTime = 0.0f;
			NoMatchesPossibleTime = 0.0f;
			_updateTimer = 0.0f;
			BreaksDetected = 0;
			HitMesDetected = 0;
			
			ExitCode = LevelExitCode.NULL;
			
//			_buttonSlideIn = false;
			_pauseTimer = false;
			_metGoal = false;
			ScoreText.Text = _displayScore.ToString();
			float x = 0.5f * BlueBox.CalcSizeInPixels().X - 0.5f * Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(ScoreText.Text);
			ScoreText.Position = new Vector2(x, ScoreText.Position.Y);
			
			if(GameScene.currentLevel != 999) {
				Goal = LevelManager.Instance.Goal;
				GoalText.Text = Goal.ToString();
				x = 0.5f * RedBox.CalcSizeInPixels().X - 0.5f * Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(GoalText.Text);
				GoalText.Position = new Vector2(x, GoalText.Position.Y);
			}
		}
		
		public void ScheduleScoreModifier( int pHowMuch ) {
			if (Score == _displayScore) { // -------------- Throw a little delay on score display update if not currently updating.
				_updateTimer = -INITIAL_SCORE_UPDATE_DELAY;
			}
			Score += pHowMuch;
		}
		
		public void MetGoal() {
			MetGoalTime = DisplayTimer;
			_metGoal = true;
			Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "levelcomplete.wav");
//			NextLevelButton.setPosition(845.0f, 587.0f); //Director.Instance.GL.Context.Screen.Height + NextLevelButton.Height);
//			_nextLevelButtonPanel.SlideIn();
			_nextLevelPanel.SlideIn();
//			NextLevelButton.Visible = true;
//			RestartButton.on = false;
//			_buttonSlideIn = true;
//			Sequence sequence = new Sequence();
//			sequence.Add(new MoveTo (new Vector2(845.0f, 451.0f), 1.0f ) );
//			sequence.Add(new DelayTime(1.0f));
//			sequence.Add(new CallFunc( () => { 
//				NextLevelButton.ButtonUpAction += HandleNextLevelButtonButtonUpAction;
//				InputManager.Instance.CircleJustUpDetected += HandleNextLevelButtonButtonUpAction;
//			} ) );
//			NextLevelButton.getNode().RunAction( sequence );
			_pauseTimer = true;
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~GameSceneHud() {
			Console.WriteLine("GameSceneHud deleted.");
        }
#endif
	}
	
	public enum LevelExitCode {
		NULL, NEXT_LEVEL, RESET, QUIT_MENU, QUIT_LEVEL_SELECT
	}
}

