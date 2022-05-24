using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceCenter.Logic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SuccessfulAuthorization() //����������� � ������������� ������� ������������ (����� � ������)
        {
            string login = "smirnova@mail.ru";
            string password = "12345AA";
            bool expected = true;            //��������� ���������

            bool actual = new Authorization().authorize(login, password); //��������� ������ ������� �����������
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void UnuccessfulAuthorization() //����������� � ���������� ������� ������������
        {
            string login = "smirnova@mail.ru";
            string password = "12345AB";
            bool expected = false;

            bool actual = new Authorization().authorize(login, password);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NullAuthorization() //�����������, ����� ������ ������������ �� ���� �������
        {
            string login = "";
            string password = "";
            bool expected = false;

            bool actual = new Authorization().authorize(login, password);
            Assert.AreEqual(expected, actual);
        }
    }
}
