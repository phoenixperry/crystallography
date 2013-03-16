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
		protected static LevelManager _instance;
		
		/// <summary>
		/// XML data describing qualities for all the cards in this level.
		/// </summary>
		private XDocument doc;
		
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
		
		public Vector4[] Palette { get; set; }
		
		// CONSTRUCTORS -----------------------------------------------------------
		
		public LevelManager (){
			Instance = this;
			Palette = new Vector4[] { new Vector4(0.956863f, 0.917647f, 0.956863f, 1.0f), 
										new Vector4(0.898039f, 0.074510f, 0.074510f, 1.0f), 
										new Vector4(0.160784f, 0.886274f, 0.886274f, 1.0f) };
		}
		
		// METHODS ----------------------------------------------------------------
		
		public void GetLevelSettings( int pLevelNumber ) {
			var levels = from data
				in doc.Elements("GameData")
					select new { AllElements = data.Elements() };
			
			foreach (var level in levels) {
				foreach( XElement element in level.AllElements ) {
					if ( (int)element.Attribute("Value") == pLevelNumber ) {
						int i = 0;
						foreach (XElement color in element.Nodes()) {
							Palette[i].X = (float)color.Attribute("Red");
							Palette[i].Y = (float)color.Attribute("Green");
							Palette[i].Z = (float)color.Attribute("Blue");
							Palette[i].W = 1.0f;
							i++;
						}
						return;
					}
				}
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
		
	}
}

