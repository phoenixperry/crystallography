using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI.Deprecated
{
    public partial class ScorePanel : Panel {
		
		Sce.PlayStation.HighLevel.GameEngine2D.Label ScoreLabel;
		
        public ScorePanel( ICrystallonEntity pEntity, int pPoints )
        {
            InitializeWidget();
			
			ScoreLabel = new Sce.PlayStation.HighLevel.GameEngine2D.Label() {
				Text = pPoints.ToString()
			};
			var font = new Font("Application/assets/fonts/Bariol_Regular.otf", 25, FontStyle.Regular);
			var map = new FontMap(font);
			ScoreLabel.FontMap = map;
			ScoreLabel.Position = new Vector2(-4.0f, 10.0f);
			ScoreLabel.Color = Colors.White;
			ScoreLabel.HeightScale = 1.0f;
			ScoreLabel.Pivot = new Vector2(0.5f, 0.5f);
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime( 0.5f ) );
			sequence.Add ( new CallFunc( () => { pEntity.getNode().Parent.AddChild(ScoreLabel); } ) );
			
			
			
//			ScoreText.Font = FontManager.Instance.Get ("Bariol", 20, "Bold");
//			ScoreText.Text = pPoints.ToString();
//			ScoreText.Alpha = 0.0f;
//			_scoreTexture = new Texture2D((int)ScoreText.Width, (int)ScoreText.Height, false, PixelFormat.Rgba);
//			ScoreText.RenderToTexture(_scoreTexture);
//			_scoreTextureInfo = new TextureInfo(_scoreTexture);
//			_scoreSprite = new SpriteUV(_scoreTextureInfo);
//			_scoreSprite.Pivot = new Vector2(0.5f, 0.5f);
//			pEntity.getNode().AddChild(_scoreSprite);
//			_scoreSprite.Position = new Vector2(0.0f, 20.0f);
//			Vector2 v = ConvertScreenToLocal( pPosition );
//			this.SetPosition( v.X - ScoreText.Width/2, Director.Instance.GL.Context.GetViewport().Height - (v.Y + 40) );
//			Sequence sequence = new Sequence();
//			sequence.Add( new DelayTime( 3.0f ) );
//			sequence.Add ( new CallFunc( () => { 
//				ScoreText.Visible = false;
//				this.Dispose(); 
//			} ) );
			Director.Instance.CurrentScene.RunAction(sequence);
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
			ScoreLabel.RunAction(sequence);
        }
		
		// METHODS ---------------------------------------------------------------------------
		
		protected override void OnUpdate (float elapsedTime)
		{
			base.OnUpdate (elapsedTime);
			if (ScoreLabel.Color.A > 0) {
				ScoreLabel.Color.A -= elapsedTime/1500.0f;
				ScoreLabel.HeightScale += 0.3f * (elapsedTime/1500.0f);
			} else {
				this.Dispose();
			}
			
//			if ( ScoreText.Alpha < 1.0f ) {
//				ScoreText.Alpha += elapsedTime/1500;
//			}
//			this.Y -= elapsedTime/100;
			
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------
		
		~ScorePanel() {
			ScoreLabel = null;
//			_scoreTexture = null;
//			_scoreTextureInfo = null;
		}
    }
}
