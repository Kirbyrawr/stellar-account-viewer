using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UStellar.Core;
using stellar_dotnetcore_sdk;
using AccountViewer.Controller.Accounts;
using AccountViewer.Controller.Balances;
using AccountViewer.Controller.Operations;
using AccountViewer.Controller.Transactions;

namespace AccountViewer.Controller
{
    public class MainController : MonoBehaviour
    {
        private static MainController instance;

        public AccountsController accounts;
        public BalanceController balance;
        public OperationsController operations;
        public TransactionsController transactions;
        
        public Server server;

        private void Awake()
        {
            SetInstance();
            JSONSerializer.Init();
            InitStellarSDK();
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

        private void InitStellarSDK()
        {
            UStellarManager.Init();
            server = UStellarManager.GetServer();
        }
    }
}