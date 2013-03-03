using System;
using Sce.PlayStation.Core;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QColor : AbstractQuality
	{
//		public static List<Card>[] lists = new List<Card>[3] {new List<Card>(), new List<Card>(), new List<Card>()};
		public static Vector4[] palette = new Vector4[3];
		public static IQuality Instance;
		
		// CONSTRUCTOR ------------------------------------------------------------------
		
		public QColor() : base()
		{
			_name = "QColor";
			Instance = this;
			setPalette( new Vector4(0.96f,0.88f,0.88f,1.0f), // pink
			           new Vector4(0.90f,0.075f,0.075f,1.0f), // red
			           new Vector4(0.16f,0.88f,0.88f,1.0f) ); // teal
		}
		
		// OVERRIDES --------------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant ) {
			pEntity.getNode().RunAction( new TintTo( palette[pVariant], 0.0f ) );
		}
		
		public override bool Match ( ICrystallonEntity[] pEntities ) {
			return base.Match(pEntities);
//			List<ICrystallonEntity>[] variants = QualityManager.Instance.qualityDict["QColor"];
//			int[] results = {0,0,0};
//			
//			foreach ( ICrystallonEntity e in pEntities ) {
//				for (int i=0; i<variants.Length; i++) {
//					if ( variants[i].Contains(e) ) {
//						results[i]++;
//						break;
//					}
//				}
//			}
//			if ( Array.IndexOf(results, 2) == -1 ) { // Successful match is 1 of each or 3 of one. Any 2s => FAIL
//				return true;
//			} else {
//				return false;
//			}
		}
		
		// METHODS ----------------------------------------------------------------------
		
		public void setPalette( Vector4 pColor1, Vector4 pColor2, Vector4 pColor3 ) {
			palette[0] = pColor1;
			palette[1] = pColor2;
			palette[2] = pColor3;
		}
	}
}
