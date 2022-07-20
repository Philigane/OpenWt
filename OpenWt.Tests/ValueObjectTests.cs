using OpenWt.Contracts.ValueObjects;
using Xunit;

namespace OpenWt.Tests;

public class ValueObjectTests
{
    #region Email

    [Theory, InlineData("toto@gmail.com"), InlineData("toto@outlook.com"), InlineData("gmail@outlook.com"),
     InlineData("outlook@gmail.com"), InlineData("outlook@gmail.fr"), InlineData("123@gmail.fr"),
     InlineData("123azerty@gmail.fr")]
    public void PassedValidEmail_ShouldNotThrow(string email) => Assert.NotNull(new Email(email));

    [Theory, InlineData("totogmail.com"), InlineData("totooutlook.com"), InlineData("gmailcom"), InlineData("123"),
     InlineData("123azerty")]
    public void PassedInvalidEmail_ShouldThrow(string email) => Assert.Throws<EmailException>(() => new Email(email));
    #endregion

    #region Phone Number

    [Theory, InlineData("0123456789"), InlineData("01 23 45 67 89"), InlineData("01.23.45.67.89"),
     InlineData("0123 45.67.89"), InlineData("0033 123-456-789"), InlineData("+33-1.23.45.67.89"),
     InlineData("+33 - 123 456 789"), InlineData("+33(0) 123 456 789"), InlineData("+33 (0)123 45 67 89"),
     InlineData("+33 (0)1 2345-6789"), InlineData("+33(0) - 123456789")]
    public void PassedValidPhoneNumber_ShouldNotThrow(string number) => Assert.NotNull(new PhoneNumber(number));

    [Theory, InlineData("012345678"), InlineData("01 23 45 67 8"), InlineData("01.23.45.67.8"),
     InlineData("0123 45.67.8"), InlineData("0033 123-456-89"), InlineData("+33-1.23.45.6.89"),
     InlineData("+33 - 123 46 789"), InlineData("+33(0) 123 56 789"), InlineData("+33 (0)123 5 67 89"),
     InlineData("+33 (0)1 235-6789"), InlineData("+33(0) - 12456789"), InlineData("01234567891"),
     InlineData("01 23 45 67 89 1"), InlineData("01.23.45.67.89.1"), InlineData("0123 45.67.89.1"),
     InlineData("0033 123-456-8991"), InlineData("+33-1.23.45.6.8991"), InlineData("+33 - 123 46 78991"),
     InlineData("+33(0) 123 56 78991"), InlineData("+33 (0)123 5 67 8991"), InlineData("+33 (0)1 235-678991"),
     InlineData("+33(0) - 1245678991")]
    public void PassedInvalidPhoneNumber_ShouldThrow(string number) => Assert.Throws<PhoneNumberException>(() => new PhoneNumber(number));
    #endregion
}