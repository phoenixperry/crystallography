using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public abstract class AbstractCrystallonEntity : ICrystallonEntity  {
		
		protected readonly Scene _scene;
		protected readonly GamePhysics _physics;
		
		protected Vector2 _offset;
		protected string _sound;
		
		// CONSTRUCTORS--------------------
		
		public AbstractCrystallonEntity( Scene pScene, GamePhysics pGamePhysics ) {
			_scene = pScene;
			_physics = pGamePhysics;
			_offset = Vector2.Zero;
		}
		
		// METHODS--------------------------
		
		public abstract Node getNode();
		
		public abstract PhysicsBody getBody();
		
		public abstract Vector2 getAttachOffset(int position);
		
		public abstract void setBody(PhysicsBody body);
		
		public abstract void setNode( Node node );
		
//		public abstract void setOrientation( string pOrientation );
		
//		public abstract void setPattern ( string pPattern );
		
		public abstract void Update(float dt);
		
		public void addImpulse(Vector2 pImpulse) {
			PhysicsBody body = getBody ();
			if ( body != null ) {
				body.Velocity = body.Velocity.Add(pImpulse);
			}
		}
		
		public void setSound( string pSoundName ) {
			_sound = pSoundName;
		}
		
		public void addToScene() {
			Node node = getNode();
			if (node.Parent == _scene) { // ALREADY ATTACHED TO SCENE -- DONE
				return;
			} else if (node.Parent != null) {
				node.Parent.RemoveChild(node, false);
			}
			(_scene as GameScene).AddChildEntity(this);
				
//			PhysicsBody body = getBody ();
//			if (body == null && hasPhysics){
//				_physics.RegisterPhysicsBody();
//			}
		}
		
		public void attachTo( Node pNewParent ) {
			Node node = getNode();
			if (node.Parent == pNewParent ) {
				return;
			}
			if (getBody() != null) {
				removePhysicsBody();
			}
			if (node.Parent != null) {
				if (node.Parent == _scene) {
					removeFromScene(false);
				} else {
					node.Parent.RemoveChild(node, false);
				}
			}
			pNewParent.AddChild(node);
		}
		
		public  virtual void removeFromScene(bool doCleanup=false) {
			Node node = getNode();
			removePhysicsBody();
			(_scene as GameScene).RemoveChildEntity( this, doCleanup );
		}
		
		private void removePhysicsBody() {
			PhysicsBody body = getBody();
			if (body != null) {
				_physics.removePhysicsBody(body);
				setBody(null);
			}
		}
		
		public void setPivot(float pX, float pY) {
			getNode().Pivot = new Vector2 ( pX, pY );
		}
		
		public void setPosition(float pX, float pY) {
			Vector2 v2 = new Vector2( pX, pY );
			PhysicsBody body = getBody();
			if (body != null) {
				body.Position = v2 / GamePhysics.PtoM;
			} else {
				getNode().Position = v2;
			}
		}
		
//		public Vector2 getPositionLocal() {
//			Node node = getNode();
//			return new Vector2(node.Position.X, node.Position.Y);
//		}
		
		public Vector2 getPosition() {
			PhysicsBody body = getBody();
			if (body != null) {
				return body.Position * GamePhysics.PtoM;
			} else {
				return getNode().Position;
			}
		}
		
		public void setPosition(Vector2 pPixelPosition) {
			PhysicsBody body = getBody();
			if (body != null) {
				body.Position = pPixelPosition / GamePhysics.PtoM;
			} else {
				getNode().Position = pPixelPosition;
			}
		}
		
		public virtual void playSound() {
			Support.SoundSystem.Instance.Play(_sound);
		}
		
		public void setVelocity(Vector2 pVelocity) {
			PhysicsBody body = getBody();
			if (body != null) {
				body.Velocity = pVelocity;
			}
		}
		
		public void setVelocity(float pPixelsPerSecond, float pRadians = float.NaN) {
			PhysicsBody body = getBody();
			if (body != null) {
				if (pPixelsPerSecond == 0) { 	// STOP MOVEMENT
					body.Velocity = Vector2.Zero;
				} else if ( float.IsNaN(pRadians) ) { 	// UPDATE SPEED, PRESERVE HEADING
					body.Velocity = new Vector2(0.0f, pPixelsPerSecond).Rotate(body.Rotation);
				} else {						// UPDATE SPEED AND HEADING
					body.Velocity = new Vector2(0.0f, pPixelsPerSecond).Rotate(pRadians);
				}
			}
		}
	}
}