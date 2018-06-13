using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperFindPlugin
{
    public class FindAllVisitor : IVisitor
    {
        private List<Transform> _foundTransforms = new List<Transform>();
        private SelectorChain _selectors;

        public FindAllVisitor(SelectorChain selectors) {
            _selectors = selectors;
        }

        public void Visit(INode node) {
            var transformNode = node as TransformNode;
            if (transformNode == null) {
                throw new InvalidCastException("TransformNode to INode");
            }
            Visit(transformNode);
        }

        public bool ShortCircuit() {
            return false;
        }

        public void Visit(TransformNode node) {
            Transform transform = node.GetTransform();

            if (MatchesSelectors(transform)) {
                _foundTransforms.Add(transform);
            }
        }

        private bool MatchesSelectors(Transform transform) {
            return _selectors.Match(transform);
        }

        public Transform[] GetTransforms() {
            return _foundTransforms.ToArray();
        }
    }
}
