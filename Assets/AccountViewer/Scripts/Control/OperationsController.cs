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
        AccountMerge,
        AllowTrust,
        ChangeTrust,
        CreateAccount,
        CreatePassiveOffer,
        Inflation,
        ManageData,
        ManageOffer,
        PathPayment,
        Payment,
        SetOptions
    }

    public class OperationsController : MonoBehaviour
    {
        public System.Action<OperationResponse> OnAddOperation;
        private MainController mainController;

        //Operations
        private Dictionary<string, OperationResponse> operations = new Dictionary<string, OperationResponse>();

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
            GetOperations(mainController.accounts.currentAccount);
        }

        private async void GetOperations(AccountsController.Account account)
        {
            Page<OperationResponse> operationsPage = await mainController.server.Operations.ForAccount(account.data.KeyPair).Execute();

            var a = (PaymentOperationResponse)operationsPage.Records[0];

            for (int i = 0; i < 5; i++)
            {
                if (OnAddOperation != null)
                {
                    OnAddOperation(operationsPage.Records[i]);
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
            OperationType type = (OperationType)Enum.Parse(typeof(OperationType), operationResponse.Type);
            return type;
        }

        public Type GetOperationResponseType(OperationResponse operationResponse)
        {
            OperationType type = (OperationType)Enum.Parse(typeof(OperationType), operationResponse.Type);

            switch (type)
            {
                case OperationType.AccountMerge:
                    return typeof(AccountMergeOperationResponse);

                case OperationType.AllowTrust:
                    return typeof(AllowTrustOperationResponse);

                case OperationType.ChangeTrust:
                    return typeof(ChangeTrustOperationResponse);

                case OperationType.CreateAccount:
                    return typeof(CreateAccountOperationResponse);

                case OperationType.CreatePassiveOffer:
                    return typeof(CreatePassiveOfferOperationResponse);

                case OperationType.Inflation:
                    return typeof(InflationOperationResponse);

                case OperationType.ManageOffer:
                    return typeof(ManageOfferOperationResponse);

                case OperationType.ManageData:
                    return typeof(ManageDataOperationResponse);

                case OperationType.PathPayment:
                    return typeof(PathPaymentOperationResponse);

                case OperationType.Payment:
                    return typeof(PaymentOperationResponse);

                case OperationType.SetOptions:
                    return typeof(SetOptionsOperationResponse);
            }

            return null;
        }
    }
}