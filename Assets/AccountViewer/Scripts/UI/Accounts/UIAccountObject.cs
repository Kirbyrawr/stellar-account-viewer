using System.Collections;
using System.Collections.Generic;
using AccountViewer.Controller;
using UnityEngine;
using UnityEngine.UI;

public class UIAccountObject : MonoBehaviour
{
    public Text nameLabel;
    public Text addressLabel;

    [System.NonSerialized]
    public AccountsController.AccountSV account;

    public void Setup(AccountsController.AccountSV account)
    {
        this.account = account;
        nameLabel.text = account.name;
        addressLabel.text = account.address;
    }
}
