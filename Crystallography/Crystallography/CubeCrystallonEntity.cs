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
		static readonly float[] ROTATIONS = { 0.999f, -0.999f, 0.0f };
		protected readonly static float DEFAULT_SPEED = 0.3f;
		protected SpriteTile[] radialSprites;
		protected Node[] radialNodes;
		protected bool finished;
		
		public static event EventHandler<CubeCompleteEventArgs>       CubeCompleteDetected;
		
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
			
			// SET UP SELF-DESTRUCT
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime(2.0f));
			sequence.Add ( new CallFunc( () => { 
				GroupManager.Instance.Remove(this, true); 
			} ) );
			getNode().RunAction(sequence);
			
			// FADE OUT SEQUENCE
			sequence = new Sequence();
			sequence.Add( new DelayTime( 0.8f ) );
			sequence.Add( new CallFunc( () => {
				foreach( AbstractCrystallonEntity e in members ) {
					if (e is CardCrystallonEntity) {
						Console.WriteLine(e.id.ToString());
						(e as CardCrystallonEntity).HideGlow();
						(e as CardCrystallonEntity).Scored = true;
						(e as CardCrystallonEntity).TintTo( Vector4.Zero, 0.5f, true);
					}
				}
			}));
			Director.Instance.CurrentScene.RunAction(sequence);
			
			addToScene();
			
			radialNodes = new Node[3];
			radialSprites = new SpriteTile[3];
			
			// RADIAL BURST VFX
			for(int i=0; i<radialNodes.Length; i++) {
				radialNodes[i] = new Node();
				radialNodes[i].Position = getNode().Position;
				radialSprites[i] = Support.SpriteFromFile("Application/assets/images/burst.png");
				radialSprites[i].Position = -radialSprites[i].CalcSizeInPixels() * radialSprites[i].Scale.X * 0.5f;
				radialSprites[i].Color = (members[i].getNode() as SpriteBase).Color.Xyz0;
				radialSprites[i].Pivot = new Vector2(0.5f, 0.5f);
				radialSprites[i].Scale = new Vector2(0.1f, 0.1f);
				radialNodes[i].AddChild (radialSprites[i]);
				sequence = new Sequence();
				sequence.Add( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, 0.5f), 0.5f) {
					Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.ExpEaseOut(t, 1.5f)
				} );
				sequence.Add( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, -0.5f), 0.75f) );
				radialSprites[i].RunAction(sequence);
				sequence = new Sequence();
				sequence.Add( new ScaleTo( new Vector2(1.5f, 1.5f), 0.5f) );
				radialSprites[i].RunAction(sequence);
				radialNodes[i].RunAction( new RotateBy ( new Vector2( 0.052f, ROTATIONS[i]), 3.0f ) );
				GameScene.Layers[0].AddChild(radialNodes[i]);
			}
			
			return this;
		}
		
		public override AbstractCrystallonEntity BeAddedToGroup (GroupCrystallonEntity pGroup)
		{
			Attach( this );
			return this;
		}
		
		public override void Break ()
		{
			// DO NOTHING.
		}
		
		public override bool CanBeAddedTo (GroupCrystallonEntity pGroup)
		{
			return false;
		}
		
		public override void Update (float dt)
		{
			base.Update(dt);
			for( int i=0; i<radialNodes.Length; i++) {
				radialSprites[i].Position = -radialSprites[i].CalcSizeInPixels() * radialSprites[i].Scale.X * 0.5f;
//				radialNodes[i].Position = getNode().Position;
			}
		}
		
		//OVERRIDES ------------------------------------------------------------
		
		public override void removeFromScene (bool doCleanup)
		{
			if( doCleanup ) {
				foreach( AbstractCrystallonEntity e in members ) {
					(e as CardCrystallonEntity).setParticle(0);
				}
				for( int i=0; i<3; i++ ) {
					GameScene.Layers[0].RemoveChild(radialNodes[i], true);
					radialNodes[i] = null;
					radialSprites[i] = null;
				}
				Top = null;
				Left = null;
				Right = null;
				EventHandler<CubeCompleteEventArgs> handler = CubeCompleteDetected;
				if ( handler != null ) {
					handler( this, new CubeCompleteEventArgs {
						members = Array.ConvertAll( this.members, item => (CardCrystallonEntity)item )
					});
				}
			}
			base.removeFromScene (doCleanup);
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
		
	}
	
	public class CubeCompleteEventArgs : EventArgs {
		public CardCrystallonEntity[] members;
	}
}

