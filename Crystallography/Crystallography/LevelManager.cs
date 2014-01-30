using System;
using System.Linq; 
using System.Xml; 
using System.Xml.Linq;
using System.Collections.Generic; 
using System.IO;
using System.Reflection;

using Sce.PlayStation.Core;

namespace Crystallography
{
	public class LevelManager
	{
		protected static readonly int MAX_DIFFICULTY = 3;
		protected static readonly int MIN_DIFFICULTY = 0;
		protected static readonly string[] DELIMITER = new string[]{";;"};
		
		protected static LevelManager _instance;
		
		/// <summary>
		/// XML data describing qualities for all the cards in this level.
		/// </summary>
		private XDocument doc;
		public Dictionary<int, List<int>> goalDict;
		public int difficulty;
		
		protected Dictionary<string, List<LevelEventData>> levelEventDict;
		
		// GET & SET --------------------------------------------------------------
		
		public static LevelManager Instance { 
			get {
				if( _instance == null) {
					return _instance = new LevelManager();
				}
				return _instance;
			}
			private set{
				_instance = value;
			}
		}
		
		public Vector4[] Palette     { get; set; }
		public string PatternPath    { get; set; }
		public string SoundPrefix    { get; set; }
		public bool SoundGlow        { get; set; }
		public int Goal	             { get; set; }
		public int PossibleSolutions { get; set; }
		public int FoundSolutions    { get; set; }
		public int Bonus             { get; set; }
		public string MessageBody    { get; set; }
		public string MessageTitle   { get; set; }
		public int StandardPop       { get; set; }
		public bool HitMeDisabled	 { get; set; }
		public Vector4 SpawnRect	 { get; set; }
		
		// CONSTRUCTORS -----------------------------------------------------------
		
