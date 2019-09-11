using System;
using System.Collections.Generic;

namespace Objects
{
    [Serializable]
    public class PlayerData {

        public List<Category> categories;
        public string path;
        public bool volume;
        public bool tutorial;
        public string language;
        public bool firstTime;

        public PlayerData()
        {

        }

        public PlayerData(List<Category> categories,string path, bool volume, bool tutorial, string language, bool firstTime)
        {
            this.categories = categories;
            this.path = path;
            this.volume = volume;
            this.tutorial = tutorial;
            this.language = language;
            this.firstTime = firstTime;
        }

        public PlayerData SetPath(string path){
            this.path = path;
            return this;
        }
        public string GetPath(){
            return path;
        }

        public PlayerData SetCategories(List<Category> categories){
            this.categories = categories;
            return this;
        }
        public List<Category> GetCategories(){
            return categories;
        }

        public Category GetCategoryWithName(String name)
        {
            foreach(var category in categories)
            {
                if (category.GetName() == name)
                {
                    return category;
                }
            }
            return null;
        }

        public Category GetCategoryWithId(int id)
        {
            foreach(var category in categories)
            {
                if (category.id == id)
                {
                    return category;
                }
            }
            return null;
        }

        public PlayerData SetVolume(bool volume){
            this.volume = volume;
            return this;
        }
        public bool GetVolume(){
            return volume;
        }

        public PlayerData SetFirstTime(bool firstTime)
        {
            this.firstTime = firstTime;
            return this;
        }
        public bool GetFirstTime()
        {
            return firstTime;
        }

        public PlayerData SetLanguage(string language)
        {
            this.language = language;
            return this;
        }
        public string GetLanguage()
        {
            return language;
        }

        public PlayerData SetTutorial(bool tutorial)
        {
            this.tutorial = tutorial;
            return this;
        }
        public bool GetTutorial()
        {
            return tutorial;
        }

        public String ToString()
        {
            var sb = "PlayerData : "
                        + "\n volume : "
                        + volume.ToString()
                        + "\n path : "
                        + path;
            foreach(var category in categories)
            {
                sb += "\n" + category.ToString();
            }
            return sb;
        }
    }
}
