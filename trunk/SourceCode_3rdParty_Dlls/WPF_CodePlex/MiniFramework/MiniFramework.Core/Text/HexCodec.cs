// <copyright file="HexCodec.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Text.HexCodec</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Text
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	
	using MiniFramework.Properties;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes de codage et de décodage de données au format hexadécimal.
	/// </summary>
	public static class HexCodec
	{
		/// <summary>
		/// Code tous les caractères de la chaîne hexadécimale spécifiée en une séquence d'octets. 
		/// </summary>
		/// <param name="value">Chaîne contenant la séquence de caractères hexadécimaux à coder.</param>
		/// <returns>Tableau d'octets contenant les résultats du codage de la chaîne spécifiée.</returns>
		/// <exception cref="ArgumentNullException">La chaîne spécifiée est une référence null.</exception>
		/// <exception cref="FormatException">La chaîne spécifiée contient un nombre impair de caractères ou des caractères invalides.</exception>
		public static byte[] GetBytes(string value)
		{
			// Validation des arguments
			if(value==null) throw new ArgumentNullException("value");
			if((value.Length%2)!=0) throw new FormatException(Resources.OddCharacterCount);

			// Chaque octet est codé avec 2 caractères hexadécimaux
			var bytes=new byte[value.Length/2];
			
			// Remplissage du tableau
			for(int i=0; i<bytes.Length; i++) bytes[i]=byte.Parse(value.Substring(i*2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			return bytes;
		}
		
		/// <summary>
		/// Décode tous les octets du tableau d'octets spécifié en une chaîne hexadécimale.
		/// </summary>
		/// <param name="value">Tableau d'octets contenant la séquence d'octets à décoder.</param>
		/// <returns>Chaîne hexadécimale contenant les résultats du décodage de la séquence d'octets spécifiée.</returns>
		/// <exception cref="ArgumentNullException">La séquence spécifiée est une référence null.</exception>
		public static string GetString(byte[] value)
		{
			// Validation des arguments
			if(value==null) throw new ArgumentNullException("value");
			
			// Chaque octet est codé avec 2 caractères hexadécimaux
			var builder=new StringBuilder(value.Length*2);
			
			// Remplissage de la chaîne
			foreach(var item in value) builder.Append(item.ToString("x2", CultureInfo.InvariantCulture));
			return builder.ToString();
		}
	}
}