using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using UnityEngine.UI;

public class UIAsset : MonoBehaviour 
{
	[ReadOnly]
	public string id;

    public Text label;

	//Balance
	private Balance balance;
	public Balance Balance
    {
        get
        {
            return balance;
        }

        set
        {
            balance = value;
			UpdateLabel();
        }
    }

	private BalancePage controller;

    public void Setup(string assetID, Balance assetBalance) 
	{
		//Set UIBalance instance;
		controller = Main.GetInstance().uiBalance;

		//Set ID
		id = assetID;

		//Set Balance
		Balance = assetBalance;

		//Subscribe to Balance updates
		controller.OnUpdateBalance += OnUpdateBalance;
	}

	public void OnUpdateBalance(string assetID, Balance assetBalance) 
	{
		//Check if this asset changed;
		if(id != assetID) { return; }
		Balance = assetBalance;
	}

	private void UpdateLabel() 
	{
		//Set Balance Label
		label.text = string.Concat(Balance.BalanceString, Balance.AssetCode);
	}
}
