using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

using Sce.PlayStation.HighLevel.UI;
namespace Crystallography
{
	public class MenuScene: Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{
		private Sce.PlayStation.HighLevel.UI.Scene _uiScene;
		Sce.PlayStation.HighLevel.UI.Panel dialog;
		Button buttonUI1;
		Button buttonUI2;
		public MenuScene ()
		{
			this.Camera.SetViewFromViewport(); 
			dialog = new Panel();
			dialog.Name = "backgroundPanel";
			dialog.Width = Director.Instance.GL.Context.GetViewport().Width; 
			dialog.Height = Director.Instance.GL.Context.GetViewport().Height; 
			dialog.SetPosition(0,0); 
			dialog.SetSize(960,544); 
			dialog.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
			dialog.Visible = true; 
			
			// BUTTON 1
			buttonUI1 = new Button();
            buttonUI1.Name = "buttonUI1";
		    buttonUI1.IconImage = null;
            buttonUI1.Style = ButtonStyle.Custom;
		    buttonUI1.SetPosition(dialog.Width/2 - 150,250.0f);

            buttonUI1.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/play.png",false),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/playOver.png",false),
                BackgroundDisabledImage = new ImageAsset("/Application/assets/images/playOver.png",false),
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };
    		buttonUI1.SetPosition(356, 24);
			buttonUI1.SetSize(289, 280);
			buttonUI1.Anchors = Anchors.None;
			buttonUI1.Visible = true;
			
			buttonUI1.TouchEventReceived += (sender, e) =>  {
				Director.Instance.ReplaceScene(new GameScene()); 
			}; 
			
			// BUTTON 2
			buttonUI2 = new Button();
            buttonUI2.Name = "buttonMenu";
             // buttonUI2
            buttonUI2.IconImage = null;
            buttonUI2.Style = ButtonStyle.Custom;
            buttonUI2.CustomImage = new CustomButtonImageSettings()
            {
                BackgroundNormalImage = new ImageAsset("/Application/assets/images/instructions.png",false),
                BackgroundPressedImage = new ImageAsset("/Application/assets/images/instructionsOver.png",false),
                BackgroundDisabledImage = null,
                BackgroundNinePatchMargin = new NinePatchMargin(42, 27, 42, 27),
            };

            buttonUI2.TouchEventReceived += (sender, e) => {
            Director.Instance.ReplaceScene(new InstructionsScene());
            };  
			
			buttonUI2.SetPosition(356, 281);
			buttonUI2.SetSize(289, 233);
			buttonUI2.Anchors = Anchors.None;
			buttonUI2.Visible = true;
			
			dialog.AddChildLast(buttonUI1); 
			dialog.AddChildLast(buttonUI2); 
			_uiScene = new Sce.PlayStation.HighLevel.UI.Scene(); 
			_uiScene.RootWidget.AddChildLast(dialog); 
			UISystem.SetScene(_uiScene); 
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false); 
			
		}
		public override void Update(float dt){
			base.Update(dt); 
			UISystem.Update(Touch.GetData(0)); 
		}
		public override void Draw ()
        {
            base.Draw();
            UISystem.Render ();
        }
        
		public override void OnEnter ()
		{
			base.OnEnter ();
		}
		
		public override void OnExit ()
		{
			base.OnExit ();
		}
		
        ~MenuScene()
        {
			buttonUI1.CustomImage.BackgroundNormalImage.Dispose();
			buttonUI1.CustomImage.BackgroundPressedImage.Dispose();
			buttonUI1.CustomImage.BackgroundDisabledImage.Dispose();
            buttonUI1.Dispose();
			
			buttonUI2.CustomImage.BackgroundNormalImage.Dispose();
			buttonUI2.CustomImage.BackgroundPressedImage.Dispose();
//			buttonUI2.CustomImage.BackgroundDisabledImage.Dispose();
			buttonUI2.Dispose();
			
			dialog.Dispose();
        }
	}
}


