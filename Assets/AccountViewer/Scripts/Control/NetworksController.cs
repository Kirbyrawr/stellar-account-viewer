using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StellarSDK = stellar_dotnetcore_sdk;

public class NetworksController : MonoBehaviour {

	[System.Serializable]
	public class Network 
	{
		public string name;
		public string passphrase;
		public List<Server> servers = new List<Server>();

		public Network(string name, string passphrase) 
		{
			this.name = name;
			this.passphrase = passphrase;
		}
	}

	[System.Serializable]
	public class Server 
	{
		public string name;
		public string url;

		public Server(string name, string url) 
		{
			this.name = name;
			this.url = url;
		}
	}

	public StellarSDK.Server server;
	public List<Network> networks = new List<Network>();

	public void AddNetwork(Network network) 
	{
		networks.Add(network);
	}

	public void AddServer(Network network, Server server) 
	{
		network.servers.Add(server);
	}

	public void ChangeNetwork(Network network, Server server) 
	{

	}
}
