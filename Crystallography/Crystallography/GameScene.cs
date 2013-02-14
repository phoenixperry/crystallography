using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.Core.Input;

namespace Crystallography
{
    public class GameScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
    {
//    private Paddle _player,_ai;
//    	public static Card ball;
		public static Card[] cards;
		public static Group[] groups;
    	private GamePhysics _physics;
//	private static DragGestureDetector _dragGestureDetector;
//    private Scoreboard _scoreboard;
    	private SoundPlayer _pongBlipSoundPlayer;
   		private Sound _pongSound;
		
		
		public bool WasTouch;
		public bool IsTouch;
		public Card SelectedCard;
		public Card FirstAttachedCard;
		public Card SecondAttachedCard;
		public Vector2 TouchStart;
        
        // Change the following value to true if you want bounding boxes to be rendered
        private static Boolean DEBUG_BOUNDINGBOXS = true;
        
        public GameScene ()
        {
            this.Camera.SetViewFromViewport();
            _physics = new GamePhysics();

            
//            ball = new Card(_physics.SceneBodies[(int)GamePhysics.BODIES.Ball]);
//            _player = new Paddle(Paddle.PaddleType.PLAYER, 
//                                 _physics.SceneBodies[(int)GamePhysics.BODIES.Player]);
//            _ai = new Paddle(Paddle.PaddleType.AI, 
//                             _physics.SceneBodies[(int)GamePhysics.BODIES.Ai]);
//            _scoreboard = new Scoreboard();
			
            var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			cards = new Card[20];
			System.Random rand = new System.Random();
			
			for (int i = 0; i < 20; i++) {
//				cards[i] = new Card(_physics.SceneBodies[(int)GamePhysics.BODIES.Ball]);
				cards[i] = new Card(_physics.addCardPhysics(new Vector2(50f + 0.75f * _screenWidth * (float)rand.NextDouble(), 50f + 0.75f * _screenHeight * (float)rand.NextDouble ())));
				this.AddChild (cards[i]);
			}
			
			groups = new Group[1];
			
			for (int i=0; i<1; i++) {
				groups[i] = new Group(_physics.addGroupPhysics(new Vector2(400f,400f)));
				this.AddChild (groups[i]);
			}
				
//            this.AddChild(_scoreboard);
//            this.AddChild(ball);
//            this.AddChild(_player);
//            this.AddChild(_ai);
			
//			 _dragGestureDetector = new DragGestureDetector();
//			_dragGestureDetector.DragDetected += delegate(object sender, DragEventArgs e) {
//				var newPos = new Vector2(e.Source.X,e.Source.Y) + e.Distance;
//				e.Source.SetPosition(newPos.X,newPos.Y);
//				_label.Text = "Dragged";
//			};
			
			
            // This is debug routine that will draw the physics bounding box around the players paddle
            if(DEBUG_BOUNDINGBOXS)
            {
                this.AdHocDraw += () => {
					foreach (PhysicsBody pb in _physics.SceneBodies) {
						if ( pb != null ) {
							var bottomLeft = pb.AabbMin;
							var topRight = pb.AabbMax;
							Director.Instance.DrawHelpers.DrawBounds2Fill (
								new Bounds2(bottomLeft*GamePhysics.PtoM, topRight*GamePhysics.PtoM));
						}
					}
//                    var bottomLeftPlayer = _physics.SceneBodies[(int)GamePhysics.BODIES.Player].AabbMin;
//                    var topRightPlayer = _physics.SceneBodies[(int)GamePhysics.BODIES.Player].AabbMax;
//                    Director.Instance.DrawHelpers.DrawBounds2Fill(
//                        new Bounds2(bottomLeftPlayer*GamePhysics.PtoM,topRightPlayer*GamePhysics.PtoM));
//
//                    var bottomLeftAi = _physics.SceneBodies[(int)GamePhysics.BODIES.Ai].AabbMin;
//                    var topRightAi = _physics.SceneBodies[(int)GamePhysics.BODIES.Ai].AabbMax;
//                    Director.Instance.DrawHelpers.DrawBounds2Fill(
//                        new Bounds2(bottomLeftAi*GamePhysics.PtoM,topRightAi*GamePhysics.PtoM));
//
//                    var bottomLeftBall = _physics.SceneBodies[(int)GamePhysics.BODIES.Ball].AabbMin;
//                    var topRightBall = _physics.SceneBodies[(int)GamePhysics.BODIES.Ball].AabbMax;
//                    Director.Instance.DrawHelpers.DrawBounds2Fill(
//                        new Bounds2(bottomLeftBall*GamePhysics.PtoM,topRightBall*GamePhysics.PtoM));
//					
//					var bottomLeftLeftBumper = _physics.SceneBodies[(int)GamePhysics.BODIES.LeftBumper].AabbMin;
//					var topRightLeftBumper = _physics.SceneBodies[(int)GamePhysics.BODIES.LeftBumper].AabbMax;
//					Director.Instance.DrawHelpers.DrawBounds2Fill(
//						new Bounds2 (bottomLeftLeftBumper*GamePhysics.PtoM,topRightLeftBumper*GamePhysics.PtoM));
//					
//					var bottomLeftRightBumper = _physics.SceneBodies[(int)GamePhysics.BODIES.RightBumper].AabbMin;
//					var topRightRightBumper = _physics.SceneBodies[(int)GamePhysics.BODIES.RightBumper].AabbMax;
//					Director.Instance.DrawHelpers.DrawBounds2Fill(
//						new Bounds2 (bottomLeftRightBumper*GamePhysics.PtoM,topRightRightBumper*GamePhysics.PtoM));
//					
//					var bottomLeftTopBumper = _physics.SceneBodies[(int)GamePhysics.BODIES.TopBumper].AabbMin;
//					var topRightTopBumper = _physics.SceneBodies[(int)GamePhysics.BODIES.TopBumper].AabbMax;
//					Director.Instance.DrawHelpers.DrawBounds2Fill(
//						new Bounds2 (bottomLeftTopBumper*GamePhysics.PtoM,topRightTopBumper*GamePhysics.PtoM));
//					
//					var bottomLeftBottomBumper = _physics.SceneBodies[(int)GamePhysics.BODIES.BottomBumper].AabbMin;
//					var topRightBottomBumper = _physics.SceneBodies[(int)GamePhysics.BODIES.BottomBumper].AabbMax;
//					Director.Instance.DrawHelpers.DrawBounds2Fill(
//						new Bounds2 (bottomLeftBottomBumper*GamePhysics.PtoM,topRightBottomBumper*GamePhysics.PtoM));
                };
            }
            
            //Now load the sound fx and create a player
         //   _pongSound = new Sound("/Application/audio/pongblip.mp3");
           // _pongBlipSoundPlayer = _pongSound.CreatePlayer();
            
            Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
        }
        
