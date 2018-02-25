using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UStellarUtils
{
    public static string ShortAddress(string address)
    {
        string addressShortened = addressShortened = string.Concat(address.Substring(0, 5), "...", address.Substring(address.Length - 5, 5));
        return addressShortened;
    }
}
