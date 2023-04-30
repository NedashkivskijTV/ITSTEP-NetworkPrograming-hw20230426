using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using StreetLibrary;

namespace Server
{
    public partial class Form1 : Form
    {

        // Колекція елементів Street
        List<Street> streets;

        public Form1()
        {
            InitializeComponent();

            // Збереження вмісту БД до Json-файлу 
            //DB_StreetsCollection.SaveDbToJsonFile();

            streets = DB_StreetsCollection.LoadDbFromFile();
        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            // Використання Task - пулу потоків, що автоматично є фоновими
            Text = "Server was started!";
            tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), "Server was started and ready to send streets info!\r\n");

            // Створення та запуск задачі
            Task.Run(async () =>
            {
                // Cтворення кінцевої точки "в один рядок"
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 1024);

                // Пасивний сокет на боці сервера - прослуховує підключення
                Socket socket_TAP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // створення сокета

                // привязка сокета до кінцевої точки
                socket_TAP.Bind(endPoint); // (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)

                // Переведення/запуск сокета в режим прослуховування
                socket_TAP.Listen(10);


                // Блок постійного прослуховування, отримання/відправки даних 
                // З ВИКОРИСТАННЯМ Task підходу
                try
                {
                    while (true)
                    {
                        // Створення сокета при підключенні клієнта
                        Socket ns = await socket_TAP.AcceptAsync();
                        tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");


                        // --------------------------------------- ОТРИМАННЯ ДАНИХ
                        // Створення нового байтового буфера для отримання даних - створення байтового масиву
                        byte[] buff_receive = new byte[1024];

                        // Рядок для збереження отриманих даних
                        string data;
                        // Змінна, що міститиме кількість отриманої інф у байтах
                        int len;

                        // Цикл завантаження даних
                        do
                        {
                            len = await ns.ReceiveAsync(buff_receive, SocketFlags.None);

                            // збереження вигружених даних (з використанням кодування)
                            data = Encoding.Default.GetString(buff_receive, 0, len);

                        } while (ns.Available > 0);
                        

                        // --------------------------------------- ОТРИМАННЯ ДАНИХ ВІДПОВІДНО ДО ЗАПИТУ
                        //int index = int.Parse(data);
                        
                        // Конструкція для запобігання виникнення збоїв програми при вводі некоректних даних
                        int index = 0;
                        List<Street> streetsForClient = new List<Street>();
                        if (int.TryParse(data, out index))
                        {
                            streetsForClient = streets.Where(street => street.Index == index).ToList();
                        }
                        

                        // --------------------------------------- ПЕРЕДАЧА ДАНИХ
                        // Формування рядка для передачі клієнту - СЕРІАЛІЗАЦІЯ даних
                        byte[] buff = Encoding.Default.GetBytes(JsonSerializer.Serialize<List<Street>>(streetsForClient));

                        // Передача даних з отриманням кількості переданої інф у байтах
                        int len_send = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);

                        // Виведення інф про кількість відправлених клієнту даних
                        tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), $"{len_send} bytes was send to {ns.RemoteEndPoint}");

                        // Закриття з'єднань
                        ns.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
                        ns.Close(); // Закриття сокета
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        // Оновлення візульного компонента при підклченні клієнта, завершенні передачі даних
        private void UpdateTextBox(string str)
        {
            StringBuilder builder = new StringBuilder(tbServerInfo.Text);
            builder.Append("\r\n" + str);
            tbServerInfo.Text = builder.ToString();
        }
    }
}