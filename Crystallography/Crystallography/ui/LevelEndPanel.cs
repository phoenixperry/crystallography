using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class LevelEndPanel : Layer
	{
		SpriteUV Background;
		ButtonEntity NextLevelButton;
		ButtonEntity LevelSelectButton;
		ButtonEntity QuitButton;
		Label YourScoreText;
		Label BestScoreText;
		Label YourTimeText;
		Label BestTimeText;
		
		protected GameScene _scene;
		protected bool _initialized = false;
		
		// CONSTRUCTOR -----------------------------------------------------------------------------------
		
		public LevelEndPanel (GameScene scene) {
			_scene = scene;
			
			if (_initialized == false) {
				Initialize();
			}
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS --------------------------------------------------------------------------------
		
		void HandleNextLevelButtonButtonUpAction (object sender, EventArgs e)
		{
			NextLevelButtonReleased();
		}
		
		void HandleLevelSelectButtonButtonUpAction (object sender, EventArgs e)
		{
			LevelSelectButtonReleased();
		}
		
		void HandleQuitButtonButtonUpAction (object sender, EventArgs e)
		{
			QuitButtonReleased();
		}
		
		// METHODS ---------------------------------------------------------------------------------------
		
		private void Initialize() {
			_initialized = true;
			FontMap map = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 48) );
			
			Background = Support.SpriteUVFromFile("/Application/assets/images/UI/levelComplete.png");
			Background.Position = new Vector2(90.0f, 94.0f);
			this.AddChild(Background);
			
			YourScoreText = new Label("1234567890", map);
			YourScoreText.Position = new Vector2(276.0f, 190.0f);
			this.AddChild(YourScoreText);
			
			BestScoreText = new Label("1234567890", map);
			BestScoreText.Position = new Vector2(656.0f, 190.0f);
			this.AddChild(BestScoreText);
			
			YourTimeText = new Label("00:00.0", map);
			YourTimeText.Position = new Vector2(276.0f, 125.0f);
			this.AddChild(YourTimeText);
			
			BestTimeText = new Label("00:00.0", map);
			BestTimeText.Position = new Vector2(656.0f, 125.0f);
			this.AddChild(BestTimeText);
			
			NextLevelButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/nextLevel.png", 1, 3).TextureInfo, new Vector2i(0,0));
			NextLevelButton.setPosition(674, 35);
			this.AddChild(NextLevelButton.getNode());
			
			LevelSelectButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/levelSelectBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			LevelSelectButton.setPosition(372, 35);
			this.AddChild(LevelSelectButton.getNode());
			
			QuitButton = new ButtonEntity("", _scene, GamePhysics.Instance, Support.TiledSpriteFromFile("Application/assets/images/quit_game.png", 1, 3).TextureInfo, new Vector2i(0,0));
			QuitButton.setPosition(181, 35);
			this.AddChild(QuitButton.getNode ());
		}
		
		public void Hide() {
			this.Visible = false;
			NextLevelButton.ButtonUpAction -= HandleNextLevelButtonButtonUpAction;
			LevelSelectButton.ButtonUpAction -= HandleLevelSelectButtonButtonUpAction;
			QuitButton.ButtonUpAction -= HandleQuitButtonButtonUpAction;
			InputManager.Instance.CircleJustUpDetected -= HandleNextLevelButtonButtonUpAction;
			InputManager.Instance.CrossJustUpDetected -= HandleQuitButtonButtonUpAction;
		}
		
		public void UpdateAndShow(int pScore, float pTime) {
			SetScore( pScore );
			SetTime( pTime );
			Show();
		}
		
		public void Show() {
			this.Visible = true;
			NextLevelButton.ButtonUpAction += HandleNextLevelButtonButtonUpAction;
			LevelSelectButton.ButtonUpAction += HandleLevelSelectButtonButtonUpAction;
			QuitButton.ButtonUpAction += HandleQuitButtonButtonUpAction;
			InputManager.Instance.CircleJustUpDetected += HandleNextLevelButtonButtonUpAction;
			InputManager.Instance.CrossJustUpDetected += HandleQuitButtonButtonUpAction;
		}

		public void SetScore( int pScore ) {
			YourScoreText.Text = pScore.ToString();
			BestScoreText.Text = pScore.ToString();
		}
		
		public void SetTime( float pTime ) {
			int minutes = (int)System.Math.Floor( pTime / 60.0f );
			float seconds = pTime - (60 * minutes);
			YourTimeText.Text = minutes + ":" + seconds.ToString("00.0");
			BestTimeText.Text = minutes + ":" + seconds.ToString("00.0");
		}
		
		protected void NextLevelButtonReleased() {
#if DEBUG
			Console.WriteLine("-----NextLevelButtonReleased--------");
#endif
			Hide();
			_scene.GoToNextLevel();
		}
		
		protected void LevelSelectButtonReleased() {
#if DEBUG
			Console.WriteLine("-----LevelSelectButtonReleased--------");
#endif
			Hide();
			GameScene.QuitToLevelSelect();
		}
		
		protected void QuitButtonReleased() {
#if DEBUG
			Console.WriteLine("-----QuitButtonReleased--------");
#endif
			Hide();
			GameScene.QuitToTitle();
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~LevelEndPanel() {
			Console.WriteLine("LevelEndPanel deleted.");
        }
#endif
	}
}

