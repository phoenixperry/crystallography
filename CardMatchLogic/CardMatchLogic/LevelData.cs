using System;

namespace CardMatchLogic
{
	public class LevelData
	{
		public static int CURRENT_LEVEL = 0;
		public static CardData[][] LEVEL_DATA = new CardData[][] { 
			
			// LEVEL 0 -- TEST LEVEL
			new CardData[]{ 
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A, (int)CardData.FACE.LEFT), // <-- card 1
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A,(int)CardData.FACE.RIGHT), // <-- card 2
				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID,(int)CardData.SOUND.A,(int)CardData.FACE.TOP) // <-- card 3 (note that there is no comma)
			},
			// LEVEL 1
//			new CardData[]{
//				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.STRIPE, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.DOT, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.RED, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.WHITE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A),
//				new CardData((int)CardData.COLOR.BLUE, (int)CardData.PATTERN.SOLID, (int)CardData.SOUND.A)
//			}
		};
		Console.WriteLine(CardData[0][1].FACE); 			
//		
		public LevelData()
		{
			//intentionally blank
		}
	}
}	


