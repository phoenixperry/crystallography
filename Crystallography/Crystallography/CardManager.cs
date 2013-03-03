using System;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class CardManager
	{
		public static List<CardCrystallonEntity> availableCards;
		protected Scene _scene;
		protected GamePhysics _physics;
		
		// GET & SET -----------------------------------------------------------------------
		
		public static CardManager Instance { get; private set; }
		
		// CONSTRUCTOR ---------------------------------------------------------------------
		
		public CardManager ( Scene pScene, GamePhysics pPhysics )
		{
			if(Instance == null) {
				Instance = this;
				availableCards = new List<CardCrystallonEntity>();
				_scene = pScene;
				_physics = pPhysics;
			}
		}
		
		// METHODS -------------------------------------------------------------------------
		
		private CardCrystallonEntity add( CardCrystallonEntity pCard ) {
			availableCards.Add(pCard);
			return pCard;
		}
		
		public void matched( CardCrystallonEntity[] pCardEntities ) {
			foreach ( CardCrystallonEntity e in pCardEntities ) {
				rm(e);
			}
		}
		
		public bool MatchesPossible() {
			int len = availableCards.Count;
			if ( len >= 3 ) {	// ---------------------------------------------------------------- At least 3 cards must remain
				CardCrystallonEntity[] triad = new CardCrystallonEntity[SelectionGroup.MAX_CAPACITY];
				for (int i=0; i < len-2; i++) {
					for (int j=i+1; j < len-1; j++) {
						for (int k=j+1; k < len; k++) {
							triad[0] = availableCards[i];
							triad[1] = availableCards[j];
							triad[2] = availableCards[k];
							if ( QualityManager.Instance.EvaluateMatch( triad ) ) { // ------------- At least 1 possible match exists
								Console.WriteLine("Possible Sets Remain: TRUE");
								return true;
							}
						}
					}
				}
			}
			Console.WriteLine("Possible Sets Remain: FALSE");
			return false;
		}
		
		public void Reset () {
			foreach( var card in availableCards ) {
				card.removeFromScene();
			}
			availableCards.Clear();
		}
		
		private void rm( CardCrystallonEntity pCard ) {
			availableCards.Remove(pCard);
		}
		
		// Spawn a card at a random location
		public CardCrystallonEntity spawn() {
			var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			return spawn( 50f + 0.75f * _screenWidth * GameScene.Random.NextFloat(),
			       50f + 0.75f * _screenHeight * GameScene.Random.NextFloat());
		}
		
		// Spawn a card at a specific location
		public CardCrystallonEntity spawn( float pX, float pY ) {
			var ss = SpriteSingleton.getInstance();
			CardCrystallonEntity card = new CardCrystallonEntity(_scene, _physics, ss.Get("TopSolid").TextureInfo, ss.Get ("TopSolid").TileIndex2D, 
			                                _physics.SceneShapes[0]); //, currentLevelData[0]);
			card.setPosition( pX, pY );
			card.addToScene();
			availableCards.Add(card);
			return card;
		}
		
		// DESTRUCTOR ----------------------------------------------------------------------------------------
		
		~CardManager() {
			availableCards.Clear ();
			availableCards = null;
			Instance = null;
			_scene = null;
			_physics = null;
		}
	}
}

