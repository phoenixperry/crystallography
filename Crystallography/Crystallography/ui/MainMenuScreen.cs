using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class MainMenuScreen : Layer
	{
		MenuSystemScene MenuSystem;
//		SpriteTile MenuBackground;
		SpriteTile MenuBGBottom;
		SpriteTile MenuBGTop;
		SpriteTile MenuBGSpacer1;
		SpriteTile MenuBGSpacer2;
		SpriteTile MenuBGSpacer3;
		SpriteTile MaskBGTop;
		SpriteTile MaskBGButton1;
		SpriteTile MaskBGSpacer1;
		SpriteTile MaskBGButton2;
		SpriteTile MaskBGSpacer2;
		SpriteTile MaskBGButton3;
		SpriteTile MaskBGSpacer3;
		SpriteTile MaskBGButton4;
		SpriteTile MaskBGBottom;
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
			
//			MenuBackground = Support.SpriteFromFile("/Application/assets/images/UI/menuButtonBackground.png");
//			MenuBackground.Position = new Vector2(351.0f, 32.0f);
//			this.AddChild(MenuBackground);
			
			MenuBGBottom = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_bottom.png");
			MenuBGBottom.Position = new Vector2(351.0f, 32.0f);
			this.AddChild(MenuBGBottom);
			
			MaskBGBottom = Support.UnicolorSprite("black", 0, 0, 0, 255);
			MaskBGBottom.Position = new Vector2(351.0f, 32.0f);
			MaskBGBottom.Scale = new Vector2(16.0f, 4.9375f);
			this.AddChild(MaskBGBottom);
			
			MenuBGSpacer3 = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_blue2.png");
//			MenuBGSpacer3.Position = new Vector2(351.0f, 174.0f);
			MenuBGSpacer3.Position = new Vector2(351.0f, 111.0f);
			MenuBGSpacer3.FlipU = true;
			this.AddChild(MenuBGSpacer3);
			
			MaskBGSpacer3 = Support.UnicolorSprite("black", 0, 0, 0, 255);
			MaskBGSpacer3.Position = new Vector2(351.0f, 112.0f);
			MaskBGSpacer3.Scale = new Vector2(16.2f, 9.4375f);
			this.AddChild(MaskBGSpacer3);
			
			MenuBGSpacer2 = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_red2.png");
//			MenuBGSpacer2.Position = new Vector2(351.0f, 255.0f);
			MenuBGSpacer2.Position = new Vector2(351.0f, 192.0f);
			MenuBGSpacer2.FlipU = true;
			this.AddChild(MenuBGSpacer2);
			
			MaskBGSpacer2 = Support.UnicolorSprite("black", 0, 0, 0, 255);
			MaskBGSpacer2.Position = new Vector2(351.0f, 194.0f);
			MaskBGSpacer2.Scale = new Vector2(16.2f, 9.4375f);
			this.AddChild(MaskBGSpacer2);
			
			MenuBGSpacer1 = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_blue2.png");
//			MenuBGSpacer1.Position = new Vector2(351.0f, 342.0f);
			MenuBGSpacer1.Position = new Vector2(351.0f, 279.0f);
			MenuBGSpacer1.FlipU = true;
			this.AddChild(MenuBGSpacer1);
			
			MaskBGSpacer1 = Support.UnicolorSprite("black", 0, 0, 0, 255);
			MaskBGSpacer1.Position = new Vector2(351.0f, 281.0f);
			MaskBGSpacer1.Scale = new Vector2(16.2f, 9.4375f);
			this.AddChild(MaskBGSpacer1);
			
			MenuBGTop = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_top.png");
			MenuBGTop.Position = new Vector2(351.0f, 423.0f);
			this.AddChild(MenuBGTop);
			
			MaskBGTop = Support.UnicolorSprite("black", 0, 0, 0, 255);
			MaskBGTop.Position = new Vector2(351.0f, 423.0f);
			MaskBGTop.Scale = new Vector2(16.0f, 4.875f);
			this.AddChild(MaskBGTop);
			
			
			
			
			
			NewGameButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/NewGameButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			NewGameButton.setPosition(480.0f, 393.0f);
			NewGameButton.setPosition(223.0f, 396.0f);
			NewGameButton.on = true;
			this.AddChild(NewGameButton.getNode());
			
			LevelSelectButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/LevelSelectButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			LevelSelectButton.setPosition (223.0f, 312.0f);
			LevelSelectButton.on = true;
			this.AddChild(LevelSelectButton.getNode());
			
			InstructionsButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/InstructionsButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			InstructionsButton.setPosition(223.0f, 228.0f);
			InstructionsButton.on = true;
			this.AddChild(InstructionsButton.getNode());
			
			CreditsButton = new ButtonEntity(" ", MenuSystem, GamePhysics.Instance, Support.TiledSpriteFromFile("/Application/assets/images/UI/CreditsButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			CreditsButton.setPosition(223.0f, 143.0f);
			CreditsButton.on = true;
			this.AddChild(CreditsButton.getNode());
			
			
			
			MaskBGButton1 = Support.UnicolorSprite("black", 0, 0, 0, 255);
//			MaskBGButton1.Position = new Vector2(351.0f, 362.0f);
			MaskBGButton1.Position = new Vector2(94.0f, 365.0f);
			MaskBGButton1.Scale = new Vector2(16.1f, 4.0f);
			this.AddChild(MaskBGButton1);
			
			MaskBGButton2 = Support.UnicolorSprite("black", 0, 0, 0, 255);
			MaskBGButton2.Position = new Vector2(94.0f, 281.0f);
			MaskBGButton2.Scale = new Vector2(16.1f, 4.0f);
			this.AddChild(MaskBGButton2);
			
			MaskBGButton3 = Support.UnicolorSprite("black", 0, 0, 0, 255);
			MaskBGButton3.Position = new Vector2(94.0f, 197.0f);
			MaskBGButton3.Scale = new Vector2(16.1f, 4.0f);
			this.AddChild(MaskBGButton3);
			
			MaskBGButton4 = Support.UnicolorSprite("black", 0, 0, 0, 255);
			MaskBGButton4.Position = new Vector2(94.0f, 112.0f);
			MaskBGButton4.Scale = new Vector2(16.1f, 4.0f);
			this.AddChild(MaskBGButton4);
			
//			var speed = 2000.0f;
//			
//			
//			Sequence sequence = new Sequence();
//			var delay0 = 0.5f;
//			sequence.Add( new CallFunc( () => { InputManager.Instance.enabled = false; } ) );
//			sequence.Add( new DelayTime( delay0 ) );
//			var duration1 = 200.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(0.0f, -78.0f), duration1) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGTop.Visible = false; } ) );
//			MaskBGTop.RunAction(sequence);
//			
//			sequence = new Sequence();
//			var delay1 = delay0 + duration1;
//			sequence.Add( new DelayTime( delay1 ) );
//			var duration2 = 257.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(257.0f, 0.0f), duration2) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGButton1.Visible = false; } ) );
////			MaskBGButton1.RunAction(sequence);
//			NewGameButton.getNode().RunAction(sequence);
//			
//			sequence = new Sequence();
//			var delay2 = delay1 + duration2;
//			sequence.Add( new DelayTime( delay2 ) );
//			var duration3 = 200.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(0.0f, -151.0f), duration3) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGSpacer1.Visible = false; } ) );
//			MaskBGSpacer1.RunAction(sequence);
//			
//			sequence = new Sequence();
//			var delay3 = delay2 + duration3;
//			sequence.Add( new DelayTime( delay3 ) );
//			var duration4 = 257.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(257.0f, 0.0f), duration4) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGButton2.Visible = false; } ) );
////			MaskBGButton2.RunAction(sequence);
//			LevelSelectButton.getNode().RunAction(sequence);
//			
//			sequence = new Sequence();
//			var delay4 = delay3 + duration4;
//			sequence.Add( new DelayTime( delay4 ) );
//			var duration5 = 200.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(0.0f, -151.0f), duration5) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGSpacer2.Visible = false; } ) );
//			MaskBGSpacer2.RunAction(sequence);
//			
//			sequence = new Sequence();
//			var delay5 = delay4 + duration5;
//			sequence.Add( new DelayTime( delay5 ) );
//			var duration6 = 257.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(257.0f, 0.0f), duration6 ) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGButton3.Visible = false; } ) );
////			MaskBGButton3.RunAction(sequence);
//			InstructionsButton.getNode().RunAction(sequence);
//			
//			sequence = new Sequence();
//			var delay6 = delay5 + duration6;
//			sequence.Add( new DelayTime( delay6 ) );
//			var duration7 = 200.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(0.0f, -151.0f), duration7) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGSpacer3.Visible = false; } ) );
//			MaskBGSpacer3.RunAction(sequence);
//			
//			sequence = new Sequence();
//			var delay7 = delay6 + duration7;
//			sequence.Add( new DelayTime( delay7 ) );
//			var duration8 = 257.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(257.0f, 0.0f), duration8) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGButton4.Visible = false; } ) );
////			MaskBGButton4.RunAction(sequence);
//			CreditsButton.getNode().RunAction(sequence);
//			
//			sequence = new Sequence();
//			var delay8 = delay7 + duration8;
//			sequence.Add( new DelayTime( delay8 ) );
//			var duration9 = 200.0f/speed;
//			sequence.Add( new MoveBy( new Vector2(0.0f, -79.0f), duration9) {Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear} );
//			sequence.Add( new CallFunc( () => { MaskBGBottom.Visible = false; } ) );
//			sequence.Add( new CallFunc( () => { InputManager.Instance.enabled = true; } ) );
//			MaskBGBottom.RunAction(sequence);
			
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
//			MenuSystem.SetScreen("Level Select");
			Director.Instance.ReplaceScene(new LoadingScene(0, false, "Level Select") );
		}
		
		void HandleCreditsButtonButtonUpAction (object sender, EventArgs e) {
#if DEBUG
			Console.WriteLine("Credits");
#endif
//			MenuSystem.SetScreen("Credits");
			MenuSystem.SetScreen("Options");
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
			
			var speed = 4000.0f;
			
			
			Sequence sequence = new Sequence();
			var delay0 = 0.25f;
			sequence.Add( new CallFunc( () => { InputManager.Instance.enabled = false; } ) );
			sequence.Add( new DelayTime( delay0 ) );
			var duration1 = 78.0f/speed * 4.0f;
			sequence.Add( new CallFunc( () => {
				MaskBGTop.RunAction( new MoveBy( new Vector2(0.0f, -78.0f), duration1) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration1 ) );
			sequence.Add( new CallFunc( () => { MaskBGTop.Visible = false; } ) );
//			MaskBGTop.RunAction(sequence);
			
//			sequence = new Sequence();
//			var delay1 = delay0 + duration1;
//			var delay1 = duration1;
//			sequence.Add( new DelayTime( delay1 ) );
			var duration2 = 257.0f/speed;
			sequence.Add( new CallFunc( () => {
				NewGameButton.getNode().RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration2) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration2 ) );
			sequence.Add( new CallFunc( () => { MaskBGButton1.Visible = false; } ) );
//			NewGameButton.getNode().RunAction(sequence);
			
//			sequence = new Sequence();
//			var delay2 = delay1 + duration2;
//			var delay2 = duration2;
//			sequence.Add( new DelayTime( delay2 ) );
			var duration3 = 151.0f/speed * 2.0f;
			sequence.Add( new CallFunc( () => {
				MaskBGSpacer1.RunAction( new MoveBy( new Vector2(0.0f, -151.0f), duration3) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration3 ) );
			sequence.Add( new CallFunc( () => { MaskBGSpacer1.Visible = false; } ) );
//			MaskBGSpacer1.RunAction(sequence);
			
//			sequence = new Sequence();
//			var delay3 = delay2 + duration3;
//			var delay3 = duration3;
//			sequence.Add( new DelayTime( delay3 ) );
			var duration4 = 257.0f/speed;
			sequence.Add( new CallFunc( () => {
				LevelSelectButton.getNode().RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration4) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration4 ) );
			sequence.Add( new CallFunc( () => { MaskBGButton2.Visible = false; } ) );
//			MaskBGButton2.RunAction(sequence);
//			LevelSelectButton.getNode().RunAction(sequence);
			
//			sequence = new Sequence();
//			var delay4 = delay3 + duration4;
//			var delay4 = duration4;
//			sequence.Add( new DelayTime( delay4 ) );
			var duration5 = 151.0f/speed * 2.0f;
			sequence.Add( new CallFunc( () => { 
				MaskBGSpacer2.RunAction( new MoveBy( new Vector2(0.0f, -151.0f), duration5) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration5 ) );
			sequence.Add( new CallFunc( () => { MaskBGSpacer2.Visible = false; } ) );
//			MaskBGSpacer2.RunAction(sequence);
			
//			sequence = new Sequence();
//			var delay5 = delay4 + duration5;
//			var delay5 = duration5;
//			sequence.Add( new DelayTime( delay5 ) );
			var duration6 = 257.0f/speed;
			sequence.Add( new CallFunc( () => {
				InstructionsButton.getNode().RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration6 ) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration6 ) );
			sequence.Add( new CallFunc( () => { MaskBGButton3.Visible = false; } ) );
//			InstructionsButton.getNode().RunAction(sequence);
			
//			sequence = new Sequence();
//			var delay6 = delay5 + duration6;
//			var delay6 = duration6;
//			sequence.Add( new DelayTime( delay6 ) );
			var duration7 = 151.0f/speed * 2.0f;
			sequence.Add( new CallFunc( () => {
				MaskBGSpacer3.RunAction( new MoveBy( new Vector2(0.0f, -151.0f), duration7) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration7 ) );
			sequence.Add( new CallFunc( () => { MaskBGSpacer3.Visible = false; } ) );
//			MaskBGSpacer3.RunAction(sequence);
			
//			sequence = new Sequence();
//			var delay7 = delay6 + duration7;
//			var delay7 = duration7;
//			sequence.Add( new DelayTime( delay7 ) );
			var duration8 = 257.0f/speed;
			sequence.Add( new CallFunc( () => {
				CreditsButton.getNode().RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration8) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration8 ) );
			sequence.Add( new CallFunc( () => { MaskBGButton4.Visible = false; } ) );
//			CreditsButton.getNode().RunAction(sequence);
			
//			sequence = new Sequence();
//			var delay8 = delay7 + duration8;
//			var delay8 = duration8;
//			sequence.Add( new DelayTime( delay8 ) );
			var duration9 = 79.0f/speed * 4.0f;
			sequence.Add( new CallFunc( () => {
				MaskBGBottom.RunAction( new MoveBy( new Vector2(0.0f, -79.0f), duration9) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration9 ) );
			sequence.Add( new CallFunc( () => { MaskBGBottom.Visible = false; } ) );
			sequence.Add( new CallFunc( () => { InputManager.Instance.enabled = true; } ) );
//			MaskBGBottom.RunAction(sequence);
			
			this.RunAction(sequence);
			
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
//			MenuBackground = null;
			MenuBGBottom = null;
			MenuBGSpacer1 = null;
			MenuBGSpacer2 = null;
			MenuBGSpacer3 = null;
			MenuBGTop = null;
			MaskBGBottom = null;
			MaskBGButton1 = null;
			MaskBGButton2 = null;
			MaskBGButton3 = null;
			MaskBGButton4 = null;
			MaskBGSpacer1 = null;
			MaskBGSpacer2 = null;
			MaskBGSpacer3 = null;
			MaskBGTop = null;
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

