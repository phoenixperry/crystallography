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
    	public static GamePhysics _physics;
		private static CardData[] currentLevelData;
		
		public bool WasTouch;
		public bool IsTouch;
		public static Card SelectedCard;
		public static Card FirstAttachedCard;
		public static Card SecondAttachedCard;
		public Vector2 TouchStart;
		
		public Node n = new Node();
		public Node childN1 = new Node();
		public Node childN2 = new Node();
		public Node childN3 = new Node();
		
		public SpriteUV s;
		
        // Change the following value to true if you want bounding boxes to be rendered
        private static Boolean DEBUG_BOUNDINGBOXS = false;
        
        public GameScene ()
		{
            this.Camera.SetViewFromViewport();
            _physics = new GamePhysics();

            var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			System.Random rand = new System.Random();
			
			currentLevelData = LevelData.LEVEL_DATA[LevelData.CURRENT_LEVEL];
			cards = new Card[currentLevelData.Length];
			for (int i = 0; i < cards.Length; i++) {
				Vector2 start_pos = new Vector2(50f + 0.75f * _screenWidth * (float)rand.NextDouble(), 
				                                50f + 0.75f * _screenHeight * (float)rand.NextDouble ());
				cards[i] = new Card(_physics.addCardPhysics(start_pos), currentLevelData[i]);
				this.AddChild (cards[i]);
			}
			
			groups = new Group[cards.Length];
			
			for (int i=0; i<cards.Length; i++) {
				groups[i] = new Group();
			}
			
//			Console.WriteLine("Possible Sets Remain: " + setsPossible(cards));
			
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
                };
            }
			n.Pivot = new Vector2(0.5f, 0.5f);
//			n.AdHocDraw += () => {
//				Director.Instance.DrawHelpers.DrawCircle( new Vector2(0.5f, 0.5f), 1, 16);
//			};
			n.Position = new Vector2(100f, 100f);
			n.Visible = false;
			childN1.Pivot = new Vector2(0.5f, 0.5f);
//			childN1.AdHocDraw += () => {
//				Director.Instance.DrawHelpers.DrawCircle( new Vector2 (0.5f,0.5f) , 1, 16);
//			};
			
			childN2.Pivot = new Vector2(0.5f, 0.5f);
//			childN2.AdHocDraw += () => {
//				Director.Instance.DrawHelpers.DrawCircle( new Vector2(0.5f, 0.5f), 1, 16);
//			};
			
			childN3.Pivot = new Vector2(0.5f, 0.5f);
