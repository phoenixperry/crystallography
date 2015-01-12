using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class MessagePanel : HudPanel
	{
		SpriteTile Background;
		SpriteTile Bar;
		
		Label MessageTitleText;
		Label MessageText;
		
		public Sce.PlayStation.HighLevel.Physics2D.PhysicsBody body;
		
		public string Text {
			get {
				return MessageText.Text;
			}
			set {
				MessageText.Text = value;
			}
		}
		
		public string TitleText {
			get {
				return MessageTitleText.Text;
			}
			set {
				MessageTitleText.Text = value;
			}
		}
		
		// CONSTRUCTOR ------------------------------------------------------------------------------------------------------------
		public MessagePanel() {
			Initialize( 480.0f, 176.0f );
		}
		
		public MessagePanel (float pWidth, float pHeight) {
			Initialize( pWidth, pHeight );
		}
		
		
		// METHODS ----------------------------------------------------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			base.Update (dt);
			if(body != null) {
				body.Position = Position / GamePhysics.PtoM;
			}
		}
		
		protected void Initialize ( float pWidth, float pHeight ) {
			DismissDelay = 1.0f;
			Height = pHeight;
			Width = pWidth;
			var xScale = pWidth/16.0f;
			var yScale = pHeight/16.0f;
			SlideInDirection = SlideDirection.UP;
			SlideOutDirection = SlideDirection.DOWN;
			
			Background = Support.UnicolorSprite("Grey", 40, 40, 40, 200);
			Background.Scale = new Vector2(xScale, yScale);
			this.AddChild(Background);
			
			Bar = Support.UnicolorSprite("white", 255, 255, 255, 255);
			Bar.Scale = new Vector2(xScale, 0.25f);
			Bar.Position = new Vector2(0.0f, pHeight - 4.0f);
			Bar.RegisterPalette(0);
			this.AddChild(Bar);
			
			MessageTitleText = new Label() {
				Text = "Lorem ipsum dolor sit amet, consectetur",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 32, "Bold") ),
//				Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f),
				Position = new Vector2(40.0f, pHeight - 41.0f)
			};
			MessageTitleText.RegisterPalette(0);
			this.AddChild(MessageTitleText);
			
			MessageText = new Label() {
				Text = "Lorem ipsum dolor sit amet, consectetur adipisicing \n" +
					"elit, sed do eiusmod tempor incididunt ut labore et \n" +
					"dolore magna aliqua. Ut enim ad minim veniam, quis \n"+
					"nostrud exercitation ullamco laboris nisi ut aliquip \n" +
					"ex ea commodo consequat. Duis aute irure dolor in \n", //+
//					"reprehenderit in voluptate velit esse cillum dolore eu \n" +
//					"fugiat nulla pariatur. Excepteur sint occaecat cupidatat \n" +
//					"non proident, sunt in culpa qui officia deserunt mollit \n" +
//					"anim id est laborum.",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25) ),
				Position = new Vector2(40.0f, pHeight - 69.0f)
			};
			this.AddChild(MessageText);
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created. " + MessageText.Text );
#endif
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------------------------
#if DEBUG
        ~MessagePanel() {
			Console.WriteLine("MessagePanel deleted.");
        }
#endif
	}
}

