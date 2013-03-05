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
		
//		protected AbstractQuality _instance;
		
		protected string _name;
		
		
		/// <summary>
		/// IQuality implementors have protected constructors. Access them through their <c>public static Instance</c> variables.
		/// </summary>
		protected AbstractQuality(){
		}
		
		/// <summary>
		/// Apply the specified Variant of this quality to the ICrystallonEntity.
		/// </summary>
		/// <param name='pEntity'>
		/// <c>ICrystallonEntity</c>
		/// </param>
		/// <param name='pVariant'>
		/// <c>int</c> of variant to apply. Probs 0, 1, or 2.
		/// </param>
		public abstract void Apply( ICrystallonEntity pEntity, int pVariant );
		
		/// <summary>
		/// Compare all qualities on all ICrystallonEntities. Returns <c>true</c> if all-same or all-different.
		/// </summary>
		/// <param name='pEntities'>
		/// <c>ICrystallonEntity</c> Array to be analyzed.
		/// </param>
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