		protected LevelManager (){
			Palette = new Vector4[3];
			goalDict = new Dictionary<int, List<int>>();
			SetToDefault();
			
			UI.GameSceneHud.CubesUpdated += HandleUIGameSceneHudCubesUpdated;
			UI.GameSceneHud.BreaksUpdated += HandleUIGameSceneHudBreaksUpdated;
			
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS ---------------------------------------------------------
		
		void HandleUIGameSceneHudBreaksUpdated (object sender, EventArgs e)
		{
			List<LevelEventData> list;
			
			if ( levelEventDict.ContainsKey("BreaksEqual") ) {
				list = levelEventDict["BreaksEqual"];
				foreach (LevelEventData d in list) {
					if ( GameScene.Hud.BreaksDetected == d.Value ) {
						HandleGenericEvent(d);
					}
				}
			}
		}
		
		void HandleUIGameSceneHudCubesUpdated (object sender, EventArgs e)
		{
			List<LevelEventData> list;
			
			if ( levelEventDict.ContainsKey("CubesEqual") ) {
				list = levelEventDict["CubesEqual"];
				foreach (LevelEventData d in list) {
					if ( GameScene.Hud.Cubes == d.Value ) {
						HandleGenericEvent(d);
					}
				}
			}
		}
		
		// OVERRIDES --------------------------------------------------------------
		
		
		
		// METHODS ----------------------------------------------------------------
		
		private void HandleGenericEvent( LevelEventData d ) {
			string[] args;
			switch(d.Action) {
			case ("UpdateMessagePanel"):
				args = d.Args.Split(DELIMITER, StringSplitOptions.None);
				GameScene.Hud.UpdateMessagePanel( args[0]
				                                , args[1]
				                                , args.Length > 2 ? float.Parse(args[2]) : 1.0f
				                                , args.Length > 3 ? float.Parse(args[3]) : 0.0f 
				                                );
				break;
			case ("AddScoringQuality"):
				QualityManager.Instance.scoringQualityList.Add(d.Args);
				if( d.Args == "QSound") {
					LevelManager.Instance.SoundGlow = true;
				}
				break;
			case ("RemoveScoringQuality"):
				QualityManager.Instance.scoringQualityList.Remove(d.Args);
				if( d.Args == "QSound") {
					LevelManager.Instance.SoundGlow = false;
				}
				break;
			case ("SetPatternPath"):
				PatternPath = d.Args;
				QPattern.Instance.setPalette();
				break;
			case ("SetStandardPop"):
				args = d.Args.Split(DELIMITER, StringSplitOptions.None);
				StandardPop = int.Parse(args[0]);
				if (args.Length > 1) {
					if (args[1] == "populate") CardManager.Instance.Populate(false);
				}
				break;
			case ("SetHitMeDisabled"):
				HitMeDisabled = Boolean.Parse(d.Args);
			break;
			case ("SetBonus"):
				Bonus = int.Parse(d.Args);
				break;
			default:
				Console.WriteLine("UNRECOGNIZED EVENT ACTION: " + d.Value);
				break;
			}
		}
		
		public void GetLevelSettings( int pLevelNumber ) {
			PossibleSolutions = 0;
			goalDict.Clear();
			var levels = from data
				in doc.Elements("GameData")
					select new { AllElements = data.Elements() };
			
			foreach (var level in levels) {
				foreach( XElement element in level.AllElements ) {
					if ( (int)element.Attribute("Value") == pLevelNumber ) {
						int i = 0;
						foreach (XElement line in element.Nodes()) {
							if (line.Name.LocalName == "Color") {
								string hexColor = line.Attribute("Hex").Value;
								Palette[i] = ExtractColor(hexColor);
								i++;
							} else if (line.Name.LocalName == "Pattern") {
								PatternPath = line.Attribute("Path").Value;
							} else if (line.Name.LocalName == "Sound") {
								SoundPrefix = line.Attribute("Prefix").Value;
								if (line.Attribute("Glow") != null) {
									SoundGlow = line.Attribute ("Glow").Value == "true";
								} else {
									SoundGlow = false;
								}
							} else if (line.Name.LocalName == "Goal" ) {
								var cubes = (int)line.Attribute("Cubes");
								var score = (int)line.Attribute("Score");
								if ( goalDict.ContainsKey(cubes) == false ) {
									goalDict.Add( cubes, new List<int>() );
								}
								goalDict[cubes].Add(score);
								PossibleSolutions++;
							} else if (line.Name.LocalName == "OldGoal") {
								Goal = (int)line.Attribute("Value");
							} else if (line.Name.LocalName == "Bonus") {
								Bonus = (int)line.Attribute("Value");
							} else if (line.Name.LocalName == "Message") {
								MessageBody = line.Attribute("Body").Value;
								MessageTitle = line.Attribute("Title").Value;
							} else if (line.Name.LocalName == "StandardPop") {
								StandardPop = (int)line.Attribute("Value");
							} else if (line.Name.LocalName == "HitMeDisabled") {
								HitMeDisabled = line.Attribute("Value").Value == "true";
							} else if (line.Name.LocalName == "SpawnRect") {
								SpawnRect = new Vector4( float.Parse(line.Attribute ("X").Value),
								                         float.Parse(line.Attribute ("Y").Value),
								                         float.Parse(line.Attribute("Width").Value),
								                         float.Parse(line.Attribute("Height").Value)
								                        );
							} else if (line.Name.LocalName == "Orientation") {
								if ( 0 == (int)line.Attribute("Value") ) {
									AppMain.ORIENTATION_MATTERS = false;
								}
							} else if ( line.Name.LocalName == "Event" ) {
								string type = line.Attribute("Type").Value;
								if ( false == levelEventDict.ContainsKey(type) ) {
									levelEventDict[type] = new List<LevelEventData>();
								}
								levelEventDict[type].Add( new LevelEventData() {
									Value = float.Parse( line.Attribute("Value").Value ),
									Action = line.Attribute("Action").Value,
									Args = line.Attribute("Args").Value
								});
							}
						}
						return;
					}
				}
			}
		}
		
		public void GetSolutions( int pLevelNumber ) {
			PossibleSolutions = 0;
			goalDict.Clear();
			var levels = from data
				in doc.Elements("GameData")
					select new { AllElements = data.Elements() };
			foreach (var level in levels) {
				foreach( XElement element in level.AllElements ) {
					if ( (int)element.Attribute("Value") == pLevelNumber ) {
						foreach (XElement line in element.Nodes()) {
							if (line.Name.LocalName == "Goal" ) {
								var cubes = (int)line.Attribute("Cubes");
								var score = (int)line.Attribute("Score");
								if ( goalDict.ContainsKey(cubes) == false ) {
									goalDict.Add( cubes, new List<int>() );
								}
								goalDict[cubes].Add(score);
								PossibleSolutions++;
							}
						}
					}
				}
			}
		}
		
		public void ChangeDifficulty( int pDeltaDiff ) {
			bool up = true;
			if ( pDeltaDiff < 0 ) {
				if ( pDeltaDiff + difficulty < MIN_DIFFICULTY ) {
					pDeltaDiff = MIN_DIFFICULTY - difficulty;	// ENFORCE LOWER CAP
				}
				up = false;
				pDeltaDiff = -pDeltaDiff;
			} else if ( pDeltaDiff + difficulty > MAX_DIFFICULTY ) {
				pDeltaDiff = MAX_DIFFICULTY - difficulty;	// ENFORCE UPPER CAP
			}
			
			for ( var i = 0; i < pDeltaDiff; i++ ) {
				if (up) {
					IncreaseDifficulty();
				} else {
					ReduceDifficulty();
				}
			}
		}
		
		protected void IncreaseDifficulty() {
#if DEBUG
			Console.WriteLine("Increase Difficulty");
#endif
			switch (QualityManager.Instance.scoringQualityList.Count) {
			case 1:
				CardManager.Instance.AddQuality("QPattern");
				difficulty++;
				break;
			case 2:
				CardManager.Instance.AddQuality("QParticle");
				difficulty++;
				break;
			case 3:
				CardManager.Instance.AddQuality("QSound");
				difficulty++;
				break;
			case 4:
			default:
				//no more to add
				break;
			}
		}
		
		protected void ReduceDifficulty() {
#if DEBUG
			Console.WriteLine("Reduce Difficulty");
#endif
			switch (QualityManager.Instance.scoringQualityList.Count) {
			case 4:
				CardManager.Instance.RemoveQuality("QSound");
				difficulty--;
				break;
			case 3:
				CardManager.Instance.RemoveQuality("QParticle");
				difficulty--;
				break;
			case 2:
				CardManager.Instance.RemoveQuality("QPattern");
				difficulty--;
				break;
			case 1:
			default:
				// no more to remove
				break;
			}
		}
		
		public void LoadGameData() {
			FileStream fileStream = File.OpenRead( "/Application/assets/levels/levelData.xml" );
			StreamReader fileStreamReader = new StreamReader( fileStream );
			string xml = fileStreamReader.ReadToEnd();
			fileStreamReader.Close();
			fileStream.Close();
			doc = XDocument.Parse( xml );
		}
		
		protected Vector4 ExtractColor( string hex ) {
			float r = (float)Convert.ToInt32( hex.Substring(0, 2), 16 );
			float g = (float)Convert.ToInt32( hex.Substring(2, 2), 16 );
			float b = (float)Convert.ToInt32( hex.Substring(4, 2), 16 );
			return new Vector4( r/255.0f, g/255.0f, b/255.0f, 1.0f );
		}
		
		public void SetToDefault() {
			AppMain.ORIENTATION_MATTERS = true;
			Palette[0] = new Vector4(0.956863f, 0.917647f, 0.956863f, 1.0f);
			Palette[1] = new Vector4(0.898039f, 0.074510f, 0.074510f, 1.0f);
			Palette[2] = new Vector4(0.160784f, 0.886274f, 0.886274f, 1.0f);
			PatternPath = "Application/assets/images/set1/gamePieces.png";
			SoundPrefix = "stack1";
			Goal = 1;
			PossibleSolutions = 0;
			FoundSolutions = 0;
			Bonus = 0;
			MessageBody = "";
			MessageTitle = "";
			StandardPop = 15;
			HitMeDisabled = false;
			SpawnRect = new Vector4(220.0f, 50.0f, 0.6f * 960.0f, 0.75f * 544.0f);
			
			if ( levelEventDict != null ) {
				foreach (var list in levelEventDict.Values) {
					list.Clear();
				}
				levelEventDict.Clear();
			}
			levelEventDict = new Dictionary<string, List<LevelEventData>>();
			
			if ( GameScene.currentLevel == 999 ) {
				difficulty = 0;
			}
		}
		
		public void Reset() {
			SetToDefault();
		}
		
		// DESTRUCTOR ----------------------------------------------------------------------------------
#if DEBUG
		~LevelManager(){
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
	
	// HELPER CLASS ---------------------------------------------------------------------------------------

	public class LevelEventData {
		public float Value;
		public string Action;
		public string Args;
	}
}

