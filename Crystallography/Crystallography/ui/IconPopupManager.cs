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
//			Console.WriteLine(pParent.Parent.Parent.ToString());
			
//			icon.Color = Colors.Red.Xyz0;
			
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
//					sequence.Add( new CallFunc( () => {
						icon = ColorIcon.Instance;
//					}));
//					sequence.Add( new DelayTime(1.0f));
					break;
				default:
					int ix = (int)EnumHelper.FromString<Crystallography.Icons>(key);
					icon = Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2 );
					(icon as SpriteTile).TileIndex1D = ix;
					(icon as SpriteBase).RegisterPalette((int)FMath.Floor(GameScene.Random.NextFloat()*3));
//					sequence.Add(new CallFunc( () => { icon.RunAction(new TintTo( Colors.Red, 0.3f )); } ));
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
//			sequence.Add (new DelayTime(0.1f));
//			sequence.Add( new CallFunc( () => {icon.Visible = true;}));
			
//			foreach( string key in pQualities.Keys) {
//				if(key == "Orientation") {
//					continue;
//				}
//				for (int i=0; i<pQualities[key]; i++) {
//					switch(key){
//					case("Color"):
////						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Color", (pParent as CardCrystallonEntity), pColor); } ) );
//						break;
//					case("Animation"):
////						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Animation", (pParent as CardCrystallonEntity), pColor); } ) );
//						break;
//					case("Pattern"):
////						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Pattern", (pParent as CardCrystallonEntity), pColor); } ) );
//						break;
//					case("Particle"):
////						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Particle", (pParent as CardCrystallonEntity), pColor); } ) );
//						break;
//					case("Sound"):
////						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Sound", (pParent as CardCrystallonEntity), pColor); } ) );
//						break;
//					default:
//#if DEBUG
//						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Particle", (pParent as CardCrystallonEntity), Colors.Green); } ) );
//#endif
//						break;
//					}
//					sequence.Add( new DelayTime( 0.05f ) );
//				}
//			}
			
//			sequence.Add(new CallFunc( () => { icon.RunAction(new TintTo(Colors.White.Xyz0, 0.3f)); } ));
//			sequence.Add (new DelayTime(0.25f));
			sequence.Add(new CallFunc( () => { 
				icon.Visible = true;
				GameScene.Layers[2].RemoveChild(icon, true);
			}));
			
			pParent.getNode().RunAction(sequence);
		}
		
		// OVERRIDES ------------------------------------------------------------------
		
//		public override void Update (float dt)
//		{
//			base.Update (dt);
//			Color.A -= dt/1.5f;
//			
//			if ( Color.A < 0) {
//				this.UnscheduleAll();
//				Parent.RemoveChild(this, true);
//			}
//		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
#if DEBUG
		~IconPopupManager() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}