using UnityEngine;

namespace SuperFindPlugin
{
    public class IndexFlair : Flair
    {
        private int index;

        public IndexFlair(int index) {
            this.index = index;
        }

        public override bool Match(Transform transform, string name) {
            var parent = transform.parent;
            int i = 0;
            foreach (Transform child in parent) {
                if (child.gameObject.name == name) {
                    if (i == index && child == transform) {
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