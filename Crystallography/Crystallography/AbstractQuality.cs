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
		public int allSameScore { get; protected set; }
		public int allDifferentScore { get; protected set; }
		
		protected string _name;
		
		
		
		/// <summary>
		/// IQuality implementors have protected constructors. Access them through their <c>public static Instance</c> variables.
		/// </summary>
		protected AbstractQuality(){
			allSameScore = 1;
			allDifferentScore = 3;
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
		/// <param name='pForScore'>
		/// Is this to score points, or just to test whether matches are still possible?
		/// </param>
		public virtual int Match( ICrystallonEntity[] pEntities, bool pForScore ) {
			List<int>[] variants = QualityManager.Instance.qualityDict[_name];
			int[] results = {0,0,0};
			foreach ( ICrystallonEntity e in pEntities ) {
				for (int i=0; i<variants.Length; i++) {
					if ( variants[i].Contains( (e as CardCrystallonEntity).id ) ) {
						results[i]++;
						break;
					}
				}
			}
			if ( Array.IndexOf(results, 2) != -1 ) { // ------- Successful match is 1 of each or 3 of one. Any 2s => FAIL
				return 0;
			} else if ( pForScore ) {
				if (results[0] == 1) { // --------------------- All Different
					return allDifferentScore;
				} else { // ----------------------------------- All Same
					return allSameScore;
				}
			}
			return -1;
		}
		
		public virtual int Score( bool pAllSame ) {
			return pAllSame ? allSameScore : allDifferentScore;
		}
	}
	
	// HELPER CLASSES ------------------------------------------------------
	
}	
