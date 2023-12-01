using HtmlAgilityPack;
using System.Data;
using System.IO;
using System.Net;
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
using System.Xml;

namespace fmtest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FolderBrowserHandler folderBrowserHandler = new FolderBrowserHandler();

        public MainWindow()
        {
            InitializeComponent();
            LoadCachedDirectory();
        }


        private void FindFileButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = GetPath();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string str = File.ReadAllText(filePath);

            var dataTable = new DataTable();

            try
            {
                dataTable = ParseHtmlToDataTable(str);
                // Now you can work with dataTable as needed
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                // Handle the exception or error logging
            }

            var usefull = MapDataTableToPlayerAttributes(dataTable);

            var abilityCalculations = new AbilityCalculators();

            var data = new List<PlayerScores>();

            foreach(var player in usefull)
            {
                data.Add(new PlayerScores
                {
                    Inf = player.Inf,
                    Name = player.Name,
                    Age = player.Age,
                    Position = player.Position,
                    Nat = player.Nat,
                    Club = player.Club,
                    TransferValue = player.TransferValue,
                    Wage = player.Wage,
                    MinAP = player.MinAP,
                    MinFeeRls = player.MinFeeRls,
                    MinFeeRlsToForeignClubs  = player.MinFeeRlsToForeignClubs,
                    Personality = player.Personality,
                    MediaHandling = player.MediaHandling,
                    LeftFoot = player.LeftFoot,
                    RightFoot = player.RightFoot,
                    AdvancedForwardScore = abilityCalculations.CalculateAdvancedForward(player),
                    BpdDefendScore = abilityCalculations.CalculateBpdOnDefend(player),
                    InsideForwardScore = abilityCalculations.CalculateInsideForward(player),
                    SegundoVolanteScore = abilityCalculations.CalculateSegundoVolanteOnSupport(player),
                    WingBackAttacking = abilityCalculations.CalculateWingBackAttacking(player),
                    SweeperKeeper = abilityCalculations.CalculateSweeperKeeper(player),
                    DeepLyingPlaymaker = abilityCalculations.CalculateDeepLyingPlaymaker(player),
                    WonderkidScore = abilityCalculations.CalculateWonderkidPotential(player),
                });
            }

            PlayerDataGrid.ItemsSource = data;
        }

        public string GetPath()
        {
            // Specify your folder path here
            string folderPath = txtSelectedFolder.Text;

            // Ensure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Folder does not exist.");
                return "";
            }

            // Get all files in the folder
            var files = Directory.GetFiles(folderPath);

            // Check if there are any files in the folder
            if (files.Length == 0)
            {
                Console.WriteLine("No files found in the folder.");
                return "";
            }

            // Find the most recent file
            var mostRecentFile = files.OrderByDescending(f => File.GetCreationTime(f)).FirstOrDefault();
            Console.WriteLine("Most recent file: " + mostRecentFile);
            return mostRecentFile;
        }

        public static DataTable ParseHtmlToDataTable(string html)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//table");

            if (htmlNode != null)
            {
                DataTable dataTable = new DataTable();

                // Adding columns
                var headerNodes = htmlNode.SelectNodes("tr/th");
                if (headerNodes != null)
                {
                    foreach (HtmlNode column in headerNodes)
                    {
                        dataTable.Columns.Add(column.InnerText.Trim());
                    }
                }
                else
                {
                    // Handle the case where there are no <th> elements
                    // For example, you might want to create columns based on the first row <td> elements
                }

                // Adding rows
                foreach (HtmlNode row in htmlNode.SelectNodes("tr[position()>1]"))
                {
                    DataRow dataRow = dataTable.NewRow();
                    int columnIndex = 0;
                    foreach (HtmlNode cell in row.SelectNodes("td"))
                    {
                        if (columnIndex < dataTable.Columns.Count)
                        {
                            dataRow[columnIndex] = cell.InnerText.Trim();
                            columnIndex++;
                        }
                    }
                    dataTable.Rows.Add(dataRow);
                }

                return dataTable;
            }
            else
            {
                throw new InvalidOperationException("No table found in HTML string.");
            }
        }

        public static List<PlayerAttributes> MapDataTableToPlayerAttributes(DataTable table)
        {
            var players = new List<PlayerAttributes>();

            foreach (DataRow row in table.Rows)
            {
                if (!(row == null || row["Name"].ToString() == ""))
                {
                    var player = new PlayerAttributes
                    {
                        Inf = row["Inf"].ToString() ?? string.Empty,
                        Name = row["Name"].ToString() ?? string.Empty,
                        Position = row["Position"].ToString() ?? string.Empty,
                        Nat = row["Nat"].ToString() ?? string.Empty,
                        Age = Convert.ToInt32(row["Age"]),
                        Club = row["Club"].ToString() ?? string.Empty,
                        TransferValue = row["Transfer Value"].ToString() ?? string.Empty,
                        Wage = row["Wage"].ToString() ?? string.Empty,
                        MinAP = row["Min AP"].ToString() ?? string.Empty,
                        MinFeeRls = row["Min Fee Rls"].ToString() ?? string.Empty,
                        MinFeeRlsToForeignClubs = row["Min Fee Rls to Foreign Clubs"].ToString() ?? string.Empty,
                        Personality = row["Personality"].ToString() ?? string.Empty,
                        MediaHandling = row["Media Handling"].ToString() ?? string.Empty,
                        LeftFoot = row["Left Foot"].ToString() ?? string.Empty,
                        RightFoot = row["Right Foot"].ToString() ?? string.Empty,
                        OneVOne = Convert.ToInt32(row["1v1"]),
                        Acc = Convert.ToInt32(row["Acc"]),
                        Aer = Convert.ToInt32(row["Aer"]),
                        Agg = Convert.ToInt32(row["Agg"]),
                        Agi = Convert.ToInt32(row["Agi"]),
                        Ant = Convert.ToInt32(row["Ant"]),
                        Bal = Convert.ToInt32(row["Bal"]),
                        Bra = Convert.ToInt32(row["Bra"]),
                        Cmd = Convert.ToInt32(row["Cmd"]),
                        Cnt = Convert.ToInt32(row["Cnt"]),
                        Cmp = Convert.ToInt32(row["Cmp"]),
                        Cro = Convert.ToInt32(row["Cro"]),
                        Dec = Convert.ToInt32(row["Dec"]),
                        Det = Convert.ToInt32(row["Det"]),
                        Dri = Convert.ToInt32(row["Dri"]),
                        Fin = Convert.ToInt32(row["Fin"]),
                        Fir = Convert.ToInt32(row["Fir"]),
                        Fla = Convert.ToInt32(row["Fla"]),
                        Han = Convert.ToInt32(row["Han"]),
                        Hea = Convert.ToInt32(row["Hea"]),
                        Jum = Convert.ToInt32(row["Jum"]),
                        Kic = Convert.ToInt32(row["Kic"]),
                        Ldr = Convert.ToInt32(row["Ldr"]),
                        Lon = Convert.ToInt32(row["Lon"]),
                        Mar = Convert.ToInt32(row["Mar"]),
                        OtB = Convert.ToInt32(row["OtB"]),
                        Pac = Convert.ToInt32(row["Pac"]),
                        Pas = Convert.ToInt32(row["Pas"]),
                        Pos = Convert.ToInt32(row["Pos"]),
                        Ref = Convert.ToInt32(row["Ref"]),
                        Sta = Convert.ToInt32(row["Sta"]),
                        Str = Convert.ToInt32(row["Str"]),
                        Tck = Convert.ToInt32(row["Tck"]),
                        Tea = Convert.ToInt32(row["Tea"]),
                        Tec = Convert.ToInt32(row["Tec"]),
                        Thr = Convert.ToInt32(row["Thr"]),
                        TRO = Convert.ToInt32(row["TRO"]),
                        Vis = Convert.ToInt32(row["Vis"]),
                        Wor = Convert.ToInt32(row["Wor"]),
                        Cor = Convert.ToInt32(row["Cor"]),
                        Height = row["Height"].ToString() ?? string.Empty
                    };

                    players.Add(player);
                }
            }

            return players;

        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            string selectedPath = folderBrowserHandler.OpenFolderBrowserDialog();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                txtSelectedFolder.Text = selectedPath;
            }
        }

        private void LoadCachedDirectory()
        {
            string cachedPath = folderBrowserHandler.GetCachedDirectory();
            if (!string.IsNullOrEmpty(cachedPath))
            {
                txtSelectedFolder.Text = cachedPath;
            }
        }

    }
}