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
		protected int _orientationIndex;
		protected int _patternIndex;
		protected int _colorIndex;
		protected int _particleIndex;
		
		// GET & SET---------------------------------
		
		public override Node getNode() {
			return _sprite;
		}
		
		public override PhysicsBody getBody() {
			return _body;
		}
		
		public int getColor() {
			return _colorIndex;
		}
		
		public void setColor(int pIndex) {
			_colorIndex = pIndex;
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
			Scheduler.Instance.Schedule(_sprite, Update, 0.0f, false);
		}
		
		// OVERRIDES------------------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			if(getBody() != null) {
				if (_body.Velocity.Length() != 0.3f) {
					_body.Velocity = _body.Velocity.Normalize() * 0.3f;
				}
				_sprite.Position = _body.Position * GamePhysics.PtoM;
			}
		}
		
		public override AbstractCrystallonEntity BeReleased ( Vector2 position ) {
			return this;
		}
		
		public override AbstractCrystallonEntity BeAddedToGroup (GroupCrystallonEntity pGroup)
		{
			pGroup.Attach( this );
			if (pGroup is SelectionGroup) {
				playSound();
			}
		return this;
		}
		
		public override AbstractCrystallonEntity BeSelected ( float delay = 0.0f )
		{
			return this;
		}
		
		public override bool CanBeAddedTo (GroupCrystallonEntity pGroup)
		{
			bool okToSnap = false;
			if( pGroup.MemberType.IsAssignableFrom(this.GetType()) ) { // CHECK FOR OBJECT TYPE COMPATIBILITY WITH GROUP
				if (AppMain.ORIENTATION_MATTERS) { // ------------------- ORIENTATION TEST
					okToSnap = ( pGroup.pucks[_orientationIndex].Children.Count == 0 );
					okToSnap = okToSnap && (Array.IndexOf(pGroup.members, this) == -1);
				} else {
					okToSnap = ( pGroup.population < pGroup.maxPopulation );
				}
			}
			return okToSnap;
		}
		
		// METHODS -------------------------------------------------------------------------------
		
		public void setParticle( int pVariant ) {
			_particleIndex = pVariant;
			if (pVariant != 0) {
				Scheduler.Instance.Schedule(_sprite, spawnParticle, 0.2f, false);
//				Support.ParticleEffectsManager.Instance.AddParticle(pVariant-1, this, QColor.palette[(_colorIndex+1)%3], 12.0f);
//				Support.ParticleEffectsManager.Instance.AddParticle(pVariant-1, this, new Vector4(0.36f, 0.23f, 0.49f, 1.0f), 12.0f);
			} else {
				Scheduler.Instance.Unschedule(_sprite, spawnParticle);
			}
		}
		
		protected void spawnParticle(float dt) {
			Support.ParticleEffectsManager.Instance.AddParticle(_particleIndex-1, this, 
			                         new Vector4(1.0f, 1.0f, 1.0f, 1.0f), 12.0f);
//				                     QColor.palette[(_colorIndex+1)%3], 12.0f);
		}
	}
}
