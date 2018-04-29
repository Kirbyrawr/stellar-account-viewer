using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AccountViewer.Controller.Accounts;

public class UIAccountInput : UIInputModule
{
    public override string inputName
    {
        get
        {
            return "Account";
        }
    }

    public AccountsController.Account AccountToEdit
    {
        get
        {
            return accountToEdit;
        }

        set
        {
            accountToEdit = value;
			nameField.text = accountToEdit.name;
            addressField.text = accountToEdit.address;
        }
    }


	[Header("Account")]
    public InputField nameField;
    public InputField addressField;

	public Button okButton;

	private AccountsController.Account accountToEdit;

	public void AddAccount() 
	{
		Show(Mode.Add);

		//Button
		okButton.onClick.RemoveAllListeners();
		okButton.onClick.AddListener(OnClickAdd);
	}

	public void EditAccount(AccountsController.Account account) 
	{
		Show(Mode.Edit);

		//Set account to edit
		AccountToEdit = account;

		//Button
		okButton.onClick.RemoveAllListeners();
		okButton.onClick.AddListener(OnClickEdit);
	}

    protected override bool IsDataValid()
    {
        bool canBeAdded = true;

        //Check Name
        if (string.IsNullOrEmpty(nameField.text))
        {
            canBeAdded = false;
        }

        //Check Address
        if (string.IsNullOrEmpty(addressField.text))
        {
            canBeAdded = false;
        }

        else if (addressField.text.Length != 56)
        {
            canBeAdded = false;
        }

        else if (!addressField.text.StartsWith("G"))
        {
            canBeAdded = false;
        }

        return canBeAdded;
    }

    public override void OnClickAdd()
    {
		if (IsDataValid())
		{
			mainController.accounts.AddAccount(nameField.text, addressField.text);
			Reset();
			Hide();
		}
    }

    public override void OnClickEdit()
    {
        if (IsDataValid())
		{
			mainController.accounts.EditAccount(accountToEdit, nameField.text, addressField.text);
			Reset();
			Hide();
		}
    }

    protected override void Reset()
    {
		nameField.text = string.Empty;
		addressField.text = string.Empty;
    }
}
