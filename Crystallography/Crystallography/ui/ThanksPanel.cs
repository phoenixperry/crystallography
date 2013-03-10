using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    public partial class ThanksPanel : Panel
    {
        public ThanksPanel()
        {
            InitializeWidget();
			ThanksText.Font = FontManager.Instance.Get("Bariol", 36);
			ThanksNamesText.Font = FontManager.Instance.Get("Bariol", 25);
        }
    }
}
