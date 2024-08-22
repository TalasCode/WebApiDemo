﻿namespace BackendAPI.RequestBody
{
    public class AddUserRequest
    {
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? PasswordHash { get; set; }
    }
}
