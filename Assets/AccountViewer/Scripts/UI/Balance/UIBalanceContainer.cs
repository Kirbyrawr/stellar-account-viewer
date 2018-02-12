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
        public GameObject balanceObjectPrefab;

        private BalanceController balanceController;
        private Dictionary<string, UIBalanceObject> balanceObjects = new Dictionary<string, UIBalanceObject>();

        public override void Start() 
        {
            base.Start();
            balanceController = UIController.GetInstance().mainController.balance;
            balanceController.OnAddBalance += OnAddBalance;
        }

        private void OnAddBalance(string id, Balance balance) 
        {
            InstantiateBalanceObject(id, balance);
        }

        private void InstantiateBalanceObject(string id, Balance balance)
        {
            //Instantiate Prefab
            GameObject assetInstance = Instantiate(balanceObjectPrefab);
            assetInstance.transform.SetParent(contentParent, false);

            //Set Data
            UIBalanceObject balanceObject = assetInstance.GetComponent<UIBalanceObject>();
            balanceObject.Setup(id, balance);

            //Add this to the dictionary
            balanceObjects.Add(id, balanceObject);
        }
    }
}