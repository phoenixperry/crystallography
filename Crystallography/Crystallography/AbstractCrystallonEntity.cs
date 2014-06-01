using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public abstract class AbstractCrystallonEntity : ICrystallonEntity  {
		
		protected readonly GamePhysics _physics;
		
		protected  Scene _scene;
		protected Vector2 _offset;
		protected string _sound;
		public int id;
		
		// GET & SET ---------------------------------------------------------
		
		public System.Collections.Generic.List<Node> Children { get { return getNode ().Children; } }
		
		public Node Parent { get { return getNode ().Parent; } }
		
		public System.Guid GUID { get; private set; }
		
		public bool Visible { 
			get { return getNode ().Visible; } 
			set { getNode ().Visible = value; } 
		}
		
		// CONSTRUCTORS--------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.AbstractCrystallonEntity"/> class.
		/// </summary>
		/// <param name='pScene'>
		/// Reference to the current scene.
		/// </param>
		/// <param name='pGamePhysics'>
		/// Reference to the <see cref="Crystallography.GamePhysics"/>.
		/// </param>
		public AbstractCrystallonEntity( Scene pScene, GamePhysics pGamePhysics ) {
			GUID = Guid.NewGuid();
			_scene = pScene;
			_physics = pGamePhysics;
			_offset = Vector2.Zero;
#if DEBUG
			Console.WriteLine("{0} {1} created", this.GetType(), GUID);
#endif
		}
		
		// METHODS--------------------------
		
		/// <summary>
		/// Object-specific code for being released from a GroupCrystallonEntity
		/// </summary>
		/// <returns>
		/// This object
		/// </returns>
		/// <param name='position'>
		/// World position to be released at.
		/// </param>
		public abstract AbstractCrystallonEntity BeReleased( Vector2 position );
		
		/// <summary>
		/// Object-specific code for being added to a GroupCrystallonEntity
		/// </summary>
		/// <returns>
		/// This object
		/// </returns>
		/// <param name='pGroup'>
		/// The group to be added to
		/// </param>
		public abstract AbstractCrystallonEntity BeAddedToGroup( GroupCrystallonEntity pGroup );
		
		/// <summary>
		/// Object-specific code for being added to the Selection Group
		/// </summary>
		/// <returns>
		/// This object
		/// </returns>
		public abstract AbstractCrystallonEntity BeSelected( float delay = 0.0f );
		
		/// <summary>
		/// Returns this entity's <c>Node</c>-descended object.
		/// </summary>
		/// <returns>
		/// <see cref="Sce.PlayStation.HighLevel.GameEngine2D.Node"/>-descended entity.
		/// </returns>
		public abstract Node getNode();
		
		/// <summary>
		/// Returns this entity's <c>PhysicsBody</c>.
		/// </summary>
		/// <returns>
		/// <see cref="Sce.PlayStation.HighLevel.Physics2D.PhysicsBody"/>
		/// </returns>
		public abstract PhysicsBody getBody();
		
		/// <summary>
		/// Gets the bounds.
		/// </summary>
		/// <returns>
		/// The bounds.
		/// </returns>
		public abstract Bounds2 getBounds();
		
		/// <summary>
		/// Returns an offset position <c>Vector2</c> for this sort of entity when it is in a <c>GroupCrystallonEntity</c>.
		/// </summary>
		/// <returns>
		/// The position <c>Vector2</c>
		/// </returns>
		/// <param name='position'>
		/// An <c>int</c>, probs 0, 1, or 2.
		/// </param>
		public abstract Vector2 getAttachOffset(int position);
		
		/// <summary>
		/// Sets the <c>PhysicsBody</c> for this entity.
		/// </summary>
		/// <param name='body'>
		/// <see cref="Sce.PlayStation.HighLevel.Physics2D.PhysicsBody"/>
		/// </param>
		public abstract void setBody(PhysicsBody body);
		
		/// <summary>
		/// Sets the <c>Node</c>-descended entity for this entity.
		/// </summary>
		/// <param name='node'>
		/// <see cref="Sce.PlayStation.HighLevel.GameEngine2D.Node"/>-descended entity.
		/// </param>
		public abstract void setNode( Node node );
		
		/// <summary>
		/// Returns the variant number for the specified Quality
		/// </summary>
		/// <param name='pQualityName'>
		/// <c>string</c> name of the quality class
		/// </param>
		public int getQualityVariant( string pQualityName ) {
			for ( int i=0; i<QualityManager.Instance.qualityDict[pQualityName].Length; i++ ) {
				var variantList = QualityManager.Instance.qualityDict[pQualityName][i];
				if (variantList == null) {
					continue;
				}
				if (QualityManager.Instance.qualityDict[pQualityName][i].Contains((int)(this.id)) ) {
					return i;
				}
			}
			return -1;
		}
		
		/// <summary>
		/// This entity's once-per-frame update method.
		/// </summary>
		/// <param name='dt'>
		/// Elapsed time since last frame.
		/// </param>
		public abstract void Update(float dt);
		
		/// <summary>
		/// Adds an impulse vector to this entity's physics body
		/// </summary>
		/// <param name='pImpulse'>
		/// <see cref="Sce.PlayStation.Core.Vector2"/> impulse.
		/// </param>
		public void addImpulse(Vector2 pImpulse) {
			PhysicsBody body = getBody ();
			if ( body != null ) {
				body.Velocity = body.Velocity.Add(pImpulse);
			}
		}
		
		/// <summary>
		/// Sets the sound that plays when this entity is interacted with.
		/// </summary>
		/// <param name='pSoundName'>
		/// <c>string</c> name of the sound.
		/// </param>
		public void setSound( string pSoundName ) {
			_sound = pSoundName;
		}
		
		/// <summary>
		/// Attaches this entity to the current <c>Scene</c> after removing it from it's current <c>Parent</c>.
		/// </summary>
		public void addToScene(int pLayerIndex=1) {
			Node node = getNode();
			if (node.Parent != null) {
				if (node.Parent == GameScene.Layers[pLayerIndex]) { // ALREADY ATTACHED TO SCENE -- DONE
					return;
				} else {
					node.Parent.RemoveChild(node, false);
				}
			}
			(_scene as GameScene).AddChildEntity(this, pLayerIndex);
		}
		
		/// <summary>
		/// Attach this entity to a <c>Node</c>-descended entity after removing it from its current <c>Parent</c>.
		/// </summary>
		/// <param name='pNewParent'>
		/// <see cref="Sce.PlayStation.HighLevel.GameEngine2D.Node"/>
		/// </param>
		public void attachTo( Node pNewParent ) {
			Node node = getNode();
			if (node.Parent == pNewParent ) {
				return;
			}
			if (getBody() != null) {
				removePhysicsBody();
			}
			if (node.Parent != null) {
				if (node.Parent is Layer) {
					removeFromScene(false);
				} else {
					node.Parent.RemoveChild(node, false);
				}
			}
			pNewParent.AddChild(node);
		}
		
		/// <summary>
		/// Respond to being tapped
		/// </summary>
		public virtual void BeTapped( float delay = 0.0f) {
			if( delay > 0.0f ) {
				Sequence sequence = new Sequence();
				sequence.Add( new DelayTime( delay ) );
				sequence.Add( new CallFunc( ()=> playSound() ) );
			} else {
				playSound();
			}
		}
		
		public virtual bool CanBeAddedTo( GroupCrystallonEntity pGroup ) {
			return false;
		}
		
		/// <summary>
		/// Removes this entity from the GameScene & the GamePhysics.
		/// </summary>
		/// <param name='doCleanup'>
		/// Do cleanup?
		/// </param>
		public  virtual void removeFromScene(bool doCleanup=false) {
			if (Parent is Layer) {
				Node node = getNode();
				removePhysicsBody();
				(_scene as GameScene).RemoveChildEntity( this, doCleanup );
			}
		}
		
		/// <summary>
		/// Removes this entity's <c>PhysicBody</c>.
		/// </summary>
		private void removePhysicsBody() {
			PhysicsBody body = getBody();
			if (body != null) {
				_physics.removePhysicsBody(body);
				setBody(null);
			}
		}
		
		/// <summary>
		/// Sets the location of this entity's pivot point in uv-coordinates. To center, use (0.5f, 0.5f).
		/// </summary>
		/// <param name='pX'>
		/// <c>float</c> Horizontal (u) coordinate.
		/// </param>
		/// <param name='pY'>
		/// <c>float</c> Vertical (v) coordinate.
		/// </param>
		public void setPivot(float pX, float pY) {
			getNode().Pivot = new Vector2 ( pX, pY );
		}
		
		/// <summary>
		/// Gets this entity's position, relative to its <c>Parent</c>, in pixels.
		/// </summary>
		/// <returns>
		/// <see cref="Sce.PlayStation.Core.Vector2"/>
		/// </returns>
		public Vector2 getPosition() {
			PhysicsBody body = getBody();
			if (body != null) {
				return body.Position * GamePhysics.PtoM;
			} else {
				return getNode().Position;
			}
		}
		
		/// <summary>
		/// Sets this entity's position relative to its <c>Parent</c> (often the scene) in pixels.
		/// </summary>
		/// <param name='pX'>
		/// P x.
		/// </param>
		/// <param name='pY'>
		/// P y.
		/// </param>
		public virtual void setPosition(float pX, float pY) {
			Vector2 v2 = new Vector2( pX, pY );
			setPosition(v2);
//			PhysicsBody body = getBody();
//			if (body != null) {
//				body.Position = v2 / GamePhysics.PtoM;
//			} else {
//				getNode().Position = v2;
//			}
		}
		
		/// <summary>
		/// Sets this entity's position, relative to its <c>Parent</c> in pixels.
		/// </summary>
		/// <param name='pPixelPosition'>
		/// <see cref="Sce.PlayStation.Core.Vector2"/>
		/// </param>
		public void setPosition(Vector2 pPixelPosition) {
			if ( pPixelPosition.Y < 34 ) {
				pPixelPosition.Y = 35;
			} else if ( pPixelPosition.Y > Director.Instance.GL.Context.GetViewport().Height ) {
				pPixelPosition.Y = Director.Instance.GL.Context.GetViewport().Height-1;
			}
			if ( pPixelPosition.X < 0 ) {
				pPixelPosition.X = 1;
			} else if ( pPixelPosition.X > Director.Instance.GL.Context.GetViewport().Width ) {
				pPixelPosition.X = Director.Instance.GL.Context.GetViewport().Width-1;
			}
			PhysicsBody body = getBody();
			if (body != null) {
				body.Position = pPixelPosition / GamePhysics.PtoM;
			} else {
				getNode().Position = pPixelPosition;
			}
		}
		
		/// <summary>
		/// Tells the <c>SoundSystem</c> to play this entity's interaction sound.
		/// </summary>
		public virtual void playSound() {
			if (_sound != null) {
				Support.SoundSystem.Instance.Play(_sound);
			}
		}
		
		/// <summary>
		/// Sets the velocity in meters / second.
		/// </summary>
		/// <param name='pVelocity'>
		/// <see cref="Sce.PlayStation.Core.Vector2"/>
		/// </param>
		public void setVelocity(Vector2 pVelocity) {
			PhysicsBody body = getBody();
			if (body != null) {
				body.Velocity = pVelocity;
			}
		}
		
		/// <summary>
		/// Sets the velocity in pixels / second.
		/// </summary>
		/// <param name='pPixelsPerSecond'>
		/// <c>float</c> pixels / second.
		/// </param>
		/// <param name='pRadians'>
		/// Heading. Ignore to just continue with current heading.
		/// </param>
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
		
		// DESTRUCTOR ----------------------------------------------------------------------------
#if DEBUG		
		~AbstractCrystallonEntity() {
			Console.WriteLine("{0} {1} : {2} deleted", this.GetType(), id, GUID);
		}
#endif
	}
}