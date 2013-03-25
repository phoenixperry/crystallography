using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace Crystallography.BG
{
	public class CrystallonBackgroundObject : Node
	{
//		private List<SpriteBase> Objects;
		protected readonly Vector2 BASE;
		protected readonly Vector2 RANGE;
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.CrystallonBackgroundObject"/> class.
		/// </summary>
		public CrystallonBackgroundObject ( Vector2 pBase, Vector2 pRange ) : base() {
			Position = BASE = pBase;
			RANGE = pRange;
#if DEBUG
			Console.WriteLine("CrystallonBackgroundObject created");
#endif
		}
		
		// OVERRIDES ---------------------------------------------------------------------------------------
		
		public override void Cleanup ()
		{
			foreach (SpriteBase s in Children){
				s.TextureInfo = null;
			}
			
			base.Cleanup ();
		}
		
		// METHODS -----------------------------------------------------------------------------------------
		
		public void OnMoveComplete() {
			Sequence sequence = new Sequence();
			sequence.Add( new DelayTime( GameScene.Random.NextFloat() * 1.0f ) );
			sequence.Add( new MoveTo( BASE + GameScene.Random.NextFloat() * RANGE, 1.0f + 1.0f * GameScene.Random.NextFloat() ) );
			sequence.Add( new CallFunc( () => { OnMoveComplete(); } ) );
			this.RunAction( sequence );
		}
		
		public void Start() {
			OnMoveComplete();
		}
		
		// DESTRUCTOR --------------------------------------------------------------------------------------

#if DEBUG		
		~CrystallonBackgroundObject() {
			Console.WriteLine("CrystallonBackgroundObject deleted");
		}
#endif

	}
}

