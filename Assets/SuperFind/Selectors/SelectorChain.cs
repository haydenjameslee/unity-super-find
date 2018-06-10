using System.Collections.Generic;
using UnityEngine;

namespace SuperFindPlugin
{
    public class SelectorChain
    {
        private List<Selector> _chain = new List<Selector>();

        public SelectorChain(string fullSelector) {
            string[] selectors = fullSelector.Split(' ');
            for (int i = 0; i < selectors.Length; i++) {
                var selector = Selector.FromString(selectors[i]);
                _chain.Add(selector);
            }
        }

        /**
         * Match conditions:
         * - Leaf selector matches this transform
         * - All selectors match a transform in the parent list, in order
         */
        public bool Match(Transform transform) {
            int currentSelectorIndex = _chain.Count - 1;
            Transform next = transform;

            // Check first node
            Selector leafSelector = _chain[currentSelectorIndex];
            if (!leafSelector.Match(transform)) {
                // Leaf does not match, therefore this node cannot match selector
                return false;
            }
            currentSelectorIndex--;

            if (next.parent == null && _chain.Count == 1) {
                // No parents and leaf matched, so this node is valid
                return true;
            }

            // Set next to parent and walk up through parents
            next = transform.parent;
            while (next != null && currentSelectorIndex >= 0) {
                Selector selector = _chain[currentSelectorIndex];
                if (selector.Match(next)) {
                    currentSelectorIndex--;
                }
                next = next.parent;
            }

            // If index is below 0 then all selectors in the chain have been matched
            return currentSelectorIndex < 0;
        }
    }
}
