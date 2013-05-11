using System;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class CardManager
	{
		public static int MAX_CARD_POPULATION = 15;
		
		public static List<CardCrystallonEntity> availableCards;
		protected static CardManager _instance;
		protected Scene _scene;
		protected GamePhysics _physics;
		
		public event EventHandler NoMatchesPossibleDetected;
		
		// GET & SET -----------------------------------------------------------------------

		public static CardManager Instance { 
			get {
				if( _instance == null) {
					return _instance = new CardManager();
				}
				return _instance;
			}
			private set{
				_instance = value;
			}
		}
		
		public int MaxPopulation { get; set; }
		
		public int TotalCardsInDeck { get; set; }
		
		public int NextId { get; private set; }
		
		public bool PickRandomly {get; set; }
		
		protected List<int> ids;
		
		// CONSTRUCTOR ---------------------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.CardManager"/> class.
		/// </summary>
		protected CardManager () {
			MaxPopulation = 30;
			NextId = 0;
			availableCards = new List<CardCrystallonEntity>();
			_scene = Director.Instance.CurrentScene;
			_physics = GamePhysics.Instance;
			SelectionGroup.Instance.CubeCompleteDetected += HandleSelectionGroupInstanceCubeCompleteDetected;
			Reset (_scene);
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// EVENT HANDLERS ------------------------------------------------------------------
		
		/// <summary>
		/// Make the cards in the cube unavailable. Repopulate if possible. If no possible matches remain, allow level transition.
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// E.
		/// </param>
		void HandleSelectionGroupInstanceCubeCompleteDetected (object sender, CubeCompleteEventArgs e)
		{
			MakeUnavailable( e.members );
			Populate();
			if ( MatchesPossible() == false ) {
				EventHandler handler = NoMatchesPossibleDetected;
				if (handler != null) {
					handler( this, null );
				}
			}
		}
		
		// METHODS -------------------------------------------------------------------------
		
		/// <summary>
		/// Add the specified <c>CardCrystallonEntity</c> to <c>availableCards</c>.
		/// </summary>
		/// <see cref="Crystallography.CardCrystallonEntity"/>
		/// P card.
		/// </param>
		public CardCrystallonEntity Add( CardCrystallonEntity pCard ) {
			if (availableCards.Contains(pCard) == false) {
				availableCards.Add(pCard);
			}
			return pCard;
			
		}
		
		public void Destroy() {
			availableCards.Clear ();
			availableCards = null;
			_instance = null;
			_scene = null;
			_physics = null;
		}
		
		public CardCrystallonEntity getCardById( int pId ) {
			foreach ( CardCrystallonEntity c in availableCards ) {
				if ( c.id == pId ) {
					return c;
				}
			}
			return null;
		}
		
		/// <summary>
		/// Removes a <c>CardCrystallonEntity</c> from the scene, with the option to delete entirely.
		/// </summary>
		/// <param name='pCardEntities'>
		/// An array of <c>CardCrystallonEntites</c>
		/// </param>
		public void Remove( CardCrystallonEntity pCardEntity, bool pDelete=false ) {
			if ( pDelete ) {
				rm(pCardEntity);
			}
			pCardEntity.removeFromScene( pDelete );
		}
		
		public void MakeUnavailable( CardCrystallonEntity[] pCardEntities ) {
			foreach ( CardCrystallonEntity e in pCardEntities ) {
				rm ( e );
			}
		}
		
		/// <summary>
		/// Returns whether or not at least one possible match remains, based on the contents of <c>availableCards</c>
		/// </summary>
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
		
		/// <summary>
		/// Spawn cards until we run out of cards to spawn, or hit the population cap.
		/// </summary>
		public void Populate () {
			if (GameScene.currentLevel == 999) {
				ids = new List<int>();
				for (int i = 0; i < TotalCardsInDeck; i++) {
					ids.Add(i+NextId);
				}
				while ( availableCards.Count <= MAX_CARD_POPULATION && TotalCardsInDeck > 0) {
					int index = (int)System.Math.Floor(GameScene.Random.NextFloat() * TotalCardsInDeck);
					spawn(ids[index]);
					ids.RemoveAt(index);
				}
			} else {
				ids = new List<int>();
				for (int i = 0; i < TotalCardsInDeck; i++) {
					ids.Add(i+NextId);
				}
				while ( availableCards.Count <= MAX_CARD_POPULATION && TotalCardsInDeck > 0) {
					int index = (int)System.Math.Floor(GameScene.Random.NextFloat() * TotalCardsInDeck);
					spawn(ids[index]);
					ids.RemoveAt(index);
				}
//				while ( availableCards.Count < MaxPopulation && TotalCardsInDeck > 0) {
//					spawn();
//				}
			}
		}
		
		/// <summary>
		/// Reset the <c>CardManager</c>. Probably want to call this before starting a new level.
		/// </summary>
		public void Reset ( Scene pScene ) {
			foreach( var card in availableCards ) {
				card.removeFromScene( true );
			}
			availableCards.Clear();
			_scene = pScene;
			ids = null;
		}
		
		/// <summary>
		/// Remove a <c>CardCrystallonEntity</c> from <c>availableCards</c>.
		/// </summary>
		/// <param name='pCard'>
		/// <see cref="Crystallography.CardCrystallonEntity"/>
		/// </param>
		private void rm( CardCrystallonEntity pCard ) {
			availableCards.Remove(pCard);
		}
		
		/// <summary>
		/// Creates a new <c>CardCrystallonEntity</c>, adds it to the current scene at a random location.
		/// </summary>
		public CardCrystallonEntity spawn( int pId = -1) {
			var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			return spawn( 50f + 0.75f * _screenWidth * GameScene.Random.NextFloat(),
			       50f + 0.75f * _screenHeight * GameScene.Random.NextFloat(), pId);
		}
		
		/// <summary>
		/// Creates a new <c>CardCrystallonEntity</c>, adds it to the current scene, and sets its starting position.
		/// </summary>
		/// <param name='pX'>
		/// <c>float</c> X coordinate in pixels.
		/// </param>
		/// <param name='pY'>
		/// <c>float</c> Y coordinate in pixels.
		/// </param>
		public CardCrystallonEntity spawn( float pX, float pY, int pId=-1 ) {
//			var ss = SpriteSingleton.getInstance();
			
			if (pId == -1){
				pId = NextId;
			}
			CardCrystallonEntity card = new CardCrystallonEntity(_scene, _physics, pId, 
			                                QPattern.Instance.patternTiles.TextureInfo, QPattern.Instance.patternTiles.TileIndex2D, 
			                                _physics.SceneShapes[0]);
			
			NextId++;
			TotalCardsInDeck--;
			QualityManager.Instance.ApplyQualitiesToEntity( card );
			card.setPosition( pX, pY );
			card.addToScene();
			availableCards.Add(card);
			return card;
		}
		
		// DESTRUCTOR ----------------------------------------------------------------------------------------
#if DEBUG
		~CardManager() {
			Console.WriteLine(GetType().ToString() + " deleted" );
		}
#endif
	}
}

