using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceCenter.Database;
using ServiceCenter.DataClasses;

namespace UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void usingExistingData() //Проверка с существующими данными пользователя (Логин и пароль)
        {
            string login = "corneev@mail.ru";
            string password = "12345AB";
            Staff expected = new Staff(2, "Артём", "Корнеев", "Мастер");//Ожидаемый результат
            DBClass db = new DBClass();
            Staff actual = db.getStaff(login, password); //Результат работы функции авторизации
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void usingNotExistingData() //Авторизация с ошибочными данными пользователя
        {
            string login = "corneev@mail.ru";
            string password = "12345ABA";
            Staff expected = new Staff(0, null, null, null);
            DBClass db = new DBClass();
            Staff actual = db.getStaff(login, password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void usingNullData() //Авторизация, когда данные пользователя не были введены
        {
            string login = "";
            string password = "";
            Staff expected = new Staff(0, null, null, null);
            DBClass db = new DBClass();
            Staff actual = db.getStaff(login, password);
            Assert.AreEqual(expected, actual);
        }
    }
}
