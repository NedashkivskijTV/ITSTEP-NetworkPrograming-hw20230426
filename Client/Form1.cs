using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using StreetLibrary;


namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            btnSendIndex.Enabled = false;

            // ������ ������� ��������� � �������� �볺��� - ��� ��������
            //Process.Start("Server.exe");
        }

        private void btnSendIndex_Click(object sender, EventArgs e)
        {
            // ��������� �� ������ ������
            Task.Run(async () =>
            {
                // C�������� ������ ����� "� ���� �����"
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 1024);

                // �������� ����� �� ���� �볺��� - ����������� �� ������� �� �������/������ ���
                Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // ��������� ������

                // ���� ���������� �� ������� �� ��������� ����� 
                // � ������������� Task ������
                try
                {
                    // ϳ��������� �� �������
                    socket_client.Connect(endPoint);

                    // �������� �'������� (�� �����������)
                    if (socket_client.Connected)
                    {
                        // ---------------------------------- ²������� �����
                        // ��������� ������
                        string query = tbIndex.Text;

                        // ��������� ��������� ������
                        byte[] buff = Encoding.Default.GetBytes(query);

                        // ³������� ������ � ������������� ������������
                        await socket_client.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);


                        // ---------------------------------- ��������� �����
                        // ��������� ������ ��������� ������ ��� ��������� ����� - ��������� ��������� ������
                        byte[] buff_receive = new byte[1024];

                        // ����� ��� ���������� ��������� �����
                        string data;
                        // �����. �� �������� ������� �������� ��� � ������
                        int len;

                        // ���� ������������ �����
                        do
                        {
                            len = await socket_client.ReceiveAsync(buff_receive, SocketFlags.None);

                            // ���������� ���������� ����� (� ������������� ���������)
                            data = Encoding.Default.GetString(buff_receive, 0, len);

                        } while (socket_client.Available > 0);

                        // �������� ����� � �������� ������ (������������ �����)
                        List<Street> streets = JsonSerializer.Deserialize<List<Street>>(data.ToString());

                        // ��������� �������/������������� �������� � ��������� ��������� ���������
                        // (����������� ����������/�������������� �����������)
                        dgvStreetsCollection.BeginInvoke(new Action<List<Street>>(ListUpdate), streets);

                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // �������� �'������
                    socket_client.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
                    socket_client.Close(); // �������� ������
                }

                // ����� ���������� (������ ���������� ��������� ������)
                Console.ReadLine();
            });

        }

        // ��������� ���������� ���������� ���� ��������� ��������
        private void ListUpdate(List<Street> streets)
        {
            dgvStreetsCollection.DataSource = null;
            dgvStreetsCollection.DataSource = streets;
        }

        // �������� ���������� ���������� Index
        private void tbIndex_TextChanged(object sender, EventArgs e)
        {
            if(tbIndex.Text.Length == 0)
            {
                btnSendIndex.Enabled = false;
            } else
            {
                btnSendIndex.Enabled = true;
            }
        }
    }
}