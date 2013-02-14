using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D; 
using Sce.PlayStation.HighLevel.GameEngine2D.Base; 

namespace CardMatchLogic
{
	public class AppMain
	{
		public static SpriteSingleton cubeFaces;
		public static SpriteTile sprite; 
		public static void Main (string[] args)
		{
			Sce.PlayStation.Core.Graphics.GraphicsContext
	 		context = new Sce.PlayStation.Core.Graphics.GraphicsContext(); 

			uint sprites_capacity = 500; 
			uint draw_helpers_capacity=400; 
		
			Director.Initialize(sprites_capacity, 
			                    draw_helpers_capacity, context); 
			Director.Instance.GL.Context.SetClearColor(Colors.Grey20); 
			
	
			var scene = new Scene(); 
			scene.Camera.SetViewFromViewport(); 
			
			cubeFaces = new SpriteSingleton(); 
			cubeFaces = SpriteSingleton.getInstance(); 
			sprite = cubeFaces.Get("leftSide"); 
		
			sprite.Position = scene.Camera.CalcBounds().Center;
			sprite.CenterSprite(); 
			//sprite.Scale = new Vector2(1,1); 
			scene.AddChild(sprite); 
			
	
  		 Director.Instance.RunWithScene(scene,true);
			//pink 
			sprite.RunAction(new TintTo (new Vector4(0.96f,0.88f,0.88f,1.0f),0.1f)); 	
			//red 
			//sprite.RunAction(new TintTo (new Vector4(0.90f,0.075f,0.075f,1.0f),0.1f)); 
			
			var spriteName="leftSide"; 
			sprite.TileIndex2D = cubeFaces.Get (spriteName).TileIndex2D;
			Console.WriteLine(sprite.TileIndex2D);
			//teal 
			//sprite.RunAction(new TintTo (new Vector4(0.16f,0.88f,0.88f,1.0f),0.1f)); 	

			var cube = new Cube(); 
			cube.card1("r"); 
			cube.card2 ("b"); 
			cube.card3("p"); 
			
				while(!Input2.GamePad0.Cross.Press) 
			{	
				Sce.PlayStation.Core.Environment.SystemEvents.CheckEvents(); 
				Director.Instance.Update(); 
				Director.Instance.Render(); 
				Director.Instance.GL.Context.SwapBuffers(); 
				Director.Instance.PostSwap();
			
			}
		}
		
	
	}}

