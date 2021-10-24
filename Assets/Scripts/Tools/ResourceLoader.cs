using UnityEngine;

namespace Tools
{
    public static class ResourceLoader
    {
        public static GameObject LoadGameObject(ResourcePath path)
        {
            return Resources.Load<GameObject>(path.Path);
        }
    }
}