using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using AccountViewer.Controller.Accounts;

namespace AccountViewer.UI.Accounts
{
    public class UIAccountsBar : UIModule
    {
        public Text headerLabel;
        public GameObject accountPrefab;
        public RectTransform accountsListRect;
        public Transform contentParent;

        private bool listOpened = false;
        private Dictionary<string, UIAccount> accountsObjects = new Dictionary<string, UIAccount>();

        protected override void Setup()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            uiController.mainController.accounts.OnAddAccount += OnAddAccount;
            uiController.mainController.accounts.OnEditAccount += OnEditAccount;
            uiController.mainController.accounts.OnSetAccount += OnSetAccount;
        }

        public void OnClickAccountList()
        {
            if (!listOpened)
            {
                OpenList();
            }
            else
            {
                CloseList();
            }
        }

        public void OpenList()
        {
            listOpened = true;
            accountsListRect.DOSizeDelta(new Vector2(accountsListRect.sizeDelta.x, 766f), 0.4f).SetEase(Ease.InOutSine);
            ShowTransparency();
        }

        public void CloseList()
        {
            listOpened = false;
            accountsListRect.DOSizeDelta(new Vector2(accountsListRect.sizeDelta.x, 84f), 0.4f).SetEase(Ease.InOutSine);
            HideTransparency();
        }

        //Callbacks
        private void OnAddAccount(AccountsController.Account account)
        {
            Debug.Log("Click");
            CreateAccount(account);
        }

        private void OnEditAccount(AccountsController.Account account)
        {
            CreateAccount(account);
        }

        private void OnSetAccount(AccountsController.Account account)
        {
            headerLabel.text = account.name;
        }

        private void CreateAccount(AccountsController.Account account)
        {
            //Instantiate Prefab
            GameObject accountInstance = Instantiate(accountPrefab);
            accountInstance.transform.SetParent(contentParent, false);

            //Set Data
            UIAccount uiAccount = accountInstance.GetComponent<UIAccount>();
            uiAccount.Setup(account);

            //Add this to the dictionary
            accountsObjects.Add(account.name, uiAccount);
        }

        private void EditAccount(AccountsController.Account account)
        {     
            UIAccount uiAccount = accountsObjects[account.name];
            uiAccount.Refresh();
        }

        //Called from Scene
        public void OnClickAddAccount()
        {
            UIAccountInput accountInput = UIController.GetInstance().GetModule<UIAccountInput>();
            accountInput.AddAccount();
        }
    }
}