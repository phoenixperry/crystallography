using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D; 
using Sce.PlayStation.HighLevel.GameEngine2D.Base; 

namespace SpriteSheet
{
 public class AppMain
 {
  private static Walker walker;
  
  public static void Main (string[] args)
  {
   Director.Initialize();
   Director.Instance.GL.Context.SetClearColor(255,255,255,0);

   walker = new Walker("walk.png","walk.xml");
		Console.WriteLine("made"); 
   
   var scene = new Scene();
   scene.Camera.SetViewFromViewport();
   var sprite = walker.Get("Walk_left00");

   sprite.Position = scene.Camera.CalcBounds().Center;
   sprite.CenterSprite();
   sprite.Scale = new Vector2(1,1);
   scene.AddChild(sprite);
   
   Director.Instance.RunWithScene(scene,true);
   
   System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
   
   int spriteOffset = 0;
			
   timer.Start();
   bool walkLeft = true;
   while(true)
   {
    if(timer.ElapsedMilliseconds > 100f)
    {
     string spriteName;
     
     if(walkLeft) 
      spriteName= "Walk_left" + spriteOffset.ToString("00");
     else 
      spriteName= "Walk_right" + spriteOffset.ToString("00");
      
     sprite.TileIndex2D = walker.Get (spriteName).TileIndex2D;
     
     if(spriteOffset >= 18)
     {
      spriteOffset = 0;
      walkLeft = !walkLeft;
     }
     else 
      spriteOffset++;
     
     timer.Reset();
     timer.Start();
    }
		Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Update(); 
		Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Render(); 
		Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.GL.Context.SwapBuffers(); 
		Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.PostSwap(); 
	
				
	}
}
}}
