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
		
		public UIColor Color { get{ return Button_1.BackgroundFilterColor; } set{ Button_1.BackgroundFilterColor = value; } }
		public Button Button { get{ return Button_1; } }
		
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
