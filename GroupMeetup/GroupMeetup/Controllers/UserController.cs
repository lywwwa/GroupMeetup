using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using Mzsoft.BCrypt;

using Xamarin.Forms;
using GroupMeetup.Models;

namespace GroupMeetup.Controllers
{
    public class UserController
    {
        WebClient client;
        public void Login(string username, string password, LoginPage login)
        {
            client = new WebClient();
            password = Sha256(password, login);
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/login.php?username=" + username + "&password=" + password));
            if (response == "success")
            {
                login.Navigation.PushAsync(new HomePage(this));
            }
            else login.DisplayAlert("Invalid credentials", "Username or password is incorrect.", "Try again");

        }

        public void Signup(string username, string password, string passwordRepeat, SignUpPage signup)
        {
            if (password == passwordRepeat)
            {
                password = Sha256(password, signup);
                string hash = BCrypt.HashPassword(password, BCrypt.GenerateSalt());
                client = new WebClient();
                string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/signup.php?username=" + username + "&password=" + hash));
                if (response == "success")
                {
                    signup.DisplayAlert("Success", response, "Log-in");
                    signup.Navigation.PopToRootAsync();
                }
                else signup.DisplayAlert("Error", response, "Try again");
            }
            else signup.DisplayAlert("Error", "Passwords do not match.", "Try again");
        }

        public User GetUserData(string username, ContentPage page)
        {
            User udata = new User();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/getUserData.php?username=" + username));
            if (response.Contains(username))
            {
                page.DisplayAlert("", response, "k");
                string[] wow = response.Split('/');
                udata.firstName = wow[0];
                udata.lastName = wow[1];
                udata.username = wow[2];
            }
            else page.DisplayAlert("", "User not found", "k");
            return udata;
        }

        public static string Sha256(string plainText, ContentPage form)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                form.DisplayAlert("", builder.ToString(), "okay");
                return builder.ToString();
            }
        }
    }
}
