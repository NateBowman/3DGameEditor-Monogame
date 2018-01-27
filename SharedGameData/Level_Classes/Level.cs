namespace SharedGameData.Level_Classes {
    #region Usings

    using System.Collections.Generic;

    #endregion

    public class Level {
        public Level() {
            Entities = new List<SceneEntity>();
        }

        public List<SceneEntity> Entities { get; set; } //short form of a getter/setter

    }
}