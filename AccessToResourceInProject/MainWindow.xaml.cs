using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccessToResourceInProject
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();

            string fileContent = "";
            using (var stream = assembly.GetManifestResourceStream("AccessToResourceInProject.g.resources"))
            {
                if (stream != null)
                {
                    using (var rr = new ResourceReader(stream))
                    {
                        if (rr != null)
                        {
                            var resourceName = "textfiles/textfile1.txt";

                            //一つ目の方法
                            string type;
                            byte[] resourceData;
                            rr.GetResourceData(resourceName, out type, out resourceData);

                            if (resourceData != null)
                            {
                                fileContent = Encoding.UTF8.GetString(resourceData);
                            }


                            //二つ目の方法
                            foreach (DictionaryEntry resource in rr)
                            {
                                if ((string) resource.Key == resourceName)
                                {
                                    using (var sr = new StreamReader((Stream) resource.Value))
                                    {
                                        fileContent = sr.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
