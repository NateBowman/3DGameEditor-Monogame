using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using SharedGameData.Level_Classes;

namespace SharedGameData.Editor
{
    using Microsoft.Xna.Framework.Graphics;

    public delegate void ObjectSelectionChange(object obj);
    public delegate void LoadedLevelChange(object obj);

    public static class STATIC_EDITOR_MODE
    {      
        public static ContentManager contentMan;


        /// <summary>
        /// Executes an action on child entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="action"></param>
        public static void ExecuteOnChildEntities(SceneEntity entity, Action<SceneEntity> action)
        {
            foreach (SceneEntity ent in LevelInstance.Entities.Where(sceneEntity => entity.Id == sceneEntity.ParentId))
            {
                ExecuteOnSelfAndChildEntities(ent, action);
            }
        }

        /// <summary>
        /// Executes an action on itself and all child entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="action"></param>
        public static void ExecuteOnSelfAndChildEntities(SceneEntity entity, Action<SceneEntity> action) {
            foreach (SceneEntity ent in LevelInstance.Entities.Where(sceneEntity => entity.Id == sceneEntity.ParentId)) {
                ExecuteOnSelfAndChildEntities(ent,action);
            }
            
            action(entity);
        }

        private static Level _levelInstance;
        public static Level LevelInstance
        {
            get
            {
                return _levelInstance;
            }
            set
            {
                _levelInstance = value;
                if(LevelChanged !=null)
                    LevelChanged(_levelInstance);
            }
        }

        public static bool bIsSomethingSelected = false;

        private static List<SceneEntity> _selectedObjects = null;
        public static List<SceneEntity> SelectedObjects
        {
            get
            {
                return _selectedObjects;
            }
            set
            {
                _selectedObjects = value;
                SelectionChanged(SelectedObjects);
            }
        }
        public static event ObjectSelectionChange SelectionChanged;
        public static event LoadedLevelChange LevelChanged;


        public static void RemoveEntity(SceneEntity entity)
        {
            LevelInstance.Entities.Remove(entity);
            SelectedObjects = new List<SceneEntity>();
            ResyncEngine();
        }

        private static void ResyncEngine() {
            Engine.Entities.Clear();
            Engine.Entities.AddRange(LevelInstance.Entities);
        }

        public static void RemoveAssetsWithHeight(string heightName)
        {
            LevelInstance.Entities.RemoveAll(actor => actor is TerrainEntity terrain && terrain.ModelName == heightName);
            SelectedObjects = new List<SceneEntity>();
            ResyncEngine();
        }

        public static void RemoveAssetsWithModel(string modelName)
        {
            LevelInstance.Entities.RemoveAll(actor => actor.ModelName == modelName);
            SelectedObjects = new List<SceneEntity>();
            ResyncEngine();
        }
    }
}
