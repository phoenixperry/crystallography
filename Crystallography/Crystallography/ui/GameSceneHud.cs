using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class GameSceneHud : Layer
	{
		SpriteUV GameHudBar;
		SpriteUV ScoreIcon;
		SpriteTile CubeIcon;
		SpriteTile BlueBox;
		SpriteTile RedBox;
		
		BetterButton HitMeButton;
		BetterButton PauseButton;
		
		Label ScoreTitleText;
		Label CubesTitleText;
		Label ScoreText;
		Label CubeText;
		
		TimerEntity GameTimer;
		
//		SpriteUV TimeBox;
//		Label TimerSeparatorText;
//		Label TimerSecondsText;
//		Label TimerMinutesText;
//		
		SpriteTile TimeBar;
		
		public LevelTitle levelTitle;
		public MessagePanel _messagePanel;
		public NextLevelPanel _nextLevelPanel;
		
//		public LevelEndPanel levelEndPanel;
		public PausePanel pausePanel;
		
		private const float SCORE_UPDATE_DELAY = 0.100f;
		private const float INITIAL_SCORE_UPDATE_DELAY = 0.400f;
		
		private int _displayScore;
		private float _updateTimer;
		
		protected GameScene _scene;
		protected bool _initialized = false;
		protected bool _pauseTimer;
		
		public static event EventHandler CubesUpdated;
		public static event EventHandler ScoreUpdated;
		public static event EventHandler BreaksUpdated;
		
		// GET & SET ---------------------------------------------------------------------------------------------
		
		public LevelExitCode ExitCode {get; private set;}
		public int BreaksDetected {get; private set;}
		public int HitMesDetected {get; private set;}
		public float DisplayTimer {get; private set;}
		public float MetGoalTime {get; private set;}
		public float NoMatchesPossibleTime {get; private set;}
		public int Goal {get; private set;}
		public int Score {get; private set;}
		public int Cubes {get; private set;}
		
		// CONSTRUCTOR -------------------------------------------------------------------------------------------
		
		public GameSceneHud( GameScene scene ) {
			_scene = scene;
			if (_initialized == false) {
				Initialize();
			}
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
			EventHandler handler = BreaksUpdated;
			if ( handler != null ) {
				handler( this, null );
			}
		}
		
		void HandleCubeCrystallonEntityCubeCompleteDetected (object sender, CubeCompleteEventArgs e)
		{
			EnableHitMeButton();
		}
		
		/// <summary>
		/// Handles the card manager instance no matches possible detected.
		/// </summary>
		void HandleCardManagerInstanceNoMatchesPossibleDetected (object sender, EventArgs e) {
//			NoMatchesPossibleTime = DisplayTimer;
			NoMatchesPossibleTime = GameTimer.LevelTimer;
			MetGoal();
//			_pauseTimer = true;
			GameTimer.Pause(true);
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
			levelTitle.SlideIn();
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
		void Handle_nextLevelPanelButtonButtonUpAction (object sender, EventArgs e) {
			ExitCode = LevelExitCode.NEXT_LEVEL;
#if METRICS
			DataStorage.CollectMetrics();
#endif
			_nextLevelPanel.SlideOut();
			bool complete = false;
			if( GameScene.currentLevel != 999 ) {
				DataStorage.SavePuzzleScore( GameScene.currentLevel, Cubes, Score, complete );
			}
			CardManager.Instance.Reset( Director.Instance.CurrentScene );
			GroupManager.Instance.Reset( Director.Instance.CurrentScene );
			InputManager.Instance.CircleJustUpDetected -= Handle_nextLevelPanelButtonButtonUpAction;
			_scene.GoToNextLevel();
		}
		
		/// <summary>
		/// On Card Spawn
		/// </summary>
		void HandleCardManagerInstanceCardSpawned (object sender, EventArgs e) {
			EnableHitMeButton();
		}
		
		/// <summary>
		/// On Match Score Detected
		/// </summary>
		void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e) {
			Cubes++;
			CubeText.Text = Cubes.ToString();
			float x = RedBox.Position.X +  50.0f - 0.5f * FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(CubeText.Text);
			CubeText.Position = new Vector2(x, CubeText.Position.Y);
			
			EventHandler handler = CubesUpdated;
			if ( handler != null ) {
				handler( this, null );
			}
			
			ScheduleScoreModifier( e.Points );
			new ScorePopup( e.Node, e.Points );
			if (e.Entity is GroupCrystallonEntity) {
				IconPopupManager.Instance.ScoreIcons( e.Entity as GroupCrystallonEntity, e.ScoreQualities );
			} else {
				IconPopupManager.Instance.ScoreIcons( e.Entity as CardCrystallonEntity, e.ScoreQualities );
			}
//			if ( GameScene.currentLevel == 999 ) {
//				DisplayTimer -= 10.0f;
//			}
		}
		
		/// <summary>
		/// On Failed Match Detected
		/// </summary>
		void HandleQualityManagerFailedMatchDetected (object sender, FailedMatchEventArgs e)
		{
			IconPopupManager.Instance.FailedIcons( e.Entity, e.Names );
		}
		
		
		void HandlePauseButtonButtonUpAction (object sender, EventArgs e)
		{
			pausePanel.PauseToggle();
		}
		
		/// <summary>
		/// On Restart Button Up
		/// </summary>
//		void HandleRestartButtonButtonUpAction (object sender, EventArgs e)
//		{
//			ExitCode = LevelExitCode.RESET;
//			_nextLevelPanel.SlideOut();
//#if METRICS
//			DataStorage.CollectMetrics();
//#endif
//			_scene.ResetToLevel();
//		}
		void HandlePausePanelResetButtonPressDetected (object sender, EventArgs e)
		{
			ExitCode = LevelExitCode.RESET;
			_nextLevelPanel.SlideOut();
#if METRICS
			DataStorage.CollectMetrics();
#endif
			_scene.ResetToLevel();
		}
		
		
		/// <summary>
		/// On LevelSelectButton Up
		/// </summary>
		void Handle_nextLevelPanelLevelSelectDetected (object sender, EventArgs e)
		{
			ExitCode = LevelExitCode.QUIT_LEVEL_SELECT;
			bool complete = false;
#if METRICS
			DataStorage.CollectMetrics();
#endif
			if( GameScene.currentLevel != 999 ) {
				DataStorage.SavePuzzleScore( GameScene.currentLevel, Cubes, Score, complete );
			}
			GameScene.QuitToLevelSelect();
		}
		
		void Handle_nextLevelPanelQuitButtonPressDetected (object sender, EventArgs e) {
			bool complete = false;
			if( GameScene.currentLevel != 999 ) {
				DataStorage.SavePuzzleScore( GameScene.currentLevel, Cubes, Score, complete );
			}
			HandlePausePanelQuitButtonPressDetected( sender, e );
		}
		
		/// <summary>
		/// On Quit From Pause Menu
		/// </summary>
		void HandlePausePanelQuitButtonPressDetected (object sender, EventArgs e)
		{
			PausePanel.QuitButtonPressDetected -= HandlePausePanelQuitButtonPressDetected;
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
//			if (GameScene.paused == false && _pauseTimer == false ) {
//				DisplayTimer += dt;
//			}
			
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
					ScoreText.Text = _displayScore.ToString();
					float x = BlueBox.Position.X + 50.0f - 0.5f * FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(ScoreText.Text);
					ScoreText.Position = new Vector2(x, ScoreText.Position.Y);
					_updateTimer = 0.0f;
					
					if ( Score == _displayScore ) {
						EventHandler handler = ScoreUpdated;
						if ( handler != null ) {
							handler( this, null );
						}
					}
				}
				
			}
		}
		
		public override void OnEnter () {
			base.OnEnter();
			_nextLevelPanel.NextLevelDetected += Handle_nextLevelPanelButtonButtonUpAction;
			_nextLevelPanel.QuitDetected += Handle_nextLevelPanelQuitButtonPressDetected;
			_nextLevelPanel.LevelSelectDetected += Handle_nextLevelPanelLevelSelectDetected;
			QualityManager.MatchScoreDetected += HandleQualityManagerMatchScoreDetected;
			QualityManager.FailedMatchDetected += HandleQualityManagerFailedMatchDetected;
			CardManager.Instance.NoMatchesPossibleDetected += HandleCardManagerInstanceNoMatchesPossibleDetected;
			GameScene.LevelChangeDetected += HandleGameSceneLevelChangeDetected;
			GroupCrystallonEntity.BreakDetected += HandleGroupCrystallonEntityBreakDetected;
			PausePanel.QuitButtonPressDetected += HandlePausePanelQuitButtonPressDetected;
			PausePanel.ResetButtonPressDetected += HandlePausePanelResetButtonPressDetected;
			CubeCrystallonEntity.CubeCompleteDetected += HandleCubeCrystallonEntityCubeCompleteDetected;
			CardManager.Instance.CardSpawned += HandleCardManagerInstanceCardSpawned;
//			if(GameScene.currentLevel == 999) {
//				this.Schedule(CalculateTimer,1);
//			}
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
			_nextLevelPanel.NextLevelDetected -= Handle_nextLevelPanelButtonButtonUpAction;
			_nextLevelPanel.QuitDetected -= Handle_nextLevelPanelQuitButtonPressDetected;
			_nextLevelPanel.LevelSelectDetected -= Handle_nextLevelPanelLevelSelectDetected;
			HitMeButton.ButtonUpAction -= HandleHitMeButtonButtonUpAction;
			PauseButton.ButtonUpAction -= HandlePauseButtonButtonUpAction;
			QualityManager.MatchScoreDetected -= HandleQualityManagerMatchScoreDetected;
			QualityManager.FailedMatchDetected -= HandleQualityManagerFailedMatchDetected;
			CardManager.Instance.NoMatchesPossibleDetected -= HandleCardManagerInstanceNoMatchesPossibleDetected;
			GameScene.LevelChangeDetected -= HandleGameSceneLevelChangeDetected;
			GroupCrystallonEntity.BreakDetected -= HandleGroupCrystallonEntityBreakDetected;
			PausePanel.QuitButtonPressDetected -= HandlePausePanelQuitButtonPressDetected;
			PausePanel.ResetButtonPressDetected -= HandlePausePanelResetButtonPressDetected;
			CubeCrystallonEntity.CubeCompleteDetected -= HandleCubeCrystallonEntityCubeCompleteDetected;
			CardManager.Instance.CardSpawned -= HandleCardManagerInstanceCardSpawned;
//			if(GameScene.currentLevel == 999) {
//				this.Unschedule(CalculateTimer);
//			}
			
			_nextLevelPanel = null;
			HitMeButton = null;
			PauseButton = null;
			pausePanel = null;
			levelTitle = null;
			_messagePanel = null;
			_scene = null;
			
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
		
//		private void CalculateTimer(float dt) {
//			var oldMinutes = TimerMinutesText.Text;
//			var minutes = System.Math.Floor(DisplayTimer/60.0f);
//			var seconds = DisplayTimer - (60.0f * minutes);
//			
//			TimerMinutesText.Text = minutes.ToString("00");
//			if (TimerMinutesText.Text != oldMinutes) {
//				var minutesOffset = 97.0f - 3.0f - FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(TimerMinutesText.Text);
//				TimerMinutesText.Position = new Vector2(minutesOffset, -4.0f);
//			}
//			TimerSecondsText.Text = seconds.ToString("00.0");
//		}
		
		private void CalculateTimer(float dt) {
//			DisplayTimer += dt;
			if ( DisplayTimer <= 0.0f ) {
				// DIFFICULTY INCREASE!
				LevelManager.Instance.ChangeDifficulty(1);
				DisplayTimer = 15.0f;
			} else if (DisplayTimer > 30.0f) {
				// DIFFICULTY DECREASE!
				LevelManager.Instance.ChangeDifficulty(-1);
				DisplayTimer = 0.0f;
			}
			var xscale = 200.0f/16.0f;
			
			TimeBar.Scale = new Vector2(xscale * ((30.0f-DisplayTimer)/30.0f), 1.0f);
		}
		
		public void EnableHitMeButton() {
			HitMeButton.On(   CardManager.Instance.TotalCardsInDeck > 0 
			               && CardManager.availableCards.Count < LevelManager.Instance.StandardPop + 3 
			               && !LevelManager.Instance.HitMeDisabled
			              );
		}
		
		private void Initialize() {
			_initialized = true;
			
			FontMap map = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 25, "Bold") );
			FontMap bigMap = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 44, "Bold") );
			
			pausePanel = new PausePanel(_scene);
			_scene.DialogLayer.AddChild(pausePanel);
			pausePanel.Hide();
			
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
			
			_messagePanel = new MessagePanel(){
				Offset = new Vector2(480.0f, 0.0f),
				Position = new Vector2(-400.0f, 0.0f),
				Lifetime = 0.0f
			};
			this.AddChild(_messagePanel);

			ScoreIcon = Support.SpriteUVFromFile("/Application/assets/images/handIcon.png");
			ScoreIcon.Position = new Vector2(244.0f, 16.0f);
			GameHudBar.AddChild(ScoreIcon);
			
			ScoreTitleText = new Label("score", map);
			ScoreTitleText.Position = new Vector2(287, 25.0f);
