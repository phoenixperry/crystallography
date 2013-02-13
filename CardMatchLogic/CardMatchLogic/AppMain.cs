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
		public static SpriteSingleton leftFace;
		public static SpriteSingleton rightFace;
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
			leftFace = new SpriteSingleton(); 
			leftFace = SpriteSingleton.getInstance(); 
			var sprite = leftFace.Get("leftSide"); 
			sprite.Position = scene.Camera.CalcBounds().Center; 
			sprite.CenterSprite(); 
			//sprite.Scale = new Vector2(1,1); 
			scene.AddChild(sprite); 

	
  		 Director.Instance.RunWithScene(scene,true);
    		string spriteName;

      		spriteName= "leftSide";
      
     		sprite.TileIndex2D = leftFace.Get (spriteName).TileIndex2D;
			sprite.RunAction(new TintTo (new Vector4(1.0f,0.0f,0.0f,1.0f),0.1f)); 	
			var cube = new Cube(); 
			cube.card1("r"); 
			cube.card2 ("b"); 
			cube.card3("p"); 
			
			var cubeTest = cube.testCube(); 
			Console.WriteLine(cubeTest); 
		
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

