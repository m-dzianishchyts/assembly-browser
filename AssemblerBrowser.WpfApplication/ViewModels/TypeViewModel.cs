using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AssemblerBrowser.Core.Entities;

namespace AssemblerBrowser.WpfApplication.ViewModels
{
    public class TypeViewModel
    {
        private IEnumerable<MemberViewModel> _members;

        private string _name;

        public TypeViewModel(TypeInformation typeInformation)
        {
            Name = typeInformation.Name;
            var memberPropertyView = typeInformation.Properties.Select(property => new MemberViewModel(property));
            var memberMethodView = typeInformation.Methods.Select(method => new MemberViewModel(method));
            var memberFieldView = typeInformation.Fields.Select(field => new MemberViewModel(field));
            Members = memberFieldView.Concat(memberPropertyView).Concat(memberMethodView);
        }


        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<MemberViewModel> Members
        {
            get => _members;
            set
            {
                _members = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
