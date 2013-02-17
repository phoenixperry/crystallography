using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	
	// HANDY DATA STRUCTURE
	public struct CardData{
		public enum COLOR { WHITE=0x1, RED=0x2, BLUE=0x4 };
		public enum PATTERN { SOLID=0x10, DOT=0x20, STRIPE=0x40 };
		public enum SOUND { A=0x100, B=0x200, C=0x400 };
		
		public int color;
		public int pattern;
		public int sound;
		
		public CardData( int Color, int Pattern, int Sound )
		{
			color = Color;
			pattern = Pattern;
			sound = Sound;
		}
	}
	
	// MAIN CARD CLASS
    public class Card : SpriteTile
    {
		
		
		//singleton vars
		private SpriteSingleton instance;
		private SpriteTile spriteTile;
		private string spriteName;
		public int[] attributes;
		public CardData cardData;


        private PhysicsBody _physicsBody;
		private static SpriteSingleton _ss = SpriteSingleton.getInstance();
//		private SpriteSingleton _ss;
        // Change this value to make the game faster or slower
        public const float BALL_VELOCITY = 1.0f;

		public int groupID;
        
        public Card (PhysicsBody physicsBody, CardData data)
        {
			instance = SpriteSingleton.getInstance();
			cardData = data;
//			setFace(); 
			spriteName = "topSide";
			spriteTile = instance.Get (spriteName); 
			spriteTile.Position = new Vector2(10.0f, 10.0f); 
			this.AddChild(spriteTile);
			setColor(data.color);
			setSound(data.sound);
			spriteTile.Position = new Vector2(50.0f, 50.0f);
		
            _physicsBody = physicsBody;
            
			groupID = -1;
			this.TextureInfo = _ss.Get ("topSide").TextureInfo;
			this.TileIndex2D = _ss.Get ("topSide").TileIndex2D;
//            this.TextureInfo = new TextureInfo(new Texture2D("Application/assets/images/topSide.png", false));
//            this.Scale = this.TextureInfo.TextureSizef/4f;
			this.Scale = this.CalcSizeInPixels()/4f;
            this.Pivot = new Sce.PlayStation.Core.Vector2(0.5f,0.5f);
			
			
			System.Random rand = new System.Random();
            this.Position = new Sce.PlayStation.Core.Vector2(0,0);
//                (float)Director.Instance.GL.Context.GetViewport().Width * (float)rand.NextDouble(),
//                (float)Director.Instance.GL.Context.GetViewport().Height * (float)rand.NextDouble());
			
//			_physicsBody.Position = new Sce.PlayStation.Core.Vector2(
//				(float)Director.Instance.GL.Context.GetViewport().Width * (float)rand.NextDouble(),
//                (float)Director.Instance.GL.Context.GetViewport().Height * (float)rand.NextDouble());
            
			
            
            //Right angles are exceedingly boring, so make sure we dont start on one
            //So if our Random angle is between 90 +- 25 degrees or 270 +- 25 degrees
            //we add 25 degree to value, ie, making 90 into 115 instead
            
            float angle = (float)rand.Next(0,360);
        
            if((angle%90) <=25) angle +=25.0f;
            this._physicsBody.Velocity = new Vector2(0.0f,BALL_VELOCITY).Rotate(PhysicsUtility.GetRadian(angle));;
            
            Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
        }
        
		public PhysicsBody physicsBody {
			get { return _physicsBody; }
		}
		
        public override void Update (float dt)
        {
	// Ungrouped
            if ( groupID == -1 ) {
            	standardMovement ();
			// Grouped
			} else {
				// group movements are based off the "top" members
				if ( this == GameScene.groups[groupID].cards[0] ) {
					standardMovement ();
				} else {
					GameScene.groups[groupID].updateCard(this);
				}
			}
            
            this.Position = _physicsBody.Position * GamePhysics.PtoM;

        }
        
		private void standardMovement() {
			// We want to prevent situations where the balls is bouncing side to side
            // so if there isnt a certain amount of movement on the Y axis, set it to + or - 0.2 randomly
            // Note, this can result in the ball bouncing "back", as in it comes from the top of the screen
            // But riccochets back up at the user.  Frankly, this keeps things interesting imho
			var normalizedVel = _physicsBody.Velocity.Normalize();
        	    if(System.Math.Abs (normalizedVel.Y) < 0.2f) {
                	System.Random rand = new System.Random();
                	if(rand.Next (0,1) == 0) {
                    	normalizedVel.Y+= 0.2f;
					} else {
                    	normalizedVel.Y-= 0.2f;
					}
				}
			// Pong is a mess with physics, so just fix the ball velocity
            // Otherwise the ball could get faster and faster ( or slower ) on each collision
            _physicsBody.Velocity = normalizedVel * BALL_VELOCITY;
		}
		private void setColor(int color) {
// 			System.Random rand = new System.Random(); 
 			switch(color) 
 			{
 				case (int)CardData.COLOR.WHITE: 
 				//pink
// 					spriteTile.RunAction(new TintTo (new Vector4(0.96f,0.88f,0.88f,1.0f),0.1f)); 
					this.Color = new Vector4(0.96f,0.88f,0.88f,1.0f);
 					break;
 				case (int)CardData.COLOR.RED:
 				//red
// 					spriteTile.RunAction(new TintTo (new Vector4(0.90f,0.075f,0.075f,1.0f),0.1f));
					this.Color = new Vector4(0.90f,0.075f,0.075f,1.0f);
 					break;
 				case (int)CardData.COLOR.BLUE: 	
 				//teal
 					spriteTile.RunAction(new TintTo (new Vector4(0.16f,0.88f,0.88f,1.0f),0.1f)); 
					this.Color = new Vector4(0.16f,0.88f,0.88f,1.0f);
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
		
		public void setSound(int sound) {
			switch(sound){
				case (int)CardData.SOUND.A:
					break;
				case (int)CardData.SOUND.B:
					break;
				case (int)CardData.SOUND.C:
					break;
				default:
					break;
			}
		}
		
        ~Card()
        {
            this.TextureInfo.Texture.Dispose();
            this.TextureInfo.Dispose();
        }
    }
}