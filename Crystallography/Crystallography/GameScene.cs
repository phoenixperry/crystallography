using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;
//using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Graphics;

namespace Crystallography
{
    public class GameScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
    {
		private static readonly int TOTAL_LEVELS = 11;
		
		// Change the following value to true if you want bounding boxes to be rendered
        private static bool DEBUG_BOUNDINGBOXS = false;
		
		public static Random Random = new Random();
		
    	public static GamePhysics _physics;
		protected static List<ICrystallonEntity> _allEntites = new List<ICrystallonEntity>();
		
//		public static SelectionGroup _selectionGroup = null;
		
		public static Sce.PlayStation.HighLevel.UI.DoubleTapGestureDetector dtgd;
		
		// GET & SET -----------------------------------------------------------------------------------
		
		public static int currentLevel { get; private set; }
		
		public static System.Collections.ObjectModel.ReadOnlyCollection<ICrystallonEntity> getAllEntities() {
			return _allEntites.AsReadOnly();
		}
		
		
		// DOUBLE TAP TEST
		
//		public static readonly float MAX_PRESS_DURATION = 0.15f;
//		public static readonly float MAX_RELEASE_DURATION = 0.3f;
//		public static readonly float MAX_TAP_DISTANCE = 50;
//		public static float lastPressDuration { get; protected set; }
//		public static float pressDuration { get; protected set; }
//		public static float releaseDuration { get; protected set; }
//		public static float tapDistance { get; protected set; }
//		public static Vector2 lastTapPosition { get; protected set; }
        
		// END DOUBLE TAP TEST
		
		// CONSTRUCTOR ----------------------------------------------------------------------------------
		
        public GameScene ( int pCurrentLevel )
		{
			currentLevel = pCurrentLevel;
            this.Camera.SetViewFromViewport();
            _physics = GamePhysics.Instance;
			
			CardManager.Instance.Reset( this );
			GroupManager.Instance.Reset( this );
			
			var sg = SelectionGroup.Instance;
			sg.Reset( this );
			sg.addToScene();

			QualityManager.Instance.Reset( CardManager.Instance, currentLevel );
//			QualityManager qm = new QualityManager();
//			qm.LoadLevelQualities( currentLevel );
//			qm.BuildQualityDictionary( this, _physics );
//			qm.ApplyQualities();
			
            // This is debug routine that will draw the physics bounding box around the players paddle
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
			
			CardManager.Instance.NoMatchesPossibleDetected += HandleCardManagerInstanceNoMatchesPossibleDetected;
			
			// FONT TEST
			
			Font bariol = new Font("/Application/assets/fonts/Bariol_Regular.otf", 18, FontStyle.Regular);
			Label scoreLabel = new Label();
			FontMap fontMap = new FontMap( bariol, 100 );
			scoreLabel.Text = "score ";
			scoreLabel.Color = Colors.White;
			scoreLabel.FontMap = fontMap;
			scoreLabel.Position = Vector2.One * 10;
			this.AddChild(scoreLabel);
			Label scoreNumber = new Label();
			scoreNumber.Text = "1234567890";
			scoreNumber.Color = Colors.White;
			scoreNumber.FontMap = fontMap;
			scoreNumber.Position = new Vector2(60, 10);
			this.AddChild(scoreNumber);
			
			// END FONT TEST
			
			
			
			
			// DOUBLE TAP GESTURE DETECTOR TEST
			
//			pressDuration = 0f;
//			lastPressDuration = MAX_PRESS_DURATION;
//			releaseDuration = MAX_RELEASE_DURATION;
			
			
//			dtgd = new Sce.PlayStation.HighLevel.UI.DoubleTapGestureDetector();
//			dtgd.DoubleTapDetected += delegate( object sender, Sce.PlayStation.HighLevel.UI.DoubleTapEventArgs e ) {
//				Console.WriteLine( "Double Tap Detected" );
//			};
//			
//			var box = new Sce.PlayStation.HighLevel.UI.ImageBox();
//			box.SetSize(Director.Instance.GL.Context.GetViewport().Width, Director.Instance.GL.Context.GetViewport().Height);
//			box.AddGestureDetector(dtgd);
//			box.SetPosition(box.Width/2, box.Height/2);
//			this.AddChild(box);
			
			// END DOUBLE TAP GESTURE DETECTOR TEST
        }

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
        }
		
        public override void OnExit ()
        {
			base.OnExit();
        }
		
        public override void Update ( float dt )
        {
            base.Update (dt);
            
            //TODO: support multitouch controls
//			Touch.GetData(0); 
//			foreach (var touchData in Touch.GetData(0)) {
//            	if (touchData.Status == TouchStatus.Down ||
//                	touchData.Status == TouchStatus.Move) {
//					
//				}
//			}
			
            //We don't need these, but sadly, the Simulate call does.
            Vector2 dummy1 = new Vector2();
            Vector2 dummy2 = new Vector2();
			
            //PHYSICS UPDATE CALL
            _physics.Simulate(-1,ref dummy1,ref dummy2);
            
			//INPUT UPDATE CALL
//			UpdateInput(dt);
			InputManager.Instance.Update(dt);
        }
		
		// METHODS -------------------------------------------------------------------------------------------------
		
		public void AddChildEntity( ICrystallonEntity pEntity ) {
			_allEntites.Add(pEntity);
			AddChild(pEntity.getNode());
		}
		
		public void RemoveChildEntity( ICrystallonEntity pEntity, bool doCleanup ) {
			_allEntites.Remove( pEntity );
			RemoveChild( pEntity.getNode(), doCleanup );
		}
		
		public void UpdateInput ( float dt ) {
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
		}
		
		/// <summary>
		/// Restarts GameScene at next level OR Goes to TitleScene if there are no more levels.
		/// </summary>
		public void goToNextLevel( ) {
//			LevelData.CURRENT_LEVEL++;
			currentLevel++;
			if (currentLevel < TOTAL_LEVELS) {
				Console.WriteLine( "Resetting to start level " + currentLevel );
				CardManager.Instance.Reset( this );
				GroupManager.Instance.Reset( this );
				QualityManager.Instance.Reset( CardManager.Instance, currentLevel );
			} else {
				Console.WriteLine( "All known levels (" + TOTAL_LEVELS + ") complete. Returning to TitleScene." );
				Director.Instance.ReplaceScene( new TitleScene() );
			}
		}
		
        ~GameScene(){
           // _pongBlipSoundPlayer.Dispose();
        }
    }
}
