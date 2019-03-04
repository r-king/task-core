using System;
using System.Net;
using System.Text.RegularExpressions;
using TaskCore.Data;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Wpf.Links
{
	public class AddEditLinkViewModel : BindableBase
	{
		public ILinkRepository linkRepository;

		public AddEditLinkViewModel(ILinkRepository linkRepository)
		{
			this.linkRepository = linkRepository;

			CancelCommand = new RelayCommand(Close);
			AddLinkCommand = new RelayCommand(AddLink);
			GetLinkNameCommand = new RelayCommand(GetLinkName);
		}

		public AddEditLinkViewModel()
		{
		}

		private void GetLinkName()
		{
			try
			{
				WebClient x = new WebClient();
				string source = x.DownloadString(Link.Url);
				string name = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
				Link.Name = Regex.Replace(name, @"\r\n?|\n", "");
			}
			catch (Exception)
			{
				//TODO: handle exception
			}
		}

		private async void AddLink()
		{
			await linkRepository.Add(Link);
			Done();
		}

		private void Close()
		{
			Done();
		}

		private Link link;

		public Link Link
		{
			get { return link; }
			set { SetProperty(ref link, value); }
		}

		public RelayCommand CancelCommand { get; set; }
		public RelayCommand AddLinkCommand { get; set; }
		public RelayCommand GetLinkNameCommand { get; set; }

		public event Action Done = delegate { };
	}
}