        private void ResetBall()
        {
            //Move ball to screen center and release in a random directory
//            _physics.SceneBodies[(int)GamePhysics.BODIES.Ball].Position = 
//                new Vector2(Director.Instance.GL.Context.GetViewport().Width/2,
//                            Director.Instance.GL.Context.GetViewport().Height/2) / GamePhysics.PtoM;
            
//            System.Random rand = new System.Random();
//            float angle = (float)rand.Next(0,360);
        
//            if((angle%90) <=15) angle +=15.0f;
        
//            _physics.SceneBodies[(int)GamePhysics.BODIES.Ball].Velocity = 
//                new Vector2(0.0f,5.0f).Rotate(PhysicsUtility.GetRadian(angle));
        }
        
        public override void Update (float dt)
        {
            base.Update (dt);
            
            if(Input2.GamePad0.Select.Press)
                Director.Instance.ReplaceScene(new MenuScene());
            //switch to multitouch
			Touch.GetData(0); 
			foreach (var touchData in Touch.GetData(0)) {
            if (touchData.Status == TouchStatus.Down ||
                touchData.Status == TouchStatus.Move) {

//                int pointX = (int)((touchData.X + 0.5f) * 
//					                   Director.Instance.GL.Context.GetViewport().Width);
//                int pointY = (int)((touchData.Y + 0.5f) * Director.Instance.GL.Context.GetViewport().Height);
//				System.Console.WriteLine("pointX" + pointX + "pointY"+pointY); 
               // this.FillCircle(colorTable[colorId], pointX, pointY, 96); --> this code lines graphics to location obvious 
            }
			}
            //We don't need these, but sadly, the Simulate call does.
            Vector2 dummy1 = new Vector2();
            Vector2 dummy2 = new Vector2();
            
            
            //Update the physics simulation
            _physics.Simulate(-1,ref dummy1,ref dummy2);
            
            //Now check if the ball it either paddle, and if so, play the sound
//            if(_physics.QueryContact((uint)GamePhysics.BODIES.Ball,(uint)GamePhysics.BODIES.Player) ||
//                _physics.QueryContact((uint)GamePhysics.BODIES.Ball,(uint)GamePhysics.BODIES.Ai))
            {
             //   if(_pongBlipSoundPlayer.Status == SoundStatus.Stopped)
               //     _pongBlipSoundPlayer.Play();
            }
            
            //Check if the ball went off the top or bottom of the screen and update score accordingly
//            Results result = Results.StillPlaying;
//            bool scored = false;
            
//            if(ball.Position.Y > Director.Instance.GL.Context.GetViewport().Height + ball.Scale.Y/2)
//            {
//                result = _scoreboard.AddScore(true);
//                scored = true;
//            }
//            if(ball.Position.Y < 0 - ball.Scale.Y/2)
//            {
//                result =_scoreboard.AddScore(false);
//                scored = true;
//            }
            
            // Did someone win?  If so, show the GameOver scene
//            if(result == Results.AiWin) 
//                Director.Instance.ReplaceScene(new GameOverScene(false));
//            if(result == Results.PlayerWin) 
//                Director.Instance.ReplaceScene(new GameOverScene(true));
            
            //If someone did score, but game isn't over, reset the ball position to the middle of the screen
//            if(scored == true)
//            {
//                ResetBall ();
//            }
            
            //Finally a sanity check to make sure the ball didn't leave the field.
//            var ballPB = _physics.SceneBodies[(int)GamePhysics.BODIES.Ball];
            
//            if(ballPB.Position.X < -(ball.Scale.X/2f)/GamePhysics.PtoM ||
//               ballPB.Position.X > (Director.Instance.GL.Context.GetViewport().Width)/GamePhysics.PtoM)
//            {
//                ResetBall();
//            }
			
			UpdateInput(dt);
        }
		
