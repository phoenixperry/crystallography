using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Graphics;

namespace Crystallography
{
    public class GameScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
    {
		// TODO make this dynamic or data-driven
		public static readonly int TOTAL_LEVELS = 41;
		
		// Change the following value to true if you want bounding boxes to be rendered
        private static bool DEBUG_BOUNDINGBOXS = false;
		
		public static Random Random = new Random();
		public static GameSceneHud Hud;
    	public static GamePhysics _physics;
		protected static List<ICrystallonEntity> _allEntites = new List<ICrystallonEntity>();
		
		public static event EventHandler LevelChangeDetected;
		
		public Layer BackgroundLayer;
		public Layer GameplayLayer;
		public Layer ForegroundLayer;
		public Layer DialogLayer;
		public static Layer[] Layers;
		
		// GET & SET -----------------------------------------------------------------------------------
		
		public static int currentLevel { get; private set; }
		
		public static System.Collections.ObjectModel.ReadOnlyCollection<ICrystallonEntity> getAllEntities() {
			return _allEntites.AsReadOnly();
		}
		
		public static bool paused { get; private set; }
		
		public static bool canPause { 
			get {
				if (Hud == null) {
					return false;
				} else {
					return (Hud.levelEndPanel.Visible == false);
				}
				
			}
		}
		
		// CONSTRUCTOR ----------------------------------------------------------------------------------
		
        public GameScene ( int pCurrentLevel, bool pTimed )
		{	
			this.AddChild( BackgroundLayer = new Layer() );
			this.AddChild( GameplayLayer = new Layer() );
			this.AddChild( ForegroundLayer = new Layer() );
			this.AddChild( DialogLayer = new Layer() );
			Layers = new Layer[] {BackgroundLayer, GameplayLayer, ForegroundLayer, DialogLayer};
						
			LevelManager.Instance.LoadGameData();
			LevelManager.Instance.GetLevelSettings( pCurrentLevel );
			
			Touch.GetData(0).Clear();
			UISystem.SetScene( new Crystallography.UI.ScoreScene() );
			InputManager.Instance.Reset();
						
//			background = new Crystallography.BG.CrystallonBackground();
//			BackgroundLayer.AddChild(background);
			
			currentLevel = pCurrentLevel;
            this.Camera.SetViewFromViewport();
            _physics = GamePhysics.Instance;
			
//			CardManager.Instance.Reset( this );
//			GroupManager.Instance.Reset( this );
			
			
			
//			QualityManager.Instance.Reset( CardManager.Instance, currentLevel );
			
//			CardManager.Instance.Populate();
			
			
			Hud = new GameSceneHud(this);
			if (pTimed) {
				// Set Up Timer
			}
			ForegroundLayer.AddChild(Hud);
			
			var sg = SelectionGroup.Instance;
			sg.Reset( this );
			ForegroundLayer.AddChild(sg.getNode());
	
            // This is debug routine that will draw the physics bounding box around all physics bodies
            if(DEBUG_BOUNDINGBOXS)
            {
                this.AdHocDraw += () => {
					foreach (PhysicsBody pb in _physics.SceneBodies) {
						if ( pb != null ) {
							var bottomLeft = pb.AabbMin;
							var topRight = pb.AabbMax;
							Director.Instance.DrawHelpers.DrawBounds2Fill (
								new Bounds2(bottomLeft*GamePhysics.PtoM, topRight*GamePhysics.PtoM));
						}
					}
                };
            }
			
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			Pause (false);
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime(0.1f) );
			sequence.Add( new CallFunc( () => resetToLevel() ) );
			this.RunAction(sequence);
			
			ForegroundLayer.AddChild( Support.ParticleEffectsManager.Instance );
			
//			TestButton = new ButtonEntity("Button", this, _physics, Support.TiledSpriteFromFile("Application/assets/images/button_85x56.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			TestButton.setPosition( 100f, 100f );
//			TestButton.addToScene(2);
//			TestButton.ButtonUpAction += (sender, e) => {
//				Console.WriteLine ("TAP!");
//			};
			
//			LevelEndPanel TestLayer = new LevelEndPanel(this);
//			ForegroundLayer.AddChild(TestLayer);
//			TestLayer.Hide();
//			TestLayer.Show();
			
#if METRICS
			DataStorage.AddMetric("Level", () => currentLevel, 1);
#endif
        }
		
		// EVENT HANDLERS --------------------------------------------------------------------------
		
        void HandleCardManagerInstanceNoMatchesPossibleDetected (object sender, EventArgs e)
        {
        	Sequence sequence = new Sequence();
			sequence.Add( new DelayTime( 1.0f ) );
			sequence.Add( new CallFunc( () => goToNextLevel() ) );
			this.RunAction(sequence);
        }
        
		// OVERRIDES ---------------------------------------------------------------------------
		
		public override void OnEnter ()
        {
			base.OnEnter();
			if( Random.NextFloat() > 0.5f ) {
				Support.MusicSystem.Instance.Play("stack1music.mp3");
			} else {
				Support.MusicSystem.Instance.Play("stack2music.mp3");
			}
			
			PausePanel.QuitButtonPressDetected += (sender, e) => { QuitToTitle(); };
			PausePanel.PauseDetected += (sender, e) => { Pause(e.isPaused); };
        }
		
