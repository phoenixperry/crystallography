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
		
		public int population {
			get { return _numMembers; }
		}
		
		// CONSTRUCTOR -----------------------------------------------------------
		
		public GroupCrystallonEntity(Scene pScene, GamePhysics pGamePhysics, PhysicsShape pShape = null, int pMaxMembers=0) 
																	: base( pScene, pGamePhysics, pShape ) {
			_maxMembers = pMaxMembers;
			_numMembers = 0;
			members = new ICrystallonEntity[_maxMembers];
			_pucks = new Node[_maxMembers];
			for (int i=0; i < _maxMembers; i++) {
				AddPuck(i);
			}
		}
		
		// OVERRIDES -------------------------------------------------------------
		
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
		
		public override void removeFromScene (bool doCleanup)
		{
			if (doCleanup) {
				foreach ( var member in members ) {
					member.removeFromScene( doCleanup );
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
		
		public virtual GroupCrystallonEntity Add(ICrystallonEntity pEntity) {
			if (pEntity is SpriteTileCrystallonEntity) {
				return AddSpriteTile(pEntity as SpriteTileCrystallonEntity);
			} else if  (pEntity is GroupCrystallonEntity) {
				return AddGroup (pEntity as GroupCrystallonEntity);
			}
			return this;
		}
		
		public GroupCrystallonEntity AddSpriteTile(SpriteTileCrystallonEntity pEntity) {
			IncreaseSlots( 1 );
//			members[GetOrientationIndex( pEntity )] = pEntity;
			members[GetFreeSlot()] = pEntity;
			Attach(pEntity);
			return this;
		}
		
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
			pGroupEntity.RemoveAll();
			pGroupEntity.removeFromScene(true);
			return this;
		}
		
		private void AddPuck( int pIndex ) {
			_pucks[pIndex] = new Node();
			getNode().AddChild(_pucks[pIndex]);
		}
		
		private void Attach( AbstractCrystallonEntity pEntity ) {
			//create a child-node, give it a positional offset, make pEntity a child of child-node
			int index = GetOrientationIndex( pEntity );
			if ( _pucks[index] == null ) {
				AddPuck(index);
			}
			Node puck = _pucks[index];
			pEntity.attachTo(puck);
			pEntity.getNode().Position *= 0;
			_numMembers++;
			if ( this is SelectionGroup  == false ) {	// SelectionGroup has its own positioning code -- HACK This implementation is just sort of inelegant. Maybe abstract some of this out later?
				if ( population  > 1 ) {
					foreach( AbstractCrystallonEntity e in members ) {
						if( e != null) {
							int attachPosition = GetOrientationIndex( e );
							_pucks[attachPosition].Position = e.getAttachOffset( attachPosition );
						}
					}
				} else { // JUST CENTER THE OBJECT IF THERE'S ONLY ONE.
					puck.Position *= 0;
				}
			}
		}
		
		private int GetOrientationIndex( AbstractCrystallonEntity pEntity ) {
			if (!GameScene.ORIENTATION_MATTERS) {
				return population;
			}
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
		
		private int GetFreeSlot() {
			for ( int i=0; i<members.Length; i++ ) {
				if (members[i] == null) {
					return i;
				}
			}
			return -1;
		}
		
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
			//TODO Remove puck nodes (unless this is the SelectionGroup, keep those.)
			return e;
		}
		
		public ICrystallonEntity Remove( ICrystallonEntity pEntity ) {
			int index = Array.IndexOf(members, pEntity);
			if (index != -1) {
				return PopIndex (index);
			}
			return null;
		}
		
		public void RemoveAll() {
			for( int i=members.Length-1; i>=0; i--) {
				if ( members[i] != null ) {
					Remove( members[i] );
				}
			}
		}
	}
}
