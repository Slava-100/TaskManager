using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class CollectionUserServices
    {
        private static CollectionUserServices _instance;

        public Dictionary<long, UserService> _userService;

        public CollectionUserServices()
        {
            _userService = new Dictionary<long, UserService>();
        }

        public static CollectionUserServices GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CollectionUserServices();
            }
            return _instance;
        }
    }
}
