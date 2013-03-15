using System;
using System.Linq; 
using System.Xml; 
using System.Collections.Generic; 
using System.IO; 

using System.Xml.Linq; 
using Sce.PlayStation.Core.Graphics; 
using Sce.PlayStation.HighLevel.GameEngine2D; 
using Sce.PlayStation.HighLevel.GameEngine2D.Base; 

namespace Crystallography
{		 
	public class SpriteSingleton {
	
		public TextureInfo _textureInfo;
		public Texture2D _texture;
		
		public TextureInfo _glitchInfo; 
		public Texture2D _glitchTexture; 
		
		public TextureInfo _fallInfo; 
		public TextureInfo _fallTexture; 
		
		
		
		private System.Collections.Generic.Dictionary<string, Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i> _sprites;  
		private static SpriteSingleton instance; 
		private static bool isOkToCreate = false;
		public static int test=0; 
		
		// CONSTRUCTOR ---------------------------------------------------------------------------------------
		
		public SpriteSingleton () {
			if(!isOkToCreate) Console.WriteLine (this + "is a singleton. Use get instance"); 
			if(isOkToCreate){
				FileStream fileStream = File.OpenRead("/Application/assets/images/gamePieces.xml");
				StreamReader fileStreamReader = new StreamReader(fileStream);
				string xml = fileStreamReader.ReadToEnd();
				fileStreamReader.Close();
				fileStream.Close();
				XDocument doc = XDocument.Parse(xml);
			
				var lines = from sprite in doc.Root.Elements("sprite")
					select new {
						Name = sprite.Attribute("n").Value,
						X1 = (int)sprite.Attribute ("x"),
						Y1 = (int)sprite.Attribute ("y"),
		      			Height = (int)sprite.Attribute ("h"),
		      			Width = (int)sprite.Attribute("w")

				///////////////////////////////////////////////
				FileStream glitchStream = File.OpenRead("/Application/assets/images/gamePieces.xml");
				StreamReader fileStreamReader = new StreamReader(fileStream);
				string xml = fileStreamReader.ReadToEnd();
				fileStreamReader.Close();
				fileStream.Close();
				XDocument doc = XDocument.Parse(xml);
			
				var lines = from sprite in doc.Root.Elements("sprite")
					select new {
						Name = sprite.Attribute("n").Value,
						X1 = (int)sprite.Attribute ("x"),
						Y1 = (int)sprite.Attribute ("y"),
		      			Height = (int)sprite.Attribute ("h"),
		      			Width = (int)sprite.Attribute("w")
				
				};
		   		
   				_sprites = new Dictionary<string,Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i>(); 
	    		foreach(var curLine in lines)
				{
	    			_sprites.Add(curLine.Name,new Vector2i((curLine.X1/curLine.Width),(curLine.Y1/curLine.Height)));
				//note if you add more than one line of sprites you must do this
				// _sprites.Add(curLine.Name,new Vector2i((curLine.X1/curLine.Width),9-(curLine.Y1/curLine.Height))); 
				//where 9 is the num of rows minus 1 to reverse the order :/ 
	   			}
   				_texture = new Texture2D("/Application/assets/images/gamePieces.png", false);
   				_textureInfo = new TextureInfo(_texture,new Vector2i(9,1));
  
			}
	  		if(!isOkToCreate) {
				Console.WriteLine("this is a singleton. access via get Instance"); 
			}
		}	
		
		// METHODS ----------------------------------------------------------------------------------------------
		
  		public static SpriteSingleton getInstance(){
			if( instance ==null ) {
				isOkToCreate = true; 
				instance = new SpriteSingleton(); 
				isOkToCreate = false; 
//				test++;
//				Console.WriteLine("Singleton instance created" + isOkToCreate + test);
			}
			return instance; 
		}
		
		public Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile Get(int x, int y) {
			var spriteTile = new SpriteTile(_textureInfo);
		   	spriteTile.TileIndex2D = new Vector2i(x,y);
		   	spriteTile.Quad.S = new Sce.PlayStation.Core.Vector2 (168,146);
		   	return spriteTile;
		}

		public Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile Get(string name) {
			return Get (_sprites[name].X, _sprites[name].Y);
		}
		
		// DESRUCTOR ------------------------------------------------------------------------------------------
		
		~SpriteSingleton() {
   			_texture.Dispose();
   			_textureInfo.Dispose ();
			_sprites.Clear();
			_sprites = null;
			instance = null;
  		}
	}
}

