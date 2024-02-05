using System.Text;
using System.Text.Json;
using Telegram.Bot.Types;

public class Pay
{
    private static string path = "C:\\Users\\Ozodbek\\Desktop\\MyJs5.json";
    public static void Add(Model5 us)
    {
        List<Model5> list = DeserilizeSerelize<Model5>.GetAll(path);
        foreach (var u in list)
        {
            if (u.card_num == us.card_num)
            {
                return;
            }
        }
        list.Add(us);
        DeserilizeSerelize<Model5>.Save(list, path);
    }

    public static string Read()
    {
        StringBuilder sb = new StringBuilder();
        List<Model5> list = DeserilizeSerelize<Model5>.GetAll(path);
        foreach (Model5 u in list)
        {
            sb.Append($"{u.card_num}\n{u.card_name}");
        }
        return sb.ToString();
    }

    public static void Update(string card_nums, string name)
    {
        List<Model5> list = DeserilizeSerelize<Model5>.GetAll(path);
        int index = list.FindIndex(us => us.card_num == card_nums);

        if (index != -1)
        {
            list[index].card_name = name;
            DeserilizeSerelize<Model5>.Save(list, path);
        }
    }
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
