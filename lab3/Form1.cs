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

        Table tb= new Table();

        private Dictionary<int, Controller> WearDictionary;
        private List<Wear> wear;
        public Wear currentWear;

        public Form1()
        {
            InitializeComponent();

            Wear plugin;
        public int CurrentIndex;
            this.itemsList = new List<Item>();
            InitializeComponent();
            Assembly asm = Assembly.LoadFile(@"D:\Serialization\AdditionalClasses\bin\Debug\AdditionalClasses.dll");
            foreach (Type t in asm.GetExportedTypes())
            {
                if (typeof(Item).IsAssignableFrom(t))
                {
                    plugin = (Item)asm.CreateInstance(t.FullName);
                    mainFactory.FactoryDictionary.Add(plugin.GetType().Name, plugin);
                    Array.Resize(ref this.ItemsTypes, this.ItemsTypes.Length + 1);
                    this.ItemsTypes[this.ItemsTypes.Length - 1] = plugin.GetType();
                }
            }

            WearDictionary = new Dictionary<int, Controller>();
            WearDictionary.Add(0, new Controller() { name = "Dress", creator = new DressCreator() });
            WearDictionary.Add(1, new Controller() { name = "Jacket", creator = new JacketCreator() });
            WearDictionary.Add(2, new Controller() { name = "Pants", creator = new PantsCreator() });
            WearDictionary.Add(3, new Controller() { name = "Shirt", creator = new ShirtCreator() });
            WearDictionary.Add(4, new Controller() { name = "Shorts", creator = new ShortsCreator() });
            WearDictionary.Add(5, new Controller() { name = "Tshirt", creator = new TshirtCreator() });
            wear = new List<Wear>();

        }

        //

        //

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
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            dialog.CheckFileExists = true;
            dialog.Multiselect = true;
            var path = dialog.FileName;
            /*
            WearDictionary.Add(6, new Controller() { name = "Shoes", creator = new ShoesCreator() });
            WearDictionary.Add(7, new Controller() { name = "Sneakers", creator = new SneakersCreator() });
            WearDictionary.Add(8, new Controller() { name = "Socks", creator = new SocksCreator() });*/
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
