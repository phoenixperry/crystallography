using Sce.PlayStation.Core;
using System;
namespace Sce.PlayStation.HighLevel.GameEngine2D
{
	public class MovePivotTo : ActionTweenGenericVector2
	{
		public MovePivotTo (Vector2 target, float duration)
		{
			this.TargetValue = target;
			this.Duration = duration;
			this.IsRelative = false;
			this.Get = (() => base.Target.Pivot);
			this.Set = delegate (Vector2 value)
			{
				base.Target.Pivot = value;
			}
			;
		}
	}
}
