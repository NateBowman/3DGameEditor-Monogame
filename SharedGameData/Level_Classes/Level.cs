using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedGameData;

namespace SharedGameData.Level_Classes
{
    public class Level
    {
        public List<SceneEntity> Entities {get;set;}  //short form of a getter/setter

        public Level()
        {
            Entities = new List<SceneEntity>();
        }

    }
}
