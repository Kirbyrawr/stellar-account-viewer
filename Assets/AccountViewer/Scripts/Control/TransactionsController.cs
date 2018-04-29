using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using AccountViewer.Controller.Accounts;
using stellar_dotnetcore_sdk.requests;
using stellar_dotnetcore_sdk.responses.page;
using System.Linq;

namespace AccountViewer.Controller.Transactions
{
    public class TransactionsController : MonoBehaviour
    {
        public System.Action<List<TransactionResponse>> OnAddTransaction;
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
            GetTransactions(main.accounts.currentAccount, 10);
        }

        public async void GetTransactions(AccountsController.Account account, int count)
        {
            Page<TransactionResponse> transactionsPage = await main.networks.server.Transactions.ForAccount(account.data.KeyPair).Limit(count).Order(OrderDirection.DESC).Execute();
            List<TransactionResponse> transactionsOrdered = SortDescending(transactionsPage.Records);

            for (int i = 0; i < transactionsOrdered.Count; i++)
            {
                //Add to dictionary     
                transactions.Add(transactionsOrdered[i].Hash, transactionsOrdered[i]);
            }

            if (OnAddTransaction != null)
            {
                OnAddTransaction(transactionsOrdered);
            }
        }

        public List<TransactionResponse> SortDescending(List<TransactionResponse> list)
        {  
            list.Sort((a, b) => UStellarUtils.FormatDate(b.CreatedAt).CompareTo(UStellarUtils.FormatDate(b.CreatedAt)));
            return list;
        }
    }
}

