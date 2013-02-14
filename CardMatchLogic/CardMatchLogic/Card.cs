using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace CardMatchLogic
{
	
	public class Card : SpriteUV
	{
		public static SpriteSingleton face; 
		
		
		public Card ()
		{
//				face = new SpriteSingleton(); 
//				face = SpriteSingleton.getInstance();
//				var spriteTile = face.Get("leftSide"); 
//				//this.AddChild(spriteTile); 
//			   Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
		}
		public override void Update (float dt)
        {
		}
		
		
		~Card()
        {
     
        }
	}
}

