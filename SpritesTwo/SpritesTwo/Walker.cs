using System;
using System.Linq; 
using System.Xml; 
using System.Collections.Generic; 
using System.IO; 

using System.Xml.Linq; 
using Sce.PlayStation.Core.Graphics; 
using Sce.PlayStation.HighLevel.GameEngine2D; 
using Sce.PlayStation.HighLevel.GameEngine2D.Base; 

namespace SpriteSheet
{

		 public class Walker
 {
 
  private TextureInfo _textureInfo;
  private Texture2D _texture;
  private System.Collections.Generic.Dictionary<string, Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i> _sprites;
  
  public Walker (string imageFilename, string imageDetailsFilename)
  {
   //XDocument doc = XDocument.Load ("application/" + imageDetailsFilename);
  
//XDocument doc = XDocument.Load ("application/" + imageDetailsFilename);
FileStream fileStream = File.OpenRead( "/Application/" + imageDetailsFilename);
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
    _sprites.Add(curLine.Name,new Vector2i((curLine.X1/curLine.Width),9-(curLine.Y1/curLine.Height)));
   }
   _texture = new Texture2D("/Application/" + imageFilename,false);
   _textureInfo = new TextureInfo(_texture,new Vector2i(4,10));
  
  }
  
  ~Walker()
  {
   _texture.Dispose();
   _textureInfo.Dispose ();
  }
  
  public Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile Get(int x, int y)
  {
   var spriteTile = new SpriteTile(_textureInfo);
   spriteTile.TileIndex2D = new Vector2i(x,y);
   spriteTile.Quad.S = new Sce.PlayStation.Core.Vector2 (128,96);
   return spriteTile;
  }
		public Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile Get(string name) 
		{
			return Get (_sprites[name].X, _sprites[name].Y); 
		
		}
		}
	}

