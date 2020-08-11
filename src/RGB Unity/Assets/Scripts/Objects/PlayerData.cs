using System;
using System.Collections.Generic;
using System.Linq;

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

        public Category GetCategoryWithName(string name)
        {
            return categories.FirstOrDefault(category => category.name == name);
        }

        public Category GetCategoryWithId(int id)
        {
            return categories.FirstOrDefault(category => category.id == id);
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

        public override string ToString()
        {
            var sb = $"PlayerData : \n volume : {volume}\n path :{path}";
            return categories.Aggregate(sb, (current, category) => current + ($"\n{category}"));
        }
    }
}
