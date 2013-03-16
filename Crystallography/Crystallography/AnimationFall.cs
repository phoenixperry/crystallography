using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D; 
using Sce.PlayStation.HighLevel.GameEngine2D.Base; 

namespace Crystallography
{
	public class AnimationFall:Node
	{
		public SpriteTile b; 

		Timer timer  = new Timer(); 
		Timer kickoffTimer = new Timer();

		int spriteOffset=1;
		bool glitchNow=true; 
		string spriteName; 
		public AnimationFall ()
		{
			b = AnimationFallSpriteSingleton.getInstance().Get("1");
	 		b.Position = new Vector2(100,100); 
			b.CenterSprite(); 
			this.AddChild(b);
			Scheduler.Instance.ScheduleUpdateForTarget(this,  0,false);
			
		}
		
		public override void  Update(float dt){
			//Console.WriteLine("kicked off");
				var hold = dt;
				var mod = timer.Seconds();
				//Console.WriteLine(mod); 
						
					spriteName = spriteOffset.ToString(); 
//					Console.WriteLine(spriteName); 
					b.Pivot = new Vector2(0.5f, 0.5f); 
				
					//Console.WriteLine(b.CalcSizeInPixels()); 
					b.TileIndex2D =AnimationFallSpriteSingleton.getInstance().Get(spriteName).TileIndex2D; 
					if(spriteOffset >= 11) {
						glitchNow = !glitchNow; 
						spriteOffset =1; 
						timer.Reset(); 
					} 
					
					else
						spriteOffset++; 
						
					}
		
	}
}

