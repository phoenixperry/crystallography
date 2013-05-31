using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class LevelTitle : Layer
	{
		Label NextLevelText;
        Label LevelNumberText;
		FontMap map;
		List<Label> QualityNames;
		
		protected GameScene _scene;
		protected bool _initialized;
		protected bool _entering;
		protected bool _exiting;
		
		// CONSTRUCTORS -------------------------------------------------------------------------
		
		public LevelTitle (GameScene scene) {
			_scene = scene;
			
			
			if (_initialized == false) {
				Initialize();
			}
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// OVERRIDES ----------------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			base.Update (dt);
			
			if(_entering) {
				var x = this.Position.X;
				x -= dt * 1000.0f;
				if (x < 100.0f) {
					x = 100.0f;
					_entering = false;
					Sequence sequence = new Sequence();
					sequence.Add( new DelayTime( 2.0f ) );
					sequence.Add( new CallFunc( () => ExitAnim() ) );
					this.RunAction(sequence);
				}
				this.Position = new Vector2(x, 272.0f);
			}
			
			if( _exiting ) {
				var x = this.Position.X;
				x -= dt * 1000.0f;
				if (x < -100.0f) {
					x = -100.0f;
					_exiting = false;
					Hide();
				}
				this.Position = new Vector2(x, 272.0f);
			}
		}
		
		// METHODS ------------------------------------------------------------------------------
		
		protected void Initialize() {
			_initialized = true;
			
			QualityNames = new List<Label>();
			map = Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 20, "Bold"));
			LevelNumberText = new Label( "0", Crystallography.UI.FontManager.Instance.GetMap(Crystallography.UI.FontManager.Instance.GetInGame("Bariol", 72, "Bold")) );
			
			this.Position = new Vector2( 970.0f, 272.0f );
			this.AddChild(LevelNumberText);
			
			NextLevelText = new Label("next level", map);
			NextLevelText.Position = new Vector2( 0.0f, 75.0f);
			this.AddChild(NextLevelText);
		}
		
		public void SetLevelText( int pNumber ) {
			LevelNumberText.Text = pNumber.ToString();
		}
		
		public void SetQualityNames( string[] pNames ) {
			foreach ( Label l in QualityNames) {
				this.RemoveChild(l, true);
			}
			QualityNames.Clear();
			Label n;
			foreach ( string name in pNames ) {
				QualityNames.Add( n = new Label() );
				n.Color = Colors.White;
				n.FontMap = map;
				n.Text = name;
				n.Position = new Vector2( (QualityNames.Count-1)*80.0f, -25.0f);
				this.AddChild(n);
			}
		}
		
		public void Hide() {
			this.Visible = false;
			_entering = false;
			_exiting = false;
		}
		
		public void Show() {
			this.Visible = true;
		}
		
		public void EnterAnim() {
			_entering = true;
			this.Position = new Vector2(970.0f, 272.0f);
			Show();
		}
		
		public void ExitAnim() {
			_exiting = true;
			Show();
		}
		
		// DESTRUCTOR -------------------------------------------------------------------------------------
#if DEBUG
        ~LevelTitle() {
			Console.WriteLine("LevelTitle deleted.");
        }
#endif
	}
}

