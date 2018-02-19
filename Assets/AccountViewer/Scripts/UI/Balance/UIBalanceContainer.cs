using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk.responses;
using AccountViewer.Controller;
using AccountViewer.Controller.Balances;
using AccountViewer.Controller.Accounts;

namespace AccountViewer.UI.Balances
{
    public class UIBalanceContainer : UIModule
    {
        public Transform contentParent;
        public GameObject assetPrefab;

        private MainController mainController;
        private Dictionary<string, UIAsset> assetsDictionary = new Dictionary<string, UIAsset>();

        protected override void Setup() 
        {
            mainController = UIController.GetInstance().mainController;
            mainController.balance.OnAddAsset += OnAddAsset;
            mainController.accounts.OnSetAccount += OnSetAccount;
        }

        private void OnAddAsset(string id, Balance balance) 
        {
            CreateAsset(id, balance);
        }

        private void OnSetAccount(AccountsController.Account account) 
        {
            foreach (var pair in assetsDictionary)
            {
                Destroy(pair.Value.gameObject);
            }

            assetsDictionary.Clear();
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