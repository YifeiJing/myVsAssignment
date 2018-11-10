namespace Example1
{
     public class ChatListItemDesignModel : ChatListItemViewModel
    {
        #region SingleTon
        public static ChatListItemDesignModel Instance { get; } = new ChatListItemDesignModel();
        #endregion
        public ChatListItemDesignModel()
        {
            Name = "Yifei";
            Initials = "YF";
            Message = "This new UI is pretty and beautiful!";
            ProfilePictureRGB = "3099c5";
        }
    }
}
