using System;
using System.Linq; 
using System.Xml; 
using System.Xml.Linq;
using System.Collections.Generic; 
using System.IO;

using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QualityManager
	{
		// This is sort of a complex data structure, so here's how it works:
		// qualityDict							:	Dictionary; Keys: names of classes that implement IQuality
		// qualityDict[ "QualityClassName" ]	:	Array of Length == number of possible variations for QualityClassName (usually 3)
		// qualityDict[ "QualityClassName" ][0]	:	List of all CardCrystallonEntities with variation 0 of QualityClassName
		public Dictionary<string, List<ICrystallonEntity>[]> qualityDict;
		
		private XDocument doc;
		
		// GET & SET ------------------------------------------------------------------------------------------------
		
		public static QualityManager Instance { get; private set; }
		
		// CONSTRUCTOR -----------------------------------------------------------------------------------------------
		
		public QualityManager() {
			if ( Instance == null ) {
				Instance = this;
			}
			if ( qualityDict == null ) {
				qualityDict = new Dictionary<string, List<ICrystallonEntity>[]>();
			}
		}
		
		// METHODS ---------------------------------------------------------------------------------------------------
		
		private void ApplyQualities() {
			foreach ( string key in qualityDict.Keys ) {
				var type = Type.GetType( "Crystallography." + key );
				var quality = (IQuality)Activator.CreateInstance(type);
				var variations = qualityDict[key];
				for ( int i=0; i<variations.Length; i++ ) {
					var cardList = variations[i];
					if ( cardList != null ) {
						foreach ( var card in cardList ) {
							//TODO Apply the quality to the card
							quality.Apply(card, i);
						}
					}
				}	
			}
		}
		
		private void BuildQualityDictionary( CardManager pCardManagerInstance ) {
			// --------------------------------------------------------------------- FACTORY
			
			// --------------------------------------------------------------------- STEP 1: Separate the card data into useful chunks
			var cards = from data
				in doc.Descendants("Card")
				select new { AllQualities = data.Elements() };
			// --------------------------------------------------------------------- STEP 2: Make as many cards as the level data says
			foreach ( var card in cards ) {
				CardCrystallonEntity cardEntity = pCardManagerInstance.spawn();
				// ----------------------------------------------------------------- STEP 3: Gather the qualities of the card
				foreach (XElement singleQuality in card.AllQualities) {
					string name = singleQuality.Attribute("Name").Value;
					int val = (int)singleQuality.Attribute("Value");
					if( qualityDict.ContainsKey(name) == false ) {	// ------------- New quality type discovered; add dict entry
						qualityDict.Add( name, new List<ICrystallonEntity>[3] );
					}
					if ( qualityDict[name][val] == null ) {	// --------------------- New quality variation discovered; add list entity
						qualityDict[name][val] = new List<ICrystallonEntity>();
					}
					qualityDict[name][val].Add(cardEntity);	// --------------------- STEP 4: Note which variation of this quality the card has
					// TEST
//					Console.WriteLine ("Name: " + name + " Value: " + val);
					// END TEST
				}
			}	// ----------------------------------------------------------------- STEP 5: Done.

			// TEST
//			foreach (string key in qualityDict.Keys) {
//				for (int i=0; i < qualityDict[key].Length; i++) {
//					if ( qualityDict[key][i] != null ) {
//						Console.WriteLine(key + " : " + i + " : " + string.Join(", ", qualityDict[key][i]));
//					}
//				}
//			}
			// END TEST
		}
		
		private void ClearQualityDictionary() {
			foreach( List<ICrystallonEntity>[] quality in qualityDict.Values ) {
				foreach (  List<ICrystallonEntity> variant in quality ) {
					if (variant != null) {
						variant.Clear();
					}
				}
//				quality = null;
			}
			qualityDict.Clear();
		}
		
		public bool EvaluateMatch( ICrystallonEntity[] pEntities ) {
			bool valid = true;
			foreach ( string key in qualityDict.Keys ) {
				var variations = qualityDict[key];
				if ( variations[0] == null || variations[1] == null || variations[2] == null ) {
					continue;  // -------------------------------- Quality has no variations in this level; don't bother.
				} else {
					var type = Type.GetType( "Crystallography." + key );
					var quality = (IQuality)Activator.CreateInstance( type );
					valid = quality.Match( pEntities );
					if ( valid == false ) {
						return valid;
					}
				}
			}
			return valid;
		}
		
		private void LoadLevelQualities( int pLevelNum ) {
			FileStream fileStream = File.OpenRead( "/Application/assets/levels/level_" + pLevelNum + ".xml" );
			StreamReader fileStreamReader = new StreamReader( fileStream );
			string xml = fileStreamReader.ReadToEnd();
			fileStreamReader.Close();
			fileStream.Close();
			doc = XDocument.Parse( xml );
		}
		
		public void Reset( CardManager pCardManagerInstance, int pLevelNum ) {
//			if( Instance == null ) {
//				Instance = new QualityManager();
//			}
			ClearQualityDictionary();
			LoadLevelQualities( pLevelNum );
			BuildQualityDictionary( pCardManagerInstance );
			ApplyQualities();
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------------
		
		~QualityManager() {
			Instance = null;
			ClearQualityDictionary();
			qualityDict = null;
			doc = null;
		}
	}
}