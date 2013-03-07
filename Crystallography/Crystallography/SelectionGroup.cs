using System;
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
		protected static readonly float EASE_DISTANCE = 50.0f;
		
		private AbstractCrystallonEntity lastEntityReleased;
		
		public event EventHandler<CubeCompleteEventArgs> CubeCompleteDetected;
		public event EventHandler CubeFailedDetected;
		
//		protected static Input2.TouchData _touch;
//		protected Vector2 _currentTouchPos;
//		protected Vector2 _touchStartPos;
//		protected bool _isTouch;
//		protected bool _wasTouch;
		
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
		
		/// <summary>
		/// Update touch data. Should be called once per frame.
		/// </summary>
//		public void setTouch() {
//			_touch = Input2.Touch00;
//			_wasTouch = _isTouch;
//			_isTouch = _touch.Down;
//			var normalized = _touch.Pos;
//			_currentTouchPos = Director.Instance.CurrentScene.Camera.NormalizedToWorld(normalized);
//		}
		
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
		}
		
		// EVENT HANDLERS --------------------------------------------------------
		
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
			if ( population >0 ) {
				EaseIn();
			}
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
			var entity = GetEntityAtPosition( e.touchPosition );
			if (entity != null) {
				Add (entity);
				EaseOut();
			}
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
			setPosition( e.touchPosition );
			if ( population > 0 ) {
				SnapTo();
			}
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		public override GroupCrystallonEntity Add (ICrystallonEntity pEntity)
		{
			if (pEntity is AbstractCrystallonEntity) {
				(pEntity as AbstractCrystallonEntity).playSound();
			}
			return base.Add (pEntity);
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
			
//			var moved = _touchStartPos - _currentTouchPos;
//			var moved_distance = moved.SafeLength();
//			
//			// HANDLE INPUT
//			if ( _isTouch ) {	// ------------------------- OnNewTouch AND OnDrag
//				setPosition(_currentTouchPos);
//				if ( !_wasTouch ) {	// --------------------- OnNewTouch ONLY
//					_touchStartPos = _currentTouchPos;
//					var entity = GetEntityAtPosition(_touchStartPos);
//					if (entity != null) {
//						Add (entity);
//						EaseOut();
//					}
//				} else {	// ----------------------------- OnDrag ONLY
//					
//				}
//			} else if ( _wasTouch ) { // ------------------- OnTouchReleased
//				if ( population >0 ) {
//					EaseIn();
//				}
//			}
		}
		
		// METHODS ---------------------------------------------------------------
		
		/// <summary>
		/// Cute li'l animation that runs when player lets go of the SelectionGroup.
		/// If the group had 3 members, we also test to see whether it was a valid match.
		/// </summary>
		private void EaseIn( bool pForceBreak = false ) {
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
			releaseDelay.Add ( new DelayTime( 0.25f ) );
			releaseDelay.Add ( new CallFunc( () => Release( this, pForceBreak ) ) );
			_scene.RunAction( releaseDelay );
		}
		
		/// <summary>
		/// Cute li'l animation that runs when the player touches an object on the screen, making it part of the SelectionGroup.
		/// This is to correct for the player's sausage-like fingers obscuring the current selection.
		/// </summary>
		private void EaseOut() {
			for (int i=0; i<MAX_CAPACITY; i++) {
				_pucks[i].StopAllActions();
			}
			Sequence sequence = new Sequence();
			sequence.Add( new MoveTo( new Vector2(0.5f, 10.0f+EASE_DISTANCE), 0.2f)
			            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			_pucks[0].RunAction( sequence );
			
			sequence = new Sequence();
			sequence.Add( new MoveTo( new Vector2(-EASE_DISTANCE, 20.5f), 0.2f)
			            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
			_pucks[1].RunAction( sequence );
			
			sequence = new Sequence();
			sequence.Add( new MoveTo( new Vector2(EASE_DISTANCE, 20.5f), 0.2f)
			            { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
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
					if (e is GroupCrystallonEntity) {
						if( (e as GroupCrystallonEntity).complete ) {
							lastEntityReleased = e as AbstractCrystallonEntity;
							continue;	// e IS A COMPLETE CUBE -- IGNORE
						}
					}
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
			Support.SoundSystem.Instance.Play("cubed.wav");
			EventHandler<CubeCompleteEventArgs> handler = CubeCompleteDetected;
			if ( handler != null ) {
				handler( this, new CubeCompleteEventArgs {
					members = Array.ConvertAll( this.members, item => (CardCrystallonEntity)item )
				});
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
			Support.SoundSystem.Instance.Play("wrong.wav");
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
			lastEntityReleased = e;
			bool isComplete = false;
			if ( e is SpriteTileCrystallonEntity ) {
				return ReleaseSingle (e as SpriteTileCrystallonEntity );
			} else if ( !pForceBreak ) {	// --------------------------- don't bother testing if Break forced
				if ( population  == MAX_CAPACITY ) { // -------------------------------------------------------- EVALUATE CUBES!
					if (QualityManager.Instance.EvaluateMatch( members ) ) {
						GroupComplete();
						isComplete = true;
					} else {
						GroupFailed();
						return null;
					}
				}
			}
			if ( e is NodeCrystallonEntity ) {
				return ReleaseGroup ( isComplete, pForceBreak );
			}
			return null;
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
			var g = GroupManager.Instance.spawn(spawnPos.X, spawnPos.Y);
			g.complete = pComplete;
			foreach (AbstractCrystallonEntity e in members) {
				g.Add(e);
			}
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
			lastEntityReleased = null;
			_scene = pScene;
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
					if (Array.IndexOf(members, e) != -1) {
						continue; // --------------------------------- e IS ALREADY PART OF THE GROUP -- IGNORE IT
					}
					if (e == this) {
						continue; // --------------------------------- e IS THE SELECTION GROUP ITSELF -- FIND A WAY TO FILTER THIS OUT, LATER...
					}
#if ORIENTATION_MATTERS 
					if ( e is GroupCrystallonEntity ) {
						bool collision = false;
						var g = e as GroupCrystallonEntity;
						for (int i=0; i<g.pucks.Length; i++) {
							if( g.pucks[i].Children.Count > 0 && this.pucks[i].Children.Count > 0) {
								collision = true;
								break;
							}
						}
						if (collision) {
							continue;	// ----------------- e IS A GROUP WITH MEMBERS THAT OVERLAP WITH SELECTION GROUP -- IGNORE
						}
					} else {
						int orientation = e.getQualityVariant( "QOrientation" );
						if ( _pucks[orientation].Children.Count != 0 ) {
							continue;	// --------------------------- e IS OF AN ORIENTATION THAT IS ALREADY IN THE GROUP
						}
					}
#endif
					distance = Vector2.Distance( getPosition(), e.getPosition() );
					if (closestDistance > distance) {
						closestDistance = distance;
						closest = e;
					}
				}
				
				if ( closestDistance < SNAP_DISTANCE ) {
					Add (closest);
				}
			}
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
		
		~SelectionGroup() {
			lastEntityReleased = null;
			Instance = null;
		}
		
	}
	
	// HELPER CLASSES ------------------------------------------------------------------------------
	
	/// <summary>
	/// CubeCompleteEvent arguments.
	/// </summary>
	public class CubeCompleteEventArgs : EventArgs {
		public CardCrystallonEntity[] members;
	}
}
