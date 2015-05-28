using System;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class CardManager
	{
		public static readonly int MAX_WILDCARDS = 3;
		public static readonly int WILDCARD_BASE_GROUP = 60;
		public static readonly float WILDCARD_BASE_CHANCE = 0.05f;
		
		public static int DEFAULT_STD_CARD_POPULATION = 15;
		public static int DEFAULT_MAX_CARD_POPULATION = 18;
		
		protected static CardManager _instance;
		
		/// <summary>
		/// List of cards that are on screen and can be used in matches.
		/// </summary>
		public static List<CardCrystallonEntity> availableCards;
		public static List<CardCrystallonEntity> availableWildCards;
		
		protected Scene _scene;
		protected GamePhysics _physics;
		
		/// <summary>
		/// A list of all the card IDs that have not been put on screen yet. This list is modified every time cards are spawned onto the screen.
		/// </summary>
		protected List<int> DeckOfIDs;
		protected List<int> WildcardIDs;
		protected int WildcardGroupCounter;
		
		public event EventHandler NoMatchesPossibleDetected;
		public event EventHandler CardSpawned;
		
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
		
		public int TotalCardsInDeck { get; set; }
		
		public int NextId { get; private set; }
		
		public bool PickRandomly {get; set; }
		
		// CONSTRUCTOR ---------------------------------------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.CardManager"/> class.
		/// </summary>
		protected CardManager () {
			NextId = 0;
			availableCards = new List<CardCrystallonEntity>();
			availableWildCards = new List<CardCrystallonEntity>();
			_scene = Director.Instance.CurrentScene;
			_physics = GamePhysics.Instance;
			CubeCrystallonEntity.CubeCompleteDetected += HandleSelectionGroupInstanceCubeCompleteDetected;
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
		
		public void AddQuality( string pQualityString ) {
//			if ( false == QualityManager.Instance.scoringQualityList.Contains(pQualityString) ) {
//				QualityManager.Instance.scoringQualityList.Add(pQualityString);
//			}
//			if( pQualityString == "QSound") {
//				LevelManager.Instance.SoundGlow = true;
//			}
			QualityManager.Instance.AddScoringQuality( pQualityString );
			foreach ( CardCrystallonEntity card in availableCards) {
				if (card is WildCardCrystallonEntity) continue;
				if (card.Scored) continue;
				QualityManager.Instance.SetQuality(card, pQualityString, (int)System.Math.Floor(GameScene.Random.NextFloat() * 3.0f) );
			}
		}
		
		public void RemoveQuality(string pQualityString) {
//			QualityManager.Instance.scoringQualityList.Remove(pQualityString);
//			if( pQualityString == "QSound") {
//				LevelManager.Instance.SoundGlow = false;
//			}
			QualityManager.Instance.RemoveScoringQuality( pQualityString );
			foreach ( CardCrystallonEntity card in availableCards) {
				QualityManager.Instance.SetQuality(card, pQualityString, 0 );
			}
		}
		
		/// <summary>
		/// Builds a particular card.
		/// </summary>
		/// <returns>
		/// The card.
		/// </returns>
		/// <param name='pId'>
		/// P identifier.
		/// </param>
		protected CardCrystallonEntity BuildCard(int pId) {
			CardCrystallonEntity card;
			if (   GameScene.currentLevel == 999
			    && availableWildCards.Count < MAX_WILDCARDS
//			    && GameScene.Random.NextFloat() < WILDCARD_BASE_CHANCE) { // ------------------------------------------ POSSIBLE WILDCARD
			    && WildcardIDs.Contains(pId) ) {
				
				card = new WildCardCrystallonEntity(_scene, _physics, pId, _physics.SceneShapes[0]);
				availableWildCards.Add(card);
			} else { // ----------------------------------------------------------------------------------------------- NORMAL CARD
				card = new CardCrystallonEntity(_scene, _physics, pId, _physics.SceneShapes[0]);
			}
			availableCards.Add(card);
			return card;
			
		}
		
		/// <summary>
		/// Constructs a deck and arranges it in ID order, that is the order the cards are listed in
		/// in our levels spread sheet.
		/// </summary>
		protected void BuildDeckOfIDs() {
			DeckOfIDs = new List<int>();
			for (int i = 0; i < TotalCardsInDeck; i++) {
				DeckOfIDs.Add(i+NextId);
			}
			DeckOfIDs.Sort();
		}
		
		public void Destroy() {
			availableCards.Clear ();
			availableCards = null;
			availableWildCards.Clear ();
			availableWildCards = null;
			DeckOfIDs.Clear();
			DeckOfIDs = null;
			_instance = null;
			_scene = null;
			_physics = null;
		}
		
		/// <summary>
		/// Gets a card with a specific ID number.
		/// </summary>
		/// <returns>
		/// The card.
		/// </returns>
		/// <param name='pId'>
		/// P identifier.
		/// </param>
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
		
		/// <summary>
		/// Removes a specific card from the list of available cards.
		/// </summary>
		/// <param name='pCardEntities'>
		/// P card entities.
		/// </param>
		public void MakeUnavailable( CardCrystallonEntity[] pCardEntities ) {
			foreach ( CardCrystallonEntity e in pCardEntities ) {
				rm ( e );
				// IN INFINITE MODE, RECYCLE CARD IDS
				if ( GameScene.currentLevel == 999 ) {
					DeckOfIDs.Add(e.id);
					TotalCardsInDeck++;
				}
			}
		}
		
		/// <summary>
		/// Returns whether or not at least one possible match remains, based on the contents of <c>availableCards</c>
		/// </summary>
		public bool MatchesPossible() {
			if ( availableCards.Count + DeckOfIDs.Count >= SelectionGroup.MAX_CAPACITY ) { // -- NEED AT LEAST 3 CARDS
				if ( availableWildCards.Count < 1 ) { // --------------------------------------- WILDCARD GUARANTEES A MATCH, SO ONLY TEST IF THERE ARE NONE
					List<int> testGroup = new List<int>();
					List<int> hitMeCards = new List<int>();
					foreach( CardCrystallonEntity c in availableCards ){
						testGroup.Add(c.id);
					}
					if (   LevelManager.Instance.HitMeDisabled == false 
					    && DeckOfIDs.Count > 0) {	// ----------------------------------------- IF CARDS CAN BE ADDED, INCLUDE THEM TOO
						var count = 3;
						if ( DeckOfIDs.Count < 3 ) {
							count = DeckOfIDs.Count;
						}
						
						testGroup.AddRange( DeckOfIDs.GetRange(0, count) );
						hitMeCards.AddRange( DeckOfIDs.GetRange(0, count) );
					}
			
					if ( QualityManager.Instance.CheckForMatch( testGroup.ToArray(), hitMeCards.Count ) ) {
#if DEBUG
						Console.WriteLine("Possible Sets Remain: TRUE");
#endif
						return true;
					}
				} else {
#if DEBUG
					Console.WriteLine("Possible Sets Remain: TRUE (Wildcard)");
#endif
					return true;
				}
			}
#if DEBUG
			Console.WriteLine("Possible Sets Remain: FALSE");
#endif
			return false;
		}
		
		/// <summary>
		/// Spawn cards until we run out of cards to spawn, or hit the population cap.
		/// </summary>
		public void Populate ( bool pForce = false ) {
			if ( DeckOfIDs == null ) {
				BuildDeckOfIDs();
				if (GameScene.currentLevel == 999) {
					PrepareWildcards();
					QualityManager.Instance.scoringQualityList.Clear();
					QualityManager.Instance.scoringQualityList.Add("QColor");
				}
			}
			
			// Player can add 3 cards above the normal limit
			int fillPop = pForce ? LevelManager.Instance.StandardPop + 3 : LevelManager.Instance.StandardPop;
			if (GameScene.currentLevel == 999) {
				fillPop -= 3; // --------------------------------------------------- Lower limits to 12 & 15
			}
			while ( availableCards.Count < fillPop && TotalCardsInDeck > 0) {
				var card = spawn (DeckOfIDs[0]);
				DeckOfIDs.RemoveAt(0);
			}
		}
		
		protected void PrepareWildcards() {
			if (WildcardIDs == null) {
				WildcardIDs = new List<int>();
			}
			WildcardIDs.Clear();
			int range = (int)Sce.PlayStation.Core.FMath.Min ( WILDCARD_BASE_GROUP, DeckOfIDs.Count );
			var list = DeckOfIDs.GetRange(0, range);
			for (int i = 0; i < MAX_WILDCARDS; i++) {
				var ix = (int)Sce.PlayStation.Core.FMath.Floor(GameScene.Random.NextFloat() * list.Count);
				WildcardIDs.Add( list[ix] ); 
				list.RemoveAt(ix);
			}
			WildcardGroupCounter = 0;
		}
		
		/// <summary>
		/// Reset the <c>CardManager</c>. Probably want to call this before starting a new level.
		/// </summary>
		public void Reset ( Scene pScene ) {
			foreach( var card in availableCards ) {
				card.removeFromScene( true );
			}
			availableCards.Clear();
			availableWildCards.Clear ();
			NextId = 0;
			_scene = pScene;
			if (DeckOfIDs != null) {
				DeckOfIDs.Clear();
			}
			DeckOfIDs = null;
			WildcardGroupCounter = 0;
		}
		
		/// <summary>
		/// Remove a <c>CardCrystallonEntity</c> from <c>availableCards</c>.
		/// </summary>
		/// <param name='pCard'>
		/// <see cref="Crystallography.CardCrystallonEntity"/>
		/// </param>
		private void rm( CardCrystallonEntity pCard ) {
			availableCards.Remove(pCard);
			if(pCard.Wild) {
				availableWildCards.Remove(pCard);
			}
		}
		
		/// <summary>
		/// Puts the cards in the deck in a random order.
		/// </summary>
		protected void ShuffleDeckOfIDs() {
			var d = new List<int>();
			for( int i=0; i<TotalCardsInDeck; i++ ) {
				var card = DeckOfIDs[ (int)System.Math.Floor(GameScene.Random.NextFloat() * DeckOfIDs.Count) ];
				d.Add(card);
				DeckOfIDs.Remove(card);
			}
			DeckOfIDs = d;
		}
		
		/// <summary>
		/// Creates a new <c>CardCrystallonEntity</c>, adds it to the current scene at a random location.
		/// </summary>
		public CardCrystallonEntity spawn( int pId = -1) {
			float x = LevelManager.Instance.SpawnRect.X + LevelManager.Instance.SpawnRect.Z * GameScene.Random.NextFloat();
			float y = LevelManager.Instance.SpawnRect.Y + LevelManager.Instance.SpawnRect.W * GameScene.Random.NextFloat();
			
			return spawn( x , y, pId);
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
			
			// INSTANTIATE
			if (pId == -1){
				pId = NextId;
				NextId++;
			}
			CardCrystallonEntity card = BuildCard(pId);
			TotalCardsInDeck--;
			if (GameScene.currentLevel == 999) {
				WildcardGroupCounter = (WildcardGroupCounter + 1)%WILDCARD_BASE_GROUP;
				if (WildcardGroupCounter == 0) {
					PrepareWildcards();
				}
			}
			
			// APPLY QUALITIES
			card.ApplyQualities();		
			
			// ADD TO WORLD
			card.setPosition( pX, pY );
			card.addToScene();
			card.Visible = false;
			
			// TRIGGER SPAWN VFX
			Sequence sequence = new Sequence(){ Tag=2 };
			sequence.Add( new DelayTime( 0.2f ));
			sequence.Add( new CallFunc( () => {card.FadeIn();} ) );
			sequence.Add( new DelayTime(2.0f) );
			card.getNode().RunAction( sequence );
			EventHandler handler = CardSpawned;
			if (handler != null) {
				handler( this, null );
			}
			
			return card;
		}
		
		/// <summary>
		/// Teleport the specified pCard.
		/// </summary>
		/// <param name='pCard'>
		/// P card.
		/// </param>
		public void Teleport( CardCrystallonEntity pCard ) {
			pCard.Visible = false;
			var _screenWidth = Director.Instance.GL.Context.GetViewport().Width * 0.6f + 220.0f;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height * 0.75f + 50.0f;
			pCard.setPosition( _screenWidth * GameScene.Random.NextFloat(), _screenHeight * GameScene.Random.NextFloat() );
			pCard.getNode().RunAction( new CallFunc( () => { pCard.FadeIn(); } ) );
		}
		
		
		public void TintAllCards(float pDuration = 1.0f) {
			foreach (var card in availableCards) {
				card.TintTo(QColor.palette[card.getColor()], pDuration, false);
			}
		}
		
		public void HideAllCards(bool pHide=true) {
			foreach (var card in availableCards) {
				card.getNode().Visible = !pHide;
			}
		}
		
		public void RotateColors( int rotations, float pRotateTime, float pRestTime=1.0f) {
			Director.Instance.CurrentScene.StopActionByTag(30);
			var cRot = new Sequence();
			cRot.Add( new CallFunc( () => {
				QColor.Instance.rotatePalette();
			} ));
			cRot.Add( new CallFunc( () => {
				TintAllCards( pRotateTime );
			} ));
			cRot.Add( new DelayTime(pRotateTime + pRestTime) );
			if (rotations > 0) {
				Director.Instance.CurrentScene.RunAction(new Repeat(cRot, rotations) {Tag = 30});
			} else {
				Director.Instance.CurrentScene.RunAction(new RepeatForever() {Tag = 30, InnerAction = cRot});
			}
		}
		
		// DESTRUCTOR ----------------------------------------------------------------------------------------
#if DEBUG
		~CardManager() {
			Console.WriteLine(GetType().ToString() + " deleted" );
		}
#endif
	}
}

