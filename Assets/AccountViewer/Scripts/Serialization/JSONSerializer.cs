using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AccountViewer.Controller.Accounts;


public class JSONSerializer : MonoBehaviour
{
    public static string currentJSON;

	public static void Init() 
	{
		LoadJSON();
		SetupJSON();
		SetupDataDirectory();
	}

	public static void SetupJSON() 
	{
		if(string.IsNullOrEmpty(currentJSON)) 
		{
			JObject json = new JObject();
			currentJSON = JsonConvert.SerializeObject(json, Formatting.Indented);
		}
	}

	public static void SetupDataDirectory() 
	{
		if(!Directory.Exists(GetDataDirectory())) 
		{
			Directory.CreateDirectory(GetDataDirectory());
		}
	}

    public static void SerializeAccount(AccountsController.Account account)
    {
		//Deserialize JSON
        JObject json = JObject.Parse(currentJSON);
		JArray accounts = null;

		//Check if the array exist
		if(json["accounts"] != null) 
		{
        	accounts = (JArray)json.GetValue("accounts");
		}
		else
		{
        	accounts = new JArray();
			json.Add("accounts", accounts);
		}

		//Add Account
		JObject accountToAdd = JObject.FromObject(account);
		accounts.Add(accountToAdd);
		
		//Serialize JSON
		currentJSON = JsonConvert.SerializeObject(json, Formatting.Indented);

		//Save JSON
		SaveJSON();
    }

	public static List<AccountsController.Account> DeserializeAccounts() 
	{
		List<AccountsController.Account> accountsList = new List<AccountsController.Account>();

		//Deserialize JSON
        JObject json = JObject.Parse(currentJSON);

		//Check if the array exist
		if(json["accounts"] != null) 
		{
        	JArray accounts = (JArray)json.GetValue("accounts");

			for (int i = 0; i < accounts.Count; i++)
			{
				AccountsController.Account account = new AccountsController.Account();
				account.name = accounts[i].SelectToken("name").ToString();
				account.address = accounts[i].SelectToken("address").ToString();
				accountsList.Add(account);
			}
		}

		return accountsList;
	}


    public static void LoadJSON()
    {
        currentJSON = File.ReadAllText(GetJSONPath());
    }

    public static void SaveJSON()
    {
        File.WriteAllText(GetJSONPath(), currentJSON);
    }

	public static string GetJSONPath() 
	{
		return string.Concat(Application.persistentDataPath, "/Data/Data.json");
	}

	public static string GetDataDirectory() 
	{
		return string.Concat(Application.persistentDataPath, "/Data/");
	}
}
