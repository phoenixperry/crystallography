using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace CardMatchLogic
{
	public class AppMain
	{
		private static GraphicsContext graphics;
		public CardList cardList = new CardList();
        public String card; 
		public enum CardList{red, pink, blue}
		//1 = red 
		//2 = pink 
		//3 = blue 
		//public static Dictionary dict <string, List<string>>(); 
			
		
			
		public static void Main (string[] args)
		{
			Initialize ();
			Console.WriteLine("pick a card"); 
			
			if(line =="red") {
				 var dict = new Dictionary<string, List<string>> ();
				List<string> possibleMatch = new List<string>(); 
	            dict["redKey"] = new List<string>{"111", "132", "123", 
	                "222"};
				var card= "red";
				
				switch (card) { 
	                case "red":
	                    Console.WriteLine("red");
						possibleMatch = dict["redKey"]; 
						foreach(string pos in possibleMatch){
						Console.WriteLine(pos); }
	                    break;
				default:
					break;
				}
				
				// next card 
				
			}
			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}

		public static void Initialize ()
		{
			// Set up the graphics system
			graphics = new GraphicsContext ();
		}

		public static void Update ()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
		}

		public static void Render ()
		{
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();

			// Present the screen
			graphics.SwapBuffers ();
		}
	}
}
