using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Xml;

public class LanguageController {

    private Hashtable Strings;

    public static string path = Path.Combine(Application.persistentDataPath, "languages.xml");

    public LanguageController(string language)
    {
        SetLanguage(language);
    }

    public void SetLanguage(string language)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(path);

        Strings = new Hashtable();
        XmlElement element = xml.DocumentElement[language];
        if (element != null)
        {
            IEnumerator elemEnum = element.GetEnumerator();
            while (elemEnum.MoveNext())
            {
                XmlElement xmlElement = (XmlElement) elemEnum.Current;
                Strings.Add(xmlElement.GetAttribute("name"), xmlElement.InnerText);
            }
        }
        else
        {
            Debug.Log("The specified language doesn't exist : " + language);
        }
    }

    public string GetString(string name)
    {
        if (!Strings.ContainsKey(name))
        {
            Debug.LogError("The specified string does not exist: " + name);
            return "";
        }
        return (string) Strings[name];
    }

}
