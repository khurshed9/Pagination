namespace DTOWithSRM.DTOs;

public readonly record struct PersonReadInfo(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber);


public readonly record struct PersonUpdateInfo(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber);
    
    
public readonly record struct PersonCreateInfo(
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber);