using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using UnityEngine.UI;
using AccountViewer.Controller.Operations;
using stellar_dotnetcore_sdk.responses.operations;

namespace AccountViewer.UI.Operations
{
    public class UIOperation : MonoBehaviour
    {
        [ReadOnly]
        public long id;

        public Text operationLabel;
        public Text sourceAccount;

        private OperationsController operationsController;

        private OperationResponse operation;

        public void Setup(long id, OperationResponse operation)
        {
            //Easy Instance
            operationsController = UIController.GetInstance().mainController.operations;

            //Set ID
            this.id = id;

            //Set Balance
            this.operation = operation;
            
            operationLabel.text = operation.Type;
            sourceAccount.text = operation.SourceAccount.AccountId;
        }
    }
}