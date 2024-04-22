namespace CourseManagementAPI.DTOs
{
    public class UserDto
    {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }

        public UserDto()
        {
            if (FirstName == null)
            {
                FirstName = "";
            }

            if (LastName == null)
            {
                LastName = "";
            }

            if (Gender == null)
            {
                Gender = "";
            }

            if (Email == null)
            {
                Email = "";
            }

            if (Username == null)
            {
                Username = "";
            }

            if (Password == null)
            {
                Password = "";
            }

            if (ConfirmPassword == null)
            {
                ConfirmPassword = "";
            }
        }

    }
}
