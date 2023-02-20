namespace FastDeliveryApi.Entity;

using FastDeliveryApi.Repositories.Interfaces;

public class Customer : IAuditableEntity
{
    public Customer(string name, string phoneNumber, string email, string address)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Status = true;
    }
    public Customer (){}
    public int Id { get; set; }
    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public bool Status { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public void ChangeName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name) && Name != name)
        {
            Name = name;
        }
    }
    public void ChangePhoneNumber(string phoneNumber)
    {
        if (!string.IsNullOrWhiteSpace(phoneNumber) && PhoneNumber != phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
    public void ChangeEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email) && Email != email)
        {
            Email = email;
        }
    }
    public void ChangeAddress(string address)
    {
        if (!string.IsNullOrWhiteSpace(address) && Address != address)
        {
            Address = address;
        }
    }
    public void ChangeStatus(bool status) => Status = status;
}