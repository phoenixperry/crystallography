using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class InstructionsScreen : Layer
	{
		MenuSystemScene MenuSystem;
//		Label InstructionsTitleText;
		ButtonEntity BackButton;
		List<Node> Panels;
		SwipePanels SwipePanels;
		
		// CONSTRUCTORS ---------------------------------------------------------------------------------------------------------------------------
		
		public InstructionsScreen (MenuSystemScene pMenuSystem) {
			MenuSystem = pMenuSystem;
			
			Panels = new List<Node>{
				new InstructionsPage1(),
				new InstructionsPage2(),
				new InstructionsPage3(),
				new InstructionsPage4(),
				new InstructionsPage5(),
				new InstructionsPage8(),
				new InstructionsPage7()
			};
			
			this.SwipePanels = new SwipePanels(Panels) {
//				Position = new Vector2(18.0f, 33.0f)
			};
			this.AddChild(this.SwipePanels);
			
			foreach (Node panel in Panels) {
				this.AddChild(panel);
			}
			
			BackButton = new ButtonEntity("       back", MenuSystem, null, Support.TiledSpriteFromFile("Application/assets/images/blueBtn.png", 1, 3).TextureInfo, new Vector2i(0,0));
			BackButton.label.FontMap = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold") );
			BackButton.setPosition(816.0f, 35.0f);
			this.AddChild(BackButton.getNode());
		}
		
		// EVENT HANDLERS ------------------------------------------------------------------------------------------------------------------------
		
		void HandleBackButtonButtonUpAction (object sender, EventArgs e) {
			MenuSystem.SetScreen("Menu");
		}
		
		// OVERRIDES -----------------------------------------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			BackButton.ButtonUpAction += HandleBackButtonButtonUpAction;
		}
		
		public override void OnExit ()
		{
			BackButton.ButtonUpAction -= HandleBackButtonButtonUpAction;
			base.OnExit ();
			
			MenuSystem = null;
//			InstructionsTitleText = null;
			BackButton = null;
			Panels.Clear();
			this.SwipePanels = null;
		}
		
		// METHODS -------------------------------------------------------------------------------------------------------------------------------
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~InstructionsScreen() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	// HELPER CLASSES =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
	
	public class InstructionsPage1 : Node {
		SpriteTile Img;
		
		public InstructionsPage1() {
			Img = Support.SpriteFromFile("/Application/assets/images/UI/instructions1.png");
			Img.Position = new Vector2(63.0f, 135.0f);
			this.AddChild(Img);
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			Img = null;
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~InstructionsPage1() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	public class InstructionsPage2 : Node {
		SpriteTile Img;
		
		public InstructionsPage2() {
			Img = Support.SpriteFromFile("/Application/assets/images/UI/instructions2.png");
			Img.Position = new Vector2(180.0f, 209.0f);
			this.AddChild(Img);
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			Img = null;
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~InstructionsPage2() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
		public class InstructionsPage3 : Node {
		SpriteTile Img;
		
		public InstructionsPage3() {
			Img = Support.SpriteFromFile("/Application/assets/images/UI/instructions3.png");
			Img.Position = new Vector2(235.0f, 89.0f);
			this.AddChild(Img);
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			Img = null;
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~InstructionsPage3() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
		public class InstructionsPage4 : Node {
		SpriteTile Img;
		
		public InstructionsPage4() {
			Img = Support.SpriteFromFile("/Application/assets/images/UI/instructions4.png");
			Img.Position = new Vector2(180.0f, 105.0f);
			this.AddChild(Img);
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			Img = null;
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~InstructionsPage4() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
		public class InstructionsPage5 : Node {
		SpriteTile Img;
		
		public InstructionsPage5() {
			Img = Support.SpriteFromFile("/Application/assets/images/UI/instructions5.png");
			Img.Position = new Vector2(108.0f, 113.0f);
			this.AddChild(Img);
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			Img = null;
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~InstructionsPage5() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
		public class InstructionsPage8 : Node {
		SpriteTile Img;
		
		public InstructionsPage8() {
			Img = Support.SpriteFromFile("/Application/assets/images/UI/instructions8.png");
			Img.Position = new Vector2(66.0f, 100.0f);
			this.AddChild(Img);
		}
		
	public override void OnExit ()
		{
			base.OnExit ();
			Img = null;
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~InstructionsPage8() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
				
	public class InstructionsPage7 : Node {
		SpriteTile Img;
		
		public InstructionsPage7() {
			Img = Support.SpriteFromFile("/Application/assets/images/UI/instructions7.png");
			Img.Position = new Vector2(128.0f, 151.0f);
			this.AddChild(Img);
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
			Img = null;
		}
		
		// DESTRUCTOR ---------------------------------------------------------------------------------------------------------------------------
#if DEBUG
		~InstructionsPage7() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
}

