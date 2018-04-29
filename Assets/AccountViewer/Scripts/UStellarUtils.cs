using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.requests;
using System;
using System.Xml;

public static class UStellarUtils
{   
    //Stellar Network
    public const string STELLAR_PUBLIC_NETWORK_PASSPHRASE = "Public Global Stellar Network ; September 2015";
    public const string STELLAR_PUBLIC_SERVER_URL = "https://horizon.stellar.org";
    public const string STELLAR_TEST_NETWORK_PASSPHRASE = "Test SDF Network ; September 2015";
    public const string STELLAR_TEST_SERVER_URL = "https://horizon-testnet.stellar.org";


    public static string ShortAddress(string address)
    {
        string addressShortened = addressShortened = string.Concat(address.Substring(0, 5), "...", address.Substring(address.Length - 5, 5));
        return addressShortened;
    }

    public static DateTime FormatDate(string date) 
    {
        DateTime formattedDate = DateTime.Parse(date, null).ToUniversalTime();
        return formattedDate;
    }
}
