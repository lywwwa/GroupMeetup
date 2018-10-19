using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using Mzsoft.BCrypt;

using Xamarin.Forms;
using GroupMeetup.Models;
using System.Diagnostics;
using GroupMeetup.Views;
using System.Threading.Tasks;

namespace GroupMeetup.Controllers
{
    public class UserController
    {
        WebClient client;
        public User currentUser;
        public void Login(string username, string password, string DeviceSerial, LoginPage login, GroupController gc)
        {
            client = new WebClient();
            password = Sha256(password, login);
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/login.php?username=" + username + "&password=" + password + "&deviceserial=" + DeviceSerial));
            Trace.WriteLine(response);
            if (response == "success")
            {
                currentUser = GetUserData(username, login);
                login.Navigation.PushAsync(new HomePage(this, gc));
            }
            else if (response == "loggedin")
            {
                login.DisplayAlert("Error", "You are logged in to another device. Log out from other devices and try again.", "Okay");
            }
            else if (response == "anotheruser")
            {
                login.DisplayAlert("Error", "This device is used by another user session. Log in with that user, log out and try again.", "Okay");
            }
            else if (response == "invalid") login.DisplayAlert("Invalid credentials", "Username or password is incorrect.", "Try again");
            else login.DisplayAlert("php error??", response, "Try again");

        }

        public void Logout(int UID, ProfilePage PPage)
        {
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/logout.php?id=" + UID));
            Trace.WriteLine(response);
            if (response == "success")
            {
                PPage.Navigation.PopToRootAsync();
            }
            else
            {
                PPage.DisplayAlert("Error", response, "Try again");
            }

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
                    signup.DisplayAlert("Success", "Account created.", "Log-in");
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

        public User GetUserDataById(int id)
        {
            User udata = new User();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/getUserDataById.php?id=" + id));
            string[] wow = response.Split('/');
            foreach(string w in wow)
            {
                if(w != "")
                {
                    udata.ID = Convert.ToInt32(wow[0]);
                    udata.FirstName = wow[1];
                    udata.LastName = wow[2];
                    udata.Username = wow[3];
                }
            }
            return udata;
        }

        public List<User> SearchUsers(string query, ContentPage page)
        {
            List<User> searchResult = new List<User>();
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/searchResult.php?searchQ=" + query));
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

        public async Task<List<User>> GetFriends(int id)
        {
            List<User> FriendsList = new List<User>();
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/populateFriends.php?id=" + id));
            string[] Friends = response.Split('\n');
            User toAdd;
            if (response != "none")
            {
                foreach (string u in Friends)
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
                        FriendsList.Add(toAdd);
                    }
                }
                return FriendsList;
            }
            else return null;
        }

        public async Task<List<int>> GetGroupMembers(int GroupId)
        {
            List<int> MembersList = new List<int>();
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/populateMembers.php?id=" + GroupId));
            if (response != "none")
            {
                Trace.WriteLine("response: " + response);
                string[] Friends = response.Split('\n');
                foreach (string f in Friends)
                {
                    if(int.TryParse(f, out int o)) MembersList.Add(o);
                }
                return MembersList;
            }
            else
            {
                Trace.WriteLine("responsen: " + response);
                return null;
            }
        }

        public async Task<List<int>> GetGroupMembersArrived(int GroupId)
        {
            List<int> ArrivedMembers = new List<int>();
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/getArrivedMembers.php?id=" + GroupId));
            if (response != "none")
            {
                Trace.WriteLine("response: " + response);
                string[] Friends = response.Split('\n');
                foreach (string f in Friends)
                {
                    if (int.TryParse(f, out int o)) ArrivedMembers.Add(o);
                }
                return ArrivedMembers;
            }
            else
            {
                Trace.WriteLine("responsen: " + response);
                return null;
            }
        }

        public string GetUserConnection(int U1, int U2)
        {
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/friendConnection.php?user1=" + U1 +"&user2="+U2));
            return response;
        }

        public string GetUserLatestAction(int U1, int U2)
        {
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/friendLatestAction.php?user1=" + U1 + "&user2=" + U2));
            return response;
        }

        public bool ManageConnection(User U1, User U2, Button Action, Button RejAction)
        {
            string ActionText = "";
            if (Action.Text.Contains("Add")) ActionText = "add";
            else if (Action.Text.Contains("Cancel")) ActionText = "cancel";
            else if (Action.Text.Contains("Remove")) ActionText = "remove";
            else if (Action.Text.Contains("Accept")) ActionText = "accept";
            else if (Action.Text.Contains("Reject")) ActionText = "reject";
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
                else if (Action.Text.Contains("Accept"))
                {
                    Action.Text = "Remove " + U2.Username + "from Friends List";
                    RejAction.IsVisible = false;
                }
                else
                {
                    RejAction.Text = "Add " + U2.Username + " as Friend";
                    Action.IsVisible = false;
                }
                return true;
            }
            else
            {
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
                //form.DisplayAlert("", builder.ToString(), "okay");
                return builder.ToString();
            }
        }
    }
}
