using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace DiasShared.Extensions.SecurityExtensions
{
    public class ExtendedPasswordHasherOptions : PasswordHasherOptions
    {

        private static readonly RandomNumberGenerator _defaultRng = RandomNumberGenerator.Create(); // security PRNG

        public ExtendedPasswordHasherOptions() : base()
        {

        }

        internal RandomNumberGenerator Rng { get; set; } = _defaultRng;
    }
}
