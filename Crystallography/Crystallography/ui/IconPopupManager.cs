using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class IconPopupManager
	{
		protected static IconPopupManager _instance;
		protected Node icon;
		protected Vector2 iconSize;
		
		public static IconPopupManager Instance {
			get {
				if (_instance == null) {
					_instance = new IconPopupManager();
				}
				return _instance;
			}
			private set {
				_instance = value;
			}
		}
		
		protected IconPopupManager () {
			icon = Support.SpriteFromFile("/Application/assets/images/icons/icons.png");
			iconSize = (icon as SpriteTile).CalcSizeInPixels();
//			icon.Pivot = Vector2.One;
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// METHODS --------------------------------------------------------------------
		
//		public void ScoreIcons (ICrystallonEntity pParent, Dictionary<string,int> pQualities) {
//			SpawnIcons (pParent, pQualities, Colors.White);
//		}
		
		public void ScoreIcons (CardCrystallonEntity pParent, Dictionary<string,int> pQualities) {
//			SpawnIcons (pParent, pQualities, Colors.White);
		}
		
		public void ScoreIcons (GroupCrystallonEntity pParent, Dictionary<string,int> pQualities) {
//			SpawnIcons (pParent.members[0], pQualities, Colors.White);
		}
			
		
		public void FailedIcons (ICrystallonEntity pParent, Dictionary<string,int> pQualities) {
			SpawnIcons (pParent, pQualities, Colors.Pink);
		}
		
		
		
		protected void SpawnIcons (ICrystallonEntity pParent, Dictionary<string,int> pQualities, Vector4 pColor) {
			icon = null;
			
			Sequence sequence = new Sequence();
			
			foreach( string key in pQualities.Keys) {
				if (icon != null) {
					continue;
				}
				if(key=="Orientation") {
					continue;
				}
				switch(key){
				case("Color"):
					icon = ColorIcon.Instance;
					break;
				default:
					int ix = (int)EnumHelper.FromString<Crystallography.Icons>(key);
					icon = Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2 );
					(icon as SpriteTile).TileIndex1D = ix;
					(icon as SpriteBase).RegisterPalette((int)FMath.Floor(GameScene.Random.NextFloat()*3));
					break;
				}
			}
			
			icon.Position = pParent.Parent.Parent.Position.Xy - iconSize/2.0f;
			if(icon.Parent != null){
				icon.Parent.RemoveChild(icon, false);
			}
			GameScene.Layers[2].AddChild(icon);
			
			sequence.Add( new DelayTime(0.1f));
			sequence.Add( new CallFunc( () => {icon.Visible = false;}));
			sequence.Add (new DelayTime(0.05f));
			sequence.Add( new CallFunc( () => {icon.Visible = true;}));
			sequence.Add( new DelayTime(0.1f));
			sequence.Add( new CallFunc( () => {icon.Visible = false;}));
			sequence.Add (new DelayTime(0.05f));
			sequence.Add( new CallFunc( () => {icon.Visible = true;}));
			sequence.Add( new DelayTime(0.75f));
			sequence.Add( new CallFunc( () => {icon.Visible = false;}));
			sequence.Add(new CallFunc( () => { 
				icon.Visible = true;
				GameScene.Layers[2].RemoveChild(icon, true);
			}));
			icon.RunAction(sequence);
		}
		
		// OVERRIDES ------------------------------------------------------------------
		
		// DESTRUCTOR ------------------------------------------------------------------------------
#if DEBUG
		~IconPopupManager() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}