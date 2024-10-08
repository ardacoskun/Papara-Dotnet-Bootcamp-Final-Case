﻿namespace Para.Schema.Entities.DTOs.User;

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public decimal WalletBalance { get; set; }
    public int PointsBalance { get; set; }
}
