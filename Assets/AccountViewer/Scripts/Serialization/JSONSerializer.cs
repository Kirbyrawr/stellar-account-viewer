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
			json.Add("", accounts);
		}

		//Add Account
		JObject accountToAdd = new JObject();
		accountToAdd.Add("name", new JProperty("name", account.name));
		accountToAdd.Add("address", new JProperty("address", account.address));
		accounts.Add(accountToAdd);
		
		//Serialize JSON
		currentJSON = JsonConvert.SerializeObject(json, Formatting.Indented);

		Debug.Log(currentJSON);

		//Save JSON
		SaveJSON();
    }

    /*
	{
		"accounts": 
		[
			{
				"name": "Account1",
				"address": "1"
			},

			{
				"name": "Account2",
				"address": "2"
			}
		]
	} */

	

    public static void LoadJSON()
    {
        string path = string.Concat(Application.persistentDataPath, "/Data/Data.lumen");
        string json = File.ReadAllText(path);
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
