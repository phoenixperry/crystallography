using System;

namespace Crystallography
{
	public class LevelData
	{
		public static int CURRENT_LEVEL = 1;
		public static CardData[][] LEVEL_DATA = new CardData[][] { 
			
			// LEVEL 0 -- TEST LEVEL
			new CardData[]{ 
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A), // <-- card 1
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A), // <-- card 2
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A) // <-- card 3 (note that there is no comma)
			},
			
			// LEVEL 1
			new CardData[]{ 
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A), // <-- card 1
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A), // <-- card 2
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A) // <-- card 3 (note that there is no comma)
			},
			
			// LEVEL 2
			new CardData[]{
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A)
			},
			
			// LEVEL 3
			new CardData[]{
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A)
			},
			
			// LEVEL 4
			new CardData[]{
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.B),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.C),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.B),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.C),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.B),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.C),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.B),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.C),
			},
			
			// LEVEL 5
			new CardData[]{
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
			},
			
			new CardData[]{
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
				
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
				
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
			}
		};
		
		public LevelData()
		{
			//intentionally blank
		}
	}
}	


