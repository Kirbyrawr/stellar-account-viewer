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
        public System.Action<List<TransactionResponse>> OnLoadTransactions;
        private MainController main;
        private bool m_loadingTransactions;

        private string m_firstPagingToken;
        private string m_lastPagingToken;

        //Operations
        private SortedDictionary<string, TransactionResponse> loadedTransactions = new SortedDictionary <string, TransactionResponse>();

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
            loadedTransactions.Clear();
        }

        private void OnUpdateAccountData(AccountResponse response)
        {
            LoadTransactions(main.accounts.currentAccount, 10);
        }

        public async void LoadTransactions(AccountsController.Account account, int count)
        {
            if(!m_loadingTransactions) 
            {
                m_loadingTransactions = true;
        
                //Load transactions page from the paging token if available
                Page<TransactionResponse> transactionsPage = await main.networks.server.Transactions.
                                                                    ForAccount(account.data.KeyPair).
                                                                    Limit(count).Cursor(m_lastPagingToken).Order(OrderDirection.DESC).
                                                                    Execute();

                List<TransactionResponse> transactionsOrdered = SortDescending(transactionsPage.Records);

                //Save First Transaction Paging Token
                if(loadedTransactions.Count == 0) 
                {
                    m_firstPagingToken = transactionsOrdered[0].PagingToken;
                }

                //Add to dictionary     
                for (int i = 0; i < transactionsOrdered.Count; i++)
                {
                    loadedTransactions.Add(transactionsOrdered[i].Hash, transactionsOrdered[i]);
                }

                //Save Last Transaction Paging Token
                if(transactionsOrdered.Count != 0) 
                {
                    TransactionResponse lastTransactionResponse = transactionsOrdered[transactionsOrdered.Count - 1];

                    if(lastTransactionResponse != null) 
                    {
                        m_lastPagingToken = lastTransactionResponse.PagingToken;
                    }
                    else
                    {
                        m_lastPagingToken = null;
                    }
                }

                //Call Event
                if (OnLoadTransactions != null)
                {
                    OnLoadTransactions(transactionsOrdered);
                }

                m_loadingTransactions = false;
            }
        }

        public TransactionResponse GetLoadedTransaction(string hash) 
        {
            TransactionResponse transaction = null;
            loadedTransactions.TryGetValue(hash, out transaction);
            return transaction;
        }

        public bool IsLoadingTransactions() 
        {
            return m_loadingTransactions;
        }

        public List<TransactionResponse> SortDescending(List<TransactionResponse> list)
        {  
            list.Sort((a, b) => UStellarUtils.FormatDate(b.CreatedAt).CompareTo(UStellarUtils.FormatDate(b.CreatedAt)));
            return list;
        }
    }
}

