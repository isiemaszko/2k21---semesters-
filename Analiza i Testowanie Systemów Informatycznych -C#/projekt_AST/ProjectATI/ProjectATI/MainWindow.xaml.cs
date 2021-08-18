using Microsoft.Win32;
using ProjectATI.Code;
using ProjectATI.Code.Enums;
using ProjectATI.Code.Parser;
using ProjectATI.Code.PKB;
using ProjectATI.Code.QueryProcessor;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ProjectATI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Node AST = null;
        private Pkb Pkb = null;
        public MainWindow()
        {
            InitializeComponent();
            Pkb = Pkb.GetPkbInstance();
        }

        private void Load_File_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                string[] lines = System.IO.File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    Load_TextBox.Text += line + "\r\n";
                }
            }
        }

        private void Run_Query_Click(object sender, RoutedEventArgs e)
        {
            var queryText = Query_TextBox.Text;
            QueryPreprocessor queryPreprocessor = new QueryPreprocessor(queryText);
            Query query = queryPreprocessor.Query;
            QueryEvaluator queryEvaluator = new QueryEvaluator(query);
            queryEvaluator.Evaluate();
            var resultProjector = queryEvaluator.ResultProjector;
            Result_TextBox.Text = resultProjector.Project();
            //int stop = 1;
        }

        private void Parse(object sender, RoutedEventArgs e)
        {
                TextBox load = (TextBox)this.Load_TextBox;
            var tmp = load.Text.Split(new string[] { "\r\n" }, System.StringSplitOptions.None).Select(item => item.Trim()).ToList();
            Parser parser = new Parser(load.Text);
            Node node = parser.Parse();
            
            Pkb.SetAST((ProgramStatement)node);
            Pkb.SetProcedureList();
            Pkb.PopulateTables(node);
            int w = 0;
        }
    }
}
