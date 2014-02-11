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
		public static float EASE_DISTANCE = 70.0f;
		public static float MAXIMUM_PICKUP_VELOCITY = 700.0f; //400.0f;
		
		private AbstractCrystallonEntity lastEntityReleased;
		private AbstractCrystallonEntity lastEntityTouched;
		private AbstractCrystallonEntity justDownPositionEntity;
		private Vector2 lastPosition;
		
		public event EventHandler CubeFailedDetected;
		
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
			Reset ( Director.Instance.CurrentScene );
			
			InputManager.Instance.DoubleTapDetected += HandleInputManagerInstanceDoubleTapDetected;
			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
			InputManager.Instance.TouchDownDetected += HandleInputManagerInstanceTouchDownDetected;
			InputManager.Instance.DragDetected += HandleInputManagerInstanceDragDetected;
			InputManager.Instance.TapDetected += HandleInputManagerInstanceTapDetected;
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS --------------------------------------------------------
		
		void HandleInputManagerInstanceTapDetected (object sender, BaseTouchEventArgs e)
		{
			if (lastEntityTouched != null ) {
				lastEntityTouched.BeTapped();
			}
		}
		
		void HandleInputManagerInstanceDragDetected (object sender, SustainedTouchEventArgs e)
		{
			if( GameScene.paused ) return;
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
			
			if( lastEntityTouched == null ) return;
			
			if ( population  > 1 ) {
				EaseIn(true);
			}
			if (lastEntityTouched is GroupCrystallonEntity) {
				(lastEntityTouched as GroupCrystallonEntity).Break();
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
			Scheduler.Instance.Unschedule(this.getNode(), AddParticle);
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
			
			lastEntityTouched = justDownPositionEntity = GetEntityAtPosition( e.touchPosition ) as AbstractCrystallonEntity;
			
			AddParticle(0.0f);
			Scheduler.Instance.Schedule(this.getNode(), AddParticle, 0.1f, false, 1);
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

			if (velocity < MAXIMUM_PICKUP_VELOCITY) {
				if ( InputManager.dragging ) {
					var entity = GetEntityAtPosition( e.touchPosition ) as AbstractCrystallonEntity;
					if (justDownPositionEntity != null) { // ------------------------------------------------ EDGE CASE: PLAYER TOUCHED DOWN ON A PIECE, BUT DRAGGED OFF OF IT
						if (justDownPositionEntity != entity ) { // -----------------------------             BEFORE WE ADDED IT TO THE SELECTION GROUP.
							if (entity != null) {
								Add (justDownPositionEntity); // -------------------------------------------- RARE:      PLAYER IS TOUCHING A DIFFERENT VALID PIECE BEFORE WE RESOLVED THE FIRST; ADD THE OLD ONE, RESOLVE THE NEW ONE BELOW
							} else {
								entity = justDownPositionEntity; // ----------------------------------------- COMMON:    PLAYER IS TOUCHING EMPTY SPACE; RESOLVE IT BELOW
							}
						}
						justDownPositionEntity = null;
					}
					
					if (entity != null) {
						if (entity is CubeCrystallonEntity) { // ------------------------------------------ IGNORE CUBES
							return; 
						} else if (entity.GetType().ToString() == "Crystallography.GroupCrystallonEntity") {
							var g = entity as GroupCrystallonEntity;
							MemberType = g.MemberType;
						} else {
							MemberType = entity.GetType ();
						}
						Add (entity);
					} 
					lastEntityTouched = entity;
				}
			}
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		/// <summary>
		///  DON'T CALL THIS FUNCTION ON SELECTION GROUP. JUST RETURNS A REFERENCE TO ITSELF.
		/// </summary>
		public override AbstractCrystallonEntity BeAddedToGroup (GroupCrystallonEntity pGroup) {
			// Selection Group should never be added to any other group.
			return this;
		}
		
		/// <summary>
		///  DON'T CALL THIS FUNCTION ON SELECTION GROUP. RETURNS NULL.
		/// </summary>
		public override AbstractCrystallonEntity BeReleased (Vector2 position) {
			// SelectionGroup should never be released.
			return null;
		}
		
		public override void PostAttach ( AbstractCrystallonEntity pEntity ) {
			pEntity.BeSelected();
			EaseOut();
		}
		
		public override void RemoveAll () {
			base.RemoveAll ();
			foreach ( var puck in pucks ) {
				puck.RemoveAllChildren(false);
			}
		}
		
		public override void Update (float dt) {	
			base.Update(dt);
			
			velocity = Vector2.Distance( getPosition(), lastPosition ) / dt;
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
				_pucks[i].StopActionByTag( 1 );
			}
			if ( population == 1 ) {	// ------------------------------------------------------ move single object to the center
				Sequence sequence = new Sequence(){ Tag=1 };
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
						Sequence sequence = new Sequence(){ Tag=1 };
						sequence.Add ( new MoveTo( offset, 0.2f ) 
						             { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
						_pucks[Array.IndexOf(_pucks, e.getNode().Parent)].RunAction( sequence );
					}
				}
			}
			Sequence releaseDelay = new Sequence(){ Tag=1 };
			if ( population > 0 ) {
				releaseDelay.Add ( new DelayTime( 0.25f ) );
				releaseDelay.Add ( new CallFunc( () => {
					Release( this, pForceBreak );
					MemberType = null;
					easeState = EaseState.IN;
				} ) );
			}
			_scene.RunAction( releaseDelay );
		}
		
		/// <summary>
		/// Cute li'l animation that runs when the player touches an object on the screen, making it part of the SelectionGroup.
		/// This is to correct for the player's sausage-like fingers obscuring the current selection.
		/// </summary>
		private void EaseOut() {
			easeState = EaseState.MOVING_OUT;
			
			for (int i=0; i<MAX_CAPACITY; i++) {
				_pucks[i].StopActionByTag( 1 );
			}
			Sequence sequence = new Sequence(){ Tag=1 };
			sequence.Add( new MoveTo( new Vector2(0.5f, 10.0f+EASE_DISTANCE), 0.2f)
			            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			_pucks[0].RunAction( sequence );
			
			sequence = new Sequence(){ Tag=1 };
			if ( typeof(CardCrystallonEntity).IsAssignableFrom(this.MemberType) ) {
				sequence.Add( new MoveTo( new Vector2(-EASE_DISTANCE-10.0f, 20.5f), 0.2f)
				            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			} else {
				sequence.Add( new MoveTo( new Vector2(-EASE_DISTANCE, EASE_DISTANCE + 40.5f), 0.2f)
				            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			}
			_pucks[1].RunAction( sequence );
			
			sequence = new Sequence(){ Tag=1 };
			if ( typeof (CardCrystallonEntity).IsAssignableFrom(this.MemberType) ) {
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
				if (e == null) continue;	// e IS NOT ACTUALLY A THING -- IGNORE (BUT IF THIS EVER HAPPENS, IT'S PROBS A BUG)
				if ( e is NodeCrystallonEntity ) { // ----------------------------- e DESCENDS FROM NodeCrystallonEntity, LIKE GROUPS DO
					PhysicsBody body = e.getBody();
					if (body == null) continue; // e IS SINGLE POINT IN SPACE -- IGNORE
					lowerLeft = body.AabbMin * GamePhysics.PtoM;
					upperRight = body.AabbMax * GamePhysics.PtoM;
				} else if (e is SpriteTileCrystallonEntity) { // ----------------- e DESCENDS FROM SpriteTileCrystallonEntity (rarer)
					Node puck = e.getNode();
					Vector2 halfDimensions = new Vector2((e as SpriteTileCrystallonEntity).Width/2.0f, (e as SpriteTileCrystallonEntity).Height/2f);
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
				entity = members[0];
				return lastEntityReleased = ReleaseSingle ( entity );
			}
			bool isComplete = false;
			if ( !pForceBreak ) {	// --------------------------- don't bother testing if Break forced
				if ( population  == MAX_CAPACITY ) { // -------------------------------------------------------- EVALUATE CUBES!
						if (QualityManager.Instance.CheckForMatch( this, true ) ) {
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
		
//		/// <summary>
//		/// Called if the SelectionGroup only has 1 CardCrystallonEntity attached.
//		/// </summary>
//		/// <returns>
//		/// The CardCrystallonEntity
//		/// </returns>
//		protected override AbstractCrystallonEntity ReleaseSingle ( AbstractCrystallonEntity e )
//		{
//			var entity = base.ReleaseSingle( e );
//			float angle = Support.GetAngle(lastPosition, getPosition());
//			Console.WriteLine(angle);
//			if ( angle != 0.0f ) {
//				entity.setVelocity( CardCrystallonEntity.DEFAULT_SPEED, angle );
//			}
//			return entity;
//		}
		
		/// <summary>
		/// Called if the SelectionGroup has 2+ AbstractCrystallonEntities attached. Releases them as children of a GroupCrystallonEntity
		/// </summary>
		/// <returns>
		/// The GroupCrystallonEntity
		/// </returns>
		protected GroupCrystallonEntity ReleaseGroup( bool pComplete, bool pForceBreak = false ) {
			var spawnPos = this.getPosition();
			var g = GroupManager.Instance.spawn(spawnPos.X, spawnPos.Y, members, pComplete);
			g.complete = pComplete;
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
			MemberType = null;
			lastPosition = getPosition();
			lastEntityReleased = null;
			lastEntityTouched = null;
			justDownPositionEntity = null;
			_scene = pScene;
			easeState = EaseState.IN;
		}

		
		// DESTRUCTOR ------------------------------------------------------------------------------
		
		~SelectionGroup() {
			lastEntityReleased = null;
			lastEntityTouched = null;
			justDownPositionEntity = null;
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
