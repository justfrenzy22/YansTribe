using core.entities;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Identity;
using server.managers;
// using server.models;


namespace TestBackend
{
    [TestClass]
    public sealed class UserManagerTest
    {
        private UserManager manager;


        public UserManagerTest()
        {
            this.manager = new UserManager("Server=mssqlstud.fhict.local;Database=dbi546373_facebook;User Id=dbi546373_facebook;Password=secret;TrustServerCertificate=True;");
        }

        // [TestMethod]
        // [Obsolete]
        // public void CreateUser_UserAlreadyExistsByEmail()
        // {

        //     Exception? ex = null;
        //     try
        //     {
        //         this.manager.create_user("testuser", "test@example.com", "hashed_password", "Test User", "bio", "pfp.png", "USA", "test.com");
        //     }
        //     catch (Exception e)
        //     {
        //         ex = e;
        //     }

        //     Assert.IsNotNull(ex);
        //     Assert.AreEqual("User with the same email or username already exists", ex?.Message);
        // }

        

        [TestMethod]
        public void GetUserByUsername()
        {
            string username = "testuser";

            User? user = this.manager.get_user_by_username(username);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetError_UserByUsernameAlreadyExists () {
            string username = "testuser1asdsadadadasdasdasda";

            User? user = this.manager.get_user_by_username(username);

            Assert.IsNull(user);
        }

        [TestMethod]
        public void GetUserByEmail() {
            string email = "test@example.com";

            User? user = this.manager.get_user_by_email(email);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetError_UserByEmailAlreadyExists () {
            string email = "test@email.com";

            User? user = this.manager.get_user_by_email(email);

            Assert.IsNull(user);
        }

        

        // [TestMethod]
        // [Obsolete]
        // public void CreateUser_ShouldReturnID_WhenUserIsCreated()
        // {
        //     int result = this.manager.create_user("testuser1354", "test@example.com1345", "hashed_password", "Test User", "bio", "pfp.png", "USA", "test.com");

        //     // Assuming the first user ID is 1. Adjust this based on your database state.
        //     Assert.AreEqual(1, result);
        // }

        [TestMethod]
        public void GetLastID_Test()
        {
            int lastId = this.manager.get_last_id();

            // Assert.AreEqual(11, lastId);

            Assert.IsTrue(lastId > 0);
        }
    }
}
