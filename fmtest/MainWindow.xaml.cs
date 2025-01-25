using AngleSharp;
using AngleSharp.Html.Parser;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

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
            try
            {
                FindFileButton.IsEnabled = false;
                btnSelectFolder.IsEnabled = false;
                LoadingIndicator.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = Cursors.Wait;

                string filePath = GetPath();
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("File not found or invalid file path.");
                    return;
                }

                LoadingIndicator.Value = 0;

                // Run all heavy operations on a background thread
                await Task.Run(async () =>
                {
                    // Load HTML (10%)
                    string html = await LoadHtmlFileAsync(filePath);
                    await UpdateProgressAsync(10);

                    // Parse HTML (20%)
                    DataTable dataTable = await ParseHtmlToDataTableAsync(html);
                    await UpdateProgressAsync(20);

                    // Map data (30%)
                    List<PlayerAttributes> playerAttributes = MapDataTableToPlayerAttributes(dataTable);
                    await UpdateProgressAsync(30);

                    // Calculate scores (30% to 100%)
                    await CalculatePlayerScoresAsync(playerAttributes, progress => 
                        UpdateProgressAsync(30 + (progress * 0.7)));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                FindFileButton.IsEnabled = true;
                btnSelectFolder.IsEnabled = true;
                LoadingIndicator.Visibility = Visibility.Collapsed;
                Mouse.OverrideCursor = null;
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
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();
            var document = await parser.ParseDocumentAsync(html);
            
            var tableElement = document.QuerySelector("table");
            if (tableElement == null)
            {
                throw new InvalidOperationException("No table found in HTML string.");
            }

            var dataTable = new DataTable();

            // Add columns from header
            var headerRow = tableElement.QuerySelector("tr");
            foreach (var headerCell in headerRow.QuerySelectorAll("th"))
            {
                dataTable.Columns.Add(headerCell.TextContent.Trim());
            }

            // Add data rows
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

        private async Task CalculatePlayerScoresAsync(List<PlayerAttributes> playerAttributes, 
            Func<double, Task> progressCallback)
        {
            var abilityCalculations = new AbilityCalculators();
            var playerScores = new List<PlayerScores>();
            const int batchSize = 1000;
            
            for (int i = 0; i < playerAttributes.Count; i += batchSize)
            {
                var batch = playerAttributes.Skip(i).Take(batchSize);
                var batchTasks = batch.Select(player =>
                {
                    return Task.Run(() =>
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
                            Height = player.Height
                        };

                        // Calculate all scores (now synchronously)
                        scores.AdvancedForwardScore = abilityCalculations.CalculateAdvancedForward(player);
                        scores.AttackingMidfielderScore = abilityCalculations.CalculateAmAbility(player);
                        scores.BpdDefendScore = abilityCalculations.CalculateBpdOnDefend(player);
                        scores.InsideForwardScore = abilityCalculations.CalculateInsideForward(player);
                        scores.SegundoVolanteScore = abilityCalculations.CalculateSegundoVolanteOnSupport(player);
                        scores.WingBackAttacking = abilityCalculations.CalculateWingBackAttacking(player);
                        scores.SweeperKeeper = abilityCalculations.CalculateSweeperKeeper(player);
                        scores.ShotStopper = abilityCalculations.CalculateShotStopper(player);
                        scores.DeepLyingPlaymaker = abilityCalculations.CalculateDeepLyingPlaymaker(player);
                        scores.WonderkidScore = abilityCalculations.CalculateWonderkidPotential(player);
                        scores.DefensiveMidfielder = abilityCalculations.CalculateDefensiveMidfielder(player);

                        // Calculate DEAL factor
                        int highestAbilityScore = new[] {
                            scores.AdvancedForwardScore,
                            scores.AttackingMidfielderScore,
                            scores.BpdDefendScore,
                            scores.InsideForwardScore,
                            scores.SegundoVolanteScore,
                            scores.WingBackAttacking,
                            scores.SweeperKeeper,
                            scores.ShotStopper,
                            scores.DeepLyingPlaymaker,
                            scores.WonderkidScore,
                            scores.DefensiveMidfielder
                        }.Max();

                        scores.DealFactor = abilityCalculations.CalculateDealFactor(
                            highestAbilityScore, 
                            player.TransferValue, 
                            player.MinFeeRls, 
                            player.MinFeeRlsToForeignClubs);

                        return scores;
                    });
                });

                var batchResults = await Task.WhenAll(batchTasks);
                playerScores.AddRange(batchResults);

                // Report progress
                double progress = (double)Math.Min(i + batchSize, playerAttributes.Count) / playerAttributes.Count;
                await progressCallback(progress);
            }

            await Dispatcher.InvokeAsync(() => PlayerDataGrid.ItemsSource = playerScores);
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

        private void CopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = PlayerDataGrid.SelectedItems.Count > 0;
        }

        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = PlayerDataGrid.SelectedItems.Cast<PlayerScores>().ToList();
            if (selectedItems.Any())
            {
                var clipboardText = string.Join(Environment.NewLine, selectedItems.Select(GetClipboardText));
                Clipboard.SetText(clipboardText);
            }
        }

        private string GetClipboardText(PlayerScores player)
        {
            // Customize the format of the clipboard text based on your requirements
            return $"{player.Name}";
        }

        private async Task<string> LoadHtmlFileAsync(string filePath)
        {
            // Use a buffer size that's a multiple of 4KB (common disk sector size)
            const int bufferSize = 4096 * 4;
            
            using var fileStream = new FileStream(
                filePath, 
                FileMode.Open, 
                FileAccess.Read, 
                FileShare.Read,
                bufferSize,
                FileOptions.Asynchronous | FileOptions.SequentialScan);
            
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            return await streamReader.ReadToEndAsync();
        }

        private async Task UpdateProgressAsync(double value)
        {
            await Dispatcher.InvokeAsync(() => LoadingIndicator.Value = value);
        }
    }
}