using System;
using UnityEngine;

namespace SuperFindPlugin
{
    public class Selector
    {
        private string _name;
        private Flair _flair;

        public Selector(string name) {
            _name = name;
        }

        public Selector(string name, Flair flair) {
            _name = name;
            _flair = flair;
        }

        public static Selector FromString(string selectorStr) {
            string[] splitSelector = selectorStr.Split(':');
            if (splitSelector.Length == 1) {
                return new Selector(selectorStr);
            } else {
                string name = splitSelector[0];
                string flairStr = splitSelector[1];
                Flair flair = Flair.FromString(flairStr);
                return new Selector(name, flair);
            }
        }

        public bool Match(Transform transform) {
            if (_flair != null) {
                return transform.name == _name && _flair.Match(transform, _name);
            } else {
                return transform.name == _name;
            }
        }
    }
}