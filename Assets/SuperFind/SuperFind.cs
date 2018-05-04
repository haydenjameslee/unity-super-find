using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SuperFindPlugin
{
    /**
     * Define SUPERFIND_DEBUG for debug logs
     */
    public static class SuperFind {

        public static GameObject Find(string selector) {
    #if SUPERFIND_DEBUG
            Debug.Log("Running SuperFind with selector: " + selector);
    #endif
            var result = _Find(null, selector);
            if (result == null) {
                return null;
            } else {
    #if SUPERFIND_DEBUG
                Debug.Log("Returning gameobject = " + result.gameObject + " id=" + result.gameObject.GetInstanceID());
    #endif
                return result.gameObject;
            }
        }

        private static Transform _Find(Transform parent, string selector) {
            string[] selectors = selector.Split(' ');
            if (selectors.Length == 0) {
                // Empty selector
    #if SUPERFIND_DEBUG
                Debug.Log("Return null for selector: " + selector + " -> Empty Selector");
    #endif
                return null;
            }
            else if (selectors.Length == 1) {
                // Leaf selector
    #if SUPERFIND_DEBUG
                Debug.Log("Leaf node: " + selectors[0]);
    #endif

                // Selector has flair, search through children to match with flair
                string[] splitSelector = selector.Split(':');
                string nakedSelector = splitSelector[0];

                Transform[] children = FindChildren(parent, nakedSelector);
                if (children == null || children.Length == 0) {
    #if SUPERFIND_DEBUG
                    Debug.Log("Return null for selector: " + selector + " -> None found");
    #endif
                    return null;
                }

                if (splitSelector.Length == 1) {
                    // Selector has no flair, return first matching GameObject
    #if SUPERFIND_DEBUG
                    Debug.Log("Returning " + children[0].name + " for selector: " + selector + " -> No flair found");
    #endif
                    return children[0];
                }

                string flairStr = splitSelector[1];
                Flair flair = Flair.FromString(flairStr);
                return flair.Find(children, nakedSelector);
            }
            else {
                // Branch selector
    #if SUPERFIND_DEBUG
                Debug.Log("Branch node: " + selectors[0]);
    #endif
                Transform[] nextParents = FindChildren(parent, selectors[0]);
                string remainingSelector = CreateRemainingSelector(selectors);

                for (int i = 0; i < nextParents.Length; i++) {
                    Transform result = _Find(nextParents[i].transform, remainingSelector);
                    if (result != null) {
    #if SUPERFIND_DEBUG
                        Debug.Log("Return "+ result.name + " for selector: " + selector);
    #endif
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
            List<Transform> output = new List<Transform>();
            foreach (Transform t in ts) {
                if (t.gameObject.name == withName) {
                    output.Add(t);
                }
            }
            return output.ToArray();
        }

        private static Transform[] FindAll(string name) {
            var gos = Resources.FindObjectsOfTypeAll<Transform>().Where(go => go.name == name);
            if (!gos.Any()) {
                return null;
            } else {
                return gos.ToArray();
            }
        }

        public static string NamesOfTransforms(Transform[] transforms, bool ids = false) {
            if (transforms == null) {
                return "";
            }
            string output = "";
            foreach (var t in transforms) {
                if (ids) {
                    output += t.name + ":" + t.gameObject.GetInstanceID() + ", ";
                } else {
                    output += t.name + ", ";
                }
            }
            if (output.Length > 0) {
                output = output.Remove(output.Length - 2);
            }
            return output;
        }
    }
}