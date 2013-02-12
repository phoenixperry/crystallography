using System;
using Sce.Pss.Core;
using Sce.Pss.Core.Graphics;
using Sce.Pss.Core.Imaging;
using Sce.Pss.HighLevel.GameEngine2D;
using Sce.Pss.HighLevel.GameEngine2D.Base;


namespace Pong
{
	public class Cubes : SpriteUV
	{
		public enum CubeType{pinkL,pinkR,pinkTop,redlL,redR,redTop,tealL,tealR,tealTop}
		private CubeType _type;  
		public Cubes (CubeType type) 	
		{
			_type = type; 
			//write logic that gets each cube type 
			
			
		}
		
		public override void Update (float dt)
        {
			
		}
		 ~Cubes()
        {
            this.TextureInfo.Texture.Dispose ();
            this.TextureInfo.Dispose();
        }
	}
}

