using System.Collections.ObjectModel;

namespace AssemblyBrowser.WpfApplication.TreeItem;

public abstract class BaseTreeItem
{
    protected ObservableCollection<BaseTreeItem> ChildrenInner;

    protected BaseTreeItem()
    {
        ChildrenInner = new ObservableCollection<BaseTreeItem>();
    }

    public ObservableCollection<BaseTreeItem> Children => ChildrenInner;
}