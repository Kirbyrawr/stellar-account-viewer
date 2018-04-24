using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccountViewer.Controller.Accounts;
using AccountViewer.Controller.Balances;
using AccountViewer.Controller.Operations;
using AccountViewer.Controller.Transactions;

namespace AccountViewer.Controller
{
    public class MainController : MonoBehaviour
    {
        private static MainController instance;
        
        public NetworksController networks;
        public AccountsController accounts;
        public BalanceController balance;
        public TransactionsController transactions;
        public OperationsController operations;

        private void Awake()
        {
            SetInstance();
        }

        public static MainController GetInstance()
        {
            return instance;
        }

        public void SetInstance()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogWarning("Deleting duplicated instance");
                Destroy(this);
            }
        }

        void Start() 
        {
            UStellar.Core.UStellarManager.SetStellarTestNetwork();
            UStellar.Core.UStellarManager.Init();
        }
    }
}