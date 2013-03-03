using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public abstract class NodeCrystallonEntity : AbstractCrystallonEntity {
		protected Node _node;
		protected PhysicsBody _body;
		
		// GET & SET -------------------------------------------------------------
		public override Node getNode() {
			return _node;
		}
		
		public override PhysicsBody getBody() {
			return _body;
		}
		
		public override void setBody (PhysicsBody body)
		{
			_body = body;
		}
		
		public override void setNode ( Node node ) {
			_node = node;
		}
		
		public override Vector2 getAttachOffset (int position)
		{
			return Vector2.Zero;
		}
		
		// CONSTRUCTORS ----------------------------------------------------------
		
		public NodeCrystallonEntity(Scene pScene, GamePhysics pGamePhysics, PhysicsShape pShape = null) : base(pScene, pGamePhysics) {
			_node = new Node();
			if (pShape != null) {
				_body = _physics.RegisterPhysicsBody(pShape, 0.1f, 0.01f, _node.Position);
			} else {
				_body = null;
			}
			Scheduler.Instance.Schedule(_node, Update, 0, false, 0);
		}
		
		// OVERRIDES -------------------------------------------------------------
		
		public override void Update (float dt)
		{
			//empty
		}
		
		// METHODS----------------------------------
		
	}
}