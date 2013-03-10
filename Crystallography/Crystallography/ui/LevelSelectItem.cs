using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    public partial class LevelSelectItem : Panel
    {
		public int levelID { get; set; }
		public static event EventHandler<LevelSelectionEventArgs> LevelSelectionDetected;		
		
        public LevelSelectItem()
        {
            InitializeWidget();
			Button_1.TouchEventReceived += (sender, e) => {
				EventHandler<LevelSelectionEventArgs> handler = LevelSelectionDetected;
				if ( handler != null ) {
					handler( this, new LevelSelectionEventArgs() { LevelID = levelID });
				}
			};
			
        }
    }
	
	public class LevelSelectionEventArgs : EventArgs {
		public int LevelID;
	}
}
