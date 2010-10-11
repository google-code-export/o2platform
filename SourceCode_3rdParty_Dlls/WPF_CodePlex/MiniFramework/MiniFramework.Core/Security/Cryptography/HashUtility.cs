// <copyright file="HashUtility.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Security.Cryptography.HashUtility</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Security.Cryptography
{
	using System;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;
	
	using MiniFramework.Text;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes statiques pour le calcul de valeurs de hachage.
	/// </summary>
	public static class HashUtility
	{
		/// <summary>
		/// Calcule la valeur de hachage CRC32 pour le tableau d'octets spécifié.
		/// </summary>
		/// <param name="buffer">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme d'entier 32 bits non signé.</returns>
		[CLSCompliant(false)]
		public static uint ComputeCrc32(byte[] buffer)
		{
			return BitConverter.ToUInt32(Crc32.Create().ComputeHash(buffer), 0);
		}
		
		/// <summary>
		/// Calcule la valeur de hachage CRC32 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme d'entier 32 bits non signé.</returns>
		[CLSCompliant(false)]
		public static uint ComputeCrc32(string value)
		{
			return ComputeCrc32(value, null);
		}
		
		/// <summary>
		/// Calcule la valeur de hachage CRC32 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <param name="encoding">Codage utilisé par les caractères de la chaîne.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme d'entier 32 bits non signé.</returns>
		[CLSCompliant(false)]
		public static uint ComputeCrc32(string value, Encoding encoding)
		{
			if(encoding==null) encoding=Encoding.Default;
			return ComputeCrc32(encoding.GetBytes(value));
		}

		//// =====================================================================================

		/// <summary>
		/// Calcule la valeur de hachage MD5 pour le tableau d'octets spécifié.
		/// </summary>
		/// <param name="buffer">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeMD5(byte[] buffer)
		{
			return HexCodec.GetString(MD5.Create().ComputeHash(buffer));
		}
		
		/// <summary>
		/// Calcule la valeur de hachage MD5 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeMD5(string value)
		{
			return ComputeMD5(value, null);
		}
		
		/// <summary>
		/// Calcule la valeur de hachage MD5 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <param name="encoding">Codage utilisé par les caractères de la chaîne.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeMD5(string value, Encoding encoding)
		{
			if(encoding==null) encoding=Encoding.Default;
			return ComputeMD5(encoding.GetBytes(value));
		}

		//// =====================================================================================

		/// <summary>
		/// Calcule la valeur de hachage SHA-1 pour le tableau d'octets spécifié.
		/// </summary>
		/// <param name="buffer">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha1(byte[] buffer)
		{
			return HexCodec.GetString(SHA1.Create().ComputeHash(buffer));
		}
		
		/// <summary>
		/// Calcule la valeur de hachage SHA-1 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha1(string value)
		{
			return ComputeSha1(value, null);
		}
		
		/// <summary>
		/// Calcule la valeur de hachage SHA-1 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <param name="encoding">Codage utilisé par les caractères de la chaîne.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha1(string value, Encoding encoding)
		{
			if(encoding==null) encoding=Encoding.Default;
			return ComputeSha1(encoding.GetBytes(value));
		}

		//// =====================================================================================

		/// <summary>
		/// Calcule la valeur de hachage SHA-256 pour le tableau d'octets spécifié.
		/// </summary>
		/// <param name="buffer">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha256(byte[] buffer)
		{
			return HexCodec.GetString(SHA256.Create().ComputeHash(buffer));
		}
		
		/// <summary>
		/// Calcule la valeur de hachage SHA-256 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha256(string value)
		{
			return ComputeSha256(value, null);
		}
		
		/// <summary>
		/// Calcule la valeur de hachage SHA-256 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <param name="encoding">Codage utilisé par les caractères de la chaîne.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha256(string value, Encoding encoding)
		{
			if(encoding==null) encoding=Encoding.Default;
			return ComputeSha256(encoding.GetBytes(value));
		}

		//// =====================================================================================

		/// <summary>
		/// Calcule la valeur de hachage SHA-384 pour le tableau d'octets spécifié.
		/// </summary>
		/// <param name="buffer">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha384(byte[] buffer)
		{
			return HexCodec.GetString(SHA384.Create().ComputeHash(buffer));
		}
		
		/// <summary>
		/// Calcule la valeur de hachage SHA-384 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha384(string value)
		{
			return ComputeSha384(value, null);
		}
		
		/// <summary>
		/// Calcule la valeur de hachage SHA-384 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <param name="encoding">Codage utilisé par les caractères de la chaîne.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha384(string value, Encoding encoding)
		{
			if(encoding==null) encoding=Encoding.Default;
			return ComputeSha384(encoding.GetBytes(value));
		}

		//// =====================================================================================

		/// <summary>
		/// Calcule la valeur de hachage SHA-512 pour le tableau d'octets spécifié.
		/// </summary>
		/// <param name="buffer">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha512(byte[] buffer)
		{
			return HexCodec.GetString(SHA512.Create().ComputeHash(buffer));
		}
		
		/// <summary>
		/// Calcule la valeur de hachage SHA-512 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha512(string value)
		{
			return ComputeSha512(value, null);
		}
		
		/// <summary>
		/// Calcule la valeur de hachage SHA-512 pour la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <param name="encoding">Codage utilisé par les caractères de la chaîne.</param>
		/// <returns>Code de hachage calculé, exprimé sous forme de chaîne hexadécimale.</returns>
		public static string ComputeSha512(string value, Encoding encoding)
		{
			if(encoding==null) encoding=Encoding.Default;
			return ComputeSha512(encoding.GetBytes(value));
		}
	}
}