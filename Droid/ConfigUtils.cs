using System;
using System.Collections.Generic;

namespace connectivity.Droid
{
    public enum SettingType
    {
        Bool,
        Float,
        Int,
        Long,
        String,
        StringSet
    }

    public static class ConfigUtils
    {
        public static void SaveSetting<T>(string name, T value, SettingType type)
        {
            var editor = TechApp2.Singleton.prefs.Edit();
            editor.Remove(name);
            switch ((int)type)
            {
                case 0:
                    editor.PutBoolean(name, (bool)(object)value);
                    break;
                case 1:
                    editor.PutFloat(name, (float)(object)value);
                    break;
                case 2:
                    editor.PutInt(name, (int)(object)value);
                    break;
                case 3:
                    editor.PutLong(name, (long)(object)value);
                    break;
                case 4:
                    editor.PutString(name, (string)(object)value);
                    break;
            }
            editor.Commit();
        }

        public static void SaveSetting(string name, List<string>values)
        {
            var editor = TechApp2.Singleton.prefs.Edit();
            editor.Remove(name);
            editor.PutStringSet(name, values);
            editor.Commit();
        }

        public static T LoadSetting<T>(string name, SettingType type)
        {
            var prefs = TechApp2.Singleton.prefs;

            var nv = new object();
            switch ((int)type)
            {
                case 0:
                    nv = prefs.GetBoolean(name, false);
                    break;
                case 1:
                    nv = prefs.GetFloat(name, 0);
                    break;
                case 2:
                    nv = prefs.GetInt(name, 0);
                    break;
                case 3:
                    nv = prefs.GetLong(name, 0);
                    break;
                case 4:
                    nv = prefs.GetString(name, "");
                    break;
            }
            return (T)nv;
        }

        public static List<string>LoadSetting(string name)
        {
            var strList = TechApp2.Singleton.prefs.GetStringSet(name, null);
            var list = new List<string>();
            if (strList == null)
                return list;
            if (strList.Count == 0)
                return list;
            foreach (var i in strList)
                list.Add(i);

            return list;
        }
    }
}