//			childN3.AdHocDraw += () => {
//				Director.Instance.DrawHelpers.DrawCircle( new Vector2(0.5f, 0.5f), 1, 16);
//			};
			
			n.AddChild (childN1);
			n.AddChild (childN2);
			n.AddChild (childN3);
			this.AddChild(n);
			
			
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
        }
        
		public override void OnEnter ()
        {
			base.OnEnter();
        }
		
        public override void OnExit ()
        {
			base.OnExit();
        }
		
        public override void Update (float dt)
        {
            base.Update (dt);
            
            //TODO: support multitouch controls
			Touch.GetData(0); 
			foreach (var touchData in Touch.GetData(0)) {
            	if (touchData.Status == TouchStatus.Down ||
                	touchData.Status == TouchStatus.Move) {
					
				}
			}
			
            //We don't need these, but sadly, the Simulate call does.
            Vector2 dummy1 = new Vector2();
            Vector2 dummy2 = new Vector2();
			
            //PHYSICS UPDATE CALL
            _physics.Simulate(-1,ref dummy1,ref dummy2);
            
			//INPUT UPDATE CALL
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
			
			if ( SelectedCard != null ) {
				SelectedCard.physicsBody.Position = childN1.LocalToWorld(childN1.Position) / GamePhysics.PtoM;
//				SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;
			}
			if ( FirstAttachedCard != null ) {
				FirstAttachedCard.physicsBody.Position = childN2.LocalToWorld(childN2.Position) / GamePhysics.PtoM;
//				SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;
			}
			if ( SecondAttachedCard != null ) {
				SecondAttachedCard.physicsBody.Position = childN3.LocalToWorld(childN3.Position) / GamePhysics.PtoM;
//				SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;
			}
			
			// New Touch Starting This Frame
			if (IsTouch && !WasTouch) {
				//TEST
				n.Visible = childN1.Visible = childN2.Visible = childN3.Visible = true;
				n.Position = new Vector2(world.X, world.Y);
				childN1.RunAction(new MovePivotTo(new Vector2(0.5f, -35.5f), 0.5f));
				childN1.RunAction(new MoveTo(new Vector2(0.5f, 35.5f), 0.5f));
//				RotateBy rot = new RotateBy(new Vector2(1f,0f).Rotate(-90.0f), 1.0f) 
//											{ Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear };
//				childN1.RunAction (new RepeatForever() { InnerAction = rot });
				
				childN2.RunAction(new MovePivotTo(new Vector2(0f, -36.0f).Rotate(0.7853981f) + new Vector2(0.5f,0.5f), 0.5f));
				childN2.RunAction(new MoveTo(new Vector2(0f, 35.0f).Rotate(0.7853981f) + new Vector2(0.5f,0.5f), 0.5f));
//				rot = new RotateBy(new Vector2(1f,0f).Rotate(-90.0f), 1.0f) 
//											{ Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear };
//				childN2.RunAction (new RepeatForever() { InnerAction = rot });
				
				childN3.RunAction(new MovePivotTo(new Vector2(0f, -36.0f).Rotate(-0.7853981f) + new Vector2(0.5f,0.5f), 0.5f));
				childN3.RunAction(new MoveTo(new Vector2(0f, 35.0f).Rotate(-0.7853981f) + new Vector2(0.5f,0.5f), 0.5f));
//				rot = new RotateBy(new Vector2(1f,0f).Rotate(-90.0f), 1.0f) 
//											{ Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear };
//				childN3.RunAction (new RepeatForever() { InnerAction = rot });
				// END TEST
				
				if (card != null) {
					TouchStart = world;
					SelectedCard = card;
					SelectedCard.physicsBody.Position = 
                				new Vector2(world.X,world.Y) / GamePhysics.PtoM;
//								new Vector2(childN1.Position.X,childN1.Position.Y) / GamePhysics.PtoM;
					// Start new group if necessary
					if (SelectedCard.groupID == -1 ) {
						SelectedCard.groupID = Array.FindIndex(groups, IsGroupFree);
						groups[SelectedCard.groupID].tryAddingCard(SelectedCard);
					}
					playSound(card.cardData);
					
					SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;;
					
//					SelectedCard.RunAction (new MovePivotTo(new Vector2(0.5f, -1.5f), 0.5f));
//					rot = new RotateBy(new Vector2(1f,0f).Rotate(-90.0f), 2.0f) 
//											{ Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear };
//					SelectedCard.RunAction (new RepeatForever() { InnerAction = rot });
				}
			}
			// On Drag
			else if (IsTouch && WasTouch) {
				//TEST
				n.Position = new Vector2(world.X, world.Y);
				//END TEST
				
//				if ( SelectedCard != null ) {
//					SelectedCard.physicsBody.Position = childN1.LocalToWorld(childN1.Position) / GamePhysics.PtoM;
////					SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;
//				}
//				if ( FirstAttachedCard != null ) {
//					FirstAttachedCard.physicsBody.Position = childN2.LocalToWorld(childN2.Position) / GamePhysics.PtoM;
////					SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;
//				}
//				if ( SecondAttachedCard != null ) {
//					SecondAttachedCard.physicsBody.Position = childN3.LocalToWorld(childN3.Position) / GamePhysics.PtoM;
////					SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;
//				}
			}
			// On Release
			else if (SelectedCard != null && touch.Release) {
//				SelectedCard.StopAllActions();
//				SelectedCard.RunAction (new RotateTo(new Vector2(1f,0f).Rotate (0f),0.25f)
//				                       { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear });
//				SelectedCard.RunAction ( new MovePivotTo(new Vector2(0.5f, 0.5f), 0.5f));
//				if (FirstAttachedCard != null) {
//					FirstAttachedCard.StopAllActions();
//					FirstAttachedCard.RunAction (new RotateTo(new Vector2(1f,0f).Rotate (0f),0.25f)
//					                             { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear });
//					FirstAttachedCard.RunAction ( new MovePivotTo(new Vector2(0.5f, 0.5f), 0.5f));
//				}
//				if (SecondAttachedCard != null) {
//					SecondAttachedCard.StopAllActions();
//					SecondAttachedCard.RunAction (new RotateTo(new Vector2(1f,0f).Rotate (0f),0.25f)
//					                              { Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear });
//					SecondAttachedCard.RunAction ( new MovePivotTo(new Vector2(0.5f, 0.5f), 0.5f));
//				}
				
				Group g = groups[SelectedCard.groupID];
			
				Sequence sequence = new Sequence();
				sequence.Add ( new DelayTime() { Duration = 0.26f } );
				sequence.Add ( new CallFunc(() => TestForCube()));
				this.RunAction (sequence);
				
//				if (g.population == 3) {
//					Card[] triad = new Card[3];
//					Array.Copy(g.cards, triad, 3);
//					if ( g.evaluateCompleteGroup() ) {
//						for (int i=0; i<3; i++) {
//							Support.SoundSystem.Instance.Play("cubed.wav");
//							cards = removeCardFromDeck (triad[i], cards);
//						}
//						if(!setsPossible(cards)) {
//							Sequence sequence = new Sequence();
//							sequence.Add( new DelayTime() { Duration = 1.0f } );
//							sequence.Add( new CallFunc(() => this.goToNextLevel()));
//							this.RunAction(sequence);
//						}
//					} else {
////						sndWrongPlayer.Play ();
//						Support.SoundSystem.Instance.Play("wrong.wav");
//					}
//					
//				}
//				SelectedCard = null;
//				FirstAttachedCard = null;
//				SecondAttachedCard = null;
			}
			
			// TEST
			if (touch.Release)
			{
				n.StopAllActions();
				childN1.StopAllActions();
				childN2.StopAllActions();
				childN3.StopAllActions();
				childN1.RunAction( new MovePivotTo( new Vector2(0.5f, 0.5f), 0.5f));
				childN1.RunAction( new MoveTo( new Vector2(0f, 7.5f), 0.5f));
				childN2.RunAction( new MovePivotTo( new Vector2(0.5f, 0.5f), 0.5f));
				childN2.RunAction( new MoveTo( new Vector2(-5f, -1.5f), 0.5f));
				childN3.RunAction( new MovePivotTo( new Vector2(0.5f, 0.5f), 0.5f));
				childN3.RunAction( new MoveTo( new Vector2(5f, -1.5f), 0.5f));
			}
			// END TEST
			
			if ( SelectedCard != null ) {
				float distance;
				float closestDistance = 0.0f;
				Card closest = null;
				foreach ( Card c in cards ) {
					if ( c != null && c != SelectedCard && c != FirstAttachedCard && c != SecondAttachedCard) {
//						distance = Vector2.Distance(c.Position, SelectedCard.Position);
						distance = Vector2.Distance(world, c.Position);
						if (closestDistance == 0.0f || closestDistance > distance) {
							closestDistance = distance;
							closest = c;
						}
					}
				}
				if ( closestDistance < 50f ) {
					if ( FirstAttachedCard == null ) {
						FirstAttachedCard = closest;
						playSound(closest.cardData);
						groups[SelectedCard.groupID].tryAddingCard(closest);
					} else if ( SecondAttachedCard == null ) {
						SecondAttachedCard = closest;
						playSound(closest.cardData);
						groups[SelectedCard.groupID].tryAddingCard (closest);
					}
					SelectedCard = groups[SelectedCard.groupID].cards[0];
				}
			}
		}
		
		private static void playSound(CardData data)
		{
			switch((int)data.sound)
 				{
					case (int)CardData.SOUND.A:
						Support.SoundSystem.Instance.Play("sound1.wav");
 						break;
 					case (int)CardData.SOUND.B:
						Support.SoundSystem.Instance.Play("sound2.wav");
 						break;
 					case (int)CardData.SOUND.C:
						Support.SoundSystem.Instance.Play("sound3.wav");
 						break;
 					default:
 					break;
 				}	
		}
		
		private static bool IsGroupFree(Group g) {
			return (g.cards[0] == null);
		}
		
		private static void TestForCube()
		{
			Group g = groups[SelectedCard.groupID];
			if (g.population == 3) {
				Card[] triad = new Card[3];
				Array.Copy(g.cards, triad, 3);
				if ( g.evaluateCompleteGroup() ) {
					for (int i=0; i<3; i++) {
						Support.SoundSystem.Instance.Play("cubed.wav");
						cards = removeCardFromDeck (triad[i], cards);
					}
					if(!setsPossible(cards)) {
						Sequence sequence = new Sequence();
						sequence.Add( new DelayTime() { Duration = 1.0f } );
						sequence.Add( new CallFunc(() => GameScene.goToNextLevel()));
						Director.Instance.CurrentScene.RunAction(sequence);
					}
				} else {
//					sndWrongPlayer.Play ();
					Support.SoundSystem.Instance.Play("wrong.wav");
				}
				
			}
			SelectedCard = null;
			FirstAttachedCard = null;
			SecondAttachedCard = null;
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
		
		public static void goToNextLevel()
		{
			LevelData.CURRENT_LEVEL++;
			if (LevelData.CURRENT_LEVEL < LevelData.LEVEL_DATA.Length) { // THERE ARE STILL MORE LEVELS
				Director.Instance.ReplaceScene(new GameScene());
			} else { // ALL LEVELS COMPLETED
				Director.Instance.ReplaceScene(new TitleScene());
			}
		}			

		public static bool setsPossible( Card[] deck )
		{			
			int len = ((Array)deck).Length;
			if ( len < 3 ) {
				return false;
			}
			
			Card[] triad = new Card[3];
			
			for (int i=0; i < len-2; i++) {
				for (int j=i+1; j < len-1; j++) {
					for (int k=j+1; k < len; k++) {
						triad[0] = deck[i];
						triad[1] = deck[j];
						triad[2] = deck[k];
						if (Group.analyze(triad)) {
							Console.WriteLine("Possible Sets Remain: TRUE");
							return true;
						}
					}
				}
			}
			Console.WriteLine("Possible Sets Remain: FALSE");
			return false;
		}
		
		public static Card[] removeCardFromDeck(Card c, Card[] deck) 
		{
			int len = ((Array)deck).Length;
			int i = Array.IndexOf (deck,c);
			deck[i] = null;
			if (i != len-1) {
				for (int j=i; j < len-1; j++) {
					deck[j] = deck[j+1];
					deck[j+1] = null;
					Director.Instance.CurrentScene.RemoveChild(c, true);
				}
			}
			GameScene._physics.removePhysicsBody(c.physicsBody);
			Card[] newDeck = new Card[len-1];
			Array.Copy(deck, newDeck, len-1);
			return newDeck;
		}
		
        ~GameScene(){
           // _pongBlipSoundPlayer.Dispose();
        }
    }
}
