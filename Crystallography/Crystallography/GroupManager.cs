using System;
using System.Collections.Generic;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public class GroupManager
	{
		public static List<GroupCrystallonEntity> availableGroups;
		protected static GroupManager _instance;
		protected Scene _scene;
		protected GamePhysics _physics;
		
		// GET & SET -----------------------------------------------------------------------
		
		public static GroupManager Instance { 
			get{
				if (_instance == null) {
					return _instance = new GroupManager();
				}
				return _instance;
		}
			private set{
				_instance = value;
			}
		}
		
		public int NextId { get; private set; }
		
		// CONSTRUCTOR ---------------------------------------------------------------------
		
		protected GroupManager ()
		{
			availableGroups = new List<GroupCrystallonEntity>();
			_scene = Director.Instance.CurrentScene;
			_physics = GamePhysics.Instance;
			Reset( _scene );
#if DEBUG
			Console.WriteLine(GetType().ToString() + " created" );
#endif
		}
		
		// METHODS -------------------------------------------------------------------------
		
		/// <summary>
		/// Add one specified GroupCrystallonEntity to the group manager's list. DOES NOT add to The Scene.
		/// </summary>
		/// <param name='pGroup'>
		/// The GroupCrystallonEntity.
		/// </param>
		public GroupCrystallonEntity Add( GroupCrystallonEntity pGroup ) {
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
			pGroup.removeFromScene( pDelete );
			if ( pDelete ) {
				rm ( pGroup );
			}
		}
		
		/// <summary>
		/// Reset this instance. Removes all known groups from the scene, and clears the list.
		/// </summary>
		public void Reset ( Scene pScene ) {
			foreach( var g in availableGroups ) {
				Remove ( g );
			}
			availableGroups.Clear();
			NextId = 0;
			_scene = pScene;
		}
		
		/// <summary>
		/// Remove one specified GroupCrystallonEntity.
		/// </summary>
		/// <param name='pGroup'>
		/// Group to be removed.
		/// </param>
		private void rm( GroupCrystallonEntity pGroup ) {
			availableGroups.Remove(pGroup);
		}
		
		/// <summary>
		/// Spawn a new group at a random location.
		/// </summary>
		public GroupCrystallonEntity spawn(AbstractCrystallonEntity[] pMembers) {
			var _screenWidth = Director.Instance.GL.Context.GetViewport().Width;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height;
			return spawn( 50f + 0.75f * _screenWidth * GameScene.Random.NextFloat(),
			       50f + 0.75f * _screenHeight * GameScene.Random.NextFloat(),
			             pMembers);
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
		public GroupCrystallonEntity spawn( float pX, float pY, AbstractCrystallonEntity[] pMembers, bool pComplete = false ) {
			GroupCrystallonEntity g;
			if (pComplete) {
				g = new CubeCrystallonEntity(_scene, _physics, null);
			} else {
				g = new GroupCrystallonEntity(_scene, _physics, null, SelectionGroup.MAX_CAPACITY, pComplete);
			}
			foreach (AbstractCrystallonEntity e in pMembers) {
				g.Add(e);
			}
			g.id = NextId;
			NextId += 1;
			g.setPosition( pX, pY );
			g.BeReleased(g.getPosition());
			return g;
//			g.addToScene();
//			return Add(g);
		}
		
		public void Teleport( GroupCrystallonEntity pGroup ) {
			pGroup.Visible = false;
			var _screenWidth = Director.Instance.GL.Context.GetViewport().Width * 0.6f + 220.0f;
            var _screenHeight = Director.Instance.GL.Context.GetViewport().Height * 0.75f + 50.0f;
			pGroup.setPosition( _screenWidth * GameScene.Random.NextFloat(), _screenHeight * GameScene.Random.NextFloat() );
			pGroup.getNode().RunAction( new CallFunc( () => { FadeIn (pGroup); } ) );
		}
		
		protected void FadeIn( GroupCrystallonEntity pGroup) {
			pGroup.Visible = true;
			foreach( AbstractCrystallonEntity e in pGroup.members ) {
				if (e is CardCrystallonEntity) {
					(e as CardCrystallonEntity).HideGlow();
					(e.getNode() as SpriteBase).Color.W = 0.0f;
					e.getNode().RunAction( new TintBy( new Sce.PlayStation.Core.Vector4(0.0f, 0.0f, 0.0f, 1.0f), 2.0f) );
				}
			}
		}
		
		// DESTRUCTOR --------------------------------------------------------------------
#if DEBUG
		~GroupManager() {
			Console.WriteLine(GetType().ToString() + " deleted");
		}
#endif
	}
}