using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography {
	public class ButtonEntity : SpriteTileCrystallonEntity {
		
		public const int NORMAL = 0;
		public const int PRESSED = 1;
		public const int DISABLED = 2;
		
		public Label label;
		public Vector2 labelOffset;
		public int status;
		public int frame;
		public Font font;
		public FontMap map;
		
		protected bool _onToggle;
		protected bool _pressed;
		protected bool _initialized;
		protected float halfHeight;
		protected float halfWidth;
		
		public event EventHandler ButtonUpAction;
		
		// GET & SET ------------------------------------------------------------------------------
		
		public bool on { 
			get { return _onToggle; }
			set { 
				_onToggle = value;
				status = _onToggle ? NORMAL : DISABLED;
			}
		}
		
		
		// CONSTRUCTOR ----------------------------------------------------------------------------
		public ButtonEntity ( string pLabel, Scene pScene, GamePhysics pGamePhysics, TextureInfo pTextureInfo, Vector2i pTileIndex2D ) 
							: base ( pScene, pGamePhysics, pTextureInfo, pTileIndex2D, null ) {
			
			halfWidth = Width / 2.0f;
			halfHeight = Height / 2.0f;
			labelOffset = new Vector2(0.1f, 0.25f);
			this.setPivot(0.5f,0.5f);
			
//			font = new Font("Application/assets/fonts/Bariol_Regular.otf", 25, FontStyle.Regular);
			font = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25);
			map = Crystallography.UI.FontManager.Instance.GetMap( font );	
//			map = new FontMap( font );
			if ( pLabel != null ) {
				label = new Label( pLabel, map );
//				label.Color = Colors.White;
				label.Pivot = new Vector2(0.15f, 0.25f );
				label.Scale = new Vector2(1.0f/this.Width,1.0f/this.Height);
//				label.HeightScale = 1.0f/this.Height;
//				label.Position = labelOffset;
			}
			
			this.getNode().AddChild(label);
			
			labelOffset = Vector2.Zero;
			status = NORMAL;
			_onToggle = false;
			_pressed = false;
			_initialized = false;
			
			
			
//			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS -------------------------------------------------------------------------
		
		void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e) {
			var buttonPos = this.getNode().Position;
			if (e.touchPosition.X > buttonPos.X - this.halfWidth && e.touchPosition.X < buttonPos.X + this.halfWidth) {
//				int h = Director.Instance.GL.Context.GetViewport().Height;
//				if (h - e.touchPosition.Y > buttonPos.Y - this.halfHeight && h - e.touchPosition.Y < buttonPos.Y + this.halfHeight) {
				if (e.touchPosition.Y > buttonPos.Y - this.halfHeight && e.touchPosition.Y < buttonPos.Y + this.halfHeight) {
					onButtonUp();
				}
			}
		}
		
		void HandleInputManagerInstanceTouchDownDetected (object sender, SustainedTouchEventArgs e)
		{
			var buttonPos = this.getNode().Position;
			_pressed = false;
			if (e.touchPosition.X > buttonPos.X - this.halfWidth && e.touchPosition.X < buttonPos.X + this.halfWidth) {
//				int h = Director.Instance.GL.Context.GetViewport().Height;
//				if (h - e.touchPosition.Y > buttonPos.Y - this.halfHeight && h - e.touchPosition.Y < buttonPos.Y + this.halfHeight) {
				if (e.touchPosition.Y > buttonPos.Y - this.halfHeight && e.touchPosition.Y < buttonPos.Y + this.halfHeight) {
					onButtonDown();
					_pressed = true;
				}
			}
		}
		
		
		// OVERRIDES ------------------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			preUpdateButton();
			updateButton();
			
//			if (label == null) return;
			
			switch( _sprite.TileIndex1D ) {
			case ButtonEntity.PRESSED:
				label.Color.A = 1.0f;
				break;
			case ButtonEntity.DISABLED:
				label.Color.A = 0.5f;
				break;
			case ButtonEntity.NORMAL:
			default:
				label.Color.A = 1.0f;
				break;
			}
		}
		
		// METHODS --------------------------------------------------------------------------------
		
		public void preUpdateButton() {
			if (!_initialized) {
				InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
				InputManager.Instance.TouchDownDetected += HandleInputManagerInstanceTouchDownDetected;
				_initialized = true;
			}
		}
		
		public void updateButton() {
//			bool offAll = false;
			// If there is a touch right now, compare the location to the button's area
			if ( !_pressed ) {
				if ( status != NORMAL && status != DISABLED ) {
					status = NORMAL;
				}
				
			}
			_sprite.TileIndex1D = status;
		}
		
		protected void onButtonDown() {
			if( !this.Visible || (status != NORMAL) ) {
				return;
			}
			Console.WriteLine("ButtonEntity.ButtonDown");
			status = PRESSED;
			//if (onDown != null) {
			//	onDown();
			//}
		}
		
		protected void onButtonUp() {
			if( !this.Visible || (status != PRESSED) ) {
				return;
			}
			Console.WriteLine("ButtonEntity.ButtonUp");
			status = NORMAL;
			//if (onUp != null) {
			//	onUp();
			//}
			EventHandler handler = ButtonUpAction;
			if ( handler != null ) {
				handler( this, null );
			}
		}
		
		// DESTRUCTOR -----------------------------------------------------------------------------
#if DEBUG
		~ButtonEntity() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
	
}