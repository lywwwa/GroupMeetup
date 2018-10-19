using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GroupMeetup.Models;
using GroupMeetup.Views;

namespace GroupMeetup.Controllers
{
    public class GroupController
    {
        WebClient client;

        public GroupController()
        {

        }

        public async void AddGroup(Group group, TabbedPages.AdditionalPages.AddEventPage AEP)
        {
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/manageGroup.php?id=" + group.Id+ "&uid=" + group.UserID + "&name="+group.Name +"&location=" + group.MeetupLocation + "&lat=" + group.MeetupLocationLat + "&lon="+ group.MeetupLocationLon +"&dt="+group.MeetupDateTime+"&dte="+group.MeetupDateTimeEnd+"&placeid="+group.MeetupLocationId));
            if (response == "create")
            {
                await AEP.DisplayAlert("Success", "Group created.", "Okay");
                await AEP.Navigation.PopAsync();
            }
            else if (response == "update")
            {
                await AEP.DisplayAlert("Success", "Group updated.", "Okay");
                await AEP.Navigation.PopAsync();
            }
            else await AEP.DisplayAlert("Error", response, "Try again");
        }

        public async void AddRemoveMember(int GroupId, int IdToAdd, string Method, AddRemoveFriendsEvent ARFE, string senderUName, int senderId)
        {
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/addRemoveMember.php?gid=" + GroupId + "&uid=" + IdToAdd + "&method=" + Method+ "&uidsend="+senderId + "&unamesend="+senderUName));
            if (response == "success")
            {
                if (Method == "add") await ARFE.DisplayAlert("Success", "User added to Participants", "OK");
                else if (Method == "remove") await ARFE.DisplayAlert("Success", "User removed from Participants", "OK");
            }
        }

        public async Task<List<Group>> GetGroups(int id, string group)
        {
            List<Group> GList = new List<Group>();
            client = new WebClient();
            string response = client.DownloadString(new Uri("https://meetup-app.000webhostapp.com/populateGroups.php?id="+id+"&groups="+group));
            string[] Grps = response.Split('\n');
            Group toAdd;
            foreach (string n in Grps)
            {
                string[] eachGrp = n.Split('/');
                if (n != "")
                {
                    toAdd = new Group
                    {
                        Id = Convert.ToInt32(eachGrp[0]),
                        UserID = Convert.ToInt32(eachGrp[1]),
                        Name = eachGrp[2],
                        MeetupLocationId = eachGrp[3],
                        MeetupLocation = eachGrp[4],
                        MeetupLocationLat = Convert.ToDouble(eachGrp[5]),
                        MeetupLocationLon = Convert.ToDouble(eachGrp[6]),
                        MeetupDateTime = Convert.ToDateTime(eachGrp[7]),
                        MeetupDateTimeEnd = Convert.ToDateTime(eachGrp[8])
                    };
                    GList.Add(toAdd);
                }
            }
            return GList;
        }

        
    }
}
