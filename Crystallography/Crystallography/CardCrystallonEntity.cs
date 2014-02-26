using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public class CardCrystallonEntity : SpriteTileCrystallonEntity
    {
		static readonly float CARD_SCALAR = 0.7f;
		/// <summary>
		/// Offsets used by different pieces to form a cube.
		/// </summary>
		static readonly Vector2[] POSITION_OFFSETS = { 	new Vector2(0f,39.0f*CARD_SCALAR),
														new Vector2(-40.5f*CARD_SCALAR,-31.5f*CARD_SCALAR),
														new Vector2(40.5f*CARD_SCALAR,-31.5f*CARD_SCALAR) };
		public readonly static float DEFAULT_SPEED = 0.3f;
		
		protected SpriteTile _anim;
		protected int _glowIndex;
		
		protected float _keepOnScreenTimer;
		
		public bool Wild;
		
//		protected int countdownMax;
//		protected Label countdownText;
//		public int countdown;
		
		// GET & SET ------------------------------------------------------------------------------------------------------------------------------------------------------
		
		public SpriteTile GlowSprite { get; protected set;}
		
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
		
		public virtual void setGlow(int pGlow) {
			if (pGlow == -1) {
				HideGlow();
				this.getNode().RemoveChild(GlowSprite, true);
				GlowSprite = null;
				return;
			}
			_glowIndex = pGlow;
			GlowSprite = new SpriteTile(QGlow.Instance.GlowTiles.TextureInfo, _orientationIndex);
			HideGlow();
			this.getNode().AddChild(GlowSprite);
		}
		
		public override void setOrientation (int pOrientation)
		{
			base.setOrientation (pOrientation);
			if(_anim != null) {
				if (pOrientation == 1) {
					_anim.FlipU = false;
				} else if (pOrientation == 2) {
					_anim.FlipU = true;
				} else {
					_anim.FlipU = false;
				}
				_anim.TextureInfo = QAnimation.Instance.GetOrientedAnimation( getOrientation() ).TextureInfo;
			}
			if(GlowSprite != null) {
				GlowSprite.TileIndex1D = _orientationIndex;
			}
		}
		
		// CONSTRUCTORS --------------------------------------------------------------------------------------------------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.CardCrystallonEntity"/> class.
		/// </summary>
		/// <param name='pScene'>
		/// The Parent Scene.
		/// </param>
		/// <param name='pGamePhysics'>
		/// Instance of <c>GamePhysics</c>
		/// </param>
		/// <param name='pTextureInfo'>
		/// <see cref="Sce.PlayStation.HighLevel.GameEngine2D.Base.TextureInfo"/>
		/// </param>
		/// <param name='pTileIndex2D'>
		/// <see cref="Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i"/>
		/// </param>
		/// <param name='pShape'>
		/// <see cref="Sce.PlayStation.HighLevel.Physics2D.PhysicsShape"/>
		/// </param>
		public CardCrystallonEntity (Scene pScene, GamePhysics pGamePhysics, int pId,
		                             TextureInfo pTextureInfo, Vector2i pTileIndex2D, PhysicsShape pShape)
													: base(pScene, pGamePhysics, pTextureInfo, pTileIndex2D, pShape) {
			id = pId;
			_anim = null;
			GlowSprite = null;
			_sprite.Scale*=CARD_SCALAR;
			_keepOnScreenTimer = -1.0f;
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
		}
		
		// OVERRIDES ----------------------------------------------------------------------------------------------------------------------------------------------------
		
		/// <summary>
		/// Object-specific code for being added to any group.
		/// </summary>
		/// <returns>
		/// This object
		/// </returns>
		/// <param name='pGroup'>
		/// The destination group
		/// </param>
		public override AbstractCrystallonEntity BeAddedToGroup (GroupCrystallonEntity pGroup)
		{
			(this.getNode() as SpriteTile).Color.W = 1.0f; // make fully opaque if selected while fading in.
			HideGlow();
			pGroup.Attach( this );
			pGroup.PostAttach( this );
			return this;
		}
		
		/// <summary>
		/// Object-specific code for being added to the Selection Group
		/// </summary>
		/// <returns>
		/// This object
		/// </returns>
		public override AbstractCrystallonEntity BeSelected ( float delay = 0.0f )
		{
			this.getNode().Unschedule( KeepOnScreen );
			getNode().StopActionByTag(20);
//			if( delay > 0.0f ) {
				Sequence sequence = new Sequence() { Tag = 20 };
				sequence.Add( new DelayTime( delay ) );
				sequence.Add( new CallFunc( ()=> {
					playSound();
					ShowGlow();
				} ) );
				getNode().RunAction(sequence);
//			} 
//			else {
//				playSound();
//				ShowGlow();
//			}
			return this;
		}
		
		public override AbstractCrystallonEntity BeReleased(Vector2 pPosition) {
			if (!AppMain.ORIENTATION_MATTERS) {
				QOrientation.Instance.Apply(this,0);
			}
			CardManager.Instance.Add( this as CardCrystallonEntity );
			setBody(_physics.RegisterPhysicsBody(_physics.SceneShapes[(int)GamePhysics.BODIES.Card], 0.02f, 0.008f, pPosition));
			_sprite.Position = _body.Position * GamePhysics.PtoM;
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
			addToScene();
			HideGlow();
			
			// Rescue pieces that were released off screen
			_keepOnScreenTimer = 0.0f;
			this.getNode().ScheduleInterval( KeepOnScreen, 0.5f, 1);
			
			return this;
		}
		
		public override void BeTapped (float delay=0.0f)
		{
//			if( delay > 0.0f ) {
				getNode().StopActionByTag(20);
				Sequence sequence = new Sequence() { Tag = 20 };
				sequence.Add( new CallFunc( ()=> {
					playSound();
					ShowGlow( 0.2f );
				} ) );
				getNode().RunAction(sequence);
//			} 
//			else {
//				playSound();
//				ShowGlow( 0.2f );
//			}
		}
		
		public override void removeFromScene (bool doCleanup)
		{
			if (doCleanup) {
				if ( _anim != null ) {
					_anim.Cleanup();
					_anim = null;
				}
			}
			base.removeFromScene (doCleanup);
		}
		
		/// <summary>
		/// The once-per-frame update method.
		/// </summary>
		/// <param name='dt'>
		/// <c>float</c> elapsed time since last frame.
		/// </param>
		public override void Update (float dt)
		{
			base.Update(dt);
		}
		
		// METHODS ------------------------------------------------------------------------------------------------------------------------------------------------------
		
		public virtual void ApplyQualities() {
			if ( GameScene.currentLevel != 999) { // ------------------------------ PUZZLE MODE QUALITIES (FROM DATA)
				QualityManager.Instance.ApplyQualitiesToEntity( this );
			} else { // ----------------------------------------------------------- INFINITE MODE QUALITIES (RANDOM)
				foreach ( string quality in QualityManager.Instance.qualityDict.Keys ) {
					if ( QualityManager.Instance.scoringQualityList.Contains(quality) ) {
						QualityManager.Instance.SetQuality(this, quality, (int)System.Math.Floor(GameScene.Random.NextFloat() * 3.0f) );
					} else{
						QualityManager.Instance.SetQuality(this, quality, 0 );
					}
				}
			}
		}
		
		public void ShowGlow( float pLifetime = 0.0f ) {
			getNode().StopActionByTag(0);
			if (GlowSprite==null) return;
			if( pLifetime > 0.0f ) {
				Sequence sequence = new Sequence() { Tag = 0 };
				sequence.Add( new DelayTime( pLifetime ) );
				sequence.Add( new CallFunc( () => HideGlow() ) );
				getNode().RunAction( sequence );
			}
			GlowSprite.Visible = true;
		}
		
		public void HideGlow() {
			if (GlowSprite==null) return;
			GlowSprite.Visible = false;
		}
		
		public void setAnim( SpriteTile anim, int pStart, int pEnd ) {
			if (pStart == pEnd) {
				if (_anim != null) {
					this.getNode().RemoveChild(_anim, true);
					_anim = null;
				}
				return;
			}
			
			_anim = new SpriteTile( anim.TextureInfo, anim.TileIndex2D );
			
			_anim.Pivot = this.getNode().Pivot;

			_anim.RunAction( new Support.AnimationAction(_anim, pStart, pEnd, 0.1f*(1+pEnd-pStart), true) );

			if (getOrientation() == 2) {
				_anim.FlipU = true;
			} 

			this.getNode().AddChild(_anim);
		}
		
		protected void KeepOnScreen (float dt) {
			_keepOnScreenTimer += dt;
			if ( false == GameScene.PlayBounds.IsInside( this.getPosition() ) ) {
				CardManager.Instance.Teleport(this);
				_keepOnScreenTimer = -1.0f;
			} else if ( _keepOnScreenTimer > 2.0f ) {
				_keepOnScreenTimer = -1.0f;
			}
			
			if (_keepOnScreenTimer < 0.0f) {
				this.getNode().Unschedule(KeepOnScreen);
			}
		}
		
		public void TintTo( Vector4 pColor, float pDuration, bool alpha) {
			if (alpha) {
				(this.getNode() as SpriteBase).ShiftSpriteAlpha(pColor, pDuration);
			} else {
				(this.getNode() as SpriteBase).ShiftSpriteColor(pColor, pDuration);
			}
		}
	}
	
	public struct VertexData
	{
		public Vector2 position;
		public Vector2 uv;
		public Vector4 color;
	};
}