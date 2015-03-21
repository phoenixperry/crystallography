using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class InfiniteModeScreen : Layer
	{
		protected MenuSystemScene MenuSystem;
		
		protected BetterButton playButton;
		protected BetterButton cancelButton;
		
		protected Label _timeLimitText;
		protected Label _fourthQualityText;
		
		protected Slider timeLimitSlider;
		protected Slider fourthQualitySlider;
		
		protected SpriteTile _bestBG;
		
		protected Label _bestTitleText;
		protected Label _bestCubesText;
		protected Label _bestPointsText;
		protected Label _bestTimeText;
		
		protected int _bestCubes;
		protected int _bestPoints;
		protected float _bestTime;
		
		protected HighScoreEntry[] _highScoreEntries;
		
//		protected SpriteTile _cubeIcon;
//		protected SpriteTile _scoreIcon;
//		protected SpriteTile _timeIcon;
		
		// CONSTRUCTOR ------------------------------------------
		
		public InfiniteModeScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			var map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18, "Bold") );
			
			_bestCubes = _bestPoints = 0;
			_bestTime = 0.0f;
			
			_timeLimitText = new Label() {
				FontMap = map,
				Color = Colors.Black
			};
//			_timeLimitText.RegisterPalette(0);
			this.AddChild(_timeLimitText);
			
			timeLimitSlider = new Slider(540) {
				Text = "time limit",
				Position = new Vector2(33.0f, 440.0f),
				max = 60.0f,
				min = 5.0f,
				discreteOptions = new List<float>() { 5.0f, 10.0f, 20.0f, 35.0f, 60.0f },
				OnChange = (unused) => {
					if ( timeLimitSlider.Value != timeLimitSlider.max ) {
						_timeLimitText.Text = timeLimitSlider.Value.ToString() + " minutes";
						_bestTitleText.Text = _timeLimitText.Text;
						if(_highScoreEntries != null) {
							_highScoreEntries[0].ShowBestTime(false);
							_highScoreEntries[0].BestCubes = DataStorage.timedCubes[timeLimitSlider.SelectedOption,0,0];
							_highScoreEntries[0].BestPoints = DataStorage.timedScores[timeLimitSlider.SelectedOption,0,1];
						}
					} else {
						_timeLimitText.Text = "infinite";
						if(_highScoreEntries != null) {
							_highScoreEntries[0].ShowBestTime(true);
							
							_highScoreEntries[0].BestCubes = DataStorage.infiniteCubes[0,0];
							_highScoreEntries[0].BestPoints = DataStorage.infiniteScores[0,1];
							_highScoreEntries[0].BestTime = DataStorage.infiniteTimes[0,2];
						}
					}
				}
			};
			timeLimitSlider.AddTickmarks();
			timeLimitSlider.RegisterPalette(0);
			timeLimitSlider.SetSliderValue( (float)DataStorage.options[4] );
			this.AddChild(timeLimitSlider);
			
//			_timeLimitText.Position = new Vector2(timeLimitSlider.Position.X + timeLimitSlider.Length + 20.0f, timeLimitSlider.Position.Y);
			_timeLimitText.Position = new Vector2(timeLimitSlider.Position.X + 4.0f, timeLimitSlider.Position.Y - 41.0f);
			
			_fourthQualityText = new Label() {
				FontMap = map
			};
			_fourthQualityText.RegisterPalette(1);
			this.AddChild(_fourthQualityText);
			
			fourthQualitySlider = new Slider() {
				Text = "fourth quality",
				Position = new Vector2(timeLimitSlider.Position.X, timeLimitSlider.Position.Y - 100.0f),
				max = 2.0f,
				min = 0.0f,
				discreteOptions = new List<float>() { 0.0f, 1.0f, 2.0f },
				OnChange = (unused) => {
					switch(fourthQualitySlider.Value.ToString("#0.#")) {
					case("0"):
						_fourthQualityText.Text = "none";
						break;
					case("1"):
						_fourthQualityText.Text = "particles";
						break;
					case("2"):
						_fourthQualityText.Text = "sound";
						break;
					}
				}
			};
			fourthQualitySlider.AddTickmarks();
			fourthQualitySlider.RegisterPalette(1);
			fourthQualitySlider.SetSliderValue( 1.0f );
			this.AddChild(fourthQualitySlider);
			
			_fourthQualityText.Position = new Vector2(fourthQualitySlider.Position.X + fourthQualitySlider.Length + 20.0f, fourthQualitySlider.Position.Y);
			
			
			_bestBG = Support.UnicolorSprite("black", 0,0,0,255);
			_bestBG.Position = new Vector2(598.0f, 0.0f);
			_bestBG.Scale = new Vector2(22.625f, 34.0f);
			this.AddChild(_bestBG);
			
			_bestTitleText = new Label() {
				Text = _timeLimitText.Text,
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") ),
				Position = new Vector2(_bestBG.Position.X + 41.0f, 465.0f)
			};
