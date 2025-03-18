﻿namespace Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
    }
}
