using UnityEditor.SceneManagement;
using UnityEngine;

public static class SuperFindTestHelpers {

    public static void CreateSceneWithRoot() {
        EditorSceneManager.OpenScene("Assets/SuperFind/Empty.unity");
        var root = new GameObject("Root");
    }

    public static void CreateDeepScene() {
        EditorSceneManager.OpenScene("Assets/SuperFind/Empty.unity");
        var root = new GameObject("Root");
        var middle = new GameObject("Middle");
        middle.transform.SetParent(root.transform);
        var child = new GameObject("Child");
        child.transform.SetParent(middle.transform);
    }

    public static void CreateTrickyDeepScene() {
        EditorSceneManager.OpenScene("Assets/SuperFind/Empty.unity");
        var root1 = new GameObject("Root");

        var root2 = new GameObject("Root");
        var middle = new GameObject("Middle");
        middle.transform.SetParent(root2.transform);
        var child = new GameObject("Child");
        child.transform.SetParent(middle.transform);
    }
}