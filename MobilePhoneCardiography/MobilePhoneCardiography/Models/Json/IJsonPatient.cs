namespace MobilePhoneCardiography.Models.Json
{
    public interface IJsonPatient
    {
        string LastName { get; }
        string FirstName { get; }
        string PatientId { get; }
    }
}