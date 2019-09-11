using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Xml;

public class LanguageController {

    private Hashtable strings;

    public static string path = Path.Combine(Application.persistentDataPath, "languages.xml");

    public LanguageController(string language)
    {
        SetLanguage(language);
    }

    public void SetLanguage(string language)
    {
        var xml = new XmlDocument();
        xml.Load(path);

        strings = new Hashtable();
        var element = xml.DocumentElement?[language];
        if (element != null)
        {
            var elemEnum = element.GetEnumerator();
            while (elemEnum.MoveNext())
            {
                var xmlElement = (XmlElement) elemEnum.Current;
                if (xmlElement != null) strings.Add(xmlElement.GetAttribute("name"), xmlElement.InnerText);
            }
        }
        else
        {
            Debug.Log("The specified language doesn't exist : " + language);
        }
    }

    public string GetString(string name)
    {
        if (strings.ContainsKey(name)) return (string) strings[name];
        
        Debug.LogError("The specified string does not exist: " + name);
        return "";
    }

}
