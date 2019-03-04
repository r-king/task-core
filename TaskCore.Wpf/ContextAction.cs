namespace TaskCore.Wpf
{
	/// <summary>
	/// Context Menu Action
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ContextAction<T> : BindableBase
	{
		public string Name { get; set; }
		public RelayCommand<T> Action { get; set; }
	}
}