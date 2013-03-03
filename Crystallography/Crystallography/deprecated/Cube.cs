using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;
using Sce.PlayStation.Core.Imaging;

namespace Crystallography
{
	public class Cube : SpriteUV
	{	
		private string _card1; 
		private string _card2; 
		private string _card3; 
		private int size;
		
		private PhysicsBody _physicsBody;
		private static Image _imgTop;
		private static Image _imgLeft; 
		private static Image _imgRight;
		private static Image _imgTopStripe;
		private static Image _imgLeftStripe; 
		private static Image _imgRightStripe;
		private static Image _imgTopDot;
		private static Image _imgLeftDot; 
		private static Image _imgRightDot;
		private static Byte[] data;
		private static bool initialized = false;
		private static int[] colorData;
		
		public Cube (Card[] cards, PhysicsBody physicsBody=null)
		{
			if (!initialized) {
				Initialize();
			}
			_physicsBody = physicsBody;
			
			this.TextureInfo = new TextureInfo();
			this.TextureInfo.Texture = new Texture2D(256, 256,false,PixelFormat.Rgba);
//			this.TextureInfo.Texture = new Texture2D(167, 191,false,PixelFormat.Rgba);
			
			setColorData (cards[0].Color);
			if ( cards[0].cardData.pattern == (int)CardData.PATTERN.SOLID ) {
				addToTexture (_imgTop, new Vector2i(45,0), colorData );
			} else if ( cards[0].cardData.pattern == (int)CardData.PATTERN.STRIPE ) {
				addToTexture (_imgTopStripe, new Vector2i(45,0), colorData );
			} else {
				addToTexture (_imgTopDot, new Vector2i(45,0), colorData );
			}
			setColorData (cards[1].Color);
			if ( cards[0].cardData.pattern == (int)CardData.PATTERN.SOLID ) {
				addToTexture (_imgLeft, new Vector2i(0,-78), colorData );
			} else if ( cards[0].cardData.pattern == (int)CardData.PATTERN.STRIPE ) {
				addToTexture (_imgLeftStripe, new Vector2i(0,-78), colorData );
			} else {
				addToTexture (_imgLeftDot, new Vector2i(0,-78), colorData );
			}
//			addToTexture (_imgLeft, new Vector2i(0,-78), colorData );
			setColorData (cards[2].Color);
			if ( cards[0].cardData.pattern == (int)CardData.PATTERN.SOLID ) {
				addToTexture (_imgRight, new Vector2i(92,-78), colorData);
			} else if ( cards[0].cardData.pattern == (int)CardData.PATTERN.STRIPE ) {
				addToTexture (_imgRightStripe, new Vector2i(92,-78), colorData);
			} else {
				addToTexture (_imgRightDot, new Vector2i(92,-78), colorData);
			}
//			addToTexture (_imgRight, new Vector2i(92,-78), colorData);
			
			this.Scale = this.CalcSizeInPixels()/4f;
			this.Pivot = new Sce.PlayStation.Core.Vector2(0.5f,0.5f);
			
			System.Random rand = new System.Random();
            this.Position = new Sce.PlayStation.Core.Vector2(0,0);
			float angle = (float)rand.Next(0,360);
        
            if((angle%90) <=25) angle +=25.0f;
            this._physicsBody.Velocity = new Vector2(0.0f,Card.BALL_VELOCITY).Rotate(PhysicsUtility.GetRadian(angle));;
            
            Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
		}
		
		private void Initialize()
		{
			_imgTop = new Image("Application/assets/images/topSide.png");
			_imgTop.Decode();
			_imgLeft = new Image("Application/assets/images/leftSide.png");
			_imgLeft.Decode();
			_imgRight = new Image("Application/assets/images/rightSide.png");
			_imgRight.Decode();
			_imgTopStripe = new Image("Application/assets/images/stripeTop.png");
			_imgTopStripe.Decode();
			_imgLeftStripe = new Image("Application/assets/images/stripeLeft.png");
			_imgLeftStripe.Decode();
			_imgRightStripe = new Image("Application/assets/images/stripeRight.png");
			_imgRightStripe.Decode();
			_imgTopDot = new Image("Application/assets/images/dotTop.png");
			_imgTopDot.Decode();
			_imgLeftDot = new Image("Application/assets/images/dotLeft.png");
			_imgLeftDot.Decode();
			_imgRightDot = new Image("Application/assets/images/dotRight.png");
			_imgRightDot.Decode();
			initialized = true;
		}
		
		private void setColorData(Vector4 color)
		{
			colorData = new int[] { (int)(color.X * 255f), (int)(color.Y * 255f), (int)(color.Z * 255f) };
		}
		
		public override void Update (float dt)
        {
			standardMovement();
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
            _physicsBody.Velocity = normalizedVel * Card.BALL_VELOCITY;
		}
		
		private void addToTexture(Image img, Vector2i offset, int[] color)
		{
			data = img.ToBuffer ();
			
			for (int j=0; j < data.Length/4; j++) {
				if (data[j*4+3] != 0) {
					if( 255 == data[j*4] && 255 == data[j*4+1] && 255 == data[j*4+2]) {
						data[j*4] = (Byte)color[0];
						data[j*4+1] = (Byte)color[1];
						data[j*4+2] = (Byte)color[2];
					}
					var x = j%168 + offset.X;
					var y = (j-x)/168 - offset.Y;
					this.TextureInfo.Texture.SetPixels(0,data,PixelFormat.Rgba,j*4,167*4,x,y,1,1);
				}
			}
		}
		
		public void SetSize(){
			size++; 
		}
		
		public  void card1(string s){
				_card1 =s; 
			}
			public  void card2(string s){
				_card2 =s;
			}
			public  void card3 (string s){
				_card3 =s; 
			}
				public bool testCube(){
				if((_card1 != _card2 && _card1 != _card3 && _card2 != _card3) || 
			   (_card1 == _card2 && _card1== _card3)){
					Console.WriteLine("it's a cube"); 
					return true;
		
				}
				else{
					Console.WriteLine("not a cube"); 
					return false;} 
					
			}
	
	}
}

