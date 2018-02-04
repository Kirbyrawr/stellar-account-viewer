using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using UStellar.Core;
using System.Threading.Tasks;

public class Main : MonoBehaviour {

	private static Main instance;
	private Server server;

	public List<string> accountsID;
	public string currentAccountID;
	public KeyPair currentAccountKeyPair;
	public Account currentAccount;

	public System.Action<AccountResponse> OnLoadAccount;
	
	[Header("Scripts")]
	public BalancePage uiBalance;

	private void Awake() 
	{
		SetInstance();
	}

	private void Start() 
	{
		UStellarManager.Init();
		server = UStellarManager.GetServer();
	}

	public static Main GetInstance() 
	{
		return instance;
	}

	public void SetInstance() 
	{
		if(instance == null) 
		{
			instance = this;
		}
		else 
		{
			Debug.LogWarning("Deleting duplicated instance");
			Destroy(this);
		}
	}
	
	public void AddAccount(string accountID) 
	{
		accountsID.Add(accountID);
	}

	public async void LoadAccount(string accountID) 
	{
		currentAccountKeyPair = KeyPair.FromAccountId(accountID);
		AccountResponse accountResponse = await server.Accounts.Account(currentAccountKeyPair);
    	currentAccount = new Account(accountResponse.KeyPair, accountResponse.SequenceNumber);
		Debug.Log(accountResponse);
		OnLoadAccount(accountResponse);
	}
}
