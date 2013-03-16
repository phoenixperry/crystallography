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
		int spriteOffset=1;
		bool glitchNow=true; 
		string spriteName; 
		public GlitchAnimation ()
		{
		
			testAnimation(); 
	//		kickoffTimer.Reset(); 
	//		timer.Reset(); 
			
		}
			public void testAnimation(){
			a = AnimationGlitchSpriteSingleton.getInstance().Get("1"); 
	 		a.Position = new Vector2(100,100); 
			a.CenterSprite(); 
			this.AddChild(a);	
			
			Scheduler.Instance.ScheduleUpdateForTarget(this,  0,false);
		}
		public override void  Update(float dt){
			Console.WriteLine("kicked off");
				var hold = dt;
				Console.WriteLine(hold); 
			
//					spriteName = spriteOffset.ToString(); 
//					Console.WriteLine(spriteName); 
//					a.Pivot = new Vector2(0.5f, 0.5f); 
//					a.TileIndex2D = AnimationGlitchSpriteSingleton.getInstance().Get(spriteName).TileIndex2D;
//					Console.WriteLine(a.CalcSizeInPixels()); 
////					b.TileIndex2D = AnimationFallSpriteSingleton.getInstance().Get(spriteName).TileIndex2D;
//					if(spriteOffset >= 5) {
//						glitchNow = !glitchNow; 
//						spriteOffset =1; 
//						timer.Reset(); 
//		
//					} 
//					
//					else
//						spriteOffset++; 
						
//					}
//				}
	
		}
		~GlitchAnimation(){}
		}
	}


