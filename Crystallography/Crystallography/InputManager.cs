using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class InputManager
	{
		// DOUBLE TAP CONSTANTS
		private static readonly float MAX_PRESS_DURATION = 0.15f;
		private static readonly float MAX_RELEASE_DURATION = 0.3f;
		private static readonly float MAX_TAP_DISTANCE = 50;
		
		private static int tapCount;
		
		protected static InputManager _instance;
		
		public event EventHandler<BaseTouchEventArgs> 		DoubleTapDetected;
		public event EventHandler<BaseTouchEventArgs> 		DragDetected;
		public event EventHandler<BaseTouchEventArgs> 		DragReleaseDetected;
		public event EventHandler<BaseTouchEventArgs> 		TapDetected;
		public event EventHandler<SustainedTouchEventArgs> 	TouchDownDetected;
		public event EventHandler<BaseTouchEventArgs> 		TouchJustDownDetected;
		public event EventHandler<BaseTouchEventArgs> 		TouchJustUpDetected;
		
		// GET & SET --------------------------------------------------------
		
		/// <summary>
		/// Returns the instance of <see cref="Crystallography.InputManager"/>, or creates it, if it doesn't already exist.
		/// </summary>
		public static InputManager Instance {
			get {
				if (_instance == null ) {
					return _instance = new InputManager();
				} else {
					return _instance;
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		public static float lastPressDuration	{ get; protected set; }
		public static float pressDuration		{ get; protected set; }
		public static float releaseDuration		{ get; protected set; }
		public static float tapDistance 		{ get; protected set; }
		public static Vector2 firstTapPosition 	{ get; protected set; }
		
		// CONSTRUCTOR ------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.InputManager"/> class.
		/// </summary>
		protected InputManager () {
			Reset ();
		}
		
		// METHODS ----------------------------------------------------------
		
		/// <summary>
		/// Reset the InputManager.
		/// </summary>
		public void Reset() {
			tapCount = 0;
			pressDuration = 0.0f;
			firstTapPosition = Vector2.Zero;
			lastPressDuration = MAX_PRESS_DURATION;
			releaseDuration = MAX_RELEASE_DURATION;
		}
		
		/// <summary>
		/// The InputManager's update method. Should be called once per frame.
		/// </summary>
		/// <param name='dt'>
		/// Elapsed time since the last frame.
		/// </param>
		public void Update( float dt ) {
			
			if ( Input2.Touch00.Press ) {	// -------------------------------- on new touch
				OnTouchJustDown( new BaseTouchEventArgs {
					touchPosition = Director.Instance.CurrentScene.Camera.NormalizedToWorld( Input2.Touch00.Pos )
				} );
			} else if (Input2.Touch00.On) { // -------------------------------- on sustained touch
				OnTouchDown( new SustainedTouchEventArgs {
					touchPosition = Director.Instance.CurrentScene.Camera.NormalizedToWorld( Input2.Touch00.Pos ),
					elapsed = dt
				} );
			} else if (Input2.Touch00.Release) {	// ------------------------ on new release
				OnTouchJustUp( new BaseTouchEventArgs {
					touchPosition = Director.Instance.CurrentScene.Camera.NormalizedToWorld( Input2.Touch00.Pos )
				} );
			} else {	// ---------------------------------------------------- on sustained release
				releaseDuration += dt;
			}
		}
		
		/// <summary>
		/// Raises the <c>DoubleTapDetected</c> event.
		/// </summary>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.BaseTouchEventArgs"/>
		/// </param>
		protected void OnDoubleTap( BaseTouchEventArgs e ) {
			EventHandler<BaseTouchEventArgs> handler = DoubleTapDetected;
			if ( handler != null ) {
				handler( this, e );
			}
		}
		
		/// <summary>
		/// Raises the <c>TapDetected</c> event or calls OnDoubleTap() as appropriate.
		/// </summary>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.BaseTouchEventArgs"/>
		/// </param>
		protected void OnTap( BaseTouchEventArgs e ) {
			tapCount++;
			if ( releaseDuration < MAX_RELEASE_DURATION ) {
				if ( tapCount > 1 ) {	// ----------------------------------- on double-tap
					OnDoubleTap ( e );
					return;
				}
			}
			EventHandler<BaseTouchEventArgs> handler = TapDetected;
			if ( handler != null ) {
				handler( this, e );
			}
			
		}
		
		/// <summary>
		/// Raises the TouchDownDetected event.
		/// </summary>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.SustainedTouchEventArgs"/>
		/// </param>
		protected void OnTouchDown( SustainedTouchEventArgs e ) {
			pressDuration += e.elapsed;
			EventHandler<SustainedTouchEventArgs> handler = TouchDownDetected;
			if (handler != null ) {
				handler( this, e );
			}
		}
		
		/// <summary>
		/// Raises the TouchJustDownDetected event.
		/// </summary>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.BaseTouchEventArgs"/>
		/// </param>
		protected void OnTouchJustDown( BaseTouchEventArgs e ) {
			pressDuration = 0.0f;
			EventHandler<BaseTouchEventArgs> handler = TouchJustDownDetected;
			if (handler != null ) {
				handler( this, e );
			}
		}
		
		/// <summary>
		/// Raises the TouchJustUpDetected event. Calls OnTap() if appropriate.
		/// </summary>
		/// <param name='e'>
		/// <see cref="Crystallography.InputManager.BaseTouchEventArgs"/>
		/// </param>
		protected void OnTouchJustUp( BaseTouchEventArgs e ) {
			if (pressDuration < MAX_PRESS_DURATION ) {	// ------------------- on tap
				OnTap( e );
			} else {
				tapCount = 0;
			}
			lastPressDuration = pressDuration;
			releaseDuration = 0.0f;
			EventHandler<BaseTouchEventArgs> handler = TouchJustUpDetected;
			if (handler != null ) {
				handler( this, e );
			}
		}
		
		// DESTRUCTOR ------------------------------------------------------------------
		
		~InputManager() {
			Instance = null;
		}
	}
	
	// HELPER CLASSES -------------------------------------------------------------------
	
	/// <summary>
	/// Base touch event arguments.
	/// </summary>
	public class BaseTouchEventArgs : EventArgs {
		public Vector2 touchPosition { get; set; }
	}
	
	/// <summary>
	/// Sustained touch event arguments.
	/// </summary>
	public class SustainedTouchEventArgs : BaseTouchEventArgs {
		public float elapsed { get; set; }
	}
}
