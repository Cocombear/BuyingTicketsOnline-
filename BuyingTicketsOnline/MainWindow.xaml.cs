using Npgsql;
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

namespace BuyingTicketsOnline
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreatingLoginInterface();
        }
        List<string> CurrentTemporaeyList = new List<string>();
        List<UIElement> ComponentsTheCurrentInterface = new List<UIElement>();
        List<TextBlock> CurrentComponentsTextBlock = new List<TextBlock>();
        List<TextBox> CurrentComponentsTextBox = new List<TextBox>();// 0 индекс это номер карты card_id, 1 индекс это ММ/ГГ, 2 это CVV код
        List<string> ListForDifferentData= new List<string>();//0 индекс это user_id, 1 индекс это user_name, 2 индекс это user_fam
        void CreatingLoginInterface() //Создение интерфейса
        {
            //CreatingPictureForLoginInterface(); 
            CreatingBoxForLoginInterface();
            CreateButtonForLoginInterface();
        }
        static NpgsqlConnection GetConnection()//Создание подключения к базе данных
        {
            return new NpgsqlConnection(@"Host=localhost;Port=1901;Database=DataBaseBuyingTicketsOnline;Username=postgres;Password=1901");
        }
        private void CreatingBoxForLoginInterface() //Создание TextBlock и TextBox для интерфейса входа
        {
                TextBlock CardNumberTextBlock = new TextBlock(); CardNumberTextBlock.Name = "Card";
            Canvas.SetLeft(CardNumberTextBlock,320); Canvas.SetTop(CardNumberTextBlock, 170);
            CardNumberTextBlock.Text = "Номер Карты"; CardNumberTextBlock.Foreground = Brushes.Black;
            ComponentsTheCurrentInterface.Add(CardNumberTextBlock); canvas.Children.Add(CardNumberTextBlock);
            CurrentComponentsTextBlock.Add(CardNumberTextBlock);
                TextBox CardNumberTextBox = new TextBox();
            Canvas.SetLeft(CardNumberTextBox, 320); Canvas.SetTop(CardNumberTextBox,190);
            ComponentsTheCurrentInterface.Add(CardNumberTextBox); canvas.Children.Add(CardNumberTextBox);
            CurrentComponentsTextBox.Add(CardNumberTextBox);
                TextBlock CardExpriyDataTextBlock = new TextBlock();
            Canvas.SetLeft(CardExpriyDataTextBlock,320); Canvas.SetTop(CardExpriyDataTextBlock,210);
            CardExpriyDataTextBlock.Text = "Срок Действия"; CardExpriyDataTextBlock.Foreground = Brushes.Black;
            ComponentsTheCurrentInterface.Add(CardExpriyDataTextBlock); canvas.Children.Add(CardExpriyDataTextBlock);
            CurrentComponentsTextBlock.Add(CardExpriyDataTextBlock);
                TextBox CardExpriyDatatextBox = new TextBox();
            Canvas.SetLeft(CardExpriyDatatextBox,320); Canvas.SetTop(CardExpriyDatatextBox,240);
            ComponentsTheCurrentInterface.Add(CardExpriyDatatextBox); canvas.Children.Add(CardExpriyDatatextBox);
            CurrentComponentsTextBox.Add(CardExpriyDatatextBox);
                TextBlock CardCvvCodeTextBlock = new TextBlock();
            Canvas.SetLeft(CardCvvCodeTextBlock,320); Canvas.SetTop(CardCvvCodeTextBlock,260);
            CardCvvCodeTextBlock.Text = "CVV Код"; CardCvvCodeTextBlock.Foreground = Brushes.Black;
            ComponentsTheCurrentInterface.Add(CardCvvCodeTextBlock); canvas.Children.Add(CardCvvCodeTextBlock);
            CurrentComponentsTextBlock.Add(CardCvvCodeTextBlock);
                TextBox CardCvvCodeTextBox = new TextBox();
            Canvas.SetLeft(CardCvvCodeTextBox,320); Canvas.SetTop(CardCvvCodeTextBox, 280);
            ComponentsTheCurrentInterface.Add(CardCvvCodeTextBox); canvas.Children.Add(CardCvvCodeTextBox);
            CurrentComponentsTextBox.Add(CardCvvCodeTextBox);
        }
        private void CreatingPictureForLoginInterface() //Создание Рисунка для Интерфейса
        {
            Rectangle PictureBackground = new Rectangle();
            PictureBackground.Height = 130;PictureBackground.Width = 200; PictureBackground.Fill = Brushes.LightPink;
            Canvas.SetLeft(PictureBackground,300); Canvas.SetTop(PictureBackground,150);
            ComponentsTheCurrentInterface.Add(PictureBackground); canvas.Children.Add(PictureBackground);
            Rectangle PictureForeGround = new Rectangle();
            PictureForeGround.Height = 40; PictureForeGround.Width = 200; PictureForeGround.Fill = Brushes.Purple;
            Canvas.SetLeft(PictureForeGround,300); Canvas.SetTop(PictureForeGround,190);
            ComponentsTheCurrentInterface.Add(PictureForeGround); canvas.Children.Add(PictureForeGround);
        }
        private void CreateButtonForLoginInterface()//Создание конпки входа 
        {
                Button LoginButton = new Button();
            LoginButton.Content = "Вход"; LoginButton.BorderBrush = Brushes.Black; LoginButton.Click += LoginButton_Click;
            Canvas.SetLeft(LoginButton,320); Canvas.SetTop(LoginButton,300);
            ComponentsTheCurrentInterface.Add(LoginButton); canvas.Children.Add(LoginButton);
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)//Описание события клика
        {
            using (NpgsqlConnection con = GetConnection())//Проверка данных о карте
            {
                con.Open();
                string TemporaryRequest1 = $"SELECT card_id FROM BasicInformationAboutCards WHERE card_id = '{CurrentComponentsTextBox[0].Text}';";
                NpgsqlCommand cmd = new NpgsqlCommand(TemporaryRequest1, con);
                string Temporary1 = cmd.ExecuteScalar().ToString();
                if (Temporary1 == null)
                {
                    MessageBox.Show("Неверно");
                }
                if (Temporary1 == CurrentComponentsTextBox[0].Text)
                {
                    CurrentTemporaeyList.Add(Temporary1);
                    string TemporaryRequest2 = $"SELECT card_expiry FROM BasicInformationAboutCards WHERE card_id = '{CurrentComponentsTextBox[0].Text}';";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(TemporaryRequest2, con);
                    string Temporary2 = cmd2.ExecuteScalar().ToString();
                    CurrentTemporaeyList.Add(Temporary2);
                    string TemporaryRequest3 = $"SELECT card_cvv FROM BasicInformationAboutCards WHERE card_id = '{CurrentComponentsTextBox[0].Text}';";
                    NpgsqlCommand cmd3 = new NpgsqlCommand(TemporaryRequest3, con);
                    string Temporary3 = cmd3.ExecuteScalar().ToString();
                    CurrentTemporaeyList.Add(Temporary2);
                    Destroy();
                    CreatingUserInterface();
                }
            }
        }
        private void Destroy() //Отчистка List содержащие UTElement  
        {
            CurrentComponentsTextBlock.Clear();
            ComponentsTheCurrentInterface.Clear();
            CurrentTemporaeyList.Clear();
        }
        private void CreatingUserInterface() //Создение интерфеса для пользователья после авторизации
        {
            Destroy();
            CreatingPicturesForUserInteface();
            CreatingBoxForUserInterface();
        }
        private void CreatingPicturesForUserInteface() //Создение Дизайна для Интерфейса пользователья
        {
            Rectangle BackGroundPictures = new Rectangle();
            BackGroundPictures.Width = 800; BackGroundPictures.Height = 50; BackGroundPictures.Fill = Brushes.LightPink;
            Canvas.SetLeft(BackGroundPictures,0); Canvas.SetTop(BackGroundPictures,0);
        }
        private void CreatingBoxForUserInterface() //Отрисовка данных в интерфейсе пользвателья
        {
            DatabaseQueryForUserInterface();
                TextBlock CurrentUserName = new TextBlock();
            Canvas.SetLeft(CurrentUserName,60); Canvas.SetTop(CurrentUserName,60);
            CurrentUserName.Text = ListForDifferentData[1];
            CurrentComponentsTextBlock.Add(CurrentUserName);canvas.Children.Add(CurrentUserName);
                TextBlock CurrentUserFam = new TextBlock();
            Canvas.SetLeft(CurrentUserFam, 100); Canvas.SetTop(CurrentUserFam, 60);
            CurrentUserName.Text = ListForDifferentData[2];
            CurrentComponentsTextBlock.Add(CurrentUserFam); canvas.Children.Add(CurrentUserFam);

            CurrentUser User = new CurrentUser(ListForDifferentData[0],
                ListForDifferentData[1], 
                ListForDifferentData[2],
                CurrentComponentsTextBox[0].Text, 
                CurrentComponentsTextBox[1].Text, 
                Convert.ToInt32(CurrentComponentsTextBox[2].Text));
        }
        private void DatabaseQueryForUserInterface()//Получение данных из базы данных
        {
            using (NpgsqlConnection con = GetConnection())//Проверка данных о карте
            {
                string correntcardid = CurrentComponentsTextBox[0].Text;
                con.Open();
                string TemporaryRequest1 = $"SELECT users.user_id FROM users LEFT JOIN basicinformationaboutcards ON users.user_id = basicinformationaboutcards.user_id WHERE basicinformationaboutcards.card_id = '{correntcardid}'";
                NpgsqlCommand cmd = new NpgsqlCommand(TemporaryRequest1, con);
                string Temporary1 = cmd.ExecuteScalar().ToString();
                CurrentTemporaeyList.Add(Temporary1); ListForDifferentData.Add(Temporary1);
                
                string TemporaryRequest2 = $"SELECT users.user_name FROM users LEFT JOIN basicinformationaboutcards ON users.user_id = basicinformationaboutcards.user_id WHERE basicinformationaboutcards.card_id = '{correntcardid}'";
                NpgsqlCommand cmd2 = new NpgsqlCommand(TemporaryRequest2, con);
                string Temporary2 = cmd.ExecuteScalar().ToString();
                CurrentTemporaeyList.Add(Temporary2); ListForDifferentData.Add(Temporary2);
                
                string TemporaryRequest3= $"SELECT users.user_fam FROM users LEFT JOIN basicinformationaboutcards ON users.user_id = basicinformationaboutcards.user_id WHERE basicinformationaboutcards.card_id = '{correntcardid}'";
                NpgsqlCommand cmd3 = new NpgsqlCommand(TemporaryRequest3, con);
                string Temporary3 = cmd.ExecuteScalar().ToString();
                CurrentTemporaeyList.Add(Temporary3); ListForDifferentData.Add(Temporary3);
            }
        }

    }
}
