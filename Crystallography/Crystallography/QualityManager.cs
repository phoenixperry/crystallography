using System;
using System.Linq; 
using System.Xml; 
using System.Xml.Linq;
using System.Collections.Generic; 
using System.IO;
using System.Reflection;

using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QualityManager
	{
		/// <summary>
		/// This is sort of a complex data structure, so here's how it works:
		/// qualityDict							:	Dictionary; Keys: names of classes that implement <c>IQuality</c>
		/// qualityDict[ "QualityClassName" ]	:	Array of Length == number of possible variations for QualityClassName (usually 3)
		/// qualityDict[ "QualityClassName" ][0]	:	List of all CardCrystallonEntities with variation 0 of QualityClassName
		/// </summary>
		public Dictionary<string, List<ICrystallonEntity>[]> qualityDict;
		
		/// <summary>
		/// XML data describing qualities for all the cards in this level.
		/// </summary>
		private XDocument doc;
		
		// GET & SET ------------------------------------------------------------------------------------------------
		
		public static QualityManager Instance { get; private set; }
		
		// CONSTRUCTOR -----------------------------------------------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.QualityManager"/> class.
		/// </summary>
		public QualityManager() {
			if ( Instance == null ) {
				Instance = this;
			}
			if ( qualityDict == null ) {
				qualityDict = new Dictionary<string, List<ICrystallonEntity>[]>();
			}
		}
		
		// METHODS ---------------------------------------------------------------------------------------------------
		
		/// <summary>
		/// Add an entity to <c>qualityDict</c>
		/// </summary>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.AbstractCrystallonEntity"/>
		/// </param>
		/// <param name='pQualityName'>
		/// <c>string</c> Class name for this quality.
		/// </param>
		/// <param name='pVariant'>
		/// <c>int</c> Index of the variant. Probs 0, 1, or 2.
		/// </param>
		private void Add( AbstractCrystallonEntity pEntity, string pQualityName, int pVariant ) {
			qualityDict[pQualityName][pVariant].Add( pEntity );
		}
		
		/// <summary>
		/// Applies qualities to all cards as prescribed in <c>qualityDict</c>
		/// </summary>
		private void ApplyQualities() {
			foreach ( string key in qualityDict.Keys ) {
				if ( key == "QOrientation" && GameScene.ORIENTATION_MATTERS == false ) {
					continue;
				}
				var type = Type.GetType( "Crystallography." + key );
				var quality = (IQuality)type.GetProperty("Instance").GetValue(null, null);
				var variations = qualityDict[key];
				for ( int i=0; i<variations.Length; i++ ) {
					var cardList = variations[i];
					if ( cardList != null ) {
						foreach ( var card in cardList ) {
							quality.Apply(card, i);
						}
					}
				}	
			}
		}
		
		/// <summary>
		/// Builds <c>qualityDict</c>
		/// </summary>
		/// <param name='pCardManagerInstance'>
		/// Instance of <c>CardManager</c> with an assembled list of cards for this level.
		/// </param>
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
		
		/// <summary>
		/// Clears <c>qualityDict</c> correctly.
		/// </summary>
		private void ClearQualityDictionary() {
			foreach( List<ICrystallonEntity>[] quality in qualityDict.Values ) {
				foreach (  List<ICrystallonEntity> variant in quality ) {
					if (variant != null) {
						variant.Clear();
					}
				}
			}
			qualityDict.Clear();
		}
		
		/// <summary>
		/// Returns whether the qualities possessed by the Array of <c>ICrystallonEntity</c> are all-same-or-all-different.
		/// </summary>
		/// <param name='pEntities'>
		/// Array of <see cref="Crystallography.ICrystallonEntity"/>
		/// </param>
		public bool EvaluateMatch( ICrystallonEntity[] pEntities ) {
			bool valid = true;
			foreach ( string key in qualityDict.Keys ) {
				if ( key == "QOrientation" && GameScene.ORIENTATION_MATTERS == false ) {
					continue;	// -------------------------------- Orientation doesn't matter; don't bother.
				}
				var variations = qualityDict[key];
				if ( variations[0] == null || variations[1] == null || variations[2] == null ) {
					continue;	// -------------------------------- This quality has no variations in this level; don't bother.
				} else {
					var type = Type.GetType( "Crystallography." + key );
//					var quality = (IQuality)Activator.CreateInstance( type );
					var quality = (IQuality)type.GetProperty("Instance").GetValue(null, null);
					valid = quality.Match( pEntities );
					if ( valid == false ) {
						return valid;
					}
				}
			}
			return valid;
		}
		
		/// <summary>
		/// Loads the level qualities from the appropriate file and puts the XML data in <c>doc</c>.
		/// </summary>
		/// <param name='pLevelNum'>
		/// <c>int</c> Which level to load
		/// </param>
		private void LoadLevelQualities( int pLevelNum ) {
			FileStream fileStream = File.OpenRead( "/Application/assets/levels/level_" + pLevelNum + ".xml" );
			StreamReader fileStreamReader = new StreamReader( fileStream );
			string xml = fileStreamReader.ReadToEnd();
			fileStreamReader.Close();
			fileStream.Close();
			doc = XDocument.Parse( xml );
		}
		
		/// <summary>
		/// Remove an entity from <c>qualityDict</c>
		/// </summary>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.AbstractCrystallonEntity"/>
		/// </param>
		/// <param name='pQualityName'>
		/// <c>string</c> Class name for this quality.
		/// </param>
		private void Remove( AbstractCrystallonEntity pEntity, string pQualityName ) {
			foreach ( List<ICrystallonEntity> list in qualityDict[pQualityName] ) {
				if( list.Remove(pEntity) ) {
					return;
				}
			}
		}
		
		/// <summary>
		/// Reset the <c>QualityManager</c>
		/// </summary>
		/// <param name='pCardManagerInstance'>
		/// The CardManager instance.
		/// </param>
		/// <param name='pLevelNum'>
		/// The level to load next.
		/// </param>
		public void Reset( CardManager pCardManagerInstance, int pLevelNum ) {
			ClearQualityDictionary();
			LoadLevelQualities( pLevelNum );
			BuildQualityDictionary( pCardManagerInstance );
			ApplyQualities();
		}
		
		/// <summary>
		/// Apply a particular variant of a particular quality to an entity. Also removes a previous variant of that quality, if it exists.
		/// </summary>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.AbstractCrystallonEntity"/>
		/// </param>
		/// <param name='pQualityName'>
		/// <c>string</c> Class name for this quality.
		/// </param>
		/// <param name='pVariant'>
		/// <c>int</c> Index of the variant. Probs 0, 1, or 2.
		/// </param>
		public void SetQuality ( AbstractCrystallonEntity pEntity, string pQualityName, int pVariant ) {
			var type = Type.GetType( "Crystallography." + pQualityName );
//			var quality = (IQuality)Activator.CreateInstance( type );
			var quality = (IQuality)type.GetProperty("Instance").GetValue(null, null);
			Remove( pEntity, pQualityName );
			Add( pEntity, pQualityName, pVariant );
			quality.Apply( pEntity, pVariant );
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