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
			Sequence sequence = new Sequence();
			foreach( string key in pQualities.Keys) {
				if(key == "Orientation") {
					continue;
				}
				for (int i=0; i<pQualities[key]; i++) {
					switch(key){
					case("Color"):
						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Color", (pParent as CardCrystallonEntity), pColor); } ) );
						break;
					case("Animation"):
						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Animation", (pParent as CardCrystallonEntity), pColor); } ) );
						break;
					case("Pattern"):
						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Pattern", (pParent as CardCrystallonEntity), pColor); } ) );
						break;
					case("Particle"):
						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Particle", (pParent as CardCrystallonEntity), pColor); } ) );
						break;
					case("Sound"):
						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Sound", (pParent as CardCrystallonEntity), pColor); } ) );
						break;
					default:
#if DEBUG
						sequence.Add ( new CallFunc( () => { Support.ParticleEffectsManager.Instance.AddScoreParticle("Particle", (pParent as CardCrystallonEntity), Colors.Green); } ) );
#endif
						break;
					}
					sequence.Add( new DelayTime( 0.05f ) );
				}
			}
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