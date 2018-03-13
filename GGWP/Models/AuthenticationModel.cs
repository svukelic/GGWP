using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GGWP.Models.db;
using GGWP.Models.Entities;
using GGWP.Security;

namespace GGWP.Models
{
    public class AuthenticationModel
    {
        //public string id;
        public string session;
        public string username;
        public string password;
        public string response;

        public object data;

        public AuthenticationModel(LoginModel lm)
        {
            this.session = "attempting_login_session";
            this.username = lm.username;
            this.password = lm.password;
            this.response = "404";
        }

        public bool LoginUser()
        {
            LoginModel model = new LoginModel();
            model.username = this.username;
            model.password = this.password;
            bool test = false;

            //get from db
            QueryManager queryManager = new QueryManager();
            ResultModel result = queryManager.InitiateQuery("Login", model);
            if (result.data != null)
            {
                UserModel lm = (UserModel) result.obj;
                this.data = (UserModel) result.obj;

                //string dbSession = "checking_login_session";
                string dbUsername = lm.username;
                string dbPassword = lm.password;
                string dbSalt = "ggwp";

                //check if correct
                /*if (session.Equals("-1"))
                {
                    //add to db
                    dbSession = this.session;
                }
                else if (!dbSession.Equals(this.session))
                {
                    return false;
                }*/

                PWSHasher pwHasher = new PWSHasher();
                byte[] saltBytes = Encoding.ASCII.GetBytes(dbSalt);
                HashWithSaltResult hashResult = pwHasher.HashWithGivenSalt(this.password, 64, saltBytes);

                if (hashResult.Digest.Equals(dbPassword) && this.username.Equals(dbUsername))
                {
                    test = true;
                }

                return test;
            }
            else return false;
        }

        public static ResultModel LoginUser(LoginModel user)
        {
            ResultModel result = new ResultModel();

            //login user

            using (var db = new ggwpDBEntities())
            {
                string dbSalt = "ggwp";
                PWSHasher pwHasher = new PWSHasher();
                byte[] saltBytes = Encoding.ASCII.GetBytes(dbSalt);
                HashWithSaltResult hashResult = pwHasher.HashWithGivenSalt(user.password, 64, saltBytes);

                Korisnik kor = null;
                try
                {
                    kor = db.Korisnik.Where(x => x.username.Equals(user.username)).SingleOrDefault();
                }
                catch (InvalidOperationException ex)
                {
                    return result;
                }

                if (hashResult.Digest.Equals(kor.password) && user.username.Equals(kor.username))
                {
                    result.SetResults(kor, true);
                }
            }

            return result;
        }

        public static ResultModel CreateUser(UserModel user)
        {
            ResultModel result = new ResultModel();

            //create new user
            PWSHasher pwHasher = new PWSHasher();
            byte[] saltBytes = Encoding.ASCII.GetBytes("ggwp");
            HashWithSaltResult hashResult = pwHasher.HashWithGivenSalt(user.password, 64, saltBytes);

            user.password = hashResult.Digest;

            using (var db = new ggwpDBEntities())
            {
                Korisnik newKorisnik = new Korisnik();
                newKorisnik.username = user.username;
                newKorisnik.password = user.password;
                newKorisnik.email = user.email;
                newKorisnik.ime = user.ime;
                newKorisnik.dob = user.dob;

                db.Korisnik.Add(newKorisnik);
                db.SaveChanges();

                user.id = newKorisnik.id;

                result.SetResults(newKorisnik, true);
            }

            return result;
        }
    }
}
