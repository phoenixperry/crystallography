using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.Physics2D;

namespace Crystallography
{
	public interface ICrystallonEntity {
		Node getNode();
		
		PhysicsBody getBody();
		
		Vector2 getPosition();
		
		int getQualityVariant( string pQualityName );
		
		void setBody( PhysicsBody body );
		
		void setNode( Node node );
		
		void addToScene();
		
		void attachTo( Node pNewParent );
		
		void removeFromScene( bool doCleanup );
	}
}