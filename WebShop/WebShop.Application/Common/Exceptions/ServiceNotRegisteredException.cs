namespace WebShop.Application.Common.Exceptions {
    public class ServiceNotRegisteredException : Exception {
        public ServiceNotRegisteredException(string serviceName) :
            base($"Service \"{serviceName}\" not registered") { }
    }
}
