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
		
		protected int countdownMax;
		protected Label countdownText;
		public int countdown;
		
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
					_anim.Rotation = new Vector2(1f,0f);
				} else if (pOrientation == 2) {
					_anim.Rotation = new Vector2(0.515038074910054f, -0.857167300702112f);
				} else {
					_anim.Rotation = new Vector2(0.484809620246337f, 0.874619707139396f);
				}
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
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
			
//			Node dummy = new Node();
//			dummy.Scale = 1/_sprite.Scale;
//			_sprite.AddChild(dummy);
//			dummy.AdHocDraw += DrawAnim;
			
//			ShaderProgram = new ShaderProgram("/Application/shaders/rotate.cgx");
//			AnimTexture  = new Texture2D("/Application/assets/animation/animation2.png", false);
//			frame = 0;
//			
//			dummy.ScheduleInterval( (dt) => {
//				frame++;
//				if (frame > 10) {
//					frame = 0;
//				}
//			}, 0.083f, 0);
//			
//			imm_quads = new ImmediateModeQuads< VertexData >( Director.Instance.GL, 4, VertexFormat.Float2, VertexFormat.Float2, VertexFormat.Float4 );
//			_scene.AdHocDraw += DrawAnim;
			
#if DEBUG
			Console.WriteLine(this.GetType().ToString() + " " + id.ToString() + " created");
#endif
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
//			_physics.RegisterPhysicsBody(_physics.SceneShapes[(int)GamePhysics.BODIES.Card], 0.1f, 0.01f, new Vector2(100f,100f + GameScene.Random.NextFloat()*100));
			_sprite.Position = _body.Position * GamePhysics.PtoM;
//			_sprite.Position = new Vector2(100f, 100f);
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
			addToScene();
			HideGlow();
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
//			if (_sprite.Position.X == float.NaN) {
//				int i = 0;
//			}
		}
		
		// METHODS ------------------------------------------------------------------------------------------------------------------------------------------------------
		
//		public void DrawAnim() {
//			Director.Instance.DrawHelpers.DrawCircle(new Vector2(Width/2.0f,Height/2.0f),60.0f,32);
//			float x1, x2, y1, y2;
//			Matrix4 transform = Director.Instance.GL.GetMVP();
//				
//			Director.Instance.GL.ModelMatrix.Push();
//			Director.Instance.GL.ModelMatrix.SetIdentity();
//			
//			ShaderProgram.SetUniformValue(ShaderProgram.FindUniform("MVP"), ref transform);
////			ShaderProgram.SetUniformValue(ShaderProgram.FindUniform("angle"), ref angle);
//
//			ShaderProgram.SetAttributeBinding(0, "iPosition");
//			ShaderProgram.SetAttributeBinding(1, "iUV");
//			ShaderProgram.SetAttributeBinding(2, "iColor");
//
//			Director.Instance.GL.Context.SetShaderProgram(ShaderProgram);
//			
//			imm_quads.ImmBeginQuads( 4 );
////			imm_quads.ImmBeginQuads( (uint)ActiveQualityParticles );
//			
////			int frame = 0;
//			
//			Vector4 white = Colors.White;
//			var column = frame % 4;
//			var row = (frame - column) / 4;
//			
//			y1 = 0.25f * row; //(float)System.Math.Floor((float)frame/4.0f);
//			y2 = 0.25f + y1;
//			x1 = 0.25f * column; //((float)frame-y1*4.0f);
//			x2 = 0.25f + x1;
//			
//			Vector2 z = Vector2.Zero;
//			
//			Director.Instance.GL.Context.SetTexture(0, AnimTexture);
//			imm_quads.ImmAddQuad( 
//				new VertexData() { position = z + new Vector2(0, 0), uv = new Vector2(x1, 1.0f-y1), color = white },
//				new VertexData() { position = z + new Vector2(Width, 0), uv = new Vector2(x2, 1.0f-y1), color = white },
//				new VertexData() { position = z + new Vector2(0, Height), uv = new Vector2(x1, 1.0f-y2), color = white },
//				new VertexData() { position = z + new Vector2(Width, Height), uv = new Vector2(x2, 1.0f-y2), color = white }
//			);
//			
//			imm_quads.ImmEndQuads();
//			
//			Director.Instance.GL.Context.SetShaderProgram(null);
//			Director.Instance.GL.Context.SetVertexBuffer(0, null);
//			Director.Instance.GL.ModelMatrix.Pop();
//			
//		}
		
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
				_anim = null;
				return;
			}
			_anim = new SpriteTile( anim.TextureInfo, anim.TileIndex2D );
//			_mask = new SpriteTile( QAnimation.Instance.maskTiles.TextureInfo, _orientationIndex);
//			_anim.Scale /= 4.0f;
			_anim.Pivot = this.getNode().Pivot;
//			_mask.Pivot = this.getNode().Pivot;
//			_mask.BlendMode.BlendFunc = new BlendFunc(BlendFuncMode.Add, BlendFuncFactor.DstColor, BlendFuncFactor.One);
//			_anim.BlendMode.BlendFunc = new BlendFunc(BlendFuncMode.ReverseSubtract, BlendFuncFactor.One, BlendFuncFactor.OneMinusSrcColor);
//			_anim.BlendMode.BlendFunc = new BlendFunc(BlendFuncMode.Add, BlendFuncFactor.DstAlpha, BlendFuncFactor.OneMinusSrcAlpha);
//			_anim.BlendMode.BlendFunc = animBlend;
//			_anim.BlendMode.Enabled = true;
			_anim.RunAction( new Support.AnimationAction(_anim, pStart, pEnd, 0.083f*(1+pEnd-pStart), true) );

			if (getOrientation() == 1) {
//				_anim.Rotation = new Vector2(1f,0f);
			} else if (getOrientation() == 2) {
				_anim.FlipU = true;
//				_anim.Rotate(Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Deg2Rad(-60.0f));
//				_anim.Rotation = new Vector2(0.515038074910054f, -0.857167300702112f);
//				_anim.Rotation = new Vector2(0.5f, -0.866025403784439f); //60
			} else {
//				_anim.Pivot = new Vector2(0.52f,0.54f);
				_anim.Position = new Vector2(0.00f, 0.015f);
//				_anim.Rotation = new Vector2(0.484809620246337f, 0.874619707139396f);
//				_anim.Rotation = new Vector2(0.5f, 0.866025403784439f);
				_anim.Rotate(Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Deg2Rad(-120.0f));
			}
			this.getNode().AddChild(_anim);
//			this.getNode().AddChild(_mask);
//			_mask.AddChild(_anim);
		}
		
		public void resetCountdown() {
			countdown = countdownMax;
		}
		
		public void setCountdown( int pCount ) {
			countdown = countdownMax = pCount;
			
			countdownText = new Label(pCount.ToString(), QCountdown.map);
			countdownText.Color = Colors.White;
			countdownText.HeightScale /=70f;
			countdownText.Pivot = new Vector2(0.5f, 0.5f);
			countdownText.Position = new Vector2(0.5f, 0.4f);
			this.getNode().AddChild(countdownText);
//			(_scene as GameScene).AddChild(countdownText);
		}
		
		public void advanceCountdown() {
			countdown--;
			if (countdown < 0) {
				countdown = countdownMax;
			}
			countdownText.Text = countdown.ToString();
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~CardCrystallonEntity() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
	
	public struct VertexData
	{
		public Vector2 position;
		public Vector2 uv;
		public Vector4 color;
	};
}