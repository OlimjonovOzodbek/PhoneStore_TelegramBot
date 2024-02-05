using System.Text;
using System.Text.Json;
using Telegram.Bot.Types;

public class Crud
{
    private static string path = "C:\\Users\\Ozodbek\\Desktop\\MyJs.json";
    public static void Add(Model1 us)
    {
        List<Model1> list = DeserilizeSerelize<Model1>.GetAll(path);
        foreach (var u in list)
        {
            if (u.seria_number == us.seria_number)
            {
                return;
            }
        }
        list.Add(us);
        DeserilizeSerelize<Model1>.Save(list, path);
    }

    public static string Read()
    {
        StringBuilder sb = new StringBuilder();
        List<Model1> list = DeserilizeSerelize<Model1>.GetAll(path);
        foreach (Model1 u in list)
        {
            sb.Append($"{u.name}\n{u.model}\n{u.seria_number}");
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
