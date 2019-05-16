using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using Moq;

namespace ESFA.DC.ESF.R2.ValidationService.Tests
{
    public class BaseTest
    {
        protected readonly Mock<IValidationErrorMessageService> _messageServiceMock;

        public BaseTest()
        {
            _messageServiceMock = new Mock<IValidationErrorMessageService>();
        }
    }
}