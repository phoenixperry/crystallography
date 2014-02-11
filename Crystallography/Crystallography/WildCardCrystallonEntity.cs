using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;


namespace Crystallography
{
	public class WildCardCrystallonEntity : CardCrystallonEntity
	{		
		// CONSTRUCTORS ----------------------------------------
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Crystallography.WildCardCrystallonEntity"/> class.
		/// </summary>
		/// <param name='pScene'>
		/// The Parent Scene.
		/// </param>
		/// <param name='pGamePhysics'>
		/// Instance of <c>GamePhysics</c>
		/// </param>
		/// <param name='pTextureInfo'>
		/// <see cref="Sce.PlayStation.HighLevel.GameEngine2D.Base.TextureInfo"/>
		/// </param>
		/// <param name='pTileIndex2D'>
		/// <see cref="Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i"/>
		/// </param>
		/// <param name='pShape'>
		/// <see cref="Sce.PlayStation.HighLevel.Physics2D.PhysicsShape"/>
		/// </param>
		public WildCardCrystallonEntity (Scene pScene, GamePhysics pGamePhysics, int pId,
		                             TextureInfo pTextureInfo, Vector2i pTileIndex2D, PhysicsShape pShape)
													: base(pScene, pGamePhysics, pId, pTextureInfo, pTileIndex2D, pShape) {
			Wild = true;
			Flash ();
		}
		
		// OVERRIDES --------------------------------------------
		
		public override void ApplyQualities () {
			// WILDCARDS HAVE NO QUALITIES
			QualityManager.Instance.RemoveAll(this);
		}
		
		// METHODS ----------------------------------------------
		
		public void Flash() {
			Sequence sequence = new Sequence();
			sequence.Add( new CallFunc( () => TintTo( QColor.palette[1], 0.08f, false) ) );
			sequence.Add( new DelayTime(0.08f) );
			sequence.Add( new CallFunc( () => TintTo( QColor.palette[2], 0.08f, false) ) );
			sequence.Add( new DelayTime(0.08f) );
			sequence.Add( new CallFunc( () => TintTo( QColor.palette[0], 0.08f, false) ) );
			sequence.Add( new DelayTime(0.08f) );
			this.getNode().RunAction( new RepeatForever() { InnerAction=sequence, Tag = 40 } );
		}
	}
}

