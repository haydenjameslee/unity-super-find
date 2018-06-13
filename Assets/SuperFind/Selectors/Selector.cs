using UnityEngine;

namespace SuperFindPlugin
{
    public class Selector
    {
        private string _name;
        private Flair[] _flairs;

        public Selector(string name) {
            _name = name;
        }

        public Selector(string name, Flair[] flairs) {
            _name = name;
            _flairs = flairs;
        }

        public static Selector FromString(string selectorStr) {
            string[] splitSelector = selectorStr.Split(':');
            if (splitSelector.Length == 1) {
                return new Selector(selectorStr);
            } else {
                string name = splitSelector[0];
                Flair[] flairs = new Flair[splitSelector.Length - 1];
                for (int i = 1; i < splitSelector.Length; i++) {
                    string flairStr = splitSelector[i];
                    Flair flair = Flair.FromString(flairStr);
                    flairs[i - 1] = flair;
                }
                return new Selector(name, flairs);
            }
        }

        public bool Match(Transform transform) {
            if (_flairs != null) {
                return MatchName(transform.name) && MatchFlairs(transform);
            } else {
                return MatchName(transform.name);
            }
        }

        private bool MatchName(string nodeName) {
            return _name == Constants.Wildcard || nodeName == _name;
        }

        private bool MatchFlairs(Transform transform) {
            foreach (Flair flair in _flairs) {
                if (!flair.Match(transform, _name)) {
                    return false;
                }
            }
            return true;
        }
    }
}