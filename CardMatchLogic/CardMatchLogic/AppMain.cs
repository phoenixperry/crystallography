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
		public static void Main (string[] args)
		{
		
			var cube = new Cube(); 
			cube.card1("r"); 
			cube.card2 ("b"); 
			cube.card3("p"); 
			
			var cubeTest = cube.testCube(); 
			Console.WriteLine(cubeTest); 
			
		}
	
				
	
	}
}
