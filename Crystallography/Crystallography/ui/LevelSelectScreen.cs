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
		SpriteTile[] Icons;
		List<SolutionIcon> Solutions;
		
//		ButtonEntity BackButton;
//		ButtonEntity PlayButton;
		BetterButton BackButton;
		BetterButton PlayButton;
		
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
				Position = new Vector2(638.0f, 390.0f),
				FontMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 102, "Bold") ),
//				Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f)
			};
			CenterText(LevelNumberText);
			this.AddChild(LevelNumberText);
			
			CompletionPercentageText = new Label() {
				Text = (1.0f).ToString("P0"),
				Position = new Vector2(638.0f, 150.0f),
				FontMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
//				Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f)
			};
			CenterText(CompletionPercentageText);
			this.AddChild(CompletionPercentageText);
			
			Icons = new SpriteTile[4];
			for( int i=0; i < Icons.Length; i++) {
				Icons[i] = Support.TiledSpriteFromFile("/Application/assets/images/icons/icons.png", 4, 2);
				Icons[i].Visible = false;
				Icons[i].Position = new Vector2(638.0f + 68 * i, 340.0f);
				this.AddChild(Icons[i]);
			}
			
			Solutions = new List<SolutionIcon>() {
				new SolutionIcon(){
					CubeText = "99",
					ScoreText = "88"
				},
				new SolutionIcon(){
					CubeText = "99",
					ScoreText = "88"
				},
				new SolutionIcon(){
					CubeText = "99",
					ScoreText = "88"
				},
				new SolutionIcon(){
					CubeText = "99",
					ScoreText = "88"
				},
				new SolutionIcon(){
					CubeText = "99",
					ScoreText = "88"
				}
			};
			for( int i=0; i < Solutions.Count; i++) {
				Solutions[i].Visible = false;
				var column = i % 4;
				var row = ( i - column ) / 4;
				Solutions[i].Position = new Vector2(638.0f + 60.0f * column, 270.0f - row * 55);
				this.AddChild(Solutions[i]);
			}
			
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
			
			BackButton = new BetterButton(369.0f, 64.0f) {
				Text = "back",
				Position = new Vector2(591.0f, 74.0f),
				Color = new Vector4(0.1608f, 0.8863f, 0.8863f, 1.0f)
			};
			this.AddChild(BackButton);
			
			PlayButton = new BetterButton(369.0f, 64.0f) {
				Text = "play",
				Position = new Vector2(591.0f, 0.0f),
				Color = new Vector4(0.8980f, 0.0745f, 0.0745f, 1.0f)
			};
			this.AddChild(PlayButton);
			
