using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using UnityEngine.UI;
using AccountViewer.Controller.Balances;

namespace AccountViewer.UI.Balances
{
    public class UIAsset : MonoBehaviour
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

        public void Setup(string id, Balance balance)
        {
            //Easy Instance
            balanceController = UIController.GetInstance().mainController.balance;

            //Set ID
            this.id = id;

            //Set Balance
            Balance = balance;

            //Subscribe to callbacks
            balanceController.OnUpdateAsset += OnUpdateBalance;
        }

        public void OnUpdateBalance(string id, Balance balance)
        {
            //Check if this asset changed
            if (this.id != id) 
            {
                Balance = balance;
            }
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