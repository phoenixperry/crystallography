using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;
	
namespace Crystallography
{
	public abstract class SpriteTileCrystallonEntity : NodeCrystallonEntity {
		protected SpriteTile _sprite;
//		protected PhysicsBody _body;
		protected int _orientationIndex;
		protected int _patternIndex;
		protected int _colorIndex;
		protected int _particleIndex;
		
		// GET & SET---------------------------------
		
//		public override Node getNode() {
//			return _sprite;
//		}
//		
//		public override PhysicsBody getBody() {
//			return _body;
//		}
		
		public SpriteTile getSprite() {
			return _sprite;
		}
		
		public override Bounds2 getBounds ()
		{
			Vector2 halfDimensions;
			if (_orientationIndex == 0)
				halfDimensions = new Vector2(Width/3.0f, Height/4.0f);
			else
				halfDimensions = new Vector2(Width/4.0f, Height/3.0f);
			return new Bounds2( getPosition() - halfDimensions, getPosition() + halfDimensions );
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
		
//		public override void setBody (PhysicsBody body) {
//			_body = body;
//		}
		
//		public override void setNode ( Node node ) {
//			_sprite = node as SpriteTile;
//		}
		
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
												: base(pScene, pGamePhysics, pShape) {
			
			// SPRITE STUFF
			_orientationIndex = 0;
			_patternIndex = 0;
			_sprite = new SpriteTile(pTextureInfo, pTileIndex2D);
			_sprite.Scale = _sprite.CalcSizeInPixels();
//			_sprite.Pivot = new Vector2(0.5f, 0.5f);
			_node.AddChild(_sprite);
			
			// PHYSICS STUFF
//			if (pShape != null) {
//				_body = _physics.RegisterPhysicsBody(pShape, 0.1f, 0.01f, _sprite.Position);
//			} else {
//				_body = null;
//			}
//			Scheduler.Instance.Schedule(_sprite, Update, 0.0f, false);
		}
		
		// OVERRIDES------------------------------------------------------------------------------
		
//		public override void Update (float dt)
//		{
//			if(getBody() != null) {
//				var len = _body.Velocity.Length();
//				if (len > 0.3f) {
//					len = FMath.Min ( 4.0f, FMath.Max(0.3f, len - dt * 5.0f));
//					_body.Velocity = _body.Velocity.Normalize() * len;
//				}
//				_sprite.Position = _body.Position * GamePhysics.PtoM;
//			}
//		}
		
		public override AbstractCrystallonEntity BeReleased ( Vector2 position ) {
			return this;
		}
		
		public override AbstractCrystallonEntity BeAddedToGroup (GroupCrystallonEntity pGroup)
		{
			pGroup.Attach( this );
			if (pGroup is SelectionGroup) {
				getNode().StopActionByTag(20);
				Sequence sequence = new Sequence() { Tag = 20 };
				sequence.Add( new DelayTime( 3.0f ) );
				sequence.Add( new CallFunc( () => {
					playSound();
				} ) );
				getNode().RunAction(sequence);
			}
			return this;
		}
		
		public override AbstractCrystallonEntity BeSelected ( float delay = 0.0f )
		{
			return this;
		}
		
		public override void BeTapped (float delay=0.0f)
		{
//			if( delay > 0.0f ) {
				getNode().StopActionByTag(20);
				Sequence sequence = new Sequence() { Tag = 20 };
				sequence.Add( new DelayTime( delay ) );
				sequence.Add( new CallFunc( ()=> {
					playSound();
					
				} ) );
				getNode().RunAction(sequence);
//			} 
//			else {
//				playSound();
//			}
		}
		
		public override bool CanBeAddedTo (GroupCrystallonEntity pGroup)
		{
			bool okToSnap = false;
			if (_sprite.Color.W < 0.8f) {
				Console.WriteLine(_sprite.Color.W);
				return false;
			}
//			if( pGroup.MemberType.IsAssignableFrom(this.GetType()) ) { // CHECK FOR OBJECT TYPE COMPATIBILITY WITH GROUP
			if ( pGroup.CheckMemberTypeCompatability(this) ){
				if (AppMain.ORIENTATION_MATTERS) { // ------------------- ORIENTATION TEST
					okToSnap = ( pGroup.pucks[_orientationIndex].Children.Count == 0 );
					okToSnap = okToSnap && (Array.IndexOf(pGroup.members, this) == -1);
				} else {
					okToSnap = ( pGroup.population < pGroup.maxPopulation );
				}
			}
			return okToSnap;
		}
		
		public override void removeFromScene (bool doCleanup)
		{
			if(doCleanup)
			{
				_sprite = null;
			}
			base.removeFromScene (doCleanup);
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
