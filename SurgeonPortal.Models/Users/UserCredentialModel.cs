using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class UserCredentialModel
    {
        /// <summary>
        /// Can be an empty string
        /// OR
        /// ^[a-zA-Z0-9_.+-]+: This matches the beginning of the string (^) followed by one or more characters that can be letters (uppercase or lowercase), digits, or the special characters _, ., +, or -.
        /// @[a-zA - Z0 - 9 -]+\.: This matches the @ symbol followed by one or more letters or digits(or -), followed by a literal . character.
        /// [a - zA - Z0 - 9 -.] +$: This matches one or more characters that can be letters(uppercase or lowercase), digits, or the special characters -, or., followed by the end of the string($).
        /// </summary>
        [RegularExpression(@"^$|^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "The email address must be a valid format")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Can be an empty string
        /// OR
        /// Minimum of 8 characters
        /// requires at least one uppercase letter
        /// requires at least one lowercase letter
        /// requires at least one digit
        /// requires at least one special character
        /// ^-match the beginning of the string
        /// (?=.*[A - Z]) - positive lookahead for at least one uppercase letter
        /// (?=.*[a - z]) - positive lookahead for at least one lowercase letter
        /// (?=.*\d) - positive lookahead for at least one digit
        /// (?=.*[^\da - zA - Z]) - positive lookahead for at least one non - alphanumeric character
        /// .{ 8,}
        ///   -match any character(except newline) at least 8 times
        /// $ -match the end of the string
        /// </summary>
        [RegularExpression(@"^$|^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "The password does not meet the minimum requirements.  Passwords must be a minimum length of 8 characters, at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string Password { get; set; }
    }
}
