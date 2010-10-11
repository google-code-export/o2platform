// <copyright file="StringExtensions.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.StringExtensions</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-10-03 20:13:52 +0200 (sam. 03 oct. 2009) $</date>
// <version>$Revision: 1984 $</version>

namespace MiniFramework
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;

	//// ========================================================================================

	/// <summary>
	/// Fournit des méthodes d'extension pour la gestion des chaînes de caractères.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Liste des espaces blancs.
		/// </summary>
		private static readonly char[] whiteSpaces=
		{
			'\t', // Tabulation (\u0009)
			'\f', // Saut de ligne (\u000A)
			'\v', // Tabulation verticale (\u000B)
			'\n', // Saut de page (\u000C)whiteSpaces
			'\r', // Retour chariot (\u000D)
			' ', // Espace (\u0020)
			'\u00A0', // Espace insécable
			'\u2000', // Demi-cadratin
			'\u2001', // Cadratin
			'\u2002', // Espace demi-cadratin
			'\u2003', // Espace cadratin
			'\u2004', // Tiers de cadratin
			'\u2005', // Quart de cadratin
			'\u2006', // Sixième de cadratin
			'\u2007', // Espace tabulaire
			'\u2008', // Espace ponctuation
			'\u2009', // Espace fine
			'\u200A', // Espace ultrafine
			'\u200B', // Espace sans chasse
			'\u3000', // Espace idéographique
			'\uFEFF' // Espace insécable sans chasse
		};
		
		//// ========================================================================================

		/// <summary>
		/// Met en majuscule la première lettre de la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Chaîne à formater.</param>
		/// <returns>Chaîne dont la première lettre a été mise en majuscule.</returns>
		public static string Capitalize(this string value)
		{
			if(string.IsNullOrEmpty(value)) return value;
			return Char.ToUpper(value[0], CultureInfo.CurrentCulture)+value.Substring(1);
		}

		/// <summary>
		/// Met en majuscule la première lettre de tous les mots de la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Chaîne à formater.</param>
		/// <returns>Chaîne dont la première lettre de chaque mot a été mise en majuscule.</returns>
		public static string CapitalizeWords(this string value)
		{
			return CapitalizeWords(value, null);
		}

		/// <summary>
		/// Met en majuscule la première lettre de tous les mots de la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Chaîne à formater.</param>
		/// <param name="delimiters">Ensemble de caractères utilisés pour déterminer les mots de la chaîne.</param>
		/// <returns>Chaîne dont la première lettre de chaque mot a été mise en majuscule.</returns>
		public static string CapitalizeWords(this string value, params char[] delimiters)
		{
			return ProcessWords(Capitalize, value, delimiters);
		}
		
		//// ========================================================================================

		/// <summary>
		/// Centre une chaîne au sein d'une chaîne plus grande de longueur donnée.
		/// </summary>
		/// <remarks>
		/// Une taille négative est traitée comme zéro.
		/// </remarks>
		/// <param name="value">Chaîne à centrer.</param>
		/// <param name="size">La taille de la nouvelle chaîne.</param>
		/// <returns>Chaîne centrée.</returns>
		public static string Center(this string value, int size)
		{
			return Center(value, size, ' ');
		}

		/// <summary>
		/// Centre une chaîne au sein d'une chaîne plus grande de longueur donnée remplie à l'aide du caractère spécifié.
		/// </summary>
		/// <remarks>
		/// Une taille négative est traitée comme zéro.
		/// </remarks>
		/// <param name="value">Chaîne à centrer.</param>
		/// <param name="size">La taille de la nouvelle chaîne.</param>
		/// <param name="padCharacter">Caractère de remplissage.</param>
		/// <returns>Chaîne centrée.</returns>
		/// <exception cref="ArgumentNullException">La chaîne spécifiée est une référence null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">La taille spécifiée est inférieure à zéro.</exception>
		public static string Center(this string value, int size, char padCharacter)
		{
			if(value==null) throw new ArgumentNullException("value");
			if(size<0) throw new ArgumentOutOfRangeException("size");
			
			if(value.Length>=size) return value;

			// Replissage de la chaîne avec le caractère spécifié
			int padding=size-value.Length;

			value=value.PadLeft(value.Length+(padding/2), padCharacter);
			value=value.PadRight(size, padCharacter);

			return value;
		}
		
		//// ========================================================================================

		/// <summary>
		/// Retourne une valeur indiquant si les deux chaînes spécifiées sont équivalentes, c'est-à-dire si elles sont égales en ignorant leur casse, en utilisant les règles de comparaison de la culture en cours.
		/// </summary>
		/// <param name="value1">Première chaîne à comparer.</param>
		/// <param name="value2">Deuxième chaîne à comparer.</param>
		/// <returns><see langword="true" /> si les chaînes spécifiées sont équivalentes suivant les règles de la culture en cours ; sinon <see langword="false" />.</returns>
		public static bool EquivalentTo(this string value1, string value2)
		{
			return EquivalentTo(value1, value2, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Retourne une valeur indiquant si les deux chaînes spécifiées sont équivalentes, c'est-à-dire si elles sont égales en ignorant leur casse, en utilisant les règles de comparaison de la culture spécifiée.
		/// </summary>
		/// <param name="value1">Première chaîne à comparer.</param>
		/// <param name="value2">Deuxième chaîne à comparer.</param>
		/// <param name="culture">Informations relatives à la culture.</param>
		/// <returns><see langword="true" /> si les chaînes spécifiées sont équivalentes suivant les règles de la culture spécifiée ; sinon <see langword="false" />.</returns>
		public static bool EquivalentTo(this string value1, string value2, CultureInfo culture)
		{
			if(value1==null || value2==null) return value1==value2;
			return value1.ToUpper(culture)==value2.ToUpper(culture);
		}

		/// <summary>
		/// Retourne une valeur indiquant si les deux chaînes spécifiées sont équivalentes, c'est-à-dire si elles sont égales en ignorant leur casse, en utilisant les règles de comparaison de la culture dite indifférente.
		/// </summary>
		/// <param name="value1">Première chaîne à comparer.</param>
		/// <param name="value2">Deuxième chaîne à comparer.</param>
		/// <returns><see langword="true" /> si les chaînes spécifiées sont équivalentes suivant les règles de la culture indifférente ; sinon <see langword="false" />.</returns>
		public static bool EquivalentToInvariant(this string value1, string value2)
		{
			return EquivalentTo(value1, value2, CultureInfo.InvariantCulture);
		}

		//// ========================================================================================

		/// <summary>
		/// Extrait les lettres initiales de chaque mot de la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Chaîne dont on doit extraire les lettres initiales.</param>
		/// <returns>Chaîne constituée des initiales de chaque mot de <paramref name="value" />.</returns>
		public static string Initials(this string value)
		{
			return Initials(value, null);
		}

		/// <summary>
		/// Extrait les lettres initiales de chaque mot de la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Chaîne dont on doit extraire les lettres initiales.</param>
		/// <param name="delimiters">Ensemble de caractères utilisés pour déterminer les mots de la chaîne.</param>
		/// <returns>Chaîne constituée des initiales de chaque mot de <paramref name="value" />.</returns>
		public static string Initials(this string value, params char[] delimiters)
		{
			if(string.IsNullOrEmpty(value)) return value;

			var initials=
				from word in value.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
				select word[0];

			return new string(initials.ToArray());
		}

		//// ========================================================================================

		/// <summary>
		/// Met en minuscule la première lettre de la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Chaîne à formater.</param>
		/// <returns>Chaîne dont la première lettre a été mise en minuscule.</returns>
		public static string Uncapitalize(this string value)
		{
			if(string.IsNullOrEmpty(value)) return value;
			return Char.ToLower(value[0], CultureInfo.CurrentCulture)+value.Substring(1);
		}

		/// <summary>
		/// Met en minuscule la première lettre de tous les mots de la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Chaîne à formater.</param>
		/// <returns>Chaîne dont la première lettre de chaque mot a été mise en minuscule.</returns>
		public static string UncapitalizeWords(this string value)
		{
			return UncapitalizeWords(value, null);
		}

		/// <summary>
		/// Met en minuscule la première lettre de tous les mots de la chaîne spécifiée.
		/// </summary>
		/// <param name="value">Chaîne à formater.</param>
		/// <param name="delimiters">Ensemble de caractères utilisés pour déterminer les mots de la chaîne.</param>
		/// <returns>Chaîne dont la première lettre de chaque mot a été mise en minuscule.</returns>
		public static string UncapitalizeWords(this string value, params char[] delimiters)
		{
			return ProcessWords(Uncapitalize, value, delimiters);
		}

		//// =====================================================================================

		/// <summary>
		/// Effectue la césure de la chaîne spécifiée, en identifiant les mots par le caractère d'espacement (' ').
		/// </summary>
		/// <remarks>
		/// Les espaces blancs en début de ligne sont enlevés ; les espaces blancs de fin de ligne sont préservés.
		/// </remarks>
		/// <param name="value">Chaîne à envelopper.</param>
		/// <param name="wrapLength">La colonne où insérer les séparateurs de ligne.</param>
		/// <returns>Une chaîne avec des sauts de ligne insérés.</returns>
		public static string Wrap(this string value, int wrapLength)
		{
			return Wrap(value, wrapLength, null, false);
		}

		/// <summary>
		/// Effectue la césure de la chaîne spécifiée, en identifiant les mots par le caractère d'espacement (' ').
		/// </summary>
		/// <remarks>
		/// Les espaces blancs en début de ligne sont enlevés ; les espaces blancs de fin de ligne sont préservés.
		/// </remarks>
		/// <param name="value">Chaîne à envelopper.</param>
		/// <param name="wrapLength">La colonne où insérer les séparateurs de ligne.</param>
		/// <param name="newLine">Chaîne à utiliser comme séparateur de ligne.</param>
		/// <param name="wrapLongWords">Valeur indiquant si on doit envelopper les mots longs.</param>
		/// <returns>Une chaîne avec des sauts de ligne insérés.</returns>
		/// <exception cref="ArgumentOutOfRangeException">La colonne spécifiée est inférieure ou égale à zéro.</exception>
		public static string Wrap(this string value, int wrapLength, string newLine, bool wrapLongWords)
		{
			if(string.IsNullOrEmpty(value) || wrapLength>=value.Length) return value;

			// Validation des arguments
			if(wrapLength<=0) throw new ArgumentOutOfRangeException("wrapLength");
			if(string.IsNullOrEmpty(newLine)) newLine=Environment.NewLine;

			// Enveloppement
			var builder=new StringBuilder();
			int offset=0;

			while((value.Length-offset)>wrapLength)
			{
				// Si les premiers caractères de la ligne en cours sont des espaces, on les saute
				if(value[offset]==' ')
				{
					offset++;
					continue;
				}

				// Détermination de la position où finit le mot courant
				int spaceToWrapAt=value.LastIndexOf(' ', wrapLength+offset);

				// Cas normal : le mot rentre sur la ligne
				if(offset<spaceToWrapAt)
				{
					builder.Append(value.Substring(offset, spaceToWrapAt-offset));
					builder.Append(newLine);

					offset=spaceToWrapAt+1;
				}
					
				// Cas particulier : mot trop long, il ne rentre pas sur la ligne
				else
				{
					// On coupe le mot, une ligne à la fois
					if(wrapLongWords)
					{
						builder.Append(value.Substring(offset, wrapLength));
						builder.Append(newLine);
						offset+=wrapLength;
					}
						
					// On ne coupe pas le mot : il s'étend au-delà de la longueur de ligne autorisée
					else
					{
						spaceToWrapAt=value.IndexOf(' ', wrapLength+offset);

						// Il reste encore des mots à traiter
						if(spaceToWrapAt>=0)
						{
							
							builder.Append(value.Substring(offset, spaceToWrapAt-offset));
							builder.Append(newLine);

							offset=spaceToWrapAt+1;
						}

						// Plus aucun autre mot : on concatène le reste de la chaîne
						else
						{
							builder.Append(value.Substring(offset));
							offset=value.Length;
						}
					}
				}
			}

			// Quoi qu'il reste dans la chaîne, cela tient sur la dernière ligne
			builder.Append(value.Substring(offset));

			return builder.ToString();
		}

		//// ========================================================================================

		/// <summary>
		/// Supprime tous les et commerciaux (&amp;) de la chaîne spécifiée.
		/// </summary>
		/// <remarks>
		/// Les et commerciaux doublés (&amp;&amp;) ne sont pas supprimés mais remplacés par un seul et commercial (&amp;).
		/// </remarks>
		/// <param name="value">Chaîne à modifier.</param>
		/// <returns>Chaîne dont les et commerciaux non doublés (&amp;) ont été supprimés.</returns>
		public static string StripAmpersands(this string value)
		{
			if(string.IsNullOrEmpty(value)) return value;
			return Regex.Replace(value, "&(.)", "$1");
		}
		
		/// <summary>
		/// Modifie la casse de la chaîne spécifiée en changeant les lettres minuscules en majuscules, et les lettres majuscules en minuscules.
		/// </summary>
		/// <param name="value">Chaîne dont on doit échanger la casse.</param>
		/// <returns>Chaîne dont la casse a été échangée.</returns>
		public static string SwapCase(this string value)
		{
			if(string.IsNullOrEmpty(value)) return value;

			// Modification de la casse des caractères
			var builder=new StringBuilder(value.Length);

			foreach(var character in value)
			{
				if(Char.IsLower(character)) builder.Append(Char.ToUpper(character, CultureInfo.CurrentCulture));
				else if(Char.IsUpper(character)) builder.Append(Char.ToLower(character, CultureInfo.CurrentCulture));
				else builder.Append(character);
			}

			return builder.ToString();
		}

		//// ========================================================================================

		/// <summary>
		/// Formate tous les mots de la chaîne spécifiée.
		/// </summary>
		/// <param name="handler">Délégué utilisé pour formater les mots.</param>
		/// <param name="value">Chaîne à formater.</param>
		/// <param name="delimiters">Ensemble de caractères utilisés pour déterminer les mots de la chaîne.</param>
		/// <returns>Chaîne formatée.</returns>
		private static string ProcessWords(Func<string, string> handler, string value, char[] delimiters)
		{
			if(string.IsNullOrEmpty(value)) return value;
			if(delimiters==null || delimiters.Length==0) delimiters=whiteSpaces;

			// Construction de la chaîne
			var builder=new StringBuilder();
			int lastIndex=value.Length-1, startIndex=0, endIndex;

			do
			{
				endIndex=value.IndexOfAny(delimiters, startIndex);
				if(endIndex<0) endIndex=lastIndex;

				builder.Append(handler(value.Substring(startIndex, (endIndex-startIndex)+1)));
				startIndex=endIndex+1;
			}
			while(startIndex<=lastIndex);

			return builder.ToString();
		}
	}
}