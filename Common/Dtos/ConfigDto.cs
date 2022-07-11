namespace Common.Dtos;

public class ConfigDto
{
    public ConfigDto(int id, string name, string nip, string phoneNumber, string city, string postcode, string street,
        string bankAccountNumber, string postalTown, string bankName, string email, bool isActive)
    {
        Id = id;
        Name = name;
        Nip = nip.Replace("-", string.Empty);
        PhoneNumber = phoneNumber;
        City = city;
        Postcode = postcode.Replace("-", string.Empty);
        Street = street;
        BankAccountNumber = bankAccountNumber;
        PostalTown = postalTown;
        BankName = bankName;
        Email = email;
        IsActive = isActive;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Nip { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string BankAccountNumber { get; set; } = null!;
    public string PostalTown { get; set; } = null!;
    public string BankName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }
}