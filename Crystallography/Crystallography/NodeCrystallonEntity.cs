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
		
		public override Bounds2 getBounds ()
		{
			if(_body != null)
				return new Bounds2(_body.AabbMin * GamePhysics.PtoM, _body.AabbMax * GamePhysics.PtoM);
			else
				return Bounds2.Zero;
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
			if(getBody() != null) {
				var len = _body.Velocity.Length();
				if (len > 0.3f) {
					len = FMath.Min ( 4.0f, FMath.Max(0.3f, len - dt * 5.0f));
					_body.Velocity = _body.Velocity.Normalize() * len;
				}
//				_sprite.Position = _body.Position * GamePhysics.PtoM;
			}
			
//			if (_body != null) {
//				if (_body.Velocity.Length() != 0.3f) {
//						_body.Velocity = _body.Velocity.Normalize() * 0.3f;
//					}
//			}
		}
		
		// METHODS----------------------------------
		
		public override AbstractCrystallonEntity BeAddedToGroup (GroupCrystallonEntity pGroup)
		{
			pGroup.Attach( this  );
			return this;
		}
	}
}