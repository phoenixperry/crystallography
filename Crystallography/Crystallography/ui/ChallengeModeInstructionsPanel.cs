using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class ChallengeModeInstructionsPanel : HudPanel
	{
		SpriteTile Background;
		
		BetterButton _okButton;
		
		public ChallengeModeInstructionsPanel () {
			Initialize(960.0f, 544.0f);
		}
		
		protected void Initialize (float pWidth, float pHeight) {
			DismissDelay = 0.0f; // dismiss only with ok button!
			Height = pHeight;
			Width = pWidth;
			var xScale = pWidth/16.0f;
			var yScale = pHeight/16.0f;
			SlideInDirection = SlideDirection.RIGHT;
			SlideOutDirection = SlideDirection.LEFT;
			
			Background = Support.UnicolorSprite("bg", (byte)(LevelManager.Instance.BackgroundColor.R * 255.0f), (byte)(LevelManager.Instance.BackgroundColor.G * 255.0f), (byte)(LevelManager.Instance.BackgroundColor.B * 255.0f), 255);
			Background.Scale = new Vector2(xScale, yScale);
			this.AddChild(Background);
			
			_okButton = new BetterButton(256.0f, 64.0f) {
				Text = "okay",
				Position = Vector2.Zero
			};
			_okButton.background.RegisterPalette(2);
			this.AddChild(_okButton);
		}
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			
			_okButton.ButtonUpAction += Handle_okButtonButtonUpAction;
		}

		void Handle_okButtonButtonUpAction (object sender, EventArgs e)
		{
			this.SlideOut();
		}
	}
}

