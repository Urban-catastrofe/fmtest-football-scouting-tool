
using AngleSharp.Html.Parser;
using System.Data;
using System.IO;
using System.Windows;

namespace fmtest
{
    public partial class MainWindow : Window
    {
        private readonly FolderBrowserHandler _folderBrowserHandler;

        public MainWindow()
        {
            InitializeComponent();
            _folderBrowserHandler = new FolderBrowserHandler();
            LoadCachedDirectory();
        }

        private async void FindFileButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = GetPath();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("File not found or invalid file path.");
                return;
            }

            try
            {
                string html = await File.ReadAllTextAsync(filePath);
                DataTable dataTable = await ParseHtmlToDataTableAsync(html);
                List<PlayerAttributes> playerAttributes = MapDataTableToPlayerAttributes(dataTable);

                await CalculatePlayerScoresAsync(playerAttributes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private string GetPath()
        {
            string folderPath = txtSelectedFolder.Text;
            if (!Directory.Exists(folderPath))
            {
                return string.Empty;
            }

            string[] files = Directory.GetFiles(folderPath);
            if (files.Length == 0)
            {
                return string.Empty;
            }

            string mostRecentFile = files.OrderByDescending(File.GetCreationTime).FirstOrDefault();
            return mostRecentFile;
        }

        private async Task<DataTable> ParseHtmlToDataTableAsync(string html)
        {
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(html);
            var tableElement = document.QuerySelector("table");

            if (tableElement == null)
            {
                throw new InvalidOperationException("No table found in HTML string.");
            }

            var dataTable = new DataTable();

            var headerRow = tableElement.QuerySelector("tr");
            foreach (var headerCell in headerRow.QuerySelectorAll("th"))
            {
                dataTable.Columns.Add(headerCell.TextContent.Trim());
            }

            var dataRows = tableElement.QuerySelectorAll("tr:not(:first-child)");
            foreach (var row in dataRows)
            {
                var dataRow = dataTable.NewRow();
                var cells = row.QuerySelectorAll("td");
                for (int i = 0; i < cells.Length && i < dataTable.Columns.Count; i++)
                {
                    dataRow[i] = cells[i].TextContent.Trim();
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        private List<PlayerAttributes> MapDataTableToPlayerAttributes(DataTable dataTable)
        {
            return dataTable.AsEnumerable()
                .Where(row => !string.IsNullOrEmpty(row.Field<string>("Name")))
                .Select(row => new PlayerAttributes
                {
                    Inf = row.Field<string>("Inf"),
                    Name = row.Field<string>("Name"),
                    Position = row.Field<string>("Position"),
                    Nat = row.Field<string>("Nat"),
                    Age = Convert.ToInt32(row.Field<string>("Age")),
                    Club = row.Field<string>("Club"),
                    TransferValue = row.Field<string>("Transfer Value"),
                    Wage = row.Field<string>("Wage"),
                    MinAP = row.Field<string>("Min AP"),
                    MinFeeRls = row.Field<string>("Min Fee Rls"),
                    MinFeeRlsToForeignClubs = row.Field<string>("Min Fee Rls to Foreign Clubs"),
                    Personality = row.Field<string>("Personality"),
                    MediaHandling = row.Field<string>("Media Handling"),
                    LeftFoot = row.Field<string>("Left Foot"),
                    RightFoot = row.Field<string>("Right Foot"),
                    OneVOne = Convert.ToInt32(row.Field<string>("1v1")),
                    Acc = Convert.ToInt32(row.Field<string>("Acc")),
                    Aer = Convert.ToInt32(row.Field<string>("Aer")),
                    Agg = Convert.ToInt32(row.Field<string>("Agg")),
                    Agi = Convert.ToInt32(row.Field<string>("Agi")),
                    Ant = Convert.ToInt32(row.Field<string>("Ant")),
                    Bal = Convert.ToInt32(row.Field<string>("Bal")),
                    Bra = Convert.ToInt32(row.Field<string>("Bra")),
                    Cmd = Convert.ToInt32(row.Field<string>("Cmd")),
                    Cnt = Convert.ToInt32(row.Field<string>("Cnt")),
                    Cmp = Convert.ToInt32(row.Field<string>("Cmp")),
                    Cro = Convert.ToInt32(row.Field<string>("Cro")),
                    Dec = Convert.ToInt32(row.Field<string>("Dec")),
                    Det = Convert.ToInt32(row.Field<string>("Det")),
                    Dri = Convert.ToInt32(row.Field<string>("Dri")),
                    Fin = Convert.ToInt32(row.Field<string>("Fin")),
                    Fir = Convert.ToInt32(row.Field<string>("Fir")),
                    Fla = Convert.ToInt32(row.Field<string>("Fla")),
                    Han = Convert.ToInt32(row.Field<string>("Han")),
                    Hea = Convert.ToInt32(row.Field<string>("Hea")),
                    Jum = Convert.ToInt32(row.Field<string>("Jum")),
                    Kic = Convert.ToInt32(row.Field<string>("Kic")),
                    Ldr = Convert.ToInt32(row.Field<string>("Ldr")),
                    Lon = Convert.ToInt32(row.Field<string>("Lon")),
                    Mar = Convert.ToInt32(row.Field<string>("Mar")),
                    OtB = Convert.ToInt32(row.Field<string>("OtB")),
                    Pac = Convert.ToInt32(row.Field<string>("Pac")),
                    Pas = Convert.ToInt32(row.Field<string>("Pas")),
                    Pos = Convert.ToInt32(row.Field<string>("Pos")),
                    Ref = Convert.ToInt32(row.Field<string>("Ref")),
                    Sta = Convert.ToInt32(row.Field<string>("Sta")),
                    Str = Convert.ToInt32(row.Field<string>("Str")),
                    Tck = Convert.ToInt32(row.Field<string>("Tck")),
                    Tea = Convert.ToInt32(row.Field<string>("Tea")),
                    Tec = Convert.ToInt32(row.Field<string>("Tec")),
                    Thr = Convert.ToInt32(row.Field<string>("Thr")),
                    TRO = Convert.ToInt32(row.Field<string>("TRO")),
                    Vis = Convert.ToInt32(row.Field<string>("Vis")),
                    Wor = Convert.ToInt32(row.Field<string>("Wor")),
                    Cor = Convert.ToInt32(row.Field<string>("Cor")),
                    Height = row.Field<string>("Height")
                })
                .ToList();
        }

        private async Task CalculatePlayerScoresAsync(List<PlayerAttributes> playerAttributes)
        {
            var abilityCalculations = new AbilityCalculators();

            var playerScores = await Task.WhenAll(playerAttributes.Select(async player =>
            {
                var scores = new PlayerScores
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
                    MinFeeRlsToForeignClubs = player.MinFeeRlsToForeignClubs,
                    Personality = player.Personality,
                    MediaHandling = player.MediaHandling,
                    LeftFoot = player.LeftFoot,
                    RightFoot = player.RightFoot,
                    Height = player.Height,
                    AdvancedForwardScore = await abilityCalculations.CalculateAdvancedForward(player),
                    BpdDefendScore = await abilityCalculations.CalculateBpdOnDefend(player),
                    InsideForwardScore = await abilityCalculations.CalculateInsideForward(player),
                    SegundoVolanteScore = await abilityCalculations.CalculateSegundoVolanteOnSupport(player),
                    WingBackAttacking = await abilityCalculations.CalculateWingBackAttacking(player),
                    SweeperKeeper = await abilityCalculations.CalculateSweeperKeeper(player),
                    DeepLyingPlaymaker = await   abilityCalculations.CalculateDeepLyingPlaymaker(player),
                    WonderkidScore = await abilityCalculations.CalculateWonderkidPotential(player),
                    DefensiveMidfielder = await abilityCalculations.CalculateDefensiveMidfielder(player)
                };
                return scores;
            }));

            PlayerDataGrid.ItemsSource = playerScores;
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            string selectedPath = _folderBrowserHandler.OpenFolderBrowserDialog();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                txtSelectedFolder.Text = selectedPath;
            }
        }

        private void LoadCachedDirectory()
        {
            string cachedPath = _folderBrowserHandler.GetCachedDirectory();
            if (!string.IsNullOrEmpty(cachedPath))
            {
                txtSelectedFolder.Text = cachedPath;
            }
        }
    }
}