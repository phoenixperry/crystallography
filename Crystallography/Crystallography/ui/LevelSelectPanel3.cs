using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    public partial class LevelSelectPanel3 : Panel
    {
        public LevelSelectPanel3()
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
			Button[] buttons = { Button_1,
						       Button_2,
						       Button_3,
						       Button_4,
						       Button_5,
						       Button_6,
						       Button_7,
						       Button_8,
						       Button_9,
						       Button_10,
						       Button_11,
						       Button_12 };
			for ( int i=0; i < panels.Length; i++ ) {
				buttons[i].Visible = false;
				LevelSelectItem item = new LevelSelectItem();
				item.Color = buttons[i].BackgroundFilterColor;
				item.levelID = i + 24;
				if ( item.levelID > GameScene.TOTAL_LEVELS-1 ) {
					item.Button.Enabled = false;
				}
				panels[i].AddChildLast( item );
			}
        }
    }
}
