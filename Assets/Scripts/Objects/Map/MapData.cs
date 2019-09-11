using System;
using System.Collections.Generic;

namespace Objects.Map
{
    [Serializable]
    public class MapData {
        private List<MapElement> elements;
        private string levelName;

        public MapData()
        {
            elements = new List<MapElement>();
            levelName = "";
        }

        public MapData(string levelName)
        {
            elements = new List<MapElement>();
            this.levelName = levelName;
        }

        public List<MapElement> GetElements() { return elements; }
        public string GetLevelName() { return levelName; }

        public void AddElement(MapElement element)
        {
            elements.Add(element);
        }
    }
}
