using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class HighScoreEntry : Node
	{
		
		protected Label _bestCubesTitle;
		protected Label _bestPointsTitle;
		
		protected Label _bestCubesText;
		protected Label _bestPointsText;
		
		protected int _bestCubes;
		protected int _bestPoints;
		
		protected SpriteTile _cubeIcon;
		protected SpriteTile _scoreIcon;
		
		
		public int BestCubes {
			get {return _bestCubes;} 
			set {
				_bestCubes = value;
				if(_bestCubesText != null)
					_bestCubesText.Text = _bestCubes.ToString();
			}
		}
		
		public int BestPoints {
			get {return _bestPoints;} 
			set {
				_bestPoints = value;
				if(_bestPointsText != null)
					_bestPointsText.Text = _bestPoints.ToString();
			}
		}
		
		public HighScoreEntry ()
		{
			var map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 102, "Bold") );
			
			_bestCubes = _bestPoints = 0;
			
			_bestCubesTitle = new Label() {
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
				Text = "the most cubes",
				Position = new Vector2(0.0f, 279.0f)
			};
			_bestCubesTitle.RegisterPalette(0);
			this.AddChild(_bestCubesTitle);
			
			_cubeIcon = Support.SpriteFromFile("/Application/assets/images/UI/cubes_big.png");
			_cubeIcon.Position = new Vector2(0.0f, 194.0f);
			_cubeIcon.RegisterPalette(0);
			this.AddChild(_cubeIcon);
			
			_bestCubesText = new Label() {
				Text = _bestCubes.ToString(),
				FontMap = map,
				Position = new Vector2(97.0f, 174.0f)
			};
			_bestCubesText.RegisterPalette(0);
			this.AddChild(_bestCubesText);
			
			
			_bestPointsTitle = new Label() {
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
				Text = "the most points",
				Position = new Vector2(0.0f, 108.0f)
			};
			_bestPointsTitle.RegisterPalette(1);
			this.AddChild(_bestPointsTitle);
			
			
			_scoreIcon = Support.SpriteFromFile("/Application/assets/images/UI/points_big.png");
			_scoreIcon.Position = new Vector2(0.0f, 20.0f);
			_scoreIcon.RegisterPalette(1);
			this.AddChild(_scoreIcon);
			
			_bestPointsText = new Label() {
				Text = _bestPoints.ToString(),
				FontMap = map,
				Position = new Vector2(97.0f, 0.0f)
			};
			_bestPointsText.RegisterPalette(1);
			this.AddChild(_bestPointsText);
			
		}
		
		public override void OnExit ()
		{
			_bestCubesTitle = null;
			_bestPointsTitle = null;
			_bestCubesText = null;
			_bestPointsTitle = null;
			_cubeIcon = null;
			_scoreIcon = null;
			
			base.OnExit ();
		}
	}
}

