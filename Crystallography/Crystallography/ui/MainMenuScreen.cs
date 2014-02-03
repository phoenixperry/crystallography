using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class MainMenuScreen : Layer
	{
		MenuSystemScene MenuSystem;
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
		
		BetterButton NewGameButton;
		BetterButton LevelSelectButton;
		BetterButton CreditsButton;
		BetterButton InstructionsButton;
#if METRICS
		BetterButton PrintAnalyticsButton;
		BetterButton ClearAnalyticsButton;
		float HoldTimer;
#endif
		
		// CONSTRUCTORS -----------------------------------------------------------------------------------------
		
		public MainMenuScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			var bgcolor = Director.Instance.GL.Context.GetClearColor();
			bgcolor.A = 1.0f;
			
			MenuBGBottom = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_bottom.png");
			MenuBGBottom.Position = new Vector2(351.0f, 32.0f);
			this.AddChild(MenuBGBottom);
			
			MaskBGBottom = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGBottom.Color = bgcolor;
			MaskBGBottom.Position = new Vector2(351.0f, 32.0f);
			MaskBGBottom.Scale = new Vector2(16.0f, 4.9375f);
			this.AddChild(MaskBGBottom);
			
			MenuBGSpacer3 = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_blue2.png");
			MenuBGSpacer3.Position = new Vector2(351.0f, 111.0f);
			MenuBGSpacer3.FlipU = true;
			this.AddChild(MenuBGSpacer3);
			
			MaskBGSpacer3 = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGSpacer3.Color = bgcolor;
			MaskBGSpacer3.Position = new Vector2(351.0f, 112.0f);
			MaskBGSpacer3.Scale = new Vector2(16.2f, 9.4375f);
			this.AddChild(MaskBGSpacer3);
			
			MenuBGSpacer2 = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_red2.png");
			MenuBGSpacer2.Position = new Vector2(351.0f, 192.0f);
			MenuBGSpacer2.FlipU = true;
			this.AddChild(MenuBGSpacer2);
			
			MaskBGSpacer2 = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGSpacer2.Color = bgcolor;;
			MaskBGSpacer2.Position = new Vector2(351.0f, 194.0f);
			MaskBGSpacer2.Scale = new Vector2(16.2f, 9.4375f);
			this.AddChild(MaskBGSpacer2);
			
			MenuBGSpacer1 = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_blue2.png");
			MenuBGSpacer1.Position = new Vector2(351.0f, 279.0f);
			MenuBGSpacer1.FlipU = true;
			this.AddChild(MenuBGSpacer1);
			
			MaskBGSpacer1 = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGSpacer1.Color = bgcolor;
			MaskBGSpacer1.Position = new Vector2(351.0f, 281.0f);
			MaskBGSpacer1.Scale = new Vector2(16.2f, 9.4375f);
			this.AddChild(MaskBGSpacer1);
			
			MenuBGTop = Support.SpriteFromFile("/Application/assets/images/UI/menuBtnBG_top.png");
			MenuBGTop.Position = new Vector2(351.0f, 423.0f);
			this.AddChild(MenuBGTop);
			
			MaskBGTop = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGTop.Color = bgcolor;
			MaskBGTop.Position = new Vector2(351.0f, 423.0f);
			MaskBGTop.Scale = new Vector2(16.0f, 4.875f);
			this.AddChild(MaskBGTop);
			
			
			NewGameButton = new BetterButton(256.0f, 64.0f) {
				Text = "puzzle mode",
				Position = new Vector2(94.0f, 365.0f),
//				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f),
			};
			NewGameButton.background.RegisterPalette(1);
			this.AddChild(NewGameButton);
			
			LevelSelectButton = new BetterButton(256.0f, 64.0f) {
				Text = "puzzle select",
				Position = new Vector2(94.0f, 281.0f),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			LevelSelectButton.background.RegisterPalette(2);
			this.AddChild(LevelSelectButton);
			
			InstructionsButton = new BetterButton(256.0f, 64.0f) {
				Text = "challenge mode",
				Position = new Vector2(94.0f, 197.0f),
//				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			InstructionsButton.background.RegisterPalette(1);
			this.AddChild(InstructionsButton);
			
			CreditsButton = new BetterButton(256.0f, 64.0f) {
				Text = "options",
				Position = new Vector2(94.0f, 112.0f),
//				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			CreditsButton.background.RegisterPalette(2);
			this.AddChild(CreditsButton);
			
			MaskBGButton1 = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGButton1.Color = bgcolor;
			MaskBGButton1.Position = new Vector2(94.0f, 365.0f);
			MaskBGButton1.Scale = new Vector2(16.1f, 4.0f);
			this.AddChild(MaskBGButton1);
			
			MaskBGButton2 = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGButton2.Color = bgcolor;
			MaskBGButton2.Position = new Vector2(94.0f, 281.0f);
			MaskBGButton2.Scale = new Vector2(16.1f, 4.0f);
			this.AddChild(MaskBGButton2);
			
			MaskBGButton3 = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGButton3.Color = bgcolor;
			MaskBGButton3.Position = new Vector2(94.0f, 197.0f);
			MaskBGButton3.Scale = new Vector2(16.1f, 4.0f);
			this.AddChild(MaskBGButton3);
			
			MaskBGButton4 = Support.UnicolorSprite("white", 255, 255, 255, 255);
			MaskBGButton4.Color = bgcolor;
			MaskBGButton4.Position = new Vector2(94.0f, 112.0f);
			MaskBGButton4.Scale = new Vector2(16.1f, 4.0f);
			this.AddChild(MaskBGButton4);
			
#if METRICS
			HoldTimer = 0.0f;
			
			PrintAnalyticsButton = new BetterButton(256.0f, 64.0f) {
				Text = "print metrics",
				Position = new Vector2(704.0f, 281.0f),
				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f),
				On = false
			}
			
			ClearAnalyticsButton = new BetterButton(256.0f, 64.0f) {
				Text = "clear metrics",
				Position = new Vector2(704.0f, 281.0f),
				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f),
				On = false
			}
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
			MenuSystem.SetScreen("Infinite Mode");
//			Director.Instance.ReplaceScene(new LoadingScene(999, false) );
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
			
			var duration2 = 257.0f/speed;
			sequence.Add( new CallFunc( () => {
//				NewGameButton.getNode().RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration2) {
				NewGameButton.RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration2) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration2 ) );
			sequence.Add( new CallFunc( () => { MaskBGButton1.Visible = false; } ) );
			
			var duration3 = 151.0f/speed * 2.0f;
			sequence.Add( new CallFunc( () => {
				MaskBGSpacer1.RunAction( new MoveBy( new Vector2(0.0f, -151.0f), duration3) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration3 ) );
			sequence.Add( new CallFunc( () => { MaskBGSpacer1.Visible = false; } ) );
			
			var duration4 = 257.0f/speed;
			sequence.Add( new CallFunc( () => {
//				LevelSelectButton.getNode().RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration4) {
				LevelSelectButton.RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration4) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration4 ) );
			sequence.Add( new CallFunc( () => { MaskBGButton2.Visible = false; } ) );
			
			var duration5 = 151.0f/speed * 2.0f;
			sequence.Add( new CallFunc( () => { 
				MaskBGSpacer2.RunAction( new MoveBy( new Vector2(0.0f, -151.0f), duration5) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration5 ) );
			sequence.Add( new CallFunc( () => { MaskBGSpacer2.Visible = false; } ) );
			
			var duration6 = 257.0f/speed;
			sequence.Add( new CallFunc( () => {
//				InstructionsButton.getNode().RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration6 ) {
				InstructionsButton.RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration6 ) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration6 ) );
			sequence.Add( new CallFunc( () => { MaskBGButton3.Visible = false; } ) );
			
			var duration7 = 151.0f/speed * 2.0f;
			sequence.Add( new CallFunc( () => {
				MaskBGSpacer3.RunAction( new MoveBy( new Vector2(0.0f, -151.0f), duration7) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration7 ) );
			sequence.Add( new CallFunc( () => { MaskBGSpacer3.Visible = false; } ) );
			
			var duration8 = 257.0f/speed;
			sequence.Add( new CallFunc( () => {
//				CreditsButton.getNode().RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration8) {
				CreditsButton.RunAction( new MoveBy( new Vector2(257.0f, 0.0f), duration8) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration8 ) );
			sequence.Add( new CallFunc( () => { MaskBGButton4.Visible = false; } ) );
			
			var duration9 = 79.0f/speed * 4.0f;
			sequence.Add( new CallFunc( () => {
				MaskBGBottom.RunAction( new MoveBy( new Vector2(0.0f, -79.0f), duration9) {
					Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
				} );
			} ) );
			sequence.Add( new DelayTime( duration9 ) );
			sequence.Add( new CallFunc( () => { MaskBGBottom.Visible = false; } ) );
			sequence.Add( new CallFunc( () => { InputManager.Instance.enabled = true; } ) );
			
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
					PrintAnalyticsButton.On = !PrintAnalyticsButton.On;
					ClearAnalyticsButton.On = !ClearAnalyticsButton.On;
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

