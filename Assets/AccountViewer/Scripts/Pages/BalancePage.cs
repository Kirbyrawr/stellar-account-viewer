using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;

public class BalancePage : Page
{
	public override string ID
    {
        get
        {
            return "BALANCE";
        }
    }

	public Transform assetsParent;
	public GameObject assetPrefab;
	public System.Action<string, Balance> OnUpdateBalance;

	private Dictionary <string, Balance> balanceDictionary = new Dictionary<string, Balance>();
	private Dictionary <string, UIAsset> uiAssets = new Dictionary<string, UIAsset>();

	public override void Start() 
	{
		base.Start();
		SubscribeEvents();
	}

	private void SubscribeEvents() 
	{
		controller.OnLoadAccount += OnLoadAccount;
	}

	private void OnLoadAccount(AccountResponse accountResponse) 
	{
		Balance[] balances = accountResponse.Balances;

		for (int i = 0; i < balances.Length; i++)
		{
			Balance balance = balances[i];
			
			string assetID = null;

			if(balance.AssetType == "native") 
			{
				assetID = "native";
			}
			
			else 
			{
				assetID = balance.AssetCode + balance.AssetIssuer.AccountId;
			}			

			//If we have the asset already added.
			if(balanceDictionary.ContainsKey(assetID)) 
			{
				balanceDictionary[balance.AssetCode] = balance;
				OnUpdateBalance(assetID, balance);
			}

			//If we don't have the asset already added.
			else
			{	
				balanceDictionary.Add(assetID, balance);
				InstantiateAssetObject(assetID, balance);
			}
		}
	}

	private void InstantiateAssetObject(string assetID, Balance balance) 
	{
		//Instantiate Prefab
		GameObject assetInstance = Instantiate(assetPrefab);
		assetInstance.transform.SetParent(assetsParent, false);

		//Set Data
		UIAsset uiAsset = assetInstance.GetComponent<UIAsset>();
		uiAsset.Setup(assetID, balance);

		//Add this to the UIAsset dictionary
		uiAssets.Add(assetID, uiAsset);
	}
}
