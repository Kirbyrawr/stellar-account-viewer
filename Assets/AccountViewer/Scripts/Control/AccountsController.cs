using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk;
using Newtonsoft.Json;

namespace AccountViewer.Controller.Accounts
{
    public class AccountsController : MonoBehaviour
    {
        [System.Serializable]
        public class AccountSV
        {
            public string name;
            public string address;

            [JsonIgnore]
            public Account data;
        }

        public List<AccountSV> accounts;
        public AccountSV currentAccount;

        //Callbacks
        public System.Action<AccountSV> OnAddAccount;
        public System.Action<AccountSV> OnSetAccount;
        public System.Action<AccountResponse> OnLoadAccountData;
        public System.Action<AccountResponse> OnUpdateAccountData;

        private MainController mainController;

        private void Start()
        {
            mainController = MainController.GetInstance();
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
            AccountSV account = new AccountSV();
            account.name = name;
            account.address = address;
            accounts.Add(account);

            //Serialize
            DataSerializer.SerializeAccount(account);

            if (OnAddAccount != null)
            {
                OnAddAccount(account);
            }
        }

        public void SetAccount(AccountSV account)
        {
            currentAccount = account;
            LoadAccountData(account);

            if (OnSetAccount != null)
            {
                OnSetAccount(account);
            }
        }

        public async void LoadAccountData(AccountSV accountSV)
        {
            KeyPair accountKeyPair = KeyPair.FromAccountId(accountSV.address);
            AccountResponse accountResponse = await mainController.server.Accounts.Account(accountKeyPair);
            accountSV.data = new Account(accountResponse.KeyPair, accountResponse.SequenceNumber);

            if (OnLoadAccountData != null)
            {
                OnLoadAccountData(accountResponse);
            }
        }

        public async void UpdateAccountData(AccountSV accountSV)
        {
            KeyPair accountKeyPair = KeyPair.FromAccountId(accountSV.address);
            AccountResponse accountResponse = await mainController.server.Accounts.Account(accountKeyPair);
            accountSV.data = new Account(accountResponse.KeyPair, accountResponse.SequenceNumber);

            if (OnUpdateAccountData != null)
            {
                OnUpdateAccountData(accountResponse);
            }
        }
    }
}