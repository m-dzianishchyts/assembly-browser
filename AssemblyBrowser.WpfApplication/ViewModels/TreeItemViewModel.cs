using System.Collections.Specialized;
using AssemblyBrowser.WpfApplication.TreeItem;

namespace AssemblyBrowser.WpfApplication.ViewModels;

public abstract class TreeItemViewModel : LabeledTreeItem, INotifyCollectionChanged
{
    protected TreeItemViewModel(string label) : base(label)
    {
        ChildrenInner.CollectionChanged += CollectionChanged;
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
}