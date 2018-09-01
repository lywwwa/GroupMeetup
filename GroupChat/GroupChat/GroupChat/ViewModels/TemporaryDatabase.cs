using System.Collections.ObjectModel;

namespace GroupChat.ViewModels
{
    static class TemporaryDatabase
    {
        public static ObservableCollection<GroupViewModel> DefaultGroups 
            = new ObservableCollection<GroupViewModel>{
                new GroupViewModel("G0", "Clemente", "Is a family of Jesy and Reymar"),
                new GroupViewModel("G1", "Unix", "Is a family of Multitasking"),
                new GroupViewModel("G2", "Posix", "Is a family of Standards"),
                new GroupViewModel("G3", "Linux", "Is a family of Free"),
                new GroupViewModel("G4", "Microsoft", "Is a family of Privacy"),
                new GroupViewModel("G5", "Apple", "Is a family of Paid"),
                new GroupViewModel("G6", "Steam", "Is a family of Games"),
                new GroupViewModel("G7", "Mobile Development", "Mobile Development"),
                new GroupViewModel("G8", "Game Development", "Game Development"),
            };
    }
}
