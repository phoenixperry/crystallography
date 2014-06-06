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
		protected static QualityManager _instance;
		
		/// <summary>
		/// This is sort of a complex data structure, so here's how it works:
		/// qualityDict							:	Dictionary; Keys: names of classes that implement <c>IQuality</c>
		/// qualityDict[ "QualityClassName" ]	:	Array of Length == number of possible variations for QualityClassName (usually 3)
		/// qualityDict[ "QualityClassName" ][0]	:	List of all CardCrystallonEntities with variation 0 of QualityClassName
		/// </summary>
		public Dictionary<string, List<int>[]> qualityDict;
		public Dictionary<AbstractQuality, bool> allSameScoreDict;
		
		public Dictionary<int,int> countdownDict;
		
		public List<string> scoringQualityList;
		
		/// <summary>
		/// XML data describing qualities for all the cards in this level.
		/// </summary>
		private XDocument doc;
		
		public static event EventHandler<MatchScoreEventArgs> MatchScoreDetected;
		public static event EventHandler<FailedMatchEventArgs> FailedMatchDetected;
		
		// GET & SET ------------------------------------------------------------------------------------------------
		
		public static QualityManager Instance { 
			get {
				if( _instance == null) {
					return _instance = new QualityManager();
				}
				return _instance;
			}
			private set{
				_instance = value;
			}
		}
		
		// CONSTRUCTOR -----------------------------------------------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.QualityManager"/> class.
		/// </summary>
		protected QualityManager() {
			Instance = this;
			qualityDict = new Dictionary<string, List<int>[]>();
			allSameScoreDict = new Dictionary<AbstractQuality, bool>();
			countdownDict = new Dictionary<int, int>();
			scoringQualityList = new List<string>();
			
			InputManager.Instance.CrossJustUpDetected += delegate {
//				CountdownTick();
				ScrambleQuality( "QColor" );
			};
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
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
			if (qualityDict[pQualityName][pVariant] == null) {
				qualityDict[pQualityName][pVariant] = new List<int>();
			}
			qualityDict[pQualityName][pVariant].Add( pEntity.id );
		}
		
		public void AddScoringQuality( string pQuality ) {
			if ( false == QualityManager.Instance.scoringQualityList.Contains(pQuality) ) {
				QualityManager.Instance.scoringQualityList.Add(pQuality);
				if( pQuality == "QSound") {
					LevelManager.Instance.SoundGlow = true;
				}
			}
		}
		
		public void RemoveScoringQuality( string pQuality ) {
			QualityManager.Instance.scoringQualityList.Remove( pQuality );
			if( pQuality == "QSound") {
				LevelManager.Instance.SoundGlow = false;
			}
		}
		
		/// <summary>
		/// Applies qualities to all cards as prescribed in <c>qualityDict</c>
		/// </summary>
