using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace GiveMeWisdomNowPlease
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            m_lines = LoadWisdom();
            m_usedLines = new List<string>();
            m_random = new Random();

            Wisdom.Text = ChooseNewWisdom();
        }

        private List<string> LoadWisdom()
        {
            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GiveMeWisdomNowPlease.Wisdom.txt");

            List<string> wisdom = LoadWisdomFromStream(new StreamReader(resourceStream));

            if (System.IO.File.Exists("wisdom.txt"))
            {
                wisdom.AddRange(LoadWisdomFromStream(new StreamReader("wisdom.txt")));
            }

            return wisdom;
        }

        private static List<string> LoadWisdomFromStream(StreamReader reader)
        {
            List<string> lines = new List<string>();

            while (reader.Peek() >= 0)
            {
                lines.Add(reader.ReadLine());
            }

            return lines;
        }

        private string ChooseNewWisdom()
        {
            int index = m_random.Next(m_lines.Count);

            string wisdom = m_lines[index];

            m_lines.RemoveAt(index);
            m_usedLines.Add(wisdom);

            if (m_usedLines.Count >= m_lines.Count)
            {
                m_lines.Add(m_usedLines[0]);
                m_usedLines.RemoveAt(0);
            }

            return wisdom;
        }

        private void NewWisdom_Click(object sender, RoutedEventArgs e)
        {
            Wisdom.Text = ChooseNewWisdom();
        }

        private List<string> m_lines;
        private List<string> m_usedLines;
        private Random m_random;
    }
}
