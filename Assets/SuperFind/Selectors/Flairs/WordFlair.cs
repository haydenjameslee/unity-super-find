using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperFindPlugin
{
    public class WordFlair : Flair
    {
        private string word;

        public WordFlair(string word) {
            this.word = word;
        }

        public override bool Match(Transform toMatch, string name) {
            var parent = toMatch.parent;
            if (word == "first") {
                if (parent == null) {
                    GameObject[] roots = SceneManager.GetActiveScene().GetRootGameObjects();
                    foreach (GameObject go in roots) {
                        if (name == Constants.Wildcard || go.name == name) {
                            return go.transform == toMatch;
                        }
                    }
                } else {
                    foreach (Transform child in parent) {
                        if (name == Constants.Wildcard || child.name == name) {
                            return child == toMatch;
                        }
                    }
                }
                return false;
            } else if (word == "last") {
                if (parent == null) {
                    GameObject[] roots = SceneManager.GetActiveScene().GetRootGameObjects();
                    Transform lastFound = null;
                    foreach (GameObject go in roots) {
                        if (name == Constants.Wildcard || go.name == name) {
                            lastFound = go.transform;
                        }
                    }
                    return lastFound == toMatch;
                } else {
                    Transform lastFound = null;
                    foreach (Transform child in parent) {
                        if (name == Constants.Wildcard || child.name == name) {
                            lastFound = child;
                        }
                    }
                    return lastFound == toMatch;
                }
            }
            throw new Exception();
        }
    }
}