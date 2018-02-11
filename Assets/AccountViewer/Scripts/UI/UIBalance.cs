using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk.responses;

namespace AccountViewer.UI
{
    public class UIBalance : MonoBehaviour
    {
        public Transform assetsParent;
        public GameObject assetPrefab;

        private Dictionary<string, UIAsset> uiAssets = new Dictionary<string, UIAsset>();

        private void InstantiateUIAsset(string assetID, Balance balance)
        {
            //Instantiate Prefab
            GameObject assetInstance = Instantiate(assetPrefab);
            assetInstance.transform.SetParent(assetsParent, false);

            //Set Data
            UIAsset uiAsset = assetInstance.GetComponent<UIAsset>();
            uiAsset.Setup(assetID, balance);

            //Add this to the UIAsset dictionary
            uiAssets.Add(assetID, uiAsset);
        }
    }
}