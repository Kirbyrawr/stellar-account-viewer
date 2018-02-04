using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountsPage : Page 
{
	public override string ID
    {
        get
        {
            return "ACCOUNTS";
        }
    }

	public InputField accountInput;

    public void OnAddAccount() 
	{
		controller.LoadAccount(accountInput.text);
		accountInput.text = string.Empty;
	}
}
