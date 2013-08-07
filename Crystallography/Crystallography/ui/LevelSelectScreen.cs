using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class LevelSelectScreen : Layer
	{
		MenuSystemScene MenuSystem;
		
		List<Node> Panels;
		SwipePanels SwipePanels;
		LevelSelectIndicator Indicator;
		
		Label LevelSelectTitleText;
		Label LevelSelectInstructionsText;
		Label LevelNumberText;
		Label CompletionPercentageText;

		SpriteTile BlackBlock1;
		SpriteTile BlackBlock2;
		
		ButtonEntity BackButton;
		ButtonEntity PlayButton;
		
		int SelectedLevel;
		
		// CONSTRUCTOR ----------------------------------------------------------------------------------------------------------------------
		
		public LevelSelectScreen (MenuSystemScene pMenuSystem) {
			SelectedLevel = 0;
			MenuSystem = pMenuSystem;
			
			var pages = FMath.Ceiling(GameScene.TOTAL_LEVELS / (float)LevelPage.ITEMS_PER_PAGE);
			
			Panels = new List<Node>{};
			
			for (int i=0; i < pages; i++) {
				Panels.Add( new LevelPage(i) );
				(Panels[i] as LevelPage).Disable();
			}
			(Panels[0] as LevelPage).Enable();
			
			this.SwipePanels = new SwipePanels(Panels) {
				Width = 457.0f,
				Position = new Vector2(95.0f,46.0f)
			};
			this.AddChild(this.SwipePanels);
			
			foreach( LevelPage panel in Panels) {
				this.AddChild(panel);
			}
			
			Indicator = new LevelSelectIndicator();
			(Panels[0] as LevelPage).Items[0].AddChild(Indicator);
			
			// BLACK MASKS TO HIDE MORE LEVELS BEHIND
			BlackBlock1 = Support.UnicolorSprite("BlackBlock", 0,0,0,255);
			BlackBlock1.Scale = new Vector2(28.5625f, 24.75f);
			BlackBlock1.Position = new Vector2(588.0f, 46.0f);
			this.AddChild(BlackBlock1);
			BlackBlock2 = Support.UnicolorSprite("BlackBlock", 0,0,0,255);
			BlackBlock2.Position = new Vector2(-397.0f, 46.0f);
			BlackBlock2.Scale = BlackBlock1.Scale;
			this.AddChild (BlackBlock2);
			
			LevelNumberText = new Label(){
				Text = SelectedLevel.ToString("00"),
				Position = new Vector2(638.0f, 312.0f),
				FontMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 102, "Bold") ),
				Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f)
			};
			CenterText(LevelNumberText);
			this.AddChild(LevelNumberText);
			
			CompletionPercentageText = new Label() {
				Text = (1.0f).ToString("P0"),
				Position = new Vector2(638.0f, 280.0f),
				FontMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
				Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f)
			};
			CenterText(CompletionPercentageText);
			this.AddChild(CompletionPercentageText);
			
			LevelSelectTitleText = new Label(){
				Text="select a level",
				Position = new Vector2(60.0f, 488.0f),
				FontMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Regular") )
			};
			this.AddChild(LevelSelectTitleText);
			
			LevelSelectInstructionsText = new Label(){
				Text="select a cube and then press play.",
				Position = new Vector2(60.0f, 465.0f),
				FontMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Regular") )
			};
			this.AddChild(LevelSelectInstructionsText);
			
			BackButton = new ButtonEntity("", MenuSystem, null, Support.TiledSpriteFromFile("Application/assets/images/levelBackBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			BackButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			BackButton.setPosition(776.0f, 105.0f);
			this.AddChild(BackButton.getNode());
			
			PlayButton = new ButtonEntity("", MenuSystem, null, Support.TiledSpriteFromFile("Application/assets/images/levelPlayBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			PlayButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			PlayButton.setPosition(776.0f, 35.0f);
			this.AddChild(PlayButton.getNode());
		}
		
		// EVENT HANDLERS -------------------------------------------------------------------------------------------------------------------
		
		void HandleBackButtonButtonUpAction (object sender, EventArgs e)
		{
#if DEBUG
			Console.WriteLine("Back");
#endif
			MenuSystem.SetScreen("Menu");
		}
		
		void HandlePlayButtonButtonUpAction (object sender, EventArgs e)
		{
#if DEBUG
			Console.WriteLine("Play: {0}", SelectedLevel);
#endif
			Director.Instance.ReplaceScene(new LoadingScene(SelectedLevel, false) );
		}
		
		void HandleLevelSelectItemLevelSelectionDetected (object sender, LevelSelectionEventArgs e)
		{
			Indicator.Parent.RemoveChild(Indicator, false);
			(sender as LevelSelectItem).AddChild(Indicator);
			SelectedLevel = e.ID;
			
			LevelManager.Instance.GetSolutions( SelectedLevel );
			var previousSolutions = DataStorage.puzzleSolutionsFound[ SelectedLevel ];
			var completion = ((float)previousSolutions.Count / (float)LevelManager.Instance.PossibleSolutions);
			
			CompletionPercentageText.Text = completion.ToString("P0") + " solved.";
			LevelNumberText.Text = SelectedLevel.ToString("00");
			CenterText(LevelNumberText);
			CenterText(CompletionPercentageText);
			
		}
		
		// OVERRIDES ----------------------------------------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			LevelSelectItem.LevelSelectionDetected += HandleLevelSelectItemLevelSelectionDetected;
			BackButton.ButtonUpAction += HandleBackButtonButtonUpAction;
			PlayButton.ButtonUpAction += HandlePlayButtonButtonUpAction;
			this.SwipePanels.OnSwipeComplete += HandleSwipePanelshandleOnSwipeComplete;
			
			// Set initial state to level 00
			(Panels[0] as LevelPage).Items[0].Button.FakePress();
		}

		void HandleSwipePanelshandleOnSwipeComplete (object sender, EventArgs e)
		{
			if (this.SwipePanels.LeftPage != null) {
				(this.SwipePanels.LeftPage as LevelPage).Disable();
			}
			if (this.SwipePanels.RightPage != null) {
				(this.SwipePanels.RightPage as LevelPage).Disable();
			}
			(this.SwipePanels.ActivePage as LevelPage).Enable();
		}
		
		public override void OnExit ()
		{
			LevelSelectItem.LevelSelectionDetected -= HandleLevelSelectItemLevelSelectionDetected;
			BackButton.ButtonUpAction -= HandleBackButtonButtonUpAction;
			PlayButton.ButtonUpAction -= HandlePlayButtonButtonUpAction;
			this.SwipePanels.OnSwipeComplete -= HandleSwipePanelshandleOnSwipeComplete;
			
			base.OnExit ();
			
			MenuSystem = null;
			Panels.Clear();
			this.SwipePanels = null;
			LevelSelectTitleText = null;
			LevelSelectInstructionsText = null;

			BlackBlock1 = null;
			BlackBlock2 = null;
			
			BackButton = null;
			PlayButton = null;
		}
		
		// METHODS ------------------------------------------------------------------------------------------------------------------------------
		
		protected void CenterText( Label pLabel ) {
			
			var textWidth = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", (int)pLabel.FontMap.CharPixelHeight, "Bold").GetTextWidth(pLabel.Text);
			pLabel.Position = new Vector2(593 + 0.5f * (367 - textWidth), pLabel.Position.Y);
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~LevelSelectScreen() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	// HELPER CLASSES =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
	
	public class LevelPage : Node {
		public static readonly int ITEMS_PER_PAGE = 12;
//		static float Width = 567.0f;
		static float Width = 457.0f;
		static float Height = 396.0f;
		public List<LevelSelectItem> Items {get; private set;}
		
		public LevelPage( int pPageNumber ) {
			Items = new List<LevelSelectItem>();
			int baseIndex = pPageNumber * ITEMS_PER_PAGE;
			int buttonCount = ITEMS_PER_PAGE;
			if ( GameScene.TOTAL_LEVELS - 1 < baseIndex + ITEMS_PER_PAGE - 1 ){
				buttonCount = GameScene.TOTAL_LEVELS - 1 - baseIndex;
#if DEBUG
				Console.WriteLine(baseIndex + "/" + GameScene.TOTAL_LEVELS);
#endif
			}
			// HACK -- THIS ENSURES THAT QColor HAS BEEN INITIALIZED, BUT IT'S KIND OF DUMB THAT WE HAVE TO DO THAT. MAYBE JUST HARD CODE THE COLORS HERE?
			var temp = QColor.Instance.allDifferentScore;
			for ( int i=0; i < buttonCount; i++ ) {
				LevelSelectItem item = new LevelSelectItem();
				item.LevelID = i + baseIndex;
				item.Position = new Vector2(0.125f*Width + 0.25f*Width*(i%4), Height - 0.167f*Height - 0.333f*Height*((i-(i%4))/4));
				(item.Button.getNode() as SpriteTile).Color = QColor.palette[i%3];
				Items.Add(item);
				this.AddChild(item);
			}
		}
		
		// OVERRIDES ------------------------------------------------------------------------------------------------------------------------------
		
		public override void OnExit ()
		{
			base.OnExit ();
			Items.Clear();
		}
		
		// METHODS --------------------------------------------------------------------------------------------------------------------------------
		
		public void Enable() {
			foreach (LevelSelectItem item in Items ) {
				item.Button.on = true;
			}
		}
		
		public void Disable() {
			foreach (LevelSelectItem item in Items ) {
				item.Button.on = false;
			}
		}
		
		// DESTRUCTOR -----------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~LevelPage() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	public class LevelSelectItem : Node {
		public int LevelID {get; set;}
		public Vector4 Color {get; set;}
		public ButtonEntity Button {get; private set;}
		public static event EventHandler<LevelSelectionEventArgs> LevelSelectionDetected;
		
		public LevelSelectItem() {
			Button = new ButtonEntity("", Director.Instance.CurrentScene, null, Support.TiledSpriteFromFile("Application/assets/images/UI/LevelSelectItemButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
			Button.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			this.AddChild(Button.getNode());
			
			Button.ButtonUpAction += HandleButtonButtonUpAction;
		}

		void HandleButtonButtonUpAction (object sender, EventArgs e)
		{
			EventHandler<LevelSelectionEventArgs> handler = LevelSelectionDetected;
			if ( handler != null ) {
				handler( this, new LevelSelectionEventArgs() { ID = this.LevelID });
			}
		}
		
		public override void OnExit ()
		{
			Button.ButtonUpAction -= HandleButtonButtonUpAction;
			base.OnExit ();
			Button = null;
		}
#if DEBUG
		~LevelSelectItem() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	public class LevelSelectIndicator : Node {
		public LevelSelectIndicator() {
			var img = Support.SpriteFromFile("Application/assets/images/UI/LevelSelectIndicator.png");
			img.Position = -0.5f*img.CalcSizeInPixels();
			AddChild(img);
		}
	}
	
	public class LevelSelectionEventArgs : EventArgs {
		public int ID;
	}
}

