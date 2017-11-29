using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using SharedGameData.Level_Classes;

namespace SharedGameData.Editor
{
    public delegate void ObjectSelectionChange(object obj);
    public delegate void LoadedLevelChange(object obj);

    public static class STATIC_EDITOR_MODE
    {      
        public static ContentManager contentMan;

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
    }
}
