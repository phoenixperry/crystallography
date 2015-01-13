using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography.UI
{
	public class StrikeHud : Node
	{
		private static readonly int DEFAULT_STRIKE_COUNT = 3;
		private static readonly int MIN_STRIKE_COUNT = 1;
		private static readonly int MAX_STRIKE_COUNT = 9;
		
		public int strikeCount;
		
		private List <Strike> strikeList;
		
		
		public event EventHandler StrikeBarSuccess;
		public event EventHandler StrikeBarFailure;
		
		
		public StrikeHud () : base() {
			Initialize();
		}
		
		private void Initialize() {
			strikeList = new List<Strike>();
			for(int i=0; i<DEFAULT_STRIKE_COUNT; i++)
			{
				Add();
			}
			Reset();
		}
		
		public void Add() {
			if(strikeCount >= MAX_STRIKE_COUNT) return;
			
			strikeCount++;
			
			if(strikeList.Count < strikeCount) {
				strikeList.Add(new Strike() {
					Position = new Vector2(35.0f * strikeList.Count, 0.0f)
				});
				this.AddChild(strikeList[strikeCount-1]);
			} else {
				(strikeList[strikeCount-1]).Visible = true;
			}
		}
		
		public void Remove() {
			if(strikeCount <= MIN_STRIKE_COUNT) return;
			
			strikeCount--;
			(strikeList[strikeCount]).Visible = false;
			(strikeList[strikeCount]).filled = false;
		}
		
		public void Despair() {
//			for(int i=strikeCount-1; i>=0; i--) {
			for(int i=0; i<strikeCount; i++) {
				Strike strike = strikeList[i];
				if(strike.filled == false || strike.isGood == true) { 
					strike.fill(false);
					Shake(strike);
					if(i == strikeCount-1) {
						EventHandler handler = StrikeBarFailure;
						if (handler != null) {
							handler( this, null );
						}
					}
					break;
				}
			}
		}
		
		public void Hope() {
			for(int i=strikeCount-1; i>=0; i--) {
				Strike strike = strikeList[i];
				if(strike.filled) {
					strike.Reset();
					Shake(strike);
					break;
				}
//				if(strike.filled == false || strike.isGood == false) { 
//					strike.fill(true);
//					Shake (strike);
//					if(i == strikeCount-1) {
//						EventHandler handler = StrikeBarSuccess;
//						if (handler != null) {
//							handler( this, null );
//						}
//					}
//					break;
//				}
			}
		}
		
		private void Shake(Strike strike) {
			var origin = strike.Position;
			strike.StopAllActions();
			Sequence s = new Sequence();
			s.Add( new MoveBy(new Vector2(2.0f, 0.0f), 0.005f) {
				Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
			});
			s.Add( new MoveBy(new Vector2(-4.0f, 0.0f), 0.01f){
				Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
			});
			s.Add( new MoveTo(origin, 0.005f){
				Tween = Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Linear
			});
			strike.RunAction(new Repeat(s, 6));
		}
		
		public void Reset() {
			strikeCount = DEFAULT_STRIKE_COUNT;
			
			for(int i=0; i<strikeList.Count; i++) {
				Strike strike = strikeList[i];
				strike.filled = false;
				if(i > strikeCount) {
					strike.Visible = false;
				}
			}
		}
	}
}

