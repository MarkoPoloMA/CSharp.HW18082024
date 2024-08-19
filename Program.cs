using System;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace HW18082024
{
	class Program
	{
		static void Main()
		{
			string doubleNumber = "894376.243643";
			int number = int.Parse(doubleNumber); // Вася уверен, что ошибка где-то тут
			Console.WriteLine(number + 1);

			/*
            //1 ЗАДАЧАл

            var rows = 3;
            var cols = 4;
            var b = new int[3, 4];

            var a = ReadLineForArray();
            FillMatrix(rows, cols, b);
            PrintArray(a);
            PrintMatrix(b);
            ElementInArrays(a, b);

            // 2 ЗАДАЧА
            var rows = 5;
            var cols = 5;
            var b = new int[rows, cols];
            FillMatrix(rows, cols, b);
            PrintMatrix(b);
            var resArray = DoubleInArray(b);

            var minIndexElem = Array.IndexOf(resArray, resArray.Min());
            var maxIndexElem = Array.IndexOf(resArray, resArray.Max());

            if (minIndexElem > maxIndexElem)
                (maxIndexElem, maxIndexElem) = (maxIndexElem, minIndexElem);
            Console.WriteLine($"Сумма между минимальным и максимальным значением:" +
                $" { resArray.Take(minIndexElem + 1).Skip(maxIndexElem - minIndexElem - 1).Sum()}");


            // 3 ЗАДАЧА
            string str = "abcde";
            int shift = 1;

            Console.WriteLine(string.Join(", ", Ceaser(str, shift)));

            //4 ЗАДАЧА
            try
            {
                int rows = 5;
                int cols = 5;
                int value = 3;

                var firstMatrix = new int[rows, cols];
                var secondMatrix = new int[rows, cols];

                FillMatrix(firstMatrix);
                FillMatrix(secondMatrix);

                MultiplMatrixOnValue(firstMatrix, value);
                PrintMatrix(firstMatrix);

                var summaMatrix = SummaMatrix(firstMatrix, secondMatrix);
                PrintMatrix(summaMatrix);

                var multipMatrices = MultipMatrices(firstMatrix, secondMatrix);
                PrintMatrix(multipMatrices);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //5 ЗАДАЧА
            var strings = Console.ReadLine();
            Console.WriteLine(Calculate(strings));



            //6 ЗАДАЧА
            var str = "today is a good day for walking.i will try to walk near the sea";
            foreach (var x in str
                         .Split(new[] { '.', '!' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(s => char.ToUpper(s.Trim()[0]) + s.Substring(1)))
                Console.WriteLine(x);

            */
			//7 ЗАДАЧА
			var strings = "Например, текст текст если и у нас есть такой текст:";
			var word = "текст";
			Console.WriteLine(WordCounter(strings, word));
			Console.ReadKey();
		}
		private static int WordCounter(string text, string word)
		{
			int count = 0;
			int index = 0;

			while ((index = text.IndexOf(word, index)) != -1)
			{
				count++;
				index += word.Length;
			}

			return count;
		}

		private static int Calculate(string values)
		{
			values = values.Replace(" ", "");

			var strings = values
				.Split('+', '-', (char) StringSplitOptions.RemoveEmptyEntries)
				.ToArray();

			var plusMinus = values
				.Where(c => c == '+' || c == '-')
				.ToArray();

			var result = int.Parse(strings[0]);

			for (var i = 0; i < plusMinus.Length; i++)
			{
				var number = int.Parse(strings[i + 1]);

				if (plusMinus[i] == '+')
					result += number;
				else
					result -= number;
			}

			return result;
		}
		private static void MultiplMatrixOnValue(int[,] matrix, int value)
		{
			for (var i = 0; i < matrix.GetLength(0); i++)
			for (var j = 0; j < matrix.GetLength(1); j++)
				matrix[i, j] *= value;
		}
		private static int[,] SummaMatrix(int[,] firstMatrix, int[,] secondMatrix)
		{
			if (firstMatrix.GetLength(0) != secondMatrix.GetLength(0)
				|| firstMatrix.GetLength(1) != secondMatrix.GetLength(1))
				throw new ArgumentException("Матрицы не равны");

			var resultArray = new int[firstMatrix.GetLength(1), firstMatrix.GetLength(0)];

			for (var i = 0; i < firstMatrix.GetLength(0); i++)
			for (var j = 0; j < firstMatrix.GetLength(1); j++)
				resultArray[i, j] = firstMatrix[i, j] + secondMatrix[i, j];

			return resultArray;
		}
		private static int[,] MultipMatrices(int[,] firstMatrix, int[,] secondMatrix)
		{
			if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
				throw new ArgumentException
					("Количество столбцов первой матрицы должно совпадать с количеством строк второй матрицы.");

			var result = new int[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];

			for (var i = 0; i < firstMatrix.GetLength(0); i++)
			for (var j = 0; j < secondMatrix.GetLength(1); j++)
			for (var k = 0; k < firstMatrix.GetLength(1); k++)
				result[i, j] += firstMatrix[i, k] * secondMatrix[k, j];

			return result;
		}
		private static char[] Ceaser(string str, int shift)
		{
			var result = new char[str.Length];
			for (var i = 0; i < str.Length; i++)
			{
				var curStr = str[i];
				if (char.IsLetter(curStr))
					result[i] = (char) ((curStr + shift - 'a') + 'a');
				else
					result[i] = curStr;
			}

			return result;
		}
		private static int[] ReadLineForArray()
		{
			var a = Console.ReadLine()
				?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();
			return a;
		}
		private static void FillMatrix(int[,] b)
		{
			var random = new Random();
			for (var i = 0; i < b.GetLength(0); i++)
			for (var j = 0; j < b.GetLength(1); j++)
				b[i, j] = random.Next(1, 10);
		}
		private static void PrintArray(int[] a)
		{
			Console.WriteLine(string.Join(", ", a));
		}
		private static void PrintMatrix(int [,] b)
		{
			for (var i = 0; i < b.GetLength(0); i++)
			{
				for (var j = 0; j < b.GetLength(1); j++)
					Console.Write($"{b[i, j]} ");
				Console.WriteLine();
			}

			Console.WriteLine();
		}
		private static void ElementInArrays(int[] a, int[,] b)
		{
			var res = DoubleInArray(b);

			int resSummaNechet = 0;
			for (var i = 0; i < b.GetLength(0); i++)
			for (var j = 0; j < b.GetLength(1); j++)
			{
				if (j % 2 != 0)
					resSummaNechet += b[i, j];
			}

			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Минимум: " + res.Min());
			stringBuilder.AppendLine("Максимум: " + res.Max());
			stringBuilder.AppendLine("Сумма: " + res.Sum());
			stringBuilder.AppendLine("Произведение: " + res.Aggregate((x, y) => x * y));
			stringBuilder.AppendLine("Сумма нечетных: " + resSummaNechet);

			Console.WriteLine(stringBuilder);
		}
		private static int[] DoubleInArray(int[,] b)
		{
			var res = new int [b.GetLength(0) * b.GetLength(1)];
			var k = 0;

			for (var i = 0; i < b.GetLength(0); i++)
			for (var j = 0; j < b.GetLength(1); j++)
				res[k++] = b[i, j];
			return res;
		}
	}

}
