using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;
namespace Crystallography.Deprecated
{
	public class InstructionsScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
		
	{
		private TextureInfo _ti;
        private Texture2D _texture;
        
		public InstructionsScene ()
		{
       
            this.Camera.SetViewFromViewport();

            _texture = new Texture2D("/Application/assets/images/allSame.png",false);
// started working out game logic in a seperate project Card Match Login
            _ti = new TextureInfo(_texture);
            SpriteUV titleScreen = new SpriteUV(_ti);
            titleScreen.Scale = _ti.TextureSizef;
            titleScreen.Pivot = new Vector2(0.5f,0.5f);
            titleScreen.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2,
                                              Director.Instance.GL.Context.GetViewport().Height/2);
            this.AddChild(titleScreen);
            
            Vector4 origColor = titleScreen.Color;
            titleScreen.Color = new Vector4(0,0,0,0);
            var tintAction = new TintTo(origColor,10.0f);
            ActionManager.Instance.AddAction(tintAction,titleScreen);
            tintAction.Run();

            Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);

            // Clear any queued clicks so we dont immediately exit if coming in from the menu
            Touch.GetData(0).Clear();
        }
        
        public override void OnEnter ()
        {

        }
        public override void OnExit ()
        {
            base.OnExit ();

        }
        
        public override void Update (float dt)
        {
            base.Update (dt);
            var touches = Touch.GetData(0).ToArray();
            if((touches.Length >0 && touches[0].Status == TouchStatus.Down) || Input2.GamePad0.Cross.Press)
            {
                Director.Instance.ReplaceScene(new InstructionsScene2());
            }
        }
    
        ~InstructionsScene()
        {
             _texture.Dispose();
            _ti.Dispose ();
        }
	}
}

