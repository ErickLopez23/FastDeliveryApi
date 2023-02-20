namespace FastDeliveryApi.Models;

public record UpdateCustomerRequest(
    int Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address,
    bool Status
);