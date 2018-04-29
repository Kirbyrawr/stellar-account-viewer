using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk;
using Newtonsoft.Json;
using UStellar.Core;

namespace AccountViewer.Controller.Accounts
{
    public class AccountsController : MonoBehaviour
    {
        [System.Serializable]
        public class Account
        {
            public string name;
            public string address;

            [JsonIgnore]
            public stellar_dotnetcore_sdk.Account data;
        }

        public List<Account> accounts;
        public Account currentAccount;

        public System.Action<Account> OnAddAccount;
        public System.Action<Account> OnEditAccount;
        public System.Action<Account> OnSetAccount;
        public System.Action<AccountResponse> OnLoadAccountData;
        public System.Action<AccountResponse> OnUpdateAccountData;

        private MainController main;

        private void Start()
        {
            main = MainController.GetInstance();
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
            Account account = new Account();
            account.name = name;
            account.address = address;
            accounts.Add(account);

            //Serialize
            //DataSerializer.SerializeAccount(account);

            if (OnAddAccount != null)
            {
                OnAddAccount(account);
            }
        }
        
        public void EditAccount(Account account, string name, string address) 
        {
            account.name = name;
            account.address = address;

            //Serialize
            //DataSerializer.SerializeAccount(account);

            if (OnEditAccount != null)
            {
                OnEditAccount(account);
            }
        }

        public void SetAccount(Account account)
        {
            currentAccount = account;
            UpdateAccountData(account);

            if (OnSetAccount != null)
            {
                OnSetAccount(account);
            }
        }

        public async void LoadAccountData(Account accountSV)
        {
            KeyPair accountKeyPair = KeyPair.FromAccountId(accountSV.address);
            AccountResponse accountResponse = await main.networks.server.Accounts.Account(accountKeyPair);
            accountSV.data = new stellar_dotnetcore_sdk.Account(accountResponse.KeyPair, accountResponse.SequenceNumber);

            if (OnLoadAccountData != null)
            {
                OnLoadAccountData(accountResponse);
            }
        }

        public async void UpdateAccountData(Account account)
        {
            KeyPair accountKeyPair = KeyPair.FromAccountId(account.address);
            AccountResponse accountResponse = await UStellarManager.GetServer().Accounts.Account(accountKeyPair);
            account.data = new stellar_dotnetcore_sdk.Account(accountResponse.KeyPair, accountResponse.SequenceNumber);

            if (OnUpdateAccountData != null)
            {
                OnUpdateAccountData(accountResponse);
            }
        }
    }
}