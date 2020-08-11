using System.Collections;
using System.IO;
using UnityEngine;
using System.Xml;

public class LanguageController {

    private Hashtable _strings;

    public static string path = Path.Combine(Application.persistentDataPath, "languages.xml");

    public LanguageController(){}

    public LanguageController(string language)
    {
        SetLanguage(language);
    }

    public void SetLanguage(string language)
    {
        var xml = new XmlDocument();
        xml.Load(path);

        _strings = new Hashtable();
        var element = xml.DocumentElement?[language];
        if (element != null)
        {
            var elemEnum = element.GetEnumerator();
            while (elemEnum.MoveNext())
            {
                var xmlElement = (XmlElement) elemEnum.Current;
                if (xmlElement != null) _strings.Add(xmlElement.GetAttribute("name"), xmlElement.InnerText);
            }
        }
        else
        {
            Debug.Log("The specified language doesn't exist : " + language);
        }
    }

    public string GetString(string name)
    {
        if (_strings.ContainsKey(name)) return (string) _strings[name];
        
        Debug.LogError("The specified string does not exist: " + name);
        return "";
    }

}
