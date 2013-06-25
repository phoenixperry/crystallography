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
		
		// CONSTRUCTORS -----------------------------------------------------------------------------------------
		
		public MainMenuScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			MenuBackground = Support.SpriteFromFile("/Application/assets/images/UI/menuButtonBackground.png");
			MenuBackground.Position = new Vector2(351.0f, 32.0f);
			this.AddChild(MenuBackground);
			
			NewGameButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/NewGameButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			NewGameButton.setPosition(480.0f, 403.0f);
//			NewGameButton.setPivot(0.0f, 0.0f);
			NewGameButton.on = true;
			NewGameButton.ButtonUpAction += HandleNewGameButtonButtonUpAction;
			this.AddChild(NewGameButton.getNode());
			
			LevelSelectButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/LevelSelectButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			LevelSelectButton.setPosition (480.0f, 315.0f);
//			LevelSelectButton.setPivot(0.0f, 0.0f);
			LevelSelectButton.on = true;
			LevelSelectButton.ButtonUpAction += HandleLevelSelectButtonButtonUpAction;
			this.AddChild(LevelSelectButton.getNode());
			
			CreditsButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/CreditsButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			CreditsButton.setPosition(480.0f, 143.0f);
//			CreditsButton.setPivot(0.0f, 0.0f);
			CreditsButton.on = true;
			CreditsButton.ButtonUpAction += HandleCreditsButtonButtonUpAction;
			this.AddChild(CreditsButton.getNode());
			
			InstructionsButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/InstructionsButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			InstructionsButton.setPosition(480.0f, 229.0f);
//			InstructionsButton.setPivot(0.0f, 0.0f);
			InstructionsButton.on = true;
			InstructionsButton.ButtonUpAction += HandleInstructionsButtonButtonUpAction;
			this.AddChild(InstructionsButton.getNode());
			
//			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
		}
		
		
		// EVENT HANDLERS ---------------------------------------------------------------------------------------
		
		void HandleNewGameButtonButtonUpAction (object sender, EventArgs e) {
			Console.WriteLine("New Game");
			Director.Instance.ReplaceScene(new LoadingScene(0, false) );
		}
		
		void HandleLevelSelectButtonButtonUpAction (object sender, EventArgs e) {
			Console.WriteLine("Level Select");
		}
		
		void HandleCreditsButtonButtonUpAction (object sender, EventArgs e) {
			Console.WriteLine("Credits");
		}
		
		void HandleInstructionsButtonButtonUpAction (object sender, EventArgs e) {
			Console.WriteLine("Instructions");
		}
		
		// OVERRIDES --------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			this.RemoveAllChildren(true);
			MenuSystem = null;
			MenuBackground = null;
			NewGameButton = null;
			LevelSelectButton = null;
			CreditsButton = null;
			InstructionsButton = null;
			RemoveAllAssets();
		}
		
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

