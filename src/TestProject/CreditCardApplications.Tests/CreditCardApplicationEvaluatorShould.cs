using System;
using System.Net.Http.Headers;
using Moq;
using Xunit;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplicatios()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.DefaultValue = DefaultValue.Mock;
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { Age = 19 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            //mockValidator.Setup(x => x.IsValid("x")).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.Is<string>(x => x.StartsWith("x")))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsInRange<string>("a", "z", Moq.Range.Inclusive))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsIn("z", "y", "x"))).Returns(true);
            mockValidator.Setup(x => x.IsValid(It.IsRegex("[a-z]"))).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
                FrequentFlyerNumber = "x"
            };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ReferInvalidFrequentFlyerApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Loose);
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("EXPIRED");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication();

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void ReferWhenLicenseKeyExpired()
        {
            //var mockLicenseData = new Mock<ILicenseData>();
            //mockLicenseData.Setup(x => x.LicenseKey).Returns("EXPIRED");
            //var mockServiceInfo = new Mock<IServiceInformation>();
            //mockServiceInfo.Setup(x => x.License).Returns(mockLicenseData.Object);
            //var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation).Returns(mockServiceInfo.Object);
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("EXPIRED");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { Age = 42 };

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        private string GetLicenseKeyExpiryString()
        {
            return "EXPIRED";
        }

        [Fact]
        public void UseDetailedLookupForOlderApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.SetupProperty(x => x.ValidationMode);
            mockValidator.SetupAllProperties();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 30 };
            sut.Evaluate(application);
            Assert.Equal(ValidationMode.Detailed, mockValidator.Object.ValidationMode);
        }

        /*Behavioral-----------------------------------------------------*/

        [Fact]
        public void ValidateFrequentFlyerForLowIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                FrequentFlyerNumber = "q"
            };
            sut.Evaluate(application);

            //mockValidator.Verify(x => x.IsValid("q"));
            //mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), "Frequent Flyer should be validated");
            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void NotToValidateFrequentFlyerForHighIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = 100_000
            };
            sut.Evaluate(application);

            //mockValidator.Verify(x => x.IsValid("q"));
            mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void CheckLicenseKeyForLowIncomeApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = 99_000
            };
            sut.Evaluate(application);

            mockValidator.VerifyGet(x => x.ServiceInformation.License.LicenseKey, Times.Once);
        }

        [Fact]
        public void SetDetailedLookupForOlderApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                Age = 30
            };
            sut.Evaluate(application);

            //mockValidator.VerifySet(x => x.ValidationMode = ValidationMode.Detailed);
            mockValidator.VerifySet(x => x.ValidationMode = It.IsAny<ValidationMode>(), Times.Once);

            //mockValidator.VerifyNoOtherCalls();
        }

        /*Advanced-----------------------------------------------------*/
        /*
        [Fact]
        public void ReferWhenFrequentFlyerFValidationError()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Throws<Exception>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                Age = 42
            };
            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }*/

        [Fact]
        public void ReferInvalidFrequentFlyerApplications_ReturnValueSequence()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            mockValidator.SetupSequence(x => x.IsValid(It.IsAny<string>()))
                .Returns(false)
                .Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication
            {
                Age = 25
            };

            CreditCardApplicationDecision firstDecision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, firstDecision);

            CreditCardApplicationDecision secondDecision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, secondDecision);
        }

        [Fact]
        public void ReferInvalidFrequentFlyerApplications_MultipleCallsSequence()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application1 = new CreditCardApplication
            {
                Age = 25,
                FrequentFlyerNumber = "aa"
            };
            var application2 = new CreditCardApplication
            {
                Age = 25,
                FrequentFlyerNumber = "bb"
            };
            var application3 = new CreditCardApplication
            {
                Age = 25,
                FrequentFlyerNumber = "cc"
            };

            sut.Evaluate(application1);
            sut.Evaluate(application2);
            sut.Evaluate(application3);


        }
    }
}
