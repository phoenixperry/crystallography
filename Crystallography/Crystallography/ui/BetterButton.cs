using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class BetterButton : Node {
		public static readonly Vector2i ButtonSpriteTileIndex2D = new Vector2i(0,0);
		protected static readonly Vector2 DefaultDimensions = new Vector2(32.0f, 32.0f);
		
		public const int NORMAL = 0;
		public const int PRESSED = 1;
		public const int DISABLED = 2;
		
		public float Height;
		public float Width;
		
		public SpriteTile background;
		protected SpriteTile _icon;
		protected Bounds2 _bounds;
		protected Label _buttonText;
		protected Vector2 _textOffset;
		protected Vector2 _iconAndTextOffset;
		protected Font _textFont;
		protected int _status;
		protected bool _onToggle;
		protected bool _pressed;
		protected bool _initialized;
		protected bool _iconOnLeft = true;
		protected float halfHeight;
		protected float halfWidth;
		
		
		public event EventHandler ButtonUpAction;
		
		
		// GET & SET --------------------------------------------------------------------
		
		public Vector4 Color {
			get { return background.Color; }
			set { background.Color = value; }
		}
		
		public SpriteTile Icon {
			get { return _icon; }
			set {
				if(_icon != null) {
					this.RemoveChild(_icon, true);
				}
				_icon = value;
				if(value != null) {
					_icon.Position = new Vector2( 0.0f, 0.5f * (Height - _icon.CalcSizeInPixels().Y) );
					this.AddChild(_icon);
				}
				CenterText();
			}
		}
		
		public Vector2 IconAndTextOffset {
			get {return _iconAndTextOffset;}
			set {
				_iconAndTextOffset = value;
				CenterText();
			}
		}
		
		public bool IconOnLeft {
			get {return _iconOnLeft;}
			set {
				_iconOnLeft = value;
				CenterText();
			}
		}
		
		public int Status { get {return _status;} }
		
		public string Text {
			get {
				if (_buttonText == null) {
					return "";
				} else {
					return _buttonText.Text;
				}
			}
			set {
				_buttonText.Text = value;
				CenterText();
			}
		}
		
		public Vector4 TextColor{
			get { return _buttonText.Color; }
			set { _buttonText.Color = value; }
		}
		
		public Font TextFont {
			get {
				return _textFont;
			}
			set {
				_textFont = value;
				if (_buttonText != null) {
					_buttonText.FontMap = Crystallography.UI.FontManager.Instance.GetMap( _textFont );
					_buttonText.Position = new Vector2(0.0f, 0.5f * (Height - _buttonText.FontMap.CharPixelHeight) );
				}
				CenterText();
			}
		}
		
		public Vector2 TextOffset {
			get { return _textOffset; }
			set {
				_textOffset = value;
				CenterText();
			}
		}
		
		
		// CONSTRUCTORS ------------------------------------------------------------------
		
		public BetterButton() {}
		
//		public BetterButton (float pWidth, float pHeight, Font pFont) {
//			_textFont = pFont;
//			Initialize(pWidth, pHeight);
//		}
//		
//		public BetterButton( string pBackgroundPath, Font pFont) {
//			_textFont = pFont;
//			background = Support.TiledSpriteFromFile(pBackgroundPath, 1, 3);
//			var size = background.CalcSizeInPixels();
//			Initialize(size.X, size.Y);
//		} 
		
		public BetterButton (string pBackgroundPath) {
			background = Support.TiledSpriteFromFile(pBackgroundPath, 1, 3);
			var size = background.CalcSizeInPixels();
			Initialize(size.X, size.Y);
		}
		
		public BetterButton (float pWidth, float pHeight) : base() {
			Initialize(pWidth, pHeight);
		}
		
		
		// EVENT HANDLERS ----------------------------------------------------------------
		
		void HandleInputManagerInstanceTouchJustDownDetected (object sender, BaseTouchEventArgs e) {
			_pressed = false;
			if(_bounds.IsInside( this.WorldToLocal(e.touchPosition) ) ) {
				OnButtonDown();
				_pressed = true;
			}
		}
		
		void HandleInputManagerInstanceTouchJustUpDetected (object sender, BaseTouchEventArgs e) {
			if( !Visible || (_status != PRESSED) ) {
				return;
			}
			_status = NORMAL;
			if(_bounds.IsInside( this.WorldToLocal(e.touchPosition) ) ) {
				OnButtonUp();
			}
		}
		
		
		// OVERRIDES ---------------------------------------------------------------------
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			InputManager.Instance.TouchJustDownDetected += HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchJustUpDetected += HandleInputManagerInstanceTouchJustUpDetected;
		}
		
		public override void OnExit ()
		{
			background = null;
			_icon = null;
			_buttonText = null;
			_textFont = null;
			base.OnExit ();
			RemoveAllChildren(true);
			InputManager.Instance.TouchJustDownDetected -= HandleInputManagerInstanceTouchJustDownDetected;
			InputManager.Instance.TouchJustUpDetected -= HandleInputManagerInstanceTouchJustUpDetected;
		}
		
		public override void Update (float dt)
		{
			base.Update (dt);
			
			if ( !_pressed ) {
				if ( _status != NORMAL && _status != DISABLED ) {
					_status = NORMAL;
				}
				
			}
			background.TileIndex1D = _status;
			
			switch( background.TileIndex1D ) {
			case BetterButton.PRESSED:
				_buttonText.Color.A = 0.7f;
				if(_icon != null) {
					_icon.Color.A = 0.7f;
				}
				break;
			case BetterButton.DISABLED:
				_buttonText.Color.A = 0.5f;
				if(_icon != null) {
					_icon.Color.A = 0.5f;
				}
				break;
			case BetterButton.NORMAL:
			default:
				_buttonText.Color.A = 1.0f;
				if(_icon != null) {
					_icon.Color.A = 1.0f;
				}
				break;
			}
		}
		
		
		// METHODS -----------------------------------------------------------------------
		
		protected void CenterText() {
			float labelWidth = 0.0f;
			if (_buttonText != null) {
				labelWidth += _buttonText.GetlContentLocalBounds().Size.X;
			}
			float x;
			if( Icon != null) {
				var iconWidth = Icon.CalcSizeInPixels().X;
				labelWidth += iconWidth;
				x = 0.5f * (Width - labelWidth);
				var y = 0.5f * (Height - _icon.CalcSizeInPixels().Y);
				var iconX = 0.0f;
				if(_iconOnLeft) {
					iconX = x;
					x += iconWidth;
				} else {
					iconX = ( Width - 0.5f * (Width - labelWidth) ) - iconWidth;
				}
				Icon.Position = new Vector2(iconX, y) + _iconAndTextOffset;
				
			} else {
				x = 0.5f * (Width - labelWidth);
			}
			if (_buttonText != null) {
				var y = 0.5f * (Height - _buttonText.FontMap.CharPixelHeight);
				_buttonText.Position = new Vector2(x, y) + _textOffset;
			}
		}
		
		public void FakePress() {
			var oldStatus = _status;
			_status = PRESSED;
			OnButtonUp();
			_status = oldStatus;
		}
		
		public void Initialize(float pWidth, float pHeight) {
			Width = pWidth;
			Height = pHeight;
			_textOffset = Vector2.Zero;
			_iconAndTextOffset = Vector2.Zero;
			Vector2 size = new Vector2(pWidth, pHeight);
			
			_status = NORMAL;
			_onToggle = true;
			_pressed = false;
			_bounds = new Bounds2(Vector2.Zero, size);
			
			if (background == null) {
				background = Support.TiledSpriteFromFile("/Application/assets/images/UI/BetterButton.png", 1, 3);
				background.Scale = size / DefaultDimensions;
			}
			this.AddChild(background);
			
			if (_textFont == null) {
				_textFont = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold");
			}
			var map = Crystallography.UI.FontManager.Instance.GetMap( _textFont );
			
			_buttonText = new Label() {
				Text = "",
				FontMap = map,
				Position = new Vector2(0.0f, 0.5f * (Height - map.CharPixelHeight) )
			};
			this.AddChild(_buttonText);
			
			ScheduleUpdate(0);
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		public bool On(bool? pTurnOn=null) {
			if (pTurnOn != null) {
				_onToggle = (bool)pTurnOn;
				_buttonText.Visible = _onToggle;
				_status = (_onToggle ? NORMAL : DISABLED);
			}
			return  _onToggle;
		}
		
		protected virtual void OnButtonDown() {
			if( !Visible || (_status != NORMAL) ) {
				return;
			}
			_status = PRESSED;
		}
		
		protected virtual void OnButtonUp() {
			EventHandler handler = ButtonUpAction;
			if ( handler != null ) {
				handler( this, null );
			}
		}
		
		
		// DESTRUCTOR -----------------------------------------------------------------------------
#if DEBUG
		~BetterButton() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}

