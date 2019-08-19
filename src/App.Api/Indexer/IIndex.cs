using App.Api.Documents;

namespace App.Api.Indexer
{
    public interface IIndex
    {
        void Add(PasswordDetails details);
    }
}