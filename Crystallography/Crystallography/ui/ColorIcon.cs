using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI
{
	static public class ColorIcon {
		
		public static Node Instance {
			get { return _instance ?? Initialize();}
		}
		
		private static Node _instance;
		
		private static Node Initialize() {
			_instance = new Node();
			SpriteTile[] pieces = {
				Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2),
				Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2),
				Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2)
			};
			pieces[0].Position = new Vector2(-2.0f, 19.0f);
			pieces[1].Position = new Vector2(23.0f, -21.0f);
			pieces[2].Position = new Vector2(-25.0f, -21.0f);
			for (int i = 0; i < pieces.Length; i++) {
				pieces[i].TileIndex1D = 1;
				pieces[i].RegisterPalette(i);
				_instance.AddChild(pieces[i]);
			}
			return _instance;
		}
		
		public static void Destroy() {
			_instance.RemoveAllChildren(true);
			_instance = null;
		}
	}
}

