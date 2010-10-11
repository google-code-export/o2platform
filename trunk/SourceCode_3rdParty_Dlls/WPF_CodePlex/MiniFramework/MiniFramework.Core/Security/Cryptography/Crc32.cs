// <copyright file="Crc32.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Security.Cryptography.Crc32</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Security.Cryptography
{
	using System;
	using System.Linq;
	using System.Security.Cryptography;

	//// ========================================================================================

	/// <summary>
	/// Calcule la valeur de hachage CRC32 pour les données d'entrée.
	/// </summary>
	public abstract class Crc32: HashAlgorithm
	{
		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="Crc32" />.
		/// </summary>
		protected Crc32() {}

		//// =====================================================================================

		/// <summary>
		/// Crée une instance de l'implémentation par défaut de l'algorithme de hachage CRC32.
		/// </summary>
		/// <returns>Nouvelle instance de l'algorithme de hachage CRC32.</returns>
		public static new Crc32 Create()
		{
			return Create("MiniFramework.Security.Cryptography.Crc32");
		}

		/// <summary>
		/// Crée une instance de l'implémentation spécifiée de l'algorithme de hachage CRC32.
		/// </summary>
		/// <param name="algName">Nom de l'implémentation spécifique de CRC32 à utiliser.</param>
		/// <returns>Nouvelle instance de l'algorithme de hachage CRC32.</returns>
		public static new Crc32 Create(string algName)
		{
			var algorithm=CryptoConfig.CreateFromName(algName);
			return (Crc32) (algorithm ?? new Crc32Managed());
		}
	}
}