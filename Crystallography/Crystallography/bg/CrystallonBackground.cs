using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.BG
{
	public class CrystallonBackground : Layer
	{
		protected int _screenWidth;
		protected int _screenHeight;
		
		protected readonly Vector2 RED_BASE;
		protected readonly Vector2 RED_RANGE;
		protected readonly Vector2 BLUE_BASE;
		protected readonly Vector2 BLUE_RANGE;
		
		public Node n1;
		public Node n2;
		
		public CrystallonBackground () : base ()
		{
			_screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			
			RED_BASE = new Vector2(0.5f*_screenWidth, 0.32f*_screenHeight);
			RED_RANGE = new Vector2(0.0f, 0.13f*_screenHeight);
			BLUE_BASE = new Vector2(0.5f*_screenWidth, 0.45f*_screenHeight);
			BLUE_RANGE = new Vector2(0.0f, 0.1f*_screenHeight);
			
			n1 = new Node();
			this.AddChild(n1);
			n1.Position = new Vector2( 0.5f*_screenWidth, 0.35f*_screenHeight);
			n2 = new Node();
			this.AddChild(n2);
			n2.Position = new Vector2( 0.5f*_screenWidth, 0.5f*_screenHeight);
			
			var s = new SpriteUV();
			s.TextureInfo = new TextureInfo("/Application/assets/images/bg/1/leftSolidWhite.png");
			s.Color = Colors.Red;
			s.Scale = s.CalcSizeInPixels();
			s.Pivot = new Vector2(1.0f, 0.5f);
//			s.Position = new Vector2( 0.5f*_screenWidth, 0.35f*_screenHeight);
			n1.AddChild(s);
			
			s = new SpriteUV();
			s.TextureInfo = new TextureInfo("/Application/assets/images/bg/1/rightSolidWhite.png");
			s.Color = Colors.Red;
			s.Scale = s.CalcSizeInPixels();
			s.Pivot = new Vector2(0.0f, 0.5f);
//			s.Position = new Vector2( 0.5f*_screenWidth, 0.35f*_screenHeight);
			n1.AddChild(s);
			
			s = new SpriteUV();
			s.TextureInfo = new TextureInfo("/Application/assets/images/bg/1/topSolidChevron.png");
			s.Color = Colors.Red;
			s.Scale = s.CalcSizeInPixels();
			s.Pivot = new Vector2(0.5f, 0.04f);
//			s.Position = new Vector2( 0.5f*_screenWidth, 0.35f*_screenHeight);
			n1.AddChild(s);
			
			s = new SpriteUV();
			s.TextureInfo = new TextureInfo("/Application/assets/images/bg/1/leftHollowWhite.png");
			s.Color = Colors.Cyan;
			s.Scale = s.CalcSizeInPixels();
			s.Pivot = new Vector2(1.0f, 0.5f);
//			s.Position = new Vector2( 0.5f*_screenWidth, 0.5f*_screenHeight);
			n2.AddChild(s);
			
			s = new SpriteUV();
			s.TextureInfo = new TextureInfo("/Application/assets/images/bg/1/rightHollowWhite.png");
			s.Color = Colors.Cyan;
			s.Scale = s.CalcSizeInPixels();
			s.Pivot = new Vector2(0.0f, 0.5f);
//			s.Position = new Vector2( 0.5f*_screenWidth, 0.5f*_screenHeight);
			n2.AddChild(s);
			
			s = new SpriteUV();
			s.TextureInfo = new TextureInfo("/Application/assets/images/bg/1/topSolidChevron.png");
			s.Color = Colors.White;
			s.Scale = s.CalcSizeInPixels();
			s.Pivot = new Vector2(0.5f, 0.04f);
//			s.Position = new Vector2( 0.5f*_screenWidth, 0.5f*_screenHeight);
			n2.AddChild(s);
			
			Start();
		}
		
		// OVERRIDES ---------------------------------------------------------------------------
		
		public override void Update (float dt)
		{
			base.Update (dt);
		}
		
		
		// METHODS -----------------------------------------------------------------------------
		
		public void Start() {
			
			Sequence sequence = new Sequence();
			sequence.Add( new MoveTo( RED_BASE + GameScene.Random.NextFloat() * RED_RANGE, 10.0f + 10.0f * GameScene.Random.NextFloat() ) );
			sequence.Add( new CallFunc( () => {OnMoveComplete( n1 );} ) );
			n1.RunAction( sequence );
			
			sequence = new Sequence();
			sequence.Add( new MoveTo( BLUE_BASE + GameScene.Random.NextFloat() * BLUE_RANGE, 10.0f + 10.0f * GameScene.Random.NextFloat() ) );
			sequence.Add( new CallFunc( () => {OnMoveComplete( n2 );} ) );
			n2.RunAction( sequence );
			

		}
		
		public void OnMoveComplete( Node pNode ) {
			Sequence sequence = new Sequence();
			sequence.Add ( new DelayTime( GameScene.Random.NextFloat() * 1.0f ) );
			if (pNode == n1) {
				sequence.Add( new MoveTo( RED_BASE + GameScene.Random.NextFloat() * RED_RANGE, 10.0f + 10.0f * GameScene.Random.NextFloat() ) );
			} else {
				sequence.Add( new MoveTo( BLUE_BASE + GameScene.Random.NextFloat() * BLUE_RANGE, 10.0f + 10.0f * GameScene.Random.NextFloat() ) );
			}
			sequence.Add( new CallFunc( () => {OnMoveComplete( pNode );} ) );
			pNode.RunAction( sequence );
		}
	}
}

