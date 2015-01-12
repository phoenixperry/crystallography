using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class Strike : Node
	{
		private static readonly TextureInfo BAD_STRIKE_TEXTUREINFO = Support.SpriteFromFile("/Application/assets/images/UI/x.png").TextureInfo;
		private static readonly TextureInfo EMPTY_STRIKE_TEXTUREINFO = Support.SpriteFromFile("/Application/assets/images/UI/whitePageIcon.png").TextureInfo;
		private static readonly TextureInfo GOOD_STRIKE_TEXTUREINFO = Support.SpriteFromFile("Application/assets/images/UI/strikeCube.png").TextureInfo;
		
		private SpriteTile Icon;
		
		protected bool _filled;
		
		public bool filled {
			get {
				return _filled;
			}
			set {
				if(_filled == value ) return;
				_filled = value;
				if(!_filled) {
					Icon.TextureInfo = EMPTY_STRIKE_TEXTUREINFO;
					Icon.Quad.S = new Vector2(EMPTY_STRIKE_TEXTUREINFO.Texture.Width, EMPTY_STRIKE_TEXTUREINFO.Texture.Height);
					Icon.Quad.T = -Icon.Quad.S/4.0f;
				}
			}
		}
		public bool isGood { get { return Icon.TextureInfo == GOOD_STRIKE_TEXTUREINFO; } }
		
		public Strike () : base() {
			Initialize();
		}
		
		private void Initialize() {
			Icon = Support.SpriteFromFile("/Application/assets/images/UI/whitePageIcon.png");
			_filled = false;
//			Icon.Pivot = new Vector2(0.5f, 0.5f);
			Icon.RegisterPalette(1);
			this.AddChild(Icon);
		}
		
		public void fill(bool isGood) {
//			if(_filled) return;
			
			_filled = true;
			TextureInfo info;
			if(isGood) {
				info = GOOD_STRIKE_TEXTUREINFO;
			} else {
				info = BAD_STRIKE_TEXTUREINFO;
			}
			Icon.TextureInfo = info;
			Icon.Quad.S = new Vector2(info.Texture.Width, info.Texture.Height);
			Icon.Quad.T = -Icon.Quad.S/4.0f;
		}
		
		public void Reset() {
			_filled = false;
		}
	}
}

