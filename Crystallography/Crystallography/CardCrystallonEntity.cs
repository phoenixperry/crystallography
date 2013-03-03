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
		static readonly Vector2[] POSITION_OFFSETS = { 	new Vector2(0f,10.0f),
														new Vector2(-10.5f,-8.0f),
														new Vector2(10.5f,-8.0f) };
		protected readonly static float DEFAULT_SPEED = 1.0f;
		
//		protected CardData _data;
		
		// GET & SET ----------------------------------------------------------------
		
		public override Vector2 getAttachOffset (int position)
		{
			return POSITION_OFFSETS[position];
		}
		
		// CONSTRUCTORS -------------------------------------------------------------
		
		public CardCrystallonEntity (Scene pScene, GamePhysics pGamePhysics, 
		                             TextureInfo pTextureInfo, Vector2i pTileIndex2D, PhysicsShape pShape)//, 
//		                             CardData data = null)
													: base(pScene, pGamePhysics, pTextureInfo, pTileIndex2D, pShape){
//			_data = data;
			_sprite.Scale/=4f;
			setVelocity(DEFAULT_SPEED, GameScene.Random.NextAngle());
		}
		
		// OVERRIDES -----------------------------------------------------------------
		
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