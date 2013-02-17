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
		private SpriteTile[] _sprites;
		private static SpriteSingleton _ss = SpriteSingleton.getInstance();
		private int _population;
//		private PhysicsBody _physicsBody;
		
		public enum POSITIONS {Top = 0, Left, Right};
		
		public Group(PhysicsBody physicsBody=null)
		{
//			_physicsBody = physicsBody;
			
			cards = new Card[3];
			
//			_textures = new Texture2D[3];
//			_tis = new TextureInfo[3];
//			_sprites = new SpriteTile[3];
			_population = 0;
			
//			_textures[0] = new Texture2D("Application/assets/images/topSide.png", false);
//			_textures[1] = new Texture2D("Application/assets/images/leftSide.png", false);
//			_textures[2] = new Texture2D("Application/assets/images/rightSide.png", false);
			
//			_sprites[0] = _ss.Get("topSide");
//			_sprites[1] = _ss.Get("leftSide");
//			_sprites[2] = _ss.Get("rightSide");
			
//			for (int i=0; i<3; i++) {
//				_tis[i] = new TextureInfo(_textures[i]);
//				_sprites[i] = new SpriteUV(_tis[i]);
//				_sprites[i].Scale = _sprites[i].CalcSizeInPixels()/4f;
//				_sprites[i].Pivot = new Vector2(0.5f, 0.5f);
//				_sprites[i].Visible = false;
//				this.AddChild(_sprites[i]);
//			}
//			_sprites[0].Position = new Vector2(0f,0f);
//			_sprites[1].Position = new Vector2(-12f,-18f);
//			_sprites[2].Position = new Vector2(10f,-18f);
			
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
		}
		
		public void tryAddingCard (Card card)
		{
			// Filter out current group members
			if (Array.IndexOf(cards,card) != -1) {
				return;
			}
			if ( card.groupID != -1) {
				Group g = GameScene.groups[card.groupID];
				if (g != this && Array.IndexOf(g.cards,card) != -1) {
					if (g.population >= 2 ) {
						// Filter out 2-groups merging with 2-groups
						if (this.population >= 2) {
							return;
						// Add single entity to existing group of 2
						} else {
							card = cards[0];
							clearGroup();
							g.tryAddingCard(card);
							return;
						}
					}
				}
			}
			addCard(card);
		}
		
		private void addCard (Card card)
		{
			for (int i=0; i<3; i++) {
				if ( cards[i] == null ) {
					cards[i] = card;
					switch(i) {
						case 0:	
							card.TileIndex2D = _ss.Get("topSide").TileIndex2D;
							_population++;
							break;
						case 1:
							card.TileIndex2D = _ss.Get ("leftSide").TileIndex2D;
							_population++;
							break;
						case 2:
							card.TileIndex2D = _ss.Get ("rightSide").TileIndex2D;
							_population++;
							break;
						default:
							break;
					}
					if (i==2) {
						complete = true;
					}
					card.groupID = cards[0].groupID;
					return;
				}
			}
		}
		
		public void removeCard (Card card)
		{
			for (int i=0; i<3; i++) {
				if ( cards[i] == card ) {
					card.groupID = -1;
					card.TileIndex2D = _ss.Get ("topSide").TileIndex2D;
					cards[i] = null;
					_population--;
				}
			}
		}
		
		public void clearGroup()
		{
			complete = false;
			for (int i=0; i<3; i++) {
				if ( cards[i] != null ) {
					removeCard (cards[i]);
				}
			}
		}
		
		public void analyze()
		{
			bool match = true;
			if( cards[0].Color == cards[1].Color && cards[0].Color == cards[2].Color ) {
//				return match;
			} else {
				match = ( cards[0].Color != cards[1].Color && 
				          cards[0].Color != cards[2].Color && 
				          cards[1].Color != cards[2].Color );
//				return match;
			}
			if (match) {
				System.Console.WriteLine("SET!");
				Cube cube = new Cube(cards, GameScene._physics.addCardPhysics(cards[0].Position));
				Director.Instance.CurrentScene.AddChild(cube);
				foreach ( Card c in cards ) {
					Director.Instance.CurrentScene.RemoveChild(c, true);
					GameScene._physics.removePhysicsBody(c.physicsBody);
				}
			}
			clearGroup();
		}
		
		public int population {
			get { return _population; }
		}
		
		public override void Update(float dt)
		{
//			this.Position = _physicsBody.Position * GamePhysics.PtoM;
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