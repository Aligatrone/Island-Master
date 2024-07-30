using UnityEditor;
using UnityEngine;

namespace _Scripts.MapGeneration.Editor
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : UnityEditor.Editor 
    {
        public override void OnInspectorGUI() 
        {
            MapGenerator mapGenerator = (MapGenerator)target;

            if(DrawDefaultInspector())
                if(mapGenerator.autoUpdate)
                    mapGenerator.GenerateMap();
    
            if(GUILayout.Button("Generate"))
                mapGenerator.GenerateMap();
        }
    }
}
