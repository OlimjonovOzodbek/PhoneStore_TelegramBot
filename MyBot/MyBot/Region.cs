using System.Text;
using System.Text.Json;
using Telegram.Bot.Types;

public class Region
{
    private static string path = "C:\\Users\\Ozodbek\\Desktop\\MyJs4.json";
    public static void Add(Model4 us)
    {
        List<Model4> list = DeserilizeSerelize<Model4>.GetAll(path);
        foreach (var u in list)
        {
            if (u.post_code == us.post_code)
            {
                return;
            }
        }
        list.Add(us);
        DeserilizeSerelize<Model4>.Save(list, path);
    }

    public static string Read()
    {
        StringBuilder sb = new StringBuilder();
        List<Model4> list = DeserilizeSerelize<Model4>.GetAll(path);
        foreach (Model4 u in list)
        {
            sb.Append($"{u.region_name}\n{u.post_code}");
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