//			BackButton = new ButtonEntity("", MenuSystem, null, Support.TiledSpriteFromFile("Application/assets/images/levelBackBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			BackButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
//			BackButton.setPosition(776.0f, 105.0f);
//			this.AddChild(BackButton.getNode());
//			
//			PlayButton = new ButtonEntity("", MenuSystem, null, Support.TiledSpriteFromFile("Application/assets/images/levelPlayBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			PlayButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
//			PlayButton.setPosition(776.0f, 35.0f);
//			this.AddChild(PlayButton.getNode());
		}
		
		// EVENT HANDLERS -------------------------------------------------------------------------------------------------------------------
		
		void HandleBackButtonButtonUpAction (object sender, EventArgs e)
		{
#if DEBUG
			Console.WriteLine("Back");
#endif
//			MenuSystem.SetScreen("Menu");
			Director.Instance.ReplaceScene( new LoadingScene(0, false, "Menu") );
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
			
			
			// HACK THIS IS SUPER INEFFICIENT!!!!!!!!!!!!!
			while( LevelManager.Instance.PossibleSolutions > Solutions.Count) {
				Solutions.Add (
					new SolutionIcon(){
					CubeText = "99",
					ScoreText = "88",
				});
			}
			foreach(SolutionIcon s in Solutions){
				s.Visible = false;
			}
			
			int i = 0;
			foreach( int cube in LevelManager.Instance.goalDict.Keys ) {
				foreach ( int points in LevelManager.Instance.goalDict[cube] ) {
					Solutions[i].CubeText = cube.ToString();
					Solutions[i].ScoreText = points.ToString();
					Solutions[i].Color = Vector4.Zero;
					foreach( var ps in previousSolutions) {
						if ( ps[0] == cube && ps[1] == points ) {
							Solutions[i].Color = new Vector4( 0.16078431f, 0.88627451f, 0.88627451f, 1.0f);
							break;
						}
					}
					if ( Solutions[i].Color == Vector4.Zero ) {
						Solutions[i].Color = Sce.PlayStation.HighLevel.GameEngine2D.Base.Colors.Grey50;
					}
					Solutions[i].Visible = true;
					i++;
				}
			}
			
			CompletionPercentageText.Text = completion.ToString("P0") + " solved.";
			LevelNumberText.Text = SelectedLevel.ToString("00");
			CenterText(LevelNumberText);
			CenterText(CompletionPercentageText);
			
			
			// HACK THIS IS ALL SUPER INEFFICIENT!!!!!!!
			QualityManager.Instance.ClearQualityDictionary();
			QualityManager.Instance.LoadLevelQualities( SelectedLevel );
			QualityManager.Instance.BuildQualityDictionary();
			
			var names = new string[4];
			int j = 0;
			foreach (SpriteBase icon in Icons) {
				icon.Visible = false;
			}
			foreach (string name in QualityManager.Instance.qualityDict.Keys) {
				if (name == "QOrientation") continue;
				var variations = QualityManager.Instance.qualityDict[name];
				if( variations[0] != null && variations[1] != null && variations[2] != null ) {
					names[j] = name.Substring(1);
					Icons[j].TileIndex1D = (int)EnumHelper.FromString<Crystallography.Icons>(names[j]);
					Icons[j].Visible = true;
					j++;
				}
			}
			
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
			(Panels[0] as LevelPage).Items[0].FakePress();
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
			
			for (int i=0; i < Icons.Length; i++){
				Icons[i] = null;
			}
			Icons = null;
			Solutions.Clear();
			Solutions = null;
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
				int column = i%4;
				int row = (i - column)/4;
				bool complete = DataStorage.puzzleComplete[i + baseIndex];
#if UNLOCK_ALL
				bool locked = false;
#else
				bool locked = DataStorage.puzzleLocked[i + baseIndex];
#endif
				LevelSelectItem item = new LevelSelectItem( complete, locked ) {
					levelID = i + baseIndex,
					Position = new Vector2(0.125f*Width + 0.25f*Width*column, Height - 0.167f*Height - 0.333f*Height*row)
				};
				if (complete) {
					item.SetPalette((i+1)%3);
				}
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
				item.On = !item.locked;
			}
		}
		
		public void Disable() {
			foreach (LevelSelectItem item in Items ) {
				item.On = false;
			}
		}
		
		// DESTRUCTOR -----------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~LevelPage() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	public class LevelSelectItem : BetterButton {
		public bool locked;
		public int levelID;
		
		public static event EventHandler<LevelSelectionEventArgs> LevelSelectionDetected;
		
		// CONSTRUCTOR ----------------------------------------------------------------------------------------------------------------------------
		
		public LevelSelectItem(bool pComplete, bool pLocked) {
			if (pComplete) {
//				Button = new ButtonEntity("", Director.Instance.CurrentScene, null, Support.TiledSpriteFromFile("Application/assets/images/UI/LevelSelectItemButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
				_background = Support.TiledSpriteFromFile("Application/assets/images/UI/LevelSelectItemButton.png", 1, 3);
			} else {
//				Button = new ButtonEntity("", Director.Instance.CurrentScene, null, Support.TiledSpriteFromFile("Application/assets/images/UI/LevelSelectItemButtonDisabled.png", 1, 3).TextureInfo, new Vector2i(0,0));
				_background = Support.TiledSpriteFromFile("Application/assets/images/UI/LevelSelectItemButtonDisabled.png", 1, 3);
			}
			_background.CenterSprite();
			var size = _background.CalcSizeInPixels();
			Initialize(size.X, size.Y);
			_bounds = new Bounds2( new Vector2(-Width/2.0f, -Height/2.0f), new Vector2(Width/2.0f, Height/2.0f) );
//			_bounds.Add( new Vector2(-Width/2.0f, -Height/2.0f) );
//			Button.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
//			this.AddChild(Button.getNode());
			locked = pLocked;
			if (locked) {
				var lockIcon = Support.SpriteFromFile("Application/assets/images/UI/lockIcon.png");
				lockIcon.Position = new Vector2( 25.0f, -38.0f);
				this.AddChild( lockIcon );
			}
//			Button.ButtonUpAction += HandleButtonButtonUpAction;
			this.On = !locked;
		}
		
		// OVERRIDES ----------------------------------------------------------------------------------------------------------------------------
		
		protected override void OnButtonUp ()
		{
			EventHandler<LevelSelectionEventArgs> handler = LevelSelectionDetected;
			if ( handler != null ) {
				handler( this, new LevelSelectionEventArgs() { ID = this.levelID });
			}
		}
		
		// METHODS ------------------------------------------------------------------------------------------------------------------------------
		
		public void SetPalette( int pIndex ) {
			Color = QColor.palette[pIndex];
		}
		
		// DESTRUCTOR -----------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~LevelSelectItem() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
