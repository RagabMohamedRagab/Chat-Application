using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalRDay1.Models;
using Microsoft.AspNetCore.Http;
using SignalRDay1.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace SignalRDay1.Hubs {
    public class ChatHub : Hub {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ChattingDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public ChatHub(ChattingDbContext dbContext, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public void ReceiveFromServer(Chat chat)
        {
            // Save Data To Db
            _dbContext.Add<Chat>(chat);
            _dbContext.SaveChanges();
            // Call Back Fun // Send Data To All User.
            Clients.All.SendAsync("SendToClients", chat);

        }
        public void JoinToGroup(string GroupName, string Name)
        {
            Group group = new Group();
            var groups = _dbContext.Groups.ToList();
            if (groups.Any(b => b.Name == GroupName))    // If Group Already Found Then Not Add It .
            {
                var GroupById = groups.FirstOrDefault(b => b.Name == GroupName);
                group.Id = GroupById.Id;
                group.Name = GroupById.Name;
            }
            else
            {
                group.Name = GroupName;
                _dbContext.Add<Group>(group);
                _dbContext.SaveChanges();
            }
            var userId = _httpContextAccessor.HttpContext?.Request.Cookies["UserId"];
            if (!_dbContext.UserGroups.ToList().Any(b => b.SenderId == userId && b.GroupId == group.Id))    // Test Cases if User already in the same Group Skip.. 
            {
                UserGroup userGroup = new UserGroup()
                {
                    GroupId = group.Id,
                    SenderId = userId
                };
                _dbContext.Add<UserGroup>(userGroup);
                _dbContext.SaveChanges();
            }

            Groups.AddToGroupAsync(Context.ConnectionId, GroupName); // Add ConnectionId To Group
            Clients.OthersInGroup(GroupName).SendAsync("NewMemmber", Name, GroupName); // Send Mesg with Name in GroupName
            //  Clients.Group(GroupName).SendAsync("NewMemmber", Name, GroupName); // Send Mesg with Name in GroupName

        }
        public void SendMessageToGroup(string GN, string msg)
        {
            Clients.OthersInGroup(GN).SendAsync("MesgInGroup", msg);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    
        public async override Task OnConnectedAsync()
        {

            var userId = _httpContextAccessor.HttpContext?.Request.Cookies["UserId"];
            // Add ConnectionId with User Id in Db
            UserConnectionId model = new UserConnectionId()
            {
                ConnectionId = Context.ConnectionId.ToString(),
                UserId = userId

            };
            await _dbContext.AddAsync<UserConnectionId>(model);
            await _dbContext.SaveChangesAsync();
            var Gusers = _dbContext.UserGroups.ToList();
            foreach (var guser in Gusers.Where(b => b.SenderId == userId))
            {
                var group = _dbContext.Groups.ToList().SingleOrDefault(b => b.Id == guser.GroupId);
                await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);
            }
        }
  
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var allConnection = _dbContext.userConnectionIds.ToList();
            var user = allConnection.SingleOrDefault(b => b.ConnectionId == Context.ConnectionId);
            _dbContext.userConnectionIds.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        //User Chat 
        public void SendMsg(string recivedId, string senderId, ChatMSGViewModel mesg)
        {
            // var SearchForReciverAndSenderInUserGroup = _dbContext.UserGroups.FirstOrDefault(
            //u => (u.SenderId == senderId && u.RecivedId == recivedId)
            // );
            // string GroupName = string.Empty;
            // Group group = new Group();
            // UserGroup userGroup = new UserGroup();
            // if (SearchForReciverAndSenderInUserGroup is not null)
            // {
            //     var groupId = SearchForReciverAndSenderInUserGroup.GroupId;

            //     if (!_dbContext.Groups.Any(b => b.Id == groupId))
            //     {
            //         group.Name = new Guid().ToString();
            //         _dbContext.Add<Group>(group);
            //         _dbContext.SaveChanges();
            //     }
            // }
            // else
            // {
            //     group.Name =Guid.NewGuid().ToString(); 
            //     _dbContext.Add<Group>(group);
            //     _dbContext.SaveChanges();

            //     userGroup.GroupId = group.Id;
            //     userGroup.RecivedId = recivedId;
            //     userGroup.SenderId = senderId;

            //     _dbContext.Add<UserGroup>(userGroup);
            //     _dbContext.SaveChanges();
            // }
            // // Get ConnectionId For Reciver And Sender ...
            var connectionIdForReciver = _dbContext.userConnectionIds.FirstOrDefault(b => b.UserId == recivedId);
            string _direction = (senderId == _httpContextAccessor.HttpContext?.Request.Cookies["UserId"]) ? "start" : "end";
            try { 
           
            // var connectionIdForSender = _dbContext.userConnectionIds.FirstOrDefault(b => b.UserId == senderId);

            // // Add ConnectionsId To Group 

            // Groups.AddToGroupAsync(connectionIdForReciver.ConnectionId, GroupName);
            // Groups.AddToGroupAsync(connectionIdForSender.ConnectionId, GroupName);

            // // Send Message To Group
           

            // Clients.OthersInGroup(GroupName).SendAsync();

            Clients.Client(connectionIdForReciver.ConnectionId).SendAsync("ReceiveMessage", _direction, mesg);
                       } catch (Exception) {
                Clients.User(recivedId).SendAsync("ReceiveMessage", _direction, mesg);
            }

        }
        //public void Send(string userId, string senderId, ChatMSGViewModel mesg)
        //{
           
        //    string _direction = (senderId == _httpContextAccessor.HttpContext?.Request.Cookies["UserId"]) ? "start" : "end";
        //    
        //}

    }
}
