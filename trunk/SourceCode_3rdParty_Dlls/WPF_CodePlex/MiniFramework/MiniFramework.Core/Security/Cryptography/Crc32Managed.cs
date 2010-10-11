// <copyright file="Crc32Managed.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Security.Cryptography.Crc32Managed</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Security.Cryptography
{
	using System;
	using System.Linq;

	//// ========================================================================================

	/// <summary>
	/// Fournit une implémentation gérée de l'algorithme de hachage CRC32.
	/// </summary>
	[CLSCompliant(false)]
	public sealed class Crc32Managed: Crc32
	{
		/// <summary>
		/// Code de hachage calculé.
		/// </summary>
		private uint hash;

		/// <summary>
		/// Table par défaut des sommes de contrôles des valeurs codées sur 8 bits.
		/// </summary>
		private static readonly uint[] table=InitializeTable(0xEDB88320);

		//// =====================================================================================

		/// <summary>
		/// Initialise une nouvelle instance de la classe <see cref="Crc32Managed" />.
		/// </summary>
		public Crc32Managed()
		{
			this.HashSizeValue=32;
			this.Initialize();
		}

		//// =====================================================================================

		/// <summary>
		/// Initialise l'instance.
		/// </summary>
		public override void Initialize()
		{
			this.hash=0xFFFFFFFF;
		}

		//// =====================================================================================

		/// <summary>
		/// Achemine les données écrites dans l'objet vers l'algorithme de hachage pour calculer le hachage.
		/// </summary>
		/// <param name="array">Entrée pour laquelle le code de hachage doit être calculé.</param>
		/// <param name="ibStart">Index dans le tableau d'entrée à partir duquel l'utilisation des données commence.</param>
		/// <param name="cbSize">Nombre d'octets dans le tableau d'entrée à utiliser comme données.</param>
		/// <exception cref="ArgumentNullException">L'entrée spécifiée est une référence null.</exception>
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if(array==null) throw new ArgumentNullException("array");
				
			this.State=1;
			for(int i=ibStart; i<cbSize; i++) this.hash=(this.hash>>8) ^ table[array[i] ^ (this.hash & 0xFF)];
		}

		/// <summary>
		/// Finalise le calcul de hachage une fois les dernières données traitées par l'objet de flux de chiffrement.
		/// </summary>
		/// <returns>Code de hachage calculé.</returns>
		protected override byte[] HashFinal()
		{
			this.HashValue=BitConverter.GetBytes(~this.hash);
			this.State=0;

			return this.HashValue;
		}

		//// =====================================================================================

		/// <summary>
		/// Initialise la table des sommes de contrôle d'un octet à partir du polynôme spécifié.
		/// </summary>
		/// <param name="polynomial">Polynôme utilisé pour générer la table des sommes de contrôle.</param>
		/// <returns>Table des sommes de contrôles des valeurs codées sur 8 bits.</returns>
		private static uint[] InitializeTable(uint polynomial)
		{
			var table=new uint[256];

			for(int i=0; i<table.Length; i++)
			{
				var value=(uint) i;

				for(int j=0; j<8; j++)
				{
					if((value & 1)!=0) value=(value>>1)^polynomial;
					else value>>=1;
				}

				table[i]=value;
			}

			return table;
		}
	}
}