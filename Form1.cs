using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab_18_OOP_Danylko
{
    public partial class Form1 : Form
    {
        private readonly SimpleArray simpleArray;
        private HardArray hardArray = new HardArray();
        private double[,] array;

        public Form1()
        {
            InitializeComponent();
            simpleArray = new SimpleArray();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            array = new double[0, 0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] separators = { ';' };
            string[] input = textBox1.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            List<double> array = new List<double>();
            foreach (string item in input)
            {
                if (double.TryParse(item.Replace(" ", ""), out double result))
                {
                    array.Add(result);
                }
                else
                {
                    MessageBox.Show($"Помилка: '{item}' не є правильним числом.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            int positiveCount = simpleArray.CountPositiveNumbers(array.ToArray());
            double sumAfterLastZero = simpleArray.SumAfterLastZero(array.ToArray());
            double[] transformedArray = simpleArray.TransformArray(array.ToArray());

            label1.Text = $"Кількість додатних елементів: {positiveCount}";
            label2.Text = $"Сума елементів після\nостаннього нульового: {sumAfterLastZero}";
            textBox2.Text = string.Join(", ", transformedArray.Select(x => x.ToString()));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Перевіряємо, чи користувач ввів значення в поле textBox5
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Будь ласка, введіть номер стовпця.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Вихід з методу, якщо значення не введено
            }

            // Отримуємо введене користувачем значення з textBox5
            string columnNumberText = textBox5.Text;

            // Перевіряємо, чи введене значення є цілим числом та більше за нуль
            if (!int.TryParse(columnNumberText, out int columnNumber) || columnNumber <= 0)
            {
                MessageBox.Show("Номер стовпця повинен бути цілим числом більше за нуль.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Вихід з методу, якщо значення не є цілим числом або від'ємним
            }

            try
            {
                // Отримуємо елементи стовпця за введеним номером
                double[] columnValues = hardArray.GetColumnValues(array, columnNumber - 1);

                // Виводимо елементи стовпця в label6
                label6.Text = $"Елементи {columnNumber}-го стовпця: {string.Join(", ", columnValues)}";
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Виведення 5-го рядка масиву
            if (array.GetLength(0) >= 5)
            {
                double[] fifthRow = new double[array.GetLength(1)]; // Створюємо масив для зберігання 5-го рядка

                // Заповнюємо масив значеннями 5-го рядка
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    fifthRow[j] = array[4, j]; // Оскільки індексація починається з 0, то 5-й рядок має індекс 4
                }

                // Виводимо 5-й рядок масиву в textBox2
                label5.Text = "П'ятий рядок масиву: " + string.Join(", ", fifthRow);
            }
            else
            {
                label5.Text = "П'ятий рядок масиву: Не існує"; 
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            // Отримуємо рядок, введений користувачем
            string inputRow = textBox3.Text;

            // Розбиваємо рядок на елементи за допомогою символу роздільника
            string[] elements = inputRow.Split(';');

            // Створюємо новий тимчасовий масив, який має на один рядок більше
            double[,] tempArray = new double[array.GetLength(0) + 1, elements.Length];

            // Копіюємо елементи зі старого масиву в новий
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    tempArray[i, j] = array[i, j];
                }
            }

            // Додаємо новий рядок до нового масиву
            for (int j = 0; j < elements.Length; j++)
            {
                if (double.TryParse(elements[j], out double value))
                {
                    tempArray[array.GetLength(0), j] = value;
                }
                else
                {
                    // Обробка помилки, якщо введений елемент масиву має неправильний формат
                    MessageBox.Show($"Помилка: елемент '{elements[j]}' не є числом.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Оновлюємо збережений масив
            array = tempArray;

            // Оновлюємо вміст textBox4
            UpdateTextBox4();
        }

        private void UpdateTextBox4()
        {
            // Очищуємо textBox4
            textBox4.Clear();

            // Виводимо масив в textBox4
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    sb.Append(array[i, j]);
                    if (j < array.GetLength(1) - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.AppendLine();
            }
            textBox4.Text = sb.ToString();
        }
    }

}

