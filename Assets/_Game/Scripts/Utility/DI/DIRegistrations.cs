using System;

namespace DI
{
    public  class DIRegistrations
    {
        public Func<DIСontainer, object> Factory { get; set; }
        public bool IsSingleton { get; set; }
        public object Instance { get; set; }
    }
}