//		private void ApplyQualities() {
//			foreach ( string key in qualityDict.Keys ) {
//				if ( !AppMain.ORIENTATION_MATTERS) {
//					if ( key == "QOrientation" ) {
//						continue;
//					}
//				}
//				var type = Type.GetType( "Crystallography." + key );
//				var quality = (IQuality)type.GetProperty("Instance").GetValue(null, null);
//				var variations = qualityDict[key];
//				for ( int i=0; i<variations.Length; i++ ) {
//					var cardList = variations[i];
//					if ( cardList != null ) {
//						foreach ( var id in cardList ) {
//							quality.Apply(CardManager.Instance.getCardById(id), i);
//						}
//					}
//				}	
//			}
//		}
		
		public void ApplySingleQualityToEntity( string pQualityName, CardCrystallonEntity pEntity ) {
			if (qualityDict.ContainsKey(pQualityName) == false) return;
			
			var type = Type.GetType( "Crystallography." + pQualityName );
			var quality = (IQuality)type.GetProperty("Instance").GetValue(null, null);
			var variations = qualityDict[pQualityName];
			for ( int i=0; i<variations.Length; i++ ) {
				if ( variations[i] == null ) { continue; }
				if ( (variations[i] as IList<int>).Contains (pEntity.id) ) {
					quality.Apply( pEntity , i );
					break;
				}
			}
		}
		
		/// <summary>
		/// Applies all qualities listed in the level definition sheet to a specific card.
		/// </summary>
		/// <param name='pEntity'>
		/// The specified Card
		/// </param>
		public void ApplyQualitiesToEntity( CardCrystallonEntity pEntity ) {
			//TODO would be nice to support entities other than cards...
			foreach ( string key in qualityDict.Keys ) {
				if ( !AppMain.ORIENTATION_MATTERS ) {
					if ( key == "QOrientation" ) {
						continue;
					}
				}
				ApplySingleQualityToEntity( key, pEntity );
//				var type = Type.GetType( "Crystallography." + key );
//				var quality = (IQuality)type.GetProperty("Instance").GetValue(null, null);
//				var variations = qualityDict[key];
//				for ( int i=0; i<variations.Length; i++ ) {
//					if ( variations[i] == null ) { continue; }
//					if ( (variations[i] as IList<int>).Contains (pEntity.id) ) {
//						quality.Apply( pEntity , i );
//						break;
//					}
//				}
			}
			// the countdown quality is formatted differently and needs to be iterated on its own.
			if ( countdownDict.Keys.Count > 0 ) {
				var c = countdownDict[pEntity.id];
				if ( c != 0 ) {
					QCountdown.Instance.Apply(pEntity, c);
				}
			}
		}
		
		/// <summary>
		/// Applies the array of qualities to entity.
		/// </summary>
		/// <param name='pQualityArray'>
		/// P array of literal quality names e.g. "QColor"
		/// </param>
		/// <param name='pEntity'>
		/// P the card to apply the qualities to
		/// </param>
		public void ApplyQualitiesToEntity( List<string> pQualityList, CardCrystallonEntity pEntity) {
//			var len = pQualityList.Count;
			foreach ( string quality in pQualityList) {
				if ( !AppMain.ORIENTATION_MATTERS ) {
					if ( quality == "QOrientation" ) {
						continue;
					}
				}
				ApplySingleQualityToEntity( quality, pEntity);
			}
//			for ( var i=0; i < len; i++ ) {
//				if ( !AppMain.ORIENTATION_MATTERS ) {
//					if ( pQualityArray[i] == "QOrientation" ) {
//						continue;
//					}
//				}
//				ApplySingleQualityToEntity( pQualityArray[i], pEntity);
//			}
		}
		
		/// <summary>
		/// Builds <c>qualityDict</c>
		/// </summary>
		public void BuildQualityDictionary() {
			// --------------------------------------------------------------------- FACTORY
			
			// --------------------------------------------------------------------- STEP 1: Separate the card data into useful chunks
			var cards = from data
				in doc.Descendants("Card")
				select new { AllQualities = data.Elements() };
			// --------------------------------------------------------------------- STEP 2: Make as many cards as the level data says
			int id = CardManager.Instance.NextId;
			int count = 0;
			foreach ( var card in cards ) {
				// ----------------------------------------------------------------- STEP 3: Gather the qualities of the card
				foreach (XElement singleQuality in card.AllQualities) {
					string name = singleQuality.Attribute("Name").Value;
					int val = (int)singleQuality.Attribute("Value");
					if (name == "QCountdown") {
						if ( val != 0 ) {
							countdownDict[id] = val;
						}
					} else {
						if( qualityDict.ContainsKey(name) == false ) {	// ------------- New quality type discovered; add dict entry
							qualityDict.Add( name, new List<int>[3] );
						}
						if ( qualityDict[name][val] == null ) {	// --------------------- New quality variation discovered; add list entity
							qualityDict[name][val] = new List<int>();
						}
						qualityDict[name][val].Add(id);	// --------------------- STEP 4: Note which variation of this quality the card has
					}
					// TEST
//					Console.WriteLine ("Name: " + name + " Value: " + val);
					// END TEST
				}
				count++;
				id++;
			}	// ----------------------------------------------------------------- STEP 5: Done.
			CardManager.Instance.TotalCardsInDeck = count;
			scoringQualityList = qualityDict.Keys.ToList();
			scoringQualityList.Remove("QOrientation");

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
		
		private void CountdownTick() {
			foreach (int id in countdownDict.Keys) {
				CardCrystallonEntity c = CardManager.Instance.getCardById(id);
				QCountdown.Instance.Tick(c);
			}
		}
		
		/// <summary>
		/// Clears <c>qualityDict</c> correctly.
		/// </summary>
		public void ClearQualityDictionary() {
			foreach( List<int>[] quality in qualityDict.Values ) {
				foreach (  List<int> variant in quality ) {
					if (variant != null) {
						variant.Clear();
					}
				}
			}
			qualityDict.Clear();
			countdownDict.Clear();
			scoringQualityList.Clear();
		}
		
		/// <summary>
		/// Checks for all-same or all-different qualities in a group
		/// </summary>
		/// <returns>
		/// Whether it matches.
		/// </returns>
		/// <param name='pGroup'>
		/// The group to be checked.
		/// </param>
		/// <param name='pForScore'>
		/// Whether this is for points or not
		/// </param>
		public bool CheckForMatch( GroupCrystallonEntity pGroup, bool pForScore = true ) {
//			if (pGroup.population < pGroup.maxPopulation) {
//				return false;
//			}
			if ( EvaluateMatch( pGroup.members, pForScore ) ) {
				if ( pForScore ) {
					MatchScoreEventArgs scoreArgs = CalculateMatchScore( allSameScoreDict );
					scoreArgs.Node = pGroup.pucks[0].Children.First();
					scoreArgs.Entity = pGroup;
					
					EventHandler<MatchScoreEventArgs> handler = MatchScoreDetected;
					if ( handler != null ) {
						handler( this, scoreArgs );
					}
				}
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Checks an arbitrary array of cards to see whether any matches are still possible.
		/// </summary>
		/// <returns>
		/// Whether any matches are possible
		/// </returns>
		/// <param name='pCards'>
		/// The array of cards
		/// </param>
		/// <param name='pForScore'>
		/// Whether this is for points or not
		/// </param>
		public bool CheckForMatch( CardCrystallonEntity[] pCards, bool pForScore = false ) {
			int len = pCards.Length;
			if (len < SelectionGroup.MAX_CAPACITY) {
				return false;
			}
			
			CardCrystallonEntity[] triad = new CardCrystallonEntity[SelectionGroup.MAX_CAPACITY];
			for (int i=0; i < len-2; i++) {
				for (int j=i+1; j < len-1; j++) {
					for (int k=j+1; k < len; k++) {
						triad[0] = pCards[i];
						triad[1] = pCards[j];
						triad[2] = pCards[k];
						if (EvaluateMatch( triad, pForScore ) ) {
							if ( pForScore ) {
								MatchScoreEventArgs scoreArgs = CalculateMatchScore( allSameScoreDict );
								scoreArgs.Node = pCards[0].getNode();
								scoreArgs.Entity = pCards[0];
								
								EventHandler<MatchScoreEventArgs> handler = MatchScoreDetected;
								if ( handler != null ) {
									handler( this, scoreArgs );
								}
							}
							return true;
						}
					}
				}
			}
			return false;
		}
		
		/// <summary>
		/// A light-weight match finder for when we don't care about points.
		/// </summary>
		/// <returns>
		/// If there is at least ONE possible match in the array of cards.
		/// </returns>
		/// <param name='pCardIds'>
		/// An array of card IDs.
		/// </param>
		/// /// <param name='pHitMeCount'>
		/// Number of offscreen cards being included
		/// </param>
//		public bool CheckForMatch( int[] pCardIds, int[] pHitMeCards ) {
		public bool CheckForMatch( int[] pCardIds, int pHitMeCount) {
			int len = pCardIds.Length;
			if (len >= SelectionGroup.MAX_CAPACITY) {
				
				int[] triad = new int[SelectionGroup.MAX_CAPACITY];
				for (int i=0; i < len-2; i++) {
					for (int j=i+1; j < len-1; j++) {
						for (int k=j+1; k < len; k++) {
							triad[0] = pCardIds[i];
							triad[1] = pCardIds[j];
							triad[2] = pCardIds[k];
							if( EvaluateMatch(triad) ) { // ------- as soon as we find one legal cube...
#if DEBUG
								Console.WriteLine("First Match Found: {0}, {1}, {2}", pCardIds[i], pCardIds[j], pCardIds[k]);
								if( pHitMeCount > 0 ) {
									int hitIndex = len - pHitMeCount;
									if( k >= hitIndex ) {
										Console.WriteLine("Hit Me Required: {0}", pCardIds[k]);
									}
									if( j >= hitIndex ) {
										Console.WriteLine("Hit Me Required: {0}", pCardIds[j]);
									}
									if( i >= hitIndex ) {
										Console.WriteLine("Hit Me Required: {0}", pCardIds[i]);
									}
								}
#endif
								return true;
							}
						}
					}
				}
			}
			return false;
		}
		
		/// <summary>
		/// A light-weight match evaluator.
		/// </summary>
		/// <returns>
		/// If these three cards form a legal cube.
		/// </returns>
		/// <param name='pIds'>
		/// An array of card IDs to be evaluated.
		/// </param>
		private bool EvaluateMatch( int[] pIds ) {
//			bool failed = false;
			foreach ( string key in qualityDict.Keys ) { // ----------- for each quality
				if ( key == "QOrientation" && AppMain.ORIENTATION_MATTERS == false ) {
					continue;
				}
				var variations = qualityDict[key];
				for( int i=0; i < variations.Length; i++) { // -------- for each variation of the quality
					if ( variations[i] == null ) {
						continue;
					}
					int count = 0;
					for (int j=0; j<pIds.Length; j++ ) { 
						if( variations[i].Contains(pIds[j]) ) { // ---------- check for each Id
							count++;
						}
					}
					if (count == 2) { // ------------------------------ if there are exactly 2 cards, this isn't a legal cube.
						return false;
					}
					if (count != 1 && key == "QOrientation") { // ---- if orientation isn't unique
						return false;
					}
				}
			}
			return true;
		}
		
		/// <summary>
		/// Returns whether the qualities possessed by the Array of <c>ICrystallonEntity</c> are all-same-or-all-different.
		/// </summary>
		/// <param name='pEntities'>
		/// Array of <see cref="Crystallography.ICrystallonEntity"/>
		/// </param>
		/// <param name='pForScore'>
		/// Is this to score points, or just to test whether matches are still possible? Default = <c>false</c>.
		/// </param>
//		private bool EvaluateMatch( ICrystallonEntity[] pEntities, out Dictionary<AbstractQuality, bool> pQDict, bool pForScore) {
		private bool EvaluateMatch( ICrystallonEntity[] pEntities, bool pForScore) {
			// TODO this whole function is inefficient... revisit later.
			
			int score;
			bool failed = false;
			FailedMatchEventArgs.Names.Clear();
			FailedMatchEventArgs failArgs = new FailedMatchEventArgs();
			EventHandler<FailedMatchEventArgs> failHandler;
			allSameScoreDict.Clear();
			
			foreach ( string key in scoringQualityList ) {
				var variations = qualityDict[key];
				var type = Type.GetType( "Crystallography." + key );
				var quality = (AbstractQuality)type.GetProperty("Instance").GetValue(null, null);
				if (   (variations[0] == null && variations[1] == null) 
				    || (variations[1] == null && variations[2] == null) 
				    || (variations[0] == null && variations[2] == null ) ) {	// no variations of this quality on any card in this entire level
					continue;
				} else {
					score = quality.Match( pEntities, pForScore );
					if ( score == 0 ) {	//------------------------------------------------------------ No match. Just quit.
						if (pForScore) { //----------------------------------------------------------- This is a player-triggered Match Evaluation that has failed
							FailedMatchEventArgs.Names.Add(key.ToString().Substring(1),1); //                      as opposed to one from CardManager.MatchesPossible()
						}
						failed = true;
					} else {	// ------------------------------------------------------------------- Is it an All-Same match?
						allSameScoreDict.Add ( quality, (score == quality.allSameScore) ) ;
					}
				}
			}
			if (failed) {
				failArgs.Entity = pEntities[0];
				failHandler = FailedMatchDetected;
				if (failHandler != null) {
					failHandler( this, failArgs);
				}
				return false;
			}
			return true;
		}
		
		/// <summary>
		/// Calculates the point value of a successful match.
		/// </summary>
		/// <returns>
		/// Event Arguments for a MatchScore event
		/// </returns>
		/// <param name='pQDict'>
		/// A dict of qualities the cards share
		/// </param>
		private MatchScoreEventArgs CalculateMatchScore( Dictionary<AbstractQuality, bool> pQDict) {
			int score = LevelManager.Instance.Bonus;
			MatchScoreEventArgs scoreArgs = new MatchScoreEventArgs();
			MatchScoreEventArgs.ScoreQualities.Clear();
			
			foreach ( AbstractQuality key in pQDict.Keys ) {
				string name = key.ToString().Substring(17);
				if ( scoringQualityList.Contains("Q" + name) ) {
					var thisQualityScore = key.Score( pQDict[key] );
					MatchScoreEventArgs.ScoreQualities.Add(name, thisQualityScore );
					score += thisQualityScore;
				}
			}
			scoreArgs.Points = score;
			return scoreArgs;
		}
		
		/// <summary>
		/// Loads the level qualities from the appropriate file and puts the XML data in <c>doc</c>.
		/// </summary>
		/// <param name='pLevelNum'>
		/// <c>int</c> Which level to load
		/// </param>
		public void LoadLevelQualities( int pLevelNum ) {
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
			foreach ( List<int> list in qualityDict[pQualityName] ) {
				if ( list == null ) {
					continue;
				}
				if( list.Remove(pEntity.id) ) {
					return;
				}
			}
		}
		
		/// <summary>
		/// Removes an entity from all qualities in <c>qualityDict</c>
		/// </summary>
		/// <param name='pEntity'>
		/// The Entity
		/// </param>
		public void RemoveAll( AbstractCrystallonEntity pEntity ) {
			var allQualities = qualityDict.Keys.ToList();
			foreach( var quality in allQualities ) {
				Remove (pEntity, quality);
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
		public void Reset( int pLevelNum ) {
			//TODO Keep this from being called both BEFORE and AFTER level transition. Pick ONE.
			ClearQualityDictionary();
			LoadLevelQualities( pLevelNum );
			QColor.Instance.setPalette();
			QPattern.Instance.setPalette();
			BuildQualityDictionary();
		}
		
		public void ScrambleQuality( string pQualityName ) {
			//take a particular quality and randomly reassign entities within the quality
			var type = Type.GetType( "Crystallography." + pQualityName );
			var quality = (IQuality)type.GetProperty("Instance").GetValue(null,null);
			foreach ( CardCrystallonEntity c in CardManager.availableCards ) {
				Remove (c, pQualityName);
				int index = (int)Math.Floor( GameScene.Random.NextFloat() * 3 );
				Add ( c, pQualityName, index);
				quality.Apply( c, index );
			}
		}
		
		/// <summary>
		/// Apply a particular variant of a particular quality to an entity. Also removes a previous variant of that quality, if it exists.
		/// </summary>
		/// <param name='pEntity'>
		/// <see cref="Crystallography.AbstractCrystallonEntity"/>
		/// </param>
		/// <param name='pQualityName'>
		/// <c>string</c> Class name for this quality. Must start with a capitol Q.
		/// </param>
		/// <param name='pVariant'>
		/// <c>int</c> Index of the variant. Probs 0, 1, or 2.
		/// </param>
		public void SetQuality ( AbstractCrystallonEntity pEntity, string pQualityName, int pVariant ) {
			var type = Type.GetType( "Crystallography." + pQualityName );
			var quality = (IQuality)type.GetProperty("Instance").GetValue(null, null);
			Remove( pEntity, pQualityName );
			Add( pEntity, pQualityName, pVariant );
			quality.Apply( pEntity, pVariant );
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------------
#if DEBUG
		~QualityManager() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
	
	// HELPER CLASSES ------------------------------------------------------------------------------------------------
	
	public class MatchScoreEventArgs : EventArgs {
		public static Dictionary<string, int> ScoreQualities = new Dictionary<string, int>();
		public int Points { get; set; }
		public Node Node { get; set; }
		public ICrystallonEntity Entity { get; set; }
	}
	
	public class FailedMatchEventArgs : EventArgs {
//		public static Dictionary<string,int> Names
		public static Dictionary<string,int> Names = new Dictionary<string, int>();
		public ICrystallonEntity Entity { get; set; }
	}
}