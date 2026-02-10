using System.Text.RegularExpressions;

namespace Domain
{
    //Expresión regular para correo: @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
    public class PersonEntity
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; } = String.Empty;
        public string FirstName { get; private set; } = String.Empty;
        public string LastName { get; private set; } = String.Empty;
        public string Email { get; private set; } = String.Empty;
        public string PhoneNumber { get; private set; } = String.Empty;

        public string FullName => $"{FirstName} {LastName}".Trim();

        public PersonEntity(string code, string firstName, string lastName, string email, string phoneNumber)
        {
            ValidateCode(code);
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidateEmail(email);

            Id = Guid.NewGuid();
            Code = code.Trim().ToUpper();
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber.Trim();
        }

        public void UpdatePersonalInfo(string firstName, string lastName, string email, string phoneNumber)
        {

            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidateEmail(email);

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber.Trim();
        }

        private void ValidateCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code cannot be empty.");
            if (code.Length < 3)
                throw new ArgumentException("Code cannot minor than 3 characters.");
            if (code.Length > 10)
                throw new ArgumentException("Code cannot exceed 10 characters.");
        }

        private void ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");
            if (firstName.Length < 2)
                throw new ArgumentException("First name cannot minor than 2 characters.");
            if (firstName.Length > 50)
                throw new ArgumentException("First name cannot exceed 50 characters.");
        }

        private void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");
            if (lastName.Length < 2)
                throw new ArgumentException("Last name cannot minor than 2 characters.");
            if (lastName.Length > 50)
                throw new ArgumentException("Last name cannot exceed 50 characters.");
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))                                      
                throw new ArgumentException("Invalid email format.");
        }
    }
}
