using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
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
		
		int getQualityVariant( string pQualityName );
		
		void setBody( PhysicsBody body );
		
		void setNode( Node node );
		
		void addToScene(int pLayerIndex);
		
		void attachTo( Node pNewParent );
		
		void removeFromScene( bool doCleanup );
		
		bool CanBeAddedTo( GroupCrystallonEntity pGroup );
	}
}