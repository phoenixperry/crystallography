using System;
using Sce.PlayStation.HighLevel.GameEngine2D;
//using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core;

namespace Crystallography
{
	public class SolutionIcon : Node
	{
		protected SpriteTile image;
		protected Label cubes;
		protected Label score;
		
		public string CubeText  { get { return cubes.Text; } set { cubes.Text = value; } }
		public string ScoreText { get { return score.Text; } set { score.Text = value; } }
		public Vector4 Color { get { return image.Color; } set { image.Color = value; cubes.Color = value; score.Color = value; } }
		public float Alpha { get { return image.Color.W; } set { image.Color.W = value; cubes.Color.W = value; score.Color.W = value; } }
		
		// CONSTRUCTOR -------------------------------------------------------------------------
		public SolutionIcon () : base() {
			image = Support.SpriteFromFile("/Application/assets/images/UI/cubePoints.png");
			AddChild(image);
			
			cubes = new Label(){
				Position = new Vector2(26.0f, 33.0f),
				FontMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 16, "Bold") )
			};
			cubes.Color = image.Color;
			AddChild(cubes);
			
			score = new Label(){
				Position = new Vector2(43.0f, 5.0f),
				FontMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 16, "Bold") )
			};
			score.Color = image.Color;
			AddChild(score);
		}
		
		// OVERRIDES ---------------------------------------------------------------------------
		
		public override void OnExit ()
		{
			base.OnExit ();
			
			image = null;
			cubes = null;
			score = null;
		}
	}
}

