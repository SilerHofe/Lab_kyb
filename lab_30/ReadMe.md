# Основной алгоритм
Задача состоит в том, чтобы создать алгоритм, которой будет генерировать таблицу пересекающихся по вертикали и горизонтали слов, с как можно большей связностью. За коэффициент связности будем принимать количество пересечений и их расположение друг с другом.

В начале необходимо определится каким образом будет задаваться таблица. Я создал список, в котором хранятся слова, после чего передаются в словарь, хранящий само слово, его координаты и расположение, а так же двумерный массив для представления доски кроссворда.
```csharp
       private List<string> words = new List<string>();
       private const int cellSize = 30;
       private const int borderSize = 1;

       private Dictionary<string, (int x, int y, bool isHorizontal)> placement = new Dictionary<string, (int x, int y, bool isHorizontal)>();
       private char[,] grid;
       private int gridWidth = 0;
       private int gridHeight = 0;
```

Теперь необходимо определится с общим алгоритмом выполнения программы: изначально мы получаем список слов от пользователя, который должны разместить в таблице, это значит что мы по очереди каждое слово будем добавлять в таблицу. При добавлении будет находить лучшую позицию для слова и пытаться добавить слово в найденное место.

Осуществляются данные операции с помощью методов FindBestPlacement и TryPlaceWordAt.
```csharp

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

```

Теперь разберем как работают данные методы.
- `FindBestPlacement`: Проходит по всей сетке, вызывает `TryPlaceWordAt` для каждой возможной позиции и выбирает ту, где будет больше всего пересечений.
- `TryPlaceWordAt`: Проверяет, можно ли разместить слово в конкретной позиции, и считает количество пересечений, при этом учитывает границы сетки, наложение, соответствие букв.
**Метод `FindBestPlacement`**
**Назначение:** Этот метод ищет наилучшее место для размещения нового слова на сетке кроссворда. “Наилучшее” означает место, где слово будет иметь больше пересечений с уже размещенными словами.

**Как это работает:**
* **Подготовка:**
    - `bestCrossCount` (лучшее количество пересечений) – это переменная, которая запоминает, сколько пересечений было у самого лучшего найденного места для слова. Изначально она равна -1, чтобы любое первое найденное размещение было лучше.
    - `bestPlacement` (лучшее размещение) – это переменная, которая будет хранить координаты x, y и ориентацию (горизонтально или вертикально) самого лучшего места для слова. Изначально она равна `null`, так как мы еще не нашли хорошее место.
* **Перебор по всей сетке:**
    - Метод проходит по всем клеткам сетки (`grid`) с помощью двух вложенных циклов (`for` по строкам и столбцам).
    - Для каждой клетки `grid[row, col]` проверяется, не пустая ли она (`if (grid[row, col] == '\0')`). Пустая клетка не подходит для начала нового слова, поэтому переходим к следующей.
* **Поиск возможных пересечений:**
    - Если клетка не пустая, метод ищет возможные пересечения для нового слова `word` с буквой в текущей клетке.
    - Проходит по каждой букве `charIndex` слова `word`, и проверяет, совпадает ли буква слова с буквой в текущей ячейке сетки (`if (grid[row, col] == word[charIndex])`). Если буквы не совпадают, то это место не подходит для пересечения.
* **Проверка вариантов размещения:**
    - Если буквы совпали, метод вызывает `TryPlaceWordAt` **дважды**:
        - **Вертикальное размещение:** `TryPlaceWordAt(word, col, row - charIndex, false)` – пытается разместить слово вертикально, начиная с клетки выше (или ниже, в зависимости от `charIndex`).
        - **Горизонтальное размещение:** `TryPlaceWordAt(word, col - charIndex, row, true)` – пытается разместить слово горизонтально, начиная с клетки левее (или правее, в зависимости от `charIndex`).
        - Переменная `charIndex` используется для корректировки начальной позиции в соответствии с тем, какая буква слова совпадает с буквой на сетке.
* **Анализ результатов `TryPlaceWordAt`:** 
*  `TryPlaceWordAt` возвращает информацию о возможности размещения слова в данной позиции (количество пересечений, координаты и ориентацию). Если метод возвращает `null`, это означает, что слово нельзя разместить в этой позиции. 
* Если `TryPlaceWordAt` вернул значение, метод проверяет:
    - `crossCount` (количество пересечений) – если количество пересечений больше, чем `bestCrossCount`, то это новое наилучшее место для размещения слова.
    - Обновляет `bestCrossCount` и `bestPlacement` с новыми данными.