        public override void OnExit ()
        {
			foreach ( Layer l in Layers ) {
				l.RemoveAllChildren(true);
			}
			Layers = null;
			Hud = null;
			Support.ParticleEffectsManager.Instance.Destroy();
			SelectionGroup.Instance.Parent.RemoveChild(SelectionGroup.Instance.getNode(), false);
			base.OnExit();
			Support.MusicSystem.Instance.StopAll();
			PausePanel.QuitButtonPressDetected -= (sender, e) => { QuitToTitle(); };
			PausePanel.PauseDetected -= (sender, e) => { Pause(e.isPaused); };
			AppMain.UI_INPUT_ENABLED = true;
        }
		
        public override void Update ( float dt )
        {
			InputManager.Instance.Update(dt);
            base.Update (dt);			
			
            //We don't need these, but sadly, the Simulate call does.
            Vector2 dummy1 = Vector2.Zero;
            Vector2 dummy2 = Vector2.Zero;
			
			if( paused == false ) {
				//PHYSICS UPDATE CALL
//	            _physics.Simulate(-1, dummy1, dummy2);
				_physics.Simulate();
			}
        }
		
//		public override void Draw ()
//		{
//			base.Draw ();
//		}
		
		// METHODS -------------------------------------------------------------------------------------------------
		
		/// <summary>
		/// Adds an entity to the specified layer. 0 = Background. 1 = Gameplay. 2 = Foreground. Defaults to 1.
		/// </summary>
		/// <param name='pEntity'>
		/// entity.
		/// </param>
		/// <param name='pLayerIndex'>
		/// layer index.
		/// </param>
		public void AddChildEntity( ICrystallonEntity pEntity, int pLayerIndex=1 ) {
			if (_allEntites.Contains(pEntity) == false) {
				_allEntites.Add(pEntity);
			}
			Layers[pLayerIndex].AddChild(pEntity.getNode());
		}
		
		private void Pause( bool pOn ) {
			SchedulerPaused = paused = pOn;
		}
		
		public void RemoveChildEntity( ICrystallonEntity pEntity, bool doCleanup ) {
			_allEntites.Remove( pEntity );
			pEntity.Parent.RemoveChild( pEntity.getNode(), doCleanup );
		}
		
		/// <summary>
		/// Restarts GameScene at next level OR Goes to TitleScene if there are no more levels.
		/// </summary>
		public void goToNextLevel( ) {
			currentLevel++;
			resetToLevel();
		}
		
		public void resetToLevel() {
			if (currentLevel < TOTAL_LEVELS || currentLevel == 999) {
				ForceGarbageCollection();
				Console.WriteLine( "Resetting to start level " + currentLevel );
				LevelManager.Instance.Reset();
				LevelManager.Instance.GetLevelSettings( currentLevel );
//				background.PickBackground();
				Clear();
//				CardManager.Instance.Reset( this );
//				GroupManager.Instance.Reset( this );
				QualityManager.Instance.Reset( CardManager.Instance, currentLevel );
				CardManager.Instance.Populate();
				EventHandler handler = LevelChangeDetected;
				if (handler != null) {
					handler( this, null );
				}
			} else {
				Console.WriteLine( "All known levels (" + TOTAL_LEVELS + ") complete. Returning to TitleScene." );
				QuitToTitle();
			}
		}
		
		protected void Clear() {
			CardManager.Instance.Reset( this );
			GroupManager.Instance.Reset( this );
		}
		
		public static void QuitToTitle() {
			( Director.Instance.CurrentScene as GameScene ).Clear();
			ForceGarbageCollection();
			UISystem.CurrentScene.RootWidget.Dispose();
			Director.Instance.ReplaceScene( new MenuSystemScene("Menu") );
		}
		
		public static void QuitToLevelSelect() {
			( Director.Instance.CurrentScene as GameScene ).Clear();
			ForceGarbageCollection();
			UISystem.CurrentScene.RootWidget.Dispose();
			Director.Instance.ReplaceScene( new MenuSystemScene("LevelSelect") );
			UISystem.SetScene( new Crystallography.UI.LevelSelectScene() );
		}
		
		private static void ForceGarbageCollection() {
#if DEBUG
				Console.WriteLine("Force Garbage Collecion.");
#endif
				System.GC.Collect();
		}
		
//		public void DisableInput() {
//			AppMain.UI_INPUT_ENABLED = false;
//		}
//		
//		public void TempDisableInput() {
//			Sequence sequence = new Sequence();
//			sequence.Add( new CallFunc( () => DisableInput() ) );
//			sequence.Add( new DelayTime(0.1f) );
//			sequence.Add( new CallFunc( () => DisableInput() ) );
//			this.RunAction(sequence);
//			Console.WriteLine("TempDisableInput");
//		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~GameScene(){
			Console.WriteLine("GameScene deleted.");
        }
#endif
	}
}
