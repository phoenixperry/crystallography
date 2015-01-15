using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class TimerEntity : Layer
	{
		public static float BASE_SPRITE_WIDTH = 16.0f;
		protected static float X_SCALE = 300.0f/16.0f;
		
		
		private static string TIMER_ICON_PATH = "/Application/assets/images/timerIcon.png";
		private static string TIMER_END_PATH = "/Application/assets/images/timerEnd.png";
		protected static float MAX_TIME_DEFAULT = 60.0f;
		
		protected float _timerFillSpeed = 5.0f;
		protected float _timerFillAcceleration = 2.0f;
		protected float _timerSpeed = 1.0f;
		
		protected SpriteTile TimerIcon;
		protected SpriteTile TimeBar;
		protected SpriteTile _activeTimeBar;
		protected SpriteTile LeftEnd;
		protected SpriteTile RightEnd;
		
		protected bool _intialized = false;
		protected bool _pauseTimer = false;
		protected float _barAdjustment;
		protected float _barAdjustmentTimer;
		
		protected float _maxTime;
		protected float _maxTimeStart;
		
		public event EventHandler BarFilled;
		public event EventHandler BarEmptied;
		
		// GET & SET -------------------------------------------------------
		
		public float DisplayTimer {get; protected set;}
		public float LevelTimer {get; private set;}
		public float AbsoluteTimer {get; private set;}
		
		public float MaxTime {get {return _maxTime;}
							  set {
										if(value>0.0f) {
											_maxTime = value;
											_maxTimeStart = value;
										}
								  }
							  }
		
		// CONSTRUCTORS ----------------------------------------------------
		
		public TimerEntity (bool pStartPaused = true) : base() {
			if ( false == _intialized ) {
				Initialize(pStartPaused);
			}
		}
		
		// EVENT HANDLERS --------------------------------------------------
		
		protected virtual void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e)
		{
			// TODO SOMETHING SPECIFIC TO THE CURRENT DIFFICULTY LEVEL IN INFINITE MODE
			if (GameScene.currentLevel == 999) {
//				_barAdjustment += 10.0f;
			}
		}
		
		protected virtual void HandleBarFilled (object sender, EventArgs e) {
			_barAdjustment = 0.0f;
//			DisplayTimer = 15.0f;
		}
		
		protected virtual void HandleBarEmptied (object sender, EventArgs e) {
			
			
//			DisplayTimer = 0.0f;
		}
		
		// OVERRIDES -------------------------------------------------------
		
		public override void OnEnter () {
			base.OnEnter();
			QualityManager.MatchScoreDetected += HandleQualityManagerMatchScoreDetected;
			BarFilled += HandleBarFilled;
			BarEmptied += HandleBarEmptied;
		}
		
		public override void OnExit () {
			base.OnExit();
			QualityManager.MatchScoreDetected -= HandleQualityManagerMatchScoreDetected;
			BarFilled -= HandleBarFilled;
			BarEmptied -= HandleBarEmptied;
		}
		
		public override void Update (float dt) {
			if ( false == _intialized ) {
				return;
			}
			
			base.Update (dt);
			AbsoluteTimer += dt;	// --------------------- UPDATE TOTAL-TIME-IN-GAME TIMER
			
			if (GameScene.paused == false && _pauseTimer == false ) { // ----- IF UNPAUSED...
				UpdateTimer(dt);
				LevelTimer += dt; // ----------------------- UPDATE TOTAL-TIME-SPENT-IN-LEVEL TIMER
			}
		}
		
		// METHODS ---------------------------------------------------------
		
		protected virtual void UpdateTimer(float dt) {
			if (_barAdjustment > 0.0f) {	// ---------- IF ADDING BONUS TIME
				_barAdjustmentTimer += _timerFillAcceleration * dt;
				float delta = (_timerFillSpeed + _timerFillSpeed * _barAdjustmentTimer) * dt;
				
				if(_barAdjustment - delta < 0) {	//----- IF THIS IS THE FINAL UPDATE TO ADD BONUS TIME
					delta = _barAdjustment;
					_barAdjustmentTimer = 0.0f;
				}
				_barAdjustment -= delta;
				DisplayTimer -= delta;	// --------------- LENGTHEN TIMER
			} else {	// ----------------------------- ELSE
				DisplayTimer += dt * _timerSpeed;	// --- SHORTEN TIMER
			}
			UpdateBar(_activeTimeBar);
		}
		
		protected virtual void Initialize(bool pStartPaused) {
			_intialized = true;
			_pauseTimer = pStartPaused;
			AbsoluteTimer = 0.0f;
			_maxTimeStart = MAX_TIME_DEFAULT;
			
			TimerIcon = Support.TiledSpriteFromFile(TIMER_ICON_PATH, 1, 1);
			TimerIcon.RegisterPalette(0);
			this.AddChild(TimerIcon);
			
			TimeBar = Support.UnicolorSprite( "white", 255, 255, 255, 255);
			TimeBar.Position = new Vector2(45.0f, 0.0f);
			TimeBar.RegisterPalette(0);
			TimerIcon.AddChild(TimeBar);
			
			LeftEnd = Support.TiledSpriteFromFile(TIMER_END_PATH, 1, 1);
			LeftEnd.FlipU = true;
			RightEnd = Support.TiledSpriteFromFile(TIMER_END_PATH, 1, 1);
			LeftEnd.Color = RightEnd.Color = LevelManager.Instance.BackgroundColor;
			LeftEnd.Position = TimeBar.Position;
			RightEnd.Position = new Vector2(TimeBar.Position.X + BASE_SPRITE_WIDTH * TimeBar.Scale.X - BASE_SPRITE_WIDTH, TimeBar.Position.Y);
			this.AddChild(LeftEnd);
			this.AddChild(RightEnd);
			
			Reset ();
			UpdateBar(_activeTimeBar);
		}
		
		public void Pause( bool pOn=true ) {
			_pauseTimer = pOn;
		}
		
		public virtual void Reset() {
			_activeTimeBar = TimeBar;
			_barAdjustment = 0.0f;
			_barAdjustmentTimer = 0.0f;
			DisplayTimer = 0.001f;
			LevelTimer = 0.0f;
			_maxTime = _maxTimeStart;
		}
		
		public void SetDisplayTimer( float pTime, bool instant=true ) {
			if(instant) {
				DisplayTimer = pTime;
			} else {
//				DisplayTimer = _maxTime;
//				_barAdjustment += (_maxTime - pTime);
				_barAdjustment += (DisplayTimer - pTime);
			}
		}
		
		protected virtual void UpdateBar(SpriteTile bar) {
			if ( DisplayTimer <= 0.0f ) {	// ------------------ BAR FILLED
				EventHandler handler = BarFilled;
				if (handler != null) {
					handler( this, null );
				}
			} else if (DisplayTimer > _maxTime) {	// ------------- BAR EMPTIED
				EventHandler handler = BarEmptied;
				if (handler != null) {
					handler( this, null );
				}
			}
			
			bar.Scale = new Vector2(X_SCALE * ((_maxTime-DisplayTimer)/_maxTime), 1.0f);
			
			RightEnd.Position = new Vector2(bar.Position.X + BASE_SPRITE_WIDTH * bar.Scale.X - BASE_SPRITE_WIDTH, bar.Position.Y);
		}
	}
}

