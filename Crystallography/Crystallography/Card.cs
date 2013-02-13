using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
    public class Card : SpriteUV
    {
        private PhysicsBody _physicsBody;
        // Change this value to make the game faster or slower
        public const float BALL_VELOCITY = 1.0f;
        
        public Card (PhysicsBody physicsBody)
        {
            _physicsBody = physicsBody;
            
            this.TextureInfo = new TextureInfo(new Texture2D("Application/assets/images/topSide.png", false));
            this.Scale = this.TextureInfo.TextureSizef/4f;
            this.Pivot = new Sce.PlayStation.Core.Vector2(0.5f,0.5f);
			this.Color = Colors.Red;
			
			System.Random rand = new System.Random();
            this.Position = new Sce.PlayStation.Core.Vector2(0,0);
//                (float)Director.Instance.GL.Context.GetViewport().Width * (float)rand.NextDouble(),
//                (float)Director.Instance.GL.Context.GetViewport().Height * (float)rand.NextDouble());
			
//			_physicsBody.Position = new Sce.PlayStation.Core.Vector2(
//				(float)Director.Instance.GL.Context.GetViewport().Width * (float)rand.NextDouble(),
//                (float)Director.Instance.GL.Context.GetViewport().Height * (float)rand.NextDouble());
            
            
            //Right angles are exceedingly boring, so make sure we dont start on one
            //So if our Random angle is between 90 +- 25 degrees or 270 +- 25 degrees
            //we add 25 degree to value, ie, making 90 into 115 instead
            
            float angle = (float)rand.Next(0,360);
        
            if((angle%90) <=25) angle +=25.0f;
            this._physicsBody.Velocity = new Vector2(0.0f,BALL_VELOCITY).Rotate(PhysicsUtility.GetRadian(angle));;
            
            Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
        }
        
		public PhysicsBody physicsBody {
			get { return _physicsBody; }
		}
		
        public override void Update (float dt)
        {

            this.Position = _physicsBody.Position * GamePhysics.PtoM;

            
            // We want to prevent situations where the balls is bouncing side to side
            // so if there isnt a certain amount of movement on the Y axis, set it to + or - 0.2 randomly
            // Note, this can result in the ball bouncing "back", as in it comes from the top of the screen
            // But riccochets back up at the user.  Frankly, this keeps things interesting imho
            var normalizedVel = _physicsBody.Velocity.Normalize();
            if(System.Math.Abs (normalizedVel.Y) < 0.2f) 
            {
                System.Random rand = new System.Random();
                if(rand.Next (0,1) == 0)
                    normalizedVel.Y+= 0.2f;
                
                else
                    normalizedVel.Y-= 0.2f;
            }
            
            // Pong is a mess with physics, so just fix the ball velocity
            // Otherwise the ball could get faster and faster ( or slower ) on each collision
            _physicsBody.Velocity = normalizedVel * BALL_VELOCITY;

        }
        
        ~Card()
        {
            this.TextureInfo.Texture.Dispose();
            this.TextureInfo.Dispose();
        }
    }
}
