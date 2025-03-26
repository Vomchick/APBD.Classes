namespace UserService.Test
{
    public class UserServiceFixture : IDisposable
    {
        public LegacyApp.UserService Service { get; }

        public UserServiceFixture(LegacyApp.UserService service)
        {
            Service = service;
        }

        public void Dispose() { }
    }
}
