using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssets
    {
        GameObject Instantiate(string heroPath);
        GameObject Instantiate(string heroPath, Vector3 at);
        T Load<T>(string path) where T : Object;
    }
}
