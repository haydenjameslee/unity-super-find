using UnityEngine;

namespace SuperFindPlugin
{
    public class ComponentFlair : Flair
    {
        private string componentName;

        public ComponentFlair(string componentName) {
            this.componentName = componentName;
        }

        public override bool Match(Transform toMatch, string name) {
            return toMatch.GetComponent(componentName);
        }
    }
}