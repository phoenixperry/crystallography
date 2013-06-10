using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class ScorePopup : Label
	{
		private static readonly Vector2 SingleDigitOffset = new Vector2(-12.0f, 60.0f);
		private static readonly Vector2 DoubleDigitOffset = new Vector2(-7.0f, 60.0f);
		private static readonly Font font = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25);
		private static readonly FontMap map = Crystallography.UI.FontManager.Instance.GetMap( font );
		
		protected Node parentNode;
		protected Vector2 offset;
		
		
		public ScorePopup (ICrystallonEntity pParent, int pPoints) : base(pPoints.ToString(), map)
		{
			parentNode = pParent.getNode();
			if (pPoints < 10) {
				offset = DoubleDigitOffset;
			} else {
				offset = SingleDigitOffset;
			}
			Position = parentNode.Parent.Parent.Position + offset;
			Color = Colors.White;
			Pivot = new Vector2(0.5f, 0.5f);
			HeightScale = 1.0f;
			
//			pParent.getNode().Parent.Parent.
			
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime( 0.1f ) );
			GameScene.Layers[2].AddChild(this);
//			sequence.Add( new CallFunc( () => { pParent.getNode().Parent.Parent.AddChild(this); } ) );
			sequence.Add( new CallFunc( () => { Scheduler.Instance.ScheduleUpdateForTarget(this,0,false); } ) );
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
			Position = parentNode.Parent.Parent.Position + offset;
			HeightScale += 0.3f * (dt/1.5f);
			
			if ( Color.A <= 0) {
				Destroy();
			}
		}
		
		// METHODS ---------------------------------------------------------------------------------
		
		public void Destroy() {
			parentNode = null;
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

