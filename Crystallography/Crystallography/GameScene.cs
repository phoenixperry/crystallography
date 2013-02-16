using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Graphics;

namespace Crystallography
{
    public class GameScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
    {
		public static Card[] cards;
		public static Group[] groups;
    	private GamePhysics _physics;
    	private SoundPlayer _pongBlipSoundPlayer;
   		private Sound _pongSound;
		
		
		public bool WasTouch;
		public bool IsTouch;
		public Card SelectedCard;
		public Card FirstAttachedCard;
		public Card SecondAttachedCard;
		public Vector2 TouchStart;
		
		public SpriteUV s;
		
		//singleton vars 
//		private SpriteSingleton instance; 
//		private SpriteTile spriteTile; 
//		private string spriteName; 
        
        // Change the following value to true if you want bounding boxes to be rendered
        private static Boolean DEBUG_BOUNDINGBOXS = true;
        
        public GameScene ()
        {
            this.Camera.SetViewFromViewport();
            _physics = new GamePhysics();

//            instance = SpriteSingleton.getInstance(); 
//			setFace(); 
//			spriteTile = instance.Get (spriteName); 
//			instance = SpriteSingleton.getInstance(); 
//			spriteTile = instance.Get ("topSide"); 
//			spriteTile.Position = new Vector2(100.0f, 100.0f); 
//			this.AddChild(spriteTile); 
//			setColor(); 			
				
//            ball = new Card(_physics.SceneBodies[(int)GamePhysics.BODIES.Ball]);
//            _player = new Paddle(Paddle.PaddleType.PLAYER, 
//                                 _physics.SceneBodies[(int)GamePhysics.BODIES.Player]);
//            _ai = new Paddle(Paddle.PaddleType.AI, 
//                             _physics.SceneBodies[(int)GamePhysics.BODIES.Ai]);
//            _scoreboard = new Scoreboard();
			
            var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			System.Random rand = new System.Random();
			
			cards = new Card[20];
			for (int i = 0; i < 20; i++) {
//				cards[i] = new Card(_physics.SceneBodies[(int)GamePhysics.BODIES.Ball]);
				cards[i] = new Card(_physics.addCardPhysics(new Vector2(50f + 0.75f * _screenWidth * (float)rand.NextDouble(), 50f + 0.75f * _screenHeight * (float)rand.NextDouble ())));
				switch(rand.Next(1,4))
				{
					case 1:
						cards[i].Color = Colors.Red;
						break;
					case 2:
						cards[i].Color = Colors.White;
						break;
					case 3:
						cards[i].Color = Colors.LightBlue;
						break;
					default:
						cards[i].Color = Colors.LightBlue;
					break;
				}
				
				this.AddChild (cards[i]);
			}
			
//			Cube cube = new Cube(new Card[] {cards[0], cards[1], cards[2] }, null);
//			cube.Position = new Vector2(100f,100f);
//			this.AddChild(cube);
			
//			cards[0].Color = Colors.Red;
				
//				FrameBuffer fb = new FrameBuffer();
//				Texture2D destination = new Texture2D(256, 256, false, PixelFormat.Rgba,PixelBufferOption.Renderable);
//				fb.SetColorTarget(destination,0);
//				
//				Director.Instance.GL.Context.SetFrameBuffer(fb);
//				Director.Instance.GL.Context.SetClearColor(0,0,0,255);
//				Director.Instance.GL.Context.Clear();
//				Director.Instance.GL.Context.SetTexture(0,cards[0].TextureInfo.Texture);
//
//				Director.Instance.GL.Context.SetFrameBuffer(Director.Instance.GL.Context.Screen);
//				Director.Instance.GL.Context.SetClearColor(0,0,0,0);
//				
//				s = new SpriteUV();
//				s.TextureInfo = new TextureInfo(destination);
//				s.Position = new Vector2(100,100);
//				s.Scale = s.CalcSizeInPixels()/4f;
//				this.AddChild(s);
				
//				s = new SpriteUV( new TextureInfo( 
//				                                  new Sce.PlayStation.Core.Graphics.Texture2D(100,100,false,
//				                                            Sce.PlayStation.Core.Graphics.PixelFormat.Rgba) ) );
				
//				Sce.PlayStation.HighLevel.UI.ImageAsset img = new Sce.PlayStation.HighLevel.UI.ImageAsset(cards[0].TextureInfo.Texture);
//				Sce.PlayStation.Core.Imaging.Image img = new Sce.PlayStation.Core.Imaging.Image(Sce.PlayStation.Core.Imaging.ImageMode.Rgba, new Sce.PlayStation.Core.Imaging.ImageSize(100,100), new Sce.PlayStation.Core.Imaging.ImageColor(255,255,255,255));
//				Director.Instance.GL.SetBlendMode(Sce.PlayStation.HighLevel.GameEngine2D.Base.BlendMode.Additive);
//				Director.Instance.GL.Context.Enable (EnableMode.Blend);
//				Director.Instance.GL.Context.SetBlendFunc();
//				Director.Instance.GL.Context.SetBlendFuncAlpha(Sce.PlayStation.Core.Graphics.BlendFuncMode.Add);
//			Image img = new Image("Application/assets/images/topSide.png");
//			Image img2 = new Image("Application/assets/images/leftSide.png");
//			img.Decode ();
//			img2.Decode ();
//			Image img4 = new Image(ImageMode.Rgba, new ImageSize(168,146), img.ToBuffer());
//			Image img5 = new Image(ImageMode.Rgba, new ImageSize(168,146), img2.ToBuffer());
//			img5.DrawImage(img4, new ImagePosition(0,50));	
				
				
//				Image img3 = new Image("Application/assets/images/rightSide.png");
//				img.Decode();
//				img2.Decode();
//				img2 = new Image(ImageMode.Rgba, new ImageSize(168, 146), img2.ToBuffer());
//				img3.Decode();
//				Director.Instance.GL.Context.Enable (Sce.PlayStation.Core.Graphics.EnableMode.Blend);
//				Director.Instance.GL.Context.SetBlendFunc(new Sce.PlayStation.Core.Graphics.BlendFunc);
//				Director.Instance.GL.Context.SetBlendFuncAlpha(Sce.PlayStation.Core.Graphics.BlendFuncMode.Add);
//				Director.Instance.GL.Context.ReadPixels(
//				img.DrawImage(img2, new ImagePosition(0,50));
//				Director.Instance.GL.SetBlendMode(Sce.PlayStation.HighLevel.GameEngine2D.Base.BlendMode.None);
//				img.DrawImage(img3, new ImagePosition(0,0));
//				System.Console.WriteLine(img.DecodeSize.Width + " " + img.DecodeSize.Height);
//				System.Console.WriteLine(img.Size.Width + " " + img.Size.Height);
//				img.Resize(new ImageSize(100,100));
				
//			Sce.PlayStation.Core.Graphics.Texture2D t = new Sce.PlayStation.Core.Graphics.Texture2D(168,146,false,Sce.PlayStation.Core.Graphics.PixelFormat.Rgba);
//			Byte[] source = Director.Instance.GL.Context.ReadPixels(cards[0].TextureInfo.Texture,0,0,0,0,0,168,146);
			
//			Byte[] data = img.ToBuffer ();
//			int[] color = new int[] { (int)(cards[0].Color.X * 255f), (int)(cards[0].Color.Y * 255f), (int)(cards[0].Color.Z * 255f) };
//			
//			for (int j=0; j < data.Length/4; j++) {
//				if (data[j*4+3] != 0) {
//					data[j*4] = (Byte)color[0];
//					data[j*4+1] = (Byte)color[1];
//					data[j*4+2] = (Byte)color[2];
//					var x = j%168;
//					var y = (j-x)/168;
//					t.SetPixels(0,data,PixelFormat.Rgba,j*4,168*4,x,y,1,1);
//				}
//			}
//				
//			data = img2.ToBuffer ();
//			
//			for (int j=0; j < data.Length/4; j++) {
//				if (data[j*4+3] != 0) {
//					var x = j%168;
//					var y = (j-x)/168;
//					t.SetPixels(0,data,PixelFormat.Rgba,j*4,168*4,x,y,1,1);
//				}
//			}
//			
////				t.SetPixels(0,img5.ToBuffer());
////				t.SetPixels (1,img2.ToBuffer ());
////				Sce.PlayStation.HighLevel.UI.ImageAsset img = new Sce.PlayStation.HighLevel.UI.ImageAsset(cards[0].TextureInfo.Texture);
//				s = new SpriteUV( new TextureInfo(t) );
//				s.Position = new Vector2(100f,100f);
//				s.Scale = s.CalcSizeInPixels()/4f;
//				img.Dispose();
//				this.AddChild(s);
			
			groups = new Group[20];
			
			for (int i=0; i<20; i++) {
				groups[i] = new Group();//_physics.addGroupPhysics(new Vector2(400f,400f)));
//				this.AddChild (groups[i]);
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
					// Start new group if necessary
					if (SelectedCard.groupID == -1 ) {
						SelectedCard.groupID = Array.FindIndex(groups, IsGroupFree);
						groups[SelectedCard.groupID].tryAddingCard(SelectedCard);
					}
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
//				SelectedCard.physicsBody.Velocity = -moved.Normalize();
				Group g = groups[SelectedCard.groupID];
				if (g.complete) {
					g.analyze();
				}
//				g.clearGroup();
//				Array.Clear(g.cards,0,g.cards.Length);
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
//				System.Console.WriteLine("Distance: " + closestDistance);
				if ( closestDistance < 50f ) {
					if ( FirstAttachedCard == null ) {
						FirstAttachedCard = closest;
//						FirstAttachedCard.groupID = SelectedCard.groupID;
						groups[SelectedCard.groupID].tryAddingCard(closest);
					} else if ( SecondAttachedCard == null ) {
						SecondAttachedCard = closest;
//						SecondAttachedCard.groupID = SelectedCard.groupID;
						groups[SelectedCard.groupID].tryAddingCard (closest);
					}
					SelectedCard = groups[SelectedCard.groupID].cards[0];
				}
//				if ( FirstAttachedCard != null ) {
//					FirstAttachedCard.physicsBody.Position = new Vector2(world.X-12f,world.Y-18f) / GamePhysics.PtoM;
//				}
//				if ( SecondAttachedCard != null ) {
//					SecondAttachedCard.physicsBody.Position = new Vector2(world.X+10f,world.Y-18f) / GamePhysics.PtoM;
//				}
			}
		}
		
