using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class GameSceneHud : Layer
	{
		public static readonly Vector2 SCORE_TEXT_POS = new Vector2(277.0f, 12.0f);
		public static readonly Vector2 CUBES_TEXT_POS = new Vector2(117.0f, 12.0f);
		
		Node GameHudBar;
		SpriteTile HudBarMask;
		SpriteTile HudBarLine;
		SpriteTile ScoreIcon;
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
		BonusTimer BonusBar;
		public StrikeHud Strikes;
		
		public LevelTitleMkTwo levelTitle;
		public MessagePanel _messagePanel;
		public HudPanel _nextLevelPanel;
		
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
		public bool Initialized {get {return _initialized;} }
		
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
			NoMatchesPossibleTime = GameTimer.LevelTimer;
			MetGoal();
			GameTimer.Pause(true);
		}
		
		/// <summary>
		/// On Level Change
		/// </summary>
		void HandleGameSceneLevelChangeDetected (object sender, EventArgs e) {
			Reset ();
//			levelTitle.SetLevelText(GameScene.currentLevel);
//			levelTitle.Title = GameScene.currentLevel;
			List<string> variables = new List<string>();
			foreach (string key in QualityManager.Instance.qualityDict.Keys) {
				if ( key == "QOrientation") continue;
				var variations = QualityManager.Instance.qualityDict[key];
				if( variations[0] != null && variations[1] != null && variations[2] != null ) {
					variables.Add(key.Substring(1));
				}
			}
			levelTitle.SetQualityNames( variables.ToArray() );
			switch(GameScene.currentLevel) {
			case 0:
				levelTitle.Title = "crystallon tutorial";
				break;
			case 999:
				levelTitle.Title = "crystallon challenge mode";
				if(GameScene.gameTimeLimit > 0.0f) {
					float minutes = GameScene.gameTimeLimit/60.0f;
					levelTitle.Title += ": " + minutes.ToString("#0.#") + " minute limit";
				}
				break;
			default:
				levelTitle.Title = "crystallon puzzle " + GameScene.currentLevel.ToString();
				break;
			}
			levelTitle.SlideIn();
		}
		
		/// <summary>
		/// On Hit Me Button Up
		/// </summary>
		void HandleHitMeButtonButtonUpAction (object sender, EventArgs e) {
			if(GameScene.currentLevel == 999) {
				if(CardManager.availableCards.Count < LevelManager.Instance.StandardPop) {
					if(CardManager.Instance.MatchesPossible()) {
						Strikes.Despair();
					} else {
						Strikes.Hope();
					}
				}
			}
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
			_messagePanel.SlideOut();
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
			GameTimer.Pause(false);
			
			if (GameScene.currentLevel == 999 && BonusBar != null) {
				BonusBar.Pause(false);
			}
		}
		
		/// <summary>
		/// On Match Score Detected
		/// </summary>
		void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e) {
			Cubes++;
			CubeText.Text = Cubes.ToString();
			CenterCubeScoreText(CubeText, CUBES_TEXT_POS);
			
			EventHandler handler = CubesUpdated;
			if ( handler != null ) {
				handler( this, null );
			}
			
			ScheduleScoreModifier( e.Points );
			new ScorePopup( e.Node, e.Points );
			
//			Strikes.Hope();
		}
		
		/// <summary>
		/// On Failed Match Detected
		/// </summary>
		void HandleQualityManagerFailedMatchDetected (object sender, FailedMatchEventArgs e)
		{
			IconPopupManager.Instance.FailedIcons( e.Entity, FailedMatchEventArgs.Names );
			Strikes.Despair();
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
			_messagePanel.SlideOut();
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
		
		void HandleLevelTitleOnSlideOutComplete (object sender, EventArgs e) {
			UpdateMessagePanel( LevelManager.Instance.MessageTitle, LevelManager.Instance.MessageBody );
//			EnableHitMeButton();
			CardManager.Instance.Populate();
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
		
		void HandleGameTimerBarFilled (object sender, EventArgs e)
		{
			
			
		}

		void HandleGameTimerBarEmptied (object sender, EventArgs e)
		{
//			if (LevelManager.Instance.difficulty > LevelManager.MIN_DIFFICULTY) {
				// DIFFICULTY DECREASE!
//				LevelManager.Instance.ChangeDifficulty(-1);
//				GameTimer.SetDisplayTimer(0.01f, false);
//			} else {
				// BOTTOM OUT -- GAME OVER
//				MetGoal();
//			}
//			Strikes.Despair();
			if(GameScene.gameTimeLimit > 0.0f) {
				// TIMED MODE TIMER ELAPSED
				MetGoal();
			}
			GameTimer.SetDisplayTimer(0.01f, false);
			GameTimer.Pause(true);
//			BonusBar.Pause(true);
		}
		
		void HandleBonusBarBarFilled (object sender, EventArgs e)
		{
			// DIFFICULTY INCREASE!
			LevelManager.Instance.ChangeDifficulty(1);
			BonusBar.increaseDifficulty();
			Strikes.Hope();
//			float bonusTime = 15.0f + 5.0f * LevelManager.Instance.difficulty;
//
//			if (bonusTime > GameTimer.DisplayTimer) {
//				GameTimer.MaxTime += bonusTime - GameTimer.DisplayTimer;
//			}
//			var time = Sce.PlayStation.Core.FMath.Max(0.01f, GameTimer.DisplayTimer - (bonusTime));
//			GameTimer.SetDisplayTimer(time, false);
		}
		
		void HandleBonusBarBarEmptied (object sender, EventArgs e)
		{
			
		}

		
		void HandleStrikesStrikeBarFailure (object sender, EventArgs e)
		{
			// BOTTOM OUT -- GAME OVER
			MetGoal();
//			Strikes.Reset();
		}

		void HandleStrikesStrikeBarSuccess (object sender, EventArgs e)
		{
			// DIFFICULTY INCREASE!
			LevelManager.Instance.ChangeDifficulty(1);
			GameTimer.SetDisplayTimer(0.01f, false);
			Strikes.Reset();

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
//					float x = BlueBox.Position.X + 50.0f - 0.5f * FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(ScoreText.Text);
//					float x = 287.0f + 50.0f - 0.5f * FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(ScoreText.Text);
//					ScoreText.Position = new Vector2(x, ScoreText.Position.Y);
					CenterCubeScoreText(ScoreText, SCORE_TEXT_POS);
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
			
			levelTitle.OnSlideOutComplete += HandleLevelTitleOnSlideOutComplete;
			QualityManager.MatchScoreDetected += HandleQualityManagerMatchScoreDetected;
			QualityManager.FailedMatchDetected += HandleQualityManagerFailedMatchDetected;
			CardManager.Instance.NoMatchesPossibleDetected += HandleCardManagerInstanceNoMatchesPossibleDetected;
			GameScene.LevelChangeDetected += HandleGameSceneLevelChangeDetected;
			GroupCrystallonEntity.BreakDetected += HandleGroupCrystallonEntityBreakDetected;
			PausePanel.QuitButtonPressDetected += HandlePausePanelQuitButtonPressDetected;
			PausePanel.ResetButtonPressDetected += HandlePausePanelResetButtonPressDetected;
			CubeCrystallonEntity.CubeCompleteDetected += HandleCubeCrystallonEntityCubeCompleteDetected;
			CardManager.Instance.CardSpawned += HandleCardManagerInstanceCardSpawned;
			if(GameScene.currentLevel == 999) {
				GameTimer.BarEmptied += HandleGameTimerBarEmptied;
				GameTimer.BarFilled += HandleGameTimerBarFilled;
				if(BonusBar != null) {
					BonusBar.BarFilled += HandleBonusBarBarFilled;
					BonusBar.BarEmptied += HandleBonusBarBarEmptied; 
				}
				Strikes.StrikeBarSuccess += HandleStrikesStrikeBarSuccess;
				Strikes.StrikeBarFailure += HandleStrikesStrikeBarFailure;
				(_nextLevelPanel as InfiniteModeEndPanel).RetryDetected += HandlePausePanelResetButtonPressDetected;
				(_nextLevelPanel as InfiniteModeEndPanel).QuitDetected += Handle_nextLevelPanelQuitButtonPressDetected;
			} else {
				(_nextLevelPanel as NextLevelPanel).ReplayDetected += HandlePausePanelResetButtonPressDetected;
				(_nextLevelPanel as NextLevelPanel).NextLevelDetected += Handle_nextLevelPanelButtonButtonUpAction;
				(_nextLevelPanel as NextLevelPanel).QuitDetected += Handle_nextLevelPanelQuitButtonPressDetected;
				(_nextLevelPanel as NextLevelPanel).LevelSelectDetected += Handle_nextLevelPanelLevelSelectDetected;
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
			if(GameScene.currentLevel == 999) {
				GameTimer.BarEmptied -= HandleGameTimerBarEmptied;
				GameTimer.BarFilled -= HandleGameTimerBarFilled;
				if (BonusBar != null) {
					BonusBar.BarFilled -= HandleBonusBarBarFilled;
					BonusBar.BarEmptied -= HandleBonusBarBarEmptied;
				}
				(_nextLevelPanel as InfiniteModeEndPanel).RetryDetected -= HandlePausePanelResetButtonPressDetected;
				(_nextLevelPanel as InfiniteModeEndPanel).QuitDetected -= Handle_nextLevelPanelQuitButtonPressDetected;
			} else {
				(_nextLevelPanel as NextLevelPanel).ReplayDetected -= HandlePausePanelResetButtonPressDetected;
				(_nextLevelPanel as NextLevelPanel).NextLevelDetected -= Handle_nextLevelPanelButtonButtonUpAction;
				(_nextLevelPanel as NextLevelPanel).QuitDetected -= Handle_nextLevelPanelQuitButtonPressDetected;
				(_nextLevelPanel as NextLevelPanel).LevelSelectDetected -= Handle_nextLevelPanelLevelSelectDetected;
			}
			
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
//			var xscale = 200.0f/16.0f;
			
//			TimeBar.Scale = new Vector2(xscale * ((30.0f-DisplayTimer)/30.0f), 1.0f);
		}
		
		public void EnableHitMeButton() {
			HitMeButton.On(   CardManager.Instance.TotalCardsInDeck > 0 
			               && CardManager.availableCards.Count < LevelManager.Instance.StandardPop + 3 
			               && !LevelManager.Instance.HitMeDisabled
			              );
		}
		
		private void Initialize() {
			FontMap map = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 25, "Bold") );
			FontMap bigMap = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 44, "Bold") );
			
			// CREATE PAUSE MENU
			pausePanel = new PausePanel(_scene);
			_scene.DialogLayer.AddChild(pausePanel);
			pausePanel.Hide();
			
			// CREATE THE HUD BAR AT THE TOP OF THE SCREEN
			GameHudBar = new Node(){
				Position = new Vector2(0.0f, 473.0f)
			};
			this.AddChild(GameHudBar);
			
			HudBarMask = Support.UnicolorSprite("white", 255, 255, 255, 255);
			HudBarMask.Color = LevelManager.Instance.BackgroundColor;
			HudBarMask.Scale = new Vector2(60.0f, 4.4375f);
			GameHudBar.AddChild(HudBarMask);
			
			HudBarLine = Support.UnicolorSprite("white", 255, 255, 255, 255);
			HudBarLine.RegisterPalette(0);
			HudBarLine.Scale = new Vector2(60.0f, 0.0625f);
			GameHudBar.AddChild(HudBarLine);
			
			// CREATE THE LEVEL TITLE HUD
			levelTitle = new LevelTitleMkTwo() {
				SlideInDirection = SlideDirection.RIGHT,
				SlideOutDirection = SlideDirection.LEFT,
				Offset = new Vector2(0.0f, 0.0f),
//				Lifetime = 4.0f
			};
			this.AddChild(levelTitle, -1);
			
			// CREATE THE END-OF-LEVEL DROP-DOWN PANEL
			if (GameScene.currentLevel == 999) {
				// INFINITE MODE VERSION
				_nextLevelPanel = new InfiniteModeEndPanel(){
					Offset = new Vector2(Director.Instance.GL.Context.Screen.Width-248.0f, 0.0f),
					
				};
			} else {
				// PUZZLE/TUTORIAL MODE VERSION
				_nextLevelPanel = new NextLevelPanel(){
					Offset = new Vector2(510.0f, 0.0f),
					
				};
			}
			GameHudBar.AddChild(_nextLevelPanel, -1);
			
			// CREATE MESSAGE PANEL
			_messagePanel = new MessagePanel(920.0f, 148.0f ){
				SourceObject = this,
				Offset = new Vector2(20.0f, 0.0f),
				Position = new Vector2(0.0f, -148.0f),
				Lifetime = 0.0f
			};
			this.AddChild(_messagePanel);
			_messagePanel.body = GamePhysics.Instance.RegisterPhysicsBody(GamePhysics.Instance.SceneShapes[4], float.MaxValue, 0.0f, _messagePanel.Position / GamePhysics.PtoM);
			_messagePanel.AdHocDraw += () => {
				_messagePanel.body.Position = _messagePanel.Position / GamePhysics.PtoM;
			};
			
			// SCORE STUFF
			ScoreIcon = Support.SpriteFromFile("/Application/assets/images/handIcon.png");
			ScoreIcon.Position = new Vector2(184.0f, 16.0f);
			ScoreIcon.RegisterPalette(1);
			GameHudBar.AddChild(ScoreIcon);
			
			ScoreTitleText = new Label("score", map);
			ScoreTitleText.Position = new Vector2(287, 25.0f);
			ScoreTitleText.RegisterPalette(1);
			
			BlueBox = Support.UnicolorSprite("white", 255,255,255,255);
			BlueBox.Scale = new Vector2(6.25f, 4.4375f);
			BlueBox.Position = new Vector2(354.0f, 0.0f);
			BlueBox.RegisterPalette(2);
			
			ScoreText = new Label("", bigMap) {
				Position = SCORE_TEXT_POS
			};
			ScoreText.RegisterPalette(1);
			GameHudBar.AddChild(ScoreText);
			
			// CUBE STUFF
			CubeIcon = Support.SpriteFromFile("/Application/assets/images/stopIcon.png");
			CubeIcon.Position = new Vector2(20.0f,16.0f);
			CubeIcon.RegisterPalette(2);
			GameHudBar.AddChild(CubeIcon);
			
			CubesTitleText = new Label("cubes", map);
			CubesTitleText.Position = new Vector2(63.0f, 25.0f);
			CubesTitleText.RegisterPalette(2);
			
			RedBox = Support.UnicolorSprite("white", 255,255,255,255);
			RedBox.Position = new Vector2(130.0f, 0.0f);
			RedBox.Scale = new Vector2(6.25f, 4.4375f);
			RedBox.RegisterPalette(1);
			
			CubeText = new Label("", bigMap){
				Position = CUBES_TEXT_POS
			};
			CubeText.RegisterPalette(2);
			GameHudBar.AddChild(CubeText);
			
			// TIMER & STRIKES STUFF
			GameTimer = new TimerEntity();
			if (GameScene.currentLevel == 999) {	// ------------------- IF CHALLENGE MODE
				if(GameScene.gameTimeLimit > 0.0f) {
					GameTimer.Position = new Vector2(348.0f, 16.0f);	// ----- ADD THE TIME BAR
					GameHudBar.AddChild(GameTimer);
				} else {
					BonusBar = new BonusTimer() {
//						Position = new Vector2(348.0f, 44.0f)
						Position = new Vector2(348.0f, 16.0f)
					};
					GameHudBar.AddChild(BonusBar);
				}
				
				Strikes = new StrikeHud() {
					Position = new Vector2(395.0f, 44.0f)
				};
				GameHudBar.AddChild(Strikes);
			}
			
			// PAUSE BUTTON
			PauseButton = new BetterButton("/Application/assets/images/UI/BetterButtonTransparent.png", 115.0f, 71.0f) {
				Text = "",
				Icon = Support.SpriteFromFile("Application/assets/images/UI/pause.png"),
				Position = new Vector2(845.0f, 473.0f),
			};
			this.AddChild(PauseButton);
			PauseButton.background.RegisterPalette(2);
			PauseButton.background.Scale = new Vector2(115.0f/16.0f, 71.0f/16.0f);
			PauseButton.Icon.RegisterPalette(2);
			PauseButton.ButtonUpAction += HandlePauseButtonButtonUpAction;
			
			// HIT ME BUTTON
			HitMeButton = new BetterButton("/Application/assets/images/UI/BetterButtonTransparent.png", 115.0f, 71.0f) {
				Text = "",
				Icon = Support.SpriteFromFile("Application/assets/images/UI/plus.png"),
				Position = new Vector2(720.0f, 473.0f),
			};
			HitMeButton.On(!LevelManager.Instance.HitMeDisabled);
			this.AddChild(HitMeButton);
			HitMeButton.background.RegisterPalette(1);
			HitMeButton.background.Scale = new Vector2(115.0f/16.0f, 71.0f/16.0f);
			HitMeButton.Icon.RegisterPalette(1);
			HitMeButton.ButtonUpAction += HandleHitMeButtonButtonUpAction;
			
			_initialized = true;
		}

		
		
		public void Reset () {
#if DEBUG
			Console.WriteLine("GameSceneHud.Reset()");
#endif
			Score = 0;
			Cubes = 0;
			_displayScore = 0;
			MetGoalTime = 0.0f;
			NoMatchesPossibleTime = 0.0f;
			_updateTimer = 0.0f;
			BreaksDetected = 0;
			HitMesDetected = 0;
			
			
			ExitCode = LevelExitCode.NULL;
			
			if (GameScene.currentLevel == 999) {
				Strikes.Reset();
				if(BonusBar != null) {
					BonusBar.Reset();
					BonusBar.Pause(true);
				}
			}
			
			GameTimer.Reset();
			GameTimer.Pause(true);
			ScoreText.Text = _displayScore.ToString();
			CenterCubeScoreText(ScoreText, SCORE_TEXT_POS);
			
			CubeText.Text = "0";
			CenterCubeScoreText(CubeText, CUBES_TEXT_POS);
		}
		
		public void SetGameTimeLimit(float pTime) {
			GameTimer.MaxTime = pTime;
		}
		
		private void CenterCubeScoreText(Label pLabel, Vector2 pAnchor) {
			float x = pAnchor.X - 0.5f * FontManager.Instance.GetInGame("Bariol", 44, "Bold").GetTextWidth(pLabel.Text);
			pLabel.Position = new Vector2(x, pAnchor.Y);
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
			GameScene.SG.LetGo(true);
			MetGoalTime = GameTimer.LevelTimer;
			Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "levelcomplete.wav");
			if (GameScene.currentLevel != 999) {
				(_nextLevelPanel as NextLevelPanel).Populate( Cubes, Score );
			}
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

