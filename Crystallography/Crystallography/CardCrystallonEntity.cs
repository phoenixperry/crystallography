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
		
		protected SpriteTile _anim;
		
		protected int countdownMax;
		protected Label countdownText;
		public int countdown;
		
		// GET & SET ----------------------------------------------------------------
		
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
		}
		
		// CONSTRUCTORS -------------------------------------------------------------
		
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
			_sprite.Scale*=0.7f;
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
		}
		
		// OVERRIDES -----------------------------------------------------------------
		
		public override AbstractCrystallonEntity BeReleased(Vector2 pPosition) {
			if (!AppMain.ORIENTATION_MATTERS) {
				QOrientation.Instance.Apply(this,0);
			}
			CardManager.Instance.Add( this as CardCrystallonEntity );
			setBody(_physics.RegisterPhysicsBody(_physics.SceneShapes[(int)GamePhysics.BODIES.Card], 0.1f, 0.01f, pPosition));
//			_physics.RegisterPhysicsBody(_physics.SceneShapes[(int)GamePhysics.BODIES.Card], 0.1f, 0.01f, new Vector2(100f,100f + GameScene.Random.NextFloat()*100));
			_sprite.Position = _body.Position * GamePhysics.PtoM;
//			_sprite.Position = new Vector2(100f, 100f);
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
			addToScene();
			return this;
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
		
		// METHODS -------------------------------------------------------------------
		

		
		public void setAnim( SpriteTile anim, int pStart, int pEnd ) {
			
			_anim = new SpriteTile( anim.TextureInfo, anim.TileIndex2D );
			_anim = new SpriteTile( anim.TextureInfo, anim.TileIndex2D );
			_anim.Pivot = this.getNode().Pivot;
			_anim.BlendMode.BlendFunc = new BlendFunc(BlendFuncMode.ReverseSubtract, BlendFuncFactor.One, BlendFuncFactor.OneMinusSrcColor);
			_anim.BlendMode.Enabled = true;
			if (pStart == pEnd) {
				_anim = null;
			} else {
				_anim.RunAction( new Support.AnimationAction(_anim, pStart, pEnd, 1.0f, true) );
			}
			if (_anim != null) {
				if (getOrientation() == 1) {
					_anim.Rotation = new Vector2(1f,0f);
				} else if (getOrientation() == 2) {
					_anim.Rotation = new Vector2(0.515038074910054f, -0.857167300702112f);
				} else {
					_anim.Rotation = new Vector2(0.484809620246337f, 0.874619707139396f);
				}
				this.getNode().AddChild(_anim);
			}
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
		
		// DESTRUCTOR -----------------------------------------------------------------------------------
#if DEBUG
		~CardCrystallonEntity() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}