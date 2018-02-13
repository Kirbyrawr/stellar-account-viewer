using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AccountViewer.Controller.Accounts;

namespace AccountViewer.UI.Accounts
{
    public class UIAccount : MonoBehaviour
    {
        public Text nameLabel;
        public Text addressLabel;

        [System.NonSerialized]
        public AccountsController.Account account;

        private UIController controller;
        private UIAccountsBar accountsList;

        public void Setup(AccountsController.Account account)
        {
            controller = UIController.GetInstance();
            accountsList = controller.GetModule<UIAccountsBar>();

            this.account = account;
            nameLabel.text = account.name;
            addressLabel.text = account.address;
        }

        //Called from Scene
        public void OnClick()
        {
            controller.mainController.accounts.SetAccount(account);
            accountsList.CloseList();
        }
    }
}
