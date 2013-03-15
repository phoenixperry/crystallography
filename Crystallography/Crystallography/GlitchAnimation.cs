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
	public class GlitchAnimation:Node
	{	
		SpriteTile a; 
		Timer timer  = new Timer(); 
		Timer kickoffTimer = new Timer(); 
		int spriteOffset=0;
		bool glitchNow=true; 
		
		public GlitchAnimation ()
		{
		
			testAnimation(); 
	//		kickoffTimer.Reset(); 
	//		timer.Reset(); 
			
		}
			public void testAnimation(){
			
			a = AnimationGlitchSpriteSingleton.getInstance().Get("1"); 
	 		a.Position = new Vector2(100,100); 
			this.AddChild(a);	
			Scheduler.Instance.ScheduleUpdateForTarget(this, 0,false);
		}
		void update(float dt){
	
			Console.WriteLine("kicked off");
			while(glitchNow)
			{
				if(timer.Milliseconds() >1000f)
				{
					string spriteName; 
					spriteName = spriteOffset.ToString(); 
					Console.WriteLine(spriteName); 
					
					if(spriteOffset >= 5) {
						glitchNow = !glitchNow; 
						spriteOffset =0; 
						timer.Reset(); 
					} 
					
					else
						spriteOffset++; 
						
					}
				}
		}	
		~GlitchAnimation(){}
		}
	}


