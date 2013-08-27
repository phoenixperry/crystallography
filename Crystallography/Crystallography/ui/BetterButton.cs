using System;
using Sce.PlayStation.Core;
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
		public Vector2 TextOffset;
		
		protected SpriteTile _background;
		protected Bounds2 _bounds;
		protected Label _buttonText;
		protected int _status;
		protected bool _onToggle;
		protected bool _pressed;
//		protected bool _initialized;
		protected float halfHeight;
		protected float halfWidth;
		
		public event EventHandler ButtonUpAction;
		
		
		// GET & SET --------------------------------------------------------------------
		
		public Vector4 Color {
			get { return _background.Color; }
			set { _background.Color = value; }
		}
		
		public Vector4 TextColor{
			get { return _buttonText.Color; }
			set { _buttonText.Color = value; }
		}
		
		public bool On { 
			get { return _onToggle; }
			set { 
				_onToggle = value;
				_buttonText.Visible = _onToggle;
				_status = (_onToggle ? NORMAL : DISABLED);
			}
		}
		
		public int Status { get {return _status;} }
		
		public string Text {
			get {
				return _buttonText.Text;
			}
			set {
				_buttonText.Text = value;
				CenterText();
			}
		}
		
		
		// CONSTRUCTORS ------------------------------------------------------------------
		
		public BetterButton() {}
		
		public BetterButton (string pPath) : base() {
			_background = Support.TiledSpriteFromFile(pPath, 1, 3);
			var size = _background.CalcSizeInPixels();
			Initialize(size.X, size.Y);
		}
		
		public BetterButton (float pWidth, float pHeight) : base() {
			Initialize(pWidth, pHeight);
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
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
			base.OnExit ();
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
			_background.TileIndex1D = _status;
			
			switch( _background.TileIndex1D ) {
			case BetterButton.PRESSED:
				_buttonText.Color.A = 0.7f;
				break;
			case BetterButton.DISABLED:
				_buttonText.Color.A = 0.5f;
				break;
			case BetterButton.NORMAL:
			default:
				_buttonText.Color.A = 1.0f;
				break;
			}
		}
		
		
		// METHODS -----------------------------------------------------------------------
		
		protected void CenterText() {
			var textWidth = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Regular").GetTextWidth(Text);
			_buttonText.Position = new Vector2(0.5f * (Width - textWidth), _buttonText.Position.Y) - TextOffset;
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
			TextOffset = Vector2.Zero;
			Vector2 size = new Vector2(pWidth, pHeight);
			
			_status = NORMAL;
			_onToggle = true;
			_pressed = false;
			_bounds = new Bounds2(Vector2.Zero, size);
			
			if (_background == null) {
				_background = Support.TiledSpriteFromFile("/Application/assets/images/UI/BetterButton.png", 1, 3);
				_background.Scale = size / DefaultDimensions;
			}
			this.AddChild(_background);
			
			var font = Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 36, "Bold");
			var map = Crystallography.UI.FontManager.Instance.GetMap( font );
			
			_buttonText = new Label() {
				Text = "",
				FontMap = map,
				Position = new Vector2(0.0f, 0.5f * (Height - 36) )
			};
			this.AddChild(_buttonText);
			
			ScheduleUpdate(0);
		}
		
		protected virtual void OnButtonDown() {
			if( !Visible || (_status != NORMAL) ) {
				return;
			}
			_status = PRESSED;
		}
		
		protected virtual void OnButtonUp() {
			if( !Visible || (_status != PRESSED) ) {
				return;
			}
			_status = NORMAL;
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

