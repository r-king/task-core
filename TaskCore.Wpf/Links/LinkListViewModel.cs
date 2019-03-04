using System.Collections.ObjectModel;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Links
{
	public class LinkListViewModel : BindableBase
	{
		public ILinkRepository linkRepository { get; set; }

		public LinkListViewModel(ILinkRepository linkRepository)
		{
			this.linkRepository = linkRepository;
			DeleteLinkCommand = new RelayCommand<Link>(DeleteLink);
		}

		private ObservableCollection<Link> links;

		public ObservableCollection<Link> Links
		{
			get { return links; }
			set { SetProperty(ref links, value); }
		}

		public RelayCommand<Link> DeleteLinkCommand { get; set; }

		private void DeleteLink(Link obj)
		{
			Links.Remove(obj);
			linkRepository.Delete(obj.Id);
		}
	}
}