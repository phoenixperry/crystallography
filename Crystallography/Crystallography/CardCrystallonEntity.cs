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
		/// <summary>
		/// Offsets used by different pieces to form a cube.
		/// </summary>
		static readonly Vector2[] POSITION_OFFSETS = { 	new Vector2(0f,10.0f),
														new Vector2(-10.5f,-8.0f),
														new Vector2(10.5f,-8.0f) };
		protected readonly static float DEFAULT_SPEED = 1.0f;
		
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
		public CardCrystallonEntity (Scene pScene, GamePhysics pGamePhysics, 
		                             TextureInfo pTextureInfo, Vector2i pTileIndex2D, PhysicsShape pShape)
													: base(pScene, pGamePhysics, pTextureInfo, pTileIndex2D, pShape) {
			_sprite.Scale/=4f;
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
		}
		
		// OVERRIDES -----------------------------------------------------------------
		
		/// <summary>
		/// The once-per-frame update method.
		/// </summary>
		/// <param name='dt'>
		/// <c>float</c> elapsed time since last frame.
		/// </param>
		public override void Update (float dt)
		{
			base.Update(dt);
			if(getBody() != null) {
				_sprite.Position = _body.Position * GamePhysics.PtoM;
			}
		}
		
		// METHODS -------------------------------------------------------------------
		
	}
}