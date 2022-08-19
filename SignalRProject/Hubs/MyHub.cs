using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRProject.Hubs
{
    public class MyHub : Hub // Backend ile frontend arasında durmaksızın akışı sağlayabilecek class a Hub denir. Hub class ından kalıtım alınarak socketler oluşturulabilir. Hub verilerin iletim merkezidir. 
    {
        static List<string> clients = new List<string>(); // Tüm clientların listesini topladığımız nesne

        public async Task SendMessageAsync(string message) => await Clients.All.SendAsync("receiveMessage", message); // Client uygulamasındaki receiveMessage fonksiyonu çalıştıırlır ve message parametresi yollanır.

        //Herhangi bir client socket e bağlandığında yakalanan event. Loglar için çok kullanışlıdır.
        public override async Task OnConnectedAsync()  
            
        {
            clients.Add(Context.ConnectionId); // client objesine günceller client listesi eklenir.
            await Clients.All.SendAsync("clients", clients); // client üzerinde clients fonksiyonu çalıştırılır. cilents objesi parametre olarak gönderilir.
            await Clients.All.SendAsync("userJoined", Context.ConnectionId); // sisteme eklenen kullanıcıyı client a userKoined fonksiyonu ile gönderir.
           
        }

        // Herhangi bir client socket e bağlantısından koptuğunda yakalanan event. Loglar için çok kullanışlıdır. 
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            clients.Remove(Context.ConnectionId); // client objesinden sistemden çıkış yapan kullanıcıyı siler.
            await Clients.All.SendAsync("clients", clients);  // client üzerinde clients fonksiyonu çalıştırılır. cilents objesi parametre olarak gönderilir.
            await Clients.All.SendAsync("userLeaved", Context.ConnectionId); // sisteme çıkan kullanıcıyı client a userLeaved fonksiyonu ile gönderir.
        }

    }

  
}

