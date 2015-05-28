using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class ChallengeModeInstructionsPanel : HudPanel
	{
		static readonly string INSTRUCTION_ONE_TEXT = "any piece can be used for any side.\nit just rotates to fit.";
		static readonly string INSTRUCTION_TWO_TEXT = "wildcards flash colors, and always make a match.";
		static readonly string INSTRUCTION_THREE_TEXT = "a failed cube gives you a strike. three strikes and you are out!";
		
		static readonly float CIRCLE_ONE_X = 119.0f;
		static readonly float CIRCLE_ONE_Y = 407.0f;
		static readonly float CIRCLE_DELTA_Y = 134.0f;
		
		SpriteTile Background;
		
		BetterButton _okButton;
		
		SpriteTile circleOneImg;
		SpriteTile circleTwoImg;
		SpriteTile circleThreeImg;
		
		SpriteTile wildcardImg;
		
		SpriteTile cubeOneTop;
		SpriteTile cubeOneRight;
		SpriteTile cubeOneLeft;
		
		SpriteTile heartOne;
		SpriteTile heartTwo;
		SpriteTile heartThree;
		
		SpriteTile plus;
		SpriteTile arrow;
		
		SpriteTile cubeTwoTop;
		SpriteTile cubeTwoRight;
		SpriteTile cubeTwoLeft;
		
		SpriteTile strikeFour;
		SpriteTile heartFive;
		SpriteTile heartSix;
		
		Label instructionOneLabel;
		Label instructionTwoLabel;
		Label instructionThreeLabel;
		
		public ChallengeModeInstructionsPanel () {
			Initialize(960.0f, 544.0f);
		}
		
		protected void Initialize (float pWidth, float pHeight) {
			DismissDelay = 0.0f; // dismiss only with ok button!
			Height = pHeight;
			Width = pWidth;
			var xScale = pWidth/16.0f;
			var yScale = pHeight/16.0f;
			SlideInDirection = SlideDirection.RIGHT;
			SlideOutDirection = SlideDirection.LEFT;
			
			Background = Support.UnicolorSprite("bg", (byte)(LevelManager.Instance.BackgroundColor.R * 255.0f), (byte)(LevelManager.Instance.BackgroundColor.G * 255.0f), (byte)(LevelManager.Instance.BackgroundColor.B * 255.0f), 255);
			Background.Scale = new Vector2(xScale, yScale);
			this.AddChild(Background);
			
			_okButton = new BetterButton(256.0f, 64.0f) {
				Text = "okay",
				Position = Vector2.Zero
			};
			_okButton.background.RegisterPalette(2);
//			this.AddChild(_okButton);
			
			
			// BIG CIRCLE BULLET POINT ICONS
			circleOneImg = Support.SpriteFromAtlas("crystallonUI", "1.png");
			circleOneImg.Position = new Vector2(CIRCLE_ONE_X, CIRCLE_ONE_Y);
			
			circleTwoImg = Support.SpriteFromAtlas("crystallonUI", "2.png");
			circleTwoImg.Position = new Vector2(CIRCLE_ONE_X, CIRCLE_ONE_Y - CIRCLE_DELTA_Y);
			
			circleThreeImg = Support.SpriteFromAtlas("crystallonUI", "3.png");
			circleThreeImg.Position = new Vector2(CIRCLE_ONE_X, CIRCLE_ONE_Y - 2.0f * CIRCLE_DELTA_Y);
			
			// DIAGRAMS
			
			//--------- ORIENTATION DOESN'T MATTER
			cubeOneTop = Support.SpriteFromAtlas("gamePieces", "set1_v0_T.png");
			cubeOneTop.RegisterPalette(0);
			cubeOneTop.Position = new Vector2(344.0f, (float)(544-81));
			cubeOneTop.Scale = new Vector2(0.66f, 0.66f);
			cubeOneRight = Support.SpriteFromAtlas("gamePieces", "set1_v0_R.png");
			cubeOneRight.RegisterPalette(1);
			cubeOneRight.Position = new Vector2(386.0f, (float)(544-130));
			cubeOneRight.Scale = new Vector2(0.66f, 0.66f);
			cubeOneLeft = Support.SpriteFromAtlas("gamePieces", "set1_v0_T.png");
			cubeOneLeft.RegisterPalette(2);
			cubeOneLeft.Position = new Vector2(247.0f, (float)(544-114));
			cubeOneLeft.Scale = new Vector2(0.66f, 0.66f);
			
			
			
			//--------- WILDCARD
			wildcardImg = Support.SpriteFromAtlas("gamePieces", "set1_v0_T.png");
			wildcardImg.RegisterPalette(0);
			wildcardImg.Scale = new Vector2(0.66f, 0.66f);
			wildcardImg.Position = new Vector2(247.0f, (float)(544-240));
			
			Sequence sequence = new Sequence();
			sequence.Add( new CallFunc( () => {
				wildcardImg.ShiftSpriteColor(QColor.palette[1], 0.08f);
			}) );
			sequence.Add( new DelayTime(0.08f) );
			sequence.Add( new CallFunc( () => {
				wildcardImg.ShiftSpriteColor(QColor.palette[2], 0.08f);
			}) );
			sequence.Add( new DelayTime(0.08f) );
			sequence.Add( new CallFunc( () => {
				wildcardImg.ShiftSpriteColor(QColor.palette[0], 0.08f);
			}) );
			sequence.Add( new DelayTime(0.08f) );
			wildcardImg.RunAction( new RepeatForever() { InnerAction=sequence, Tag = 40 } );
			
			
			//---------- STRIKES
			
			heartOne = Support.SpriteFromAtlas("crystallonUI", "heart.png");
			heartOne.RegisterPalette(1);
			heartOne.Position = new Vector2(247.0f, (float)(544-380));
			heartTwo = Support.SpriteFromAtlas("crystallonUI", "heart.png");
			heartTwo.RegisterPalette(1);
			heartTwo.Position = new Vector2(304.0f, (float)(544-380));
			heartThree = Support.SpriteFromAtlas("crystallonUI", "heart.png");
			heartThree.RegisterPalette(1);
			heartThree.Position = new Vector2(362.0f, (float)(544-380));
			strikeFour = Support.SpriteFromAtlas("crystallonUI", "strike.png");
			strikeFour.RegisterPalette(2);
			strikeFour.Position = new Vector2(666.0f, (float)(544-379));
			heartFive = Support.SpriteFromAtlas("crystallonUI", "heart.png");
			heartFive.RegisterPalette(1);
			heartFive.Position = new Vector2(716.0f, (float)(544-380));
			heartSix = Support.SpriteFromAtlas("crystallonUI", "heart.png");
			heartSix.RegisterPalette(1);
			heartSix.Position = new Vector2(774.0f, (float)(544-380));
			
			cubeTwoTop = Support.SpriteFromAtlas("gamePieces", "set1_v0_T.png");
			cubeTwoTop.RegisterPalette(2);
			cubeTwoTop.Position = new Vector2(491.0f, (float)(544-360));
			cubeTwoTop.Scale = new Vector2(0.66f, 0.66f);
			cubeTwoRight = Support.SpriteFromAtlas("gamePieces", "set1_v0_R.png");
			cubeTwoRight.RegisterPalette(1);
			cubeTwoRight.Position = new Vector2(533.0f, (float)(544-409));
			cubeTwoRight.Scale = new Vector2(0.66f, 0.66f);
			cubeTwoLeft = Support.SpriteFromAtlas("gamePieces", "set1_v0_L.png");
			cubeTwoLeft.RegisterPalette(1);
			cubeTwoLeft.Position = new Vector2(491.0f, (float)(544-409));
			cubeTwoLeft.Scale = new Vector2(0.66f, 0.66f);
			
			plus = Support.SpriteFromAtlas("crystallonUI", "plus.png");
			plus.Color = Colors.Black;
			plus.Scale = new Vector2(0.66f, 0.66f);
			plus.Position = new Vector2(432.0f, (float)(544-372));
			
			arrow = Support.SpriteFromAtlas("crystallonUI", "arrow.png");
			arrow.Color = Colors.Black;
			arrow.Scale = new Vector2(0.5f, 0.5f);
			arrow.Position = new Vector2(607.0f, (float)(544-372));
			
//			equationLabel = new Label() {
//				Text = "+      =",
//				Color = Colors.Black,
//				FontMap = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
//				Position = new Vector2(432.0f, (float)(544-372))
//			};
			
			// TEXT LABELS
			instructionOneLabel = new Label() {
				Text = INSTRUCTION_ONE_TEXT,
				Color = Colors.Black,
				FontMap = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
				Position = new Vector2(453.0f, 466.0f)
			};
			
			instructionTwoLabel = new Label() {
				Text = INSTRUCTION_TWO_TEXT,
				Color = Colors.Black,
				FontMap = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
				Position = new Vector2(358.0f, 315.0f)
			};
			
			instructionThreeLabel = new Label() {
				Text = INSTRUCTION_THREE_TEXT,
				Color = Colors.Black,
				FontMap = FontManager.Instance.GetMap( FontManager.Instance.GetInGame("Bariol", 25, "Bold") ),
				Position = new Vector2(249.0f, 98.0f)
			};
			
			this.AddChild(_okButton);
			
			this.AddChild(circleOneImg);
			this.AddChild(circleTwoImg);
			this.AddChild(circleThreeImg);
			
			this.AddChild(cubeOneTop);
			this.AddChild(cubeOneRight);
			this.AddChild(cubeOneLeft);
			this.AddChild(wildcardImg);
			this.AddChild(heartOne);
			this.AddChild(heartTwo);
			this.AddChild(heartThree);
			this.AddChild(strikeFour);
			this.AddChild(heartFive);
			this.AddChild(heartSix);
			this.AddChild(cubeTwoTop);
			this.AddChild(cubeTwoRight);
			this.AddChild(cubeTwoLeft);
//			this.AddChild(equationLabel);
			this.AddChild(plus);
			this.AddChild(arrow);
			
			this.AddChild(instructionOneLabel);
			this.AddChild(instructionTwoLabel);
			this.AddChild(instructionThreeLabel);
		}
		
		void Handle_okButtonButtonUpAction (object sender, EventArgs e)
		{
			this.SlideOut();
		}
		
		
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			
			_okButton.ButtonUpAction += Handle_okButtonButtonUpAction;
		}

		
		
		public override void OnExit()
		{
			_okButton.ButtonUpAction -= Handle_okButtonButtonUpAction;
			
			_okButton = null;
			circleOneImg = null;
			circleTwoImg = null;
			circleThreeImg = null;
			wildcardImg = null;
			cubeOneTop = null;
			cubeOneRight = null;
			cubeOneLeft = null;
			heartOne = null;
			heartTwo = null;
			heartThree = null;
			strikeFour = null;
			heartFive = null;
			heartSix = null;
			plus = null;
			arrow = null;
			cubeTwoTop = null;
			cubeTwoRight = null;
			cubeTwoLeft = null;
			instructionOneLabel = null;
			instructionTwoLabel = null;
			instructionThreeLabel = null;
			
			RemoveAllChildren(true);
		}
	}
}

