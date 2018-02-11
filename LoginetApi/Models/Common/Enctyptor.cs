using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using System.IO;

namespace LoginetApi.Models.Common
{
    // В задании ничего не сказано про то, что мы знаем о пользователях сервиса => авторизацию решил не применять.
    // для скрытия личных данных,  можно использовать либо  симметричное, либо ассиметричное шифрование. Для ассиметричного шифрования следует хранить ключи наших юзеров,
    // так же  для его использования нужен некий handshake между сервисом и пользователем.
    // решил, что наш сервис будет являться REST сервисом, те не будет хранить никаких "состояний" и сессий, поэтому остановимся на симметричном шифровании.
    // подрузамеваем, что пользователи сервиса - доверенные лица, и мы скрываем личные данные от только третьих лиц, которые не являются нашими клиентами,
    // поэтому пользователь API должен знать ключ для дешифровки email'a
    //
    // в качестве источника информации использовал несколько статей с хабра и эту книгу:
    //https://the-eye.eu/public/Books/IT%20Various/pro_asp.net_web_api_security.pdf
    // Выбрал в качестве шифрования симметричное шифрование Рэндала.
    public static class Enctyptor
    {
        public static string key { get { return "br0Bgf/UTietg3b+Z4gaOzvInWnMIgvTAEJ/FkJG6Zs="; } } // рандомный ключ и вектор, который сгенерил во время тестирования
        public static string vector { get { return "Nsm9KqrHkhXVAkOvm9q1kA=="; }
        }

        public static string Decrypt(string data, string key, string initializingVector)
        {
            using (RijndaelManaged provider = new RijndaelManaged())
            {
                //инициализируем ключ и вектор инициализации для нужд алгоритма Рэндала.
                byte[] keybytes = Convert.FromBase64String(key); 
                byte[] initVector = Convert.FromBase64String(initializingVector);

                byte[] foggyBytes = Convert.FromBase64String(data); //преобразуем base64 строку в массив байтов

                string result = Encoding.UTF8.GetString(Transform(foggyBytes, provider.CreateDecryptor(keybytes, initVector)));

                return result;
            }
        }
        public static string Encrypt(string Data, string key, string initializingVector)
        {
            using (RijndaelManaged provider = new RijndaelManaged())
            {
                //инициализируем ключ и вектор инициализации для нужд алгоритма Рэндала.
                byte[] keybytes = Convert.FromBase64String(key);
                byte[] initVector = Convert.FromBase64String(initializingVector);
                
                byte[] clearBytes = Encoding.UTF8.GetBytes(Data);//преобразуем base64 строку в массив байтов
                byte[] foggyBytes = Transform(clearBytes,provider.CreateEncryptor(keybytes, initVector)); //шифруем данные используя встроенный в .net алгоритм Рэндала.

                string encryptedData = Convert.ToBase64String(foggyBytes);
                Console.WriteLine(encryptedData);
                return encryptedData;

            }
        }
        /// <summary>
        /// Метод шифрует/дешифрует массив байтов, в зависимости от заданного  ICryptoTransform
        /// </summary>
        /// <param name="textBytes"></param>
        /// <param name="transform"></param>
        /// <returns></returns>
        private static byte[] Transform(byte[] textBytes, ICryptoTransform transform)
        {
            using (var buf = new MemoryStream())
            {
                using (var stream = new CryptoStream(buf, transform,CryptoStreamMode.Write))
                {
                    stream.Write(textBytes, 0, textBytes.Length);
                    stream.FlushFinalBlock();
                    return buf.ToArray();
                }
            }
         }
     }
}