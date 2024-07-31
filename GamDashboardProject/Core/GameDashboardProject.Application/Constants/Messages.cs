namespace GameDashboardProject.Application.Constants
{
    public static class Messages
    {
        // General Messages
        public static readonly string Actionful = "Action completed successfully.";
        public static readonly string ActionFailed = "Action failed. Please try again.";
        public static readonly string UnauthorizedAccess = "Unauthorized access.";

        // User-related Messages
        public static readonly string UserNotFound = "The user could not be found. Please check the username and try again.";
        public static readonly string UserAlreadyExists = "A user with the same username or email already exists. Please try logging in or use a different email address.";
        public static readonly string InvalidUsernameOrPassword = "The username or password you entered is incorrect. Please check and try again.";
        public static readonly string Registrationful = "You have registered successfully. Welcome!";
        public static readonly string RegistrationFailed = "Registration failed due to an unexpected error. Please try again later.";
        public static readonly string Loginful = "Login successful. Welcome back!";
        public static readonly string LoginFailed = "Login failed. Please check your credentials and try again.";

        // New User-related Messages
        public static readonly string DuplicateUserName = "This username is already taken. Please choose a different username.";
        public static readonly string DuplicateEmail = "This email address is already registered. Please use a different email address or try logging in.";
        public static readonly string PasswordsDoNotMatch = "The passwords you entered do not match. Please re-enter and try again.";
        public static readonly string InvalidUserName = "The username contains invalid characters. Please use only letters, numbers, and basic punctuation.";
        public static readonly string PasswordTooWeak = "The password is too weak. It must include at least one uppercase letter, one lowercase letter, one number, and one special character.";
        public static readonly string AccountLocked = "Your account has been locked due to multiple failed login attempts. Please try again later or contact support.";

        // Building-related Messages
        public static readonly string BuildingNotFound = "The specified building could not be found.";
        public static readonly string BuildingAlreadyExists = "A building with the same name or type already exists.";
        public static readonly string InvalidBuildingType = "The building type is invalid. Please select a valid building type.";
        public static readonly string BuildingCostMustBePositive = "The building cost must be a positive value.";
        public static readonly string ConstructionTimeOutOfRange = "The construction time must be between 30 and 1800 seconds.";

        // Validation Messages
        public static readonly string UsernameRequired = "Username is required.";
        public static readonly string PasswordRequired = "Password is required.";
        public static readonly string EmailRequired = "Email is required.";
        public static readonly string InvalidEmailFormat = "The email format is invalid. Please enter a valid email address.";
        public static readonly string BuildingTypeRequired = "Building type is required.";
        public static readonly string BuildingCostRequired = "Building cost is required.";
        public static readonly string ConstructionTimeRequired = "Construction time is required.";
        public static readonly string BuildingTypeAlreadyAdded = "This building type has already been added.";

        // Additional User-related Messages
        public static readonly string InvalidPasswordFormat = "The password format is invalid. Ensure it meets the required criteria: one uppercase letter, one lowercase letter, one number, and one special character.";
        public static readonly string UsernameTooShort = "The username is too short. Please enter a username with at least 3 characters.";
        public static readonly string UsernameTooLong = "The username is too long. Please enter a username with no more than 20 characters.";
        public static readonly string EmailNotConfirmed = "Your email address is not confirmed. Please check your email for a confirmation link.";

        // Additional Validation Messages
        public static readonly string PasswordTooShort = "The password must be at least 6 characters long.";
        public static readonly string ConfirmPasswordRequired = "Confirm password is required.";

        public static readonly string InvalidIdFormat = "The provided ID format is invalid.";
    }
}
