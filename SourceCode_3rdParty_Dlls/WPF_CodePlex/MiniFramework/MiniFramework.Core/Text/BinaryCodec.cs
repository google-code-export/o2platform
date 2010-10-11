// <copyright file="BinaryCodec.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Text.BinaryCodec</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Text
{
	using System;
	using System.Linq;
	using System.Text;
	
	using MiniFramework.Properties;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes de codage et de décodage de données au format binaire (suites de 0 et de 1).
	/// </summary>
	public static class BinaryCodec
	{
		/// <summary>
		/// Code tous les caractères de la chaîne binaire spécifiée en une séquence d'octets. 
		/// </summary>
		/// <param name="value">Chaîne contenant la séquence de caractères binaires à coder.</param>
		/// <returns>Tableau d'octets contenant les résultats du codage de la chaîne spécifiée.</returns>
		/// <exception cref="ArgumentNullException">La chaîne spécifiée est une référence null.</exception>
		/// <exception cref="FormatException">La chaîne spécifiée contient un nombre impair de caractères ou des caractères invalides.</exception>
		public static byte[] GetBytes(string value)
		{
			// Validation des arguments
			if(value==null) throw new ArgumentNullException("value");
			if((value.Length%8)!=0) throw new FormatException(Resources.InvalidCharacterCount);

			// Chaque octet est codé avec 8 caractères binaires
			var bytes=new byte[value.Length/8];

			// Remplissage du tableau
			for(int i=0; i<bytes.Length; i++)
			{
				// Extraction de la valeur binaire d'un octet
				var binary=value.Substring(i*8, 8);
				for(int j=0; j<8; j++)
				{
					// Extraction du bit équivalent au caractère binaire
					var bit=(int) Char.GetNumericValue(binary, j);
					if(bit<0) throw new FormatException(Resources.IllegalBinaryCharacter);
					
					// Addition du bit multiplié par la puissance de 2 adéquate
					bytes[i]+=(byte) (bit<<(7-j));
				}
			}

			return bytes;
		}
		
		/// <summary>
		/// Décode tous les octets du tableau d'octets spécifié en une chaîne binaire.
		/// </summary>
		/// <param name="value">Tableau d'octets contenant la séquence d'octets à décoder.</param>
		/// <returns>Chaîne binaire contenant les résultats du décodage de la séquence d'octets spécifiée.</returns>
		/// <exception cref="ArgumentNullException">La séquence spécifiée est une référence null.</exception>
		public static string GetString(byte[] value)
		{
			// Validation des arguments
			if(value==null) throw new ArgumentNullException("value");
			
			// Chaque octet est codé avec 8 caractères binaires
			var builder=new StringBuilder(value.Length*8);
			
			// Remplissage de la chaîne
			foreach(var item in value)
			{
				var binary=new StringBuilder(8);
				
				// On procède par divisions successives de l'octet par 2 : le reste de la division
				// représente la valeur du bit qui a été isolé à chaque itération
				var data=item;
				for(int i=0; i<8; i++)
				{
					binary.Append((data%2)!=0 ? '1' : '0');
					data>>=1;
				}
				
				// La valeur binaire obtenue est inversée :
				// on la remet dans le bon sens avant de la stocker
				var characters=binary.ToString().ToCharArray();
				Array.Reverse(characters);
				builder.Append(characters);
			}
			
			return builder.ToString();
		}
	}
}