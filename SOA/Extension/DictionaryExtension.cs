using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SOA.Extension
{
    public static class DictionaryExtension
    {
        public static bool TryAdd<T>(this Dictionary<Keys, T> dictionary, Keys key, T value)
        {
            if (dictionary.ContainsKey(key) == false)
            {
                dictionary.Add(key, value);

                return true;
            }

            return false;
        }

        public static void TryInvoke(this Dictionary<Keys, Action> dictionary, Keys key)
        {
            Action handler = null;

            if(dictionary.TryGetValue(key, out handler) == true)
            {
                handler();
            }
        }
    }
}
