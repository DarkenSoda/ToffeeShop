namespace CS251_A3_ToffeeShop.User
{
    public class User
    {
        protected string? name;
        protected string? userName;
        protected string? password;
        protected string? phoneNumber;
        protected string? emailAdress;

        public void SetName(string Name)
        {
            name = Name;
        }
        public string GetName()
        {
            return name;
        }
        public void SetUsername(string username)
        {
            userName = username;
        }
        public string GetUsername()
        {
            return userName;
        }
        public void SetPassword(string Password)
        {
            password = Password;
        }
        public string GetPassword()
        {
            return password;
        }
        public void SetPhonenumber(string Phonenumber)
        {
            phoneNumber = Phonenumber;
        }
        public string GetPhonenumber()
        {
            return phoneNumber;
        }
        public void SetEmail(string Email)
        {
            emailAdress = Email;
        }
        public string GetEmail()
        {
            return emailAdress;
        }
        public void UserInterface(){

        }
        /*public void Authernicate()
        {
        }
        public void BrowseCatalogue(cataloge Catalogue){

        }*/
    }
}