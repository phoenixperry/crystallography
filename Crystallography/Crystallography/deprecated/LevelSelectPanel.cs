using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI.Deprecated
{
    public partial class LevelSelectPanel : Panel
    {
        public LevelSelectPanel(int pageNumber)
        {
            InitializeWidget();
			
			int baseIndex = pageNumber * 12;
			
//			Button[] buttons;
			
//			for ( int i=0; i < 12; i++ ) {
//				
//			}
			
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
			int buttonCount = panels.Length;
			if ( GameScene.TOTAL_LEVELS < baseIndex + 11 ) {
				buttonCount = GameScene.TOTAL_LEVELS - baseIndex;
				Console.WriteLine(baseIndex + "/" + GameScene.TOTAL_LEVELS);
			}
			for ( int i=0; i < buttonCount; i++ ) {
//				buttons[i].Visible = false;
				LevelSelectItem item = new LevelSelectItem();
//				item.Color = buttons[i].BackgroundFilterColor;
				item.levelID = i + baseIndex;
				panels[i].AddChildLast( item );
			}
        }
    }
}
