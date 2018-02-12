using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using UnityEngine.UI;
using AccountViewer.Controller.Balances;

namespace AccountViewer.UI.Balances
{
    public class UIBalanceObject : MonoBehaviour
    {
        [ReadOnly]
        public string id;

        public Text label;

        private BalanceController balanceController;

        //Balance
        public Balance Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
                UpdateLabel();
            }
        }

        private Balance balance;

        public void Setup(string assetID, Balance assetBalance)
        {
            //Easy Instance
            balanceController = UIController.GetInstance().mainController.balance;

            //Set ID
            id = assetID;

            //Set Balance
            Balance = assetBalance;

            //Subscribe to callbacks
            balanceController.OnUpdateBalance += OnUpdateBalance;
        }

        public void OnUpdateBalance(string assetID, Balance assetBalance)
        {
            //Check if this asset changed;
            if (id != assetID) { return; }
            Balance = assetBalance;
        }

        private void UpdateLabel()
        {
            //Native Asset
            if (balance.AssetType == "native")
            {
                label.text = string.Concat(Balance.BalanceString, " ", "XLM");
            }

            //Non Native Asset
            else
            {
                label.text = string.Concat(Balance.BalanceString, " ", Balance.AssetCode);
            }
        }
    }
}