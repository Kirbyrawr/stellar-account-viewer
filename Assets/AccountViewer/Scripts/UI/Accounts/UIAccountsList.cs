using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using AccountViewer.Controller;

namespace AccountViewer.UI.Accounts
{
    public class UIAccountsList : UIModule
    {
        public GameObject accountObjectPrefab;
        public RectTransform accountsListRect;
        public Transform scrollContent;

        private bool listOpened = false;
        private Dictionary<string, UIAccountObject> accountsObjects = new Dictionary<string, UIAccountObject>();

        public override void Start()
        {
            base.Start();
            uiController.mainController.accounts.OnAddAccount += OnAddAccount;
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
            accountsListRect.DOSizeDelta(new Vector2(accountsListRect.sizeDelta.x, -1190f), 0.4f).SetEase(Ease.InOutSine);

            //Transparency
            ShowTransparency();
        }

        public void CloseList()
        {
            listOpened = false;
            accountsListRect.DOSizeDelta(new Vector2(accountsListRect.sizeDelta.x, -1835f), 0.4f).SetEase(Ease.InOutSine);
            HideTransparency();
        }

        private void OnAddAccount(AccountsController.AccountSV account)
        {
            InstantiateAccountsObject(account);
        }

        private void InstantiateAccountsObject(AccountsController.AccountSV account) 
        {
            //Instantiate Prefab
            GameObject accountObjectInstance = Instantiate(accountObjectPrefab);
            accountObjectInstance.transform.SetParent(scrollContent, false);

            //Set Data
            UIAccountObject uiAccountObject = accountObjectInstance.GetComponent<UIAccountObject>();
            uiAccountObject.Setup(account);

            //Add this to the dictionary
            accountsObjects.Add(account.name, uiAccountObject);
        }

        public void OnClickAddAccount()
        {
            UIAddAccount addAccountModule = UIController.GetInstance().GetModule<UIAddAccount>();
            addAccountModule.Show();
        }
    }
}