* **Возврат результата:** После того, как метод перебрал все клетки сетки и все варианты размещения слова, он возвращает `bestPlacement` – наилучшее место для слова (координаты x, y и ориентацию). Если ни одного подходящего места не было найдено, то вернется `null`.

**Метод `TryPlaceWordAt`**
**Назначение:** Этот метод проверяет, можно ли разместить слово в заданной позиции на сетке (с заданными координатами и ориентацией), и подсчитывает количество пересечений.
**Как это работает:**
* **Подготовка:**
    - `crossCount` (количество пересечений) – счетчик для отслеживания количества пересечений, начинает с 0.
* **Проверка границ:**
    - Сначала метод проверяет, не выходит ли предлагаемое размещение слова за границы сетки (`if (x < 0 || y < 0 || ...)`). Если слово выходит за границы, возвращается `null`.
* **Проверка на пересечения:**
    - Метод перебирает все буквы `i` в слове `word`.
    - Вычисляет координаты `gridX` и `gridY` для каждой буквы слова на сетке.
    - Проверяет, не пуста ли клетка `grid[gridY, gridX]` (с помощью `if (grid[gridY, gridX] != '\0')`).
    - Если клетка не пустая, это значит, что на этом месте уже есть буква другого слова.
    - Далее идет проверка с помощью Linq: `if (placement.Any(p => ...))`, тут проходятся по всем добавленным словам в `placement`. И проверяется есть ли такое слово которое пересекается с тем которое мы хотим добавить в сетку (имеется ввиду накладывается на туже ячейку, что и наше слово)
    - Если пересечение есть, то метод проверяет, правильное ли оно (т.е. чтобы новое слово пересекалось с добавленным словом на той же букве, на которой мы и пытались поставить пересечение, проверка на `if (placement.Any(p =>...))`)
    - Если пересечение не правильное, то есть буквы не совпадают на месте пересечения, то метод возвращает `null`.
    - Если пересечение правильное, то есть буквы совпадают на месте пересечения, то метод увеличивает счетчик пересечений `crossCount++`.
* **Проверка на наложение:**
    - После проверки пересечений метод еще раз перебирает все буквы в слове.
    - Проверяет, чтобы буква слова совпадала с буквой в той же клетке сетки (`if (grid[gridY, gridX] != '\0' && grid[gridY, gridX] != word[i])`).
    - Если буква не совпадает, то слово нельзя разместить, и метод возвращает `null`.
* **Возврат результата:** Если все проверки прошли успешно (слово можно разместить), метод возвращает кортеж `(x, y, crossCount, isHorizontal)`, в котором содержатся:
    - `x`, `y`: координаты начала слова.
    - `crossCount`: количество пересечений.
    - `isHorizontal`: ориентация слова (горизонтальная или вертикальная).

# Визуализация
Для визуализации будет использоваться WPF .NET Framework. С помощью компонентов создадим интерфейс, который должен предусматривать возможность добавления слов пользователем и заполнение таблицы.

Для начала, реализуем метод, который берет слово и размещает его на сетке.
```csharp
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
```

Разберем, как работает данный метод.
**Метод `PlaceWord` выполняет следующие действия:**
1. Находит наилучшее место для слова.
2. Размещает буквы слова на сетке.
3. Сохраняет информацию о размещении слова.
4. Обновляет размеры сетки (если это необходимо) и копирует данные из старой сетки в новую.

**Шаги выполнения:**
1. **Поиск наилучшего места:**
    - `FindBestPlacement(word)`: Сначала метод вызывает метод `FindBestPlacement`, который мы разбирали ранее. Этот метод возвращает координаты x, y и ориентацию (горизонтальную или вертикальную) наилучшего места для заданного слова.
    - Результат сохраняется в переменной `bestPlacement`. Эта переменная может содержать либо информацию о размещении (если место найдено), либо значение `null` (если подходящего места не найдено).
2. **Проверка наличия места:**
    - `if (bestPlacement.HasValue)`: Метод проверяет, было ли найдено подходящее место. `HasValue` – это свойство nullable-типа, которое указывает, есть ли значение, в нашем случае место для слова.
    - Если место найдено (`HasValue` равно `true`), то метод продолжает выполнение. Если место не найдено (т.е. `bestPlacement` равно `null`), то код внутри `if` не выполняется, и слово не будет размещено на сетке.
