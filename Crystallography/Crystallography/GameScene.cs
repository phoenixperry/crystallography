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
		public Card SelectedCard;
		public Card FirstAttachedCard;
		public Card SecondAttachedCard;
		public Vector2 TouchStart;
		
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
					playSound(card.cardData);
				}
			}
			// On Drag
			else if (IsTouch && WasTouch) {
				if ( SelectedCard != null ) {
					SelectedCard.physicsBody.Position = new Vector2(world.X,world.Y) / GamePhysics.PtoM;
				}
			}
			// On Release
			else if (SelectedCard != null && touch.Release) {
				Group g = groups[SelectedCard.groupID];
				if (g.population == 3) {
					Card[] triad = new Card[3];
//					triad[0] = cards[Array.IndexOf(cards,g.cards[0])];
//					triad[1] = cards[Array.IndexOf(cards,g.cards[1])];
//					triad[2] = cards[Array.IndexOf(cards,g.cards[2])];
					Array.Copy(g.cards, triad, 3);
					if ( g.evaluateCompleteGroup() ) {
						for (int i=0; i<3; i++) {
							Support.SoundSystem.Instance.Play("cubed.wav");
							cards = removeCardFromDeck (triad[i], cards);
						}
						if(!setsPossible(cards)) {
								goToNextLevel();
							}
					} else {
//						sndWrongPlayer.Play ();
						Support.SoundSystem.Instance.Play("wrong.wav");
					}
					
				}
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
						playSound(closest.cardData);
						groups[SelectedCard.groupID].tryAddingCard(closest);
					} else if ( SecondAttachedCard == null ) {
						SecondAttachedCard = closest;
						playSound(closest.cardData);
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
		
		public void goToNextLevel()
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