//			ScoreTitleText.Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f);
			ScoreTitleText.RegisterPalette(2);
			GameHudBar.AddChild(ScoreTitleText);
			
//			BlueBox = Support.SpriteUVFromFile("/Application/assets/images/blueBox.png");
			BlueBox = Support.UnicolorSprite("white", 255,255,255,255);
			BlueBox.Scale = new Vector2(6.25f, 4.4375f);
			BlueBox.Position = new Vector2(354.0f, 0.0f);
			BlueBox.RegisterPalette(2);
			GameHudBar.AddChild(BlueBox);
			
			ScoreText = new Label("", bigMap);
			ScoreText.Position = new Vector2(359.0f, 12.0f);
			GameHudBar.AddChild(ScoreText);
			
			CubeIcon = Support.SpriteFromFile("/Application/assets/images/stopIcon.png");
			CubeIcon.Position = new Vector2(20.0f,16.0f);
			GameHudBar.AddChild(CubeIcon);
			
			CubesTitleText = new Label("cubes", map);
			CubesTitleText.Position = new Vector2(63.0f, 25.0f);
//			CubesTitleText.Color = new Vector4( 0.89803922f, 0.0745098f, 0.0745098f, 1.0f);
			CubesTitleText.RegisterPalette(1);
			GameHudBar.AddChild(CubesTitleText);
			
