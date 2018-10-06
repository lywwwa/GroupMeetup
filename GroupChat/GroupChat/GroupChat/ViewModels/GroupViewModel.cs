using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GroupChat.ViewModels
{
    public class GroupViewModel : INotifyPropertyChanged
    {
        public string strGroupID;
        public string strGroupName;
        public string strGroupSubName;

        public event PropertyChangedEventHandler PropertyChanged;

        public GroupViewModel(string strGroupID, string strGroupName, string strGroupSubName)
        {
            this.strGroupID = strGroupID ?? throw new ArgumentNullException(nameof(strGroupID));
            this.strGroupName = strGroupName ?? throw new ArgumentNullException(nameof(strGroupName));
            this.strGroupSubName = strGroupSubName ?? throw new ArgumentNullException(nameof(strGroupSubName));
        }

        public string GroupID
        {
            set
            {
                if (strGroupID != value)
                {
                    strGroupID = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GroupID"));
                }
            }
            get
            {
                return strGroupID;
            }
        }

        public string GroupLabel => strGroupName[0].ToString();

        public string GroupName
        {
            set
            {
                if (strGroupName != value)
                {
                    strGroupName = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GroupName"));
                }
            }
            get
            {
                return strGroupName;
            }
        }

        public string GroupSubName
        {
            set
            {
                if (strGroupSubName != value)
                {
                    strGroupSubName = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GroupSubName"));
                }
            }
            get
            {
                return strGroupSubName;
            }
        }
    }
}
