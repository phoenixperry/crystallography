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
		protected readonly static float DEFAULT_SPEED = 0.3f;
		
//		protected readonly static BlendFunc animBlend = new BlendFunc(BlendFuncMode.Add, BlendFuncFactor.DstColor, BlendFuncFactor.SrcAlpha);
//		protected readonly static BlendFunc animBlend = new BlendFunc(BlendFuncMode.Subtract, BlendFuncFactor.DstColor, BlendFuncFactor.Zero);
		
//		protected ShaderProgram ShaderProgram;
//		protected Texture2D AnimTexture;
//		protected ImmediateModeQuads< VertexData > imm_quads;
//		protected int frame;
		
		protected SpriteTile _anim;
//		protected SpriteTile _mask;
//		protected SpriteTile _glowSprite;
		protected int _glowIndex;
		
		protected float _keepOnScreenTimer;
		
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
			if( delay > 0.0f ) {
				Sequence sequence = new Sequence();
				sequence.Add( new DelayTime( delay ) );
				sequence.Add( new CallFunc( ()=> {
					playSound();
					ShowGlow();
				} ) );
				getNode().RunAction(sequence);
			} else {
				playSound();
				ShowGlow();
			}
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
			if( delay > 0.0f ) {
				Sequence sequence = new Sequence();
				sequence.Add( new DelayTime( delay ) );
				sequence.Add( new CallFunc( ()=> {
					playSound();
					ShowGlow( 0.2f );
				} ) );
				getNode().RunAction(sequence);
			} else {
				playSound();
				ShowGlow( 0.2f );
			}
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
		
		public void ShowGlow( float pLifetime = 0.0f ) {
			getNode().StopActionByTag(0);
			if (GlowSprite==null) return;
			if( pLifetime > 0.0f ) {
				Sequence sequence = new Sequence() {
					Tag = 0
				};
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
			this.getNode().StopActionByTag(alpha ? 2 : 3);
			var entityTintTo = new TintTo(Support.RGBToHSB(pColor), pDuration) {
				Tag = alpha ? 2 : 3,
				Get = () => Support.RGBToHSB(( this.getNode() as SpriteBase).Color),
				Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.PowEaseOut(t,2)
			};
			if (alpha) {
				entityTintTo.Set = (value) => { 
					(this.getNode() as SpriteBase).Color.W = value.W;
				};
			} else {
				entityTintTo.Set = (value) => {
					var hsba = Support.HSBToRGB(value);
					(this.getNode() as SpriteBase).Color.X = hsba.X;
					(this.getNode() as SpriteBase).Color.Y = hsba.Y;
					(this.getNode() as SpriteBase).Color.Z = hsba.Z;
				};
			}
			this.getNode().RunAction(entityTintTo);
		}
		
//		public void resetCountdown() {
//			countdown = countdownMax;
//		}
//		
//		public void setCountdown( int pCount ) {
//			countdown = countdownMax = pCount;
//			
//			countdownText = new Label(pCount.ToString(), QCountdown.map);
//			countdownText.Color = Colors.White;
//			countdownText.HeightScale /=70f;
//			countdownText.Pivot = new Vector2(0.5f, 0.5f);
//			countdownText.Position = new Vector2(0.5f, 0.4f);
//			this.getNode().AddChild(countdownText);
//		}
//		
//		public void advanceCountdown() {
//			countdown--;
//			if (countdown < 0) {
//				countdown = countdownMax;
//			}
//			countdownText.Text = countdown.ToString();
//		}
		
	}
	
	public struct VertexData
	{
		public Vector2 position;
		public Vector2 uv;
		public Vector4 color;
	};
}