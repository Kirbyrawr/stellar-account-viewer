﻿using System;
using System.Collections;
using System.Collections.Generic;
using AccountViewer.Controller;
using AccountViewer.Controller.Operations;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.operations;
using UnityEngine;
using UnityEngine.UI;

namespace AccountViewer.UI.Operations {
    public class UIOperation : MonoBehaviour {
        [ReadOnly]
        public long id;

        public Text typeLabel;
        public Text detailsLabel;
        public Text amountLabel;
        public Text dateLabel;

        private MainController mainController;

        private TransactionResponse transactionResponse;
        private OperationResponse operationResponse;
        private UIOperationData uiOperationData;

        public void Setup (long id, TransactionResponse transactionResponse, OperationResponse operationResponse) {
            //Easy Instance
            mainController = UIController.GetInstance ().mainController;

            //Set ID
            this.id = id;

            //Set Data
            this.transactionResponse = transactionResponse;
            this.operationResponse = operationResponse;

            SetLabelData ();
        }

        public void OnClick () {
            Debug.Log ("Click");
        }

        public OperationResponse GetOperationResponse () {
            return operationResponse;
        }

        public TransactionResponse GetTransactionResponse () {
            return transactionResponse;
        }

        private void SetLabelData () {
            OperationType operationType = mainController.operations.GetOperationResponseOperationType (operationResponse);

            switch (operationType) {
                case OperationType.ACCOUNT_MERGE:
                    var accountMergeOperation = (AccountMergeOperationResponse) operationResponse;
                    typeLabel.text = "Account Merge";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;

                case OperationType.ALLOW_TRUST:
                    var allowTrustOperation = (AllowTrustOperationResponse) operationResponse;
                    typeLabel.text = "Allow Trust";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = DateTime.Parse (transactionResponse.CreatedAt).ToShortDateString ();
                    break;

                case OperationType.CHANGE_TRUST:
                    var changeTrustOperation = (ChangeTrustOperationResponse) operationResponse;
                    typeLabel.text = "Change Trust";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;

                case OperationType.CREATE_ACCOUNT:
                    var createAccountOperation = (CreateAccountOperationResponse) operationResponse;
                    typeLabel.text = "Create Account";
                    detailsLabel.text = string.Concat ("Starting Balance ➟ ", createAccountOperation.StartingBalance);
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;

                case OperationType.CREATE_PASSIVE_OFFER:
                    var createPassiveOfferOperation = (CreatePassiveOfferOperationResponse) operationResponse;
                    typeLabel.text = "Create Passive Offer";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;

                case OperationType.INFLATION:
                    var inflationOperation = (InflationOperationResponse) operationResponse;
                    typeLabel.text = "Set Inflation";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;

                case OperationType.MANAGE_DATA:
                    var manageDataOperation = (ManageDataOperationResponse) operationResponse;
                    typeLabel.text = "Manage Data";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;

                case OperationType.MANAGE_OFFER:
                    var manageOfferOperation = (ManageOfferOperationResponse) operationResponse;
                    typeLabel.text = "Manage Offer";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;

                case OperationType.PATH_PAYMENT:
                    var pathPaymentOperation = (PathPaymentOperationResponse) operationResponse;
                    typeLabel.text = "Path Payment";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;

                case OperationType.PAYMENT:
                    var paymentOperation = (PaymentOperationResponse) operationResponse;
                    if (paymentOperation.To.AccountId == MainController.GetInstance ().accounts.currentAccount.address) 
                    {
                        typeLabel.text = "Received";
                    } 
                    else 
                    {
                        typeLabel.text = "Sent";
                    }

                    //Check if the asset is native or not.
                    if (paymentOperation.AssetType == "native") 
                    {
                        amountLabel.text = string.Concat (paymentOperation.Amount, " ", "XLM");
                    } 
                    else 
                    {
                        amountLabel.text = string.Concat (paymentOperation.Amount, " ", paymentOperation.AssetCode);
                    }
                    break;

                case OperationType.SET_OPTIONS:
                    var setOptionsOperation = (SetOptionsOperationResponse) operationResponse;
                    typeLabel.text = "Set Options";
                    detailsLabel.text = string.Concat ("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;
            }
        }
    }
}