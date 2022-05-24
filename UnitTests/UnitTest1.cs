using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceCenter.Logic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SuccessfulAuthorization() //Авторизация с существующими данными пользователя (Логин и пароль)
        {
            string login = "smirnova@mail.ru";
            string password = "12345AA";
            bool expected = true;            //Ожидаемый результат

            bool actual = new Authorization().authorize(login, password); //Результат работы функции авторизации
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void UnuccessfulAuthorization() //Авторизация с ошибочными данными пользователя
        {
            string login = "smirnova@mail.ru";
            string password = "12345AB";
            bool expected = false;

            bool actual = new Authorization().authorize(login, password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NullAuthorization() //Авторизация, когда данные пользователя не были введены
        {
            string login = "";
            string password = "";
            bool expected = false;

            bool actual = new Authorization().authorize(login, password);
            Assert.AreEqual(expected, actual);
        }
    }
}
