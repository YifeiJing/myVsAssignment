using System.Collections.Generic;

namespace Example1
{
     public class ChatListDesignModel : ChatListViewModel
    {
        #region SingleTon
        public static ChatListDesignModel Instance { get; } = new ChatListDesignModel();
        #endregion
        public ChatListDesignModel()
        {
            Items = new List<ChatListItemViewModel>();
            DataBaseControl db = new DataBaseControl();
            //{
            //    new ChatListItemViewModel
            //    {
            //        Name = "Yifei",
            //        Initials = "YF",
            //        Message = "99999",
            //        ProfilePictureRGB = "3099c5",
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Name = "Jesse",
            //        Initials = "JA",
            //        Message = "88888",
            //        ProfilePictureRGB = "fe4503"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Name = "Parnell",
            //        Initials = "PL",
            //        Message = "77777",
            //        ProfilePictureRGB = "ffa800"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Name = "Parnell",
            //        Initials = "PL",
            //        Message = "66666",
            //        ProfilePictureRGB = "ac1680",
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Name = "Parnell",
            //        Initials = "PL",
            //        Message = "55555",
            //        ProfilePictureRGB = "fa90ef"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Name = "Parnell",
            //        Initials = "PL",
            //        Message = "44444",
            //        ProfilePictureRGB = "08f2c5"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Name = "Parnell",
            //        Initials = "PL",
            //        Message = "33333",
            //        ProfilePictureRGB = "b9e5c7"
            //    },

            //};

            for (int i = 0; i != 16; i++)
            {
                Items.Add(new ChatListItemViewModel(db.access.Names[i], db.access.Ranks[i], db.access.Times[i], db.access.IDs[i].ToString()));
            }
        }
    }
}