		public void UpdateInput (float dt) {
			Input2.TouchData touch = Input2.Touch00;
			
			WasTouch = IsTouch;
			IsTouch = touch.Down;
			var normalized = touch.Pos;
			var world = Director.Instance.CurrentScene.Camera.NormalizedToWorld(normalized);
			var card = (Card)GetCardAtPosition(world);
			var moved = TouchStart - world;
			var moved_distance = moved.SafeLength();
			
			// New Touch Starting This Frame
			if (IsTouch && !WasTouch) {
				
				if (card != null) {
					TouchStart = world;
					SelectedCard = card;
					SelectedCard.physicsBody.Position = 
                				new Vector2(world.X,world.Y) / GamePhysics.PtoM;
					groups[0].addCard(SelectedCard);
				}
			}
			// Drag
			else if (IsTouch && WasTouch) {
				if ( SelectedCard != null ) {
					SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;
				}
			}
			// Release
			else if (SelectedCard != null && touch.Release) {
				SelectedCard.physicsBody.Velocity = -moved.Normalize();
				SelectedCard = null;
				FirstAttachedCard = null;
				SecondAttachedCard = null;
			}
			
			if ( SelectedCard != null ) {
				float distance;
				float closestDistance = 0.0f;
				Card closest = null;
				foreach ( Card c in cards ) {
					if ( c != null && c != SelectedCard && c != FirstAttachedCard && c != SecondAttachedCard) {
						distance = Vector2.Distance(c.Position, SelectedCard.Position);
						if (closestDistance == 0.0f || closestDistance > distance) {
							closestDistance = distance;
							closest = c;
						}
					}
				}
				System.Console.WriteLine("Distance: " + closestDistance);
				if ( closestDistance < 50f ) {
					if ( FirstAttachedCard == null ) {
						System.Console.WriteLine ("Snap!");
//						closest.physicsBody.Position = new Vector2(world.X-30f,world.Y-20f) / GamePhysics.PtoM;
						FirstAttachedCard = closest;
						groups[0].addCard(closest);
					} else if ( SecondAttachedCard == null ) {
//						closest.physicsBody.Position = new Vector2(world.X+30f,world.Y-20f) / GamePhysics.PtoM;
						SecondAttachedCard = closest;
						groups[0].addCard (closest);
					}
				}
				if ( FirstAttachedCard != null ) {
					FirstAttachedCard.physicsBody.Position = new Vector2(world.X-30f,world.Y-20f) / GamePhysics.PtoM;
				}
				if ( SecondAttachedCard != null ) {
					SecondAttachedCard.physicsBody.Position = new Vector2(world.X+30f,world.Y-20f) / GamePhysics.PtoM;
				}
			}
		}
		
        public Card GetCardAtPosition(Vector2 position) {
			foreach ( Card c in cards ) {
				var halfDimensions = new Vector2(c.CalcSizeInPixels().X/2f, c.CalcSizeInPixels().Y/2f);
				var lowerLeft = new Vector2(c.Position.X-halfDimensions.X, c.Position.Y+halfDimensions.Y);
				var upperRight = new Vector2(c.Position.X+halfDimensions.X, c.Position.Y-halfDimensions.Y);
			
				if (position.X > lowerLeft.X && position.Y < lowerLeft.Y &&
			    	position.X < upperRight.X && position.Y > upperRight.Y) {
					return c;
				}
			}
			return null;                            
		}
		
        ~GameScene(){
           // _pongBlipSoundPlayer.Dispose();
        }
    }
}
