using System.Collections.ObjectModel;
using LoginDemo.UI.Windows;

namespace REB.UI
{
    internal enum WindowName
    {
        Login,
        Admin,
        Customer,
        BankAccount,
        MoneyTransfer,
        CreateAccount,
        Logout
    }

    internal static class WindowManager
    {
        private static Stack<IWindow?> _windowStack = new();
        private static readonly Dictionary<WindowName, IWindow> windowDictionary = new();
        public static int Level => _windowStack.Count;

        public static ReadOnlyDictionary<WindowName, IWindow> WindowDictionary;

        /// <summary>
        /// Keeps all windows that the application will use.
        /// </summary>
        static WindowManager()
        {
            windowDictionary.Add(WindowName.Login, new LoginWindow());
            windowDictionary.Add(WindowName.Admin, new AdminWindow());
            windowDictionary.Add(WindowName.Customer, new CustomerLandingWindow());
            windowDictionary.Add(WindowName.BankAccount, new BankAccountWindow());
            windowDictionary.Add(WindowName.MoneyTransfer, new MoneyTransferWindow());
            windowDictionary.Add(WindowName.CreateAccount, new CreateAccountWindow());
            WindowDictionary = new ReadOnlyDictionary<WindowName, IWindow>(windowDictionary);
        }

        /// <summary>
        /// Method used for debugging purposes.
        /// Shows the windows layered on each other from top to bottom.
        /// </summary>
        public static void showWindowStack()
        {
            foreach (var window in _windowStack)
            {
                Console.WriteLine(window?.GetType());
            }
        }

        /// <summary>
        /// Switches to another IWindow. The 'from' window is put on the stack,
        /// so when the show method in IWindow returns, it will pop and you will be in
        /// this window.
        /// </summary>
        /// <param name="from">The IWindow you are in now.</param>
        /// <param name="to">The IWindow you will jump to.</param>
        public static void Navigate(IWindow? from, IWindow to)
        {
            _windowStack.Push(from);
            to?.Show();
            // show method return here.
            _windowStack.Pop()?.Show();
        }
    }
}
