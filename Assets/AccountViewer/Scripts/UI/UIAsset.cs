using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using UnityEngine.UI;

namespace AccountViewer.UI
{
    public class UIAsset : MonoBehaviour
    {
        [ReadOnly]
        public string id;

        public Text label;

        //Balance
        private UIBalance uiBalance;

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
            //Set UIBalance instance;
            //balanceController = MainController.GetInstance();

            //Set ID
            id = assetID;

            //Set Balance
            Balance = assetBalance;

            //Subscribe to Balance updates
            //balanceController.OnUpdateBalance += OnUpdateBalance;
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