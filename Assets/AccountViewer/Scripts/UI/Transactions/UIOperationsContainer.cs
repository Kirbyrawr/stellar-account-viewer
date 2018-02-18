using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccountViewer.Controller.Operations;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses.operations;
using stellar_dotnetcore_sdk.responses;

namespace AccountViewer.UI.Operations
{
    public class UIOperationsContainer : UIModule
    {
        public Transform contentParent;
        public GameObject operationPrefab;

        private OperationsController operationsController;
        private Dictionary<long, OperationResponse> operations = new Dictionary<long, OperationResponse>();

        protected override void Setup() 
        {
            operationsController = UIController.GetInstance().mainController.operations;
            operationsController.OnAddOperation += OnAddOperation;
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
            operations.Add(operation.Id, operation);
        }
    }
}