		private static bool IsGroupFree(Group g) {
			return (g.cards[0] == null);
		}
		
        public Card GetCardAtPosition(Vector2 position) {
			foreach ( Card c in cards ) {
				if ( c == null ) continue;
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
//		 		private void setColor() {
// 			System.Random rand = new System.Random(); 
// 			switch(rand.Next(1,4)) 
// 			{
// 				case 1: 
// 				//pink
// 					spriteTile.RunAction(new TintTo (new Vector4(0.96f,0.88f,0.88f,1.0f),0.1f)); 
// 					break;
// 				case 2:
// 				//red 
// 					spriteTile.RunAction(new TintTo (new Vector4(0.90f,0.075f,0.075f,1.0f),0.1f)); 
// 					break;
// 				case 3: 	
// 				//teal
// 					spriteTile.RunAction(new TintTo (new Vector4(0.16f,0.88f,0.88f,1.0f),0.1f)); 	
// 					break; 
// 				default:
// 					break; 
// 			}
// 		}
 		
// 		public void setFace() {
// 			System.Random rand = new System.Random();
// 			switch(rand.Next(1,4))
// 				{
// 					case 1:
// 						spriteName = "leftSide";
// 						break;
// 					case 2:
// 						spriteName = "rightSide";
// 						break;
// 					case 3:
// 						spriteName = "topSide";
// 						break;
// 					default:
// 					break;
// 		}
// 		}			

		
        ~GameScene(){
           // _pongBlipSoundPlayer.Dispose();
        }
    }
}
