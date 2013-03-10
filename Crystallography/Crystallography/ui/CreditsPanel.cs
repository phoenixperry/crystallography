using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI
{
    public partial class CreditsPanel : Panel
    {
        public CreditsPanel()
        {
            InitializeWidget();
			
			BenText.Font = FontManager.Instance.Get ("Bariol", 36);
			PhoenixText.Font = FontManager.Instance.Get ("Bariol", 36);
			MargaretText.Font = FontManager.Instance.Get ("Bariol", 36);
        }
    }
}
