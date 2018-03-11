using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using AccountViewer.Controller.Accounts;
using stellar_dotnetcore_sdk.requests;
using stellar_dotnetcore_sdk.responses.operations;
using stellar_dotnetcore_sdk.responses.page;

namespace AccountViewer.Controller.Operations
{
    public enum OperationType
    {
        CREATE_ACCOUNT = 0,
        PAYMENT = 1,
        PATH_PAYMENT = 2,
        MANAGE_OFFER = 3,
        CREATE_PASSIVE_OFFER = 4,
        SET_OPTIONS = 5,
        CHANGE_TRUST = 6,
        ALLOW_TRUST = 7,
        ACCOUNT_MERGE = 8,
        INFLATION = 9,
        MANAGE_DATA = 10
    }

    public class OperationsController : MonoBehaviour
    {
        public System.Action<TransactionResponse, OperationResponse> OnAddOperation;

        private MainController main;
        private Dictionary<string, OperationResponse> operations = new Dictionary<string, OperationResponse>();

        public void Start()
        {
            Setup();
        }

        private void Setup()
        {
            main = MainController.GetInstance();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            main.accounts.OnSetAccount += OnSetAccount;
            main.transactions.OnAddTransaction += OnAddTransaction;
        }

        private void OnSetAccount(AccountsController.Account account)
        {
            operations.Clear();
        } 

        private void OnAddTransaction(TransactionResponse response)
        {
            GetOperations(response);
        }

        private async void GetOperations(TransactionResponse transactionResponse)
        {
            Page<OperationResponse> operationsPage = await main.networks.server.Operations.ForTransaction(transactionResponse.Hash).Order(OrderDirection.ASC).Execute();

            for (int i = 0; i < operationsPage.Records.Count; i++)
            {
                if (OnAddOperation != null)
                {
                    OnAddOperation(transactionResponse, operationsPage.Records[i]);
                }
            }
        }

        public T GetOperations<T>(int page) where T : OperationResponse
        {
            foreach (var pair in operations)
            {
                Type type = GetOperationResponseType(pair.Value);
                if (type == typeof(T))
                {
                    return (T)pair.Value;
                }
            }

            return null;
        }

        public OperationType GetOperationResponseOperationType(OperationResponse operationResponse)
        {
            OperationType type = (OperationType)Enum.Parse(typeof(OperationType), operationResponse.Type, true);
            return type;
        }

        public Type GetOperationResponseType(OperationResponse operationResponse)
        {
            OperationType type = (OperationType)Enum.Parse(typeof(OperationType), operationResponse.Type, true);

            switch (type)
            {
                case OperationType.ACCOUNT_MERGE:
                    return typeof(AccountMergeOperationResponse);

                case OperationType.ALLOW_TRUST:
                    return typeof(AllowTrustOperationResponse);

                case OperationType.CHANGE_TRUST:
                    return typeof(ChangeTrustOperationResponse);

                case OperationType.CREATE_ACCOUNT:
                    return typeof(CreateAccountOperationResponse);

                case OperationType.CREATE_PASSIVE_OFFER:
                    return typeof(CreatePassiveOfferOperationResponse);

                case OperationType.INFLATION:
                    return typeof(InflationOperationResponse);

                case OperationType.MANAGE_OFFER:
                    return typeof(ManageOfferOperationResponse);

                case OperationType.MANAGE_DATA:
                    return typeof(ManageDataOperationResponse);

                case OperationType.PATH_PAYMENT:
                    return typeof(PathPaymentOperationResponse);

                case OperationType.PAYMENT:
                    return typeof(PaymentOperationResponse);

                case OperationType.SET_OPTIONS:
                    return typeof(SetOptionsOperationResponse);
            }

            return null;
        }
    }
}