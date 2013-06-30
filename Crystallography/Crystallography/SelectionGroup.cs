using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public class SelectionGroup : GroupCrystallonEntity {
		
		protected static SelectionGroup _instance;
		
		public static readonly int MAX_CAPACITY = 3;
		protected static readonly float SNAP_DISTANCE = 50.0f;
		protected static readonly float EASE_DISTANCE = 70.0f;
		protected static readonly float MAXIMUM_PICKUP_VELOCITY = 400.0f;
		
		private AbstractCrystallonEntity lastEntityReleased;
		private Vector2 lastPosition;
//		private SpriteTile selectionMarker;
		
		
		public event EventHandler<CubeCompleteEventArgs>       CubeCompleteDetected;
		public event EventHandler<CubeGroupCompleteEventArgs>  CubeGroupCompleteDetected;
		public event EventHandler                              CubeFailedDetected;
		
		// GET & SET -------------------------------------------------------------
		
		public static SelectionGroup Instance {
			get {
				if (_instance == null) {
					return _instance = new SelectionGroup();
				} else {
					return _instance;
				}
			}
			private set{
				_instance = value;
			}
		}
		
		public EaseState easeState { get; private set; }
		public float velocity { get; private set; }
		
		// CONSTRUCTOR -----------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.SelectionGroup"/> class.
		/// </summary>
		/// <param name='pScene'>
		/// The Scene to which we will be attached
		/// </param>
		/// <param name='pGamePhysics'>
		/// Reference to GamePhysics
		/// </param>
		/// <param name='pShape'>
		/// A PhysicsShape for collision purposes
		/// </param>
		protected SelectionGroup() : base( Director.Instance.CurrentScene, GamePhysics.Instance, null, MAX_CAPACITY ) {
//			potentialMembers = new List<AbstractCrystallonEntity>();
//			selectionMarker = Support.SpriteFromFile("Application/assets/images/SelectionMarker.png");
//			selectionMarker.Visible = false;
//			selectionMarker.Position = -0.5f*selectionMarker.CalcSizeInPixels();
//			selectionMarker.Position = new Vector2(-400.0f, -400.0f);
			
			
			Reset ( Director.Instance.CurrentScene );
			
			InputManager.Instance.DoubleTapDetected += HandleInputManagerInstanceDoubleTapDetected;
			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
			InputManager.Instance.TouchDownDetected += HandleInputManagerInstanceTouchDownDetected;
			InputManager.Instance.DragDetected += HandleInputManagerInstanceDragDetected;
			InputManager.Instance.TapDetected += HandleInputManagerInstanceTapDetected;
		}
		
		// EVENT HANDLERS --------------------------------------------------------
		
		void HandleInputManagerInstanceTapDetected (object sender, BaseTouchEventArgs e)
		{
			// HACK this is a little hacky, because CardCrystallonEntites do this in their BeReleased() method
			// this covers the case where the player just taps on the piece, instead of pressing and holding,
			// to make sure the glow shuts off correctly
			if (lastEntityReleased is CardCrystallonEntity) {
				(lastEntityReleased as CardCrystallonEntity).HideGlow();
			}
		}
		
		void HandleInputManagerInstanceDragDetected (object sender, SustainedTouchEventArgs e)
		{
			if( GameScene.paused ) return;
			
//			var entity = GetEntityAtPosition( e.touchPosition );
			if (lastEntityReleased!=null) {
				if (lastEntityReleased is CubeCrystallonEntity) return;
				if (lastEntityReleased.GetType().ToString() == "Crystallography.GroupCrystallonEntity") {
					GroupCrystallonEntity g = lastEntityReleased as GroupCrystallonEntity;
					MemberType = g.MemberType;
				} else {
					MemberType = lastEntityReleased.GetType ();
				}
			} else {
				MemberType = null;
			}
			if(MemberType != null) {
				Console.WriteLine(MemberType + " " + lastEntityReleased.id + " selected");
			}
			if (lastEntityReleased != null) {
				Add (lastEntityReleased);
				EaseOut();
			}
		}
		
		/// <summary>
		/// Handles <c>InputManager.DoubleTapDetected</c>.
		/// </summary>
		/// <param name='sender'>
		/// InputManager instance
		/// </param>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.BaseTouchEventArgs"/>
		/// </param>
		void HandleInputManagerInstanceDoubleTapDetected (object sender, EventArgs e)
		{
			if( GameScene.paused ) return;
			
			if( population > 1 ) {
				EaseIn ( true );
			}
			if (lastEntityReleased is GroupCrystallonEntity) {
				(lastEntityReleased as GroupCrystallonEntity).Break ();
			} 
		}
		
		/// <summary>
		/// Handles <c>InputManager.TouchJustUpDetected</c>.
		/// </summary>
		/// <param name='sender'>
		/// InputManager instance
		/// </param>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.BaseTouchEventArgs"/>
		/// </param>
		void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e)
		{
			if ( GameScene.paused ) return;
			
			if ( population > 0 && (easeState == EaseState.OUT || easeState == EaseState.MOVING_OUT) ) {
				EaseIn();
			}
			
//			selectionMarker.Visible = false;
//			this.getNode().RemoveChild(selectionMarker, false);
			Scheduler.Instance.Unschedule(this.getNode(), AddParticle);
			
			// HACK this is a little hacky, because CardCrystallonEntites do this in their BeReleased() method
			// this covers the case where the player just taps on the piece, instead of pressing and holding,
			// to make sure the glow shuts off correctly
//			if (lastEntityReleased is CardCrystallonEntity) {
//				(lastEntityReleased as CardCrystallonEntity).HideGlow();
//			}
		}
		
		/// <summary>
		/// Handles <c>InputManager.TouchJustDownDetected</c>.
		/// </summary>
		/// <param name='sender'>
		/// Input Manager instance
		/// </param>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.BaseTouchEventArgs"/>
		/// </param>
		void HandleInputManagerInstanceTouchJustDownDetected (object sender, BaseTouchEventArgs e)
		{
			if ( GameScene.paused ) return;
			
			var entity = GetEntityAtPosition( e.touchPosition );
			lastEntityReleased = entity as AbstractCrystallonEntity;
			if (lastEntityReleased is AbstractCrystallonEntity) {
				lastEntityReleased.playSound();
			}
			if (lastEntityReleased is CardCrystallonEntity) {
				(lastEntityReleased as CardCrystallonEntity).ShowGlow();
			}
			
			AddParticle(0.0f);
			Scheduler.Instance.Schedule(this.getNode(), AddParticle, 0.1f, false, 1);
			
			
//			selectionMarker.Position = -0.5f*selectionMarker.CalcSizeInPixels();
//			this.getNode().AddChild(selectionMarker);
//			selectionMarker.Visible = true;
			    
//			MemberType = (entity!=null) ? entity.GetType() : null;	// -------------- Cards or Cubes?
//			if (entity != null) {
//				Add (entity);
//				EaseOut();
//			}
		}
		
		/// <summary>
		/// Handles <c>InputManager.TouchDownDetected</c>.
		/// </summary>
		/// <param name='sender'>
		/// InputManager instance
		/// </param>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.BaseTouchEventArgs"/>
		/// </param>
		void HandleInputManagerInstanceTouchDownDetected (object sender, SustainedTouchEventArgs e)
		{
			if ( GameScene.paused ) return;
			setPosition( e.touchPosition );
			if ( population > 0 ) {
				if (velocity < MAXIMUM_PICKUP_VELOCITY) {
					SnapTo();
				}
			}
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		public override GroupCrystallonEntity Add (ICrystallonEntity pEntity)
		{
			return base.Add (pEntity);
		}
		
		public override AbstractCrystallonEntity BeReleased (Vector2 position)
		{
			// SelectionGroup should never be released.
			return null;
		}
		
		public override void RemoveAll ()
		{
			base.RemoveAll ();
			foreach ( var puck in pucks ) {
				puck.Children.Clear();
			}
		}
		
		public override void Update (float dt)
		{	
			base.Update(dt);
			
			velocity = Vector2.Distance( getPosition(), lastPosition ) / dt;
//			if (velocity>0) {
//				Console.WriteLine(velocity);
//			}
			lastPosition = getPosition();
		}
		
		// METHODS ---------------------------------------------------------------
		
		public void AddParticle(float dt) {
			Support.ParticleEffectsManager.Instance.AddSelectionMarkerParticle( this, Colors.White);
		}
			
		/// <summary>
		/// Cute li'l animation that runs when player lets go of the SelectionGroup.
		/// If the group had 3 members, we also test to see whether it was a valid match.
		/// </summary>
		private void EaseIn( bool pForceBreak = false ) {
			easeState = EaseState.MOVING_IN;
			
			for (int i=0; i<MAX_CAPACITY; i++) {
				_pucks[i].StopAllActions();
			}
			if ( population == 1 ) {	// ------------------------------------------------------ move single object to the center
				Sequence sequence = new Sequence();
				sequence.Add( new MoveTo( Vector2.Zero, 0.2f )
							{ Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
				foreach (AbstractCrystallonEntity e in members) {
					if ( e != null ) {
						_pucks[Array.IndexOf(_pucks, e.getNode ().Parent)].RunAction( sequence );
					}
				}
			} else {	// ---------------------------------------------------------------------- move multiple objects to their positional offsets
				foreach (AbstractCrystallonEntity e in members) {
					if ( e != null ) {
						var offset = ( e.getAttachOffset( Array.IndexOf(_pucks, e.getNode().Parent) ) );
						Sequence sequence = new Sequence();
						sequence.Add ( new MoveTo( offset, 0.2f ) 
						             { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
						_pucks[Array.IndexOf(_pucks, e.getNode().Parent)].RunAction( sequence );
					}
				}
			}
			Sequence releaseDelay = new Sequence();
			if ( population > 0 ) {
				releaseDelay.Add ( new DelayTime( 0.25f ) );
				releaseDelay.Add ( new CallFunc( () => {
					Release( this, pForceBreak );
					easeState = EaseState.IN;
				} ) );
			}
//			releaseDelay.Add ( new CallFunc( () => {
//				easeState = EaseState.IN;
//			} ) );
			_scene.RunAction( releaseDelay );
		}
		
		/// <summary>
		/// Cute li'l animation that runs when the player touches an object on the screen, making it part of the SelectionGroup.
		/// This is to correct for the player's sausage-like fingers obscuring the current selection.
		/// </summary>
		private void EaseOut() {
			easeState = EaseState.MOVING_OUT;
			
			for (int i=0; i<MAX_CAPACITY; i++) {
				_pucks[i].StopAllActions();
			}
			Sequence sequence = new Sequence();
			sequence.Add( new MoveTo( new Vector2(0.5f, 10.0f+EASE_DISTANCE), 0.2f)
			            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			_pucks[0].RunAction( sequence );
			
			sequence = new Sequence();
			if ( MemberType.ToString() == "Crystallography.CardCrystallonEntity") { 
				sequence.Add( new MoveTo( new Vector2(-EASE_DISTANCE-10.0f, 20.5f), 0.2f)
				            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			} else {
				sequence.Add( new MoveTo( new Vector2(-EASE_DISTANCE, EASE_DISTANCE + 40.5f), 0.2f)
				            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			}
			_pucks[1].RunAction( sequence );
			
			sequence = new Sequence();
			if ( MemberType.ToString() == "Crystallography.CardCrystallonEntity") { 
				sequence.Add( new MoveTo( new Vector2(EASE_DISTANCE+10.0f, 20.5f), 0.2f)
			           	 	{ Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			} else {
				sequence.Add( new MoveTo( new Vector2(EASE_DISTANCE, EASE_DISTANCE + 40.5f), 0.2f)
			           	 	{ Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
				sequence.Add( new CallFunc( () => {easeState = EaseState.OUT;} ) );
			}
			_pucks[2].RunAction( sequence );
		}
		
		/// <summary>
		/// Gets the first ICrystallonEntity it finds at the given position.
		/// </summary>
		/// <returns>
		/// The ICrystallonEntity.
		/// </returns>
		/// <param name='position'>
		/// Screen Position in pixels.
		/// </param>
		protected ICrystallonEntity GetEntityAtPosition( Vector2 position ) {
			var lowerLeft = Vector2.Zero;
			var upperRight = Vector2.Zero;
			System.Collections.ObjectModel.ReadOnlyCollection<ICrystallonEntity> allEntities = GameScene.getAllEntities();
			foreach (ICrystallonEntity e in allEntities) {
				if (e is ButtonEntity) continue; // HACK this should probably be done more intelligently
				if (e == null) continue;	// e IS NOT ACTUALLY A THING -- IGNORE (BUT IF THIS EVER HAPPENS, IT'S PROBS A BUG)
				if ( e is NodeCrystallonEntity ) { // ----------------------------- e DESCENDS FROM NodeCrystallonEntity, LIKE GROUPS DO
					PhysicsBody body = e.getBody();
					if (body == null) continue; // e IS SINGLE POINT IN SPACE -- IGNORE
					lowerLeft = body.AabbMin * GamePhysics.PtoM;
					upperRight = body.AabbMax * GamePhysics.PtoM;
				} else if (e is SpriteTileCrystallonEntity) { // ----------------- e DESCENDS FROM SpriteTileCrystallonEntity (rarer)
					Node puck = e.getNode();
					Vector2 halfDimensions = new Vector2((e as SpriteTileCrystallonEntity).Width/2f, (e as SpriteTileCrystallonEntity).Height/2f);
					lowerLeft = (e as SpriteTileCrystallonEntity).getPosition() - halfDimensions;
					upperRight = (e as SpriteTileCrystallonEntity).getPosition() + halfDimensions;
				} 
				if (position.X >= lowerLeft.X && position.Y >= lowerLeft.Y &&
			    	position.X <= upperRight.X && position.Y <= upperRight.Y) {
					return e;
				}
			}
			return null; // PLAYER TAPPED ON EMPTY SPACE, LIKE A N00B
		}
		
		/// <summary>
		/// Called if the three group members form a successful match. If there are no possible matches left, end the current level
		/// </summary>
		public void GroupComplete() {
			Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "threetiles.wav");
			if ( MemberType == typeof(CardCrystallonEntity) ) {
				EventHandler<CubeCompleteEventArgs> handler = CubeCompleteDetected;
				if ( handler != null ) {
					handler( this, new CubeCompleteEventArgs {
						members = Array.ConvertAll( this.members, item => (CardCrystallonEntity)item )
					});
				}
			} else {
				EventHandler<CubeGroupCompleteEventArgs> handler = CubeGroupCompleteDetected;
				if ( handler != null ) {
					handler( this, new CubeGroupCompleteEventArgs {
						members = Array.ConvertAll( this.members, item => (CubeCrystallonEntity)item )
					});
				}
			}
//			CardManager.Instance.MakeUnavailable( Array.ConvertAll( members, item => (CardCrystallonEntity)item ) );
//			if ( CardManager.Instance.MatchesPossible() == false ) {
//				Sequence sequence = new Sequence();
//				sequence.Add( new DelayTime( 1.0f ) );
//				sequence.Add( new CallFunc( () => (_scene as GameScene).goToNextLevel() ) );
//				_scene.RunAction(sequence);
//			}
		}
		
		/// <summary>
		/// Called if three group members do not form a successful match. Breaks them up.
		/// </summary>
		public void GroupFailed() {
			Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "wrong.wav");
			EventHandler handler = CubeFailedDetected;
			if ( handler != null ) {
				handler( this, null );
			}
			this.Break();
		}
		
		/// <summary>
		/// Release the currently selected ICrystallonEntity from the SelectionGroup.
		/// </summary>
//		public ICrystallonEntity Release () {
		public override AbstractCrystallonEntity Release ( AbstractCrystallonEntity e, bool pForceBreak = false )
		{
			AbstractCrystallonEntity entity = e;
			if (pForceBreak) {
				lastEntityReleased = null;
				return ReleaseSingle ( entity );
			} else if (population == 1) {
				entity = members[0] as AbstractCrystallonEntity;
				return lastEntityReleased = ReleaseSingle ( entity );
			}
			bool isComplete = false;
			if ( !pForceBreak ) {	// --------------------------- don't bother testing if Break forced
				if ( population  == MAX_CAPACITY ) { // -------------------------------------------------------- EVALUATE CUBES!
//					if ( MemberType == typeof(CubeCrystallonEntity) ) {
//						ICrystallonEntity[] a = { (members[0] as CubeCrystallonEntity).Top,
//													(members[1] as CubeCrystallonEntity).Right,
//													(members[2] as CubeCrystallonEntity).Left };
//						if (!QualityManager.Instance.EvaluateMatch( a, true )) { // TOP:LEFT + LEFT:TOP
//							GroupFailed ();
//							return null;
//						}
//						GroupComplete();
//					} else {
						if (QualityManager.Instance.EvaluateMatch( members, true ) ) {
							GroupComplete();
							isComplete = true;
						} else {
							GroupFailed();
							return null;
						}
//					}
				}
			}
			if ( entity is NodeCrystallonEntity ) {
				return lastEntityReleased = ReleaseGroup ( isComplete, pForceBreak );
			}
			return lastEntityReleased = null;
		}
		
		/// <summary>
		/// Called if the SelectionGroup only has 1 CardCrystallonEntity attached.
		/// </summary>
		/// <returns>
		/// The CardCrystallonEntity
		/// </returns>
//		private CardCrystallonEntity ReleaseSingle() {
		protected override AbstractCrystallonEntity ReleaseSingle ( AbstractCrystallonEntity e )
		{
			return base.ReleaseSingle( e );
		}
		
		/// <summary>
		/// Called if the SelectionGroup has 2+ AbstractCrystallonEntities attached. Releases them as children of a GroupCrystallonEntity
		/// </summary>
		/// <returns>
		/// The GroupCrystallonEntity
		/// </returns>
		protected GroupCrystallonEntity ReleaseGroup( bool pComplete, bool pForceBreak = false ) {
			var spawnPos = this.getPosition();
//			var g = GroupManager.Instance.spawn(spawnPos.X, spawnPos.Y, pComplete);
			var g = GroupManager.Instance.spawn(spawnPos.X, spawnPos.Y, members, pComplete);
			g.complete = pComplete;
//			foreach (AbstractCrystallonEntity e in members) {
//				g.Add(e);
//			}
			RemoveAll();
			Array.Clear(members,0,MAX_CAPACITY);
			g.Update(0); //HACK prevents group images from being at origin for 1 frame.
			g.setVelocity(1.0f, GameScene.Random.NextAngle());
			if (pForceBreak) {
				g.Break();
			}
			return g;
		}
		
		/// <summary>
		/// Reset the SelectionGroup
		/// </summary>
		/// <param name='pScene'>
		/// Instance of the scene to be a part of after resetting.
		/// </param>
		public void Reset( Scene pScene ) {
			RemoveAll();
//			potentialMembers.Clear();
			MemberType = null;
			lastPosition = getPosition();
			lastEntityReleased = null;
			_scene = pScene;
			easeState = EaseState.IN;
		}
		
		/// <summary>
		/// Snaps nearby objects onto the SelectionGroup's available nodes.
		/// </summary>
		private void SnapTo() {
			if ( population < MAX_CAPACITY ) {
				float distance;
				float closestDistance = float.MaxValue;
				ICrystallonEntity closest = null;
				//TODO Reduce the total number of entities we run the distance check on!!! Use a trigger area or something.
				foreach ( ICrystallonEntity e in GameScene.getAllEntities() )
				{
					if (e == null) {
						continue; // --------------------------------- e IS NOT A THING -- (IF THIS HAPPENS, IT'S PROBS A BUG)
					}
//					if (Array.IndexOf(members, e) != -1) {
//						continue; // --------------------------------- e IS ALREADY PART OF THE GROUP -- IGNORE IT
//					}
					if (e == this) {
						continue; // --------------------------------- e IS THE SELECTION GROUP ITSELF -- FIND A WAY TO FILTER THIS OUT, LATER...
					}
//					if ( this.MemberType != e.GetType () ) {
//						if (e.GetType().ToString() != "Crystallography.GroupCrystallonEntity") {	// types don't match & not a group
//							continue;
//						} else {
//							var g = e as GroupCrystallonEntity;
//							if ( g.MemberType != this.MemberType ) { // ----------------- type doesn't match group members' type
//								continue;
//							}
//						}
//					}
//					if ( e is GroupCrystallonEntity && !(e is CubeCrystallonEntity) ) {
//						var g = e as GroupCrystallonEntity;
//						if ( g.MemberType != this.MemberType ) {
//							continue;
//						}
//					}
//					if ( e.GetType() != this.MemberType ){
//						continue;
//					}
					if ( AppMain.ORIENTATION_MATTERS ) {
						bool c = e.CanBeAddedTo(this);
						if(false == c) {
							continue;
						}
//						if ( e is GroupCrystallonEntity ) {
//							if ( !(e is CubeCrystallonEntity) ) {
//								bool collision = false;
//								var g = e as GroupCrystallonEntity;
//								for (int i=0; i<g.pucks.Length; i++) {
//									if( g.pucks[i].Children.Count > 0 && this.pucks[i].Children.Count > 0) {
//										collision = true;
//										break;	// ----------------- found an overlapping group member...
//									}
//								}
//								if (collision) {
//									continue;	// ----------------- e IS A GROUP WITH MEMBERS THAT OVERLAP WITH SELECTION GROUP -- IGNORE
//								}
//							}
//						} else {
//							int orientation = (e as SpriteTileCrystallonEntity).getOrientation();
//							if ( _pucks[orientation].Children.Count != 0 ) {
//								continue;	// --------------------------- e IS OF AN ORIENTATION THAT IS ALREADY IN THE GROUP
//							}
//						}
					}
					distance = Vector2.Distance( getPosition(), e.getPosition() );
					if (closestDistance > distance) {
						closestDistance = distance;
						closest = e;
					}
				}
				
				if ( closestDistance < SNAP_DISTANCE ) {
					( closest as AbstractCrystallonEntity ).pickupLocation = closest.getPosition();
					( closest as AbstractCrystallonEntity).playSound();
					if ( closest is CardCrystallonEntity ) {
						( closest as CardCrystallonEntity ).ShowGlow();
					}
					Add (closest);
				}
			}
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
		
		~SelectionGroup() {
			lastEntityReleased = null;
			Instance = null;
#if DEBUG
			Console.WriteLine("SelectionGroup deleted.");
#endif
		}
		
	}
	
	// HELPER CLASSES ------------------------------------------------------------------------------
	
	/// <summary>
	/// CubeCompleteEvent arguments.
	/// </summary>
	public class CubeCompleteEventArgs : EventArgs {
		public CardCrystallonEntity[] members;
	}
	public class CubeGroupCompleteEventArgs : EventArgs {
		public CubeCrystallonEntity[] members;
	}
	public enum EaseState {
		IN = 0,
		OUT = 1,
		MOVING_IN = 2,
		MOVING_OUT = 3
	}
}
