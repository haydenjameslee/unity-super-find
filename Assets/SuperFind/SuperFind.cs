using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SuperFind {

    public static GameObject Find(string selector) {
        var result = _Find(null, selector);
        if (result == null) {
            return null;
        } else {
            return result.gameObject;
        }
    }

    private static Transform _Find(Transform parent, string selector) {
        string[] selectors = selector.Split(' ');
        if (selectors.Length == 0) {
            return null;
        }
        else if (selectors.Length == 1) {
            var children = FindChildren(parent, selector);
            if (children == null || children.Length == 0) {
                return null;
            }
            return children[0];
        }
        else {
            Transform[] nextParents = FindChildren(parent, selectors[0]);
            string remainingSelector = CreateRemainingSelector(selectors);

            for (int i = 0; i < nextParents.Length; i++) {
                Transform result = _Find(nextParents[i].transform, remainingSelector);
                if (result != null) {
                    return result;
                }
            }
            return null;
        }
    }

    private static string CreateRemainingSelector(string[] selectors) {
        string remaining = "";
        for (int i = 1; i < selectors.Length; i++) {
            remaining += selectors[i];
        }
        return remaining;
    }

    private static Transform[] FindChildren(Transform parent, string withName) {
        if (parent == null) {
            return FindAll(withName);
        }

        Transform[] ts = parent.transform.GetComponentsInChildren<Transform>(true);
        List<Transform> gos = new List<Transform>();
        foreach (Transform t in ts) {
            if (t.gameObject.name == withName) {
                gos.Add(t);
            }
        }
        return gos.ToArray();
    }

    private static Transform[] FindAll(string name) {
        var gos = Resources.FindObjectsOfTypeAll<Transform>().Where(go => go.name == name);
        if (!gos.Any()) {
            return null;
        } else {
            return gos.ToArray();
        }
    }
}
