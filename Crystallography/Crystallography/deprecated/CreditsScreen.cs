using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class CreditsScreen : Layer
	{
		MenuSystemScene MenuSystem;
		Label CreditsTitleText;
		ButtonEntity BackButton;
		List<Node> Panels;
		SwipePanels SwipePanels;
		
		// CONSTRUCTORS --------------------------------------------------------------------------------------------------------------------
		
		public CreditsScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			Panels = new List<Node> {
				new CreditsPanel(),
				new ThanksPanel()
			};
			
			SwipePanels = new SwipePanels(Panels) {
				Position = new Vector2(44.0f, 88.0f)
			};
			this.AddChild(this.SwipePanels);
			
			foreach(Node panel in Panels) {
				this.AddChild(panel);
			}
			
			CreditsTitleText = new Label{
				Text = "credits",
				Position = new Vector2(36.0f, 444.0f),
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 72, "Regular") )
			};
			this.AddChild(CreditsTitleText);
			
			BackButton = new ButtonEntity("       back", MenuSystem, null, Support.TiledSpriteFromFile("Application/assets/images/blueBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			BackButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			BackButton.setPosition(816.0f, 35.0f);
			this.AddChild(BackButton.getNode());
		}
		
		// EVENT HANDLERS ------------------------------------------------------------------------------------------------------------------
		
		void HandleBackButtonButtonUpAction (object sender, EventArgs e) {
			MenuSystem.SetScreen("Menu");
		}
		
		// OVERRIDES -----------------------------------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			BackButton.ButtonUpAction += HandleBackButtonButtonUpAction;
		}

		
		public override void OnExit ()
		{
			BackButton.ButtonUpAction -= HandleBackButtonButtonUpAction;
			base.OnExit ();
			
			Panels.Clear();
			this.RemoveAllChildren(true);
			MenuSystem = null;
			this.SwipePanels = null;
			CreditsTitleText = null;
			BackButton = null;
		}
		
		// METHODS -------------------------------------------------------------------------------------------------------------------------
		
		
		
		// DESTRUCTOR ----------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~CreditsScreen() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	// HELPER CLASSES ---------------------------------------------------------------------------------------------------------------------
	
	public class CreditsPanel : Node
	{
		SpriteTile BenIcon;
		SpriteTile MegIcon;
		SpriteTile NixIcon;
		
		Label BenText;
		Label MegText;
		Label NixText;
		
		FontMap Map;
		
		// CONSTRUCTOR ---------------------------------------------------------------------------------------------------------------------
		
		public CreditsPanel() {
			Map = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Regular") );
			
			BenIcon = Support.SpriteFromFile("/Application/assets/images/UI/icons/tie.png");
			BenIcon.Position = new Vector2(302.0f, 184.0f);
			this.AddChild( BenIcon );
			
			BenText = new Label(){
				Text = "ben johnson",
				Position = new Vector2(113.0f, 186.0f),
				Color = Colors.White,
				FontMap = Map
			};
			this.AddChild( BenText );
			
			MegIcon = Support.SpriteFromFile("/Application/assets/images/UI/icons/phones.png");
			MegIcon.Position = new Vector2(536.0f, 96.0f);
			this.AddChild( MegIcon );
			
			MegText = new Label(){
				Text = "margaret schedel",
				Position = new Vector2(272.0f, 100.0f),
				Color = Colors.White,
				FontMap = Map
			};
			this.AddChild( MegText );
			
			NixIcon = Support.SpriteFromFile("/Application/assets/images/UI/icons/glasses.png");
			NixIcon.Position = new Vector2(595.0f, 184.0f);
			this.AddChild( NixIcon );
			
			NixText = new Label(){
				Text = "phoenix perry",
				Position = new Vector2(385.0f, 186.0f),
				Color = Colors.White,
				FontMap = Map
			};
			this.AddChild( NixText );
		}
		
		// OVERRIDES -----------------------------------------------------------------------------------------------------------------------
		
		public override void OnExit ()
		{
			base.OnExit ();
			this.RemoveAllChildren(true);
			BenIcon = null;
			MegIcon = null;
			NixIcon = null;
			BenText = null;
			MegText = null;
			NixText = null;
			Map = null;
		}
		
		// DESTRUCTOR ----------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~CreditsPanel() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	public class ThanksPanel : Node
	{
		Label ThanksToText;
		Label ThanksNamesText;
		Label ColophonText;
		
		FontMap BigMap;
		FontMap MidMap;
		FontMap SmMap;
		
		public ThanksPanel() {
			BigMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			MidMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25) );
			SmMap = UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 14, "Regular") );
			
			ThanksToText = new Label() {
				Text = "thanks to",
				Position = new Vector2(32.0f, 245.0f),
				Color = Colors.White,
				FontMap = BigMap
			};
			this.AddChild( ThanksToText );
			
			ThanksNamesText = new Label() {
				Text = "indiecade, matt parker, nicolas fortugno, jim wallace and the psm team.",
				Position = new Vector2(32.0f, 205.0f),
				Color = Colors.White,
				FontMap = MidMap
			};
			this.AddChild( ThanksNamesText );
			
			ColophonText = new Label() {
//				Text = "colophon: crystallon uses bariol and bariol bold by ismael gonzález gonzález & raúl garcía del pomar",
				Text = "colophon: crystallon uses bariol regular and bariol bold by ismael gonzalez gonzalez & raul garcia del pomar",
				Position = new Vector2(32.0f, 137.0f),
				Color = Colors.White,
				FontMap = SmMap
			};
			this.AddChild( ColophonText );
		}
		
		// OVERRIDES -----------------------------------------------------------------------------------------------------------------------
		
		public override void OnExit ()
		{
			base.OnExit ();
			this.RemoveAllChildren(true);
			ThanksToText = null;
			ThanksNamesText = null;
			ColophonText = null;
			BigMap = null;
			MidMap = null;
			SmMap = null;
		}
		
		// DESTRUCTOR ----------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~ThanksPanel() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}

