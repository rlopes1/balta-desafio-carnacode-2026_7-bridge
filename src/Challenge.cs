// DESAFIO: Sistema de Notificações Multi-Plataforma
// PROBLEMA: Um aplicativo precisa exibir notificações em diferentes plataformas (Web, Mobile, Desktop)
// com diferentes tipos de conteúdo (Texto, Imagem, Vídeo). O código atual cria uma explosão de classes
// combinando cada tipo de notificação com cada plataforma

using System;

namespace DesignPatternChallenge
{
    // Contexto: Sistema que renderiza notificações em múltiplas plataformas
    // Cada combinação de tipo + plataforma requer código específico
    
    // Problema: Explosão combinatória de classes
    // 3 tipos × 3 plataformas = 9 classes concretas!
    

    // SOLUÇÃO: Bridge Pattern
    // Separa a interface de renderização da lógica de negócios
    // Cada plataforma tem sua própria implementação de renderização
    // Cada tipo de notificação tem sua própria implementação de renderização
    

   public interface IPlatform
    {
        void Render(string title, string content, string imageUrl = "");
    }

    public abstract class Notification
    {
        protected string title;
        protected string content;
        protected IPlatform notification;

        public Notification (IPlatform notification, string title, string content)
        {
            this.notification = notification;
            this.title = title;
            this.content = content;
        }

        public abstract void Send();    
    }

 
    // Implementações de renderização    
    public class WebPlatform : IPlatform
    {
        public void Render(string title, string content, string imageUrl = "")
        {
            Console.WriteLine($"[Web - HTML] <div class='notification'>");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }
    }

    public class MobilePlatform : IPlatform
    {
        public void Render(string title, string content, string imageUrl = "")
        {
            Console.WriteLine($"[Mobile - Native] Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Icon: notification_icon.png");
        }
    }

    public class DesktopPlatform : IPlatform
    {
        public void Render(string title, string content, string imageUrl = "")
        {
            Console.WriteLine($"[Desktop - Toast] Windows Notification:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine($"╚══════════════════════════╝");
        }
    }
 



    // Implementações de notificação
    
    public class ImageNotification : Notification
    {
        private string imageUrl;

        public ImageNotification(IPlatform notification, string title, string content, string imageUrl) 
            : base(notification, title, content)
        {
            this.imageUrl = imageUrl;
        }

        public override void Send()
        {
            notification.Render(title, content, imageUrl);
        }
    }

    public class TextNotification : Notification
    {
        public TextNotification(IPlatform notification, string title, string content) 
            : base(notification, title, content)
        {
        }

        public override void Send()
        {
            notification.Render(title, content);
        }
    }

    public class VideoNotification : Notification
    {
        private string videoUrl;

        public VideoNotification(IPlatform notification, string title, string content, string videoUrl) 
            : base(notification, title, content)
        {
            this.videoUrl = videoUrl;
        }

        public override void Send()
        {
            notification.Render(title, content, videoUrl);
        }
    }

    


    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Notificações Multi-Plataforma ===\n");

            // Problema: Precisamos de uma classe para cada combinação
            // SOLUÇÃO: Bridge Pattern
            // Separa a interface de renderização da lógica de negócios
            // Cada plataforma tem sua própria implementação de renderização
            // Cada tipo de notificação tem sua própria implementação de renderização
            
            // Implementações de renderização
            var web = new WebPlatform();
            var mobile = new MobilePlatform();
            var desktop = new DesktopPlatform();
            
            // Implementações de notificação
            var textWeb = new TextNotification(web, "Novo Pedido", "Você tem um novo pedido");
            textWeb.Send();
            Console.WriteLine();

            var textMobile = new TextNotification(mobile, "Novo Pedido", "Você tem um novo pedido");
            textMobile.Send();
            Console.WriteLine();

            var imageWeb = new ImageNotification(web, "Promoção", "50% de desconto!", "promo.jpg");
            imageWeb.Send();
            Console.WriteLine();

            var videoMobile = new VideoNotification(mobile, "Tutorial", "Aprenda a usar o app", "tutorial.mp4");
            videoMobile.Send();
            Console.WriteLine();

            Console.WriteLine("=== PROBLEMAS ===");
            Console.WriteLine("✗ Explosão de classes: 3 tipos × 3 plataformas = 9 classes");
            Console.WriteLine("✗ Código duplicado entre classes similares");
            Console.WriteLine("✗ Adicionar novo tipo = criar 3 classes (uma por plataforma)");
            Console.WriteLine("✗ Adicionar nova plataforma = criar 3 classes (uma por tipo)");
            Console.WriteLine("✗ As duas hierarquias (tipo e plataforma) estão fortemente acopladas");
            Console.WriteLine();

            // Perguntas para reflexão:
            // - Como separar a abstração (tipo de notificação) da implementação (plataforma)?
            // - Como adicionar novos tipos de notificação sem criar classes para cada plataforma?
            // - Como adicionar novas plataformas sem modificar os tipos existentes?
            // - Como evitar a explosão combinatória de classes?
        }
    }
}
