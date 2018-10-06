using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using Mzsoft.BCrypt;

using Xamarin.Forms;
using GroupMeetup.Models;
using System.Diagnostics;

namespace GroupMeetup.Controllers
{
    public class UserController
    {
        WebClient client;
        public User currentUser;
        public void Login(string username, string password, LoginPage login)
        {
            client = new WebClient();
            password = Sha256(password, login);
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/login.php?username=" + username + "&password=" + password));
            Trace.WriteLine(response);
            if (response == "success")
            {
                currentUser = GetUserData(username, login);
                login.Navigation.PushAsync(new HomePage(this, currentUser));
            }
            else login.DisplayAlert("Invalid credentials", "Username or password is incorrect.", "Try again");

        }

        public void Signup(string username, string password, string passwordRepeat, string firstName, string lastName, SignUpPage signup)
        {
            if (password == passwordRepeat)
            {
                password = Sha256(password, signup);
                string hash = BCrypt.HashPassword(password, BCrypt.GenerateSalt());
                client = new WebClient();
                string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/signup.php?username=" + username + "&password=" + hash + "&firstname=" + firstName + "&lastname="+ lastName));
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
                string[] wow = response.Split('/');
                udata.ID = Convert.ToInt32(wow[0]);
                udata.FirstName = wow[1];
                udata.LastName = wow[2];
                udata.Username = wow[3];
            }
            else page.DisplayAlert("", "User not found", "k");
            return udata;
        }

        public List<User> SearchUsers(string query, ContentPage page)
        {
            List<User> searchResult = new List<User>();
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/searchResult.php?searchQ=" + query));
            //page.DisplayAlert("",response,"nice");
            string[] user = response.Split('\n');
            User toAdd;
            foreach (string u in user)
            {
                string[] eachUser = u.Split('/');
                if (u != "")
                { 
                    toAdd = new User
                    {
                        ID = Convert.ToInt32(eachUser[0]),
                        FirstName = eachUser[1],
                        LastName = eachUser[2],
                        Username = eachUser[3]
                    };
                    searchResult.Add(toAdd);
                }
            }
            /*if (searchResult.Count > 0) searchResult.Add("End of results");
            else searchResult.Add("No search results");
            string[] searchResultString = searchResult.ToArray();*/

            //return searchResultString;
            return searchResult;
        }

        public int GetUserConnection(int U1, int U2)
        {
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/friendConnection.php?user1=" + U1 +"&user2="+U2));
            return Convert.ToInt32(response);
        }

        public int GetUserLatestAction(int U1, int U2)
        {
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/friendLatestAction.php?user1=" + U1 + "&user2=" + U2));
            return Convert.ToInt32(response);
        }

        public bool ManageConnection(User U1, User U2, Button Action, ContentPage page)
        {
            string ActionText = "";
            if (Action.Text.Contains("Add")) ActionText = "add";
            else if (Action.Text.Contains("Cancel")) ActionText = "cancel";
            else if (Action.Text.Contains("Remove")) ActionText = "remove";

            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/manageConnection.php?user1=" + U1.ID + "&user2=" + U2.ID + "&action=" + ActionText));
            if (response == "success")
            {
                if (Action.Text.Contains("Add"))
                {
                    Action.Text = "Cancel friend request";
                }
                else if (Action.Text.Contains("Cancel") || Action.Text.Contains("Remove"))
                {
                    Action.Text = "Add " + U2.Username + " as Friend";
                }
                return true;
            }
            else
            {
                page.DisplayAlert("", "Error", "Okay");
                return false;
            }
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
