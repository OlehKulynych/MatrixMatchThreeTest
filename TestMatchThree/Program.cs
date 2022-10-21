using System.IO;
using System.Text;
using TestMatchThree;


static int[,] FillMatrix() //Заповнюємо матрицю рандомними значеннями від 0 до 3 включно.
{
    int[,] matrix = new int[9, 9];
    Random r = new Random();
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            matrix[i, j] = r.Next(0, 4);
        }
    }
    return matrix;
}


static void PrintMatrix(int[,] matrix) //Ф-ція для виводу матриці.
{
    StringBuilder stringBuilder = new StringBuilder();

    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            stringBuilder.Append(matrix[i, j] + " ");
        }
        stringBuilder.Append("\n");
    }
    stringBuilder.Append("----------------------------------------------");

    using (StreamWriter sw = new StreamWriter("log.txt",true))
    {
        sw.WriteLine(stringBuilder.ToString());
    }



   
}


static int[,] TranspositionMatrix(int[,] matrix) //Транспонування матриці.
{
    int tmp;
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < i; j++)
        {
            tmp = matrix[i, j];
            matrix[i, j] = matrix[j, i];
            matrix[j, i] = tmp;
        }
    }
    return matrix;
}


static Point GetIndex(int[,] matrix) //Ф-ція для отримання індексів й рядка елементів, яких в ряд більше 3
{
    int counter = 0;
    int StartIndex = 0;
    int EndIndex = 0;
    int row = 0;
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1) - 1; j++)
        {
            if (matrix[i, j] == matrix[i, j + 1])
            {
                counter++;
                if (counter == 1)
                {
                    StartIndex = j;
                }
                EndIndex = j + 1;
                row = i;
            }
            else
            {
                if (counter >= 2)
                {
                    break;
                }
                EndIndex = 0;
                counter = 0;
            }
        }
        if (counter >= 2)
        {
            return new Point(StartIndex, EndIndex, row);
        }
        else
        {
            counter = 0;
        }
    }
    return new Point(-1, -1, -1);
}


static int[,] ChangeMatrixRow(int[,] matrix, Point point) //Тут я зсуваю елементи до низу в рядках.
{
    for (int i = point.row; i > 0; i--)
    {
        for (int j = point.start; j < point.end + 1; j++)
        {
            matrix[i, j] = matrix[i - 1, j];
        }
    }
    Random r = new Random();
    for (int j = point.start; j < point.end + 1; j++)
    {
        matrix[0, j] = r.Next(0, 4);
    }
    return matrix;
}


static int[,] ChangeMatrixCols(int[,] matrix, Point point) // Це метод для зсуву елементів стопця.
{

    for (int j = point.start - 1; j >= 0; j--)
    {
        matrix[point.row, j + (point.end - point.start + 1)] = matrix[point.row, j];

    }
    Random r = new Random();
    for (int j = 0; j < (point.end - point.start + 1); j++)
    {
        matrix[point.row, j] = r.Next(0, 4);
    }
    return matrix;
}




bool horizontalClear = false;
bool verticalClear = false;
bool stop = false;


int[,] matrix = FillMatrix(); // Заповняю матрицю
Point p = new Point(0,0,0);

while (!stop)
{
    p = GetIndex(matrix); // Отримую індекси, спочатку ми перевіряємо рядки.
    if (p.start == -1) // Якщо один із таких індексів -1, то це означає, що не має послідовностей однакових елементів в рядках.
    {
        horizontalClear = true;
    }
    else
    {
        PrintMatrix(ChangeMatrixRow(matrix, p)); // Міняємо та виводимо проміжний результат.
        p = GetIndex(matrix);
        verticalClear = false; // При якійсь зміні є можливість, що в стовпцях появиться послідовність.
    }

    if (horizontalClear) // Якщо в рядках все чисто, то переходимо до перевірки стовпців.
    {
        TranspositionMatrix(matrix); // Для того, щоб перевірити стовпці, я транспоную матрицю й можу працювати, як з рядками, тільки метод зсуву буде відрязнятись
                                     //Адже в цьому випадку ми зсуваємо з ліва на право.

        Point pj = new Point(0,0,0);

        while (!verticalClear)
        {
            pj = GetIndex(matrix);
            if (pj.start == -1) // Перевіряємо індекс
            {
                verticalClear = true;
            }
            else
            {
               

                PrintMatrix(ChangeMatrixCols(matrix, pj)); //Виводимо проміжний результат
                pj = GetIndex(matrix);
                horizontalClear = false; // При якійсь зміні є можливість, що в рядках появиться послідовність.
            }
        }
        TranspositionMatrix(matrix); // Обертаємо матрицю назад.
    }
    if (verticalClear == true && horizontalClear == true) // Цикл буде працювати допоки обидві змінні не стануть позитивними, тобто при зміні в стовпцях, ми знову починаємо перевіряти рядки й навпаки.
    {
        stop = true;
    }
}


using (StreamWriter sw = new StreamWriter("log.txt", true))
{
    sw.WriteLine("Result: ");
}
PrintMatrix(matrix);


