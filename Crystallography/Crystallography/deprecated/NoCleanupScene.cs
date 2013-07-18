/* SCE CONFIDENTIAL
 * PlayStation(R)Suite SDK 0.98.2
 * Copyright (C) 2013 Sony Computer Entertainment Inc.
 * All Rights Reserved.
 */

using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.Deprecated
{
	public class NoCleanupScene : Scene
	{
		public override void OnEnter()
		{
			base.OnEnter();
		}

		public override void OnExit()
		{
			StopAllActions();
		}
	}

}

