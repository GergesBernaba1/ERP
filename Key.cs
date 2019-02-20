using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;

/// <summary>
/// Summary description for Key
/// </summary>
public static class Key
{
    public static string _KeyValuePair(string key, string value)
    {
        return "{\"" + key + "\":\"" + value + "\"}";
    }
}