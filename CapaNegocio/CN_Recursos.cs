using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CapaNegocio
{
    public class CN_Recursos
    {

        public static string GenerarClave() { 
        
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
        }

        public static string ConvertirSha256(string texto) {

            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {

            bool resultado = false;

            try {
            
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From =  new MailAddress("alejandroangel148@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml= true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("alejandroangel148@gmail.com", "rezrythzhkfcgykd"),
                    Host= "smtp.gmail.com",
                    Port = 587,
                    EnableSsl= true,
                };

                smtp.Send(mail);
                resultado = true;


            } catch {

                resultado = false;

            }


            return resultado;
        
        }





    }
}
