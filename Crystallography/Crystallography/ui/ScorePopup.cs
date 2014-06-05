using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class ScorePopup : Label
	{
		private static readonly Vector2 SingleDigitOffset = new Vector2(48.0f, 84.0f);
		private static readonly Vector2 DoubleDigitOffset = new Vector2(51.0f, 84.0f);
//		private static readonly Vector2 SingleDigitOffset = new Vector2(48.0f, 0.0f);
//		private static readonly Vector2 DoubleDigitOffset = new Vector2(51.0f, 0.0f);
		private static readonly Font font = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36);
		private static readonly FontMap map = Crystallography.UI.FontManager.Instance.GetMap( font );
		
		protected Node parent;
		protected Vector2 offset;
//		protected Label inside;
		
		
		public ScorePopup (Node pParent, int pPoints) : base(pPoints.ToString(), map)
		{
			parent = pParent;
			if (pPoints < 10) {
				offset = DoubleDigitOffset;
			} else {
				offset = SingleDigitOffset;
			}
//			velocity = Vector2.Zero;
			Position = parent.LocalToWorld(parent.Position) + offset;
//			this.RegisterPalette(0);
			Color = Support.ExtractColor("333330");
			Pivot = new Vector2(0.5f, 0.5f);
//			HeightScale = 1.0f;
			
//			inside = new Label() {
//				Text = pPoints.ToString(),
//				FontMap = map,
//				Pivot = Vector2.One/2.0f,
////				Position = Vector2.One
////				Scale = new Vector2(0.9f, 0.9f)
//			};
//			inside.Color = LevelManager.Instance.BackgroundColor;
//			this.AddChild(inside);
						
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime( 0.1f ) );
			GameScene.Layers[2].AddChild(this);
			sequence.Add( new CallFunc( () => {
				Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			} ) );
			this.RunAction(sequence);
			
#if DEBUG
			Console.WriteLine (GetType().ToString() + " created" );
#endif
		}
		
		// OVERRIDES ------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			base.Update (dt);
			Color.A -= dt/1.5f;
			
//			if( parent != null) {
//				Position = parent.LocalToWorld(parent.Position) + offset;
//			} 
//			else {
//				Position += offset;
//			}
//			HeightScale += 0.3f * (dt/1.5f);
			
			Scale += Vector2.One * (dt/1.5f);

			
			if ( Color.A <= 0) {
				Destroy();
			}
		}
		
		// METHODS ---------------------------------------------------------------------------------
		
		public void Destroy() {
			parent = null;
			this.UnscheduleAll();
			this.RemoveAllChildren(true);
			this.Parent.RemoveChild(this, true);
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
#if DEBUG
		~ScorePopup() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}

