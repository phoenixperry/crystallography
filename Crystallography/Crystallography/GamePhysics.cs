    using System;
    using Sce.PlayStation.Core;
    using Sce.PlayStation.HighLevel.GameEngine2D;
    using Sce.PlayStation.HighLevel.GameEngine2D.Base;
    using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public class GamePhysics:PhysicsScene
	{
		protected static GamePhysics _instance;
		
		//pixels to meters 
		public static readonly float PtoM = 50.0f;
		private const float BALLRADIUS = 45.0f/2f;
		private const float CUBERADIUS = 60.0f/2f;
		private const float PADDLEWIDHT = 125.0f; 
		private const float PADDLEHEIGHT = 38.0f; 
		private float _screenWidth;
        private float _screenHeight;
            
            
        public enum BODIES { Card = 0, Cube, LeftBumper, RightBumper, TopBumper, BottomBumper };
    	
		// GET & SET ------------------------------------------------------------------------------------
		
		public static GamePhysics Instance {
			get {
				if (_instance == null) {
					return _instance = new GamePhysics();
				}
				return _instance;
			}
			private set {
				_instance = value;
			}
		}
		
		// CONSTRUCTOR ----------------------------------------------------------------------------------
		
		protected GamePhysics ()
		{
			_screenWidth = Director.Instance.GL.Context.Screen.Width;
            _screenHeight = Director.Instance.GL.Context.Screen.Height;
			
  			// turn gravity off
            this.InitScene();
            this.Gravity = new Vector2(0.0f,0.0f);
                
            // Set the screen boundaries + 2m or 100pixel
            this.SceneMin = new Vector2(-100f,-100f) / PtoM;
            this.SceneMax = new Vector2(_screenWidth + 100.0f,_screenHeight + 100.0f) / PtoM;
                
            // And turn the bouncy bouncy on
            this.RestitutionCoeff = 1.0f;
                
			//Shape Library
			
			//CARD
			this.SceneShapes[this.NumShape] = new PhysicsShape(GamePhysics.BALLRADIUS/PtoM);
			this.NumShape++;
			
			//CUBE!
			this.SceneShapes[this.NumShape] = new PhysicsShape(GamePhysics.CUBERADIUS/PtoM);
			this.NumShape++;
			
			//PADDLE
//			Vector2 box = new Vector2(PADDLEWIDHT/2f/PtoM, PADDLEHEIGHT/2f/PtoM);
//            this.SceneShapes[this.NumShape] = new PhysicsShape(box);
//			this.NumShape++;
			
			//VERT. BUMPERS
			this.SceneShapes[this.NumShape] = new PhysicsShape((new Vector2(1.0f,_screenHeight)) / PtoM);
			this.NumShape++;
			
			//Left bumper
            this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[(NumShape-1)],PhysicsUtility.FltMax);
            this.SceneBodies[this.NumBody].Position = new Vector2(0,_screenHeight/2f) / PtoM;
            this.sceneBodies[this.NumBody].ShapeIndex = (uint)(NumShape-1);
            this.sceneBodies[this.NumBody].Rotation = 0;
            this.SceneBodies[this.NumBody].SetBodyStatic();
			this.NumBody++;
            
            //Right bumper
            this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[(NumShape-1)],PhysicsUtility.FltMax);
            this.SceneBodies[this.NumBody].Position = new Vector2(_screenWidth,_screenHeight/2f) / PtoM;
            this.sceneBodies[this.NumBody].ShapeIndex = (uint)(NumShape-1);
            this.sceneBodies[this.NumBody].Rotation = 0;
            this.SceneBodies[this.NumBody].SetBodyStatic();
			this.NumBody++;
			
			//HORIZ. BUMPERS
			this.SceneShapes[this.NumShape] = new PhysicsShape((new Vector2(_screenWidth,1.0f)) / PtoM);
			this.NumShape++;
			
			//Top bumper
			this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[(NumShape-1)],PhysicsUtility.FltMax);
			this.SceneBodies[this.NumBody].Position = new Vector2(_screenWidth/2f,34) / PtoM;
			this.sceneBodies[this.NumBody].ShapeIndex = (uint)(NumShape-1);
			this.sceneBodies[this.NumBody].Rotation = 0;
			this.SceneBodies[this.NumBody].SetBodyStatic();
			this.NumBody++;
			
			//Bottom bumper
			this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[(NumShape-1)],PhysicsUtility.FltMax);
			this.SceneBodies[this.NumBody].Position = new Vector2(_screenWidth/2f,_screenHeight) / PtoM;
			this.sceneBodies[this.NumBody].ShapeIndex = (uint)(NumShape-1);
			this.sceneBodies[this.NumBody].Rotation = 0;
			this.SceneBodies[this.NumBody].SetBodyStatic();
			this.NumBody++;
			
