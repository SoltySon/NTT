using lab3.Clothes;
using lab3.Core;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab3
{
    public partial class Form1 : Form
    {
        class Controller
        {
            public string name { get; set; }
            public Creator creator;
        }
        List<Type> newCreateType = new List<Type>();
        List<Type> newEquipmentType = new List<Type>();
        Table tb= new Table();

        private Dictionary<int, Controller> WearDictionary;
        private List<Wear> wear;
        public Wear currentWear;
	public Wear plugin;
        	public int CurrentIndex;
        public Form1()
        {
            InitializeComponent();

            WearDictionary = new Dictionary<int, Controller>();
            WearDictionary.Add(0, new Controller() { name = "Dress", creator = new DressCreator() });
            WearDictionary.Add(1, new Controller() { name = "Jacket", creator = new JacketCreator() });
            WearDictionary.Add(2, new Controller() { name = "Pants", creator = new PantsCreator() });
            WearDictionary.Add(3, new Controller() { name = "Shirt", creator = new ShirtCreator() });
            WearDictionary.Add(4, new Controller() { name = "Shorts", creator = new ShortsCreator() });
            WearDictionary.Add(5, new Controller() { name = "Tshirt", creator = new TshirtCreator() });

            Assembly asm = Assembly.LoadFile(@"C:\Users\Ilya\Desktop\lab4\ClassLibrary1\bin\Debug\ClassLibrary1.dll");
            foreach (Type t in asm.GetExportedTypes())
            {
                if (typeof(Wear).IsAssignableFrom(t))
                {
                    plugin = (Wear)asm.CreateInstance(t.FullName);
                }
            }
            //var num = 6;
            //Type refer;
            //Assembly asc = Assembly.LoadFile(@"C:\Users\Ilya\Desktop\lab4\ClassLibrary2\bin\Debug\ClassLibrary2.dll");
            /*foreach (Type t in asc.GetExportedTypes())
            {
                    plugin = (Wear)asc.CreateInstance(t.FullName);
                    WearDictionary.Add(num, new Controller() { name = "Shoes", creator = new t.FullName() });
                num += 1;
            }
            Type[] types = asc.GetTypes();
            foreach (Type typ in types)
            {
            if (typ.IsSubclassOf(typeof(Creator)))
                { refer = typ; }
            }
            WearDictionary.Add(num, new Controller() { name = "Shoes", creator = new (Creator)Activator.CreateInstance(refer()) });
            wear = new List<Wear>();*/

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb.ChangeIndex(this);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Creator currentCreator = WearDictionary[comboBox1.SelectedIndex].creator;
            wear.Add(currentCreator.Create(WearDictionary[comboBox1.SelectedIndex].name));
            refreshListView1();
        }

        private void refreshListView1()
        {
            listBox1.Items.Clear();
            foreach (var item in wear)
            {
                listBox1.Items.Add(item.name);
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            try {
            tb.But_change(currentWear, this);
            refreshListView1();
            }
            catch (NullReferenceException ex) { MessageBox.Show("No selected elements"); }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            foreach (var item in WearDictionary)
            {
                comboBox1.Items.Add(WearDictionary[item.Key].name);
            }
            comboBox1.SelectedIndex = 0;
        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            wear.Remove(currentWear);
            refreshListView1();
        }

        private void buttonSerialize_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = File.Create("wear.dat"))
            {
                formatter.Serialize(fs, wear);
            }

        }

        private void plugins_Click(object sender, EventArgs e)
        {
            getNewTypes();
            addCreators();
            addNewButtons();

            /*var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            dialog.CheckFileExists = true;
            dialog.Multiselect = true;
            var path = dialog.FileName;
            /*
            WearDictionary.Add(6, new Controller() { name = "Shoes", creator = new ShoesCreator() });
            WearDictionary.Add(7, new Controller() { name = "Sneakers", creator = new SneakersCreator() });
            WearDictionary.Add(8, new Controller() { name = "Socks", creator = new SocksCreator() });*/
        }

        private void addNewButtons()
        {
            for (int i = 0; i < newEquipmentType.Count; i++)
            {
                Button button = new Button();
                button.Name = "btn" + i.ToString();
                button.Text = newEquipmentType[i].Name;
                button.Width = buttonDeserialize.Width;
                button.Height = buttonDeserialize.Height;
                button.Left = buttonDeserialize.Left;
                button.Font = buttonDeserialize.Font;
                button.Top = (105 + button.Height) + i * 28;

                button.Click += new System.EventHandler(buttonAdd_Click);

                this.Controls.Add(button);

            }


        }

        private void addCreators()
        {
            var num = 6;
            for (int i = 0; i < newEquipmentType.Count; i++)
            {
                WearDictionary.Add(num, new Controller() { name = "Shoes", creator = new (Activator.CreateInstance(newCreateType[i])as Creator) }); /////////error
                WearDictionary[num] = (Controller)Activator.CreateInstance(newCreateType[i]);
                num++;
            }
        }

        private void getNewTypes()
        {
            Load pl = new Load();
            List<Type> l1 = new List<Type>();
            List<Type> l2 = new List<Type>();
            l1 = pl.LoadPlugin(true);
            l2 = pl.LoadPlugin(false);
            foreach (Type create in l1)
            {
                foreach (Type eq in l2)
                {
                    int start = eq.Name.Length;
                    int end = create.Name.Length;
                    if (String.Compare(eq.Name, create.Name.Substring((end - start), start)) == 0)
                    {
                        newCreateType.Add(create);
                        newEquipmentType.Add(eq);
                    }
                }
            }
        }

        private void buttonDeserialize_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = File.OpenRead("wear.dat"))
            {
                wear.Clear();
                wear = (List<Wear>)formatter.Deserialize(fs);
                refreshListView1();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int nameOfSelectItem = listBox1.SelectedIndex;
                currentWear = wear[nameOfSelectItem];
                tb.Index_change(currentWear, this);
            }
            catch (ArgumentOutOfRangeException ex) { MessageBox.Show("No selected elements"); }
        }
    }
}
