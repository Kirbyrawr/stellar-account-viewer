using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses.operations;
using stellar_dotnetcore_sdk.responses;

using AccountViewer.Controller;
using AccountViewer.Controller.Accounts;
using AccountViewer.Controller.Operations;

namespace AccountViewer.UI.Operations
{
    public class UIOperationsContainer : UIModule
    {
        public Transform contentParent;
        public GameObject operationPrefab;
        private Dictionary<long, UIOperation> operations = new Dictionary<long, UIOperation>();

        protected override void Setup() 
        {
            mainController.accounts.OnSetAccount += OnSetAccount;
            mainController.operations.OnAddOperation += OnAddOperation;
        }

        private void OnSetAccount(AccountsController.Account account) 
        {
            foreach (var pair in operations)
            {
                Destroy(pair.Value.gameObject);
            }

            operations.Clear();
        }

        private void OnAddOperation(TransactionResponse transactionResponse, OperationResponse operationResponse) 
        {
            CreateOperation(transactionResponse, operationResponse);
        }

        private void CreateOperation(TransactionResponse transaction, OperationResponse operation)
        {
            //Instantiate Prefab
            GameObject operationInstance = Instantiate(operationPrefab);
            operationInstance.transform.SetParent(contentParent, false);

            //Set Data
            UIOperation uiOperation = operationInstance.GetComponent<UIOperation>();
            uiOperation.Setup(operation.Id, transaction, operation);

            //Add this to the dictionary
            operations.Add(operation.Id, uiOperation);
        }
    }
}