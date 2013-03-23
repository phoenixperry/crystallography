using System;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class Layer : Node
	{
		public Layer () : base()
		{
			Scheduler.Instance.ScheduleUpdateForTarget(this, 0, false);
		}
	}
}

