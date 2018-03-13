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
                    result.SetResults(KorisnikToModel(kor), true);
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

                result.SetResults(user, true);
            }

            return result;
        }

        public static UserModel GetKorisnikData(int uID)
        {
            UserModel um = null;

            using (var db = new ggwpDBEntities())
            {
                um = KorisnikToModel(db.Korisnik.Find(uID));
            }

            return um;
        }

        private static UserModel KorisnikToModel(Korisnik k)
        {
            UserModel um = new UserModel();

            um.username = k.username;
            //um.password = user.password;
            um.email = k.email;
            um.ime = k.ime;
            um.dob = k.dob;

            return um;
        }
    }
}
