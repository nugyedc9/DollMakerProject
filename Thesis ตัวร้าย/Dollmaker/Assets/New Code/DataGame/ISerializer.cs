namespace System.Peristence
{
    public interface ISerializer 
    { 
        string Serialize<T>(T obj);
        T Deserilaizer<T>(string json);
    }

}
