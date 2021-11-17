namespace AssemblyBrowser.WpfApplication.TreeItem;

public abstract class LabeledTreeItem : BaseTreeItem
{
    protected LabeledTreeItem(string label)
    {
        Label = label;
    }

    public string Label { get; }
}