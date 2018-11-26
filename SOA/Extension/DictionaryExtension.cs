using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SOA.Extension
{
    public static class DictionaryExtension
    {
        public static bool TryAdd(this Dictionary<Keys, Action> dictionary, Keys key, Action value)
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
