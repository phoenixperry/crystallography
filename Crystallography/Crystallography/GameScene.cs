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
		private static readonly int TOTAL_LEVELS = 11;
		
		// Change the following value to true if you want bounding boxes to be rendered
        private static bool DEBUG_BOUNDINGBOXS = false;
		
		public static Random Random = new Random();
		
//		public static Card[] cards;
//		public static Group[] groups;
    	public static GamePhysics _physics;
		protected static List<ICrystallonEntity> _allEntites = new List<ICrystallonEntity>();
//		private static CardData[] currentLevelData;
		
		public static SelectionGroup _selectionGroup = null;
		
//		public bool WasTouch;
//		public bool IsTouch;
//		public static Card SelectedCard;
//		public static Card FirstAttachedCard;
//		public static Card SecondAttachedCard;
		public Vector2 TouchStart;
		
		// TEST VARIABLES
		public CardCrystallonEntity test;
		public GroupCrystallonEntity testGroup;
		
//		public Node n = new Node();
//		public Node childN1 = new Node();
//		public Node childN2 = new Node();
//		public Node childN3 = new Node();
		
//		public SpriteUV s;
		
		// END TEST VARIABLES
		
		// GET & SET -----------------------------------------------------------------------------------
		
		public static int currentLevel { get; private set; }
		
		public static System.Collections.ObjectModel.ReadOnlyCollection<ICrystallonEntity> getAllEntities() {
			return _allEntites.AsReadOnly();
		}
        
		// CONSTRUCTOR ----------------------------------------------------------------------------------
		
        public GameScene ( int pCurrentLevel )
		{
			currentLevel = pCurrentLevel;
            this.Camera.SetViewFromViewport();
            _physics = GamePhysics.Instance;
			
			_selectionGroup = new SelectionGroup(this, _physics, null);
			_selectionGroup.addToScene();
			
			CardManager.Instance.Reset( this );
			GroupManager.Instance.Reset( this );
//			new QualityManager();
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
			UpdateInput(dt);
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
			_selectionGroup.setTouch();
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
