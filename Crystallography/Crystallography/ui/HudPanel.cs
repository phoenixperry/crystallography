using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class HudPanel : Layer
	{
		public float Height {get; set;}
		public float Width {get; set;}
		public float Lifetime {get; set;}
		public float DismissDelay {get; set;}
		public Node SourceObject {get; set;}
		public Vector2 Offset {get; set;}
		public SlideDirection SlideInDirection {get; set;}
		public SlideDirection SlideOutDirection {get; set;}
		
		public event EventHandler OnSlideInStart;
		public event EventHandler OnSlideOutStart;
		public event EventHandler OnSlideInComplete;
		public event EventHandler OnSlideOutComplete;
		
		// CONSTRUCTOR ----------------------------------------------------------------------------------------------------
		
		public HudPanel () {
			Offset = Vector2.Zero;
			Lifetime = 0.0f;
			DismissDelay = 0.1f;
			SlideInDirection = SlideDirection.DOWN;
			SlideOutDirection = SlideDirection.UP;
			Visible = false;
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS -------------------------------------------------------------------------------------------------
		
		void HandleInputManagerInstanceTapDetected (object sender, BaseTouchEventArgs e)
		{
			Dismiss();
		}
		
		void HandleInputManagerInstanceDragDetected (object sender, SustainedTouchEventArgs e)
		{
			Dismiss();
		}
		
		// OVERRIDES ------------------------------------------------------------------------------------------------------
		
		// METHODS --------------------------------------------------------------------------------------------------------
		
		protected void AllowDismiss() {
			InputManager.Instance.TapDetected += HandleInputManagerInstanceTapDetected;
			InputManager.Instance.DragDetected += HandleInputManagerInstanceDragDetected;
		}
		
		public void Dismiss() {
			this.StopAllActions();
			SlideOut();
		}
		
		public void Reset() {
			Position = Offset;
			if (SourceObject != null ) {
				Position += SourceObject.Position;
			}
		}
		
		public void SlideIn() {
			SlideIn (SlideInDirection);
		}
		
		public void SlideIn(SlideDirection pDirection) {
			Position = Offset;
			if( SourceObject != null ) {
				Position += SourceObject.Position;
			}
			
			Vector2 Destination;
			
			switch(pDirection){
			case(SlideDirection.LEFT):
				Destination = new Vector2(Position.X - Width, Position.Y);
				break;
			case(SlideDirection.RIGHT):
				Position += new Vector2(-Width, 0.0f);
				Destination = new Vector2(Position.X + Width, Position.Y);
				break;
			case(SlideDirection.UP):
				Position += new Vector2(0.0f, -Height);
				Destination = new Vector2(Position.X, Position.Y + Height);
				break;
			case(SlideDirection.DOWN):
			default:
				Destination = new Vector2(Position.X, Position.Y - Height);
				break;
			}
			
			Sequence baseSequence = new Sequence();
			baseSequence.Add( new CallFunc( () => {
				Visible = true;
				EventHandler handler = OnSlideInStart;
				if ( handler != null ) {
					handler( this, null );
				}
			} ) );
			baseSequence.Add( new MoveTo( Destination, 1.0f) );
			baseSequence.Add( new CallFunc( () => {
				EventHandler handler = OnSlideInComplete;
				if ( handler != null ) {
					handler( this, null );
				}
			} ) );
			this.RunAction( baseSequence );
			
			if (DismissDelay > 0.0f) {
				Sequence sequence = new Sequence();
				sequence.Add( new DelayTime(DismissDelay) );
				sequence.Add( new CallFunc( () => {AllowDismiss (); } ) );
				this.RunAction(sequence);
			}
			
			if (Lifetime > 0.0f) {
				Sequence sequence = new Sequence();
				sequence.Add( new DelayTime(1.0f + Lifetime) );
				sequence.Add( new CallFunc( () => {SlideOut(); } ) );
				this.RunAction(sequence);
			}
		}
		
		public void SlideOut() {
			SlideOut (SlideOutDirection);
		}
		
		public void SlideOut(SlideDirection pDirection) {
			InputManager.Instance.TapDetected -= HandleInputManagerInstanceTapDetected;
			InputManager.Instance.DragDetected -= HandleInputManagerInstanceDragDetected;
			
			Vector2 Destination;
			
			Destination = Offset;
			if (SourceObject != null) {
				Destination += SourceObject.Position;
			}
			
			switch(pDirection){
			case(SlideDirection.LEFT):
				Destination += new Vector2(-Width, 0.0f);
				break;
			case(SlideDirection.DOWN):
				Destination += new Vector2(0.0f, -Height);
				break;
			default:
				break;
			}
			
			Sequence sequence = new Sequence();
			sequence.Add( new CallFunc( () => {
				EventHandler handler = OnSlideOutStart;
				if ( handler != null ) {
					handler( this, null );
				}
			} ) );
			sequence.Add( new MoveTo( Destination, 1.0f) );
			sequence.Add( new CallFunc( () => {
//				Visible=false;
				EventHandler handler = OnSlideOutComplete;
				if ( handler != null ) {
					handler( this, null );
				}
			} ) );
			
			this.RunAction(sequence);
		}
		
		// DESTRUCTOR -----------------------------------------------------------------------------------------------------
#if DEBUG
        ~HudPanel() {
			Console.WriteLine("HudPanel deleted.");
        }
#endif
	}
	
	public enum SlideDirection {
		LEFT = 0,
		RIGHT,
		UP,
		DOWN
	}
}

