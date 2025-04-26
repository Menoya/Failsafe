using Failsafe.Scripts;
using UnityEditor;
using UnityEngine;

namespace Failsafe.Editor
{
    [RequireComponent(typeof(DungeonGenerator))]
    [CustomEditor(typeof(DungeonGenerator))]
    public class GeneratorEditor:UnityEditor.Editor {


        public override void OnInspectorGUI() {

            //Generator g = (Generator)target;
            //Generator.voxelScale = EditorGUILayout.FloatField("VoxelScale: ", Generator.voxelScale);

            base.OnInspectorGUI();
        }

    }
}
  