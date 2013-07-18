using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI.Deprecated
{
    public partial class InstructionsScene : Scene
    {
        public InstructionsScene()
        {
            InitializeWidget();
        }
		
		// DESTRUCTOR ------------------------------------------------------------------
		
#if DEBUG
		~InstructionsScene(){
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
    }
}
