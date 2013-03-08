using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
	public class FontManager
	{
		protected Dictionary<string, UIFont> fontDict;
		
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
	}
}

