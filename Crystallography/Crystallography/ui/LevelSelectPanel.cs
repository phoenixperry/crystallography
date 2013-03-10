using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    public partial class LevelSelectPanel : Panel
    {
        public LevelSelectPanel()
        {
            InitializeWidget();
			
			Panel[] panels = { Panel_1,
						       Panel_2,
						       Panel_3,
						       Panel_4,
						       Panel_5,
						       Panel_6,
						       Panel_7,
						       Panel_8,
						       Panel_9,
						       Panel_10,
						       Panel_11,
						       Panel_12 };
			for ( int i=0; i < panels.Length; i++ ) {
				LevelSelectItem item = new LevelSelectItem();
				item.levelID = i;
				panels[i].AddChildLast( item );
			}
        }
    }
}
