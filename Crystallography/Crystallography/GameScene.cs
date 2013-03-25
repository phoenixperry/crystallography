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
		public static readonly int TOTAL_LEVELS = 25;
		
		// Change the following value to true if you want bounding boxes to be rendered
        private static bool DEBUG_BOUNDINGBOXS = false;
		
		public static Random Random = new Random();
		
    	public static GamePhysics _physics;
		public static Crystallography.BG.CrystallonBackground background;
		protected static List<ICrystallonEntity> _allEntites = new List<ICrystallonEntity>();
		
		public static event EventHandler LevelChangeDetected;
		
		public Layer BackgroundLayer;
		public Layer GameplayLayer;
		public Layer ForegroundLayer;
		public Layer[] Layers;
		
		// GET & SET -----------------------------------------------------------------------------------
		
		public static int currentLevel { get; private set; }
		
		public static System.Collections.ObjectModel.ReadOnlyCollection<ICrystallonEntity> getAllEntities() {
			return _allEntites.AsReadOnly();
		}
		
		public static bool paused { get; private set; }
		
		// CONSTRUCTOR ----------------------------------------------------------------------------------
		
        public GameScene ( int pCurrentLevel )
		{	
			Touch.GetData(0).Clear();
			UISystem.SetScene( new Crystallography.UI.ScoreScene() );
			
			this.AddChild( BackgroundLayer = new Layer() );
			this.AddChild( GameplayLayer = new Layer() );
			this.AddChild( ForegroundLayer = new Layer() );
			Layers = new Layer[] {BackgroundLayer, GameplayLayer, ForegroundLayer};
						
			LevelManager.Instance.LoadGameData();
			LevelManager.Instance.GetLevelSettings( pCurrentLevel );
			
			background = new Crystallography.BG.CrystallonBackground();
			BackgroundLayer.AddChild(background);
			
			currentLevel = pCurrentLevel;
            this.Camera.SetViewFromViewport();
            _physics = GamePhysics.Instance;
			
			CardManager.Instance.Reset( this );
			GroupManager.Instance.Reset( this );
			
			var sg = SelectionGroup.Instance;
			sg.Reset( this );
			this.AddChild(sg.getNode());
			
			QualityManager.Instance.Reset( CardManager.Instance, currentLevel );
			
			CardManager.Instance.Populate();
			
//			ForegroundLayer.AddChild( Support.ParticleEffectsManager.Instance );
	
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
			if( Random.NextBool()) {
				Support.MusicSystem.Instance.Play("stack1music.mp3");
			} else {
				Support.MusicSystem.Instance.Play("stack2music.mp3");
			}
			Crystallography.UI.ScoreScene.QuitButtonPressDetected += (sender, e) => { QuitToTitle(); };
			Crystallography.UI.ScoreScene.PauseDetected += (sender, e) => { Pause(e.isPaused); };
        }
		
        public override void OnExit ()
        {
			SelectionGroup.Instance.Parent.RemoveChild(SelectionGroup.Instance.getNode(), false);
			base.OnExit();
			Support.MusicSystem.Instance.StopAll();
			Crystallography.UI.ScoreScene.QuitButtonPressDetected -= (sender, e) => { QuitToTitle(); };
			Crystallography.UI.ScoreScene.PauseDetected -= (sender, e) => { Pause(e.isPaused); };
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
			if (currentLevel < TOTAL_LEVELS) {
				ForceGarbageCollection();
				Console.WriteLine( "Resetting to start level " + currentLevel );
				LevelManager.Instance.GetLevelSettings( currentLevel );
				background.PickBackground();
				CardManager.Instance.Reset( this );
				GroupManager.Instance.Reset( this );
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
		
		public static void QuitToTitle() {
			ForceGarbageCollection();
			UISystem.CurrentScene.RootWidget.Dispose();
			Director.Instance.ReplaceScene( new MenuSystemScene("Menu") );
		}
		
		public static void QuitToLevelSelect() {
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
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~GameScene(){
			Console.WriteLine("GameScene deleted.");
        }
#endif
	}
}
