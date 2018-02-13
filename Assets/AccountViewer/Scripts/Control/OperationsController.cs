using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using AccountViewer.Controller.Accounts;
using stellar_dotnetcore_sdk.requests;
using stellar_dotnetcore_sdk.responses.operations;
using stellar_dotnetcore_sdk.responses.page;

namespace AccountViewer.Controller.Operations
{
    public class OperationsController : MonoBehaviour
    {
        public System.Action<OperationResponse> OnAddOperation;
        private MainController mainController;
        private Dictionary<string, Operation> operationsDictionary = new Dictionary<string, Operation>();

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
            mainController.accounts.OnUpdateAccountData += OnUpdateAccountData;
        }

        private void OnUpdateAccountData(AccountResponse response) 
        {
            GetOperationsForAccount(mainController.accounts.currentAccount);
        }

        private async void GetOperationsForAccount(AccountsController.Account account)
        {
            Page<OperationResponse> operationsPage = await mainController.server.Operations.ForAccount(account.data.KeyPair).Execute();

            for (int i = 0; i < 5; i++)
            {
                if(OnAddOperation != null) 
                {
                    OnAddOperation(operationsPage.Records[i]);
                }
            }
        }
    }
}