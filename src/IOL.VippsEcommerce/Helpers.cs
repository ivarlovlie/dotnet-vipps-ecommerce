using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IOL.VippsEcommerce
{
	internal static class Helpers
	{
		private const int AES_BLOCK_BYTE_SIZE = 128 / 8;
		private static readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

		public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
		public static bool IsPresent(this string value) => !string.IsNullOrWhiteSpace(value);

		public static bool IsDirectoryWritable(this string dirPath, bool throwIfFails = false) {
			try {
				using (var fs = File.Create(
				                            Path.Combine(dirPath, Path.GetRandomFileName()),
				                            1,
				                            FileOptions.DeleteOnClose)
				) { }

				return true;
			} catch {
				if (throwIfFails)
					throw;

				return false;
			}
		}

		//https://tomrucki.com/posts/aes-encryption-in-csharp/
		public static string EncryptWithAes(this string toEncrypt, string password) {
			var key = GetKey(password);

			using var aes = CreateAes();
			var iv = GenerateRandomBytes(AES_BLOCK_BYTE_SIZE);
			var plainText = Encoding.UTF8.GetBytes(toEncrypt);

			using var encryptor = aes.CreateEncryptor(key, iv);
			var cipherText = encryptor
				.TransformFinalBlock(plainText, 0, plainText.Length);

			var result = new byte[iv.Length + cipherText.Length];
			iv.CopyTo(result, 0);
			cipherText.CopyTo(result, iv.Length);

			return Convert.ToBase64String(result);
		}

		private static Aes CreateAes() {
			var aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			return aes;
		}

		public static string DecryptWithAes(this string input, string password) {
			var key = GetKey(password);
			var encryptedData = Convert.FromBase64String(input);

			using var aes = CreateAes();
			var iv = encryptedData.Take(AES_BLOCK_BYTE_SIZE).ToArray();
			var cipherText = encryptedData.Skip(AES_BLOCK_BYTE_SIZE).ToArray();

			using var decryptor = aes.CreateDecryptor(key, iv);
			var decryptedBytes = decryptor
				.TransformFinalBlock(cipherText, 0, cipherText.Length);
			return Encoding.UTF8.GetString(decryptedBytes);
		}

		private static byte[] GetKey(string password) {
			var keyBytes = Encoding.UTF8.GetBytes(password);
			using var md5 = MD5.Create();
			return md5.ComputeHash(keyBytes);
		}

		private static byte[] GenerateRandomBytes(int numberOfBytes) {
			var randomBytes = new byte[numberOfBytes];
			_random.GetBytes(randomBytes);
			return randomBytes;
		}
	}
}