3. **Извлечение информации о размещении:**
    - `var placementInfo = bestPlacement.Value;`: Если место найдено, то из переменной `bestPlacement` извлекается фактическое значение, которое содержит координаты `x`, `y`, и логическую переменную `isHorizontal`, которая указывает на ориентацию слова.
4. **Размещение слова на сетке:**
    - Метод проходит по всем буквам слова `word` с помощью цикла `for (int i = 0; i < word.Length; i++)`.
    - Внутри цикла проверяется ориентация слова `if (!isHorizontal)`.
        - **Вертикальное размещение:** Если слово размещается вертикально, то текущая буква слова записывается в ячейку сетки `grid[y+i, x] = word[i]`. Координата `y` изменяется с каждой буквой (то есть идем по вертикали), а `x` остается неизменным.
        - **Горизонтальное размещение:** Если слово размещается горизонтально, то текущая буква слова записывается в ячейку сетки `grid[y, x+i] = word[i]`. Координата `x` изменяется с каждой буквой (то есть идем по горизонтали), а `y` остается неизменным.
5. **Сохранение информации о размещении:**
    - `placement.Add(word, (x, y, isHorizontal));`: Метод сохраняет информацию о размещении слова в словарь `placement`. Словарь `placement` хранит соответствие между словом (ключ) и кортежем, содержащим координаты x, y и ориентацию (значение). Эта информация будет использоваться далее для рисования кроссворда и для поиска пересечений.
6. **Обновление размеров сетки:**
    - `gridWidth = Math.Max(gridWidth, x + (isHorizontal ? word.Length : 0) + 10);`: Метод вычисляет новую ширину сетки, сравнивая текущую ширину `gridWidth` с максимально возможным значением после добавления слова. К ширине слова прибавляется 10 клеток для запаса.
    - `gridHeight = Math.Max(gridHeight, y + (!isHorizontal ? word.Length : 0) + 10);`: Аналогично вычисляется новая высота сетки.
    - Эти строки гарантируют, что сетка будет достаточно большой, чтобы вместить новое слово и останется запас места.
7. **Создание и копирование новой сетки:**
    - `char[,] newGrid = new char[gridHeight, gridWidth];`: Создается новая сетка `newGrid` с новыми размерами `gridHeight` и `gridWidth`.
    - Следующий цикл `for` копирует данные из старой сетки в новую:
        - Проходится по строкам `r` и столбцам `c` старой и новой сетки.
        - Копирует значение из ячейки `grid[r, c]` старой сетки в ячейку `newGrid[r, c]` новой сетки.
    - Важно, что копируются только те данные, которые существуют в обеих сетках (то есть учитывается минимальный размер между ними).
8. **Обновление сетки:**
    - `grid = newGrid;`: Старая сетка `grid` заменяется на новую сетку `newGrid`. Это делается для того, чтобы отразить изменения размера сетки после добавления нового слова.

Теперь реализуем добавления слов. Для этого создадим обработчик события, который будет добавлять слова в список:
```csharp
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(wordTextBox.Text))
            {
                words.Add(wordTextBox.Text);
                wordTextBox.Clear();
            }
       }
```

Теперь метод для заполнений таблицы, который также вызовет метод её отрисовки:
```csharp
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
```
Этот метод при нажатии кнопки "нарисовать", вызывает метод заполнения таблицы, выводит сообщение в случае если это невозможно и вызывает метод построения таблицы.
```csharp
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
```
Метод `BuildCrissCrossPuzzle` берет список слов, размещает первое слово в центре сетки, а затем размещает остальные слова на сетке с помощью метода `PlaceWord`, после чего отображает получившийся кроссворд с помощью метода `DrawPuzzle`.
```csharp
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
```

Метод `DrawPuzzle` берет информацию о размещении слов из словаря `placement` и рисует их на холсте `gridCanvas`, используя прямоугольники для ячеек и текстовые блоки для букв.