//	public class LevelSelectItem : Node {
//		public bool locked;
//		public int levelID;
//
//		public Vector4 Color {
//			get {
//				return (Button.getNode() as SpriteBase).Color;
//			} 
//			set {
//				(Button.getNode() as SpriteBase).Color = value;
//			}
//		}
////		public ButtonEntity Button {get; private set;}
//		public BetterButton Button (get; private set;}
//		
//		
//		public static event EventHandler<LevelSelectionEventArgs> LevelSelectionDetected;
//		
//		// CONSTRUCTOR ----------------------------------------------------------------------------------------------------------------------------
//		
//		public LevelSelectItem(bool pComplete, bool pLocked) {
//			if (pComplete) {
//				Button = new ButtonEntity("", Director.Instance.CurrentScene, null, Support.TiledSpriteFromFile("Application/assets/images/UI/LevelSelectItemButton.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			} else {
//				Button = new ButtonEntity("", Director.Instance.CurrentScene, null, Support.TiledSpriteFromFile("Application/assets/images/UI/LevelSelectItemButtonDisabled.png", 1, 3).TextureInfo, new Vector2i(0,0));
//			}
//			Button.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
//			this.AddChild(Button.getNode());
//			locked = pLocked;
//			if (locked) {
//				var lockIcon = Support.SpriteFromFile("Application/assets/images/UI/lockIcon.png");
//				lockIcon.Position = new Vector2( 25.0f, -38.0f);
//				this.AddChild( lockIcon );
//			}
//			Button.ButtonUpAction += HandleButtonButtonUpAction;
//			Button.on = !locked;
//		}
//		
//		// EVENT HANDLERS -------------------------------------------------------------------------------------------------------------------------
//		
//		void HandleButtonButtonUpAction (object sender, EventArgs e)
//		{
//			EventHandler<LevelSelectionEventArgs> handler = LevelSelectionDetected;
//			if ( handler != null ) {
//				handler( this, new LevelSelectionEventArgs() { ID = this.levelID });
//			}
//		}
//		
//		// OVERRIDES ------------------------------------------------------------------------------------------------------------------------------
//		
//		public override void OnExit() {
//			Button.ButtonUpAction -= HandleButtonButtonUpAction;
//			Button = null;
//			base.OnExit ();
//		}
//		
//		// METHODS --------------------------------------------------------------------------------------------------------------------------------
//		
//		public void SetPalette( int pIndex ) {
//			Color = QColor.palette[pIndex];
//		}
//		
//		// DESTRUCTOR -----------------------------------------------------------------------------------------------------------------------------
//#if DEBUG
//		~LevelSelectItem() {
//			Console.WriteLine(GetType().ToString() + " " + "Deleted");
//		}
//#endif
//	}
	
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