//			RedBox = Support.SpriteFromFile("/Application/assets/images/redbox.png");
			RedBox = Support.UnicolorSprite("white", 255,255,255,255);
			RedBox.Position = new Vector2(130.0f, 0.0f);
			RedBox.Scale = new Vector2(6.25f, 4.4375f);
			RedBox.RegisterPalette(1);
			GameHudBar.AddChild(RedBox);
			
			CubeText = new Label("", bigMap);
			CubeText.Position = new Vector2(135.0f, 12.0f);
			GameHudBar.AddChild(CubeText);
			
			GameTimer = new TimerEntity();
			if (GameScene.currentLevel == 999) {
				GameTimer.Position = new Vector2(468.0f, 16.0f);
				GameHudBar.AddChild(GameTimer);
//				TimeBox = Support.SpriteUVFromFile("/Application/assets/images/timerIcon.png");
//				TimeBox.Position = new Vector2(468.0f, 16.0f);
//				GameHudBar.AddChild(TimeBox);
				
//				TimerSeparatorText = new Label(":", bigMap);
//				TimerSeparatorText.Position = new Vector2(97.0f, -2.0f);
//				TimeBox.AddChild(TimerSeparatorText);
//			
//				TimerSecondsText = new Label("00.0", bigMap);
//				TimerSecondsText.Position = new Vector2(107.0f, -4.0f);
//				TimeBox.AddChild(TimerSecondsText);
//			
//				TimerMinutesText = new Label("00", bigMap);
//				TimerMinutesText.Position = new Vector2(45.0f, -4.0f);
//				TimeBox.AddChild(TimerMinutesText);
				
//				TimeBar = Support.UnicolorSprite("TimeBar", 255, 0, 0, 255);
//				TimeBar.Position = new Vector2(45.0f, 0.0f);
//				TimeBox.AddChild(TimeBar);
			}
			
			PauseButton = new BetterButton(115.0f, 71.0f) {
				Text = "| |",
				Position = new Vector2(845.0f, 473.0f),
//				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			this.AddChild(PauseButton);
			PauseButton.background.RegisterPalette(1);
			PauseButton.ButtonUpAction += HandlePauseButtonButtonUpAction;
			
			HitMeButton = new BetterButton(115.0f, 71.0f) {
				Text = "+",
				Position = new Vector2(720.0f, 473.0f),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			HitMeButton.On(!LevelManager.Instance.HitMeDisabled);
			this.AddChild(HitMeButton);
			HitMeButton.background.RegisterPalette(2);
			HitMeButton.ButtonUpAction += HandleHitMeButtonButtonUpAction;
			
			
		}

		
		
		public void Reset () {
#if DEBUG
			Console.WriteLine("GameSceneHud.Reset()");
#endif
			Score = 0;
			Cubes = 0;
			_displayScore = 0;
//			DisplayTimer = 0.0f;
			MetGoalTime = 0.0f;
			NoMatchesPossibleTime = 0.0f;
			_updateTimer = 0.0f;
			BreaksDetected = 0;
			HitMesDetected = 0;
			
			ExitCode = LevelExitCode.NULL;
			
			GameTimer.Reset();
			ScoreText.Text = _displayScore.ToString();
			float x = BlueBox.Position.X + 50.0f - 0.5f * FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(ScoreText.Text);
			ScoreText.Position = new Vector2(x, ScoreText.Position.Y);
			
//			if(GameScene.currentLevel != 999) {
				CubeText.Text = "0";
				x = RedBox.Position.X + 50.0f - 0.5f * FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(CubeText.Text);
				CubeText.Position = new Vector2(x, CubeText.Position.Y);
//			}
			
			UpdateMessagePanel( LevelManager.Instance.MessageTitle, LevelManager.Instance.MessageBody );
			EnableHitMeButton();
			
//			_messagePanel.Text = LevelManager.Instance.MessageBody;
//			_messagePanel.TitleText = LevelManager.Instance.MessageTitle;
//			if (_messagePanel.Text != "" || _messagePanel.TitleText != "") {
//				_messagePanel.SlideIn();
//			}
		}
		
		public void UpdateMessagePanel( string pTitle, string pMessage, float? pDismissDelay=null, float? pLifetime=null ) {
			_messagePanel.DismissDelay = pDismissDelay ?? 1.0f;
			_messagePanel.Lifetime = pLifetime ?? 0.0f;
			_messagePanel.Text = pMessage;
			_messagePanel.TitleText = pTitle;
			if (_messagePanel.Text != "" || _messagePanel.TitleText != "") {
				_messagePanel.SlideIn();
			}
		}
		
		public void ScheduleScoreModifier( int pHowMuch ) {
			if (Score == _displayScore) { // -------------- Throw a little delay on score display update if not currently updating.
				_updateTimer = -INITIAL_SCORE_UPDATE_DELAY;
			}
			Score += pHowMuch;
		}
		
		public void MetGoal() {
			MetGoalTime = GameTimer.LevelTimer;
			Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "levelcomplete.wav");
			_nextLevelPanel.Populate( Cubes, Score );
			_nextLevelPanel.SlideIn();
			GameTimer.Pause(true);
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

