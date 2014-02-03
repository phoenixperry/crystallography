using System;
using Sce.PlayStation.Core;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography
{
	public class QColor : AbstractQuality
	{
		protected static AbstractQuality _instance;
		
		// GET & SET --------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QColor Instance {
			get {
				if(_instance == null) {
					_instance = new QColor();
					return _instance as QColor;
				} else { 
					return _instance as QColor; 
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		public static Vector4[] palette = new Vector4[3];
//		public static Vector4[] uiPalette = new Vector4[3];
		
		public static List<Node>[] registry = new List<Node>[3];
		
		// CONSTRUCTOR ------------------------------------------------------------------
		
		protected QColor() : base()
		{
			_name = "QColor";
			setPalette();
		}
		
		// OVERRIDES --------------------------------------------------------------------
		
		public override void Apply ( ICrystallonEntity pEntity, int pVariant ) {
			if ( pEntity is SpriteTileCrystallonEntity ) {
				(pEntity.getNode() as SpriteBase).Color = palette[pVariant];
				( pEntity as SpriteTileCrystallonEntity ).setColor(pVariant);
			}
		}
		
		// METHODS ----------------------------------------------------------------------
		
		public void ApplyUI ( Node pNode, int pVariant ) {
			if (pNode is SpriteBase) {
				(pNode as SpriteBase).Color = palette[pVariant];
			} else {
				(pNode as Label).Color = palette[pVariant];
			}
		}
		
		public void setPalette () {
			setPalette( LevelManager.Instance.Palette[0], LevelManager.Instance.Palette[1], LevelManager.Instance.Palette[2] );
		}
		
		public void setPalette( Vector4 pColor1, Vector4 pColor2, Vector4 pColor3 ) {
			palette[0] = pColor1;
			palette[1] = pColor2;
			palette[2] = pColor3;
		}
		
		public void rotatePalette() {
			Vector4 temp = palette[0];
			for (var i = 0; i < palette.Length-1; i++) {
				palette[i] = palette[i+1];
			}
			palette[palette.Length-1] = temp;
		}
		
		public void ShiftColors( int pShifts, float pShiftTime, float pRestTime=1.0f) {
			Director.Instance.CurrentScene.StopActionByTag(30);
			var cRot = new Sequence();
//			cRot.Add( new CallFunc( () => {
//				rotatePalette();
//			} ));
			cRot.Add( new CallFunc( () => {
				for (int i=0; i < registry.Length; i++) {
					var list = registry[i];
					if ( list != null ) {
						foreach ( Node node in list ) {
							if ( node is SpriteBase) {
								(node as SpriteBase).ShiftSpriteColor(QColor.palette[i], pShiftTime);
							} else {
								(node as Label).ShiftLabelColor(QColor.palette[i], pShiftTime);
							}
						}
					}
				}
			} ));
			cRot.Add( new DelayTime(pShiftTime + pRestTime) );
			if (pShifts > 1) {
				Director.Instance.CurrentScene.RunAction(new Repeat(cRot, pShifts) {Tag = 30});
			} else if (pShifts == 1) {
				Director.Instance.CurrentScene.RunAction(cRot);
			} else {
				Director.Instance.CurrentScene.RunAction(new RepeatForever() {Tag = 30, InnerAction = cRot});
			}
		}
		
		public void StopShiftColors() {
			Director.Instance.CurrentScene.StopActionByTag(30);
		}
	}
}
