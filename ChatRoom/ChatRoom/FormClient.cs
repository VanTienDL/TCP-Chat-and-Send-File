using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
//<EnableUnsafeBinaryFormatterSerialization>true</ EnableUnsafeBinaryFormatterSerialization>
namespace ChatRoom
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;//Khong cho check dung do luong
            Connectt();//Khoi tao port la connect luon
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            Sendd();
            Addmess(txtMessage.Text);
        }
        /*
         * De ket noi client va server, can co 2 thu:
         * - Socket, la mot cai cua
         * - Ip, la mot cai dia chi
         */
        IPEndPoint IP;
        Socket client;





        //Ket noi toi server
        void Connectt()
        {
            //Cai lenh nay tao ra cai IP localhost la 127.0.0.1 va port la 9999
            //Ke di, khoi xai nao, hoc thuoc long la dc
            //127.0.0.1 la con so mac dinh khi dung tren localhost tuc 1 may voi nhau
            //Neu dung tren mang LAN ta phai doi no lai thanh cai IP may minh trong ip config
            //port cu lay dai so nao tren 1500 de khoi trung voi may ung dung khac
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            //Hoc thuoc di dung dung nao
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            //Voi IP la dia chi server, client la o cam socket
            try
            {
                client.Connect(IP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the ket noi","Loi ket noi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            Thread listen = new Thread(Receivee);// Tao luong Receivee
            listen.IsBackground = true;//Luon chay ngam
            listen.Start();// Connect dc thi bat dau nghe( nhan- receive tin)
        }
        //Dong ket noi hien tai
        void Closee()
        {
            client.Close();
        }
        //Nhan tin
        void Receivee()//Chat thi string, cai tien hon thi khac
        {
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
                Closee();
            }
        }
        //Add message vao khung chat
        void Addmess(string s)
        {
            lsvMessage.Items.Add(new ListViewItem(){ Text = s});
            txtMessage.Clear();
        }
        //Gui tin
        void Sendd()
        {
            if (txtMessage.Text != string.Empty) {//Neu lo chua viet gi vo textbox ma nhan Send
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
        object Deserialize(byte[] data,Type objectType)
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
        private void Client_FormClose(object sender, EventArgs e)
        {
            Closee();
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }
    }
}
