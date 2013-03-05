using System;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class GroupManager
	{
		public static List<GroupCrystallonEntity> availableGroups;
		protected Scene _scene;
		protected GamePhysics _physics;
		
		// GET & SET -----------------------------------------------------------------------
		
		public static GroupManager Instance { get; private set; }
		
		// CONSTRUCTOR ---------------------------------------------------------------------
		
		public GroupManager ( Scene pScene, GamePhysics pPhysics )
		{
			if(Instance == null) {
				Instance = this;
				availableGroups = new List<GroupCrystallonEntity>();
				_scene = pScene;
				_physics = pPhysics;
			}
		}
		
		// METHODS -------------------------------------------------------------------------
		
		/// <summary>
		/// Add one specified GroupCrystallonEntity to the group manager's list. DOES NOT add to The Scene.
		/// </summary>
		/// <param name='pGroup'>
		/// The GroupCrystallonEntity.
		/// </param>
		private GroupCrystallonEntity add( GroupCrystallonEntity pGroup ) {
			availableGroups.Add(pGroup);
			return pGroup;
		}
		
		/// <summary>
		/// Remove a group from the Scene, with the option to delete it entirely
		/// </summary>
		/// <param name='pGroup'>
		/// <see cref="Crystallography.GroupCrystallonEntity"/>
		/// </param>
		/// <param name='pDelete'>
		/// Remove all references to the group from the GroupManager? Defaults to <c>false</c>.
		/// </param>
		public void Remove( GroupCrystallonEntity pGroup, bool pDelete=false ) {
			if ( pDelete ) {
				rm ( pGroup );
			}
			pGroup.removeFromScene( pDelete );
		}
		
		/// <summary>
		/// Reset this instance. Removes all known groups from the scene, and clears the list.
		/// </summary>
		public void Reset () {
			foreach( var g in availableGroups ) {
				Remove ( g );
			}
			availableGroups.Clear();
		}
		
		/// <summary>
		/// Remove one specified GroupCrystallonEntity.
		/// </summary>
		/// <param name='pGroup'>
		/// Group to be removed.
		/// </param>
		private void rm( GroupCrystallonEntity pGroup ) {
			pGroup.RemoveAll();
			availableGroups.Remove(pGroup);
		}
		
		/// <summary>
		/// Spawn a new group at a random location.
		/// </summary>
		public GroupCrystallonEntity spawn() {
			var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			return spawn( 50f + 0.75f * _screenWidth * GameScene.Random.NextFloat(),
			       50f + 0.75f * _screenHeight * GameScene.Random.NextFloat());
		}
		
		/// <summary>
		/// Spawn a new group the specified pX and pY.
		/// </summary>
		/// <param name='pX'>
		/// X coordinate
		/// </param>
		/// <param name='pY'>
		/// Y coordinate
		/// </param>
		public GroupCrystallonEntity spawn( float pX, float pY, bool pComplete = false ) {
			var ss = SpriteSingleton.getInstance();
			GroupCrystallonEntity g = new GroupCrystallonEntity(_scene, _physics, _physics.SceneShapes[0], 
			                                                    SelectionGroup.MAX_CAPACITY, pComplete);
			g.setPosition( pX, pY );
			g.addToScene();
			return add(g);
		}
	}
}