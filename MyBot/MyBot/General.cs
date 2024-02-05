using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Mybot

{
    public class MyBot
    {
        public bool isContactShared = false;
        private string MyToken;

        #region O'zgaruvchilar
        bool phone_create = false;
        bool phone_crud = false;

        bool order_status = false;
        bool order_create = false;

        bool office_crud = false;
        bool office_create = false;

        bool paytype_crud = false;
        bool paytype_create = false;

        bool pH_crud = false;
        bool pH_create = false;

        bool region_create = false;
        bool region_crud = false;   

        bool pay_up = false;
        #endregion

        string path = "C:\\Users\\Ozodbek\\Desktop\\MyJs.json";

        public MyBot(string token)
        {
            MyToken = token;
        }
        public async Task Begin()
        {
            var botClient = new TelegramBotClient(MyToken);

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        

        public async Task buttons(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup1 = new(new[]
                    {
                        new[]{
                        new KeyboardButton("CREATE"),
                        new KeyboardButton("DELETE"),
                        new KeyboardButton("UPDATE"),
                        new KeyboardButton("READ")
                            },
                         new[]
                         {
                            new KeyboardButton("Back")
                         },
            })
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(
                 chatId: update.Message.Chat.Id,
                 text: "Choose a response",
                 replyMarkup: replyKeyboardMarkup1,
                 cancellationToken: cancellationToken);
            return;

        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            
            var messageText = "";
            if (update.Message is not { } message)
                return;
            if (message.Type == MessageType.Text)
            {
                messageText = message.Text;
            }

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' \n{chatId}.");

            if (messageText == "/start")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(
                    new[]
            {
                  KeyboardButton.WithRequestContact("Contact")
            })

                {
                    ResizeKeyboard = true
                };

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "𝐀𝐬𝐬𝐚𝐥𝐨𝐦𝐮 𝐚𝐥𝐞𝐲𝐤𝐮𝐦, 𝐛𝐨𝐭𝐝𝐚𝐧 𝐟𝐨𝐲𝐝𝐚𝐥𝐚𝐧𝐢𝐬𝐡 𝐮𝐜𝐡𝐮𝐧 𝐤𝐨𝐧𝐭𝐚𝐤𝐭𝐧𝐢 𝐣𝐨'𝐧𝐚𝐭𝐢𝐧𝐠",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                return;
            }
            if (messageText == "Back")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(
                   new[]
           {
                                new[]
                                {

                                new KeyboardButton("Phone Crud"),
                                new KeyboardButton("Download Excel"),
                                new KeyboardButton("Office Crud"),
                                new KeyboardButton("Customer Region"),
                                    },
                                new []
                                {
                                 new KeyboardButton("Order Status"),
                                new KeyboardButton("PayType Crud"),
                                new KeyboardButton("Payment History")
                                    },


           })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Bosh Menu",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                return;
            }
            

            if (message.Contact is not null)
            {
                isContactShared = true;
            }
            if (isContactShared)
            {
                if (messageText == "Download Excel")
                {
                    await botClient.SendDocumentAsync(
                    chatId: chatId,
                    document: new InputFileStream(new FileStream("D:\\MyDoc.docx",FileMode.Open)),
                    cancellationToken: cancellationToken);
                    return;
                }
                if (messageText == "Phone Crud")
                {
                    phone_crud = true;
                    await buttons(botClient, update, cancellationToken);
                    return;
                }
                if (phone_create == true)
                {
                    string[] st1= messageText.Split(',');
                    Crud.Add(new Model1()
                    {
                        seria_number = st1[0],
                        name = st1[1],
                        model = st1[2],

                    });
                    phone_create = false;
                }
                if (phone_crud == true)
                {
                    if (messageText == "CREATE")
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos ma'lumotlarni ( , ) orqali kiriting❗❗\n\n" +
                            "1)𝐒𝐄𝐑𝐈𝐘𝐀 𝐑𝐀𝐐𝐀𝐌𝐈\n2)𝐍𝐎𝐌𝐈\n3)𝐌𝐎𝐃𝐄𝐋𝐈",
                            cancellationToken: cancellationToken);
                        phone_create = true;
                        phone_crud = false;
                        return;
                    }
                    if (messageText == "READ")
                    {
                        string lst1 = Crud.Read();
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: lst1,
                            cancellationToken: cancellationToken);
                        phone_crud = false;
                    }
                }

                if (messageText == "Office Crud")
                {
                    office_crud = true;
                    await buttons(botClient, update, cancellationToken);
                    return;
                }
                if (office_create == true)
                {
                    string[] st2 = messageText.Split(',');
                    OfficeC.Add(new MyModel2()
                    {
                        name = st2[0],
                        ShopNum = st2[1]

                    }) ;
                    office_create = false;
                }
                if (office_crud == true)
                {
                    if (messageText == "CREATE")
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Yangi office manzili ni kiriting\n" +
                            "1) Manzil\n" +
                            "2) Do'kon raqami",
                            cancellationToken: cancellationToken);
                        office_create = true;
                        office_crud = false;
                        return;
                    }
                    if (messageText == "READ")
                    {
                        string lst = OfficeC.Read();
                        //UPDATE
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: lst,
                            cancellationToken: cancellationToken);
                        office_crud = false;

                    }
                }

                if (messageText == "Customer Region")
                {
                    region_crud = true;
                    await buttons(botClient, update, cancellationToken);
                    return;
                }
                if (region_create == true)
                {
                    string[] st3 = messageText.Split(',');
                    Region.Add(new Model4()
                    {
                        region_name = st3[0],
                        post_code = st3[1],

                    });
                    region_create = false;
                }
                if (region_crud == true)
                {
                    if (messageText == "CREATE")
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos ma'lumotlarni ( , ) orqali kiriting❗❗\n\n" +
                            "1)REGION NAME\n2)POST CODE\n",
                            cancellationToken: cancellationToken);
                        region_create = true;
                        region_crud = false;
                        return;
                    }
                    if (messageText == "READ")
                    {
                        string lst2 = Region.Read();
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: lst2,
                            cancellationToken: cancellationToken);
                        region_crud = false;
                    }
                }


                if (messageText == "PayType Crud")
                {
                    paytype_crud = true;
                    await buttons(botClient, update, cancellationToken);
                    return;
                }
                if (paytype_create == true)
                {
                    string[] st4 = messageText.Split(',');
                    Pay.Add(new Model5()
                    {
                        card_name = st4[0],
                        card_num = st4[1],

                    });
                    paytype_create = false;
                }
                if(pay_up == true)
                {
                    if(message.Text != null)
                    {
                        string[] sb = messageText.Split(',');
                        Pay.Update(sb[0], sb[1]);
                        await botClient.SendTextMessageAsync(
                        chatId: chatId,
                            text: "O'zgardi",
                            cancellationToken: cancellationToken);
                        return;
                    }
                }
                if (paytype_crud == true)
                {
                    if (messageText == "CREATE")
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos ma'lumotlarni ( , ) orqali kiriting❗❗\n\n" +
                            "1)NOMI\n2)KARTA RAQAMI",
                            cancellationToken: cancellationToken);
                        paytype_create = true;
                        paytype_crud = false;
                        return;
                    }
                    if (messageText == "READ")
                    {
                        string lst = Pay.Read();
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: lst,
                            cancellationToken: cancellationToken);
                        paytype_crud = false;
                    }
                    if(messageText == "UPDATE")
                    {
                        pay_up = true;
                        return;
                    }
                }
                if (messageText == "Order Status")
                {
                    order_status = true;
                    await buttons(botClient, update, cancellationToken);
                    return;
                }
                if (order_create == true)
                {
                    string[] st5 = messageText.Split(',');
                    Status.Add(new Model3()
                    {
                        name = st5[0],
                        model = st5[1],
                        status = st5[2],

                    });
                    order_create = false;
                }
                if (order_status == true)
                {
                    if (messageText == "CREATE")
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos ma'lumotlarni ( , ) orqali kiriting❗❗\n\n" +
                            "1)NOMI\n2)MODELI\n3)STATUSI",
                            cancellationToken: cancellationToken);
                        order_create = true;
                        order_status = false;
                        return;
                    }
                    if(messageText == "READ")
                    {
                        string lst = Status.Read();
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: lst,
                            cancellationToken: cancellationToken);
                        order_status = false;
                    }
                }
                if (messageText == "Payment History")
                {
                    pH_crud = true;
                    await buttons(botClient, update, cancellationToken);
                    return;
                }
                if (pH_create == true)
                {
                    string[] st6 = messageText.Split(',');
                    PayHistory.Add(new Model6()
                    {
                        card_number = st6[0],
                        card_type = st6[1],
                        sum = st6[2],

                    });
                    pH_create = false;
                }
                if (pH_crud == true)
                {
                    if (messageText == "CREATE")
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos ma'lumotlarni ( , ) orqali kiriting❗❗\n\n" +
                            "1)KARTA 𝐑𝐀𝐐𝐀𝐌𝐈\n2)KARTA TURI\n3)KARTA MIQDORI",
                            cancellationToken: cancellationToken);
                        pH_create = true;
                        pH_crud = false;
                        return;
                    }
                    if (messageText == "READ")
                    {
                        string lst = PayHistory.Read();
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: lst,
                            cancellationToken: cancellationToken);
                        pH_crud = false;
                    }
                }

                if (message.Chat.Id == 967332332)
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(
                        new[]
                {
                                new[]
                                {

                                new KeyboardButton("Phone Crud"),
                                new KeyboardButton("Download Excel"),
                                new KeyboardButton("Office Crud"),
                                new KeyboardButton("Customer Region"),
                                    },
                                new []
                                {
                                 new KeyboardButton("Order Status"),
                                new KeyboardButton("PayType Crud"),
                                new KeyboardButton("Payment History")
                                    },


                })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Bosh Menu",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);
                    return;
                }
                List<string> userList = new List<string> { "Olimjonov Ozodbek \n+998934013443", "Jamshidbek \n+998945672345", "Sattorov Abdulaziz \n+998912345678", "Sotimov Alisher \n+998934013443"};
                if (messageText == "REGISTERED USERS")
                {
                    foreach (var i in userList)
                    {
                        await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: i,
                        cancellationToken: cancellationToken);
                    }
                    return;
                }
                List<string> stringList = new List<string> { "Iphone 12", "Samsung J7", "Honor 8x", "Vivo 5", "LG Q5", "Iphone 14 pro", "Iphone 7" };
                if (messageText == "PHONE LIST")                    
                {
                    foreach(var i in stringList) 
                    { 
                    await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: i,
                    cancellationToken: cancellationToken);
                    
                    }
                    return;
                }
                else if (message.Chat.Id != 967332332)
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup1 = new(new[]
                      {
                        new[]{
                        new KeyboardButton("PHONE LIST"),
                        new KeyboardButton("REGISTERED USERS"),
                        new KeyboardButton("UPDATE"),
                        new KeyboardButton("READ")
                            }

            })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                         chatId: update.Message.Chat.Id,
                         text: "Choose a response",
                         replyMarkup: replyKeyboardMarkup1,
                         cancellationToken: cancellationToken);
                    return;
                }
            }
        }


    }
}



public class Model1
{
    public string seria_number { get; set; }
    public string name { get; set; }
    public string model { get; set; }
}

public class MyModel2
{
    public string name { get; set; }
    public string ShopNum {  get; set; }
}

public class Model3
{
    public string name { get; set; }
    public string model { get; set; }
    public string status { get; set; }
}

public class Model4
{
    public string post_code { get; set; }
    public string region_name { get; set; }
}

public class Model5
{
    public string card_name { get; set; }
    public string card_num { get; set; }
}

public class Model6
{
    public string card_number { get; set; }
    public string card_type { get; set;}
    public string sum { get; set; }

}