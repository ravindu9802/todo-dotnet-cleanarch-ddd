using Newtonsoft.Json;
using System.Buffers;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Caching.Hybrid;

public class NewtonsoftHybridCacheSerializer<T> : IHybridCacheSerializer<T>
{
    private static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
    {
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
        }
    };

    public T Deserialize(ReadOnlySequence<byte> source)
    {
        var json = Encoding.UTF8.GetString(source.ToArray());
        return JsonConvert.DeserializeObject<T>(json, DefaultSettings);
    }

    public void Serialize(T value, IBufferWriter<byte> target)
    {
        var json = JsonConvert.SerializeObject(value, DefaultSettings);
        var bytes = Encoding.UTF8.GetBytes(json);
        target.Write(bytes);
    }
}