using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public class CubeCrystallonEntity : GroupCrystallonEntity {
		
		/// <summary>
		/// Offsets used by different pieces to form a cube.
		/// </summary>
		static readonly Vector2[] POSITION_OFFSETS = { 	new Vector2(0f,-23.5f),
														new Vector2(-27.0f,23.5f),
														new Vector2(27.0f,23.5f) };
		protected readonly static float DEFAULT_SPEED = 1.0f;
		
		// GET & SET -----------------------------------------------------------
		
		/// <summary>
		/// Gets the attach offset.
		/// </summary>
		/// <returns>
		/// The attach offset.
		/// </returns>
		/// <param name='position'>
		/// <c>int</c> of which position this entity is in as part of a group. 0, 1, or 2.
		/// </param>
		public override Vector2 getAttachOffset (int position)
		{
			return POSITION_OFFSETS[position];
		}
		
		private CardCrystallonEntity _top;
		private CardCrystallonEntity _left;
		private CardCrystallonEntity _right;
		
		public CardCrystallonEntity Top { 
			get{ 
				if (_top != null) {
					return _top;
				} else {
					return _top = GetSideEntity(0);
				}
			} 
			private set{
				_top = value;
			}
		}
		
		public CardCrystallonEntity Left { 
			get{ 
				if (_left != null) {
					return _left;
				} else {
					return _left = GetSideEntity(1);
				}
			} 
			private set{
				_left = value;
			}
		}
		
		public CardCrystallonEntity Right { 
			get{ 
				if (_right != null) {
					return _right;
				} else {
					return _right = GetSideEntity(2);
				}
			} 
			private set{
				_right = value;
			}
		}
		
		// CONSTRUCTORS --------------------------------------------------------
		
		public CubeCrystallonEntity(Scene pScene, GamePhysics pGamePhysics, PhysicsShape pShape = null ) 
																			: base( pScene, pGamePhysics, pShape, 3, true ) {
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
		}
			
		// OVERRIDES -----------------------------------------------------------
		
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
			return this;
		}
		
		// METHODS -------------------------------------------------------------
		
		public void Rotate ( bool pClockwise ) {
			if (pClockwise) {
				Top.attachTo(_pucks[2]);
				Right.attachTo(_pucks[1]);
				Left.attachTo (_pucks[0]);
			} else {
				Top.attachTo(_pucks[1]);
				Right.attachTo(_pucks[0]);
				Left.attachTo (_pucks[2]);
			}
			Top = GetSideEntity(0);
			Right = GetSideEntity(2);
			Left = GetSideEntity(1);
		}
		
		private CardCrystallonEntity GetSideEntity(int pIndex) {
			Node n = pucks[pIndex].Children[0];
				foreach (var member in members) {
					if (member.getNode() == n) {
						return member as CardCrystallonEntity;
					}
				}
				return null;
		}
		
		// DESTRUCTOR -----------------------------------------------------------
		
		~CubeCrystallonEntity() {
			Top = null;
			Left = null;
			Right = null;
		}
		
	}
}

