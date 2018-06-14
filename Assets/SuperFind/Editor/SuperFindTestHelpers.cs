#pragma warning disable 0219

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

    public static GameObject CreateSiblingScene(int numSiblings, int returnIndex) {
        EditorSceneManager.OpenScene("Assets/SuperFind/Empty.unity");
        var root1 = new GameObject("Root");
        var root2 = new GameObject("Root");
        var middle = new GameObject("Middle");
        middle.transform.SetParent(root2.transform);

        GameObject[] children = new GameObject[numSiblings];
        for (int i = 0; i < numSiblings; i++) {
            var child = new GameObject("Child(Clone)");
            child.transform.SetParent(middle.transform);
            children[i] = child;
            child.transform.SetAsLastSibling();
        }
        return children[returnIndex];
    }

    public static GameObject CreateMultipleSetsofSiblingsScene(int numFirstSiblings, int numSecondSiblings, int returnIndex) {
        EditorSceneManager.OpenScene("Assets/SuperFind/Empty.unity");
        var root1 = new GameObject("Root");

        GameObject[] siblings1 = new GameObject[numFirstSiblings];
        for (int i = 0; i < numFirstSiblings; i++) {
            var child = new GameObject("Child(Clone)");
            child.transform.SetParent(root1.transform);
            siblings1[i] = child;
            child.transform.SetAsLastSibling();
        }

        var root2 = new GameObject("Root");
        var middle = new GameObject("Middle");
        middle.transform.SetParent(root2.transform);

        GameObject[] sublings2 = new GameObject[numSecondSiblings];
        for (int i = 0; i < numSecondSiblings; i++) {
            var child = new GameObject("Child(Clone)");
            child.transform.SetParent(middle.transform);
            sublings2[i] = child;
            child.transform.SetAsLastSibling();
        }
        return sublings2[returnIndex];
    }

    public static void CreateSceneWithSpacesInName() {
        EditorSceneManager.OpenScene("Assets/SuperFind/Empty.unity");
        var root = new GameObject("Root With Spaces");
        var middle = new GameObject("Middle With Spaces");
        middle.transform.SetParent(root.transform);
        var child = new GameObject("Child With Spaces");
        child.transform.SetParent(middle.transform);
    }
}