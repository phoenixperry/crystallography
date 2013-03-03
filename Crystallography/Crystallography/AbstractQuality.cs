using System;
using System.Linq; 
using System.Xml; 
using System.Collections.Generic; 
using System.IO;

using Sce.PlayStation.HighLevel.GameEngine2D;

using System.Xml.Linq;

namespace Crystallography
{
	public abstract class AbstractQuality : IQuality
	{
//		public enum VALUE { ONE=0x1, TWO=0x2, THREE=0x4 };
		
		protected string _name;
		
		public AbstractQuality(){
		}
		
		public abstract void Apply( ICrystallonEntity pEntity, int pVariant );
		
		public virtual bool Match( ICrystallonEntity[] pEntities ) {
			List<ICrystallonEntity>[] variants = QualityManager.Instance.qualityDict[_name];
			int[] results = {0,0,0};
			
			foreach ( ICrystallonEntity e in pEntities ) {
				for (int i=0; i<variants.Length; i++) {
					if ( variants[i].Contains(e) ) {
						results[i]++;
						break;
					}
				}
			}
			if ( Array.IndexOf(results, 2) == -1 ) { // Successful match is 1 of each or 3 of one. Any 2s => FAIL
				return true;
			} else {
				return false;
			}
		}
	}
}	