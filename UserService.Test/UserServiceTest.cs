namespace UserService.Test
{
    public class UserServiceTest : IClassFixture<UserServiceFixture>
    {
        private UserServiceFixture _fixture;

        public UserServiceTest( UserServiceFixture fixture)
        {
            _fixture = fixture;
        }
        //To add *normal* tests I have to swap computers, so I'll do it later
        [Theory]
        [InlineData("","","", )]//how to pass DateTime in InlineData? че за дауны это делали
        public void AddUser_InputIncorrectValues_ShouldReturnFalseBecauseOfValidation(string firstName, string lastName, string email, DateTime dateOfBirth)
        {

        }
    }
}
