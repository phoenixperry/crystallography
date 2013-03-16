using System;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class CardManager
	{
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
			while ( availableCards.Count < MaxPopulation && TotalCardsInDeck > 0) {
				spawn();
			}
		}
		
		/// <summary>
		/// Reset the <c>CardManager</c>. Probably want to call this before starting a new level.
		/// </summary>
		public void Reset ( Scene pScene ) {
			foreach( var card in availableCards ) {
				card.removeFromScene();
			}
			availableCards.Clear();
			_scene = pScene;
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
		public CardCrystallonEntity spawn() {
			var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			return spawn( 50f + 0.75f * _screenWidth * GameScene.Random.NextFloat(),
			       50f + 0.75f * _screenHeight * GameScene.Random.NextFloat());
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
		public CardCrystallonEntity spawn( float pX, float pY ) {
			var ss = SpriteSingleton.getInstance();
			CardCrystallonEntity card = new CardCrystallonEntity(_scene, _physics, NextId, ss.Get("TopSolid").TextureInfo, ss.Get ("TopSolid").TileIndex2D, 
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
		
		~CardManager() {
			availableCards.Clear ();
			availableCards = null;
			Instance = null;
			_scene = null;
			_physics = null;
		}
	}
}

