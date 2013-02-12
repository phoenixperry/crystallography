using System;
using System.Linq; 
using System.Xml; 
using System.Collections.Generic; 
using System.IO; 

using System.Xml.Linq; 
using Sce.PlayStation.Core.Graphics; 
using Sce.PlayStation.HighLevel.GameEngine2D; 
using Sce.PlayStation.HighLevel.GameEngine2D.Base; 



namespace Pong
{
	public class SpriteSheetSingleton : SpriteUV
	{
		private TextureInfo _textureInfo;
  private Texture2D _texture2D;
  private System.Collections.Generic.Dictionary<string, Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i> _sprites;
		
		//finish me sleepy human... zzzz 
		public SpriteSheetSingleton ()
		{
			  this.Camera.SetViewFromViewport();
   
   _texture2D = new Texture2D("/Application/images/cubePieces.png",false);
   _textureInfo = new TextureInfo(_texture2D);
   
   var w = Director.Instance.GL.Context.GetViewport().Width;
   var h = Director.Instance.GL.Context.GetViewport().Height;
   
   System.Random rand = new System.Random();
   
		}
	}
	}


