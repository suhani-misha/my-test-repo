using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using SocialSecurity.Application.Services;
using SocialSecurity.Domain.Models;
using SocialSecurity.Shared.Dtos.Identity;
using SocialSecurity.Shared.Interfaces;
using SocialSecurity.Application.UnitOfWorks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SocialSecurity.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            // Setup UserManager mock
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            // Setup SignInManager mock
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                _mockUserManager.Object,
                Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null, null);

            // Setup Configuration mock with all required JWT settings
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(x => x["Jwt:Key"]).Returns("YourSecretKeyHere12345678901234567890");
            _mockConfiguration.Setup(x => x["Jwt:Issuer"]).Returns("TestIssuer");
            _mockConfiguration.Setup(x => x["Jwt:Audience"]).Returns("TestAudience");
            _mockConfiguration.Setup(x => x["Jwt:ExpireDays"]).Returns("7");
            _mockConfiguration.Setup(x => x["AppSettings:ClientUrl"]).Returns("http://localhost:3000");

            // Setup configuration section
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(x => x.Value).Returns("YourSecretKeyHere12345678901234567890");
            _mockConfiguration.Setup(x => x.GetSection("Jwt:Key")).Returns(configurationSection.Object);

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmailService = new Mock<IEmailService>();

            _authService = new AuthService(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _mockConfiguration.Object,
                _mockUnitOfWork.Object,
                _mockEmailService.Object);
        }

        [Fact]
        public async Task InitiateRegistrationAsync_WithValidEmail_ShouldSucceed()
        {
            // Arrange
            var email = "test@example.com";
            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authService.InitiateRegistrationAsync(email);

            // Assert
            Assert.True(result);
            _mockEmailService.Verify(x => x.SendVerificationCodeAsync(email, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task InitiateRegistrationAsync_WithExistingEmail_ShouldThrowException()
        {
            // Arrange
            var email = "existing@example.com";
            var existingUser = new ApplicationUser { Email = email };
            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(existingUser);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _authService.InitiateRegistrationAsync(email));
        }

        [Fact]
        public async Task VerifyRegistrationCodeAsync_WithValidCode_ShouldSucceed()
        {
            // Arrange
            var email = "test@example.com";
            var code = "1234";
            await _authService.InitiateRegistrationAsync(email);

            // Act
            var result = await _authService.VerifyRegistrationCodeAsync(email, code);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task VerifyRegistrationCodeAsync_WithInvalidCode_ShouldThrowException()
        {
            // Arrange
            var email = "test@example.com";
            var code = "1234";
            await _authService.InitiateRegistrationAsync(email);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _authService.VerifyRegistrationCodeAsync(email, "wrongcode"));
        }

        [Fact]
        public async Task CompleteRegistrationAsync_WithValidData_ShouldReturnUserDto()
        {
            // Arrange
            var email = "test@example.com";
            var code = "1234";
            await _authService.InitiateRegistrationAsync(email);
            await _authService.VerifyRegistrationCodeAsync(email, code);

            var registerDto = new RegisterDto
            {
                Email = email,
                Password = "Test123!",
                FirstName = "Test",
                LastName = "User",
                MobileNumber = "1234567890"
            };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerDto.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.CompleteRegistrationAsync(registerDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            Assert.Equal(email, result.User.Email);
        }

        [Fact]
        public async Task LoginAsync_WithValidCredentials_ShouldReturnUserDto()
        {
            // Arrange
            var email = "test@example.com";
            var password = "Test123!";
            var user = new ApplicationUser 
            { 
                Id = "1",
                Email = email, 
                UserName = email,
                FirstName = "Test",
                LastName = "User",
                MobileNumber = "1234567890"
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);
            _mockSignInManager.Setup(x => x.PasswordSignInAsync(user, password, false, false))
                .ReturnsAsync(SignInResult.Success);

            var loginDto = new LoginDto { Email = email, Password = password };

            // Act
            var result = await _authService.LoginAsync(loginDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            Assert.Equal(email, result.User.Email);
            Assert.Equal(user.FirstName, result.User.FirstName);
            Assert.Equal(user.LastName, result.User.LastName);
            Assert.Equal(user.MobileNumber, result.User.MobileNumber);
        }

        [Fact]
        public async Task LoginAsync_WithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var email = "test@example.com";
            var password = "WrongPassword";
            var user = new ApplicationUser { Email = email, UserName = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);
            _mockSignInManager.Setup(x => x.PasswordSignInAsync(user, password, false, false))
                .ReturnsAsync(SignInResult.Failed);

            var loginDto = new LoginDto { Email = email, Password = password };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _authService.LoginAsync(loginDto));
        }

        [Fact]
        public async Task ChangePasswordAsync_WithValidData_ShouldSucceed()
        {
            // Arrange
            var email = "test@example.com";
            var currentPassword = "Current123!";
            var newPassword = "New123!";
            var user = new ApplicationUser { Email = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);
            _mockUserManager.Setup(x => x.ChangePasswordAsync(user, currentPassword, newPassword))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.ChangePasswordAsync(email, currentPassword, newPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ForgotPasswordAsync_WithValidEmail_ShouldSucceed()
        {
            // Arrange
            var email = "test@example.com";
            var user = new ApplicationUser { Email = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user))
                .ReturnsAsync("reset-token");

            // Act
            var result = await _authService.ForgotPasswordAsync(email);

            // Assert
            Assert.True(result);
            _mockEmailService.Verify(x => x.SendPasswordResetEmailAsync(email, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_WithValidData_ShouldSucceed()
        {
            // Arrange
            var email = "test@example.com";
            var token = "valid-token";
            var newPassword = "New123!";
            var user = new ApplicationUser { Email = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);
            _mockUserManager.Setup(x => x.ResetPasswordAsync(user, token, newPassword))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.ResetPasswordAsync(email, token, newPassword);

            // Assert
            Assert.True(result);
            _mockEmailService.Verify(x => x.SendPasswordResetConfirmationAsync(email), Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_WithInvalidToken_ShouldThrowException()
        {
            // Arrange
            var email = "test@example.com";
            var token = "invalid-token";
            var newPassword = "New123!";
            var user = new ApplicationUser { Email = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(user);
            _mockUserManager.Setup(x => x.ResetPasswordAsync(user, token, newPassword))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _authService.ResetPasswordAsync(email, token, newPassword));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task InitiateRegistrationAsync_WithInvalidEmail_ShouldThrowArgumentException(string email)
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _authService.InitiateRegistrationAsync(email));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task LoginAsync_WithInvalidEmail_ShouldThrowArgumentException(string email)
        {
            // Arrange
            var loginDto = new LoginDto { Email = email };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _authService.LoginAsync(loginDto));
        }

        [Fact]
        public async Task LoginAsync_WithNullDto_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _authService.LoginAsync(null));
        }

        [Fact]
        public async Task CompleteRegistrationAsync_WithNullDto_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _authService.CompleteRegistrationAsync(null));
        }
    }
} 