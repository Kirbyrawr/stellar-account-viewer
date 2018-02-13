using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk.responses;
using AccountViewer.Controller.Balances;

namespace AccountViewer.UI.Balances
{
    public class UIBalanceContainer : UIModule
    {
        public Transform contentParent;
        public GameObject assetPrefab;

        private BalanceController balanceController;
        private Dictionary<string, UIAsset> assetsDictionary = new Dictionary<string, UIAsset>();

        protected override void Setup() 
        {
            balanceController = UIController.GetInstance().mainController.balance;
            balanceController.OnAddAsset += OnAddAsset;
        }

        private void OnAddAsset(string id, Balance balance) 
        {
            CreateAsset(id, balance);
        }

        private void CreateAsset(string id, Balance balance)
        {
            //Instantiate Prefab
            GameObject assetInstance = Instantiate(assetPrefab);
            assetInstance.transform.SetParent(contentParent, false);

            //Set Data
            UIAsset uiAsset = assetInstance.GetComponent<UIAsset>();
            uiAsset.Setup(id, balance);

            //Add this to the dictionary
            assetsDictionary.Add(id, uiAsset);
        }
    }
}