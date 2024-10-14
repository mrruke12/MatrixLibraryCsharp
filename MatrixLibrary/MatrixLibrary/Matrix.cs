namespace MatrixLibrary {
    public class Matrix : IEnumerable<int> {
        int[][] m;
        public int size {
            get;
            init;
        }

        public Matrix(int size) {
            if (size < 2 || size > 10) throw new InvalidDataException(message: $"\nНекорректный порядок матрицы: {size}. Ранг должен быть между [2, 10]");

            m = new int[size][];
            for (int i = 0; i < size; i++) m[i] = new int[size];
            this.size = size;
        }

        public static Matrix Parse() {
            int size = System.Int32.Parse(Console.ReadLine());
            Matrix m = new Matrix(size);

            string temp;

            for (int i = 0; i < size; i++) {
                temp = Console.ReadLine();
                m.m[i] = temp.Split(' ').Select(el => System.Int32.Parse(el)).ToArray();
            }
            return m;
        }

        public string ToString() {
            string s = "";

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                   s += $"{m[i][j]} ";
                }
                s += "\n";
            }

            return s;
        }

        public int Minor(int i, int j) {
            Matrix minor = new Matrix(size - 1); // создаем контейнер для минора текущего элемента

            int cRow = 0; // текущая строка в миноре
            int cCol = 0; // текущий столбец в 

            for (int a = 0; a < size; a++) {
                cCol = 0; // обнуляем текущий столбец
                if (a == i) continue; // пропускаем ряд нашего элемента
                for (int b = 1; b < size; b++) {
                    minor[cRow, cCol] = this[a, b];
                    cCol++;
                }
                cRow++;
            }

            return Determinator(minor);
        }

        public int Determinator(Matrix curr = null) {
            if (curr == null) curr = this;

            if (curr.size == 2) return curr[0, 0] * curr[1, 1] - curr[0, 1] * curr[1, 0]; // базовый случай

            int res = 0;

            for (int i = 0; i < curr.size; i++) { // перебираем элементы первого столбца
                int multiplier = i % 2 == 0 ? 1 : -1;
                res += curr[i, 0] * multiplier * curr.Minor(i, 0); // вычисляем текущее алгебраическое дополнение, минор и умножаем на элемент
            }

            return res;
        }

        // Ниже реализована возможность индексации к объекту типа Matrix
        public int this[int row, int column] {
            get => this.m[row][column];
            set => this.m[row][column] = value;
        }

        public IEnumerator<int> GetEnumerator() {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    yield return m[i][j];
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}

/*
тестовый ввод:
4
1 2 3 4
5 6 7 8
1 5 2 6
3 7 9 8
    */
