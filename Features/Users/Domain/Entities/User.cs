using System;

namespace TaskManagementApi.Features.Users.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime UpdatedAtUtc { get; private set; }

        // EF Core constructor
        protected User() { }

        // Domain constructor
        public User(string username, string passwordHash)
        {
            Id = Guid.NewGuid();
            SetUsername(username);
            SetPasswordHash(passwordHash);
            CreatedAtUtc = DateTime.UtcNow;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        // Domain methods for encapsulation
        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));

            if (username.Length < 3)
                throw new ArgumentException("Username must be at least 3 characters long", nameof(username));

            Username = username;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash cannot be empty", nameof(passwordHash));

            PasswordHash = passwordHash;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public bool VerifyPassword(string password, Func<string, string, bool> verifyFunction)
        {
            return verifyFunction(password, PasswordHash);
        }
    }
}