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
		protected float _width;
		protected float _height;
		protected int _orientationIndex;
		protected int _patternIndex;
		
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
			get {
				return _height;
			}
		}
		
		public float Width {
			get {
				return _width;
			}
		}
		
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
			_width = _sprite.Scale.X;
			_height = _sprite.Scale.Y;
			
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
			//empty
		}
		
		// METHODS -------------------------------------------------------------------------------
		
		public override AbstractCrystallonEntity BeReleased ( Vector2 position ) {
			return this;
		}
	}
}