//            System.Random rand = new System.Random();
            
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// METHODS ------------------------------------------------------------------------------------------------
		
//		public PhysicsBody addCardPhysics(Vector2 position) {
//			this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[0],0.1f);
//            this.SceneBodies[this.NumBody].ShapeIndex = 0;
//            this.sceneBodies[this.NumBody].ColFriction = 0.01f;
//            this.SceneBodies[this.NumBody].Position = position / PtoM;
//            this.NumBody++;
//			return SceneBodies[this.NumBody-1];
//		}
		
		public void RegisterPhysicsShape( PhysicsShape pShape ) {
			this.SceneShapes[numShape] = new PhysicsShape( pShape );
			this.NumShape++;
		}
		
		public PhysicsBody RegisterPhysicsBody(PhysicsShape pShape, float pMass, float pColFriction, Vector2 pPosition) {
//			if (!Array.Exists<PhysicsShape>(SceneShapes, pShape)) {
			int i = NumShape-1;
			while(i>=0) {
				if(SceneShapes[i] == pShape) {
					break;
				}
				i--;
			}
			if( i == -1) { // REQUESTED SceneShape COULD NOT BE FOUND IN REGISTRY, SO REGISTER IT
				RegisterPhysicsShape( pShape );
				i = NumShape-1;
			}
			this.SceneBodies[this.NumBody] = new PhysicsBody( pShape, pMass );
//			this.SceneBodies[this.NumBody].ShapeIndex = Array.FindIndex<PhysicsShape>(this.SceneShapes, pShape);
			this.SceneBodies[this.NumBody].ShapeIndex = (uint)i;
			this.SceneBodies[this.NumBody].ColFriction = pColFriction;
			this.SceneBodies[this.NumBody].Position = pPosition / PtoM;
			this.NumBody++;
			return SceneBodies[this.NumBody-1];
		}
		
		public void removePhysicsBody(PhysicsBody pb)
		{
			pb.Clear();
			int i = Array.IndexOf(this.SceneBodies, pb);
			this.SceneBodies[i] = null;
			if (i != NumBody-1 ) {
				// clean up hole in the array unless we just removed the last element
				for (int j=i; j < numBody-1; j++) {
					this.SceneBodies[j] = this.SceneBodies[j+1];
					this.SceneBodies[j+1] = null;
				}
			}
			numBody--;
		}
		
//		public PhysicsBody addGroupPhysics(Vector2 position) {
//			this.SceneBodies[this.NumBody] = new PhysicsBody(SceneShapes[0],0.1f);
//            this.SceneBodies[this.NumBody].ShapeIndex = 0;
//            this.sceneBodies[this.NumBody].ColFriction = 0.01f;
//            this.SceneBodies[this.NumBody].Position = position / PtoM;
//            this.NumBody++;
//			return SceneBodies[this.NumBody-1];
//		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------------
#if DEBUG
		~GamePhysics() {
			Console.WriteLine(GetType().ToString() + " deleted" );
		}
#endif
    }
}	
