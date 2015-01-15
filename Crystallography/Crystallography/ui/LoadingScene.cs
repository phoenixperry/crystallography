using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI
{
	public class LoadingScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{
		readonly TimeSpan minProcTime = new TimeSpan(0, 0, 0, 0, 30);
		readonly GameSceneData DEFAULT_GAME_SCENE_DATA = new GameSceneData(){level=0, timeLimit=0.0f, fourthQuality="none"};
		public static GameSceneData GAME_SCENE_DATA = new GameSceneData();
		List<Action> loadProc;
		Stopwatch stopwatch;
		int _loadIndex;
		
		Scene _scene;
		
		const float TWO_PI = 6.2831853f;
		
		protected int _levelNumber;
		protected float _timer;
		protected float _angle;
		protected float _gameTimer;
		
		SpriteTile LoadingSpinner;
		Node Hub;
		Label LoadingText;
		
		// CONSTRUCTOR ---------------------------------------------------------------------------------------
		
		public LoadingScene (string pDestination, GameSceneData pData = null) {
			if(pData==null) {
				pData = DEFAULT_GAME_SCENE_DATA;
				GAME_SCENE_DATA = pData;
			}
			
			this.Camera.SetViewFromViewport();
			
			stopwatch = Stopwatch.StartNew();
			
			_levelNumber = pData.level;
			_gameTimer = pData.timeLimit;
			_angle = 0.0f;
			_timer = 0.0f;
			_loadIndex = 0;
			
			Hub = new Node();
			Hub.Position = new Vector2(480.0f, 272.0f);
			this.AddChild(Hub);
			
			LoadingSpinner = Support.SpriteFromFile("/Application/assets/images/UI/loading.png");
			LoadingSpinner.Pivot = new Vector2(0.5f, 0.5f);
			LoadingSpinner.Position = new Vector2(-145.0f, -119.0f);
			LoadingSpinner.RegisterPalette(1);
			Hub.AddChild(LoadingSpinner);
			
			LoadingText = new Label("loading", UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold")));
			LoadingText.Position = new Vector2(445.0f, 257.0f);
			LoadingText.RegisterPalette(2);
			this.AddChild(LoadingText);
			
			// LOADING PROCEDURE HEADER
			loadProc = new List<Action>{
				() => {
					;//dummy
				},
				() => {
					InputManager.Instance.enabled = false;
				},
				// PRE-LOAD LEVEL DATA
				() => {
					LevelManager.Instance.LoadGameData();
				}
			};
			
			switch(pDestination) {
			case("Level Select"):
				AddLevelSelectProcs();
				break;
			case("Menu"):
				AddMenuProcs();
				break;
			case("Game"):
			default:
				AddGameProcs();
				break;
			}
			
			loadProc.AddRange( new List<Action> {
				() => {
					InputManager.Instance.enabled = true;
				}
			} );
			
			Scheduler.Instance.ScheduleUpdateForTarget(this, 0, false);
		}
		
		// OVERRIDES ----------------------------------------------------------------------------------------
		
#if DEBUG
		public override void OnEnter ()
		{
			base.OnEnter ();
			Console.WriteLine("########### ENTER LoadingScene ###############");
		}
#endif
		
		public override void OnExit ()
		{
			base.OnExit ();
			loadProc.Clear();
			loadProc = null;
			stopwatch.Stop();
			stopwatch = null;
			_scene = null;
			Hub.RemoveAllChildren(true);
			Hub = null;
			LoadingSpinner = null;
#if DEBUG
			Console.WriteLine("########### EXIT LoadingScene ###############");
#endif
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
				Director.Instance.ReplaceScene( _scene );
			}
		}
		
		// METHODS ------------------------------------------------------------------------------------------
		
		/// <summary>
		/// Preload assets for Game Scene
		/// </summary>
		protected void AddGameProcs() {
			List<Action> proc = new List<Action> {
			// PRE-LOAD IMAGES
				() => {
					LevelManager.Instance.GetLevelSettings(_levelNumber);
				},
				() => {
					var temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 16, "Bold");
					temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18, "Regular");
					temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold");
					temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 44, "Bold");
					temp = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 102, "Bold");
				},
//				() => {
//					var temp = Support.TiledSpriteFromFile("Application/assets/images/next_level_sm.png", 1, 3);
//				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/GameHudBar.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/handIcon.png");
				},
				() => {
					Support.SoundSystem.Instance.Stop(LevelManager.Instance.SoundPrefix + "levelcomplete.wav");
					Support.SoundSystem.Instance.Stop(LevelManager.Instance.SoundPrefix + "threetiles.wav");
					Support.SoundSystem.Instance.Stop(LevelManager.Instance.SoundPrefix + "wrong.wav");
					Support.SoundSystem.Instance.Stop(LevelManager.Instance.SoundPrefix + "break.wav");
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
					var temp = Support.SpriteFromFile("/Application/assets/images/UI/cubePoints.png");
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
					var temp = Support.SpriteFromFile("Application/assets/images/burst.png");
				},
				() => {
					var temp = Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2);
				},
				() => {
					var temp = Crystallography.UI.IconPopupManager.Instance;
				},
				() => {
					System.GC.Collect();
				},
				// PREPARE GAME SCENE
				() => {
					_scene = new GameScene(GAME_SCENE_DATA);
				},
				() => {
					while (GameScene.Hud.Initialized == false) {
						//WAIT
					}
					(_scene as GameScene).ResetToLevel();
				}
			};
			
			loadProc.AddRange( proc );
			proc.Clear();
		}
		
		/// <summary>
		/// Preload assets for Menu Scene
		/// </summary>
		protected void AddMenuProcs() {
			List<Action> proc = new List<Action> {
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_bottom.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_blue2.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_red2.png");
				},
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_top.png");
				},
				() => {
					var temp = Support.TiledSpriteFromFile("/Application/assets/images/UI/NewGameButton.png", 1, 3);
				},
				() => {
					var temp = Support.TiledSpriteFromFile("/Application/assets/images/UI/LevelSelectButton.png", 1, 3);
				},
				() => {
					var temp = Support.TiledSpriteFromFile("/Application/assets/images/UI/CreditsButton.png", 1, 3);
				},
				() => {
					var temp = Support.TiledSpriteFromFile("/Application/assets/images/UI/InstructionsButton.png", 1, 3);
				},
				() => {
					System.GC.Collect();
				},
				() => {
					_scene = new MenuSystemScene("Menu");
				}
			};
			
			loadProc.AddRange( proc );
			proc.Clear();
		}
		
		/// <summary>
		/// Preload assets for Level Select Scene
		/// </summary>
		protected void AddLevelSelectProcs() {
			List<Action> proc = new List<Action>{
				() => {
					var temp = Support.SpriteFromFile("/Application/assets/images/UI/statsBox.png");
				},
				() => {
					var temp = Support.SpriteFromFile("Application/assets/images/UI/LevelSelectIndicator.png");
				},
				() => {
					var temp = Support.TiledSpriteFromFile("Application/assets/images/UI/LevelSelectItemButton.png", 1, 3);
				},
				() => {
					System.GC.Collect();
				},
				() => {
					_scene = new MenuSystemScene("Level Select");
				}
			};
			
			loadProc.AddRange( proc );
			proc.Clear();
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------
#if DEBUG
		~LoadingScene() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

