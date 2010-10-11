// <copyright file="OpenSourceLicenses.cs" company="Cédric Belin">
// 	Copyright (c) 2007-2009, Cédric Belin &lt;contact@cedric-belin.fr&gt;
// 	GNU Lesser General Public License (LGPLv3) - http://www.gnu.org/licenses/lgpl-3.0.txt
// </copyright>
// <summary>
// 	Implémentation de la classe <c>MiniFramework.Text.OpenSourceLicenses</c>.
// </summary>
// <author>$Author: cedx $</author>
// <date>$Date: 2009-09-10 19:12:59 +0200 (jeu. 10 sept. 2009) $</date>
// <version>$Revision: 1817 $</version>

namespace MiniFramework.Text
{
	using System;
	using System.Linq;
	using System.Resources;

	using MiniFramework.Properties;

	//// ========================================================================================

	/// <summary>
	/// Fournit une collection d'objets <see cref="License" /> représentant des licences Open Source communes.
	/// </summary>
	public static class OpenSourceLicenses
	{
		/// <summary>
		/// Obtient l'accord de licence &quot;GNU General Public License&quot; version 1.
		/// </summary>
		/// <value>Licence &quot;GNU General Public License&quot; version 1.</value>
		public static License GnuGeneralPublicLicenseV1
		{
			get { return new License { Name="GNU General Public License Version 1", Text=Resources.GPLv1 }; }
		}

		/// <summary>
		/// Obtient l'accord de licence &quot;GNU General Public License&quot; version 2.
		/// </summary>
		/// <value>Licence &quot;GNU General Public License&quot; version 2.</value>
		public static License GnuGeneralPublicLicenseV2
		{
			get { return new License { Name="GNU General Public License Version 2", Text=Resources.GPLv2 }; }
		}

		/// <summary>
		/// Obtient l'accord de licence &quot;GNU General Public License&quot; version 3.
		/// </summary>
		/// <value>Licence &quot;GNU General Public License&quot; version 3.</value>
		public static License GnuGeneralPublicLicenseV3
		{
			get { return new License { Name="GNU General Public License Version 3", Text=Resources.GPLv3 }; }
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient l'accord de licence &quot;GNU Lesser General Public License&quot; version 2.1.
		/// </summary>
		/// <value>Licence &quot;GNU Lesser General Public License&quot; version 2.1.</value>
		public static License GnuLesserGeneralPublicLicenseV2
		{
			get { return new License { Name="GNU Lesser General Public License Version 2.1", Text=Resources.LGPLv2 }; }
		}

		/// <summary>
		/// Obtient l'accord de licence &quot;GNU Lesser General Public License&quot; version 3.
		/// </summary>
		/// <value>Licence &quot;GNU Lesser General Public License&quot; version 3.</value>
		public static License GnuLesserGeneralPublicLicenseV3
		{
			get { return new License { Name="GNU Lesser General Public License Version 3", Text=Resources.LGPLv3 }; }
		}

		//// =====================================================================================

		/// <summary>
		/// Obtient l'accord de licence &quot;Microsoft Public License&quot;.
		/// </summary>
		/// <value>Licence &quot;Microsoft Public License&quot;.</value>
		public static License MicrosoftPublicLicense
		{
			get { return new License { Name="Microsoft Public License", Text=Resources.MsPL }; }
		}

		/// <summary>
		/// Obtient l'accord de licence &quot;Microsoft Reciprocal License&quot;.
		/// </summary>
		/// <value>Licence &quot;Microsoft Reciprocal License&quot;.</value>
		public static License MicrosoftReciprocalLicense
		{
			get { return new License { Name="Microsoft Reciprocal License", Text=Resources.MsRL }; }
		}
	}
}