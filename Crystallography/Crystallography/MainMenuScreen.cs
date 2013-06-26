using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class MainMenuScreen : Layer
	{
		MenuSystemScene MenuSystem;
		SpriteTile MenuBackground;
		ButtonEntity NewGameButton;
		ButtonEntity LevelSelectButton;
		ButtonEntity CreditsButton;
		ButtonEntity InstructionsButton;
#if METRICS
		ButtonEntity PrintAnalyticsButton;
		ButtonEntity ClearAnalyticsButton;
		float HoldTimer;
#endif
		
		// CONSTRUCTORS -----------------------------------------------------------------------------------------
		
		public MainMenuScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			MenuBackground = Support.SpriteFromFile("/Application/assets/images/UI/menuButtonBackground.png");
			MenuBackground.Position = new Vector2(351.0f, 32.0f);
			this.AddChild(MenuBackground);
			
			NewGameButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/NewGameButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			NewGameButton.setPosition(480.0f, 403.0f);
			NewGameButton.on = true;
			this.AddChild(NewGameButton.getNode());
			
			LevelSelectButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/LevelSelectButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			LevelSelectButton.setPosition (480.0f, 315.0f);
			LevelSelectButton.on = true;
			this.AddChild(LevelSelectButton.getNode());
			
			CreditsButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/CreditsButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			CreditsButton.setPosition(480.0f, 143.0f);
			CreditsButton.on = true;
			this.AddChild(CreditsButton.getNode());
			
			InstructionsButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/InstructionsButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			InstructionsButton.setPosition(480.0f, 229.0f);
			InstructionsButton.on = true;
			this.AddChild(InstructionsButton.getNode());
			
#if METRICS
			HoldTimer = 0.0f;
			
			PrintAnalyticsButton = new ButtonEntity("Print Metrics", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/blueBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			PrintAnalyticsButton.setPosition(780.0f, 229.0f);
			PrintAnalyticsButton.on = false;
			this.AddChild(PrintAnalyticsButton.getNode());
			
			ClearAnalyticsButton = new ButtonEntity("Clear Metrics", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/blueBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			ClearAnalyticsButton.setPosition(180.0f, 229.0f);
			ClearAnalyticsButton.on = false;
			this.AddChild(ClearAnalyticsButton.getNode());
#endif	
		}
		
		
		// EVENT HANDLERS ---------------------------------------------------------------------------------------
		
		void HandleNewGameButtonButtonUpAction (object sender, EventArgs e) {
#if DEBUG
			Console.WriteLine("New Game");
#endif
			Director.Instance.ReplaceScene(new LoadingScene(0, false) );
		}
		
		void HandleLevelSelectButtonButtonUpAction (object sender, EventArgs e) {
#if DEBUG
			Console.WriteLine("Level Select");
#endif
			MenuSystem.SetScreen("Level Select");
		}
		
		void HandleCreditsButtonButtonUpAction (object sender, EventArgs e) {
#if DEBUG
			Console.WriteLine("Credits");
#endif
			MenuSystem.SetScreen("Credits");
		}
		
		void HandleInstructionsButtonButtonUpAction (object sender, EventArgs e) {
#if DEBUG
			Console.WriteLine("Instructions");
#endif
			MenuSystem.SetScreen("Instructions");
		}
		
#if METRICS
		void HandleClearAnalyticsButtonButtonUpAction (object sender, EventArgs e)
		{
			DataStorage.ClearMetrics();
		}

		void HandlePrintAnalyticsButtonButtonUpAction (object sender, EventArgs e)
		{
			DataStorage.PrintMetrics();
		}
#endif
		
		// OVERRIDES --------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			NewGameButton.ButtonUpAction += HandleNewGameButtonButtonUpAction;
			LevelSelectButton.ButtonUpAction += HandleLevelSelectButtonButtonUpAction;
			CreditsButton.ButtonUpAction += HandleCreditsButtonButtonUpAction;
			InstructionsButton.ButtonUpAction += HandleInstructionsButtonButtonUpAction;
#if METRICS
			PrintAnalyticsButton.ButtonUpAction += HandlePrintAnalyticsButtonButtonUpAction;
			ClearAnalyticsButton.ButtonUpAction += HandleClearAnalyticsButtonButtonUpAction;
#endif
		}

		
		
		public override void OnExit ()
		{
			base.OnExit ();
			NewGameButton.ButtonUpAction -= HandleNewGameButtonButtonUpAction;
			LevelSelectButton.ButtonUpAction -= HandleLevelSelectButtonButtonUpAction;
			CreditsButton.ButtonUpAction -= HandleCreditsButtonButtonUpAction;
			InstructionsButton.ButtonUpAction -= HandleInstructionsButtonButtonUpAction;
#if METRICS
			PrintAnalyticsButton.ButtonUpAction -= HandlePrintAnalyticsButtonButtonUpAction;
			ClearAnalyticsButton.ButtonUpAction -= HandleClearAnalyticsButtonButtonUpAction;
#endif
			this.RemoveAllChildren(true);
			MenuSystem = null;
			MenuBackground = null;
			NewGameButton = null;
			LevelSelectButton = null;
			CreditsButton = null;
			InstructionsButton = null;
			RemoveAllAssets();
		}

#if METRICS
		public override void Update (float dt)
		{
			base.Update (dt);
			if (Input2.GamePad0.L.Down && Input2.GamePad0.R.Down) {
				HoldTimer += dt;
				if (HoldTimer > 2.0f) {
					HoldTimer = 0.0f;
					PrintAnalyticsButton.on = !PrintAnalyticsButton.on;
					ClearAnalyticsButton.on = !ClearAnalyticsButton.on;
				}
			} else {
				HoldTimer = 0.0f;
			}
		}
#endif
		
		// METHODS ----------------------------------------------------------------------------------------------
		
		private void RemoveAllAssets() {
			Support.RemoveTextureWithFileName("/Application/assets/images/UI/menuButtonBackground.png");
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------
#if DEBUG
		~MainMenuScreen() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

