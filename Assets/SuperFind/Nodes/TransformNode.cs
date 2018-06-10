using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperFindPlugin
{
    public class TransformNode : INode
    {
        private readonly Transform _transform;
        private List<TransformNode> _children = new List<TransformNode>();

        public TransformNode(Transform transform) {
            _transform = transform;

            foreach (Transform child in transform) {
                _children.Add(new TransformNode(child));
            }
        }

        public IEnumerator GetEnumerator() {
            return _children.GetEnumerator();
        }

        public Transform GetTransform() {
            return _transform;
        }
    }
}