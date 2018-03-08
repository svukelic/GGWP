﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public bool CreateUser()
        {
            bool result = false;

            //create new user

            return result;
        }
    }
}