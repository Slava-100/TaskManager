namespace TaskManager
{
    public class ClientServicesCollection
    {
        private static ClientServicesCollection _instance;

        public Dictionary<long, ClientService> _userService;

        private ClientServicesCollection()
        {
            _userService = new Dictionary<long, ClientService>();
        }

        public static ClientServicesCollection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ClientServicesCollection();
            }

            return _instance;
        }
    }
}
