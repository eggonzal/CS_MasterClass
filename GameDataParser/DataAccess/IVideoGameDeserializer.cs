using GameDataParser.Model;

namespace GameDataParser.DataAccess;

public interface IVideoGameDeserializer
{
    List<VideoGame> DeserializeFrom(string fileName, string fileContents);
}