namespace SCA.Infraestrutura.Interfaces
{
    public interface IAntiCSRFService
    {
        string Login { get; }
        string HeaderAntiCSRF { get; }
    }
}
