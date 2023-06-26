using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Threading;

namespace RepeatTheSequence
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        
        // Задаю кількість точок що будуть загоратися
        byte quantityOfAktivation = 1;
        // змінна для перевірки правильності послідовності
        byte checkNaumberOfCall = 0;
        // Змінна для показу рівня
        byte countOflevl = 1;

        public MainWindow()
        {
            InitializeComponent();
            // Присвоюю івент кнопкам
            AssignmentEvent();
            // запускаю гру
            Aktivate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckAktivationWithNaumber((ButtonVsBoolean)sender);
        }

        // Активую певну послідовність кнопок з використанням асинхрону. async\await
        private async void Aktivate()
        {
            // Створюю список всіх доступних кнопок ( для зручності активування )
            List<ButtonVsBoolean> buttons = new List<ButtonVsBoolean>() { bu1, bu2, bu3, bu4, bu5, bu6, bu7, bu8, bu9, bu10, bu11, bu12, bu13, bu14, bu15, bu16, bu17, bu18, bu19, bu20, bu21, bu22, bu23, bu24, bu25 };

            // повертаю в вихідне положення
            ReturnChange();

            // Виводжу рівень
            lbScorLevl.Content = countOflevl;
            
            for (int i = 0; i < quantityOfAktivation; i++)
            {
                // Загальний рандом для вибору кнопки і маніпулювання знею
                int randomButton = random.Next(0, buttons.Count);
                //перевірка чи кнопка вже активована
                if (buttons[randomButton].isButtonSwich)
                { 
                    i--;
                    continue;
                }
                // Візуальне відтворення, зміна флажків для подальшої обробки інформації
                buttons[randomButton].Background = Brushes.Red;
                await Task.Delay(500);// затримка для візуального ефекту
                buttons[randomButton].Background = Brushes.White;
                // встановлюю прапорець кнопки
                buttons[randomButton].isButtonSwich = true;
                // встановлюю порядок активації
                buttons[randomButton].NamberOfCall = i;
            }
        }

        // Логіка гри з\без привязки до почерговості загорання
        private void CheckAktivationWithNaumber(ButtonVsBoolean buttonVsBoolean)
        {
            // якщо точку повторено вірно
            if (buttonVsBoolean.isButtonSwich)
            {
                // якщо співпадає послідовність активації і перевірки (перехід до наступної ітерації)
                if (buttonVsBoolean.NamberOfCall==checkNaumberOfCall)
                {
                    // змінюю прапорець кнопки
                    buttonVsBoolean.isButtonSwich = false;
                    // збільшую параметр перевірки почерговості активації
                    checkNaumberOfCall++;
                    
                    buttonVsBoolean.Background = Brushes.Yellow;
                }
                else
                {
                    MessageBox.Show("You are lose");
                    // перевіряю і встановлюю наступний рівень
                    if (quantityOfAktivation > 1)
                    {
                        quantityOfAktivation--;
                        countOflevl--;
                    }
                    // Запускаю новий рівень
                    Aktivate();
                }
            }
            // якщо неправильно вказана точка
            else
            {
                MessageBox.Show("You are lose");
                // перевіряю і встановлюю наступний рівень
                if (quantityOfAktivation > 1)
                {
                    quantityOfAktivation--;
                    countOflevl--;
                }
                // Запускаю новий рівень
                Aktivate();
            }

            // якщо кількість правильних активацій і початково встановлених точок співпадає (починаємо новий рівень)
            if (quantityOfAktivation == checkNaumberOfCall)
            {
                MessageBox.Show("lets go to next levl!");
                // підвищую кількість точок і рівень
                quantityOfAktivation++;
                countOflevl++;

                // запускаю новий рівень
                Aktivate();
            }
        }
        private void CheckAktivationiWthoutNaumber(ButtonVsBoolean buttonVsBoolean)
        {
            // якщо точку повторено вірно
            if (buttonVsBoolean.isButtonSwich)
            {
                    // змінюю прапорець кнопки
                    buttonVsBoolean.isButtonSwich = false;

                    buttonVsBoolean.Background = Brushes.Yellow;
            }
            // якщо неправильно вказана точка
            else
            {
                MessageBox.Show("You are lose");
                // перевіряю і встановлюю наступний рівень
                if (quantityOfAktivation > 1)
                {
                    quantityOfAktivation--;
                    countOflevl--;
                }
                // Запускаю новий рівень
                Aktivate();

            }
            // якщо кількість правильних активацій і початково встановлених точок співпадає (починаємо новий рівень)
            if (quantityOfAktivation == checkNaumberOfCall)
            {
                MessageBox.Show("lets go to next levl!");
                // підвищую кількість точок і рівень
                quantityOfAktivation++;
                countOflevl++;

                // запускаю новий рівень
                Aktivate();
            }
        }

        // Повертаю всі параметри в вихідне положення
        private void ReturnChange()
        {
            List<ButtonVsBoolean> buttons = new List<ButtonVsBoolean>() { bu1, bu2, bu3, bu4, bu5, bu6, bu7, bu8, bu9, bu10, bu11, bu12, bu13, bu14, bu15, bu16, bu17, bu18, bu19, bu20, bu21, bu22, bu23, bu24, bu25 };
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].isButtonSwich = false;
                buttons[i].NamberOfCall = 0;
                buttons[i].Background = Brushes.White;
            }
            checkNaumberOfCall = 0;
        }

        // присвоюю івент кліку кожній кнопці
        private void AssignmentEvent()
        {
            List<ButtonVsBoolean> buttons = new List<ButtonVsBoolean>() { bu1, bu2, bu3, bu4, bu5, bu6, bu7, bu8, bu9, bu10, bu11, bu12, bu13, bu14, bu15, bu16, bu17, bu18, bu19, bu20, bu21, bu22, bu23, bu24, bu25 };

            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Click += Button_Click;
        }


    }
}
