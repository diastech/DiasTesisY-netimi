using DiasShared.Extensions.SecurityExtensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;

namespace DiasShared.Operations.SecurityOperations
{
    /// <summary>
    /// Optimize edilmiş Microsoft destekli github kütüphanesidir
    /// Orjinali generic olduğundan ve projeye uyumlu olmadığından, non generic yapıldı
    /// Yorumlar Türkçeleştirildi(takım isterse tekrar İngilizceleştirilecek)    
    /// ExtendedPasswordHasherOptions eklendi
    /// </summary>
    public class SaltOperations
    {
        /* =======================
         * HASHED PASSWORD FORMATS
         * =======================
         *
         * Versiyon 2:
         * HMAC-SHA1 ile PBKDF2, 128-bit salt, 256-bit subkey, 1000 iterasyon.
         * (Bakınız: SDL crypto guidelines v5.1, Part III)
         * Format: { 0x00, salt, subkey }
         *
         * Versiyon 3:
         * HMAC-SHA256 ile PBKDF2, 128-bit salt, 256-bit subkey, 10000 iterasyon.
         * Format: { 0x01, prf (UInt32), iter sayısı (UInt32), salt uzunluğu (UInt32), salt, subkey }
         * (Tüm UInt32 ler big-endian olarak depolanır.)
         */

        private readonly PasswordHasherCompatibilityMode _compatibilityMode;

        private readonly int _iterCount;

        private readonly RandomNumberGenerator _rng;

        /// <summary>
        /// Yeni bir instance üret <bakınız cref="ExtendedPasswordHasherOptions"/>.
        /// </summary>
        /// <param name="optionsAccessor">Bu instance için options.</param>
        public SaltOperations(IOptions<ExtendedPasswordHasherOptions> optionsAccessor = null)
        {
            ExtendedPasswordHasherOptions options = optionsAccessor?.Value ?? new ExtendedPasswordHasherOptions();

            _compatibilityMode = options.CompatibilityMode;

            switch (_compatibilityMode)
            {
                case PasswordHasherCompatibilityMode.IdentityV2:
                    {
                        // Yapacak bir şey yok
                        break;
                    }

                case PasswordHasherCompatibilityMode.IdentityV3:
                    {
                        _iterCount = options.IterationCount;

                        if (_iterCount < 1)
                        {
                            //TODO : CRUD Error'a eklenecek
                            throw new InvalidOperationException("InvalidPasswordHasherIterationCount");
                        }

                        break;
                    }

                default:
                    {
                        //TODO : CRUD Error'a eklenecek
                        throw new InvalidOperationException("InvalidPasswordHasherCompatibilityMode");
                    }

            }

            _rng = options.Rng;
        }


        /// <summary>
        /// Verilen <paramref name="password"/> in hashed gösterimini dündürür.
        /// </summary>
        /// <param name="password">Hashlenecek şifre.</param>
        /// <returns> Verilen <paramref name="password"/> in hashed gösterimini dündürür.</returns>
        public virtual string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (_compatibilityMode == PasswordHasherCompatibilityMode.IdentityV2)
            {
                return Convert.ToBase64String(HashPasswordV2(password, _rng));
            }
            else
            {
                return Convert.ToBase64String(HashPasswordV3(password, _rng));
            }
        }

        private static byte[] HashPasswordV2(string password, RandomNumberGenerator rng)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // Rfc2898DeriveBytes için standart

            const int Pbkdf2IterCount = 1000; // Rfc2898DeriveBytes için standart

            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bit

            const int SaltSize = 128 / 8; // 128 bit

            //Versiyon 2 (aşağıdaki yorumlara bakınız) text hash üret.

            byte[] salt = new byte[SaltSize];

