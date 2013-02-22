using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QColor : Quality
	{
		public QColor()
		{
		}
		
		public override void ApplyQuality (Node host, uint val)
		{
			switch(val) 
 			{
 				case (int)VALUE.ONE: 
 				//pink
//					host.Color = new Vector4(0.96f,0.88f,0.88f,1.0f);
					host.RunAction(new TintTo (new Vector4(0.96f,0.88f,0.88f,1.0f),0.1f));
 					break;
 				case (int)VALUE.TWO:
 				//red
//					host.Color = new Vector4(0.90f,0.075f,0.075f,1.0f);
					host.RunAction(new TintTo (new Vector4(0.90f,0.075f,0.075f,1.0f),0.1f));
 					break;
 				case (int)VALUE.THREE: 	
 				//teal
//					host.Color = new Vector4(0.16f,0.88f,0.88f,1.0f);
					host.RunAction(new TintTo (new Vector4(0.16f,0.88f,0.88f,1.0f),0.1f));
 					break; 
 				default:
 					break; 
 			}
		}
	}
}
