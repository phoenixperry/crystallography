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
			addToTexture (_imgTop, new Vector2i(45,0), colorData );
			setColorData (cards[1].Color);
			addToTexture (_imgLeft, new Vector2i(0,-78), colorData );
			setColorData (cards[2].Color);
			addToTexture (_imgRight, new Vector2i(92,-78), colorData);
			
			this.Scale = this.CalcSizeInPixels()/4f;
			this.Pivot = new Sce.PlayStation.Core.Vector2(0.5f,0.5f);
		}
		
		private void Initialize()
		{
			_imgTop = new Image("Application/assets/images/topSide.png");
			_imgTop.Decode();
			_imgLeft = new Image("Application/assets/images/leftSide.png");
			_imgLeft.Decode();
			_imgRight = new Image("Application/assets/images/rightSide.png");
			_imgRight.Decode();
			initialized = true;
		}
		
		private void setColorData(Vector4 color)
		{
			colorData = new int[] { (int)(color.X * 255f), (int)(color.Y * 255f), (int)(color.Z * 255f) };
		}
		
		private void addToTexture(Image img, Vector2i offset, int[] color)
		{
			data = img.ToBuffer ();
			
			for (int j=0; j < data.Length/4; j++) {
				if (data[j*4+3] != 0) {
					data[j*4] = (Byte)color[0];
					data[j*4+1] = (Byte)color[1];
					data[j*4+2] = (Byte)color[2];
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

