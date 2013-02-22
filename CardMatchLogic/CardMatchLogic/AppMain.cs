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
		public  static SpriteSingleton cubeFaces;
		public static  SpriteTile sprite;
		public  static SpriteTile sprite2; 
		public  static SpriteTile sprite3; 
		public static Node cube3;  
		private static Card[] cards; 
		public static GamePhysics _physics;
		private static CardData[] currentLevelData; 
		private static Node holder; 
		private string faceName; 
		
		public static void Main (string[] args)
		{
						Sce.PlayStation.Core.Graphics.GraphicsContext
	 		context = new Sce.PlayStation.Core.Graphics.GraphicsContext(); 
						uint sprites_capacity = 500; 
			uint draw_helpers_capacity=400; 
	
			System.Random rand = new System.Random();
				Director.Initialize(sprites_capacity, 
			                    draw_helpers_capacity, context); 
			Director.Instance.GL.Context.SetClearColor(Colors.Grey20); 
			
			var scene = new Scene(); 
			scene.Camera.SetViewFromViewport(); 
				
			
			Director.Instance.RunWithScene(scene,true);
			_physics = new GamePhysics();
			holder = new Node(); 
			
			var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			currentLevelData = LevelData.LEVEL_DATA[0];
			cards = new Card[currentLevelData.Length];
			for (int i = 0; i < cards.Length; i++) {
				Vector2 start_pos = new Vector2(50f + 0.75f * _screenWidth * (float)rand.NextDouble(), 50f + 0.75f * _screenHeight * (float)rand.NextDouble ());
				cards[i] = new Card(_physics.addCardPhysics(start_pos), currentLevelData[i]);

				holder.AddChild (cards[i]);
			}
			
		scene.AddChild(holder); 
		
			//cubeFaces = new SpriteSingleton(); 
			cubeFaces = SpriteSingleton.getInstance(); 
			sprite = cubeFaces.Get("topSide"); 
			sprite.Name = "topSide"; 
			sprite2 = cubeFaces.Get ("leftSide"); 
			sprite2.Name = "leftSide"; 
			sprite3 = cubeFaces.Get("rightSide"); 
			sprite3.Name = "rightSide";  
			
			Vector2 vectHolder = sprite2.CalcSizeInPixels(); 
			sprite3.Position = new Vector2(vectHolder.X-84, sprite3.Position.Y); 
			sprite.Position = new Vector2(vectHolder.X/4, (sprite.Position.Y + vectHolder.X/2)-12); 
			//sprite3.Position = new Vector2(sprite2.Position.Length, 
			//                               sprite2.Position.Y); 
			//sprite.Position = scene.Camera.CalcBounds().Center;
			//sprite.CenterSprite(); 
			//sprite.Scale = new Vector2(1,1); 
			
			cube3 = new Node(); 
			cube3.AddChild(sprite); 
			cube3.AddChild(sprite2); 
			cube3.AddChild(sprite3); 
			scene.AddChild(cube3); 
			
						
	

			//pink 
			//sprite.RunAction(new TintTo (new Vector4(0.96f,0.88f,0.88f,1.0f),0.1f)); 	
			//red 
			//sprite.RunAction(new TintTo (new Vector4(0.90f,0.075f,0.075f,1.0f),0.1f)); 
			
		
			var spriteName="topSide"; 
			sprite.TileIndex2D = cubeFaces.Get (spriteName).TileIndex2D;
			Console.WriteLine(sprite.TileIndex2D);
			//teal 
			//sprite.RunAction(new TintTo (new Vector4(0.16f,0.88f,0.88f,1.0f),0.1f)); 	

			var cube = new Cube(); 
			cube.card1("red"); 
			cube.card2 ("blue"); 
			cube.card3("blue"); 
		
				while(!Input2.GamePad0.Cross.Press) 
			{	
				var vect = new Vector2(cube3.Position.X +10, cube3.Position.Y +3); 
				cube3.Position = vect; 
				Sce.PlayStation.Core.Environment.SystemEvents.CheckEvents(); 
				Director.Instance.Update(); 
				Director.Instance.Render(); 
				Director.Instance.GL.Context.SwapBuffers(); 
				Director.Instance.PostSwap();
					cube.testCube(); 
			
			}
		}
		
	
	}}