//			_bestTitleText.RegisterPalette(2);
//			_bestTitleText.Color = Colors.White;
			this.AddChild(_bestTitleText);
			
			
			_highScoreEntries = new HighScoreEntry[3];
			_highScoreEntries[0] = new HighScoreEntry() {
				BestCubes = DataStorage.infiniteCubes[0,0],
				BestPoints = DataStorage.infiniteCubes[0,1],
				BestTime = (float)DataStorage.infiniteCubes[0,2],
				Position = new Vector2(_bestTitleText.Position.X, _bestTitleText.Position.Y - 316.0f)
			};
//			_highScoreEntries[1] = new HighScoreEntry() {
//				BestCubes = DataStorage.infiniteScores[0,0],
//				BestPoints = DataStorage.infiniteScores[0,1],
//				BestTime = (float)DataStorage.infiniteScores[0,2],
//				Position = new Vector2(_bestTitleText.Position.X, _bestTitleText.Position.Y - 120)
//			};
//			_highScoreEntries[2] = new HighScoreEntry() {
//				BestCubes = DataStorage.infiniteTimes[0,0],
//				BestPoints = DataStorage.infiniteTimes[0,1],
//				BestTime = (float)DataStorage.infiniteTimes[0,2],
//				Position = new Vector2(_bestTitleText.Position.X, _bestTitleText.Position.Y - 180)
//			};
//			for(int i=0; i<3; i++){
//				this.AddChild(_highScoreEntries[i]);
//			}
			this.AddChild(_highScoreEntries[0]);
			
			cancelButton = new BetterButton(362.0f, 62.0f) {
				Text = "main menu",
				Position = new Vector2(598.0f, 62.0f)
			};
			cancelButton.background.RegisterPalette(2);
			this.AddChild(cancelButton);
			
			playButton = new BetterButton(362.0f, 62.0f) {
				Text = "play",
				Position = new Vector2(598.0f, 0.0f)
			};
			playButton.background.RegisterPalette(0);
			this.AddChild(playButton);
		}
		
		// EVENT HANDLERS ----------------------------------------
		
		void HandleCancelButtonButtonUpAction (object sender, EventArgs e)
		{
			Exit();
			MenuSystem.SetScreen("Menu");
		}
		
		void HandleplayButtonButtonUpAction (object sender, EventArgs e)
		{
			Exit();
			float limit = (timeLimitSlider.Value < 60.0f) ? timeLimitSlider.Value * 60.0f : 0.0f;
			LoadingScene.GAME_SCENE_DATA.level = 999;
			LoadingScene.GAME_SCENE_DATA.timeLimit = limit;
			LoadingScene.GAME_SCENE_DATA.fourthQuality = _fourthQualityText.Text;
			Director.Instance.ReplaceScene(new LoadingScene("Game", LoadingScene.GAME_SCENE_DATA) );
		}
		
		// OVERRIDES ---------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			playButton.ButtonUpAction += HandleplayButtonButtonUpAction;
			cancelButton.ButtonUpAction += HandleCancelButtonButtonUpAction;
		}
		
		public override void OnExit ()
		{
			playButton.ButtonUpAction -= HandleplayButtonButtonUpAction;
			base.OnExit ();
			timeLimitSlider = null;
			playButton = null;
		}
		
		// METHODS -----------------------------------------------
		
		protected void Exit() {
			// TODO WRITE SETTINGS TO DATA FOR PERSISTENCE
			DataStorage.options[4] = (int)timeLimitSlider.Value;
		}
		
		// DESTRUCTOR --------------------------------------------
#if DEBUG
		~InfiniteModeScreen() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}

