using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Xml.Schema;

namespace Chatroom_Server
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;//Khong cho check dung do luong
            Connectt();
        }
        //Server phai gui cho nhieu client
        private void btnSend_Click(object sender, EventArgs e)
        {
            foreach(Socket item in clientList)
            {
                Sendd(item);
                Addmess(txtMessage.Text);
            }
            //txtMessage.Clear();
        }



        /*
         * De ket noi client va server, can co 2 thu:
         * - Socket, la mot cai cua
         * - Ip, la mot cai dia chi
         */
        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;// Danh sach cac client da ket noi toi server




        //Ket noi toi server
        void Connectt()
        {
            clientList=new List<Socket>();//Tao mot danh sach cac ket noi
            //Cai lenh nay tao ra cai IP localhost la 127.0.0.1 va port la 9999
            //Ke di, khoi xai nao, hoc thuoc long la dc
            //127.0.0.1 la con so mac dinh khi dung tren localhost tuc 1 may voi nhau
            //Neu dung tren mang LAN ta phai doi no lai thanh cai IP may minh trong ip config
            //port cu lay dai so nao tren 1500 de khoi trung voi may ung dung khac
            IP = new IPEndPoint(IPAddress.Any, 9999);
            //Hoc thuoc di dung dung nao
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            //Voi IP la dia chi server, client la o cam socket
            server.Bind(IP);
            Thread Listen = new Thread(() =>{
                try
                {
                    while (true)
                    {
                        server.Listen(100);//Server lang nghe max la 100 client
                        Socket client = server.Accept();
                        clientList.Add(client);
                        Thread receive = new Thread(Receivee);//Tao luong receive tin cua client ay
                        receive.IsBackground = true;
                        receive.Start(client);
                    }
                }
                catch 
                {
                    IP = new IPEndPoint(IPAddress.Any, 9999);
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                }
                
            });
            Listen.IsBackground = true;
            Listen.Start();
        }
        //Dong ket noi hien tai
        void Closee()
        {
            
            server.Close();
        }
        //Nhan tin
        void Receivee(object obj)//Phai co tham so de biet nhan tu ai
        {
            Socket client = obj as Socket;
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];//Khoang 5 MB
                    client.Receive(data);
                    string message = (string)Deserialize(data, typeof(string));//giai ma ra string va gan
                    Addmess(message);
                }
            }
            catch//Cu co lang nghe den khi khong con nghe dc nx
            {
                clientList.Remove(client);
                client.Close();
            }
        }
        //Add message vao khung chat
        void Addmess(string s)
        {
            lsvMessage.Items.Add(new ListViewItem() { Text = s });
            txtMessage.Clear();
        }
        //Gui tin
        void Sendd(Socket client)//Phai co tham so de biet send cho ai
        {
            if (txtMessage.Text != string.Empty)
            {//Neu lo chua viet gi vo textbox ma nhan Send
                client.Send(Serialize(txtMessage.Text));//gui cac byte phan manh tu text vao socket
            }
        }
        //Phan manh, ma hoa
        /*byte[] Serialize(object obj)
        {
            MemoryStream stream=new MemoryStream();
            BinaryFormatter formatter= new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();// Phan manh text ra cac byte len stream thanh array
        }*/
        byte[] Serialize(object obj)
        {
            // Tuần tự hóa đối tượng thành chuỗi JSON
            string jsonString = JsonSerializer.Serialize(obj);

            // Chuyển chuỗi JSON thành mảng byte
            return Encoding.UTF8.GetBytes(jsonString);
        }
        //Ghep manh, giai ma
        object Deserialize(byte[] data, Type objectType)
        {
            /*MemoryStream stream = new MemoryStream(data);// Can truyen data byte cho stream
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);// Ghep cac manh byte cua stream thanh mot string
            */
            // Chuyển mảng byte thành chuỗi JSON
            string jsonString = Encoding.UTF8.GetString(data);

            // Giải tuần tự hóa chuỗi JSON thành đối tượng
            return JsonSerializer.Deserialize(jsonString, objectType);

        }
        //Dong ket noi moi khi dong form Client
        private void Server_FormClose(object sender, EventArgs e)
        {
            Closee();
        }
        private void Server_Load(object sender, EventArgs e)
        {

        }

    }
}

