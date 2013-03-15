using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.UI
{
    public partial class ScorePanel : Panel
    {
        public ScorePanel( Vector2 pPosition, int pPoints )
        {
            InitializeWidget();
			
			ScoreText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
			ScoreText.Text = pPoints.ToString();
			ScoreText.Alpha = 0.0f;
			Vector2 v = ConvertScreenToLocal( pPosition );
//			this.ParentScene.
			this.SetPosition( v.X - ScoreText.Width/2, Director.Instance.GL.Context.GetViewport().Height - (v.Y + 40) );
			
			Sequence sequence = new Sequence();
//			sequence.Add ( new MoveBy( new Vector2(0.0f, 10.0f), 3.0f ) );
			sequence.Add( new DelayTime( 3.0f ) );
			sequence.Add ( new CallFunc( () => { 
				ScoreText.Visible = false;
				this.Dispose(); 
			} ) );
			Director.Instance.CurrentScene.RunAction(sequence);
			
//			AbstractQuality.MatchScoreDetected += HandleAbstractQualityMatchScoreDetected;
        }

        void HandleAbstractQualityMatchScoreDetected (object sender, MatchScoreEventArgs e)
        {
        	ScoreText.Text = e.Points.ToString();
			Sequence sequence = new Sequence();
//			sequence.Add ( new MoveBy( new Vector2(0.0f, 100.0f), 3.0f ) );
//			sequence.Add( new DelayTime( 3.0f ) );
			sequence.Add ( new CallFunc( () => { 
				ScoreText.Visible = false; 
				this.Dispose(); 
			} ) );
			Director.Instance.CurrentScene.RunAction(sequence);
        }
		
		// METHODS ---------------------------------------------------------------------------
		
		protected override void OnUpdate (float elapsedTime)
		{
			base.OnUpdate (elapsedTime);
			if ( ScoreText.Alpha < 1.0f ) {
				ScoreText.Alpha += elapsedTime/1500;
			}
			this.Y -= elapsedTime/100;
			
		}
    }
}
