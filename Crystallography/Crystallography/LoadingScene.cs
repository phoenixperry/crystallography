using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class LoadingScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{
		readonly TimeSpan minProcTime = new TimeSpan(0, 0, 0, 0, 30);
		List<Action> loadProc;
		Stopwatch stopwatch;
		int _loadIndex;
		
		GameScene _gameScene;
		
		const float TWO_PI = 6.2831853f;
		
		protected int _levelNumber;
		protected float _timer;
		protected float _angle;
		protected bool _timed;
		
		SpriteTile LoadingSpinner;
		Node Hub;
		Label LoadingText;
		
		// CONSTRUCTOR ---------------------------------------------------------------------------------------
		
		public LoadingScene (int pLevelNumber, bool pTimed=false) {
			this.Camera.SetViewFromViewport();
			
			stopwatch = Stopwatch.StartNew();
			
			_levelNumber = pLevelNumber;
			_timed = pTimed;
			_angle = 0.0f;
			_timer = 0.0f;
			_loadIndex = 0;
			
			Hub = new Node();
			Hub.Position = new Vector2(480.0f, 272.0f);
			this.AddChild(Hub);
			
			LoadingSpinner = Support.SpriteFromFile("/Application/assets/images/UI/loading.png");
			LoadingSpinner.Pivot = new Vector2(0.5f, 0.5f);
			LoadingSpinner.Position = new Vector2(-145.0f, -119.0f);
			Hub.AddChild(LoadingSpinner);
			
			LoadingText = new Label("loading", UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold")));
			LoadingText.Position = new Vector2(445.0f, 257.0f);
			this.AddChild(LoadingText);
			
			
			loadProc = new List<Action>{
				() => {
					;//dummy
				},
				// PRE-LOAD LEVEL DATA
				() => {
					LevelManager.Instance.LoadGameData();
				},
				// PRE-LOAD IMAGES
				() => {
					var temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18, "Regular");
					temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold");
					temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold");
					temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 102, "Bold");
				},
				() => {
					var temp = Support.TiledSpriteFromFile("Application/assets/images/next_level_sm.png", 1, 3);
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/GameHudBar.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/handIcon.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/blueBox.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/stopIcon.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/redbox.png");
				},
				() => {
					if (_levelNumber != 999) {
						var temp = Support.TiledSpriteFromFile("Application/assets/images/restartBtn.png", 1, 3);
					} else {
						var temp = Support.SpriteUVFromFile("/Application/assets/images/timerIcon.png");
					}
				},
				() => {
					var temp = Support.TiledSpriteFromFile("Application/assets/images/hitMe.png", 1, 3);
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/PausePanelBG.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/LevelTitleBG.png");
				},
				() => {
					var temp = Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2);
				},
				// PREPARE GAME SCENE
				() => {
					_gameScene = new GameScene(_levelNumber, _timed);
				}
				
			};
			
			Scheduler.Instance.ScheduleUpdateForTarget(this, 0, false);
		}
		
		// OVERRIDES ----------------------------------------------------------------------------------------
		
		public override void OnExit ()
		{
			base.OnExit ();
			loadProc.Clear();
			loadProc = null;
			stopwatch.Stop();
			stopwatch = null;
			_gameScene = null;
			Hub.RemoveAllChildren(true);
			Hub = null;
			LoadingSpinner = null;
		}
		
		public override void Update (float dt)
		{
			base.Update (dt);
			
			var totalProcTime = new TimeSpan();
			
			if( _timer > 0.5f ) {
				do {
					var start = stopwatch.Elapsed;
					loadProc[_loadIndex++]();
					var procTime = stopwatch.Elapsed - start;
					totalProcTime += procTime;
				} while ( totalProcTime < minProcTime && _loadIndex < loadProc.Count );
			}
			
			_angle += dt * -TWO_PI;
			if (_angle > 360.0) {
				_angle -= 360.0f;
			}
			
			Hub.Angle = _angle;
			_timer += dt;
			if (_loadIndex >= loadProc.Count) {
				Director.Instance.ReplaceScene( _gameScene );
			}
		}
		
		// METHODS ------------------------------------------------------------------------------------------
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------
#if DEBUG
		~LoadingScene() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

