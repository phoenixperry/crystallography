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
		
		
		// TEST +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		
//		public Support.ParticleEffectsManager pManager;
//		public float particleTimer = 0.0f;
		
		// END TEST +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		
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
			sg.addToScene();
			
			QualityManager.Instance.Reset( CardManager.Instance, currentLevel );
			
			CardManager.Instance.Populate();
	
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
			ForegroundLayer.AddChild( Support.ParticleEffectsManager.Instance );
			
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
			base.OnExit();
			Support.MusicSystem.Instance.StopAll();
//			Crystallography.UI.ScoreScene.QuitButtonPressDetected += (sender, e) => { QuitToTitle(); };
//			Crystallography.UI.ScoreScene.PauseDetected += (sender, e) => { Pause(e.isPaused); };
        }
		
        public override void Update ( float dt )
        {

			InputManager.Instance.Update(dt);
            base.Update (dt);			
			
            //We don't need these, but sadly, the Simulate call does.
            Vector2 dummy1 = new Vector2();
            Vector2 dummy2 = new Vector2();
			
			if( paused == false ) {
				//PHYSICS UPDATE CALL
	            _physics.Simulate(-1,ref dummy1,ref dummy2);
			}
        }
		
//		public override void Draw ()
//		{
//			base.Draw ();
//		}
		
		// METHODS -------------------------------------------------------------------------------------------------
		
		public void AddChildEntity( ICrystallonEntity pEntity, int pLayerIndex=1 ) {
			_allEntites.Add(pEntity);
			Layers[pLayerIndex].AddChild(pEntity.getNode());
//			switch(pLayerIndex) {
//			case(1):
//				BackgroundLayer.AddChild(pEntity.getNode());
//				break;
//			case(0):
//				BackgroundLayer.AddChild(pEntity.getNode());
//				break;
//			case(2):
//				ForegroundLayer.AddChild(pEntity.getNode());
//				break;
//			default:
//				GameplayLayer.AddChild(pEntity.getNode());
//				break;
//			}
			
		}
		
		private void Pause( bool pOn ) {
			SchedulerPaused = paused = pOn;
		}
		
		public void RemoveChildEntity( ICrystallonEntity pEntity, bool doCleanup ) {
			_allEntites.Remove( pEntity );
			pEntity.Parent.RemoveChild( pEntity.getNode(), doCleanup );
		}
		
//		public void UpdateInput ( float dt ) {
			// OBJECT SELECTION, DRAGGING, ETC.
			
//			if ( Input2.Touch00.Press ) {
//				pressDuration = 0.0f;
//				
//			} else if (Input2.Touch00.On) {
//				pressDuration += dt;
//			} else if (Input2.Touch00.Release) {
//				Console.WriteLine ("Press: " + pressDuration);
//				Console.WriteLine ("Release: " + releaseDuration);
//				if (pressDuration < MAX_PRESS_DURATION && lastPressDuration < MAX_PRESS_DURATION 
//				    && releaseDuration < MAX_RELEASE_DURATION) {
//					Console.WriteLine("Double Tap Detected!");
//				}
//				lastPressDuration = pressDuration;
//				releaseDuration = 0.0f;
//			} else {
//				releaseDuration += dt;
//			}
			
//			SelectionGroup.Instance.setTouch();
//		}
		
		/// <summary>
		/// Restarts GameScene at next level OR Goes to TitleScene if there are no more levels.
		/// </summary>
		public void goToNextLevel( ) {
			currentLevel++;
			if (currentLevel < TOTAL_LEVELS) {
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
			Director.Instance.ReplaceScene( new MenuSystemScene("Menu") );
		}
		
		public static void QuitToLevelSelect() {
			Director.Instance.ReplaceScene( new MenuSystemScene("LevelSelect") );
			UISystem.SetScene( new Crystallography.UI.LevelSelectScene() );
		}

        ~GameScene(){
        }

	}
}
