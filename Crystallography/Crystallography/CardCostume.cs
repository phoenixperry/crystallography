using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;


namespace Crystallography
{

	public class CardCostume : SpriteUV 
	{
		public TextureInfo _textureInfo;
 		public Texture2D _texture;
		
		private SpriteSingleton instance; 
		private SpriteTile spriteTile; 
		private string spriteName; 
		public Texture2D myTexture; 
		
		public CardCostume ()
		{
			instance = SpriteSingleton.getInstance(); 
			setFace(); 
			spriteTile = instance.Get (spriteName); 
			spriteTile.Position = new Vector2(100.0f, 100.0f); 
			this.AddChild(spriteTile); 
			setColor(); 			
			
		}
		
		 	private void setColor() {
 			System.Random rand = new System.Random(); 
 			switch(rand.Next(1,4)) 
 			{
 				case 1: 
 				//pink
 					spriteTile.RunAction(new TintTo (new Vector4(0.96f,0.88f,0.88f,1.0f),0.1f)); 
 					break;
 				case 2:
 				//red 
 					spriteTile.RunAction(new TintTo (new Vector4(0.90f,0.075f,0.075f,1.0f),0.1f)); 
 					break;
 				case 3: 	
 				//teal
 					spriteTile.RunAction(new TintTo (new Vector4(0.16f,0.88f,0.88f,1.0f),0.1f)); 	
 					break; 
 				default:
 					break; 
 			}
 		}
 		
 		public void setFace() {
 			System.Random rand = new System.Random();
 			switch(rand.Next(1,4))
 				{
 					case 1:
 						spriteName = "leftSide";
 						break;
 					case 2:
 						spriteName = "rightSide";
 						break;
 					case 3:
 						spriteName = "topSide";
 						break;
 					default:
 					break;
 		}
 		}	
		
		public SpriteUV getCard() {
			myTexture = SpriteSingleton._texture; 
			
		}
	}
}

