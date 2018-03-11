using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using AccountViewer.Controller.Accounts;
using stellar_dotnetcore_sdk.requests;
using stellar_dotnetcore_sdk.responses.page;

namespace AccountViewer.Controller.Transactions
{
    public class TransactionsController : MonoBehaviour
    {
        public System.Action<TransactionResponse> OnAddTransaction;
        private MainController main;

        //Operations
        private Dictionary<string, TransactionResponse> transactions = new Dictionary<string, TransactionResponse>();

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
            main.accounts.OnUpdateAccountData += OnUpdateAccountData;
        }

        private void OnSetAccount(AccountsController.Account account) 
        {
            transactions.Clear();
        }

        private void OnUpdateAccountData(AccountResponse response)
        {
            GetTransactions(main.accounts.currentAccount);
        }

        private async void GetTransactions(AccountsController.Account account)
        {
            Page<TransactionResponse> transactionsPage = await main.networks.server.Transactions.ForAccount(account.data.KeyPair).Order(OrderDirection.DESC).Execute();

            for (int i = 0; i < 5; i++)
            {
                if (OnAddTransaction != null)
                {
                    OnAddTransaction(transactionsPage.Records[i]);
                }
            }
        }
    }
}