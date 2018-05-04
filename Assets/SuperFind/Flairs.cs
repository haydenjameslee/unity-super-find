using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperFindPlugin
{
    public abstract class Flair {

        public static string[] ValidWords = {
            "first",
            "last",
        };

        public static Flair FromString(string flairStr) {
            int parsed;
            bool isNumeric = int.TryParse(flairStr, out parsed);
            if (isNumeric) {
                return new IndexFlair(parsed);
            } else if (ValidateWord(flairStr)) {
                return new WordFlair(flairStr);
            } else {
                throw new Exception("Flair " + flairStr + " is not a valid flair.");
            }
        }

        public static bool ValidateWord(string word) {
            return Array.Exists(ValidWords, element => element == word);
        }

        public abstract Transform Find(Transform[] haystack, string nakedSelector);
    }

    public class IndexFlair : Flair {

        private int index;

        public IndexFlair(int index) {
            this.index = index;
        }

        public override Transform Find(Transform[] haystack, string nakedSelector) {
            foreach (var posibility in haystack) {
                var parent = posibility.parent;
                int i = 0;
                foreach (Transform transform in parent) {
                    if (transform.gameObject.name == nakedSelector) {
                        if (i == index) {
                            return transform;
                        }
                        i++;
                    }
                }
            }
            return null;
        }
    }

    public class WordFlair : Flair {

        private string word;

        public WordFlair(string word) {
            this.word = word;
        }

        public override Transform Find(Transform[] haystack, string nakedSelector) {
            var posibility = haystack[haystack.Length - 1];
            var parent = posibility.parent;
            if (word == "first") {
                if (parent == null) {
                    GameObject[] roots = SceneManager.GetActiveScene().GetRootGameObjects();
                    foreach (GameObject go in roots) {
                        if (go.name == nakedSelector) {
                            return go.transform;
                        }
                    }
                } else {
                    foreach (Transform transform in parent) {
                        if (transform.gameObject.name == nakedSelector) {
                            return transform;
                        }
                    }
                }
                return null;
            } else if (word == "last") {
                Transform lastFound = null;
                foreach (Transform transform in parent) {
                    if (transform.gameObject.name == nakedSelector) {
                        lastFound = transform;
                    }
                }
                return lastFound;
            }
            throw new Exception();
        }
    }
}