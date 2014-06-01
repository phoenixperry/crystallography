using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public interface ICrystallonEntity {
		System.Collections.Generic.List<Node> Children {get;}
		
		Node Parent {get;}
		
		bool Visible {get; set;}
		
		Node getNode();
		
		PhysicsBody getBody();
		
		Vector2 getPosition();
		
		Bounds2 getBounds();
		
		int getQualityVariant( string pQualityName );
		
		void setBody( PhysicsBody body );
		
		void setNode( Node node );
		
		void addToScene(int pLayerIndex);
		
		void attachTo( Node pNewParent );
		
		void removeFromScene( bool doCleanup );
		
		AbstractCrystallonEntity BeReleased( Vector2 position );
		
		AbstractCrystallonEntity BeAddedToGroup( GroupCrystallonEntity pGroup );
		
		AbstractCrystallonEntity BeSelected( float delay );
		
		bool CanBeAddedTo( GroupCrystallonEntity pGroup );
	}
}