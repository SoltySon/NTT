using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;


namespace lab3.Core
{
    class Load
    {

        public List<Type> LoadPlugin(bool flag)
        {

            string[] plugins = Directory.GetFiles("Plugin", "*.dll");
            List<Type> listEquipment = new List<Type>();
            List<Type> listCreateEquipment = new List<Type>();

            foreach (var plugin in plugins)
            {
                Assembly asm = Assembly.LoadFrom(plugin);

                Type[] types = asm.GetTypes();

                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(Clothes.Wear)))
                    {
                        listEquipment.Add(type);
                    }
                    if (type.IsSubclassOf(typeof(Creator)))
                    {
                        listCreateEquipment.Add(type);
                    }
                }
            }
            if (flag)
                return listCreateEquipment;
            else
                return listEquipment;
        }


    }
}
