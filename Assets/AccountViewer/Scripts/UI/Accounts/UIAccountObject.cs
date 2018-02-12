using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AccountViewer.Controller.Accounts;

namespace AccountViewer.UI.Accounts
{
    public class UIAccountObject : MonoBehaviour
    {
        public Text nameLabel;
        public Text addressLabel;

        [System.NonSerialized]
        public AccountsController.AccountSV account;

        private UIController uiController;
        private UIAccountsList uiAccountsList;

        public void Setup(AccountsController.AccountSV account)
        {
            uiController = UIController.GetInstance();
            uiAccountsList = uiController.GetModule<UIAccountsList>();

            this.account = account;
            nameLabel.text = account.name;
            addressLabel.text = account.address;
        }

        public void OnClick()
        {
            uiController.mainController.accounts.SetAccount(account);
            uiAccountsList.CloseList();
        }
    }
}
