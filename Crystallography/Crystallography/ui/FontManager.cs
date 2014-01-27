using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class FontManager
	{
		protected Dictionary<string, UIFont> fontDict;
//		protected Dictionary<string, Font> inGameFontDict;
		protected Dictionary<string, FontMap> fontMapDict;
		
		public string assetRoot;
		
		public static FontManager Instance = new FontManager("Application/assets/fonts/");
		
		// CONSTRUCTOR ---------------------------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.UI.FontManager"/> class.
		/// </summary>
		/// <param name='pAssetRoot'>
		/// local path to /fonts/
		/// </param>
		protected FontManager (string pAssetRoot) {
			assetRoot = pAssetRoot;
			fontDict = new Dictionary<string, UIFont>();
//			inGameFontDict = new Dictionary<string, Font>();
			fontMapDict = new Dictionary<string, FontMap>();
#if DEBUG
			Console.WriteLine(GetType().ToString() + " Created");
#endif
		}
		
		// METHODS -------------------------------------------------------------------------------
		
		/// <summary>
		/// Checks whether we already have the requested font settings in memory, and creates it if not.
		/// </summary>
		/// <param name='pName'>
		/// Name of font
		/// </param>
		/// <param name='pSize'>
		/// Size of font
		/// </param>
		/// <param name='pStyle'>
		/// Regular, Bold, or Italic
		/// </param>
		protected void CheckCache( string pName, int pSize, string pStyle ) {
			string key = pName + pStyle + pSize;
			if (fontDict.ContainsKey( key ) ) {
					return;
			} else {
				var font = new UIFont( assetRoot + pName + "_" + pStyle + ".otf", pSize, FontStyle.Regular );
				fontDict.Add( key, font );
#if DEBUG
				Console.WriteLine ( "UIFont: " + pName + "_" + pStyle + "_" + pSize + " created. " + fontDict.Keys.Count + " members in fontDict." );
#endif
			}
		}
		
		protected void CheckMapCache( string pKey, Font font ) {
			if (fontMapDict.ContainsKey( pKey ) ) {
				return;
			} else {
				var map = new FontMap( font );
				fontMapDict.Add( pKey, map );
			}
		}
		
		/// <summary>
		/// Get the specified UIFont object..
		/// </summary>
		/// <param name='pName'>
		/// Name of font
		/// </param>
		/// <param name='pSize'>
		/// Size of font
		/// </param>
		/// <param name='pStyle'>
		/// defaults to "Regular"
		/// </param>
		public UIFont Get( string pName, int pSize, string pStyle = "Regular" ) {
			CheckCache( pName, pSize, pStyle );
			return fontDict[ pName + pStyle + pSize ];
		}
		
		public Font GetInGame( string pName, int pSize, string pStyle = "Regular" ) {
			CheckCache( pName, pSize, pStyle );
			return fontDict[ pName + pStyle + pSize ].GetFont();
		}
		
		public FontMap GetMap( Font font ) {
			string key = font.Name + font.Size.ToString();
			CheckMapCache( key, font );
			return fontMapDict[ key ];
		}
	}
}

