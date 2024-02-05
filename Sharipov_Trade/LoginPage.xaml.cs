using System;
using System.Collections.Generic;
using System.Drawing;
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
using static System.Net.Mime.MediaTypeNames;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using System.Drawing.Imaging;
using System.IO;
using Image = System.Windows.Controls.Image;
using System.Windows.Interop;

namespace Sharipov_Trade
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
                
        private string captcha = String.Empty;

               
        private Bitmap CreateCaptcha(int Height, int Width)
        {
            Random rnd = new Random();
            Bitmap result = new Bitmap(Height, Width);
            int Xpos = rnd.Next(0, Width - 50);
            int Ypos = rnd.Next(15, Height - 15);

            
            System.Drawing.Brush[] colors = { System.Drawing.Brushes.Black,
                     System.Drawing.Brushes.Red,
                     System.Drawing.Brushes.RoyalBlue,
                     System.Drawing.Brushes.Green };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);
            g.Clear(System.Drawing.Color.Gray);

            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                captcha += ALF[rnd.Next(ALF.Length)];

            g.DrawString(captcha,
                new Font("Arial", 15),
                colors[rnd.Next(colors.Length)],
                new PointF(Xpos, Ypos));
            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));
            ////Белые точки
            /*
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);*/


            result.Save("img.png", System.Drawing.Imaging.ImageFormat.Png);
            return result;
        }
        public Image ConvertBitmapToImage(Bitmap bitmap)
        {
            // Создаем новый объект BitmapImage
            BitmapImage bitmapImage = new BitmapImage();

            // Создаем новый поток для загрузки изображения
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Сохраняем Bitmap в поток в формате PNG
                bitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Position = 0;

                // Загружаем изображение из потока в BitmapImage
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            // Создаем новый объект Image
            Image image = new Image();
            image.Source = bitmapImage;
            return image;
        }
            private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //CapchaImage = ConvertBitmapToImage(CreateCaptcha(Convert.ToInt32(CapchaImage.Width), Convert.ToInt32(CapchaImage.Height)));
            //CapchaImage = ConvertBitmapToImage(CreateCaptcha(350,100));
            var currentUser = SharipovEntities.GetContext().User.ToList();
            currentUser = currentUser.Where(p => p.UserLogin == LoginTB.Text && p.UserPassword == PasvordTB.Text).ToList();
            int userID = 0;
            foreach (User user in currentUser)
            {
                userID = user.UserID;
            }
            if (currentUser.Count == 0)
            {

            }
            else if (currentUser.Count == 1)
            {
                MainClass.MainFrame.Navigate(new ProductPage(userID - 1));
            }
        }
        
        private void VizitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainClass.MainFrame.Navigate(new ProductPage(-1));
        }
    }
}
