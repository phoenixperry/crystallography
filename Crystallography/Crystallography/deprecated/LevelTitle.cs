using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Crystallography.UI.Deprecated
{
    public partial class LevelTitle : Panel
    {
		List<Label> VariableNames;
		
        public LevelTitle()
        {
            VariableNames = new List<Label>();
			
			InitializeWidget();
			
			NextLevelText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
			LevelNumberText.Font = FontManager.Instance.Get ("Bariol", 72, "Bold");
			
        }
		
		// METHODS ------------------------------------------------------------------------------
		
		public void SetLevelText( int pNumber ) {
			LevelNumberText.Text = pNumber.ToString();
		}
		
		public void SetVariableNames( string[] pNames ) {
			foreach ( Label l in VariableNames) {
				this.RemoveChild(l);
				l.Dispose();
			}
			VariableNames.Clear();
			Label n;
			foreach ( string name in pNames ){
				VariableNames.Add( n = new Label() );
				n.TextColor = new UIColor(1.0f, 1.0f, 1.0f, 1.0f);
				n.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
				n.LineBreak = LineBreak.Character;
				n.Text = name;
				n.Y = 180;
				n.X = 50 + (VariableNames.Count-1)*80;
				this.AddChildLast(n);
			}
		}
		
		public void SetVisible( bool pIsVisible ) {
			this.Visible = pIsVisible;
		}
    }
}
