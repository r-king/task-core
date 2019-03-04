namespace TaskCore.Wpf
{
	public class PopUpViewModel : BindableBase
	{
		private BindableBase _currentViewModel;

		public BindableBase CurrentViewModel
		{
			get
			{
				return _currentViewModel;
			}
			set
			{
				SetProperty(ref _currentViewModel, value);
			}
		}
	}
}