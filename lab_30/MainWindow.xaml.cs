using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace lab_30
{   public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private List<string> words = new List<string>();
        private const int cellSize = 30;
        private const int borderSize = 1;

        private Dictionary<string, (int x, int y, bool isHorizontal)> placement = new Dictionary<string, (int x, int y, bool isHorizontal)>();
        private char[,] grid;
        private int gridWidth = 0;
        private int gridHeight = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // words.Clear();
            if (!string.IsNullOrEmpty(wordTextBox.Text))
            {
                words.Add(wordTextBox.Text);
                wordTextBox.Clear();
            }
       }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gridCanvas.Children.Clear(); // Очищаем предыдущую отрисовку
            placement.Clear(); // Очищаем текущие размещения
            if (words.Count == 0)
            {
                MessageBox.Show("Добавьте слова для построения головоломки");
                return;
            }

            //  Очищаем сетку
            grid = null;
            gridWidth = 0;
            gridHeight = 0;

            // Вызываем метод для построения головоломки
            BuildCrissCrossPuzzle();
        }
        private void BuildCrissCrossPuzzle()
        {
            if (words.Count == 0)
                return;

            string firstWord = words[0];
            int startX = 10;
            int startY = 10;

            placement.Add(firstWord, (startX, startY, true));

            gridWidth = Math.Max(gridWidth, startX + firstWord.Length + 10);
            gridHeight = Math.Max(gridHeight, startY + 10);

            grid = new char[gridHeight, gridWidth];
            for (int i = 0; i < firstWord.Length; i++)
            {
                grid[startY, startX + i] = firstWord[i];
            }

            for (int i = 1; i < words.Count; i++)
            {
                PlaceWord(words[i]);
            }

            DrawPuzzle();
        }

        private void PlaceWord(string word)
        {
            (int x, int y, bool isHorizontal)? bestPlacement = FindBestPlacement(word);

            if (bestPlacement.HasValue)
            {
                var placementInfo = bestPlacement.Value;
                var x = placementInfo.x;
                var y = placementInfo.y;
                var isHorizontal = placementInfo.isHorizontal;

                for (int i = 0; i < word.Length; i++)
                {
                    if (!isHorizontal)
                    {
                        grid[y+i, x] = word[i];
                    }
                    else
                    {
                        grid[y, x+i] = word[i];
                    }
                }

                placement.Add(word, (x, y, isHorizontal));

                gridWidth = Math.Max(gridWidth, x + (isHorizontal ? word.Length : 0) + 10);
                gridHeight = Math.Max(gridHeight, y + (!isHorizontal ? word.Length : 0) + 10);
                char[,] newGrid = new char[gridHeight, gridWidth];

                for (int r = 0; r < Math.Min(newGrid.GetLength(0), grid.GetLength(0)); r++)
                {
                    for (int c = 0; c < Math.Min(newGrid.GetLength(1), grid.GetLength(1)); c++)
                    {
                        newGrid[r, c] = grid[r, c];
                    }
                }

                grid = newGrid;
            }
        }

        private (int x, int y, bool isHorizontal)? FindBestPlacement(string word)
        {
            int bestCrossCount = -1;
            (int x, int y, bool isHorizontal)? bestPlacement = null;

            for (int row = 0; row < gridHeight; row++)
            {
                for (int col = 0; col < gridWidth; col++)
                {
                    if (grid[row, col] == '\0')
                        continue;

                    for (int charIndex = 0; charIndex < word.Length; charIndex++)
                    {
                        if (grid[row, col] == word[charIndex])
                        {
                            var ver = TryPlaceWordAt(word, col, row - charIndex, false);
                            if (ver.HasValue)
                            {
                                int crossCount = ver.Value.crossCount;
                                if (crossCount > bestCrossCount)
                                {
                                    bestCrossCount = crossCount;
                                    bestPlacement = (ver.Value.x, ver.Value.y, ver.Value.isHorizontal);
                                }
                            }
                            var hor = TryPlaceWordAt(word, col - charIndex, row, true);
                            if (hor.HasValue)
                            {
                                int crossCount = hor.Value.crossCount;
                                if (crossCount > bestCrossCount)
                                {
                                    bestCrossCount = crossCount;
                                    bestPlacement = (hor.Value.x, hor.Value.y, hor.Value.isHorizontal);
                                }
                            }

                        }
                    }
                }
            }
            return bestPlacement;
        }

        private (int x, int y, int crossCount, bool isHorizontal)? TryPlaceWordAt(string word, int x, int y, bool isHorizontal)
        {
            int crossCount = 0;
            if (x < 0 || y < 0 || x + (isHorizontal ? word.Length : 0) >= gridWidth || y + (!isHorizontal ? word.Length : 0) >= gridHeight)
            {
                return null;
            }
            for (int i = 0; i < word.Length; i++)
            {
                int gridX = x;
                int gridY = y;
                if (isHorizontal)
                    gridX += i;
                else
                    gridY += i;

                // Проверяем на пересечение
                if (grid[gridY, gridX] != '\0')
                {
                    if (placement.Any(p =>
                    {
                        var (px, py, pIsHorizontal) = p.Value;

                        if (pIsHorizontal)
                        {
                            if (gridY == py && gridX >= px && gridX < px + p.Key.Length)
                                return true;
                        }
                        else
                        {
                            if (gridX == px && gridY >= py && gridY < py + p.Key.Length)
                                return true;
                        }
                        return false;
                    }))
                    {
                        if (placement.Any(p =>
                        {
                            var (px, py, pIsHorizontal) = p.Value;
                            if (pIsHorizontal)
                            {
                                if (gridY == py && gridX >= px && gridX < px + p.Key.Length)
                                {
                                    return isHorizontal;
                                }
                            }
                            else
                            {
                                if (gridX == px && gridY >= py && gridY < py + p.Key.Length)
                                {
                                    return !isHorizontal;
                                }
                            }
                            return false;
                        }))
                        {
                            return null;
                        }
                        crossCount++;
                    }
                }
            }
            // Проверка наложения
            for (int i = 0; i < word.Length; i++)
            {
                int gridX = x;
                int gridY = y;
                if (isHorizontal)
                    gridX += i;
                else
                    gridY += i;

                if (grid[gridY, gridX] != '\0' && grid[gridY, gridX] != word[i])
                    return null;
            }
            return (x, y, crossCount, isHorizontal);
        }


        private void DrawPuzzle()
        {
            if (placement == null || placement.Count == 0) return;

            double canvasWidth = gridWidth * cellSize + 20;
            double canvasHeight = gridHeight * cellSize + 20;

            gridCanvas.Width = canvasWidth;
            gridCanvas.Height = canvasHeight;

            foreach (var wordPlacement in placement)
            {
                string word = wordPlacement.Key;
                (int x, int y, bool isHorizontal) pos = wordPlacement.Value;
                for (int i = 0; i < word.Length; i++)
                {
                    int x = pos.x;
                    int y = pos.y;

                    if (pos.isHorizontal)
                    {
                        x += i;
                    }
                    else
                    {
                        y += i;
                    }

                    var rect = new Rectangle
                    {
                        Width = cellSize,
                        Height = cellSize,
                        Stroke = Brushes.Black,
                        StrokeThickness = borderSize,
                        Fill = Brushes.White,
                    };

                    Canvas.SetLeft(rect, x * cellSize - 10);
                    Canvas.SetTop(rect, y * cellSize - 10);

                    var textBlock = new TextBlock
                    {
                        Text = word[i].ToString(),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontSize = cellSize * 0.7,
                        FontWeight = FontWeights.Bold,
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetLeft(textBlock, x * cellSize - 10);
                    Canvas.SetTop(textBlock, y * cellSize - 10);

                    gridCanvas.Children.Add(rect);
                    gridCanvas.Children.Add(textBlock);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
/*            if(comboBox.SelectedIndex == 0)
            {
                string path = "C:/Users/sashs/source/repos/lab_30/bin/Debug/narkoman.txt";
                StreamReader StreamReader = new StreamReader(path, Encoding.GetEncoding(1251));
                string line= StreamReader.ReadLine();
                while(line != null)
                {
                    words.Add(line);
                    line = StreamReader.ReadLine();
                }
                StreamReader.Close();
            }
            else
            {
                if(comboBox.SelectedIndex == 1)
                {
                    string path = "C:/Users/sashs/source/repos/lab_30/bin/Debug/zveri.txt";
                    StreamReader StreamReader = new StreamReader(path, Encoding.GetEncoding(1251));
                    string line = StreamReader.ReadLine();
                    while (line != null)
                    {
                        words.Add(line);
                        line = StreamReader.ReadLine();
                    }
                    StreamReader.Close();
                }
            }
*/
        }
    }
}
