using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class ButtonEntity : SpriteTileCrystallonEntity {
		
		public static readonly Vector2i ButtonSpriteTileIndex2D = new Vector2i(0,0);
		
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
				label.Visible = _onToggle;
				status = (_onToggle ? NORMAL : DISABLED);
			}
		}
		
		
		// CONSTRUCTOR ----------------------------------------------------------------------------
		public ButtonEntity ( string pLabel, Scene pScene, GamePhysics pGamePhysics, TextureInfo pTextureInfo, Vector2i pTileIndex2D ) 
							: base ( pScene, pGamePhysics, pTextureInfo, pTileIndex2D, null ) {
			
			halfWidth = Width / 2.0f;
			halfHeight = Height / 2.0f;
			labelOffset = new Vector2(0.1f, 0.25f);
			this.setPivot(0.5f,0.5f);
			
			font = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 25);
			map = Crystallography.UI.FontManager.Instance.GetMap( font );	
			if ( pLabel != null ) {
				label = new Label( pLabel, map );
				label.Pivot = new Vector2(0.15f, 0.25f );
				label.Scale = new Vector2(1.0f/this.Width,1.0f/this.Height);
			}
			
			this.getNode().AddChild(label);
			
			labelOffset = Vector2.Zero;
			status = NORMAL;
			_onToggle = false;
			_pressed = false;
			_initialized = false;
			
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS -------------------------------------------------------------------------
		
		void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e) {
			if(this.getNode().IsWorldPointInsideContentLocalBounds(e.touchPosition) ) {
				onButtonUp();
			}
		}
		
		void HandleInputManagerInstanceTouchDownDetected (object sender, SustainedTouchEventArgs e) {
			_pressed = false;
			if(this.getNode().IsWorldPointInsideContentLocalBounds(e.touchPosition) ) {
				onButtonDown();
				_pressed = true;
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
			// If there is a touch right now, compare the location to the button's area
			if ( !_pressed ) {
				if ( status != NORMAL && status != DISABLED ) {
					status = NORMAL;
				}
				
			}
			_sprite.TileIndex1D = status;
		}
		
		public void FakePress() {
			var oldStatus = status;
			status = PRESSED;
			onButtonUp();
			status = oldStatus;
		}
		
		protected void onButtonDown() {
			if( !this.Visible || (status != NORMAL) ) {
				return;
			}
			status = PRESSED;
		}
		
		protected void onButtonUp() {
			if( !this.Visible || (status != PRESSED) ) {
				return;
			}
			status = NORMAL;
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