using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using StreetLibrary;

namespace Server
{
    public partial class Form1 : Form
    {

        // �������� �������� Street
        List<Street> streets;

        public Form1()
        {
            InitializeComponent();

            // ���������� ����� �� �� Json-����� 
            //DB_StreetsCollection.SaveDbToJsonFile();

            streets = DB_StreetsCollection.LoadDbFromFile();
        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            // ������������ Task - ���� ������, �� ����������� � ��������
            Text = "Server was started!";
            tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), "Server was started and ready to send streets info!\r\n");

            // ��������� �� ������ ������
            Task.Run(async () =>
            {
                // C�������� ������ ����� "� ���� �����"
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 1024);

                // �������� ����� �� ���� ������� - ���������� ����������
                Socket socket_TAP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // ��������� ������

                // �������� ������ �� ������ �����
                socket_TAP.Bind(endPoint); // (�������� ����� ���������������� ��� � ������ ����� - socket ���� �������� �� ������ �����)

                // �����������/������ ������ � ����� ���������������
                socket_TAP.Listen(10);


                // ���� ��������� ���������������, ���������/�������� ����� 
                // � ������������� Task ������
                try
                {
                    while (true)
                    {
                        // ��������� ������ ��� ��������� �볺���
                        Socket ns = await socket_TAP.AcceptAsync();
                        tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");


                        // --------------------------------------- ��������� �����
                        // ��������� ������ ��������� ������ ��� ��������� ����� - ��������� ��������� ������
                        byte[] buff_receive = new byte[1024];

                        // ����� ��� ���������� ��������� �����
                        string data;
                        // �����, �� �������� ������� �������� ��� � ������
                        int len;

                        // ���� ������������ �����
                        do
                        {
                            len = await ns.ReceiveAsync(buff_receive, SocketFlags.None);

                            // ���������� ���������� ����� (� ������������� ���������)
                            data = Encoding.Default.GetString(buff_receive, 0, len);

                        } while (ns.Available > 0);
                        

                        // --------------------------------------- ��������� ����� ²���²��� �� ������
                        //int index = int.Parse(data);
                        
                        // ����������� ��� ���������� ���������� ���� �������� ��� ���� ����������� �����
                        int index = 0;
                        List<Street> streetsForClient = new List<Street>();
                        if (int.TryParse(data, out index))
                        {
                            streetsForClient = streets.Where(street => street.Index == index).ToList();
                        }
                        

                        // --------------------------------------- �������� �����
                        // ���������� ����� ��� �������� �볺��� - ��в�˲��ֲ� �����
                        byte[] buff = Encoding.Default.GetBytes(JsonSerializer.Serialize<List<Street>>(streetsForClient));

                        // �������� ����� � ���������� ������� �������� ��� � ������
                        int len_send = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);

                        // ��������� ��� ��� ������� ����������� �볺��� �����
                        tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), $"{len_send} bytes was send to {ns.RemoteEndPoint}");

                        // �������� �'������
                        ns.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
                        ns.Close(); // �������� ������
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        // ��������� ��������� ���������� ��� �������� �볺���, ��������� �������� �����
        private void UpdateTextBox(string str)
        {
            StringBuilder builder = new StringBuilder(tbServerInfo.Text);
            builder.Append("\r\n" + str);
            tbServerInfo.Text = builder.ToString();
        }
    }
}