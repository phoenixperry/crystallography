using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class QCountdown : AbstractQuality
	{
		public static readonly Font font = new Font("Application/assets/fonts/Bariol_Regular.otf", 25, FontStyle.Regular);
		public static readonly FontMap map = new FontMap(font);
		protected static AbstractQuality _instance;
		
		// GET & SET -----------------------------------------------------------------------
		
		/// <summary>
		/// An instance of the class. Creates one if it doesn't already exist.
		/// </summary>
		public static QCountdown Instance {
			get {
				if(_instance == null) {
					_instance = new QCountdown();
					return _instance as QCountdown;
				} else { 
					return _instance as QCountdown;
				}
			}
			protected set {
				_instance = value;
			}
		}
		
		// CONSTRUCTOR --------------------------------------------------------------------
		
		public QCountdown () : base () {
			_name = "QCountdown";
		}
		
		// OVERRIDES ----------------------------------------------------------------------
		
		override public void Apply ( ICrystallonEntity pEntity, int pVariant ) {
//			CardCrystallonEntity e = pEntity as CardCrystallonEntity;
//			e.setCountdown( pVariant );
		}
		
		// METHODS ------------------------------------------------------------------------
		
		public void Tick ( ICrystallonEntity pEntity ) {
//			CardCrystallonEntity e = pEntity as CardCrystallonEntity;
//			e.advanceCountdown();
			
//			e.countdown--;
//			if ( e.countdown < 0 ) {
//				// Fire some sort of event and reset the countdown
//				e.resetCountdown();
//			}
		}
	}
}

