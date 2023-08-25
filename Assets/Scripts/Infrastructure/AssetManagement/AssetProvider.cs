using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string heroPath)
        {
            var prefab = Resources.Load<GameObject>(heroPath);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string heroPath, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(heroPath);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}
