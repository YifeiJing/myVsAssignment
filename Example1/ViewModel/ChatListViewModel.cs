using System.Collections.Generic;

namespace Example1
{
    public class ChatListViewModel : BaseViewModel
    {
        public List<ChatListItemViewModel> Items { get; set; }

        public ChatListViewModel()
        {
            Items = new List<ChatListItemViewModel>();
            DataBaseControl db = new DataBaseControl();
            for (int i = 0; i != 16; i++)
            {
                Items.Add(new ChatListItemViewModel(db.access.Names[i], db.access.Ranks[i], db.access.Times[i], db.access.IDs[i].ToString()));
            }
        }
    }
}
