using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public class GroupCrystallonEntity : NodeCrystallonEntity {
		
		public enum POSITIONS {TOP=0, LEFT=1, RIGHT=2};
		public AbstractCrystallonEntity[] members;
		protected int _maxMembers;
		protected Node[] _pucks;
		private int _numMembers;
		
		protected float _keepOnScreenTimer;
		
		public static event EventHandler BreakDetected;
		
		
//		public static event EventHandler BreakDetected;
		
		// GET & SET -------------------------------------------------------------
		
		public bool complete;
		
		/// <summary>
		/// Use this to get the number of members of this group!
		/// </summary>
		/// <value>
		/// <c>int</c> population.
		/// </value>
		public int population {
			get { return _numMembers; }
		}
		
		public int maxPopulation {
			get { return _maxMembers; }
		}

		public Node[] pucks { get {return _pucks;} }
		
		public System.Type MemberType {get; set;}
		
		// CONSTRUCTOR -----------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.GroupCrystallonEntity"/> class.
		/// </summary>
		/// <param name='pScene'>
		/// Instance of the parent scene.
		/// </param>
		/// <param name='pGamePhysics'>
		/// Instance of <c>GamePhysics</c>
		/// </param>
		/// <param name='pShape'>
		/// <see cref="Sce.PlayStation.HighLevel.Physics2D.PhysicsShape"/>
		/// </param>
		/// <param name='pMaxMembers'>
		/// <c>int</c> Maximum allowed population. Probs 3. 0 = no limit.
		/// </param>
		public GroupCrystallonEntity(Scene pScene, GamePhysics pGamePhysics, PhysicsShape pShape = null, 
		                             int pMaxMembers=0, bool pComplete = false) 
																				: base( pScene, pGamePhysics, pShape ) {
			_maxMembers = pMaxMembers;
			_numMembers = 0;
			members = new AbstractCrystallonEntity[_maxMembers];
			_pucks = new Node[_maxMembers];
			for (int i=0; i < _maxMembers; i++) {
				AddPuck(i);
			}
			complete = pComplete;
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		/// <summary>
		/// Adds the members of this group to the specified GroupCrystallonEntity descendent, 
		/// and schedules this newly emptied group's destruction.
		/// </summary>
		/// <returns>
		/// null -- because this group is scheduled for destruction
		/// </returns>
		/// <param name='pGroup'>
		/// The destination group
		/// </param>
		public override AbstractCrystallonEntity BeAddedToGroup (GroupCrystallonEntity pGroup)
		{
//			if( AppMain.ORIENTATION_MATTERS == false ) {
//				if ( pGroup.population == 1 ) {
//					AbstractCrystallonEntity piece = (AbstractCrystallonEntity)pGroup.Remove(pGroup.members[0]);
//					(piece as SpriteTileCrystallonEntity).setOrientation(2);
//					pGroup.Attach( piece );
//				}
//			}
			for ( int i=0; i<members.Length; i++) {
				AbstractCrystallonEntity e = members[i];
				if ( e != null ) {
					pGroup.Attach( e );
				}
			}
			pGroup.PostAttach( this );
			// CLEAR MEMBERS LIST & SCHEDULE DELETION OF EMPTIED GROUP
			RemoveAll();
			getNode().Schedule((dt) => { GroupManager.Instance.Remove(this, true); } );
			return null;
		}
		
		/// <summary>
		/// Reattaches this group to the scene.
		/// </summary>
		/// <returns>
		/// This group.
		/// </returns>
		/// <param name='position'>
		/// Position.
		/// </param>
		public override AbstractCrystallonEntity BeReleased( Vector2 pPosition ) {
			GroupManager.Instance.Add( this );
			setBody(_physics.RegisterPhysicsBody(_physics.SceneShapes[(int)GamePhysics.BODIES.Cube], 0.1f, 0.01f, pPosition));
			setVelocity(1.0f, GameScene.Random.NextAngle());

			addToScene();
			
			_keepOnScreenTimer = 0.0f;
			this.getNode().ScheduleInterval( KeepOnScreen, 0.5f, 1);
			
			return this;
		}
		
		public override void BeTapped ( float delay = 0.0f)
		{
			foreach (AbstractCrystallonEntity member in members) {
				if (member == null) continue;
				member.BeTapped( delay );
				delay += 0.2f;
			}
		}
		
		public override AbstractCrystallonEntity BeSelected ( float delay = 0.0f )
		{
			foreach (AbstractCrystallonEntity member in members) {
				if (member == null) continue;
				member.BeSelected( delay );
				delay += 0.2f;
			}
			return this;
		}
		
		/// <summary>
		/// Determines whether this instance can be added to the specified pGroup.
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance can be added to the specified pGroup; otherwise, <c>false</c>.
		/// </returns>
		/// <param name='pGroup'>
		/// If set to <c>true</c> p group.
		/// </param>
		public override bool CanBeAddedTo ( GroupCrystallonEntity pGroup )
		{
			bool okToSnap = false;
//			if ( pGroup.MemberType.IsAssignableFrom(this.MemberType) ) { // GROUPS' MEMBER TYPES ARE COMPATIBLE
			if ( pGroup.CheckMemberTypeCompatability(this) ) {
				if( AppMain.ORIENTATION_MATTERS ) { // -------------------- ORIENTATION TEST
					okToSnap = true;
					for ( int i=0; i < _maxMembers; i++ ) {
						if (_pucks[i].Children.Count > 0 && pGroup._pucks[i].Children.Count > 0) {
							okToSnap = false;
							break;
						}
					}
				} else {
					okToSnap = ( this.population + pGroup.population <= pGroup.maxPopulation );
				}
			}
			return okToSnap;
		}
		
		/// <summary>
		/// Play the sounds of all group members in sequence: top, left, right.
		/// </summary>
		public override void playSound ()
		{
			//TODO This doesn't work, but it needs to. At least it doesn't crash for some reason...
//			Sequence sequence = new Sequence();
			foreach ( AbstractCrystallonEntity member in members ) {
				if (member == null) {
					continue;
				}
			}
		}
		
		/// <summary>
		/// Removes the group from the scene. If doCleanup is true, it gets the object and any children ready for deletion.
		/// </summary>
		/// <param name='doCleanup'>
		/// Do cleanup.
		/// </param>
		public override void removeFromScene (bool doCleanup)
		{
			if (doCleanup) {
				foreach ( var member in members ) {
					if( member != null ) {
						member.removeFromScene( doCleanup );
					}
				}
				members = null;
				for( int i=0; i<_pucks.Length; i++ ) {
					_pucks[i].RemoveAllChildren( doCleanup );
					_pucks[i] = null;
				}
				_pucks = null; 
				_numMembers = 0;
			}
			base.removeFromScene (doCleanup);
		}
		
		/// <summary>
		/// The once-per-frame update method.
		/// </summary>
		/// <param name='dt'>
		/// <c>float</c> elapsed time since last frame.
		/// </param>
		public override void Update (float dt)
		{
			base.Update(dt);
			
			if (getBody() != null) {
				getNode().Position = getBody().Position * GamePhysics.PtoM;
			}
		}
		
		// METHODS ---------------------------------------------------------------
		
		/// <summary>
		/// Add the specified <c>ICrystallonEntity</c> to this group.
		/// </summary>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.ICrystallonEntity"/>
		/// </param>
		public virtual GroupCrystallonEntity Add(ICrystallonEntity pEntity) {
			if ( pEntity != null ) {
//				if ( population == 0 && !(this is SelectionGroup) ) {
//				if ( population == 0 ) {
//					MemberType = pEntity.GetType();
//					if ( MemberType == typeof(WildCardCrystallonEntity) ) {
//						MemberType = typeof(CardCrystallonEntity);
//					}
//				}
				if( pEntity.CanBeAddedTo(this) ){
					pEntity.BeAddedToGroup(this);
				}
			}
			return this;
		}
		
//		public GroupCrystallonEntity AddCube( CubeCrystallonEntity pEntity ) {
//			// CHECK FOR SPACE IN GROUP
//			if (population + 1 > _maxMembers) {
//				return this;
//			}
//			IncreaseSlots( 1 );
//			members[GetFreeSlot()] = pEntity;
//			//Attach (pEntity);
//			return this;
//		}
//		
//		/// <summary>
//		/// Add the specified <c>SpriteTileCrystallonEntity</c> to the group.
//		/// </summary>
//		/// <returns>
//		/// This <c>GroupCrystallonEntity</c>
//		/// </returns>
//		/// <param name='pEntity'>
//		/// <see cref="Crystallography.SpriteTileCrystallonEntity"/>
//		/// </param>
//		public GroupCrystallonEntity AddSpriteTile(SpriteTileCrystallonEntity pEntity) {
//			// CHECK FOR SPACE IN GROUP
//			if (population + 1 > _maxMembers) {
//				return this;
//			}
//			IncreaseSlots( 1 );
//			members[GetFreeSlot()] = pEntity;
//			//Attach(pEntity);
//			return this;
//		}
//		
//		/// <summary>
//		/// Add all members of a <c>GroupCrystallonEntity</c> to this group.
//		/// </summary>
//		/// <returns>
//		/// This <c>GroupCrystallonEntity</c>
//		/// </returns>
//		/// <param name='pGroupEntity'>
//		/// <see cref="Crystallography.GroupCrystallonEntity"/>
//		/// </param>
//		public GroupCrystallonEntity AddGroup( GroupCrystallonEntity pGroupEntity ) {
//			// CHECK FOR SPACE IN GROUP
//			if (population + pGroupEntity.population > _maxMembers) {
//				return this;
//			}
//			if (pGroupEntity.complete) {
//				IncreaseSlots( 1 );
//				members[GetFreeSlot()] = pGroupEntity;
//				//Attach(pGroupEntity);
//				return this;
//			} else {
//				IncreaseSlots(pGroupEntity.members.Length);
//				for ( int i=0; i<pGroupEntity.members.Length; i++) {
//					AbstractCrystallonEntity e = pGroupEntity.members[i] as AbstractCrystallonEntity;
//					if ( e != null ) {
//						int index = GetFreeSlot();
//						members[index] = e;
//						//Attach(e);	// LAZY TYPECASTING... CLEAN UP LATER?
//					}
//				}
//				GroupManager.Instance.Remove( pGroupEntity );
//				return this;
//			}
//		}
		
		/// <summary>
		/// Add a puck, i.e. a root node, to help position entities relative to each other in the group.
		/// </summary>
		/// <param name='pIndex'>
		/// <c>int</c> which puck is this? Probs 0, 1 or 2.
		/// </param>
		private void AddPuck( int pIndex ) {
			_pucks[pIndex] = new Node();
			getNode().AddChild(_pucks[pIndex]);
		}
		
		/// <summary>
		/// Actually reparent a single entity to this group.
		/// </summary>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.AbstractCrystallonEntity"/>
		/// </param>
		public void Attach( AbstractCrystallonEntity pEntity ) {
			IncreaseSlots( 1 );
			members[GetFreeSlot()] = pEntity;
			
			//create a child-node, give it a positional offset, make pEntity a child of child-node
			int index = GetOrientationIndex( pEntity );
			if ( _pucks[index] == null ) {
				AddPuck(index);
			}
			Node puck = _pucks[index];
			pEntity.attachTo(puck);
			pEntity.getNode().Position *= 0;
			if ( !AppMain.ORIENTATION_MATTERS ) {
				if ((pEntity is CubeCrystallonEntity) == false ) {
					QualityManager.Instance.SetQuality( pEntity, "QOrientation", index );
				}
			}
			_numMembers++;
		}
		
		/// <summary>
		/// This is primarily used for positioning the group's pucks
		/// </summary>
		/// <param name='pEntity'>
		/// P entity.
		/// </param>
		public virtual void PostAttach( AbstractCrystallonEntity pEntity ) {
			if ( population  > 1 ) {
				foreach( AbstractCrystallonEntity e in members ) {
					if( e != null) {
						int puckIndex = Array.IndexOf(_pucks, e.getNode().Parent);
						_pucks[puckIndex].Position = e.getAttachOffset( puckIndex );
					}
				}
			} else { // JUST CENTER THE OBJECT IF THERE'S ONLY ONE.
				foreach (var puck in _pucks) {
					puck.Position *= 0;
				}
			}
		}
		
		
		/// <summary>
		/// Break this group into its component objects.
		/// </summary>
		public virtual void Break() {
			EventHandler handler = BreakDetected;
			if (handler != null ) {
				handler( this, null );
			}
			
			Support.SoundSystem.Instance.Play(LevelManager.Instance.SoundPrefix + "break.wav");
			
			Node puck;
			for (int i=members.Length-1; i>=0; i--) {
				if ( members[i] != null ) {
					var e = members[i];
					puck = Array.Find( _pucks, (obj) => obj == e.getNode().Parent );
					var launchVelocity = puck.Position.Normalize();
					Release( e, true );
					e.setVelocity( launchVelocity );
					e.setPosition( e.getPosition() + launchVelocity*10);
				}
			}
			
			if (this is SelectionGroup == false) { 
				GroupManager.Instance.Remove(this, true);
			} else {
				RemoveAll();
			}
		}
		
		public bool CheckMemberTypeCompatability( AbstractCrystallonEntity pEntity ) {
			// NO CURRENT MEMBER TYPE
			if (MemberType == null) {
				if ( typeof(GroupCrystallonEntity).IsAssignableFrom(pEntity.GetType()) ) { // ------- HANDLE GROUP-TYPE OBJECTS
					MemberType = (pEntity as GroupCrystallonEntity).MemberType;
				} else if ( typeof(CardCrystallonEntity).IsAssignableFrom(pEntity.GetType()) ) { // - HANDLE CARD-TYPE OBJECTS
					MemberType = typeof(CardCrystallonEntity);
				}
			} else {
				Type t = null;
				if ( typeof(GroupCrystallonEntity).IsAssignableFrom(pEntity.GetType()) ) { // ------- HANDLE GROUP-TYPE OBJECTS
					t = (pEntity as GroupCrystallonEntity).MemberType;
				} else if ( typeof(CardCrystallonEntity).IsAssignableFrom(pEntity.GetType()) ) { // - HANDLE CARD-TYPE OBJECTS
					t = typeof(CardCrystallonEntity);
				}
				return this.MemberType.IsAssignableFrom(t);
			}
			return true;
		}
		
		/// <summary>
		/// If an entity has a fixed orientation, this tells us what it is. If not, it just tells us what orientation to use.
		/// </summary>
		/// <returns>
		/// <c>int</c> The orientation index.
		/// </returns>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.AbstractCrystallonEntity"/>
		/// </param>
		private int GetOrientationIndex( AbstractCrystallonEntity pEntity ) {
			if (!AppMain.ORIENTATION_MATTERS) {
				return population;
			}
			if ( pEntity is GroupCrystallonEntity ) {
				if ( (pEntity as GroupCrystallonEntity).complete) {
					return population;
				}
			}
			int orientation = ( pEntity as SpriteTileCrystallonEntity ).getOrientation();
			int attachPosition = -1;
			attachPosition = orientation;
			return attachPosition;
		}
		
		/// <summary>
		/// Returns the index of the first <c>null</c> position in <c>members</c>. -1 if all positions contain something.
		/// </summary>
		private int GetFreeSlot() {
			for ( int i=0; i<members.Length; i++ ) {
				if (members[i] == null) {
					return i;
				}
			}
			return -1;
//			Array.FindIndex( members, (obj) => obj == null );
		}
		
		protected void KeepOnScreen (float dt) {
			_keepOnScreenTimer += dt;
			if ( false == GameScene.PlayBounds.IsInside( this.getPosition() ) ) {
				GroupManager.Instance.Teleport(this);
				_keepOnScreenTimer = -1.0f;
			} else if ( _keepOnScreenTimer > 2.0f ) {
				_keepOnScreenTimer = -1.0f;
			}
			
			if (_keepOnScreenTimer < 0.0f) {
				this.getNode().Unschedule(KeepOnScreen);
			}
		}
		
		/// <summary>
		/// Resize <c>members</c> and <c>_pucks</c> so the group can hold more members.
		/// </summary>
		/// <param name='pNumber'>
		/// <c>int</c> How many entities do we need to add to the group?
		/// </param>
		private void IncreaseSlots( int pNumber ) {
			int freeSlots = members.Length - _numMembers;
			if ( pNumber > freeSlots ) {
				int slotsNeeded = members.Length + (pNumber - freeSlots);
				if ( slotsNeeded >= _maxMembers && _maxMembers > 0) {	// CHECK FOR DEFIND MEMBER CAP
					Array.Resize( ref members, _maxMembers );
					Array.Resize( ref _pucks, _maxMembers );
				} else {
					Array.Resize( ref members, slotsNeeded );
					Array.Resize( ref _pucks, slotsNeeded );
				}
			}
		}
		
		/// <summary>
		/// Removes the <c>ICrystallonEntity</c> at the given index from <c>members</c>.
		/// </summary>
		/// <returns>
		/// <see cref="Crystallography.ICrystallonEntity"/>
		/// </returns>
		/// <param name='pIndex'>
		/// <c>int</c> the index of the entity to be removed.
		/// </param>
		protected ICrystallonEntity PopIndex( int pIndex ) {
			ICrystallonEntity e = members[pIndex];
			if ( pIndex == members.Length-1 ) {
				_numMembers--;
			} else {
				for (int i=pIndex; i<members.Length-1;i++) {
					members[i] = members[i+1];
				}
				_numMembers--;
			}
			members[pIndex] = null;
			//TODO Remove puck nodes (unless this is the SelectionGroup, keep those.)
			return e;
		}
		
		/// <summary>
		/// Release an entity from the Group.
		/// </summary>
		public virtual AbstractCrystallonEntity Release ( AbstractCrystallonEntity e, bool pForceBreak = false) {
			if ( e is SpriteTileCrystallonEntity ) {
				return ReleaseSingle (e as SpriteTileCrystallonEntity );
			} else if ( e is CubeCrystallonEntity ) { 
				return ReleaseSingle( e as CubeCrystallonEntity );
			} else {
				return null;
			}
		}
		
		/// <summary>
		/// Called we're only releasing a single entity.
		/// </summary>
		/// <returns>
		/// The CardCrystallonEntity
		/// </returns>
		protected virtual AbstractCrystallonEntity ReleaseSingle( AbstractCrystallonEntity pEntity ) {
			Remove( pEntity );
			return pEntity.BeReleased( this.getPosition() );
		}
		
		/// <summary>
		/// Remove the specified <c>ICrystallonEntity</c> from <c>members</c>. DOES NOT detach it from its parent puck.
		/// </summary>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.ICrystallonEntity"/>
		/// </param>
		protected ICrystallonEntity Remove( ICrystallonEntity pEntity ) {
			int index = Array.IndexOf(members, pEntity);
			if (index != -1) {
				return PopIndex (index);
			}
			return null;
		}
		
		/// <summary>
		/// Clear this group of all its members.
		/// </summary>
		public virtual void RemoveAll() {
			for( int i=members.Length-1; i>=0; i--) {
				if ( members[i] != null ) {
					Remove( members[i] );
				}
			}
		}
	}
}
