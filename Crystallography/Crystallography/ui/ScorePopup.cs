using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class ScorePopup : Label
	{
		private static readonly Font font = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25);
		private static readonly FontMap map = Crystallography.UI.FontManager.Instance.GetMap( font );
		
		public ScorePopup (ICrystallonEntity pParent, int pPoints) : base(pPoints.ToString(), map)
		{
			if (pPoints < 10) {
				Position = new Vector2(-7.0f, 60.0f);
			} else {
				Position = new Vector2(-12.0f, 60.0f);
			}
			Color = Colors.White;
			Pivot = new Vector2(0.5f, 0.5f);
			HeightScale = 1.0f;
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime( 0.1f ) );
			sequence.Add( new CallFunc( () => { pParent.getNode().Parent.Parent.AddChild(this); } ) );
			sequence.Add( new CallFunc( () => { Scheduler.Instance.ScheduleUpdateForTarget(this,0,false); } ) );
			this.RunAction(sequence);
		}
		
		// OVERRIDES ------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			base.Update (dt);
			Color.A -= dt/1.5f;
			HeightScale += 0.3f * (dt/1.5f);
			
			if ( Color.A < 0) {
				this.UnscheduleAll();
				Parent.RemoveChild(this, true);
			}
		}
		
		// DESTRUCTOR ------------------------------------------------------------------------------
#if DEBUG
		~ScorePopup() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}

