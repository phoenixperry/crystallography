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
		public static readonly Bounds2 PlayBounds = new Bounds2( Vector2.Zero, new Vector2( Director.Instance.GL.Context.Screen.Width, Director.Instance.GL.Context.Screen.Height - 71.0f ) );

#if DEBUG
		// Change the following value to true if you want bounding boxes to be rendered
        private static bool DEBUG_BOUNDINGBOXS = false;
#endif
		public static Random Random = new Random();
		public static Crystallography.UI.GameSceneHud Hud;
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
				}// else {
//					return (Hud.levelEndPanel.Visible == false);
//				}
				return true;
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
						
//			LevelManager.Instance.GetLevelSettings( pCurrentLevel );
			
			Touch.GetData(0).Clear();
			InputManager.Instance.Reset();
			
			currentLevel = pCurrentLevel;
            this.Camera.SetViewFromViewport();
            _physics = GamePhysics.Instance;
			
			Hud = new Crystallography.UI.GameSceneHud(this);
			if (pTimed) {
				// TODO Set Up Timer
			}
			ForegroundLayer.AddChild(Hud);
			
			var sg = SelectionGroup.Instance;
			sg.Reset( this );
			ForegroundLayer.AddChild(sg.getNode());

#if DEBUG
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
#endif
			
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			Pause (false);
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime(0.1f) );
			sequence.Add( new CallFunc( () => ResetToLevel() ) );
			this.RunAction(sequence);
			
			ForegroundLayer.AddChild( Support.ParticleEffectsManager.Instance );
			
//			var c = new Vector4(0.271f, 0.688f, 0.525f, 0.666f);
//			Console.WriteLine(c.ToString());
//			c = Support.RGBToHSB(c);
//			Console.WriteLine(c.ToString());
//			c = Support.HSBToRGB(c);
//			Console.WriteLine(c.ToString());
			
#if METRICS
			DataStorage.AddMetric("Level", () => currentLevel, 1);
#endif
        }
		
		// EVENT HANDLERS --------------------------------------------------------------------------
		
        void HandleCardManagerInstanceNoMatchesPossibleDetected (object sender, EventArgs e)
        {
        	Sequence sequence = new Sequence();
			sequence.Add( new DelayTime( 1.0f ) );
			sequence.Add( new CallFunc( () => GoToNextLevel() ) );
			this.RunAction(sequence);
        }
        
		void HandleCrystallographyUIPausePanelPauseDetected (object sender, PauseEventArgs e)
		{
			Pause (e.isPaused);
		}
		
		// OVERRIDES ---------------------------------------------------------------------------
		
		public override void OnEnter ()
        {
#if DEBUG
			Console.WriteLine("########### ENTER GameScene ###############");
#endif
			base.OnEnter();
			if( Random.NextFloat() > 0.5f ) {
				Support.MusicSystem.Instance.Play("stack1music.mp3");
			} else {
				Support.MusicSystem.Instance.Play("stack2music.mp3");
			}
			Crystallography.UI.PausePanel.PauseDetected += HandleCrystallographyUIPausePanelPauseDetected;
			
			if( currentLevel == 999 ) {
				InputManager.Instance.UpJustUpDetected += HandleInputManagerInstanceUpJustUpDetected;
				InputManager.Instance.DownJustUpDetected += HandleInputManagerInstanceDownJustUpDetected;
			}
        }

		void HandleInputManagerInstanceDownJustUpDetected (object sender, EventArgs e)
		{
//			CardManager.Instance.RemoveQuality("QPattern");
			LevelManager.Instance.ChangeDifficulty(-1);
		}

		void HandleInputManagerInstanceUpJustUpDetected (object sender, EventArgs e)
		{
//			CardManager.Instance.AddQuality("QPattern");
			LevelManager.Instance.ChangeDifficulty(1);
		}
		
        public override void OnExit ()
        {
			foreach ( Layer l in Layers ) {
				l.RemoveAllChildren(true);
			}
			Layers = null;
			Hud = null;
			Support.ParticleEffectsManager.Instance.Destroy();
			base.OnExit();
			Support.MusicSystem.Instance.StopAll();
			Crystallography.UI.PausePanel.PauseDetected -= HandleCrystallographyUIPausePanelPauseDetected;
			
			InputManager.Instance.UpJustUpDetected -= HandleInputManagerInstanceUpJustUpDetected;
			
			AppMain.UI_INPUT_ENABLED = true;
#if DEBUG
			Console.WriteLine("########### EXIT GameScene ###############");
#endif
        }
		
        public override void Update ( float dt )
        {
			InputManager.Instance.Update(dt);
            base.Update (dt);			
        }
		
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
		public void GoToNextLevel( ) {
			currentLevel++;
			ForceGarbageCollection();
			ResetToLevel();
		}
		
		public void ResetToLevel() {
			if (currentLevel < TOTAL_LEVELS || currentLevel == 999) {
#if DEBUG
				Console.WriteLine( "Resetting to start level " + currentLevel );
#endif
				
				LevelManager.Instance.Reset();
				LevelManager.Instance.GetLevelSettings( currentLevel );
				Clear();
				QualityManager.Instance.Reset( currentLevel );
				CardManager.Instance.Populate();
				EventHandler handler = LevelChangeDetected;
				if (handler != null) {
					handler( this, null );
				}
				
				QColor.Instance.ShiftColors(1, 1.0f);
				CardManager.Instance.RotateColors(-1, 1.0f, 5.0f);
				
			} else {
#if DEBUG
				Console.WriteLine( "All known levels (" + TOTAL_LEVELS + ") complete. Returning to TitleScene." );
#endif
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
			Director.Instance.ReplaceScene( new Crystallography.UI.LoadingScene(0,false,"Menu") );
		}
		
		public static void QuitToLevelSelect() {
			( Director.Instance.CurrentScene as GameScene ).Clear();
			ForceGarbageCollection();
			Director.Instance.ReplaceScene( new Crystallography.UI.LoadingScene(0,false,"Level Select") );
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
