using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class BonusTimer : TimerEntity
	{
		private static readonly float DEFAULT_CUBE_LEVELUP_REQUIREMENT = 5.0f;
		
		protected float cubeGoal;
		
		protected SpriteTile TimeBar2;
		
		public BonusTimer () : base() {
//			this.TimerIcon.TextureInfo = Support.SpriteFromFile("Application/assets/images/UI/strikeCube.png").TextureInfo;
//			TimerIcon.Quad.S = new Vector2(TimerIcon.TextureInfo.Texture.Width, TimerIcon.TextureInfo.Texture.Height);
		}
		
		
		public void increaseDifficulty() {
			cubeGoal = LevelManager.Instance.difficulty * DEFAULT_CUBE_LEVELUP_REQUIREMENT;
		}
		
		protected override void HandleQualityManagerMatchScoreDetected (object sender, MatchScoreEventArgs e)
		{
			if (GameScene.currentLevel == 999) {
				_barAdjustment += this._maxTime / (cubeGoal-1.0f);
			}
		}
		
		protected override void HandleBarFilled (object sender, EventArgs e)
		{
			if(this._activeTimeBar == TimeBar)
				this._activeTimeBar = TimeBar2;
			else
				this._activeTimeBar = TimeBar;
			_barAdjustment = _maxTime - _maxTime * (1.0f - 1.0f/X_SCALE);
			_activeTimeBar.Parent.Children.Reverse();
			DisplayTimer = _maxTime;
		}
		
		protected override void HandleBarEmptied (object sender, EventArgs e)
		{
			if(this._activeTimeBar == TimeBar)
				this._activeTimeBar = TimeBar2;
			else
				this._activeTimeBar = TimeBar;
			
			DisplayTimer = _maxTime-0.01f;
		}
		
		protected override void UpdateTimer (float dt)
		{
			if(_barAdjustment > 0.0f || (DisplayTimer < (_maxTime * (1.0f - 1.0f/X_SCALE))) )
				base.UpdateTimer (dt);
		}
		
		protected override void Initialize (bool pStartPaused)
		{
			TimeBar2 = Support.UnicolorSprite( "white", 255, 255, 255, 255);
			
			base.Initialize (pStartPaused);
			
			TimeBar2.Position = TimeBar.Position;
			TimeBar.RegisterPalette(2);
			TimeBar2.RegisterPalette(1);
			TimerIcon.AddChild(TimeBar2);
			_activeTimeBar.Parent.Children.Reverse();
		}
		
		public override void Reset ()
		{
			base.Reset ();
			this._timerSpeed = 0.3f;
			DisplayTimer = _maxTime * (1.0f - 1.0f/X_SCALE);
			
//			_maxTime * (1 - ( 1 - 1/X_SCALE));
			
			cubeGoal = DEFAULT_CUBE_LEVELUP_REQUIREMENT;
			
//			_activeTimeBar = TimeBar;
			TimeBar.Scale = new Vector2(X_SCALE * (_maxTime - DisplayTimer)/_maxTime, 1.0f);
			TimeBar2.Scale = new Vector2(X_SCALE * (_maxTime - DisplayTimer)/_maxTime, 1.0f);
		}
		
//		protected override void UpdateBar (SpriteTile bar)
//		{
//			if ( DisplayTimer <= 0.0f ) {	// ------------------ BAR FILLED
//				EventHandler handler = BarFilled;
//				if (handler != null) {
//					handler( this, null );
//				}
//			} else if (DisplayTimer > _maxTime) {	// ------------- BAR EMPTIED
//				EventHandler handler = BarEmptied;
//				if (handler != null) {
//					handler( this, null );
//				}
//			}
//			
//			bar.Scale = new Vector2(X_SCALE * ((_maxTime-DisplayTimer)/_maxTime), 1.0f);
//			
//			RightEnd.Position = new Vector2(bar.Position.X + 16.0f * bar.Scale.X - 16.0f, bar.Position.Y);
//		}
	}
}

