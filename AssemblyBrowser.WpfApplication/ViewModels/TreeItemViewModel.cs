using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyBrowser.WpfApplication.TreeItem;

namespace AssemblyBrowser.WpfApplication.ViewModels
{
    public abstract class TreeItemViewModel : LabeledTreeItem, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected TreeItemViewModel(string label) : base(label)
        {
            ChildrenInner.CollectionChanged += CollectionChanged;
        }
    }
}