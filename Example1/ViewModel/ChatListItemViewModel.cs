
namespace Example1
{
    public class ChatListItemViewModel : BaseViewModel
    {
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
    }
}