**Метод `DrawPuzzle` выполняет следующие действия:**
1. Вычисляет размеры холста, необходимые для отображения кроссворда.
2. Проходит по каждому слову и его буквам.
3. Создает прямоугольник для каждой ячейки.
4. Создает текстовый блок для каждой буквы.
5. Размещает прямоугольники и текстовые блоки на холсте, отображая тем самым готовый кроссворд
**Шаги выполнения:**
1. **Проверка наличия данных:**
    - `if (placement == null || placement.Count == 0) return;`: Метод сначала проверяет, что словарь `placement` не равен `null` и содержит хотя бы одну запись. Если это не так (нет слов для отрисовки), то метод завершает свою работу.
2. **Вычисление размеров холста:**
    - `double canvasWidth = gridWidth * cellSize + 20;`: Вычисляет ширину холста `gridCanvas`. Она равна ширине сетки `gridWidth`, умноженной на размер ячейки `cellSize`, плюс 20 пикселей для запаса.
    - `double canvasHeight = gridHeight * cellSize + 20;`: Аналогично вычисляет высоту холста, учитывая высоту сетки `gridHeight`, размер ячейки `cellSize`, и запас в 20 пикселей.
3. **Установка размеров холста:**
    - `gridCanvas.Width = canvasWidth;`: Устанавливает вычисленную ширину для холста `gridCanvas`.
    - `gridCanvas.Height = canvasHeight;`: Устанавливает вычисленную высоту для холста `gridCanvas`. Теперь холст готов для отрисовки кроссворда.
4. **Отрисовка каждого слова:**
    - `foreach (var wordPlacement in placement)`: Метод проходит по каждой записи в словаре `placement`. Каждая запись содержит:
        - `string word`: Само слово.
        - `(int x, int y, bool isHorizontal) pos`: Кортеж с информацией о размещении слова (координаты x, y и ориентация - горизонтальная или вертикальная).
    - Внутри цикла для каждого слова выполняется:
        - `for (int i = 0; i < word.Length; i++)`: Метод проходит по каждой букве слова.
5. **Вычисление координат для каждой буквы:**
    - `int x = pos.x; int y = pos.y;`: Координаты начала слова.
    - `if (pos.isHorizontal) { x += i; } else { y += i; }`: Координаты x и y меняются в зависимости от ориентации слова. Если слово горизонтальное, то меняется `x`, если вертикальное, то `y`. Это позволяет правильно расположить каждую букву слова в нужном месте.
6. **Создание и размещение прямоугольника для ячейки:**
    - `var rect = new Rectangle { ... };`: Создается прямоугольник `Rectangle`, который будет представлять собой ячейку для буквы.
        - `Width = cellSize; Height = cellSize;`: Устанавливает размеры прямоугольника в соответствии с размером ячейки.
        - `Stroke = Brushes.Black; StrokeThickness = borderSize;`: Устанавливает цвет границы и ее толщину.
        - `Fill = Brushes.White;`: Заполняет прямоугольник белым цветом.
    - `Canvas.SetLeft(rect, x * cellSize - 10); Canvas.SetTop(rect, y * cellSize - 10);`: Устанавливает позицию прямоугольника на холсте `gridCanvas` с учетом смещения (-10) для корректного расположения.
7. **Создание и размещение текстового блока для буквы:**
    - `var textBlock = new TextBlock { ... };`: Создается текстовый блок `TextBlock`, в котором будет отображаться буква.
        - `Text = word[i].ToString();`: Устанавливает текст для блока равным текущей букве.
        - `TextAlignment = TextAlignment.Center; VerticalAlignment = VerticalAlignment.Center;`: Выравнивает текст по центру ячейки.
        - `FontSize = cellSize * 0.7; FontWeight = FontWeights.Bold;`: Устанавливает размер и начертание шрифта для буквы.
        - `Width = cellSize; Height = cellSize;`: Устанавливает размеры текстового блока.
    - `Canvas.SetLeft(textBlock, x * cellSize - 10); Canvas.SetTop(textBlock, y * cellSize - 10);`: Устанавливает позицию текстового блока на холсте `gridCanvas` с учетом смещения (-10) для корректного расположения.
8. **Добавление элементов на холст:**
    - `gridCanvas.Children.Add(rect); gridCanvas.Children.Add(textBlock);`: Добавляет созданный прямоугольник `rect` и текстовый блок `textBlock` на холст `gridCanvas`. Таким образом, ячейка и буква появляются на экране.

Примеры работы алгоритма:![[Pasted image 20250113182217.png]]
![[Pasted image 20250113183022.png]]
