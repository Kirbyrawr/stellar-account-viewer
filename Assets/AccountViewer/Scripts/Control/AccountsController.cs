using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk;

namespace AccountViewer.Controller
{
    public class AccountsController : MonoBehaviour
    {
        [System.Serializable]
        public class AccountSV
        {
            public string name;
            public string address;
            public Account data;
        }

        public List<AccountSV> accounts;
        public AccountSV currentAccount;

        private MainController controller;

        //Callbacks
        public System.Action<AccountSV> OnAddAccount;
        public System.Action<AccountResponse> OnLoadAccountData;
        public System.Action<AccountResponse> OnUpdateAccountData;

        private void Start()
        {
            controller = MainController.GetInstance();
            LoadAccountsData();
        }

        private void LoadAccountsData()
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                LoadAccountData(accounts[i]);
            }
        }

        public void AddAccount(string name, string address)
        {
            AccountSV accountToAdd = new AccountSV();
            accountToAdd.name = name;
            accountToAdd.address = address;
            accounts.Add(accountToAdd);
            OnAddAccount(accountToAdd);
        }

        public void SetAccount(AccountSV account)
        {
            currentAccount = account;
        }
        
        public async void LoadAccountData(AccountSV accountSV)
        {
            KeyPair accountKeyPair = KeyPair.FromAccountId(accountSV.address);
            AccountResponse accountResponse = await controller.server.Accounts.Account(accountKeyPair);
            accountSV.data = new Account(accountResponse.KeyPair, accountResponse.SequenceNumber);

            if (OnLoadAccountData != null)
            {
                OnLoadAccountData(accountResponse);
            }
        }

        public async void UpdateAccountData(AccountSV accountSV)
        {
            KeyPair accountKeyPair = KeyPair.FromAccountId(accountSV.address);
            AccountResponse accountResponse = await controller.server.Accounts.Account(accountKeyPair);
            accountSV.data = new Account(accountResponse.KeyPair, accountResponse.SequenceNumber);

            if (OnUpdateAccountData != null)
            {
                OnUpdateAccountData(accountResponse);
            }
        }
    }
}