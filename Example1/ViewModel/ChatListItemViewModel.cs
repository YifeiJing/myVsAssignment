
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Example1
{
    public class ChatListItemViewModel : BaseViewModel
    {
        #region private members

        private static Random mrandom = new Random();

        #endregion
        /// <summary>
        /// The display name of this chat list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The latest messgae from this chat list
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The initials determine the profile background color
        /// </summary>
        public string Initials { get; set; }

        public string ProfilePictureRGB { get; set; }

        public bool NewContentAvaliable { get; set; }

        public bool IsSelected { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Rank { get; set; }

        public int Time { get; set; }

        /// <summary>
        /// The data of rgb values
        /// </summary>
        public string[] rgbarray = { "3099c5", "fe4503", "ffa800", "ac1680", "08f2c5", "b9e5c7", "5cff05", "bc6af4",
        "ee5c9f", "c59aaa", "ef5678"};

        #region ctor

        public ChatListItemViewModel()
        {

        }

        public ChatListItemViewModel(string name, string rank, int time, string ID)
        {
            Name = name;
            Rank = rank;
            Time = time;
            Initials = ID;
            ProfilePictureRGB = rgbarray[mrandom.Next(rgbarray.Length)];
        }

        #endregion
    }
}
