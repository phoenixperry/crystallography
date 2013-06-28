using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class MessagePanel : HudPanel
	{
		SpriteTile Background;
		SpriteTile Bar;
		
		Label MessageTitleText;
		Label MessageText;
		
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
		public MessagePanel (){
			DismissDelay = 1.67f;
			Height = 176.0f;
			Width = 450.0f;
			SlideInDirection = SlideDirection.UP;
			SlideOutDirection = SlideDirection.DOWN;
			
			Background = Support.UnicolorSprite("Grey", 40, 40, 40, 200);
			Background.Scale = new Vector2(30.0f, 11.0f);
			this.AddChild(Background);
			
			Bar = Support.UnicolorSprite("LightBlue", 41, 226, 226, 255);
			Bar.Scale = new Vector2(30.0f, 0.25f);
			Bar.Position = new Vector2(0.0f, 172.0f);
			this.AddChild(Bar);
			
			MessageTitleText = new Label() {
				Text = "Lorem ipsum dolor sit amet, consectetur",
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25) ),
				Color = new Vector4(0.161f, 0.886f, 0.886f, 1.0f),
				Position = new Vector2(40.0f, 135.0f)
			};
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
				FontMap = Crystallography.UI.FontManager.Instance.GetMap( Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 18) ),
				Position = new Vector2(40.0f, 107.0f)
			};
			this.AddChild(MessageText);
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------------------------
#if DEBUG
        ~MessagePanel() {
			Console.WriteLine("MessagePanel deleted.");
        }
#endif
	}
}

