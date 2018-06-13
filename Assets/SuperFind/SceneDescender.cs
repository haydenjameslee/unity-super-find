using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperFindPlugin
{
    public interface IDescender
    {
        void Descend(IVisitor visitor);
    }

    public class SceneDescender : IDescender
    {
        private Scene _scene;
        private IVisitor _visitor;

        public SceneDescender(Scene scene) {
            _scene = scene;
        }

        public void Descend(IVisitor visitor) {
            _visitor = visitor;

            GameObject[] roots = _scene.GetRootGameObjects();
            for (int i = 0; i < roots.Length; i++) {
                INode node = new TransformNode(roots[i].transform);
                Descend(node);
                if (_visitor.ShortCircuit()) {
                    return;
                }
            }
        }

        private void Descend(INode node) {
            _visitor.Visit(node);
            if (_visitor.ShortCircuit()) {
                return;
            }

            foreach (object child in node) {
                INode childNode = child as TransformNode;
                if (childNode == null) {
                    throw new InvalidCastException("TransformNode to INode");
                }
                Descend(childNode);
                if (_visitor.ShortCircuit()) {
                    return;
                }
            }
        }
    }
}