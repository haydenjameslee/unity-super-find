using System;
using UnityEngine;

namespace SuperFindPlugin
{
    public class FindVisitor : IVisitor
    {
        private Transform _result;
        private SelectorChain _selectors;

        public FindVisitor(SelectorChain selectors) {
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
            return _result != null;
        }

        public void Visit(TransformNode node) {
            Transform transform = node.GetTransform();
            if (MatchesSelectors(transform)) {
                _result = transform;
            }
        }

        private bool MatchesSelectors(Transform transform) {
            return _selectors.Match(transform);
        }

        public Transform GetTransform() {
            return _result;
        }
    }
}
