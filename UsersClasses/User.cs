namespace CS251_A3_ToffeeShop.UsersClasses {
    public class User {
        protected string name;
        protected string userName;
        protected string password;
        protected string? phoneNumber;
        protected string emailAdress;
        protected bool isAuthenticated = false;

        public User(string name, string userName, string password, string emailAdress) {
            this.name = name;
            this.userName = userName;
            this.password = password;
            this.emailAdress = emailAdress;
        }

        public void SetName(string Name) {
            name = Name;
        }

        public string GetName() {
            return name;
        }

        public void SetUsername(string username) {
            userName = username;
        }

        public string GetUsername() {
            return userName;
        }

        public void SetPassword(string Password) {
            password = Password;
        }

        public string GetPassword() {
            return password;
        }

        public void SetPhonenumber(string Phonenumber) {
            phoneNumber = Phonenumber;
        }

        public string GetPhonenumber() {
            if (phoneNumber == null) return "";

            return phoneNumber;
        }

        public void SetEmail(string Email) {
            emailAdress = Email;
        }

        public string GetEmail() {
            return emailAdress;
        }

        public bool GetAuthentication() {
            return isAuthenticated;
        }

        // To load authentication
        // Can be used to Disable Authentication as well
        public void SetAuthentication(bool authenticate) {
            isAuthenticated = authenticate;
        }

        public void Authernicate() {
            if(isAuthenticated) {
                Console.WriteLine("Authentication Removed!");
                isAuthenticated = false;
                return;
            }
            
            if (!Authentication.Authenticate(emailAdress)) {
                Console.WriteLine("Failed To  Authenticate Account!");
                return;
            }

            Console.WriteLine("Authentication Added!");
            isAuthenticated = true;
        }
    }
}