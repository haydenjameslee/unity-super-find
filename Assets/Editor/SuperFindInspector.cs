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

        if (GUILayout.Button("SuperFind.Find")) {
            GameObject found = SuperFind.Find(_input);
            if (found != null) {
                Selection.SetActiveObjectWithContext(found, null);
            }
        }

        if (GUILayout.Button("SuperFind.FindAll")) {
            GameObject[] found = SuperFind.FindAll(_input);
            if (found.Length != 0) {
                Selection.objects = found;
            }
        }
    }
}
    