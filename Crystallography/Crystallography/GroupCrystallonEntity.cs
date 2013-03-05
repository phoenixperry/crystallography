using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public class GroupCrystallonEntity : NodeCrystallonEntity {
		
		public enum POSITIONS {TOP=0, LEFT=1, RIGHT=2};
		public ICrystallonEntity[] members;
		protected int _maxMembers;
		protected Node[] _pucks;
		private int _numMembers;
		
		// GET & SET -------------------------------------------------------------
		
		public bool complete { get; protected set; }
		
		/// <summary>
		/// Use this to get the number of members of this group!
		/// </summary>
		/// <value>
		/// <c>int</c> population.
		/// </value>
		public int population {
			get { return _numMembers; }
		}

		public Node[] pucks { get {return _pucks;} }
		
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
			members = new ICrystallonEntity[_maxMembers];
			_pucks = new Node[_maxMembers];
			for (int i=0; i < _maxMembers; i++) {
				AddPuck(i);
			}
			complete = pComplete;
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		/// <summary>
		/// Play the sounds of all group members in sequence: top, left, right.
		/// </summary>
		public override void playSound ()
		{
			//TODO This doesn't work, but it needs to. At least it doesn't crash for some reason...
			Sequence sequence = new Sequence();
			sequence.Add( new CallFunc( () => ( members[0] as AbstractCrystallonEntity ).playSound() ) );
			sequence.Add ( new DelayTime( 0.5f ) );
			sequence.Add( new CallFunc( () => ( members[1] as AbstractCrystallonEntity ).playSound() ) );
			sequence.Add ( new DelayTime( 0.5f ) );
			sequence.Add( new CallFunc( () => ( members[2] as AbstractCrystallonEntity ).playSound() ) );
			getNode().RunAction( sequence );
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
					//TODO what is the GC protocol for nodes?
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
			
//			foreach (ICrystallonEntity e in members) {
				// updates to children?
//			}
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
			if (pEntity is SpriteTileCrystallonEntity) {
				return AddSpriteTile(pEntity as SpriteTileCrystallonEntity);
			} else if  (pEntity is GroupCrystallonEntity) {
				return AddGroup (pEntity as GroupCrystallonEntity);
			}
			return this;
		}
		
		/// <summary>
		/// Add the specified <c>SpriteTileCrystallonEntity</c> to the group.
		/// </summary>
		/// <returns>
		/// This <c>GroupCrystallonEntity</c>
		/// </returns>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.SpriteTileCrystallonEntity"/>
		/// </param>
		public GroupCrystallonEntity AddSpriteTile(SpriteTileCrystallonEntity pEntity) {
			IncreaseSlots( 1 );
//			members[GetOrientationIndex( pEntity )] = pEntity;
			members[GetFreeSlot()] = pEntity;
			Attach(pEntity);
			return this;
		}
		
		/// <summary>
		/// Add all members of a <c>GroupCrystallonEntity</c> to this group.
		/// </summary>
		/// <returns>
		/// This <c>GroupCrystallonEntity</c>
		/// </returns>
		/// <param name='pGroupEntity'>
		/// <see cref="Crystallography.GroupCrystallonEntity"/>
		/// </param>
		public GroupCrystallonEntity AddGroup( GroupCrystallonEntity pGroupEntity ) {
			IncreaseSlots(pGroupEntity.members.Length);
			for ( int i=0; i<pGroupEntity.members.Length; i++) {
				AbstractCrystallonEntity e = pGroupEntity.members[i] as AbstractCrystallonEntity;
				if ( e != null ) {
					int index = GetFreeSlot();
					members[index] = e;
					Attach(e);	// LAZY TYPECASTING... CLEAN UP LATER?
				}
			}
			GroupManager.Instance.Remove( pGroupEntity );
			return this;
		}
		
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
		/// Actually reparent an entity into this group.
		/// </summary>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.AbstractCrystallonEntity"/>
		/// </param>
		private void Attach( AbstractCrystallonEntity pEntity ) {
			//create a child-node, give it a positional offset, make pEntity a child of child-node
			int index = GetOrientationIndex( pEntity );
			if ( _pucks[index] == null ) {
				AddPuck(index);
			}
			Node puck = _pucks[index];
			pEntity.attachTo(puck);
			pEntity.getNode().Position *= 0;
#if( !ORIENTATION_MATTERS )
				QualityManager.Instance.SetQuality( pEntity, "QOrientation", index );
#endif
			_numMembers++;
			if ( this is SelectionGroup  == false ) {	// SelectionGroup has its own positioning code -- HACK This implementation is just sort of inelegant. Maybe abstract some of this out later?
				if ( population  > 1 ) {
					foreach( AbstractCrystallonEntity e in members ) {
//					for (int i=0; i<members.Length; i++ {
						if( e != null) {
//						if ( members[i] != null ) {
							int puckIndex = Array.IndexOf(_pucks, e.getNode().Parent);
//							int attachPosition = GetOrientationIndex( e );
							_pucks[puckIndex].Position = e.getAttachOffset( puckIndex );
						}
					}
				} else { // JUST CENTER THE OBJECT IF THERE'S ONLY ONE.
					puck.Position *= 0;
				}
			}
		}
		
		public void Break() {
			Node puck;
			for (int i=members.Length-1; i>=0; i--) {
				if ( members[i] != null ) {
					var e = members[i] as AbstractCrystallonEntity;
					puck = Array.Find( _pucks, (obj) => obj == e.getNode().Parent );
					var launchVelocity = puck.Position.Normalize();
					Release( e );
					e.setVelocity( launchVelocity );
				}
			}
			RemoveAll();
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
#if (!ORIENTATION_MATTERS)
				return population;
#endif
			string orientation = ( pEntity as SpriteTileCrystallonEntity ).getOrientation();
			int attachPosition = -1;
			switch (orientation) {
			case ("Top"):
					attachPosition = 0;
				break;
			case ("Left"):
					attachPosition = 1;
				break;
			case ("Right"):
					attachPosition = 2;
				break;
			default:
				break;
			}
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
			Array.FindIndex( members, (obj) => obj == null );
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
		public virtual AbstractCrystallonEntity Release ( AbstractCrystallonEntity e) {
			if ( e is SpriteTileCrystallonEntity ) {
				return ReleaseSingle (e as SpriteTileCrystallonEntity );
			} else {
				return null;
			}
//			if (population < 1) {
//					return null;
//			}
//			if (population < 2) {
//				return ReleaseSingle();
//			} else {
//				return ReleaseGroup();
//			}
		}
		
		/// <summary>
		/// Called we're only releasing a single entity.
		/// </summary>
		/// <returns>
		/// The CardCrystallonEntity
		/// </returns>
		protected virtual AbstractCrystallonEntity ReleaseSingle( AbstractCrystallonEntity pEntity ) {
#if !ORIENTATION_MATTERS
			if ( pEntity is CardCrystallonEntity ) {
				QOrientation.Instance.Apply(pEntity,0);
			}
#endif
			Remove (pEntity);
			if(complete) {
				if ( pEntity is CardCrystallonEntity ) {
					CardManager.Instance.Add( pEntity as CardCrystallonEntity );
				}
			}
			pEntity.setBody(_physics.RegisterPhysicsBody(_physics.SceneShapes[0], 0.1f, 0.01f, this.getPosition()));
			pEntity.setVelocity(1.0f, GameScene.Random.NextAngle());
			pEntity.addToScene();
			return pEntity;
//			Node node = getNode();
//			for ( int i=0; i<members.Length; i++ ) {
//				if ( members[i] != null ) {
//					CardCrystallonEntity c = members[i] as CardCrystallonEntity;
//					Remove( c );
//					c.setBody(_physics.RegisterPhysicsBody(_physics.SceneShapes[0], 0.1f, 0.01f, this.getPosition()));
//					c.setVelocity(1.0f, GameScene.Random.NextAngle());
//					c.addToScene();
//					return c;
//				}
//			}
//			return null;
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
		public void RemoveAll() {
			for( int i=members.Length-1; i>=0; i--) {
				if ( members[i] != null ) {
					Remove( members[i] );
				}
			}
		}
	}
}
