namespace Pilllar.Vocal.Domain.Constracts.Services
{
    public interface IPropertyCheckerService
    {
        bool TypeHasProperties<T>(string fields);
    }
}