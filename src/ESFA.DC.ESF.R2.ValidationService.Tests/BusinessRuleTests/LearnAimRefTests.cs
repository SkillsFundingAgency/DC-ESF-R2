using System;
using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class LearnAimRefTests
    {
        [Theory]
        [InlineData("NR01")]
        [InlineData("RQ01")]
        public void LearnAimRef01FailsNoLearnAimRef(string deliverableCode)
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = null,
                DeliverableCode = deliverableCode
            };

            var rule = new LearnAimRef01();

            Assert.False(rule.IsValid(suppData));
        }

        [Theory]
        [InlineData("NR01")]
        [InlineData("RQ01")]
        public void LearnAimRef01PassesLearnAimRefPresent(string deliverableCode)
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = deliverableCode
            };

            var rule = new LearnAimRef01();

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef01PassesIrrelevantDeliverableCode()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = null,
                DeliverableCode = "Foo"
            };

            var rule = new LearnAimRef01();

            Assert.True(rule.IsValid(suppData));
        }

        [Theory]
        [InlineData("CG01")]
        [InlineData("CG02")]
        [InlineData("SD01")]
        [InlineData("SD02")]
        public void LearnAimRef02FailsLearnAimRefFound(string deliverableCode)
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = deliverableCode
            };

            var rule = new LearnAimRef02();

            Assert.False(rule.IsValid(suppData));
        }

        [Theory]
        [InlineData("CG01")]
        [InlineData("CG02")]
        [InlineData("SD01")]
        [InlineData("SD02")]
        public void LearnAimRef02PassesNoLearnAimRef(string deliverableCode)
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = null,
                DeliverableCode = deliverableCode
            };

            var rule = new LearnAimRef02();

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef02PassesIrrelevantDeliverableCode()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "Foo"
            };

            var rule = new LearnAimRef02();

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef03PassesNoLearnAimRef()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = null,
                DeliverableCode = "foo"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();

            var rule = new LearnAimRef03(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef03FailsLearnAimRefNotFoundInLars()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "Foo"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns((LarsLearningDeliveryModel)null);

            var rule = new LearnAimRef03(refDataServiceMock.Object);

            Assert.False(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef03PassesLearnAimRefFoundInLars()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "Foo"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns(new LarsLearningDeliveryModel());

            var rule = new LearnAimRef03(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef04FailsNoMatchingValidityDates()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                CalendarMonth = 5,
                CalendarYear = 2018
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns(new LarsLearningDeliveryModel
                {
                    LearnAimRef = suppData.LearnAimRef,
                    ValidityPeriods = new List<LarsValidityPeriod>
                    {
                        new LarsValidityPeriod
                        {
                            ValidityStartDate = new DateTime(2017, 4, 1),
                            ValidityEndDate = new DateTime(2018, 4, 30)
                        },
                        new LarsValidityPeriod
                        {
                            ValidityStartDate = new DateTime(2018, 6, 1),
                            ValidityEndDate = null
                        }
                    }
                });

            var rule = new LearnAimRef04(refDataServiceMock.Object);

            Assert.False(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef04PassesNoLearnRefNum()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = null,
                CalendarMonth = 5,
                CalendarYear = 2018
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();

            var rule = new LearnAimRef04(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef04PassesNoMatchingLarsLearningDelivery()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                CalendarMonth = 5,
                CalendarYear = 2018
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns((LarsLearningDeliveryModel)null);

            var rule = new LearnAimRef04(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef04PassesMatchingValidityDates()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                CalendarMonth = 5,
                CalendarYear = 2018
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns(new LarsLearningDeliveryModel
                {
                    LearnAimRef = suppData.LearnAimRef,
                    ValidityPeriods = new List<LarsValidityPeriod>
                    {
                        new LarsValidityPeriod
                        {
                            ValidityStartDate = new DateTime(2018, 5, 1),
                            ValidityEndDate = null
                        }
                    }
                });

            var rule = new LearnAimRef04(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef05FailsGenreWrong()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "RQ01"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns(new LarsLearningDeliveryModel
                {
                    LearnAimRef = suppData.LearnAimRef,
                    LearningDeliveryGenre = "Foo"
                });

            var rule = new LearnAimRef05(refDataServiceMock.Object);

            Assert.False(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef05PassesNoLearnAimRef()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = null,
                DeliverableCode = "RQ01"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();

            var rule = new LearnAimRef05(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef05PassesIrrelevantDeliveryCode()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "Foo"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();

            var rule = new LearnAimRef05(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef05PassesNoLarsLearningDelivery()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "RQ01"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns((LarsLearningDeliveryModel)null);

            var rule = new LearnAimRef05(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Theory]
        [InlineData("EOQ")]
        [InlineData("EQQ")]
        [InlineData("EOU")]
        [InlineData("IHE")]
        public void LearnAimRef05PassesGenreCorrect(string genre)
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "RQ01"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns(new LarsLearningDeliveryModel
                {
                    LearnAimRef = suppData.LearnAimRef,
                    LearningDeliveryGenre = genre
                });

            var rule = new LearnAimRef05(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Theory]
        [InlineData("EOQ")]
        [InlineData("EQQ")]
        [InlineData("EOU")]
        [InlineData("IHE")]
        public void LearnAimRef06FailsGenreFound(string genre)
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "NR01"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns(new LarsLearningDeliveryModel
                {
                    LearnAimRef = suppData.LearnAimRef,
                    LearningDeliveryGenre = genre
                });

            var rule = new LearnAimRef06(refDataServiceMock.Object);

            Assert.False(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef06PassesNoLearnAimRef()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = null,
                DeliverableCode = "NR01"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();

            var rule = new LearnAimRef06(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef06PassesIrrelevantDeliveryCode()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "Foo"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();

            var rule = new LearnAimRef06(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef06PassesNoLarsLearningDelivery()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "NR01"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns((LarsLearningDeliveryModel)null);

            var rule = new LearnAimRef06(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void LearnAimRef06PassesGenreIrrelevant()
        {
            var suppData = new SupplementaryDataModel
            {
                LearnAimRef = "Foo",
                DeliverableCode = "NR01"
            };

            var refDataServiceMock = new Mock<IReferenceDataService>();
            refDataServiceMock
                .Setup(m => m.GetLarsLearningDelivery(suppData.LearnAimRef))
                .Returns(new LarsLearningDeliveryModel
                {
                    LearnAimRef = suppData.LearnAimRef,
                    LearningDeliveryGenre = "Foo"
                });

            var rule = new LearnAimRef06(refDataServiceMock.Object);

            Assert.True(rule.IsValid(suppData));
        }
    }
}