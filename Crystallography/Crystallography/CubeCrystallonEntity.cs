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
		protected readonly static float DEFAULT_SPEED = 0.3f;
		protected SpriteTile radial;
		protected SpriteTile radial2;
		protected Node radialNode;
		protected Node radialNode2;
		protected bool finished;
		
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
//			_node.AdHocDraw += DrawAnim;
			finished = false;
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
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime(2.0f));
//			sequence.Add( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, -1.0f), 3.0f));
//			sequence.Add( new CallFunc( () => Finish() ) );
			sequence.Add ( new CallFunc( () => { 
				GroupManager.Instance.Remove(this, true); 
			} ) );
			getNode().RunAction(sequence);
			foreach( AbstractCrystallonEntity e in members ) {
				if (e is CardCrystallonEntity) {
					(e as CardCrystallonEntity).HideGlow();
					sequence = new Sequence();
					sequence.Add( new DelayTime( 0.8f ) );
					sequence.Add( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, -1.0f), 0.5f) );
//					(e as CardCrystallonEntity).getNode().RunAction( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, -1.0f), 3.0f));
					(e as CardCrystallonEntity).getNode().RunAction( sequence );
				}
			}
			addToScene();
//			getNode().RunAction(sequence);
			
			radialNode = new Node();
			radial = Support.SpriteFromFile("Application/assets/images/burst.png");
//			radial.Color.A = 0.0f;
			radial.Color = new Vector4(1.0f, 1.0f, 0.49f, 0.0f);
			radial.Pivot = new Vector2(0.5f, 0.5f);
			radial.Scale = new Vector2(0.1f, 0.1f);
			radialNode.AddChild(radial);
			radial.Position = -radial.CalcSizeInPixels() * radial.Scale.X * 0.5f;
			radialNode.Position = getNode().Position;
			sequence = new Sequence();
			sequence.Add( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, 0.25f), 0.5f) {
				Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.ExpEaseOut(t, 1.5f)
			} );
			sequence.Add( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, -0.25f), 1.0f) );
			radial.RunAction(sequence);
			sequence = new Sequence();
			sequence.Add( new ScaleTo( new Vector2(1.5f, 1.5f), 0.5f) );
//			sequence.Add( new ScaleTo( new Vector2(0.1f, 0.1f), 0.5f) );
			radial.RunAction(sequence);
			radialNode.RunAction( new RotateBy ( new Vector2( 0.052f, 0.999f), 3.0f ) );
			GameScene.Layers[0].AddChild(radialNode);
			
			
			radialNode2 = new Node();
			radial2 = Support.SpriteFromFile("Application/assets/images/burst.png");
			radial2.Color.A = 0.0f;
			radial2.Pivot = new Vector2(0.5f, 0.5f);
			radial2.Scale = new Vector2(0.1f, 0.1f);
			radialNode2.AddChild(radial2);
			radial2.Position = -radial2.CalcSizeInPixels() * radial2.Scale.X * 0.5f;
			radialNode2.Position = getNode().Position;
			sequence = new Sequence();
			sequence.Add( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, 0.25f), 0.5f) {
				Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.ExpEaseOut(t, 1.5f)
			} );
			sequence.Add( new TintBy( new Vector4(0.0f, 0.0f, 0.0f, -0.25f), 1.0f) );
			radial2.RunAction(sequence);
			sequence = new Sequence();
			sequence.Add( new ScaleTo( new Vector2(1.2f, 1.3f), 0.5f) );
//			sequence.Add( new ScaleTo( new Vector2(0.1f, 0.1f), 0.5f) );
			radial2.RunAction(sequence);
			radialNode2.RunAction( new RotateBy ( new Vector2( 0.052f, -0.999f ), 3.0f ) {
				Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear(t)
			});
			GameScene.Layers[0].AddChild(radialNode2);
			
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
		
		public override void Update (float dt)
		{
			base.Update(dt);
			if(!finished) {
//				Console.WriteLine(radial.CalcSizeInPixels().X);
				radial.Position = -radial.CalcSizeInPixels() * radial.Scale.X * 0.5f;
				radialNode.Position = getNode().Position;
				radial2.Position = -radial2.CalcSizeInPixels() * radial2.Scale.X * 0.5f;
				radialNode2.Position = getNode().Position;
//				radial.Color.A = 1.0f - (members[0].getNode() as SpriteTile).Color.A;
			}
		}
		
		//OVERRIDES ------------------------------------------------------------
		
		public override void removeFromScene (bool doCleanup)
		{
			if( doCleanup ) {
				foreach( AbstractCrystallonEntity e in members ) {
					(e as CardCrystallonEntity).setParticle(0);
				}
				GameScene.Layers[0].RemoveChild(radialNode, true);
				GameScene.Layers[0].RemoveChild(radialNode2, true);
				Top = null;
				Left = null;
				Right = null;
				radial = null;
				radial2 = null;
				radialNode = null;
				radialNode2 = null;
			}
			base.removeFromScene (doCleanup);
		}
		
		// METHODS -------------------------------------------------------------
		
//		public void DrawAnim() {
////			Console.WriteLine("Draw");
//			if ( _body != null ) {
//				var bottomLeft = Vector2.Zero;
//				var topRight = getPosition();
//				Director.Instance.DrawHelpers.DrawCircle(Vector2.Zero,60.0f,32);
////				Director.Instance.DrawHelpers.DrawBounds2Fill (
////				new Bounds2(bottomLeft, topRight));
//			}
//		}
		
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
		
		public void Finish() {
			finished = true;
			Visible = false;
			//_physics.removePhysicsBody(_body);
			//setBody(null);
//			foreach( AbstractCrystallonEntity e in members ) {
//				(e as CardCrystallonEntity).setParticle(0);
//			}
//			GameScene.Layers[0].RemoveChild(radialNode, true);
//			GameScene.Layers[0].RemoveChild(radialNode2, true);
//			Top = null;
//			Left = null;
//			Right = null;
//			radial = null;
//			radial2 = null;
//			radialNode = null;
//			radialNode2 = null;
//			GroupManager.Instance.Remove(this, true);
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
}

