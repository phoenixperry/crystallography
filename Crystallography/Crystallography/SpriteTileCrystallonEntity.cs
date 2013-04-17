using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;
	
namespace Crystallography
{
	public abstract class SpriteTileCrystallonEntity : AbstractCrystallonEntity {
		protected SpriteTile _sprite;
		protected PhysicsBody _body;
//		protected float _width;
//		protected float _height;
		protected int _orientationIndex;
		protected int _patternIndex;
		protected int _particleIndex;
		
		// GET & SET---------------------------------
		
		public override Node getNode() {
			return _sprite;
		}
		
		public override PhysicsBody getBody() {
			return _body;
		}
		
		public int getOrientation() {
			return _orientationIndex;
		}
		
		public int getPattern() {
			return _patternIndex;
		}
		
		public override void setBody (PhysicsBody body) {
			_body = body;
		}
		
		public override void setNode ( Node node ) {
			_sprite = node as SpriteTile;
		}
		
		public virtual void setOrientation ( int pOrientation ) {
			_orientationIndex = pOrientation;
		}
		
		public virtual void setPattern ( int pPattern ) {
			_patternIndex = pPattern;
		}
		
		public override Vector2 getAttachOffset (int position)
		{
			return Vector2.Zero;
		}
		
		public float Height {
			get { return _sprite.Scale.Y; } }
		
		public float Width {
			get { return _sprite.Scale.X; } }
		
		// CONSTRUCTORS----------------------------------------------------------------------------
		
		public SpriteTileCrystallonEntity( Scene pScene, GamePhysics pGamePhysics, 
		                              TextureInfo pTextureInfo, Vector2i pTileIndex2D, PhysicsShape pShape = null) 
												: base(pScene, pGamePhysics) {
			
			// SPRITE STUFF
			_orientationIndex = 0;
			_patternIndex = 0;
			_sprite = new SpriteTile(pTextureInfo, pTileIndex2D);
			_sprite.Scale = _sprite.CalcSizeInPixels();
			_sprite.Pivot = new Vector2(0.5f, 0.5f);
			
			// PHYSICS STUFF
			if (pShape != null) {
				_body = _physics.RegisterPhysicsBody(pShape, 0.1f, 0.01f, _sprite.Position);
			} else {
				_body = null;
			}
			
			Scheduler.Instance.Schedule(_sprite, Update, 0, false, 0);
		}
		
		// OVERRIDES------------------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			if(getBody() != null) {
				if (_body.Velocity.Length() != 1.0f) {
					_body.Velocity = _body.Velocity.Normalize();
				}
				_sprite.Position = _body.Position * GamePhysics.PtoM;
			}
		}
		
		public override AbstractCrystallonEntity BeReleased ( Vector2 position ) {
			return this;
		}
		
		// METHODS -------------------------------------------------------------------------------
		
		public void setParticle( int pVariant ) {
			_particleIndex = pVariant;
			if (pVariant != 0) {
				Scheduler.Instance.Schedule(_sprite, spawnParticle, 0.2f, false);
				Support.ParticleEffectsManager.Instance.AddParticle(pVariant-1, this, QColor.palette[0], 12.0f);
			} else {
				Scheduler.Instance.Unschedule(_sprite, spawnParticle);
			}
		}
		
		protected void spawnParticle(float dt) {
			Support.ParticleEffectsManager.Instance.AddParticle(_particleIndex-1, this, 
				                     QColor.palette[0], 12.0f);
		}
	}
}
