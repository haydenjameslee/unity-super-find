using SuperFindPlugin;
using UnityEditor;
using UnityEngine;

public class SuperFindInspector : EditorWindow
{
    private static string _input = "";

    [MenuItem("Window/SuperFindInspector")]
    public static void ShowWindow() {
        GetWindow(typeof(SuperFindInspector));
    }

    private void OnGUI() {
        _input = GUILayout.TextField(_input);

        if (GUILayout.Button("SuperFind")) {
            GameObject found = SuperFind.Find(_input);
            if (found != null) {
                Selection.SetActiveObjectWithContext(found, null);
            }
        }
    }
}
    