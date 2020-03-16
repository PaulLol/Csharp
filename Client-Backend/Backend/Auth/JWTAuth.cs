using Backend.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Auth
{
    public class JWTAuth
    {
        private static string secret = "test";

        public static string GenerateToken(User user)
        {
            DateTime expires = DateTime.UtcNow.AddDays(1);
            var payload = new Dictionary<string, object>
            {
                {"user_id", user.Id},
                {"mail", user.Mail},
                {"expires", expires}
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);

            return token;
        }

        public static string GenerateToken(Temp temp)
        {
            var payload = new Dictionary<string, object>
            {
                {"user_id", temp.Id},
                {"mail", temp.Mail},
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);

            return token;
        }

        public static bool ValidateToken(string token, string userId)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(token, secret, verify: true);

                dynamic obj = JObject.Parse(json);
                TimeSpan ts = DateTime.UtcNow - DateTime.Parse(obj.expires.ToString());

                if (obj.user_id == userId && ts.Days < 1)
                {
                    return true;
                }

                return false;
            }
            catch (TokenExpiredException)
            {
                //Console.WriteLine("Token has expired");
                return false;
            }
            catch (SignatureVerificationException)
            {
                //Console.WriteLine("Token has invalid signature");
                return false;
            }
        }
    }
}
