﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

        private OperationResponse operationResponse;

        public void Setup(long id, OperationResponse operationResponse)
        {
            //Easy Instance
            operationsController = UIController.GetInstance().mainController.operations;

            //Set ID
            this.id = id;

            //Set Balance
            this.operationResponse = operationResponse;
            
            operationLabel.text = operationResponse.Type;
            sourceAccount.text = operationResponse.SourceAccount.AccountId;
        }

        private void SetLabelData() 
        {
            OperationType operationType = operationsController.GetOperationResponseOperationType(operationResponse);

        }
    }
}