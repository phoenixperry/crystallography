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
		protected Label _bestTimeText;
		
		protected int _bestCubes;
		protected int _bestPoints;
		protected float _bestTime;
		
		protected SpriteTile _cubeIcon;
		protected SpriteTile _scoreIcon;
		protected SpriteTile _timeIcon;
		
		
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
		
		public float BestTime {
			get {return _bestTime;} 
			set {
				_bestTime = value;
				if(_bestTimeText != null)
					_bestTimeText.Text = TimeSpan.FromSeconds((double)_bestTime).ToString("c");
			}
		}
		
		public HighScoreEntry ()
		{
			var map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			
			_bestCubes = _bestPoints = 0;
			_bestTime = 0.0f;
			
			_bestCubesTitle = new Label() {
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
				Text = "the most cubes",
				Position = new Vector2(0.0f, 263.0f)
			};
			_bestCubesTitle.RegisterPalette(0);
			this.AddChild(_bestCubesTitle);
			
			_cubeIcon = Support.SpriteFromFile("/Application/assets/images/stopIcon.png");
			_cubeIcon.Position = new Vector2(0.0f, 174.0f);
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
				Position = new Vector2(0.0f, 93.0f)
			};
			_bestPointsTitle.RegisterPalette(1);
			this.AddChild(_bestPointsTitle);
			
			
			_scoreIcon = Support.SpriteFromFile("/Application/assets/images/handIcon.png");
			_scoreIcon.Position = new Vector2(0.0f, 0.0f);
			_scoreIcon.RegisterPalette(1);
			this.AddChild(_scoreIcon);
			
			_bestPointsText = new Label() {
				Text = _bestPoints.ToString(),
				FontMap = map,
				Position = new Vector2(97.0f, 0.0f)
			};
			_bestPointsText.RegisterPalette(1);
			this.AddChild(_bestPointsText);
			
			
			_timeIcon = Support.SpriteFromFile("/Application/assets/images/timerIcon.png");
			_timeIcon.Position = new Vector2(240.0f, 0.0f);
			_timeIcon.RegisterPalette(2);
//			this.AddChild(_timeIcon);
			
			var time = TimeSpan.FromSeconds((double)_bestTime);
			
			_bestTimeText = new Label() {
				Text = time.ToString("c"),
				FontMap = map,
				Position = new Vector2(300.0f, 0.0f)
			};
			_bestTimeText.RegisterPalette(2);
//			this.AddChild(_bestTimeText);
		}
		
		public void ShowBestTime(bool pShow)
		{
			_timeIcon.Visible = pShow;
			_bestTimeText.Visible = pShow;
		}
	}
}

