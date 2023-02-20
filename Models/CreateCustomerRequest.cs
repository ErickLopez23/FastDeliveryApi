namespace FastDeliveryApi.Models;

public record CreateCustomerRequest(
    string Name,
    string PhoneNumber,
    string Email,
    string Address,
    bool Status
);