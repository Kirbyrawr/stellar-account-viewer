using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;

using AccountViewer.Controller.Accounts;

namespace AccountViewer.Controller.Balances
{
    public class BalanceController : MonoBehaviour
    {
        public System.Action<string, Balance> OnAddAsset;
        public System.Action<string, Balance> OnUpdateAsset;

        private MainController mainController;
        private Dictionary<string, Balance> assetsDictionary = new Dictionary<string, Balance>();

        public void Start()
        {
            Setup();
        }

        private void Setup() 
        {
            mainController = MainController.GetInstance();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            mainController.accounts.OnSetAccount += OnSetAccount;
            mainController.accounts.OnUpdateAccountData += OnUpdateAccountData;
        }

        private void OnSetAccount(AccountsController.Account account) 
        {
            assetsDictionary.Clear();
        }

        private void OnUpdateAccountData(AccountResponse accountResponse)
        {
            Balance[] balances = accountResponse.Balances;

            for (int i = 0; i < balances.Length; i++)
            {
                Balance asset = balances[i];

                string assetID = null;

                if (asset.AssetType == "native")
                {
                    assetID = "native";
                }

                else
                {
                    assetID = asset.AssetCode + asset.AssetIssuer.AccountId;
                }

                //If we have the asset already added.
                if (assetsDictionary.ContainsKey(assetID))
                {
                    assetsDictionary[asset.AssetCode] = asset;
                    OnUpdateAsset(assetID, asset);
                }

                //If we don't have the asset already added.
                else
                {
                    assetsDictionary.Add(assetID, asset);
                    OnAddAsset(assetID, asset);
                }
            }
        }
    }
}