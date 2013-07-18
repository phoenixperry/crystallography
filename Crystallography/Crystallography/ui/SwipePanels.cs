using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class SwipePanels : Node
	{
		
		List<AnchorPoint> AnchorPoints;
		List<Node> Panels;
		Vector2 TouchStartPosition;
		Vector2 TouchPosition;
		float TouchVelocity;
		
		public float Width {get; set;}
		
		public event EventHandler OnSwipeStart;
		public event EventHandler OnSwipeComplete;
		
		new public Vector2 Position {
			get {return base.Position;}
			set {
				base.Position = value;
				AnchorPoints[0].Position = value + new Vector2(-Width, 0.0f);
				AnchorPoints[1].Position = value;
				AnchorPoints[2].Position = value + new Vector2( Width, 0.0f);
				foreach(AnchorPoint point in AnchorPoints) {
					if (point.Node == null) {
						continue;
					}
					point.Node.Position = point.Position;
				}
			}
		}
		
		public Node ActivePage {get {return AnchorPoints[1].Node;} }
		public Node LeftPage   {get {return AnchorPoints[0].Node;} }
		public Node RightPage  {get {return AnchorPoints[2].Node;} }
		
		// CONSTRUCTOR --------------------------------------------------------------------------------------------------------------
		
		public SwipePanels (List<Node> pPanels) {
			TouchVelocity = 0.0f;
			Width = Director.Instance.GL.Context.GetViewport().Width;
			Panels = pPanels;
			
			AnchorPoints = new List<AnchorPoint> {
				new AnchorPoint(){
					Index = -1,
					Position = new Vector2(-Width, 0.0f)
				},
				new AnchorPoint(){
					Node = Panels[0],
					Index = 0,
					Position = new Vector2(0.0f, 0.0f)
				},
				new AnchorPoint(){
					Index = 1,
					Position = new Vector2(Width, 0.0f)
				}
			};
			
			Panels[0].Position = AnchorPoints[1].Position;
			
			if (Panels.Count > 1) {
				AnchorPoints[2].Node = Panels[1];
				for(int i=1; i<Panels.Count; i++) {
					Panels[i].Position = AnchorPoints[2].Position;
				}
			}
		}
		
		// EVENT HANDLERS ----------------------------------------------------------------------------------------------------------------
		
		void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e) {
			var LastTouchPosition = TouchPosition.Xy;
			bool AdvancePanel = false;
			TouchPosition = e.touchPosition;
			// SWITCH ACTIVE PANELS IF PANEL MIDPOINT IS OFF THE SCREEN
			if (FMath.Abs(AnchorPoints[1].Node.Position.X - AnchorPoints[1].Position.X) > 0.5f*Width || 
			    FMath.Abs (TouchVelocity * (TouchPosition.X - TouchStartPosition.X)) > 4000.0f) {
				// SWIPE TO RIGHT?
				AdvancePanel = (TouchPosition.X - TouchStartPosition.X) > 0;
				// CHECK IF RIGHTWARD MOVEMENT IS POSSIBLE
				if (AnchorPoints[0].Node != null && AdvancePanel) {
					foreach (AnchorPoint point in AnchorPoints) {
						point.Index--;
						if (point.Index < 0){
							point.Node = null;
						} else {
							point.Node = Panels[point.Index];
						}
					}
				// CHECK IF LEFTWARD MOVEMENT IS POSSIBLE
				} else if (AnchorPoints[2].Node != null && !AdvancePanel) {
					foreach (AnchorPoint point in AnchorPoints) {
						point.Index++;
						if (point.Index == Panels.Count) {
							point.Node = null;
						} else {
							point.Node = Panels[point.Index];
						}
					}
				}
			}
			// MOVE PANELS TO NEW RESTING PLACES
			foreach (AnchorPoint point in AnchorPoints) {
				if (point.Node == null) {
					continue;
				}
				point.Node.RunAction( new MoveTo(point.Position, 0.3f) {
					Tween = (t) => Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.ExpEaseOut(t,4.0f)
				} );
				point.Node.ScheduleUpdate(0);
			}
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime(0.35f) );
			sequence.Add ( new CallFunc( () => {
				EventHandler handler = OnSwipeComplete;
				if ( handler != null ) {
					handler( this, null );
				}
			} ) );
			this.RunAction(sequence);
		}
		
		void HandleInputManagerInstanceTouchDownDetected (object sender, SustainedTouchEventArgs e) {
			var LastTouchPosition = TouchPosition.Xy;
			var LastTouchVelocity = TouchVelocity;
			TouchPosition = e.touchPosition;
			TouchVelocity = 0.5f*(FMath.Abs(TouchPosition.X - LastTouchPosition.X) + LastTouchVelocity);
//			Console.WriteLine("Touch Velocity: {0}", TouchVelocity * (TouchPosition.X - TouchStartPosition.X));
			bool CapLeft = (AnchorPoints[2].Node == null);
			bool CapRight = (AnchorPoints[0].Node == null);
			foreach( AnchorPoint point in AnchorPoints) {
				if (point.Node == null) {
					continue;
				}
				var v = point.Position;
				var d = TouchStartPosition.X - TouchPosition.X;
				if ((d>0 && CapLeft == false) || (d<0 && CapRight == false)) {
					point.Node.Position = new Vector2(v.X - d, v.Y);
				}
			}
		}
		
		void HandleInputManagerInstanceTouchJustDownDetected (object sender, BaseTouchEventArgs e) {
			this.StopAllActions();
			TouchVelocity = 0.0f;
			TouchStartPosition = e.touchPosition;
			TouchPosition = TouchStartPosition.Xy;
			foreach (AnchorPoint point in AnchorPoints) {
				if (point.Node == null) {
					continue;
				}
				point.Node.StopAllActions();
			}
			EventHandler handler = OnSwipeStart;
				if ( handler != null ) {
					handler( this, null );
				}
		}
		
		// OVERRIDES ---------------------------------------------------------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
			InputManager.Instance.TouchDownDetected += HandleInputManagerInstanceTouchDownDetected;
		}
		
		public override void OnExit ()
		{
			InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchDownDetected -= HandleInputManagerInstanceTouchDownDetected;
			InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
			base.OnExit ();
			
			AnchorPoints.Clear();
			Panels.Clear();
		}
		
		// METHODS -----------------------------------------------------------------------------------------------------------------------
		
		
		
		// DESTRUCTOR --------------------------------------------------------------------------------------------------------------------
		
#if DEBUG
		~SwipePanels() {
			Console.WriteLine(GetType().ToString() + " " + "Deleted");
		}
#endif
	}
	
	public class AnchorPoint {
		public Node Node;
		public int Index;
		public Vector2 Position;
		
		~AnchorPoint() {
			this.Node = null;
		}
		
	}
}

