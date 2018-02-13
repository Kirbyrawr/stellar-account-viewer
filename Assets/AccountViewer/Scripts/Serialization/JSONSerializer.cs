using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using AccountViewer.Controller.Accounts;


public class DataSerializer : MonoBehaviour 
{
	public static string currentJSON;

	public static void SerializeAccount(AccountsController.Account account) 
	{
		string json = JsonConvert.SerializeObject(account, Formatting.Indented);
	}

	public static void LoadJSON() 
	{
		string path = string.Concat(Application.persistentDataPath, "/Data/Data.lumen");
		string json = File.ReadAllText(path);
	}

	public static void SaveJSON(string json) 
	{
		string path = string.Concat(Application.persistentDataPath, "/Data/Data.lumen");
		File.WriteAllText(path, json);
	}
}
