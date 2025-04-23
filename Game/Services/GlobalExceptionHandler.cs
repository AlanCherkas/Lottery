namespace Game.Services
{
	internal static class GlobalExceptionHandler
	{
		public static void Register()
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
		}

		static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
		{
			Console.WriteLine("Something went wrong. Please contact the application administrator. Press Enter to continue.");
			Console.ReadLine();
		}
	}
}
