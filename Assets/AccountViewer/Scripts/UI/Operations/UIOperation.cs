using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using stellar_dotnetcore_sdk;
using UnityEngine.UI;
using AccountViewer.Controller;
using AccountViewer.Controller.Operations;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.operations;

namespace AccountViewer.UI.Operations
{
    public class UIOperation : MonoBehaviour
    {
        [ReadOnly]
        public long id;

        public Text operationTypeLabel;
        public Text operationDetailsLabel;
        public Text dateLabel;

        private MainController mainController;

        private TransactionResponse transactionResponse;
        private OperationResponse operationResponse;

        public void Setup(long id, TransactionResponse transactionResponse, OperationResponse operationResponse)
        {
            //Easy Instance
            mainController = UIController.GetInstance().mainController;

            //Set ID
            this.id = id;

            //Set Data
            this.transactionResponse = transactionResponse;
            this.operationResponse = operationResponse;

            SetLabelData();
        }

        private void SetLabelData()
        {
            OperationType operationType = mainController.operations.GetOperationResponseOperationType(operationResponse);

            switch (operationType)
            {
                case OperationType.ACCOUNT_MERGE:
                    var accountMergeOperation = (AccountMergeOperationResponse)operationResponse;
                    operationTypeLabel.text = "Account Merge";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;


                case OperationType.ALLOW_TRUST:
                    var allowTrustOperation = (AllowTrustOperationResponse)operationResponse;
                    operationTypeLabel.text = "Allow Trust";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = DateTime.Parse(transactionResponse.CreatedAt).ToShortDateString();
                    break;


                case OperationType.CHANGE_TRUST:
                    var changeTrustOperation = (ChangeTrustOperationResponse)operationResponse;
                    operationTypeLabel.text = "Change Trust";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;


                case OperationType.CREATE_ACCOUNT:
                    var createAccountOperation = (CreateAccountOperationResponse)operationResponse;
                    operationTypeLabel.text = "Create Account";
                    operationDetailsLabel.text = string.Concat("Starting Balance ➟ ", createAccountOperation.StartingBalance);
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;


                case OperationType.CREATE_PASSIVE_OFFER:
                    var createPassiveOfferOperation = (CreatePassiveOfferOperationResponse)operationResponse;
                    operationTypeLabel.text = "Create Passive Offer";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;


                case OperationType.INFLATION:
                    var inflationOperation = (InflationOperationResponse)operationResponse;
                    operationTypeLabel.text = "Set Inflation";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;


                case OperationType.MANAGE_DATA:
                    var manageDataOperation = (ManageDataOperationResponse)operationResponse;
                    operationTypeLabel.text = "Manage Data";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;


                case OperationType.MANAGE_OFFER:
                    var manageOfferOperation = (ManageOfferOperationResponse)operationResponse;
                    operationTypeLabel.text = "Manage Offer";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;


                case OperationType.PATH_PAYMENT:
                    var pathPaymentOperation = (PathPaymentOperationResponse)operationResponse;
                    operationTypeLabel.text = "Path Payment";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;


                case OperationType.PAYMENT:
                    var paymentOperation = (PaymentOperationResponse)operationResponse;
                    if (paymentOperation.To.AccountId == mainController.accounts.currentAccount.address)
                    {
                        operationTypeLabel.text = "Received";
                    }
                    else 
                    {
                        operationTypeLabel.text = "Sent";
                    }

                    if (paymentOperation.AssetType == "native")
                    {
                        operationDetailsLabel.text = string.Concat(paymentOperation.Amount, " ", "XLM");
                    }
                    else
                    {
                        operationDetailsLabel.text = string.Concat(paymentOperation.Amount, " ", paymentOperation.AssetCode);
                    }
                    dateLabel.text = DateTime.Parse(transactionResponse.CreatedAt).ToShortDateString();
                    break;


                case OperationType.SET_OPTIONS:
                    var setOptionsOperation = (SetOptionsOperationResponse)operationResponse;
                    operationTypeLabel.text = "Set Options";
                    operationDetailsLabel.text = string.Concat("");
                    dateLabel.text = transactionResponse.CreatedAt;
                    break;
            }
        }
    }
}