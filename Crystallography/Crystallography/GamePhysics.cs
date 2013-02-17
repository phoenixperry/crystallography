    using System;
    using Sce.PlayStation.Core;
    using Sce.PlayStation.HighLevel.GameEngine2D;
    using Sce.PlayStation.HighLevel.GameEngine2D.Base;
    using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public class GamePhysics:PhysicsScene
	{
		//pixels to meters 
		public const float PtoM =50.0f; 
		private const float BALLRADIUS = 35.0f/2f; 
		private const float PADDLEWIDHT = 125.0f; 
		private const float PADDLEHEIGHT = 38.0f; 
		private float _screenWidth;
        private float _screenHeight;
            
            
        public enum BODIES { Ball = 0, Player, Ai, LeftBumper, RightBumper, TopBumper, BottomBumper };
    
		
		public GamePhysics ()
		{
			_screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			
  				// turn gravity off
                this.InitScene();
                this.Gravity = new Vector2(0.0f,0.0f);
                
                // Set the screen boundaries + 2m or 100pixel
                this.SceneMin = new Vector2(-100f,-100f) / PtoM;
                this.SceneMax = new Vector2(_screenWidth + 100.0f,_screenHeight + 100.0f) / PtoM;
                
                // And turn the bouncy bouncy on
                this.RestitutionCoeff = 1.0f;
                
//                this.NumBody = 7; // Ball, 2 paddles, 2 bumpers
//                this.NumShape = 4; // One of each of the above
			
			//Shape Library
			//BALL
			this.SceneShapes[this.NumShape] = new PhysicsShape(GamePhysics.BALLRADIUS/PtoM);
			this.NumShape++;
			//PADDLE
			Vector2 box = new Vector2(PADDLEWIDHT/2f/PtoM,-PADDLEHEIGHT/2f/PtoM);
            this.SceneShapes[this.NumShape] = new PhysicsShape(box);
			this.NumShape++;
			//VERT. BUMPERS
			this.SceneShapes[this.NumShape] = new PhysicsShape((new Vector2(1.0f,_screenHeight)) / PtoM);
			this.NumShape++;
			//HORIZ. BUMPERS
			this.SceneShapes[this.NumShape] = new PhysicsShape((new Vector2(_screenWidth,1.0f)) / PtoM);
			this.NumShape++;
			
			
                //create the ball physics object
            System.Random rand = new System.Random();
//			addCardPhysics (new Vector2(_screenWidth * (float)rand.NextDouble(), _screenHeight * (float)rand.NextDouble()));
//                this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[0],0.1f);
//                this.SceneBodies[this.NumBody].ShapeIndex = 0;
//                this.sceneBodies[this.NumBody].ColFriction = 0.01f;
//                this.SceneBodies[this.NumBody].Position = new Vector2(_screenWidth/2,_screenHeight/2) / PtoM;
//                this.NumBody++;
                
                //Paddle shape --> this is where we need to plug in our diiferent types 
                
                
                //Player paddle
//                this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[1],1.0f);
//                this.SceneBodies[this.NumBody].Position = new Vector2(-50f,-50f) / PtoM;
//                this.SceneBodies[this.NumBody].Rotation = 0;
//                this.SceneBodies[this.NumBody].ShapeIndex = 1;
//                this.NumBody++;
			                 
                //Ai paddle
//                this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[1],1.0f);
//                float aiX = (50f/PtoM);
//                float aiY = (-50f/ PtoM);
//                this.SceneBodies[this.NumBody].Position = new Vector2(aiX,aiY);
//                this.SceneBodies[this.NumBody].Rotation = 0;
//                this.SceneBodies[this.NumBody].ShapeIndex = 1;
//            	this.NumBody++;
			
                //Now a shape for left and right bumpers to keep ball on screen
                
				//This is the shape for the top and bottom bumpers
				
                
                //Left bumper
                this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[2],PhysicsUtility.FltMax);
                this.SceneBodies[this.NumBody].Position = new Vector2(0,_screenHeight/2f) / PtoM;
                this.sceneBodies[this.NumBody].ShapeIndex = 2;
                this.sceneBodies[this.NumBody].Rotation = 0;
                this.SceneBodies[this.NumBody].SetBodyStatic();
				this.NumBody++;
                
                //Right bumper
                this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[2],PhysicsUtility.FltMax);
                this.SceneBodies[this.NumBody].Position = new Vector2(_screenWidth,_screenHeight/2f) / PtoM;
                this.sceneBodies[this.NumBody].ShapeIndex = 2;
                this.sceneBodies[this.NumBody].Rotation = 0;
                this.SceneBodies[this.NumBody].SetBodyStatic();
				this.NumBody++;
			
				//Top bumper
				this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[3],PhysicsUtility.FltMax);
				this.SceneBodies[this.NumBody].Position = new Vector2(_screenWidth/2f,0) / PtoM;
				this.sceneBodies[this.NumBody].ShapeIndex = 3;
				this.sceneBodies[this.NumBody].Rotation = 0;
				this.SceneBodies[this.NumBody].SetBodyStatic();
				this.NumBody++;
			
				//Bottom bumper
				this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[3],PhysicsUtility.FltMax);
				this.SceneBodies[this.NumBody].Position = new Vector2(_screenWidth/2f,_screenHeight) / PtoM;
				this.sceneBodies[this.NumBody].ShapeIndex = 3;
				this.sceneBodies[this.NumBody].Rotation = 0;
				this.SceneBodies[this.NumBody].SetBodyStatic();
				this.NumBody++;
            }
		
		public PhysicsBody addCardPhysics(Vector2 position) {
			this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[0],0.1f);
            this.SceneBodies[this.NumBody].ShapeIndex = 0;
            this.sceneBodies[this.NumBody].ColFriction = 0.01f;
            this.SceneBodies[this.NumBody].Position = position / PtoM;
            this.NumBody++;
			return SceneBodies[this.NumBody-1];
		}
		
		public void removePhysicsBody(PhysicsBody pb)
		{
			pb.Clear();
			int i = Array.IndexOf(this.SceneBodies, pb);
			this.SceneBodies[i] = null;
			for (int j=i; j < numBody-1; j++) {
				this.SceneBodies[j] = this.SceneBodies[j+1];
			}
			numBody--;
		}
		
		public PhysicsBody addGroupPhysics(Vector2 position) {
			this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[0],0.1f);
            this.SceneBodies[this.NumBody].ShapeIndex = 0;
            this.sceneBodies[this.NumBody].ColFriction = 0.01f;
            this.SceneBodies[this.NumBody].Position = position / PtoM;
            this.NumBody++;
			return SceneBodies[this.NumBody-1];
		}
    }
}	
