using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Mzsoft.BCrypt;

namespace GroupMeetup.Controllers
{
    public class UserController
    {
        WebClient client;
        public void login(string username, string password, LoginPage login)
        {
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/login.php?username=" + username + "&password=" + password));
            if (response == "success")
            {
                login.Navigation.PushAsync(new HomePage());
            }
            else login.DisplayAlert("Invalid credentials", "Username or password is incorrect.", "Try again");

        }

        public void signup(string username, string password, string passwordRepeat, SignUpPage signup)
        {
            if (password == passwordRepeat)
            {
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
    }
}
