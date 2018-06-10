using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperFindPlugin
{
    public static class SuperFind {

        public static GameObject Find(string selector) {
            SelectorChain selectors = new SelectorChain(selector);
            FindVisitor visitor = new FindVisitor(selectors);
            SceneDescender descender = new SceneDescender(SceneManager.GetActiveScene());

            descender.Descend(visitor);

            Transform result = visitor.GetTransform();
            return result != null ? result.gameObject : null;
        }

        public static GameObject[] FindAll(string selector) {
            SelectorChain selectors = new SelectorChain(selector);
            FindAllVisitor visitor = new FindAllVisitor(selectors);
            SceneDescender descender = new SceneDescender(SceneManager.GetActiveScene());

            descender.Descend(visitor);

            Transform[] results = visitor.GetTransforms();
            GameObject[] output = GetGameObjectsFromTransforms(results);
            return output;
        }

        private static GameObject[] GetGameObjectsFromTransforms(Transform[] transforms) {
            GameObject[] gos = new GameObject[transforms.Length];
            for (int i = 0; i < transforms.Length; i++) {
                gos[i] = transforms[i].gameObject;
            }
            return gos;
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