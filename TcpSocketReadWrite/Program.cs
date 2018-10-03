using System;
using System.IO;
using System.Net.Sockets;

namespace TcpSocketReadWrite
{
    class Program
    {
        public static int Main()
        {
            //Cihazın Ip adresi
            const string host = "192.168.40.19";
            //Cihazın portu
            const int port = 7001;
            //Tcp client ile cihaza bağlantı kuruluyor
            using (var socket = new TcpClient(host, port))
            {
                //cihaza tcp/ip üzerinden veri göndermek ve almak için streami alıyoruz
                var stream = socket.GetStream();
                //cihaza komut göndermek için streamwriter sınıfından yararlanıyoruz
                using (var output = new StreamWriter(stream))
                {
                    //cihazdan veri almak için streamreader sınıfını kullanıyoruz
                    using (var input = new StreamReader(stream))
                    {
                        //cihaza XB komutunu göndererek enter anlamına gelen \r\n komutlarını örnek olması için gönderdim. Siz cihaza ne komut gönderecekseniz onu yazıp sonuna \r\n ekleyerek enter görevi görmesini sağlayabilirsiniz
                        //ben bu uygulamayı tcp/ip ile networke bağlı kantar indikatörü için geliştirdim. XB komutunu alan cihaz geriye ağırlık verisini dönüyordu
                        output.Write("XB\r\n");
                        output.Flush();
                        //cihaza komut gönderdikten sonra reader ile dönen veriyi yakalayarak üzerinde çeşitli replace işlemleri yaptım. Burada sizin için logic ne ise onu kodlayabilirsiniz.
                        var Newvalue = input.ReadLine().ToUpper().Replace(" ", "").Replace("K", "").Replace("G", "").Replace("B", "");
                        //Dönen datayı ekrana basıyoruz.
                        Console.WriteLine(Newvalue);
                    }
                }
            }
            //uygulama hemen kapanmasın diye bu komutu yazarak bir tuşa basılmasını beklemesi gerektiğini belirtiyoruz.
            Console.ReadKey();
            return 0;
        }
    }
}