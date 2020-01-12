using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ASG.Objects;
using ASG.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASG.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public string imageUrl;
        public int Gender;
        private Services.AuthenticationService _service = new Services.AuthenticationService();
        private ApiUserService _service2;
        public static string Token;
        private StorageFile photo;
        public Login()
        {
            this.InitializeComponent();
            this._service2 = new ApiUserService();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Email_Text_Login.Text.Length == 0)
            {
                Login_Error.Text = "Email is required!";
                return;
            }
            if (Password_Text_Login.Password.Length == 0)
            {
                Login_Error.Text = "Password is required!";
                return;
            }
            var email = Email_Text_Login.Text;
            var password = Password_Text_Login.Password;
            Token = await this._service.LoginTask(email, password);

            Debug.WriteLine("Token: "+Token);
        }

        
        private async void Shoot_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var uploadUrl = client.GetAsync("https://2-dot-backup-server-003.appspot.com/get-upload-token").Result.Content.ReadAsStringAsync().Result;

            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                return;
            }

            HttpUploadFile(uploadUrl, "myFile", "image/png");
            ImageUrl.Text = "Waiting...";
        }

        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            Stream fileStream = await this.photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                imageUrl = reader2.ReadToEnd();
                ImageControl.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                ImageUrl.Text = "Done!";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                ImageUrl.Text = "Error";
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        private void Gender_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string GenderName = rb.Tag.ToString();
                switch (GenderName)
                {
                    case "Male":
                        Gender = 1;
                        break;
                    case "Female":
                        Gender = 0;
                        break;
                    case "Other":
                        Gender = 2;
                        break;
                    default:
                        break;
                }
            }
        }

            private void Register_Click(object sender, RoutedEventArgs e)
        {
            var date = birthday.Date;
            DateTime time = date.Value.DateTime;
            var endBirthday = time.ToString("yyyy-MM-dd");
            var user = new User()
            {
                firstName = FirstName_Text.Text,
                lastName = LastName_Text.Text,
                avatar = imageUrl,
                address = Address_Text.Text,
                introduction = Intro_Text.Text,
                phone = Phone_Text.Text,
                gender = Gender,
                birthday = endBirthday,
                email = Email_Text_Register.Text,
                password = Password_Text_Register.Password
            };
            var errors = user.CheckValidate();
            if (errors.Count > 0)
            {
                Register_Error.Text = errors.Values.ToString();
            }
            _service2.Create(user);
        }
    }
}
