using APP.Resources;
using Bunifu.UI.WinForms;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace APP
{
    public partial class Chat : Form
    {
        private Thread receiveThread;
        private TcpClient client;
        private string username, email, name;
        private ChatlistUser[] chatlistUsers;
        private UserFriend[] listFriends;
        private string[] notiList;
        private StreamReader reader;
        private bool isRunning = false;
        private StreamWriter writer;
        Image receivedImage;
        //list bạn bè tạm
        private string[] friendList;
        private string[] userList;
        private List<string> chatRoomList;
        string roomList;
        // tạo ra 1 list các pair chatlistuser và flowlayoutpenl
        private Dictionary<ChatlistUser, FlowLayoutPanel> chatListUserToFlowLayoutPanelMap = new Dictionary<ChatlistUser, FlowLayoutPanel>();
        private Dictionary<ChatlistUser, string> keyValuePairs = new Dictionary<ChatlistUser, string>();
        private Dictionary<string, UserFriend> keyValuePairs2 = new Dictionary<string, UserFriend>();
        private Dictionary<string, string[]> keyValuePairs3 = new Dictionary<string, string[]>();
        int Request;
        string talkWith;
        CallWaitRe Reform;
        CallWaitSe Seform;
        Call Callform;
        private WaveIn waveIn;
        private WaveOut waveOut;
        private BufferedWaveProvider bufferedWaveProvider;
        private NetworkStream stream;
        public Chat(TcpClient tcpClient, string username, string Email)
        {
            this.client = tcpClient;
            this.username = username;
            this.isRunning = true;
            InitializeComponent();
            this.Load += LoadDatatoApp;
            EmotionAndImage.Visible = false;
            reddd.Visible = false ;
            email = Email;
        }

        private void LoadData()
        {

            waveIn = new WaveIn();
            waveIn.WaveFormat = new WaveFormat(44100, 1); // Ensure format matches server
            waveIn.BufferMilliseconds = 50;
            waveIn.DataAvailable += WaveIn_DataAvailable;

            waveOut = new WaveOut();
            bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1));
            waveOut.Init(bufferedWaveProvider);



            
            
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;
            //yêu cầu danh sách ds user
            writer.WriteLine($"Name|{username}");
            string Uname = reader.ReadLine();
            bunifuLabel1.Text = Uname;
            name = Uname;
            writer.WriteLine("ListUser");

            //Nhận ds user
            string temp1 = reader.ReadLine();
            userList = temp1.Split('|');

            //yêau cầu nhận ds bạn bè
            writer.WriteLine("Listfriend");


            //nhận ds bạn bè
            string temp = reader.ReadLine();
            if (temp == "Null")
            {
            }
            else
            {
                friendList = temp.Split('|');
            }

            detailAn();
            Colorcolumn1();
            listfriendshow();
            listconversation();
            chatRoomList = new List<string>();

            writer.WriteLine("GroupName");

            string[] groupList = null;
            string grouplisttemp = reader.ReadLine();
            if (grouplisttemp != "Null")
            {
                groupList = grouplisttemp.Split('|');
                foreach (string item in groupList)
                {
                    chatRoomList.Add(item);
                }
                chatlistUsers = new ChatlistUser[groupList.Length];
                for (int i = 0; i < chatlistUsers.Length; i++)
                {
                    if (groupList[i] != "")
                    {
                        Invoke(new Action(() =>
                        {
                            chatlistUsers[i] = new ChatlistUser();
                            Image image = Image.FromFile("Resources\\4.jpg");
                            // tạo ra 1 chatlistuser
                            chatlistUsers[i].username = groupList[i];
                            chatlistUsers[i].userimage = image;
                            // tạo ra 1 flowlayoutpanel tương ứng với chatlistuser ở trên
                            FlowLayoutPanel tempFlowLayoutPanel = createFlowlayoutPanel();
                            //getRoomKey(groupList[i]);
                            keyValuePairs.Add(chatlistUsers[i], getRoomKey(groupList[i]));
                            chatListUserToFlowLayoutPanelMap.Add(chatlistUsers[i], tempFlowLayoutPanel);
                            // gán sự kiện hiển thị flowlayoutpanel khi ấn vào chatlistuser tương ứng
                            chatlistUsers[i].MouseDown += click_show_panel;
                            ContactNameConversation.Text = "Unknow";
                            ContactNameMore.Text = "Unknow";
                            // thêm chatlistuser vào panel bên trái 
                            ChatlistFlowPanel.Controls.Add(chatlistUsers[i]);
                        }));
                    }
                }

                string str1 = "";
                int j = 0;
                while (str1 != "Null")
                {
                    str1 = reader.ReadLine();
                    string[] userlisttemp = str1.Split('|');
                    getRoomKey(groupList[j]);
                    keyValuePairs3.Add(groupList[j], userlisttemp);
                    j++;
                }
            }

            writer.WriteLine("LoadNotification");

            string temp3 = reader.ReadLine();
            if (temp3 != "Null")
            {
                notiList = temp3.Split('|');
                for (int i = 0; i < notiList.Length; i++)
                {
                    if (keyValuePairs2.ContainsKey(notiList[i]))
                    {
                        keyValuePairs2[notiList[i]].setButton("Chấp nhận");
                    }
                }
            }

            string temp4 = "";
            if (groupList != null)
            {
                for (int i = 0; i < groupList.Length; i++)
                {
                    if (groupList[i] != "")
                    {
                        temp4 += getRoomKey(groupList[i]) + "|";
                    }
                }
            }

            writer.WriteLine("LoadMessageForGroup");
            writer.WriteLine(temp4);

            string receive2 = "";
            string receive3 = "";
            //bool check = true;
            while (receive2 != "Null")
            {
                receive2 = reader.ReadLine();
                foreach (var item in keyValuePairs)
                {
                    if (item.Value == receive2)
                    {
                        receive3 = "";
                        receive3 = reader.ReadLine();
                        var remessage = new ReMessage();
                        var semessage = new SeMessage();
                        if (!string.IsNullOrEmpty(receive3))
                        {
                            string checkSender = receive3.Substring(0, receive3.IndexOf(":"));
                            if (checkSender == username)
                            {
                                semessage.message = receive3;
                                chatListUserToFlowLayoutPanelMap[item.Key].Controls.Add(semessage);
                            }
                            else
                            {
                                remessage.messgae = receive3;
                                chatListUserToFlowLayoutPanelMap[item.Key].Controls.Add(remessage);
                            }
                        }
                        break;
                    }
                }
            }

            convertRoomList();
            writer.WriteLine("LoadMessage");
            writer.WriteLine(roomList);

            string receive = "";
            string receive1 = "";
            //bool check = true;
            string[] temp2;
            while (receive != "Null")
            {
                receive = reader.ReadLine();
                foreach (var item in keyValuePairs)
                {
                    if (item.Value == receive)
                    {
                        receive1 = "";
                        receive1 = reader.ReadLine();
                        temp2 = receive1.Split('|');
                        foreach (var item2 in temp2)
                        {
                            var remessage = new ReMessage();
                            var semessage = new SeMessage();
                            if (!string.IsNullOrEmpty(item2))
                            {
                                string checkSender = item2.Substring(0, item2.IndexOf(":"));
                                if (checkSender == username)
                                {
                                    semessage.message = item2;
                                    chatListUserToFlowLayoutPanelMap[item.Key].Controls.Add(semessage);
                                }
                                else
                                {
                                    remessage.messgae = item2;
                                    chatListUserToFlowLayoutPanelMap[item.Key].Controls.Add(remessage);
                                }
                            }
                        }
                        break;
                    }
                }
            }

            Thread thread = new Thread(Receive);
            thread.Start();
            thread.IsBackground = true;
        }
        //cấu trúc 1 tin nhắn được gửi đi 
        private void LoadDatatoApp(object sender, EventArgs e)
        {
            Thread loadAppThread = new Thread(() => ShowLoadingMessageBoxAsync());
            loadAppThread.Start();

            LoadData();

            loadAppThread.Join();
        }
        private void ShowLoadingMessageBoxAsync()
        {
            // Hiển thị MessageBox thông báo chờ
            MessageBox.Show("Vui lòng đợi, đang tải dữ liệu...", "Đang tải", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        class tinNhan
        {
            public string sender { get; set; }
            public string contentMess { get; set; }
            public string receiver { get; set; }
            public string roomkey { get; set; }
        }
        private void detailAn()
        {
            Detail.Visible = false;
            listFriend.Visible = false;
            MoreConversation.Visible = false;
            searchchat.Visible = false;
        }
        private void Colorcolumn1()
        {
           
                conversation.BackColor = Color.LightGray;
            
        }
        private void Chat_Load(object sender, EventArgs e)
        {
            
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel4_Click(object sender, EventArgs e)
        {
           
          
            
        }
        
        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
           
            if (Detail.Visible == false)
            {
                Detail.Visible = true;
                listFriend.Visible = false;
                Chatlist.Visible = false;
                conversation.BackColor = Color.WhiteSmoke;
                add.BackColor = Color.WhiteSmoke;

            }
            else if (Detail.Visible == true)
            {
                Detail.Visible = false;
            }

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            ChangeNameAndImae form = new ChangeNameAndImae(username,email,name,writer,reader);
            form.ShowDialog();
        }

        private void listFriend_Click(object sender, EventArgs e)
        {
            
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (Chatlist.Visible == false)
            {
                Chatlist.Visible = true;
                listFriend.Visible = false;
                Detail.Visible = false;
                add.BackColor = Color.WhiteSmoke;
                conversation.BackColor= Color.LightGray;
            }
            else
            {
                Chatlist.Visible = false;
                conversation.BackColor = Color.WhiteSmoke;
            }
            

          
        }
        private void listfriendshow()
        {
            if (friendList != null)
            {
                if (friendList.Length == 0)
                {
                    return;
                }
                listFriends = new UserFriend[userList.Length];
                for (int i = 0; i < listFriends.Length; i++)
                {
                    bool isExist = false;
                    foreach (var item in friendList)
                    {
                        if (userList[i] == item)
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        if (userList[i] != "")
                        {
                            if (userList[i] != username)
                            {
                                listFriends[i] = new UserFriend(client, username);
                                Image image = Image.FromFile("Resources\\4.jpg");
                                listFriends[i].userimage = image;
                                listFriends[i].username = userList[i];
                                keyValuePairs2.Add(userList[i], listFriends[i]);
                                if (flowLayoutPanelListfriend.Controls.Count < 0)
                                {
                                    flowLayoutPanelListfriend.Controls.Clear();
                                }
                                else
                                {
                                    flowLayoutPanelListfriend.Controls.Add(listFriends[i]);
                                }
                            }
                        }
                    }
                }
            }
            else if (friendList == null)
            {
                listFriends = new UserFriend[userList.Length];
                for (int i = 0; i < listFriends.Length; i++)
                {
                    if (userList[i] != "")
                    {
                        if (userList[i] != username)
                        {
                            listFriends[i] = new UserFriend(client, username);
                            Image image = Image.FromFile("Resources\\4.jpg");
                            listFriends[i].userimage = image;
                            listFriends[i].username = userList[i];
                            keyValuePairs2.Add(userList[i], listFriends[i]);
                            if (flowLayoutPanelListfriend.Controls.Count < 0)
                            {
                                flowLayoutPanelListfriend.Controls.Clear();
                            }
                            else
                            {
                                flowLayoutPanelListfriend.Controls.Add(listFriends[i]);
                            }
                        }
                    }
                }
            }
        }
        private void listconversation()
        {
            if (friendList != null)
            {
                chatlistUsers = new ChatlistUser[friendList.Length];
                for (int i = 0; i < chatlistUsers.Length; i++)
                {
                    if (friendList[i] != "")
                    {
                        chatlistUsers[i] = new ChatlistUser();
                        Image image = Image.FromFile("Resources\\4.jpg");
                        // tạo ra 1 chatlistuser
                        chatlistUsers[i].username = friendList[i];
                        chatlistUsers[i].userimage = image;
                        // tạo ra 1 flowlayoutpanel tương ứng với chatlistuser ở trên
                        FlowLayoutPanel tempFlowLayoutPanel = createFlowlayoutPanel();
                        getRoomKey(username, friendList[i]);
                        keyValuePairs.Add(chatlistUsers[i], getRoomKey(username, friendList[i]));
                        chatListUserToFlowLayoutPanelMap.Add(chatlistUsers[i], tempFlowLayoutPanel);
                        // gán sự kiện hiển thị flowlayoutpanel khi ấn vào chatlistuser tương ứng
                        chatlistUsers[i].MouseDown += click_show_panel;
                        ContactNameConversation.Text = "Unknow";
                        ContactNameMore.Text = "Unknow";
                        // thêm chatlistuser vào panel bên trái 
                        ChatlistFlowPanel.Controls.Add(chatlistUsers[i]);
                    }
                }
            }
        }
        private void click_show_panel(object sender, EventArgs e)
        {
            //bắt sự kiện xem chatlistuser nào được ấn
            ChatlistUser clickedChatListUser = (ChatlistUser)sender;    
            if (chatListUserToFlowLayoutPanelMap.ContainsKey(clickedChatListUser))
            {
                // tìm flowlayoutpanel tương ứng để hiển thị lên
                foreach (var item in chatListUserToFlowLayoutPanelMap)
                {
                    if (item.Key == clickedChatListUser)
                    {
                        item.Value.Visible = true;
                        bunifuPanel12.Visible = true;
                        bunifuTextBox3.Clear();
                        ContactNameConversation.Text = item.Key.username;
                        ContactNameMore.Text = item.Key.username;
                        talkWith = item.Key.username;
                        
                        if(clickedChatListUser.username == ContactNameConversation.Text)
                        {
                            clickedChatListUser.BackColor = Color.WhiteSmoke;
                        }
                    }
                    else 
                    {
                        item.Value.Visible = false;
                        item.Key.BackColor = Color.White;

                    }
                }
            }
        }
        // tạo ra một flowlayoutpanel 
        private FlowLayoutPanel createFlowlayoutPanel()
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Location = new Point(0, 183);
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.AllowDrop = true;
            flowLayoutPanel.BackColor = Color.White;
            flowLayoutPanel.Dock = DockStyle.Fill;
            panel2.Controls.Add(flowLayoutPanel);
            //flowLayoutPanel.AutoSize = true;
            //flowLayoutPanel.Size = new Size(225, 718);
            flowLayoutPanel.Visible = false;
            return flowLayoutPanel;
        }
        private void add_Click(object sender, EventArgs e)
        {
            if (listFriend.Visible == false || Detail.Visible == true)
            {
                Detail.Visible = false;
                Chatlist.Visible = false;
                listFriend.Visible = true;
                add.BackColor = Color.LightGray;
                conversation.BackColor = Color.WhiteSmoke;
                reddd.Visible = false;

            }
            else if (listFriend.Visible == true)
            {
                listFriend.Visible = false;
               add.BackColor = Color.WhiteSmoke;
            }

        }

        private void More_Click(object sender, EventArgs e)
        {
            if(MoreConversation.Visible == false)
            {
                MoreConversation.Visible = true;
            }
            else
            {
                MoreConversation.Visible = false;
            }
            
        }
        private void convertRoomList()
        {
            if (friendList != null)
            {
                foreach (var item in friendList)
                {
                    string temp = getRoomKey(username, item);
                    roomList += temp + "|";
                }
            }
        }
        private string getRoomKey(string username1 = "", string username2 = "")
        {
            int total = 0;

            foreach (char item in username1)
            {
                total += (int)item;
            }
            foreach (char item in username2)
            {
                total += (int)item;
            }
            return total.ToString();
        }
        // gửi tin nhắn đển server
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (friendList.Contains(ContactNameConversation.Text))
            {
                tinNhan newMsg = new tinNhan { sender = username, contentMess = bunifuTextBox3.Text, receiver = ContactNameConversation.Text, roomkey = getRoomKey(username, ContactNameConversation.Text) };
                string stringMess = JsonConvert.SerializeObject(newMsg);
                writer.WriteLine("Message");
                writer.WriteLine(stringMess);
                string messDisplay = $"{username}: " + bunifuTextBox3.Text + "\r\n";
                foreach (var item in chatListUserToFlowLayoutPanelMap)
                {
                    if (item.Key.username == $"{newMsg.receiver}")
                    {

                        SeMessage se = new SeMessage();
                        //System.Windows.Forms.Label nl1 = new System.Windows.Forms.Label();
                        // nl1.Text = messDisplay;
                        se.message = messDisplay;
                        item.Value.Controls.Add(se);
                        bunifuTextBox3.Clear();
                    }
                }
            }
            else if (chatRoomList.Contains(ContactNameConversation.Text))
            {
                string userListStringForGroup = "";
                foreach (var item in keyValuePairs3[ContactNameConversation.Text])
                {
                    userListStringForGroup += item + "|";
                }
                writer.WriteLine("MessageForGroup");
                writer.WriteLine(username);
                writer.WriteLine(bunifuTextBox3.Text);
                writer.WriteLine(ContactNameConversation.Text);
                writer.WriteLine(getRoomKey(ContactNameConversation.Text));
                writer.WriteLine(userListStringForGroup);
                string messDisplay = $"{username}: " + bunifuTextBox3.Text + "\r\n";
                foreach (var item in chatListUserToFlowLayoutPanelMap)
                {
                    if (item.Key.username == $"{ContactNameConversation.Text}")
                    {

                        SeMessage se = new SeMessage();
                        se.message = messDisplay;
                        item.Value.Controls.Add(se);
                        bunifuTextBox3.Clear();
                    }
                }
            }
        }

        private void bunifuPanel11_Click(object sender, EventArgs e)
        {

        }

        

        private void bunifuPanel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
        {
            if (searchchat.Visible == false)
            {
                searchchat.Visible = true;

            }
            else
            {
                searchchat.Visible = false;
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            // gửi yêu cầu ngắt kết nối đến server
            string disconnect = "quit";
            writer.WriteLine(disconnect);
            // đóng kết nối tại client
            client.Close();

            // dừng luồng nhận tin nhắn từ server
            Application.Exit();
        }

        // nhận tin nhắn từ server 
        private void Receive()
        {
            try
            {
                while (isRunning)
                {
                    string messageFromServer = reader.ReadLine();

                    if (messageFromServer == "Message")
                    {
                        string newMsg = reader.ReadLine();
                        tinNhan tinNhan = JsonConvert.DeserializeObject<tinNhan>(newMsg);
                        foreach (var item in chatListUserToFlowLayoutPanelMap)
                        {
                            if (item.Key.username == tinNhan.sender)
                            {
                                Invoke(new Action(() =>
                                {
                                    ReMessage re = new ReMessage();
                                    re.messgae = tinNhan.sender + ": " + tinNhan.contentMess;
                                    item.Value.Controls.Add(re);
                                }));
                            }
                        }
                    }
                    else if (messageFromServer == "MessageForGroup")
                    {
                        string sender = reader.ReadLine();
                        string content = reader.ReadLine();
                        string groupName = reader.ReadLine();
                        foreach (var item in chatListUserToFlowLayoutPanelMap)
                        {
                            if (item.Key.username == groupName)
                            {
                                Invoke(new Action(() =>
                                {
                                    ReMessage re = new ReMessage();
                                    re.messgae = sender + ": " + content;
                                    item.Value.Controls.Add(re);
                                }));
                            }
                        }
                    }
                    else if (messageFromServer == "Image")
                    {
                        string senderName = reader.ReadLine();
                        string imageData = reader.ReadLine();


                        receivedImage = StringToImage(imageData);
                        string savePath = "Resources\\";
                        string fileName = $"{senderName}_{DateTime.Now:yyyyMMddHHmmss}.png";
                        string fullPath = System.IO.Path.Combine(savePath, fileName);

                        try
                        {

                            if (!System.IO.Directory.Exists(savePath))
                            {
                                System.IO.Directory.CreateDirectory(savePath);
                            }


                            using (var bmp = new Bitmap(receivedImage))
                            {
                                bmp.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        foreach (var item in chatListUserToFlowLayoutPanelMap)
                        {
                            if (item.Key.username == senderName)
                            {
                                Invoke(new Action(() =>
                                {
                                    ReImage re = new ReImage();
                                    re.image = receivedImage;
                                    /*PictureBox newPictureBox = new PictureBox();
                                    newPictureBox.Size = new Size(250, 250);
                                    newPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                                    newPictureBox.Image = receivedImage;*/
                                    item.Value.Controls.Add(re);
                                }));
                            }
                        }
                    }
                    else if (messageFromServer == "File")
                    {
                        NetworkStream networkStream = client.GetStream();
                        string sendername = reader.ReadLine();
                        string filename = reader.ReadLine();
                        long filesize = Convert.ToInt64(reader.ReadLine());
                        string filePath = Path.Combine("Resources\\", filename);
                        byte[] buffer = new byte[52428800];
                        int bytesRead;
                        long bytesReceived = 0;
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {

                            while (bytesReceived < filesize &&
                               (bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                            {

                                fs.Write(buffer, 0, bytesRead);
                                bytesReceived += bytesRead;

                            }

                        }

                        foreach (var item in chatListUserToFlowLayoutPanelMap)
                        {
                            if (item.Key.username == sendername)
                            {
                                Invoke(new Action(() =>
                                {
                                    ReFile re = new ReFile();
                                    re.filename = filename;
                                    /*PictureBox newPictureBox = new PictureBox();
                                    newPictureBox.Size = new Size(250, 250);
                                    newPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                                    newPictureBox.Image = receivedImage;*/
                                    item.Value.Controls.Add(re);
                                }));
                            }
                        }

                    }
                    else if (messageFromServer == "Icon")
                    {
                        string senderName = reader.ReadLine();
                        string Location = reader.ReadLine();

                        foreach (var item in chatListUserToFlowLayoutPanelMap)
                        {
                            if (item.Key.username == senderName)
                            {
                                Invoke(new Action(() =>
                                {
                                    ReIcon re = new ReIcon();
                                    re.image = Image.FromFile(Location);

                                    item.Value.Controls.Add(re);
                                }));
                            }
                        }
                    }
                    else if (messageFromServer == "AddFriend")
                    {


                        string sender = reader.ReadLine();
                        if (keyValuePairs2.ContainsKey(sender))
                        {
                            Invoke(new Action(() =>
                            {
                                keyValuePairs2[sender].setButton("Chấp nhận");
                                reddd.Visible = true;
                            }));
                        }
                    }
                    else if (messageFromServer == "AcceptedSuccessfullyForReceiver")
                    {






                        string userName = reader.ReadLine();
                        Invoke(new Action(() =>
                        {
                            ChatlistUser temp = new ChatlistUser();
                            Image image = Image.FromFile("Resources\\4.jpg");
                            // tạo ra 1 chatlistuser
                            temp.username = userName;



                            string[] newFriendList = new string[friendList.Length + 1];
                            for (int i = 0; i < friendList.Length; i++)
                            {
                                newFriendList[i] = friendList[i];
                            }
                            newFriendList[friendList.Length] = userName;
                            friendList = newFriendList;




                            temp.userimage = image;
                            // tạo ra 1 flowlayoutpanel tương ứng với chatlistuser ở trên
                            FlowLayoutPanel tempFlowLayoutPanel = createFlowlayoutPanel();
                            getRoomKey(username, userName);
                            keyValuePairs.Add(temp, getRoomKey(username, userName));
                            chatListUserToFlowLayoutPanelMap.Add(temp, tempFlowLayoutPanel);
                            // gán sự kiện hiển thị flowlayoutpanel khi ấn vào chatlistuser tương ứng
                            temp.MouseDown += click_show_panel;
                            // thêm chatlistuser vào panel bên trái 
                            ChatlistFlowPanel.Controls.Add(temp);

                        }));
                        if (keyValuePairs2.ContainsKey(userName))
                        {
                            Invoke(new Action(() =>
                            {
                                flowLayoutPanelListfriend.Controls.Remove(keyValuePairs2[userName]);
                            }));
                        }
                    }
                    else if (messageFromServer == "AcceptedSuccessfullyForSender")
                    {
                        string userName = reader.ReadLine();
                        Invoke(new Action(() =>
                        {
                            ChatlistUser temp = new ChatlistUser();
                            Image image = Image.FromFile("Resources\\4.jpg");
                            // tạo ra 1 chatlistuser
                            temp.username = userName;
                            string[] newFriendList = new string[friendList.Length + 1];
                            for (int i = 0; i < friendList.Length; i++)
                            {
                                newFriendList[i] = friendList[i];
                            }
                            newFriendList[friendList.Length] = userName;
                            friendList = newFriendList;
                            temp.userimage = image;
                            // tạo ra 1 flowlayoutpanel tương ứng với chatlistuser ở trên
                            FlowLayoutPanel tempFlowLayoutPanel = createFlowlayoutPanel();
                            getRoomKey(username, userName);
                            keyValuePairs.Add(temp, getRoomKey(username, userName));
                            chatListUserToFlowLayoutPanelMap.Add(temp, tempFlowLayoutPanel);
                            // gán sự kiện hiển thị flowlayoutpanel khi ấn vào chatlistuser tương ứng
                            temp.MouseDown += click_show_panel;
                            // thêm chatlistuser vào panel bên trái 
                            ChatlistFlowPanel.Controls.Add(temp);
                        }));
                        if (keyValuePairs2.ContainsKey(userName))
                        {
                            Invoke(new Action(() =>
                            {
                                flowLayoutPanelListfriend.Controls.Remove(keyValuePairs2[userName]);
                            }));
                        }
                    }
                    else if (messageFromServer == "CreatedSuccessfully")
                    {
                        string sender = reader.ReadLine();
                        string receivers = reader.ReadLine();
                        string groupName = reader.ReadLine();
                        chatRoomList.Add(groupName);
                        string[] receiverList = receivers.Split('|');

                        Invoke(new Action(() =>
                        {
                            ChatlistUser temp = new ChatlistUser();
                            Image image = Image.FromFile("Resources\\4.jpg");
                            // tạo ra 1 chatlistuser
                            temp.username = groupName;
                            temp.userimage = image;
                            // tạo ra 1 flowlayoutpanel tương ứng với chatlistuser ở trên
                            FlowLayoutPanel tempFlowLayoutPanel = createFlowlayoutPanel();
                            getRoomKey(groupName);
                            keyValuePairs3.Add(groupName, receiverList);
                            chatListUserToFlowLayoutPanelMap.Add(temp, tempFlowLayoutPanel);
                            // gán sự kiện hiển thị flowlayoutpanel khi ấn vào chatlistuser tương ứng
                            temp.MouseDown += click_show_panel;
                            // thêm chatlistuser vào panel bên trái 
                            ChatlistFlowPanel.Controls.Add(temp);
                        }));
                    }
                    else if (messageFromServer == "CreatedFailure")
                    {
                        MessageBox.Show("Tên nhóm đã tồn tại");
                    }
                    else if (messageFromServer == "INCOMINGCALL")
                    {
                        string sender = reader.ReadLine();
                        Invoke(new Action(() =>
                        {
                            Reform = new CallWaitRe(sender, client, writer, reader, username);
                            Reform.Show();
                        }));
                    }
                    else if (messageFromServer == "changename_success")
                    {
                        string newname = reader.ReadLine();
                        MessageBox.Show("Đổi tên thành công");
                        Invoke(new Action(() =>
                        {
                            bunifuLabel1.Text = newname;
                        }));
                    }
                    else if (messageFromServer == "NguoiGoiCup")
                    {
                        string sender = reader.ReadLine();
                        Invoke(new Action(() =>
                        {
                            Reform.Close();
                        }));
                    }
                    else if (messageFromServer == "NguoiNhanCup")
                    {
                        string sender = reader.ReadLine();
                        Invoke(new Action(() =>
                        {
                            Seform.Close();

                        }));
                    }
                    else if (messageFromServer == "NguoiNhanChapNhan")
                    {
                        string nguoigoi = reader.ReadLine();
                        Invoke(new Action(() =>
                        {
                            Reform.Close();
                            Callform = new Call(nguoigoi, writer, reader, username, waveIn);
                            Callform.Show();

                        }));
                        call();

                    }
                    else if (messageFromServer == "NguoiNhanChapNhanNG")
                    {
                        string nguoinhancuoigoi = reader.ReadLine();
                        Invoke(new Action(() =>
                        {
                            Seform.Close();
                            Callform = new Call(nguoinhancuoigoi, writer, reader, username, waveIn);
                            Callform.Show();

                        }));
                        call();
                    }
                    else if (messageFromServer == "CALLING")
                    {

                        byte[] buffer = new byte[16384];
                        while (true)
                        {
                            try
                            {
                                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                                if (bytesRead == 0)
                                {
                                    break; // Server disconnected
                                }
                                bufferedWaveProvider.AddSamples(buffer, 0, bytesRead);
                            }
                            catch
                            {
                                break; // Handle connection errors
                            }
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                
            }
        }
        private void call()
        {
            stream = client.GetStream();
            writer.WriteLine("CALLING");
            writer.WriteLine(username + "|" + ContactNameConversation.Text);
            waveIn.StartRecording();
           
            waveOut.Play();

            
        }
        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            try
            {
                stream.Write(e.Buffer, 0, e.BytesRecorded);
            }
            catch
            {
              
            }
        }
        private Image StringToImage(string imageDataString)
        {
            try
            {
                // Chuyển đổi chuỗi base64 thành mảng byte
                byte[] imageBytes = Convert.FromBase64String(imageDataString);
                // Tạo một MemoryStream từ mảng byte
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    // Tạo một đối tượng hình ảnh từ MemoryStream
                    Image image = Image.FromStream(ms);
                    return image;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                return null;
            }
        }
        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            writer.WriteLine("LogOut");
            LogIn lg = new LogIn();
            lg.Show();
            this.Close();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in username)
            {
                
            }
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bunifuTextBox2.Text))
            {
                foreach (ChatlistUser item in chatlistUsers)
                {
                    if (item != null)
                    {
                        item.Visible = true;
                    }
                }
            }
            foreach (ChatlistUser item in chatlistUsers)
            {
                if (item != null)
                {
                    if (item.username.IndexOf(bunifuTextBox2.Text) != -1)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
            }
        }
        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuTextBox4_Enter(object sender, EventArgs e)
        {
            FlowLayoutPanel temp = new FlowLayoutPanel();
            foreach (var item in chatListUserToFlowLayoutPanelMap)
            {
                if (item.Key.username == ContactNameConversation.Text)
                {
                    temp = item.Value;
                    break;
                }
            }
            if (temp != null)
            {
                foreach (Control control in temp.Controls)
                {
                    if (control is SeMessage se && se.message.IndexOf(bunifuTextBox4.Text) != -1)
                    {
                        se.Visible = true;
                    }
                    else if (control is ReMessage re && re.messgae.IndexOf(bunifuTextBox4.Text) != -1)
                    {
                        re.Visible = true;
                    }
                    else
                    {
                        control.Visible = string.IsNullOrEmpty(bunifuTextBox4.Text);
                    }
                }
            }
        }

        private void bunifuTextBox4_TextChange(object sender, EventArgs e)
        {
            FlowLayoutPanel temp = new FlowLayoutPanel();
            foreach (var item in chatListUserToFlowLayoutPanelMap)
            {
                if (item.Key.username == ContactNameConversation.Text)
                {
                    temp = item.Value;
                    break;
                }
            }
            if (temp != null)
            {
                
                foreach (Control control in temp.Controls)
                {
                    if (control is SeMessage se && se.message.IndexOf(bunifuTextBox4.Text) != -1)
                    {
                        se.Visible = true;
                    }
                    else if (control is ReMessage re && re.messgae.IndexOf(bunifuTextBox4.Text) != -1)
                    {
                        re.Visible = true;
                    }
                    else
                    {
                        control.Visible = string.IsNullOrEmpty(bunifuTextBox4.Text);
                    }
                }
            }
        }
        private string ImageToString(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        private void bunifuSendFile_Click(object sender, EventArgs e)
        {
            if(EmotionAndImage.Visible == false)
            {

            EmotionAndImage.Visible = true;
            }
            else if(EmotionAndImage.Visible == true)
            {
                EmotionAndImage.Visible = false;
            }
            
            
            /*
            */
        }
        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    foreach (var item in chatListUserToFlowLayoutPanelMap)
                    {
                        if (item.Key.username == $"{ContactNameConversation.Text}")
                        {
                            try
                            {
                                FileInfo fileInfo = new FileInfo(filePath);
                                string fileName = fileInfo.Name;
                                long fileSize = fileInfo.Length;

                                SeFile sef = new SeFile();
                                sef.filename = fileName;
                                Invoke(new Action(() =>
                                {

                                    item.Value.Controls.Add(sef);
                                }));
                                // Get the network stream from the client


                                // Create a StreamWriter to write metadata to the network stream
                                StreamWriter writer = new StreamWriter(client.GetStream());
                                
                                    writer.AutoFlush = true;
                                    writer.WriteLine("File");
                                    writer.WriteLine(username + "|" + ContactNameConversation.Text);
                                    writer.WriteLine(fileName);
                                    writer.WriteLine(fileSize.ToString());
                                
                                    byte[] buffer = new byte[52428800]; // 50MB buffer
                                    int bytesRead;
                                    NetworkStream stream = client.GetStream();

                                // Open the file and send its content using BinaryWriter
                                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                { 
                               
                                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        stream.Write(buffer, 0, bytesRead);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Handle or log the exception
                                MessageBox.Show($"An error occurred while sending the file: {ex.Message}");
                            }
                        }
                    }
                }
            }
            EmotionAndImage.Visible = false;
        }   
        
        public static string ImageToBase64String(Image image)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Lưu hình ảnh vào MemoryStream dưới dạng JPEG
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    // Chuyển đổi sang chuỗi base64
                    byte[] imageBytes = ms.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                return null;
            }
        }
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {

            AddGroup form = new AddGroup(friendList, writer, username);

           

            form.ShowDialog();
            


        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bunifuTextBox1.Text))
            {
                if (listFriends.Length != 0)
                {
                    foreach (UserFriend item in listFriends)
                    {
                        if (item != null)
                        {
                            item.Visible = true;
                        }
                    }
                }
                else
                {

                }
            }
            foreach (UserFriend item in listFriends)
            {
                if (item != null)
                {
                    if (item.username.IndexOf(bunifuTextBox1.Text) != -1)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
            }
        }

        private void SendImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in chatListUserToFlowLayoutPanelMap)
                    {
                        if (item.Key.username == $"{ContactNameConversation.Text}")
                        {
                            SeImage se = new SeImage();
                            PictureBox pictureBox = new PictureBox();
                            pictureBox.Size = new Size(250, 250);
                            //pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                            pictureBox.Image = Image.FromFile(ofd.FileName);
                            string savePath = "Resources\\" + Path.GetFileName(ofd.FileName); // Thay đổi đường dẫn tại đây

                            // Lưu ảnh vào đường dẫn
                            //pictureBox.Image.Save(savePath);

                            se.image = Image.FromFile(ofd.FileName);
                            se.image.Save(savePath);
                            Invoke(new Action(() =>
                            {

                                item.Value.Controls.Add(se);
                            }));
                            try
                            {

                                //string ImageDataString = ImageToString(pictureBox.Image);


                                StreamWriter writer = new StreamWriter(client.GetStream());
                                string imageData = ImageToBase64String(se.image);
                                //buffer = ImageToByteArray(pictureBox.Image);
                                writer.AutoFlush = true;
                                writer.WriteLine("Image");
                                writer.WriteLine(username + "|" + ContactNameConversation.Text);
                                //writer.WriteLine(Path.GetFileName(ofd.FileName));

                                //writer.WriteLine(ImageDataString);
                                writer.WriteLine(imageData);


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            EmotionAndImage.Visible = false;
        }
        private void SendIcon(string Location)
        {
            foreach (var item in chatListUserToFlowLayoutPanelMap)
            {
                if (item.Key.username == $"{ContactNameConversation.Text}")
                {
                    Image icon = Image.FromFile(Location);

                    SeIcon se = new SeIcon();
                    se.image = icon;
                    Invoke(new Action(() =>
                    {

                        item.Value.Controls.Add(se);
                    }));
                    try
                    {

                        //string ImageDataString = ImageToString(pictureBox.Image);


                        StreamWriter writer = new StreamWriter(client.GetStream());
                        string imageData = ImageToBase64String(se.image);
                        //buffer = ImageToByteArray(pictureBox.Image);
                        writer.AutoFlush = true;
                        writer.WriteLine("Icon");
                        writer.WriteLine(username + "|" + ContactNameConversation.Text);
                        //writer.WriteLine(Path.GetFileName(ofd.FileName));

                        //writer.WriteLine(ImageDataString);
                        writer.WriteLine(Location);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            SendIcon(bunifuImageButton7.Tag.ToString());
           
        }

        private void ClickIcon(object sender, EventArgs e)
        {
            BunifuImageButton bt = (BunifuImageButton)sender;   
            SendIcon(bt.Tag.ToString());
            EmotionAndImage.Visible = false;
        }

        private void btn_changepass_Click(object sender, EventArgs e)
        {
            ChangePassword form = new ChangePassword(username, email,writer,reader);
            form.ShowDialog();
        }

        private void bunifuImageButton18_Click(object sender, EventArgs e)
        {
            foreach (var item in chatListUserToFlowLayoutPanelMap)
            {
                if (item.Key.username == $"{ContactNameConversation.Text}")
                {
                    try
                    {

                        //string ImageDataString = ImageToString(pictureBox.Image);


                        StreamWriter writer = new StreamWriter(client.GetStream());
                        
                        
                        writer.AutoFlush = true;
                        writer.WriteLine("INCOMINGCALL");
                        writer.WriteLine(username + "|" + ContactNameConversation.Text);
                        
                        
                        Seform = new CallWaitSe(talkWith, client, writer, reader,username);
                        Seform.Show();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
           
            
        }
    }
}