using System;
using System.Linq; 
using System.Xml; 
using System.Collections.Generic; 
using System.IO; 

using System.Xml.Linq; 
using Sce.PlayStation.Core.Graphics; 
using Sce.PlayStation.HighLevel.GameEngine2D; 
using Sce.PlayStation.HighLevel.GameEngine2D.Base; 

namespace CardMatchLogic
{

		 public class SpriteSingleton
 {
 
  private TextureInfo _textureInfo;
  private Texture2D _texture;
  private System.Collections.Generic.Dictionary<string, Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i> _sprites;
  private static SpriteSingleton instance; 
  private static bool isOkToCreate = false; 	

  public SpriteSingleton ()
  {
	if(isOkToCreate){
		FileStream fileStream = File.OpenRead( "/Application/assets/gamePieces.xml");
		StreamReader fileStreamReader = new StreamReader(fileStream);
		string xml = fileStreamReader.ReadToEnd();
		fileStreamReader.Close();
		fileStream.Close();
		XDocument doc = XDocument.Parse(xml);
			
			var lines = from sprite in doc.Root.Elements("sprite")
			select new
		     {
		      Name = sprite.Attribute("n").Value,
		      X1 = (int)sprite.Attribute ("x"),
		      Y1 = (int)sprite.Attribute ("y"),
		      Height = (int)sprite.Attribute ("h"),
		      Width = (int)sprite.Attribute("w")
		     };
		   
		   _sprites = new Dictionary<string,Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i>(); 
		    foreach(var curLine in lines)
			{
		   // _sprites.Add(curLine.Name,new Vector2i((curLine.X1/curLine.Width),2-(curLine.Y1/curLine.Height)));
			_sprites.Add(curLine.Name, new Vector2i(curLine.X1, curLine.Y1)); 
		   }
		   _texture = new Texture2D("/Application/assets/gamePieces.png", false);
		   _textureInfo = new TextureInfo(_texture,new Vector2i(3,1));
					//the Vector2i are number of sprites, number of rows - playstation reads from bottom up 
			}
	  if(!isOkToCreate) {
			Console.WriteLine("this is a singleton. access via get Instance"); 
		}
			
}			
  ~SpriteSingleton()
  {
   _texture.Dispose();
   _textureInfo.Dispose ();
  }
  public static SpriteSingleton getInstance(){
			if(instance==null){
				isOkToCreate = true; 
				instance = new SpriteSingleton(); 
				isOkToCreate = false; 
				Console.WriteLine("Singleton instance created"); 
			}
			return instance; 
		}
  public Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile Get(int x, int y)
  {
   var spriteTile = new SpriteTile(_textureInfo);
   spriteTile.TileIndex2D = new Vector2i(x,y);
   spriteTile.Quad.S = new Sce.PlayStation.Core.Vector2 (168,146);
			
   return spriteTile;
  }
		public Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile Get(string name) 
		{
			return Get (_sprites[name].X, _sprites[name].Y); 
		
		}
		}
	}

