using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperFindPlugin
{
    public class IndexFlair : Flair
    {
        private int index;

        public IndexFlair(int index) {
            this.index = index;
        }

        public override bool Match(Transform toMatch, string name) {
            var parent = toMatch.parent;
            if (parent == null) {
                GameObject[] roots = SceneManager.GetActiveScene().GetRootGameObjects();
                int i = 0;
                foreach (GameObject childGo in roots) {
                    Transform child = childGo.transform;
                    if (name == Constants.Wildcard || child.name == name) {
                        if (i == index && child == toMatch) {
                            return true;
                        } else if (i == index) {
                            return false;
                        }
                        i++;
                    }
                }
                return false;
            } else {
                int i = 0;
                foreach (Transform child in parent) {
                    if (name == Constants.Wildcard || child.name == name) {
                        if (i == index && child == toMatch) {
                            return true;
                        } else if (i == index) {
                            return false;
                        }
                        i++;
                    }
                }
                return false;
            }
        }
    }
}