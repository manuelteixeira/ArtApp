using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArtApp.Controls
{
    public class GroupList<TKey, TItem> : ObservableCollection<TItem>
    {
        public TKey Key { get; set; }

        public GroupList(TKey key, IEnumerable<TItem> items)
        {
            this.Key = key;
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }
    }
}
