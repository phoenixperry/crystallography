using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public class Group : Node
	{
		public Card[] cards;
		public Boolean complete;
		
		private Texture2D[] _textures;
		private TextureInfo[] _tis;
		private SpriteUV[] _sprites;
		private PhysicsBody _physicsBody;
		
		public enum POSITIONS {Top = 0, Left, Right};
		
		public Group(PhysicsBody physicsBody)
		{
			_physicsBody = physicsBody;
			
			cards = new Card[3];
			
			_textures = new Texture2D[3];
			_tis = new TextureInfo[3];
			_sprites = new SpriteUV[3];
			
			_textures[0] = new Texture2D("Application/assets/images/topSide.png", false);
			_textures[1] = new Texture2D("Application/assets/images/leftSide.png", false);
			_textures[2] = new Texture2D("Application/assets/images/rightSide.png", false);
			
			for (int i=0; i<3; i++) {
				_tis[i] = new TextureInfo(_textures[i]);
				_sprites[i] = new SpriteUV(_tis[i]);
				_sprites[i].Scale = _tis[i].TextureSizef/4f;
				_sprites[i].Pivot = new Vector2(0.5f, 0.5f);
				_sprites[i].Visible = false;
				this.AddChild(_sprites[i]);
			}
			_sprites[0].Position = new Vector2(0f,0f);
			_sprites[1].Position = new Vector2(-12f,-18f);
			_sprites[2].Position = new Vector2(10f,-18f);
			
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
		}
		
		public void addCard (Card card)
		{
			for (int i=0; i<3; i++) {
				if ( cards[i] == null ) {
					cards[i] = card;
//					Director.Instance.CurrentScene.RemoveChild(card,false);
//					card.physicsBody.Sleep = true;
//					card.physicsBody.CollisionFilter = (1 << 1);
					card.TextureInfo = _tis[i];
//					_sprites[i].Color = card.Color;
//					_sprites[i].Visible = true;
					if (i==2) {
						complete = true;
					}
					return;
				}
			}
		}
		
		public void removeCard (Card card)
		{
			for (int i=0; i<3; i++) {
				if ( cards[i] == card ) {
					card.groupID = -1;
					cards[i] = null;
				}
			}
		}
		
		public void clearGroup()
		{
			for (int i=0; i<3; i++) {
				if ( cards[i] != null ) {
					removeCard (cards[i]);
				}
			}
		}
		
		public override void Update(float dt)
		{
			this.Position = _physicsBody.Position * GamePhysics.PtoM;
			base.Update (dt);
			
		}
		
		public void updateCard(Card card) 
		{
			if ( card == cards[1] ) {
				card.physicsBody.Position = new Vector2(cards[0].Position.X-12f,cards[0].Position.Y-18f) / GamePhysics.PtoM;
			} else {
				card.physicsBody.Position = new Vector2(cards[0].Position.X+10f,cards[0].Position.Y-18f) / GamePhysics.PtoM;
			}
		}
		
		~Group()
		{
			for (int i=0; i<3; i++) {
				_textures[i].Dispose();
				_tis[i].Dispose();
			}
			_textures = null;
			_tis = null;
		}
	}
}