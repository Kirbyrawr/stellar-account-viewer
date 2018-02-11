using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;


namespace AccountViewer.Controller
{
    public class BalanceController : MonoBehaviour
    {
        private MainController mainController;
        private Dictionary<string, Balance> balanceDictionary = new Dictionary<string, Balance>();


        public void Start()
        {
            mainController = MainController.GetInstance();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            mainController.accounts.OnUpdateAccountData += OnUpdateAccountData;
        }

        private void OnUpdateAccountData(AccountResponse accountResponse)
        {
            Balance[] balances = accountResponse.Balances;

            for (int i = 0; i < balances.Length; i++)
            {
                Balance balance = balances[i];

                string assetID = null;

                if (balance.AssetType == "native")
                {
                    assetID = "native";
                }

                else
                {
                    assetID = balance.AssetCode + balance.AssetIssuer.AccountId;
                }

                //If we have the asset already added.
                if (balanceDictionary.ContainsKey(assetID))
                {
                    balanceDictionary[balance.AssetCode] = balance;
                    //OnUpdateBalance(assetID, balance);
                }

                //If we don't have the asset already added.
                else
                {
                    balanceDictionary.Add(assetID, balance);
                    //InstantiateUIAsset(assetID, balance);
                }
            }
        }
    }
}