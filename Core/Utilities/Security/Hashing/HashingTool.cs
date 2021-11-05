using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing {
    public class HashingTool {
        /*
         * WARNING!!!
         * 
         * System.Security.Cryptography.HMACSHA512(byte[] key) constructor has a weird behavior: If the given key's length 
         * is longer than the expected length (128), it will use "the hashed value of the given key" as the key.
         * If the DB column you store the key has a longer data length, it will be a problem. You will be able to register,
         * but not be able to login (since it doesn't use the same key for checking the password).
         * 
         * Side note: Because of the way we compare the hashes, you might also get wrong results or even IndexOutOfRange 
         * exception when the byte arrays are of different length. So you better set the DB column's length to the exact 
         * length of data.
         * 
         * References:
         * - https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha512.-ctor?view=net-5.0
         */

        public static void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var alg = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = alg.Key;
                passwordHash = alg.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string passwordToBeVerified, byte[] passwordHash, byte[] passwordSalt) {
            using (var alg = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                byte[] passwordHashToBeVerified = alg.ComputeHash(Encoding.UTF8.GetBytes(passwordToBeVerified));
                for (int i = 0; i < passwordHash.Length; i++) {
                    if (passwordHashToBeVerified[i] != passwordHash[i]) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
