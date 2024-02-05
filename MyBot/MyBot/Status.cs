using System.Text;
using System.Text.Json;
using Telegram.Bot.Types;
using Mybot;
namespace Mybot;
public class Status
{
    private static string path = "C:\\Users\\Ozodbek\\Desktop\\MyJs3.json";
    public static void Add(Model3 us)
    {
        List<Model3> list = DeserilizeSerelize<Model3>.GetAll(path);
        foreach (var u in list)
        {
            if (u.name == us.name)
            {
                return;
            }
        }
        list.Add(us);
        DeserilizeSerelize<Model3>.Save(list, path);
    }

    public static string Read()
    {
        StringBuilder sb = new StringBuilder();
        List<Model3> list = DeserilizeSerelize<Model3>.GetAll(path);
        foreach (Model3 u in list)
        {
            sb.Append($"{u.status}\n{u.model}\n{u.name}");
        }
        return sb.ToString();
    }

    //public static void Update(long chatId, string phn)
    //{
    //    List<Crud> list = DeserilizeSerelize<Crud>.GetAll(path);
    //    int index = list.FindIndex(us => us.chatId == chatId);

    //    if (index != -1)
    //    {
    //        list[index].phoneNumber = phn;
    //        DeserilizeSerelize<Crud>.Save(list, path);
    //    }
    //}
    //public static void Delete(long chatId)
    //{
    //    List<Crud> list = DeserilizeSerelize<Crud>.GetAll(path);
    //    int index = list.FindIndex(us => us.chatId == chatId);

    //    if (index != -1)
    //    {
    //        list.RemoveAt(index);
    //        DeserilizeSerelize<Crud>.Save(list, path);
    //    }
    //}

    class DeserilizeSerelize<T>
    {
        public static List<T> GetAll(string path)
        {
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            else
            {
                return new List<T>();
            }
        }

        public static void Save(List<T> lst, string path)
        {
            string json = JsonSerializer.Serialize(lst);
            System.IO.File.WriteAllText(path, json);
        }
    }
}
