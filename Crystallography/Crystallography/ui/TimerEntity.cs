using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class TimerEntity : Layer
	{
		SpriteTile TimerIcon;
		SpriteTile TimeBar;
		SpriteTile LeftEnd;
		SpriteTile RightEnd;
		
		protected bool _intialized = false;
		protected bool _pauseTimer = false;
		protected float _barAdjustment;
		
		public event EventHandler BarFilled;
		public event EventHandler BarEmptied;
		
		// GET & SET -------------------------------------------------------
		
		public float DisplayTimer {get; private set;}
		public float LevelTimer {get; private set;}
		public float AbsoluteTimer {get; private set;}
		
		// CONSTRUCTORS ----------------------------------------------------
		
		public TimerEntity (bool pStartPaused = true) : base() {
			if ( false == _intialized ) {
				Initialize(pStartPaused);
			}
		}
		
		// EVENT HANDLERS --------------------------------------------------
		
		void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e)
		{
			// TODO SOMETHING SPECIFIC TO THE CURRENT DIFFICULTY LEVEL IN INFINITE MODE
			if (GameScene.currentLevel == 999) {
				_barAdjustment += 10.0f;
			}
		}
		
		void HandleBarFilled (object sender, EventArgs e) {
			_barAdjustment = 0.0f;
//			DisplayTimer = 15.0f;
		}
		
		void HandleBarEmptied (object sender, EventArgs e) {
			
			
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
			AbsoluteTimer += dt;
			if (GameScene.paused == false && _pauseTimer == false ) {
				if (_barAdjustment > 0.0f) {
					_barAdjustment -= 5.0f * dt;
					if ( _barAdjustment < 0.0f ) {
						_barAdjustment = 0.0f;
					}
					DisplayTimer -= 5.0f * dt;
				} else {
					DisplayTimer += dt;
				}
				LevelTimer += dt;
				UpdateBar();
			}
		}
		
		// METHODS ---------------------------------------------------------
		
		private void Initialize(bool pStartPaused) {
			_intialized = true;
			_pauseTimer = pStartPaused;
			AbsoluteTimer = 0.0f;
			
			TimerIcon = Support.TiledSpriteFromFile("/Application/assets/images/timerIcon.png", 1, 1);
			TimerIcon.RegisterPalette(0);
			this.AddChild(TimerIcon);
			
			TimeBar = Support.UnicolorSprite( "white", 255, 255, 255, 255);
			TimeBar.Position = new Vector2(45.0f, 8.0f);
			TimeBar.RegisterPalette(0);
			TimerIcon.AddChild(TimeBar);
			
			LeftEnd = Support.TiledSpriteFromFile("/Application/assets/images/timerEnd.png", 1, 1);
			LeftEnd.FlipU = true;
			RightEnd = Support.TiledSpriteFromFile("/Application/assets/images/timerEnd.png", 1, 1);
			LeftEnd.Color = RightEnd.Color = LevelManager.Instance.BackgroundColor;
			LeftEnd.Position = TimeBar.Position;
			RightEnd.Position = new Vector2(TimeBar.Position.X + 16.0f * TimeBar.Scale.X - 16.0f, TimeBar.Position.Y);
			this.AddChild(LeftEnd);
			this.AddChild(RightEnd);
			
			Reset ();
			UpdateBar();
		}
		
		public void Pause( bool pOn=true ) {
			_pauseTimer = pOn;
		}
		
		public void Reset() {
			_barAdjustment = 0.0f;
			DisplayTimer = 0.001f;
			LevelTimer = 0.0f;
		}
		
		public void SetDisplayTimer( float pTime ) {
			DisplayTimer = pTime;
		}
		
		protected void UpdateBar() {
			if ( DisplayTimer <= 0.0f ) {	// ------------------ BAR FILLED
				EventHandler handler = BarFilled;
				if (handler != null) {
					handler( this, null );
				}
			} else if (DisplayTimer > 30.0f) {	// ------------- BAR EMPTIED
				EventHandler handler = BarEmptied;
				if (handler != null) {
					handler( this, null );
				}
			}
			var xscale = 300.0f/16.0f;
			
			TimeBar.Scale = new Vector2(xscale * ((30.0f-DisplayTimer)/30.0f), 1.0f);
			
			RightEnd.Position = new Vector2(TimeBar.Position.X + 16.0f * TimeBar.Scale.X - 16.0f, TimeBar.Position.Y);
		}
	}
}

