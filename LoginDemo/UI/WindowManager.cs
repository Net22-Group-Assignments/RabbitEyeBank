using System.Collections.ObjectModel;
using LoginDemo.UI.Windows;

namespace LoginDemo.UI
{
    internal enum WindowName
    {
        Login,
        Admin,
        CreateCustomer,
        Currency,
        TransferControl,
        BankAccount,
        BankAccountDetails,
        MoneyTransfer,
        CreateAccount,
        Logout,
        Exit
    }

    internal static class WindowManager
    {
        private static Stack<IWindow?> _windowStack = new();
        private static readonly Dictionary<WindowName, IWindow> windowDictionary = new();
        public static int Level => _windowStack.Count;

        public static ReadOnlyDictionary<WindowName, IWindow> Windows;

        /// <summary>
        /// Keeps all windows that the application will use.
        /// </summary>
        static WindowManager()
        {
            windowDictionary.Add(Login, new LoginWindow());
            windowDictionary.Add(Admin, new AdminWindow());
            windowDictionary.Add(CreateCustomer, new CreateCustomerWindow());
            windowDictionary.Add(Currency, new CurrencyWindow());
            windowDictionary.Add(TransferControl, new TransferControlWindow());
            windowDictionary.Add(BankAccount, new BankAccountWindow());
            windowDictionary.Add(BankAccountDetails, new BankAccountDetailsWindow());
            windowDictionary.Add(MoneyTransfer, new MoneyTransferWindow());
            windowDictionary.Add(CreateAccount, new CreateAccountWindow());
            Windows = new ReadOnlyDictionary<WindowName, IWindow>(windowDictionary);
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
