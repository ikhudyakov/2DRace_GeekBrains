using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Bundle
{
    public class AssetBundleViewBase : MonoBehaviour
    {
        //private const string UrlAssetBundleRewardSprites = "https://drive.google.com/uc?export=download&id=1xJb5_M2T2yQ5wwlUZvMkMkbEA0i564L_";
        private const string UrlAssetBundleRewardSprites = "http://ilei.ml/reward_bundle";

        [SerializeField]
        private DataSpriteBundle[] _dataSpriteBundles;

        private AssetBundle _spritesAssetBundle;
        private CachedAssetBundle cachedAssetBundle = new CachedAssetBundle("Version", new Hash128(5L, 5L));

        protected IEnumerator DownloadAndSetAssetBundle()
        {
            yield return GetSpritesAssetBundle();

            if (_spritesAssetBundle == null)
            {
                Debug.LogError("Error in asset bundle");
                yield break;
            }

            SetDownloadAsset();
            yield return null;

            _spritesAssetBundle.Unload(false);
        }

        private void SetDownloadAsset()
        {
            foreach (var data in _dataSpriteBundles)
                data.Image.sprite = _spritesAssetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }

        private IEnumerator GetSpritesAssetBundle()
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleRewardSprites);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, ref _spritesAssetBundle);
        }

        private void StateRequest(UnityWebRequest request, ref AssetBundle assetBundle)
        {
            if (request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.LogError("COMPLETE!!!");
            }
            else
            {
                Debug.LogError(request.error);
                Debug.LogError("Ошибка");
            }
        }
    }
}