            rng.GetBytes(salt);

            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];

            outputBytes[0] = 0x00; // format işareti

            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);

            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);

            return outputBytes;
        }

        private byte[] HashPasswordV3(string password, RandomNumberGenerator rng)
        {
            return HashPasswordV3(password, rng,  prf: KeyDerivationPrf.HMACSHA256, iterCount: _iterCount,  saltSize: 128 / 8,
                                                                                                             numBytesRequested: 256 / 8);
        }

        private static byte[] HashPasswordV3(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, 
                                                                                                                               int numBytesRequested)
        {
            //Versiyon 3 (aşağıdaki yorumlara bakınız) text hash üret.

            byte[] salt = new byte[saltSize];

            rng.GetBytes(salt);

            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];

            outputBytes[0] = 0x01; // format işareti

            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);

            WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);

            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);

            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);

            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);

            return outputBytes;
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return (((uint)(buffer[offset + 0]) << 24)  | ((uint)(buffer[offset + 1]) << 16)  | ((uint)(buffer[offset + 2]) << 8)
                                                                                                  | ((uint)(buffer[offset + 3])));
        }

        /// <summary>
        /// Şifre hash karşılaştırmasında sonuç olarak <bakınız cref="PasswordVerificationResult"/> değerini döndürür.
        /// </summary>
        /// <param name="hashedPassword">Şifrenin saklanmış hash edilmiş versiyonu.</param>
        /// <param name="providedPassword">Karşılaştırılacak şifre.</param>
        /// <returnsŞifre hash karşılaştırmasında sonuç olarak <bakınız cref="PasswordVerificationResult"/> değerini döndürür.</returns>
        /// <remarks>Bu methodun kullanımı zaman tutarlı olmalıdır
        /// Eski hash metodları ile üretilen hash, yeni hash metodları ile karşılaştırılmamalı</remarks>
        public virtual PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }

            if (providedPassword == null)
            {
                throw new ArgumentNullException(nameof(providedPassword));
            }

            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // hashlenmiş şifreden format işaretini oku
            if (decodedHashedPassword.Length == 0)
            {
                return PasswordVerificationResult.Failed;
            }

            switch (decodedHashedPassword[0])
            {
                case 0x00:
                    {
                        if (VerifyHashedPasswordV2(decodedHashedPassword, providedPassword))
                        {
                            // Bu eski şifreleme formatı -çağıran eğer eski uyumluluk modunda değilse yeniden hashlemeli
                            return (_compatibilityMode == PasswordHasherCompatibilityMode.IdentityV3)
                                ? PasswordVerificationResult.SuccessRehashNeeded
                                : PasswordVerificationResult.Success;
                        }
                        else
                        {
                            return PasswordVerificationResult.Failed;
                        }
                    }

                case 0x01:
                    {
                        int embeddedIterCount;
                        if (VerifyHashedPasswordV3(decodedHashedPassword, providedPassword, out embeddedIterCount))
                        {
                            // Eğer bu hashleyici daha yüksek iterasyon sayısı ile çalışıyorsa girişi şimdi değiştir.
                            return (embeddedIterCount < _iterCount)
                                ? PasswordVerificationResult.SuccessRehashNeeded
                                : PasswordVerificationResult.Success;
                        }
                        else
                        {
                            return PasswordVerificationResult.Failed;
                        }
                    }

                default:
                    {
                        return PasswordVerificationResult.Failed; // bilinmeyen format işaretçisi
                    }
            }
        }

        private static bool VerifyHashedPasswordV2(byte[] hashedPassword, string password)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // Rfc2898DeriveBytes için standart

            const int Pbkdf2IterCount = 1000; // Rfc2898DeriveBytes için standart

            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bit

            const int SaltSize = 128 / 8; // 128 bit

            //Geçerli hashlenmiş şifrenin boyutunu önceden biliyoruz
            if (hashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
            {
                return false; // geçersiz boyut
            }

            byte[] salt = new byte[SaltSize];

            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];

            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Gelen şifreyi hashle ve kontrol et
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }

        private static bool VerifyHashedPasswordV3(byte[] hashedPassword, string password, out int iterCount)
        {
            iterCount = default(int);

            try
            {
                // Başlık bilgisini oku
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);

                iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);

                int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

                // Saltı oku; >= 128 bit olmalı
                if (saltLength < 128 / 8)
                {
                    return false;
                }

                byte[] salt = new byte[saltLength];

                Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

                // Alt anahtarı oku (geri kalan data); >= 128 bit olamlı
                int subkeyLength = hashedPassword.Length - 13 - salt.Length;

                if (subkeyLength < 128 / 8)
                {
                    return false;
                }

                byte[] expectedSubkey = new byte[subkeyLength];

                Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Gelen şifreyi hashle ve kontrol et
                byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);

                return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
            }
            catch
            {
                // Data bozuk değilse buraya girmemesi gerek
                return false;
            }
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);

            buffer[offset + 1] = (byte)(value >> 16);

            buffer[offset + 2] = (byte)(value >> 8);

            buffer[offset + 3] = (byte)(value >> 0);
        }
    }
}
