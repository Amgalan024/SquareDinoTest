using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Menu
{
    public class MenuConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _gameplayScene;
    }
}