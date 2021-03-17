using DTOs;

namespace BusinessLogic
{
    public interface ISaveData
    {
        void SaveToStorage(Measurement elementToStoreage);
    }
}